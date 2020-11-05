using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class EmployeeInfo : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    EmployeeBAL ebl2 = new EmployeeBAL();
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
        

        if (!IsPostBack)
        {
            //d.con1.Open();

            //MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT role_name FROM pay_role_master where comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            //MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //DropDownList1.DataSource = ds.Tables[0];
            //DropDownList1.DataTextField = "role_name";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("--Select Role--", ""));
           

            //ddl_permstate.Items.Clear();
            //ddl_state.Items.Clear();
            //  ddl_location.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_state_master ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                   // ddl_permstate.Items.Add(dr_item1[0].ToString());
                 //   ddl_state.Items.Add(dr_item1[0].ToString());
                    //  ddl_location.Text=(dr_item1[0].ToString());
                }
                dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }
          //  ddl_permstate.Items.Insert(0, new ListItem("Select"));
          //  ddl_state.Items.Insert(0, new ListItem("Select"));
          //  txt_presentcity.Items.Insert(0, new ListItem("Select"));
           
         //   ddl_unitcode.Items.Insert(0, new ListItem("Select", "Select"));
            if (Session["EMP_CODE"].ToString() != "")
            {
                getEmployee();
                Session["EMP_CODE"] = "";

            }
            
            // clien_namelist();
            newpanel.Visible = true;
            client_list();
           
           
        }
      
        Page.Form.Attributes.Add("enctype", "multipart/form-data");


        newpanel.Visible = true;
        set_data();
        
       
       
        getEmployee();
        text_Clear();

        d.con.Open();
        MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.Text + "' and STATE_NAME='" + ddl_clientwisestate.Text + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and UNIT_CODE='"+ddl_unitcode.Text+"' ", d.con);
        MySqlDataReader dr = cmd2.ExecuteReader();
        if (dr.Read())
        {
            ddl_unitcode.Text = dr.GetValue(2).ToString();
        }
        dr.Close();
        d.con.Close();

        d.con.Open();
        MySqlCommand cmd_i = new MySqlCommand("SELECT  GRADE_DESC FROM pay_grade_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  GRADE_CODE = '" +ddl_grade.Text + "' ", d.con);
        MySqlDataReader dr1 = cmd_i.ExecuteReader();
        if (dr1.Read())
        {
            ddl_grade.Text = dr1.GetValue(0).ToString();
        }
        dr1.Close();
        d.con.Close();

        d.con.Open();
        MySqlCommand cmd_i1 = new MySqlCommand("SELECT  CLIENT_NAME FROM pay_client_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.Text + "' ", d.con);
        MySqlDataReader dr2 = cmd_i1.ExecuteReader();
        if (dr2.Read())
        {
            ddl_unit_client.Text = dr2.GetValue(0).ToString();
        }
        dr2.Close();
        d.con.Close();
       
    }

    public void text_Clear()
    {
        select_designation.ReadOnly = true;
        ddl_product_type.ReadOnly = true;
        ddl_uniformset.ReadOnly = true;
        txt_kra.ReadOnly = true;
        txt_fine.ReadOnly = true;
        nominee_address.ReadOnly = true;
        txt_relation5.ReadOnly = true;
        txt_relation6.ReadOnly = true;
        txt_relation7.ReadOnly = true;
        txt_fmobile1.ReadOnly = true;
        txt_fmobile2.ReadOnly = true;
        txt_fmobile3.ReadOnly = true;
        txt_fmobile4.ReadOnly = true;
        txt_fmobile5.ReadOnly = true;
        txt_fmobile6.ReadOnly = true;
        txt_fmobile7.ReadOnly = true;
        txt_eename.ReadOnly = true;
        txt_eefatharname.ReadOnly = true;

        txtreasonforleft.ReadOnly = true;
        // txtautoattendancecode.Text = "";

        ddl_relation.ReadOnly = true;
        txt_birthdate.ReadOnly = true;
        ddl_gender.ReadOnly = true;
        txt_ihmscode.ReadOnly = true;

       
        //   ddl_unitcode.SelectedIndex = 0;
        //ddl_dept.SelectedIndex = 0;
        ddl_grade.ReadOnly = true;
        ddl_grade_SelectedIndexChanged(null, null);
        txt_presentaddress.ReadOnly = true;
        txt_presentcity.ReadOnly = true;
        ddl_state.ReadOnly = true;
        txt_presentpincode.ReadOnly = true;
        txtrefname1.ReadOnly = true;
        txtrefname2.ReadOnly = true;

       
        ddl_permstate.ReadOnly = true;
        txt_permanantpincode.ReadOnly = true;
        txtref1mob.ReadOnly = true;
        txtref2mob.ReadOnly = true;
        txt_address2.ReadOnly = true;
        txt_address1.ReadOnly = true;
        txt_mobilenumber.ReadOnly = true;
        txt_residencecontactnumber.ReadOnly = true;
        txt_confirmationdate.ReadOnly = true;
        txt_joiningdate.ReadOnly = true;
        txt_advance_payment.ReadOnly = true;

        ddl_location.ReadOnly = true;
        ddl_location_city.ReadOnly = true;

        ddl_ptaxdeductionflag.Enabled = false;
        txt_ptaxnumber.ReadOnly = true;
        txt_leftdate.ReadOnly = true;
        txtreasonforleft.ReadOnly = true;
        ddlpfregisteremp.Enabled = false;
        txt_pfbankname.ReadOnly = true;
        txt_pfnomineename.ReadOnly = true;
        txt_pfifsccode.ReadOnly = true;
        txt_pfnomineerelation.ReadOnly = true;
        txt_employeeaccountnumber.ReadOnly = true;
        txt_pfbdate.ReadOnly = true;
        //ddl_bankcode.SelectedIndex =;
        txt_maritalstaus.ReadOnly = true;
        ddl_bloodgroup.Enabled = false;
        txt_hobbies.ReadOnly = true;

        txt_weight.ReadOnly = true;
        ddl_religion.ReadOnly = true;

        txt_height.ReadOnly = true;
       
            ddl_reporting_to.ReadOnly = true;
        

        txtsearchempid.ReadOnly = true;
        txt_email.ReadOnly = true;



        txt_Nationality.ReadOnly = true;
        txt_Identitymark.ReadOnly = true;
        ddl_Mother_Tongue.ReadOnly = true;
        txt_Passport_No.ReadOnly = true;
        ddl_Visa_Country.Enabled = false;
        txt_Driving_License_No.ReadOnly = true;
        txt_Mise.ReadOnly = true;
        txt_Place_Of_Birth.ReadOnly = true;
        txt_Language_Known.ReadOnly = true;
        txt_Area_Of_Expertise.ReadOnly = true;
        txt_Passport_Validity_Date.ReadOnly = true;
        txt_Visa_Validity_Date.ReadOnly = true;
        txt_Details_Of_Handicap.ReadOnly = true;
        txt_qualification_1.ReadOnly = true;
        txt_year_of_passing_1.ReadOnly = true;
        txt_qualification_2.ReadOnly = true;
        txt_year_of_passing_2.ReadOnly = true;
        txt_qualification_3.ReadOnly = true;
        txt_year_of_passing_3.ReadOnly = true;
        txt_qualification_4.ReadOnly = true;
        txt_year_of_passing_4.ReadOnly = true;
        txt_qualification_5.ReadOnly = true;
        txt_year_of_passing_5.ReadOnly = true;
        txt_key_skill_1.ReadOnly = true;
        txt_experience_in_months_1.ReadOnly = true;
        txt_key_skill_2.ReadOnly = true;
        txt_experience_in_months_2.ReadOnly = true;
        txt_key_skill_3.ReadOnly = true;
        txt_experience_in_months_3.ReadOnly = true;
        txt_key_skill_4.ReadOnly = true;
        txt_experience_in_months_4.ReadOnly = true;
        txt_key_skill_5.ReadOnly = true;
        txt_experience_in_months_5.ReadOnly = true;
        //--family Details ---
        txt_name1.ReadOnly = true;
        txt_relation1.Text = "Father";
        txt_dob1.ReadOnly = true;
        txt_pan1.ReadOnly = true;
        txt_adhaar1.ReadOnly = true;
        txt_name2.ReadOnly = true;
        txt_relation2.Text = "Mother";
        txt_dob2.ReadOnly = true;
        txt_pan2.ReadOnly = true;
        txt_adhaar2.ReadOnly = true;
        txt_name3.ReadOnly = true;
        txt_relation3.ReadOnly = true;
        txt_dob3.ReadOnly = true;
        txt_pan3.ReadOnly = true;
        txt_adhaar3.ReadOnly = true;
        txt_name4.ReadOnly = true;
        txt_relation4.ReadOnly = true;
        txt_dob4.ReadOnly = true;
        txt_pan4.ReadOnly = true;
        txt_adhaar4.ReadOnly = true;
        txt_name5.ReadOnly = true;
        txt_relation5.Text = "Child";
        txt_dob5.ReadOnly = true;
        txt_pan5.ReadOnly = true;
        txt_adhaar5.ReadOnly = true;
        txt_name6.ReadOnly = true;
        txt_relation6.Text = "Child";
        txt_dob6.ReadOnly = true;
        txt_pan6.ReadOnly = true;
        txt_adhaar6.ReadOnly = true;
        txt_name7.ReadOnly = true;
        txt_relation7.Text = "Child";
        txt_dob7.ReadOnly = true;
        txt_pan7.ReadOnly = true;
        txt_adhaar7.ReadOnly = true;
        Numberchild.ReadOnly = true;
        ddl_unit_client.ReadOnly = true;
        uniform_size.ReadOnly = true;
       //
        txt_attendanceid.ReadOnly = true;
        ddl_intime.ReadOnly = true;
      
        ddl_unitcode.ReadOnly = true;
        txt_emailid1.ReadOnly = true;
        ddl_clientwisestate.ReadOnly = true;
        txt_emailid2.ReadOnly = true;
        txt_permanantaddress.ReadOnly = true;
        txt_permanantcity.ReadOnly = true;
        try
        {
            DropDownList1.ReadOnly = true;
        }
        catch { }
        txt_bankholder.ReadOnly = true;
        ddl_bankcode.ReadOnly = true;
        ddl_infitcode.ReadOnly = true;
        //Txt_cca.Text = "0";
        //Txt_gra.Text = "0";
        //Txt_allow.Text = "0";
        txt_fine.Text = "0";
        ddl_employee_type.ReadOnly = true;
        ddl_location.ReadOnly = true;
        //Image4.ImageUrl = "~/Images/placeholder.png";
        //Image1.ImageUrl = "~/Images/pan.jpg";
        //Image10.ImageUrl = "~/Images/certificate.jpg";
        //Image11.ImageUrl = "~/Images/certificate.jpg";
        //Image12.ImageUrl = "~/Images/certificate.jpg";
        //Image2.ImageUrl = "~/Images/passbook.jpg";
        //Image3.ImageUrl = "~/Images/Biodata.png";
        //Image5.ImageUrl = "~/Images/Passport.jpg";
        //Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
        //Image7.ImageUrl = "~/Images/marksheet.jpg";
        //Image8.ImageUrl = "~/Images/marksheet.jpg";
        //Image9.ImageUrl = "~/Images/certificate.jpg";
        //Image14.ImageUrl = "~/Images/certificate.jpg";
        //Image15.ImageUrl = "~/Images/certificate.jpg";
        //Image16.ImageUrl = "~/Images/certificate.jpg";
        //Image17.ImageUrl = "~/Images/certificate.jpg";
        //Image18.ImageUrl = "~/Images/certificate.jpg";
        //Image19.ImageUrl = "~/Images/certificate.jpg";
       // txt_attendanceid.Text = "0";
    }

    public void designation_unitwise(object sender, EventArgs e)
    {
        //  ddl_location_city.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT  UNIT_CITY FROM pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  UNIT_CODE = '" + ddl_unitcode.Text + "' ", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location_city.Text = (dr_item1[0].ToString());

            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

        //ddl_location_city.Items.Insert(0, new ListItem("Select"));




       // ddl_grade.Items.Clear();
        //System.Data.DataTable dt_item = new System.Data.DataTable();
        ////    MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT GRADE_CODE, CONCAT(GRADE_DESC,'-',Working_Hours) as GRADE_DESC FROM pay_designation_count  WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con);
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.Text + "' and UNIT_CODE='" + ddl_unitcode.Text + "'", d.con);

        //d.con.Open();
        //try
        //{
        //    cmd_item.Fill(dt_item);
        //    if (dt_item.Rows.Count > 0)
        //    {
        //        ddl_grade.Text = dt_item;
        //       ddl_grade.DataTextField = dt_item.Columns[1].ToString();
        //        ddl_grade.DataValueField = dt_item.Columns[0].ToString();
        //        ddl_grade.DataBind();

        //    }
        //    dt_item.Dispose();
        //    d.con.Close();
        //    ddl_grade.Items.Insert(0, "Select");
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //    newpanel.Visible = true;
           
            

        //}

    }
    public void client_list()
    {
        d.con1.Close();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and pay_client_master.client_code in (select distinct(client_code) from pay_client_state_role_grade where role_name = '" + Session["ROLE"].ToString() + "') ORDER BY client_code", d.con1);
        d.con1.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
              //  ddl_unit_client.DataSource = dt_item;
                ddl_unit_client.Text = dt_item.Columns[0].ToString();
              //  ddl_unit_client.DataValueField = dt_item.Columns[1].ToString();
                ddl_unit_client.DataBind();


               
            }
            dt_item.Dispose();
            d.con1.Close();
           // ddl_unit_client.Items.Insert(0, "Select");
          
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();

        }

    }
    //protected void get_city_list1(object sender, EventArgs e)
    //{
    //    ddl_location.Text = ddl_clientwisestate.Text;
    //    newpanel.Visible = true;


    //   // ddl_unitcode.Items.Clear();

    //    MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_NAME,'_',UNIT_ADD1) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.Text + "' and STATE_NAME='" + ddl_clientwisestate.Text + "'  AND comp_code='" + Session["comp_code"].ToString() + "' ", d.con);
    //    MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
    //    DataSet ds1 = new DataSet();
    //    sda1.Fill(ds1);
    //    ddl_unitcode.Text = Tables[0];
    //    ddl_unitcode.DataValueField = "UNIT_CODE";
    //    ddl_unitcode.DataTextField = "CUNIT";
    //    ddl_unitcode.DataBind();
    //   // ddl_unitcode.Items.Insert(0, new ListItem("Select"));
    //    newpanel.Visible = true;

       
       

    //}
    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ddl_clientwisestate.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.Text + "' and unit_code is null ORDER BY STATE", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_clientwisestate.Text=(dr_item1[0].ToString());
                ddl_location.Text = (dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }

  //      ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));

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
    
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }


    public int getEmployee()
    {
        int returnEmployee = 0;
        d.con1.Open();
        try
        {
           

            // MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),LEFT_DATE,GENDER,UNIT_CODE,DESIGNATION_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_QUALIFICATION,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,STATUS,LOCATION,BASIC_PAY,PAN_NUMBER,PF_DEDUCTION_FLAG,PF_NUMBER,ESIC_DEDUCTION_FLAG,ESIC_NUMBER,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER,DEPT_CODE,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,ADHARNO,E_HEAD01,E_HEAD02,E_HEAD03,E_HEAD04,E_HEAD05,E_HEAD06,E_HEAD07,E_HEAD08,E_HEAD09,E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15,LS_HEAD01,LS_HEAD02,LS_HEAD03,LS_HEAD04,LS_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD010,LEFT_REASON,AUTOATTENDANCE_CODE,PF_SHEET,EARN_TOTAL,FATHER_RELATION,DATE_STMP,ENTER_USER_ID,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,KYC_CONFIRM,REPORTING_TO,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,emistartdate,attendance_id,in_time,pfemployeepercentage FROM pay_employee_master WHERE EMP_CODE='" + l_EMP_CODE + "'", d.con1);

            MySqlCommand cmd = new MySqlCommand("SELECT comp_code,EMP_CODE,EMP_NAME,EMP_FATHER_NAME,date_format(BIRTH_DATE,'%d/%m/%Y'),date_format(JOINING_DATE,'%d/%m/%Y'),date_format(CONFIRMATION_DATE,'%d/%m/%Y'),LEFT_DATE,GENDER,UNIT_CODE,GRADE_CODE,BANK_BRANCH,BANK_EMP_AC_CODE,EMP_CURRENT_ADDRESS,EMP_CURRENT_CITY,EMP_CURRENT_STATE,EMP_CURRENT_PIN,EMP_PERM_ADDRESS,EMP_PERM_CITY,EMP_PERM_STATE,EMP_PERM_PIN,EMP_MOBILE_NO,EMP_MOBILE_NO2,EMP_EMAIL_ID,EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP,EMP_HOBBIES,EMP_WEIGHT,EMP_RELIGION,EMP_HEIGHT,LOCATION,P_TAX_DEDUCTION_FLAG,P_TAX_NUMBER, LEFT_REASON,PF_SHEET,FATHER_RELATION,PF_BANK_NAME,PF_IFSC_CODE,PF_NOMINEE_NAME,PF_NOMINEE_RELATION,date_format(PF_NOMINEE_BDATE,'%d/%m/%Y'),EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id,in_time,name1,relation1,dob1,pan1,adhaar1,name2,relation2,dob2,pan2,adhaar2,name3,relation3,dob3,pan3,adhaar3,name4,relation4,dob4,pan4,adhaar4,name5,relation5,dob5,pan5,adhaar5,name6,relation6,dob6,pan6,adhaar6,name7,relation7,dob7,pan7,adhaar7,No_of_child,KRA,LOCATION_CITY,BANK_HOLDER_NAME,POLICE_STATION_NAME,F_MOBILE1,F_MOBILE2,F_MOBILE3,F_MOBILE4,F_MOBILE5,F_MOBILE6,F_MOBILE7, ihmscode,  case NFD_CODE when 'N' then 'NEFT TRANSFER' when 'I' then 'INTERNAL TRANSFER' end as NFD_CODE,CONTANCEPERSON1_EMAILID,CONTANCEPERSON2_EMAILID,CLIENT_CODE,(select client_code from pay_unit_master where unit_code = pay_employee_master.unit_code and comp_code = '" + Session["COMP_CODE"].ToString() + "') as client_code,refaddress1,refaddress2,cca,gratuity,special_allow,fine,Employee_type,police_verification_start_date,police_verification_End_date,rent_agreement_start_date,rent_agreement_end_date ,client_wise_state,pre_mobileno_1,pre_mobileno_2,premnent_mobileno_1,premnent_mobileno_2,comments FROM pay_employee_master WHERE EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
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
                ddl_bankcode.Text = dr.GetValue(11).ToString();
                txt_employeeaccountnumber.Text = dr.GetValue(12).ToString();
                txt_presentaddress.Text = dr.GetValue(13).ToString();
                ddl_state.Text = dr.GetValue(15).ToString();
             //   get_city_list(null, null);
                txt_presentcity.Text = dr.GetValue(14).ToString();

                txt_presentpincode.Text = dr.GetValue(16).ToString();
                txt_permanantaddress.Text = dr.GetValue(17).ToString();

                ddl_permstate.Text = dr.GetValue(19).ToString();
                get_city_list_shipping(null, null);
                txt_permanantcity.Text = dr.GetValue(18).ToString();

                txt_permanantpincode.Text = dr.GetValue(20).ToString();
                txt_mobilenumber.Text = dr.GetValue(21).ToString();
                txt_residencecontactnumber.Text = dr.GetValue(22).ToString();
                txt_email.Text = dr.GetValue(23).ToString();
                txt_maritalstaus.Text = dr.GetValue(24).ToString();
                ddl_bloodgroup.Text = dr.GetValue(25).ToString();
                txt_hobbies.Text = dr.GetValue(26).ToString();

                txt_weight.Text = dr.GetValue(27).ToString();
                ddl_religion.Text = dr.GetValue(28).ToString();
                txt_height.Text = dr.GetValue(29).ToString();

                /////-------------------------------------------------------------

                ddl_ptaxdeductionflag.SelectedItem.Text = dr.GetValue(31).ToString();
                txt_ptaxnumber.Text = dr.GetValue(32).ToString();//adhaar no;
                // ddl_dept.SelectedValue= dr.GetValue(41).ToString();

                /////----------------------------------------------------------

                txtreasonforleft.Text = dr.GetValue(33).ToString();
                ddlpfregisteremp.SelectedValue = dr.GetValue(34).ToString();

                ddl_relation.Text = dr.GetValue(35).ToString();
                // txt_eecode.Text = dr.GetValue(76).ToString();

                txt_pfbankname.Text = dr.GetValue(36).ToString();
                txt_pfifsccode.Text = dr.GetValue(37).ToString();
                txt_pfnomineename.Text = dr.GetValue(38).ToString();
                txt_pfnomineerelation.Text = dr.GetValue(39).ToString();

                string pfbdate = dr.GetValue(40).ToString();
                if (pfbdate == "")
                {
                    txt_pfbdate.Text = dr.GetValue(40).ToString();
                }
                else
                {

                    txt_pfbdate.Text = pfbdate.ToString();
                }

                //txt_pan_new_num.Text = dr.GetValue(50).ToString();
                txt_advance_payment.Text = dr.GetValue(41).ToString();

                txtrefname1.Text = dr.GetValue(42).ToString();
                txtref1mob.Text = dr.GetValue(43).ToString();
                txtrefname2.Text = dr.GetValue(44).ToString();
                txtref2mob.Text = dr.GetValue(45).ToString();

                txt_Nationality.Text = dr.GetValue(46).ToString();
                txt_Identitymark.Text = dr.GetValue(47).ToString();
                ddl_Mother_Tongue.Text = dr.GetValue(48).ToString();
                txt_Passport_No.Text = dr.GetValue(49).ToString();
                ddl_Visa_Country.SelectedItem.Text = dr.GetValue(50).ToString();
                txt_Driving_License_No.Text = dr.GetValue(51).ToString();
                txt_Mise.Text = dr.GetValue(52).ToString();
                txt_Place_Of_Birth.Text = dr.GetValue(53).ToString();
                txt_Language_Known.Text = dr.GetValue(54).ToString();
                txt_Area_Of_Expertise.Text = dr.GetValue(55).ToString();
                txt_Passport_Validity_Date.Text = dr.GetValue(56).ToString();
                txt_Visa_Validity_Date.Text = dr.GetValue(57).ToString();
                txt_Details_Of_Handicap.Text = dr.GetValue(58).ToString();


                txt_qualification_1.Text = dr.GetValue(59).ToString();
                txt_year_of_passing_1.Text = dr.GetValue(60).ToString();
                txt_qualification_2.Text = dr.GetValue(61).ToString();
                txt_year_of_passing_2.Text = dr.GetValue(62).ToString();
                txt_qualification_3.Text = dr.GetValue(63).ToString();
                txt_year_of_passing_3.Text = dr.GetValue(64).ToString();
                txt_qualification_4.Text = dr.GetValue(65).ToString();
                txt_year_of_passing_4.Text = dr.GetValue(66).ToString();
                txt_qualification_5.Text = dr.GetValue(67).ToString();
                txt_year_of_passing_5.Text = dr.GetValue(68).ToString();
                txt_key_skill_1.Text = dr.GetValue(69).ToString();
                txt_experience_in_months_1.Text = dr.GetValue(70).ToString();
                txt_key_skill_2.Text = dr.GetValue(71).ToString();
                txt_experience_in_months_2.Text = dr.GetValue(72).ToString();
                txt_key_skill_3.Text = dr.GetValue(73).ToString();
                txt_experience_in_months_3.Text = dr.GetValue(74).ToString();
                txt_key_skill_4.Text = dr.GetValue(75).ToString();
                txt_experience_in_months_4.Text = dr.GetValue(76).ToString();
                txt_key_skill_5.Text = dr.GetValue(77).ToString();
                txt_experience_in_months_5.Text = dr.GetValue(78).ToString();

                //ddl_grade_SelectedIndexChanged(null, null);

                //  else
                //{
                //    ddl_reporting_to.SelectedValue = dr.GetValue(122).ToString();
                //}
                // txt_loandate.Text = dr.GetValue(80).ToString();

                ddl_intime.Text = dr.GetValue(82).ToString();

                //---Family details -----
                txt_name1.Text = dr.GetValue(83).ToString();
                txt_relation1.Text = dr.GetValue(84).ToString();
                txt_dob1.Text = dr.GetValue(85).ToString();
                txt_pan1.Text = dr.GetValue(86).ToString();
                txt_adhaar1.Text = dr.GetValue(87).ToString();
                txt_name2.Text = dr.GetValue(88).ToString();
                txt_relation2.Text = dr.GetValue(89).ToString();
                txt_dob2.Text = dr.GetValue(90).ToString();
                txt_pan2.Text = dr.GetValue(91).ToString();
                txt_adhaar2.Text = dr.GetValue(92).ToString();
                txt_name3.Text = dr.GetValue(93).ToString();
                txt_relation3.Text = dr.GetValue(94).ToString();
                txt_dob3.Text = dr.GetValue(95).ToString();
                txt_pan3.Text = dr.GetValue(96).ToString();
                txt_adhaar3.Text = dr.GetValue(97).ToString();
                txt_name4.Text = dr.GetValue(98).ToString();
                txt_relation4.Text = dr.GetValue(99).ToString();
                txt_dob4.Text = dr.GetValue(100).ToString();
                txt_pan4.Text = dr.GetValue(101).ToString();
                txt_adhaar4.Text = dr.GetValue(102).ToString();
                txt_name5.Text = dr.GetValue(103).ToString();
                txt_relation5.Text = dr.GetValue(104).ToString();
                txt_dob5.Text = dr.GetValue(105).ToString();
                txt_pan5.Text = dr.GetValue(106).ToString();
                txt_adhaar5.Text = dr.GetValue(107).ToString();
                txt_name6.Text = dr.GetValue(108).ToString();
                txt_relation6.Text = dr.GetValue(109).ToString();
                txt_dob6.Text = dr.GetValue(110).ToString();
                txt_pan6.Text = dr.GetValue(111).ToString();
                txt_adhaar6.Text = dr.GetValue(112).ToString();
                txt_name7.Text = dr.GetValue(113).ToString();
                txt_relation7.Text = dr.GetValue(114).ToString();
                txt_dob7.Text = dr.GetValue(115).ToString();
                txt_pan7.Text = dr.GetValue(116).ToString();
                txt_adhaar7.Text = dr.GetValue(117).ToString();
                Numberchild.Text = dr.GetValue(119).ToString();



                txt_kra.Text = dr.GetValue(119).ToString();

                //ddl_location.Text = dr.GetValue(30).ToString();
                // ddl_location_SelectedIndexChanged(null, null);

                ddl_location_city.Text = dr.GetValue(120).ToString();


                txt_bankholder.Text = dr.GetValue(121).ToString();
                txt_policestationname.Text = dr.GetValue(122).ToString();
                txt_fmobile1.Text = dr.GetValue(123).ToString();
                txt_fmobile2.Text = dr.GetValue(124).ToString();
                txt_fmobile3.Text = dr.GetValue(125).ToString();
                txt_fmobile4.Text = dr.GetValue(126).ToString();
                txt_fmobile5.Text = dr.GetValue(127).ToString();
                txt_fmobile6.Text = dr.GetValue(128).ToString();
                txt_fmobile7.Text = dr.GetValue(129).ToString();


                txt_ihmscode.Text = dr.GetValue(130).ToString();
                ddl_infitcode.Text = dr.GetValue(131).ToString();
                txt_emailid1.Text = dr.GetValue(132).ToString();
                txt_emailid2.Text = dr.GetValue(133).ToString();

                // ddl_clientname.SelectedValue = dr.GetValue(183).ToString();
                // ddl_clientname_SelectedIndexChanged(null, null);
                // if (ddl_unitcode.Items.Count > 0)
                // {
                ddl_unit_client.Text = dr.GetValue(134).ToString();
                ddl_clientname_SelectedIndexChanged(null, null);
                try
                {
                    ddl_clientwisestate.Text = dr.GetValue(147).ToString();
                 //   get_city_list1(null, null);
                    ddl_unitcode.Text = dr.GetValue(9).ToString();
                    designation_unitwise(null, null);
                    ddl_grade.Text = dr.GetValue(10).ToString();
                    ddl_grade_SelectedIndexChanged(null, null);
                    txt_attendanceid.Text = dr.GetValue(81).ToString();
                   
                        ddl_reporting_to.Text = dr.GetValue(79).ToString();
                    
                }
                catch { }



                txt_address1.Text = dr.GetValue(136).ToString();
                txt_address2.Text = dr.GetValue(137).ToString();

                //Txt_cca.Text = dr.GetValue(138).ToString();
                //Txt_gra.Text = dr.GetValue(139).ToString();
                //Txt_allow.Text = dr.GetValue(140).ToString();
                txt_fine.Text = dr.GetValue(141).ToString();
                ddl_employee_type.Text = dr.GetValue(142).ToString();
                txt_start_date.Text = dr.GetValue(143).ToString();
                txt_end_date.Text = dr.GetValue(144).ToString();
                txt_ranteagrement_satrtdate.Text = dr.GetValue(145).ToString();
                txt_ranteagrement_enddate.Text = dr.GetValue(146).ToString();
                try
                {

                    ddl_location.Text = ddl_clientwisestate.Text;
                }
                catch { }
                pre_mobileno_1.Text = dr.GetValue(148).ToString();
                pre_mobileno_2.Text = dr.GetValue(149).ToString();
                txt_premanent_mob1.Text = dr.GetValue(150).ToString();
                txt_premanent_mob2.Text = dr.GetValue(151).ToString();
               


                MySqlCommand cmd_hd = new MySqlCommand("SELECT document_type,No_of_set,size,start_date,end_date FROM pay_document_details WHERE emp_code ='" + txt_eecode.Text + "' AND COMP_CODE='" + Session["COMP_CODE"].ToString() + "' ", d.con);
                d.con.Open();
                try
                {
                    MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
                    if (dr_hd.HasRows)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Load(dr_hd);
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["headtable"] = dt;
                        }
                        gv_product_details.DataSource = dt;
                        gv_product_details.DataBind();
                    }

                }
                catch (Exception ex) { }
                finally { d.con.Close(); }




            }
            dr.Close();
            cmd.Dispose();

            //Role of user
            cmd = new MySqlCommand("select role from pay_user_master where login_id ='" +Session["LOGIN_ID"].ToString()+ "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                try
                {
                    DropDownList1.Text = dr.GetValue(0).ToString();
                }
                catch { }
            }

            dr.Close();
            cmd.Dispose();

            MySqlCommand cmd1 = new MySqlCommand("SELECT EMP_PHOTO,EMP_ADHAR_PAN,EMP_BANK_STATEMENT,EMP_BIODATA,EMP_PASSPORT,EMP_DRIVING_LISCENCE,EMP_10TH_MARKSHEET,EMP_12TH_MARKSHEET,EMP_DIPLOMA_CERTIFICATE,EMP_DEGREE_CERTIFICATE,EMP_POST_GRADUATION_CERTIFICATE,EMP_EDUCATION_CERTIFICATE,POLICE_VERIFICATION_DOC,FormNo_2,FormNo_11,present_add_proof,noc_form,medical_form FROM pay_images_master WHERE EMP_CODE='" + Session["LOGIN_ID"].ToString()+ "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                //////--------------------------------------EMP_PHOTO-----------------------------------------------------

                if (dr.GetValue(0).ToString() != "")
                {

                    Image4.ImageUrl = "~/EMP_Images/" + dr.GetValue(0).ToString();
                }
                else
                {
                    Image4.ImageUrl = "~/Images/placeholder.png";
                }

                //////--------------------------------EMP_ADHAR_PAN----------------------------------------------------------

                if (dr.GetValue(1).ToString() != "")
                {

                    Image1.ImageUrl = "~/EMP_Images/" + dr.GetValue(1).ToString();
                }
                else
                {
                    Image1.ImageUrl = "~/Images/pan.jpg";
                }
                //////----------------------------------EMP_BANK_STATEMENT---------------------------------------------------

                if (dr.GetValue(2).ToString() != "")
                {

                    Image2.ImageUrl = "~/EMP_Images/" + dr.GetValue(2).ToString();
                }
                else
                {
                    Image2.ImageUrl = "~/Images/passbook.jpg";
                }

                //////-----------------------------EMP_BIODATA--------------------------------------------------------

                if (dr.GetValue(3).ToString() != "")
                {
                    Image3.ImageUrl = "~/EMP_Images/" + dr.GetValue(3).ToString();
                }
                else
                {
                    Image3.ImageUrl = "~/Images/Biodata.png";
                }

                //////------------------------------EMP_PASSPORT-------------------------------------------------------

                if (dr.GetValue(4).ToString() != "")
                {
                    Image5.ImageUrl = "~/EMP_Images/" + dr.GetValue(4).ToString();
                }
                else
                {
                    Image5.ImageUrl = "~/Images/Passport.jpg";
                }
                //////------------------------------------EMP_DRIVING_LISCENCE-------------------------------------------------
                if (dr.GetValue(5).ToString() != "")
                {
                    Image6.ImageUrl = "~/EMP_Images/" + dr.GetValue(5).ToString();
                }
                else
                {
                    Image6.ImageUrl = "~/Images/Driving_liscence.jpg";
                }
                //////------------------------------------EMP_10TH_MARKSHEET-------------------------------------------------
                if (dr.GetValue(6).ToString() != "")
                {
                    Image7.ImageUrl = "~/EMP_Images/" + dr.GetValue(6).ToString();
                }
                else
                {
                    Image7.ImageUrl = "~/Images/marksheet.jpg";
                }
                //////------------------------------------EMP_12TH_MARKSHEET-------------------------------------------------
                if (dr.GetValue(7).ToString() != "")
                {
                    Image8.ImageUrl = "~/EMP_Images/" + dr.GetValue(7).ToString();
                }
                else
                {
                    Image8.ImageUrl = "~/Images/marksheet.jpg";
                }
                //////-------------------------------------EMP_DIPLOMA_CERTIFICATE------------------------------------------------
                if (dr.GetValue(8).ToString() != "")
                {
                    Image9.ImageUrl = "~/EMP_Images/" + dr.GetValue(8).ToString();
                }
                else
                {
                    Image9.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_DEGREE_CERTIFICATE------------------------------------------------
                if (dr.GetValue(9).ToString() != "")
                {
                    Image10.ImageUrl = "~/EMP_Images/" + dr.GetValue(9).ToString();
                }
                else
                {
                    Image10.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_POST_GRADUATION_CERTIFICATE------------------------------------------------
                if (dr.GetValue(10).ToString() != "")
                {
                    Image11.ImageUrl = "~/EMP_Images/" + dr.GetValue(10).ToString();
                }
                else
                {
                    Image11.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------EMP_EDUCATION_CERTIFICATE------------------------------------------------
                if (dr.GetValue(11).ToString() != "")
                {
                    Image12.ImageUrl = "~/EMP_Images/" + dr.GetValue(11).ToString();
                }
                else
                {
                    Image12.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(12).ToString() != "")
                {
                    Image14.ImageUrl = "~/EMP_Images/" + dr.GetValue(12).ToString();
                }
                else
                {
                    Image14.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(13).ToString() != "")
                {
                    Image15.ImageUrl = "~/EMP_Images/" + dr.GetValue(13).ToString();
                }
                else
                {
                    Image15.ImageUrl = "~/Images/certificate.jpg";
                }

                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(14).ToString() != "")
                {
                    Image16.ImageUrl = "~/EMP_Images/" + dr.GetValue(14).ToString();
                }
                else
                {
                    Image16.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(15).ToString() != "")
                {
                    Image17.ImageUrl = "~/EMP_Images/" + dr.GetValue(15).ToString();
                }
                else
                {
                    Image17.ImageUrl = "~/Images/Biodata.png";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(16).ToString() != "")
                {
                    Image18.ImageUrl = "~/EMP_Images/" + dr.GetValue(16).ToString();
                }
                else
                {
                    Image18.ImageUrl = "~/Images/certificate.jpg";
                }
                //////-------------------------------------------------------------------------------------
                if (dr.GetValue(16).ToString() != "")
                {
                    Image19.ImageUrl = "~/EMP_Images/" + dr.GetValue(17).ToString();
                }
                else
                {
                    Image19.ImageUrl = "~/Images/certificate.jpg";
                }


            }
            dr.Close();
            cmd1.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            ddl_grade_SelectedIndexChanged(null, null);
           
           
        }

        return returnEmployee;
    }
   

    protected void image_click(object sender, ImageClickEventArgs e)
    {
        System.Web.UI.WebControls.Image button = sender as System.Web.UI.WebControls.Image;
        Response.Redirect(button.ImageUrl);
        
        
    }

    private void set_data()
    {
        if (txt_weight.Text == "") { txt_weight.Text = "0"; }
        if (txt_height.Text == "") { txt_height.Text = "0"; }

        if (txt_advance_payment.Text == "") { txt_advance_payment.Text = "0"; }


    }

    protected void ddl_grade_SelectedIndexChanged(object sender, EventArgs e)
    {

        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
        MySqlCommand cmd_item2 = new MySqlCommand("SELECT UNIT_CITY FROM pay_unit_master where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  UNIT_CODE = '" + ddl_unitcode.Text + "' ", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item2 = cmd_item2.ExecuteReader();
            while (dr_item2.Read())
            {
                ddl_location_city.Text = (dr_item2[0].ToString());
            }
            dr_item2.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
        }
        //  ddl_location_city.Items.Insert(0, new ListItem("Select"));




        d1.con1.Open();
        try
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("select emp_code, emp_name, grade_code from pay_employee_master where grade_code in (select reporting_to from pay_grade_master where grade_code = '" + ddl_grade.Text.ToString() + "')  AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
            adp.Fill(ds);
            ddl_reporting_to.Text = "";
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

        //d.con.Open();
        //txt_attendanceid.Items.Clear();
        //System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(hours) from pay_designation_count where unit_code ='" + ddl_unitcode.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_unit_client.Text + "' and designation = (Select grade_desc from pay_grade_master where grade_code = '" + ddl_grade.Text + "' and COMP_CODE = '" + Session["COMP_CODE"].ToString() + "')", d.con);
        //try
        //{
        //    cmd_item.Fill(dt_item);
        //    if (dt_item.Rows.Count > 0)
        //    {
        //        txt_attendanceid.DataSource = dt_item;
        //        txt_attendanceid.DataValueField = dt_item.Columns[0].ToString();
        //        txt_attendanceid.DataTextField = dt_item.Columns[0].ToString();
        //        txt_attendanceid.DataBind();

        //    }
        //    d.con.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //}
        ////txt_attendanceid.Items.Insert(0, new ListItem("Select","0"));

    }

    protected void get_city_list_shipping(object sender, EventArgs e)
    {

      //  txt_permanantcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_permstate.Text.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
             //   txt_permanantcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
           // txt_permanantcity.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }

        //  ddl_clientwisestate.SelectedValue = ddl_location.Text;
    }
    //protected void get_city_list(object sender, EventArgs e)
    //{

    //    //string name=  ddl_state.SelectedItem.ToString();
    //    txt_presentcity.Items.Clear();
    //    MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.Text.ToString() + "' order by city", d1.con);
    //    d1.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
    //        while (dr_item1.Read())
    //        {
    //            txt_presentcity.Items.Add(dr_item1[0].ToString());
    //            txt_permanantcity.Items.Add(dr_item1[0].ToString());
    //        }
    //        dr_item1.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d1.con.Close();

    //        txt_presentcity.Items.Insert(0, new ListItem("Select", "Select"));
    //        txt_permanantcity.Items.Insert(0, new ListItem("Select", "Select"));
    //        newpanel.Visible = true;
    //    }

    //}

    protected void ddl_location_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ddl_location_city.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_location.Text + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                ddl_location_city.Text = (dr_item1[0].ToString());
            }

           
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            // ddl_location_city.Items.Insert(0, new ListItem("Select", "Select"));
            newpanel.Visible = true;
        }
    }

    protected void get_client_details(object sender, EventArgs e)
    {

        //string name=  ddl_state.SelectedItem.ToString();
       // txt_presentcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + ddl_state.Text.ToString() + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
             //   txt_presentcity.Items.Add(dr_item1[0].ToString());

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


    private string generateihmscode()
    {
        //IHMS/CLient Name/Gradecode/statecodeautoincrementnumber
        //examle IHMS/KM/HK/MH18

        string ihmscode = "IH&MS";
        //Client code
        ihmscode = ihmscode + "/" + getsingledata("select client_code from pay_unit_master where unit_code = '" + ddl_unitcode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
        //grade code
        ihmscode = ihmscode + "/" + ddl_grade.Text;
        //state code
        ihmscode = ihmscode + "/" + getsingledata("select state_code from pay_state_master where state_name = '" + ddl_location.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");

        return ihmscode + int.Parse(txt_eecode.Text.Substring(1, txt_eecode.Text.Length - 1)); ;

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
  

    private Boolean chkcount()
    {
        if (ddl_employee_type.Text == "Reliever")
        {
            return false;
        }
        if (ddl_grade.Text != "Select")
        {
            string unit_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + ddl_unit_client.Text + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.Text + "' and HOURS = '" + txt_attendanceid.Text + "'");
            if (unit_emp_count == "")
            {
                return false;
            }
            string emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + ddl_unit_client.Text + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.Text + "' and attendance_id = '" + txt_attendanceid.Text + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
            try
            {
                if (unit_emp_count != "" && emp_count != "")
                {
                    if (int.Parse(unit_emp_count) <= int.Parse(emp_count))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        return false;
    }
   
    protected void lnk_zone_add_Click(object sender, EventArgs e)
    {

        gv_product_details.Visible = true;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("document_type");
        dt.Columns.Add("No_of_set");
        dt.Columns.Add("size");
        dt.Columns.Add("start_date");
        dt.Columns.Add("end_date");


        int rownum = 0;
        for (rownum = 0; rownum < gv_product_details.Rows.Count; rownum++)
        {
            if (gv_product_details.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["document_type"] = gv_product_details.Rows[rownum].Cells[2].Text;
                dr["No_of_set"] = gv_product_details.Rows[rownum].Cells[3].Text;
                dr["size"] = gv_product_details.Rows[rownum].Cells[4].Text;
                dr["start_date"] = gv_product_details.Rows[rownum].Cells[5].Text;
                dr["end_date"] = gv_product_details.Rows[rownum].Cells[6].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();

        dr["document_type"] = ddl_product_type.Text;
        dr["No_of_set"] = ddl_uniformset.Text;

        dr["size"] = uniform_size.Text;
        dr["start_date"] = uniform_issue_date.Text;
        dr["end_date"] = uniform_expiry_date.Text;

        dt.Rows.Add(dr);
        gv_product_details.DataSource = dt;
        gv_product_details.DataBind();

        ViewState["headtable"] = dt;

        newpanel.Visible = true;

        ddl_product_type.Text = "Select";
        ddl_uniformset.Text = "0";
        uniform_size.Text = "";
        uniform_expiry_date.Text = "";
        uniform_issue_date.Text = "";
    }

    protected void gv_product_details_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void lnk_remove_product_Click(object sender, EventArgs e)
    {
       
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_product_details.DataSource = dt;
            gv_product_details.DataBind();
        }
    }

   

    protected void txt_workinghours_count(object sender, EventArgs e)
    {
        d.con.Open();
        MySqlCommand cmd = new MySqlCommand("select HOURS from pay_designation_count where unit_code='" + ddl_unitcode.Text + "' and client_code='" + ddl_unit_client.Text + "' and UNIT_CODE='" + ddl_unitcode.Text + "' and DESIGNATION='" + ddl_grade.Text + "' and comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            string a = dr.GetValue(0).ToString();

            if (txt_attendanceid.Text != a)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Hours Not Match With Branch Hours');", true);
                return;
            }

            newpanel.Visible = true;
        }
        newpanel.Visible = true;
    }


    private void downloadFile(string filepath)
    {
        Response.Redirect(Server.MapPath(filepath));
        Response.End(); 
    
    }
}
