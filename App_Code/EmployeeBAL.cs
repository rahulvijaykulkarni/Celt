using System.Data;

/// <summary>
/// Summary description for EmployeeBAL
/// </summary>
public class EmployeeBAL
{
    DAL de = new DAL();
    public EmployeeBAL()
    {
        //
        // TODO: Add constructor logic here
        ////
    }

    //-----------insert query-----------------------------------------------------
    public int EmployeeInsert(string compcode, string empcode, string empname, string empfathername, string birthdate, string joiningdate, string confirmationdate, string leftdate, string gender, string maritalstatus, string qualification, string religion, string bloodgroup, float weight, float height, string hobbies, string presentaddress, string presentcity, string presentstate, string pressentpincode, string permanantaddress, string permanantcity, string permanantstate, string permanantpincode, string mobilenumber, string residencecontactnumber, string status, string location, float basicpay, string pannumber, string pfdeductionflag, string pfnumber, string esicdeductionflag, string esicnumber, string ptaxdeductionflag, string ptaxnumber, string bankempaccode, string bankcode, string gradecode, string unitcode, string deptcode, float ehead1, float ehead2, float ehead3, float ehead4, float ehead5, float ehead6, float ehead7, float ehead8, float ehead9, float ehead10, float ehead11, float ehead12, float ehead13, float ehead14, float ehead15, float lshead1, float lshead2, float lshead3, float lshead4, float lshead5, float dhead1, float dhead2, float dhead3, float dhead4, float dhead5, float dhead6, float dhead7, float dhead8, float dhead9, float dhead10, string leftreason, string pfsheet, float earntotal, string relation, string enteruserid, string entrydatestmp, string txt_pfbankname, string txt_pfifsccode, string txt_pfnomineename, string txt_pfnomineerelation, string txt_pfbdate, string EMP_NEW_PAN_NO, string emp_advance_payment, string refname1, string ref1mob, string refname2, string ref2mob, string emp_nationality, string emp_identitymark, string emp_mothertongue, string emp_passportnumber, string emp_visa_country, string emp_driving_liscence, string emp_mise, string emp_place_of_birth, string emp_language_known, string emp_area_of_expertise, string emp_passport_validity_date, string emp_visa_validity_date, string emp_details_of_handicap, string qualification_1, string year_of_passing_1, string qualification_2, string year_of_passing_2, string qualification_3, string year_of_passing_3, string qualification_4, string year_of_passing_4, string qualification_5, string year_of_passing_5, string key_skill_1, string experience_month_1, string key_skill_2, string experience_month_2, string key_skill_3, string experience_month_3, string key_skill_4, string experience_month_4, string key_skill_5, string experience_month_5, string reporting_to, string loandate, string attendance_id, string intime,string pfemployeepercentage)//insert agrs
    {
        int res = 0;

        try
        {
            //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            comp_code,            EMP_CODE,       EMP_NAME,           EMP_FATHER_NAME,                    BIRTH_DATE,                                      JOINING_DATE,                     GENDER,          UNIT_CODE,             DESIGNATION_CODE,       GRADE_CODE,         BANK_BRANCH,              BANK_EMP_AC_CODE,   EMP_CURRENT_ADDRESS,     EMP_CURRENT_CITY,       EMP_CURRENT_STATE,          EMP_CURRENT_PIN,       EMP_PERM_ADDRESS,           EMP_PERM_CITY,          EMP_PERM_STATE,         EMP_PERM_PIN,           EMP_MOBILE_NO,              EMP_EMAIL_ID,          EMP_MARRITAL_STATUS,        EMP_BLOOD_GROUP,    EMP_HOBBIES,    EMP_QUALIFICATION,      EMP_WEIGHT,     EMP_RELIGION,       EMP_HEIGHT,         STATUS,             LOCATION,       BASIC_PAY,          PAN_NUMBER,         PF_DEDUCTION_FLAG,          PF_NUMBER,          ESIC_DEDUCTION_FLAG,      ESIC_NUMBER,       P_TAX_DEDUCTION_FLAG,          P_TAX_NUMBER
            res = de.operation("INSERT INTO pay_employee_master(comp_code, EMP_CODE, EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, JOINING_DATE,CONFIRMATION_DATE,LEFT_DATE, GENDER, UNIT_CODE,GRADE_CODE, BANK_BRANCH, BANK_EMP_AC_CODE, EMP_CURRENT_ADDRESS, EMP_CURRENT_CITY, EMP_CURRENT_STATE, EMP_CURRENT_PIN,EMP_PERM_ADDRESS, EMP_PERM_CITY, EMP_PERM_STATE, EMP_PERM_PIN, EMP_MOBILE_NO, EMP_EMAIL_ID, EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP, EMP_HOBBIES, EMP_QUALIFICATION, EMP_WEIGHT, EMP_RELIGION, EMP_HEIGHT, STATUS, LOCATION, BASIC_PAY, PAN_NUMBER,PF_DEDUCTION_FLAG, PF_NUMBER, ESIC_DEDUCTION_FLAG, ESIC_NUMBER, P_TAX_DEDUCTION_FLAG, P_TAX_NUMBER,DEPT_CODE, E_HEAD01, E_HEAD02, E_HEAD03, E_HEAD04, E_HEAD05, E_HEAD06, E_HEAD07, E_HEAD08, E_HEAD09, E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15, LS_HEAD01, LS_HEAD02, LS_HEAD03, LS_HEAD04, LS_HEAD05, D_HEAD01, D_HEAD02, D_HEAD03, D_HEAD04, D_HEAD05, D_HEAD06, D_HEAD07, D_HEAD08, D_HEAD09, D_HEAD010,LEFT_REASON,PF_SHEET,EARN_TOTAL,FATHER_RELATION,ENTER_USER_ID,DATE_STMP, PF_BANK_NAME ,PF_IFSC_CODE ,PF_NOMINEE_NAME ,PF_NOMINEE_RELATION ,PF_NOMINEE_BDATE,EMP_NEW_PAN_NO,EMP_ADVANCE_PAYMENT,REFNAME1,REFMOBILE1,REFNAME2,REFMOBILE2,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5,reporting_to,emistartdate,attendance_id, in_time,pfemployeepercentage) VALUES ('" + compcode + "', '" + empcode + "', '" + empname + "', '" + empfathername + "',  str_to_date('" + birthdate + "','%d/%m/%Y'), str_to_date('" + joiningdate + "','%d/%m/%Y'),str_to_date('" + confirmationdate + "','%d/%m/%Y'),'" + (leftdate == "" ? null : leftdate) + "', '" + gender + "', '" + unitcode + "', '" + gradecode + "', '" + bankcode + "', '" + bankempaccode + "', '" + presentaddress + "', '" + presentcity + "', '" + presentstate + "', '" + pressentpincode + "', '" + permanantaddress + "', '" + permanantcity + "', '" + permanantstate + "','" + permanantpincode + "','" + mobilenumber + "','" + residencecontactnumber + "','" + maritalstatus + "','" + bloodgroup + "', '" + hobbies + "', '" + qualification + "'," + weight + ", '" + religion + "', " + height + ", '" + status + "', '" + location + "', " + basicpay + ", '" + pannumber + "', '" + pfdeductionflag + "', '" + pfnumber + "','" + esicdeductionflag + "','" + esicnumber + "','" + ptaxdeductionflag + "','" + ptaxnumber + "','" + deptcode + "'," + ehead1 + "," + ehead2 + "," + ehead3 + "," + ehead4 + "," + ehead5 + "," + ehead6 + "," + ehead7 + "," + ehead8 + ", " + ehead9 + "," + ehead10 + "," + ehead11 + "," + ehead12 + "," + ehead13 + "," + ehead14 + "," + ehead15 + "," + lshead1 + ", " + lshead2 + "," + lshead3 + "," + lshead4 + "," + lshead5 + "," + dhead1 + "," + dhead2 + ", " + dhead3 + "," + dhead4 + "," + dhead5 + ", " + dhead6 + "," + dhead7 + "," + dhead8 + "," + dhead9 + "," + dhead10 + ",'" + leftreason + "','" + pfsheet + "'," + earntotal + ", '" + relation + "','" + enteruserid + "',STR_TO_DATE('" + entrydatestmp + "','%d/%m/%Y'),'" + txt_pfbankname + "', '" + txt_pfifsccode + "','" + txt_pfnomineename + "', '" + txt_pfnomineerelation + "','" + (txt_pfbdate == "" ? null : txt_pfbdate) + "', '" + EMP_NEW_PAN_NO + "', '" + emp_advance_payment + "','" + refname1 + "','" + ref1mob + "','" + refname2 + "','" + ref2mob + "','" + emp_nationality + "','" + emp_identitymark + "','" + emp_mothertongue + "','" + emp_passportnumber + "','" + emp_visa_country + "','" + emp_driving_liscence + "','" + emp_mise + "','" + emp_place_of_birth + "','" + emp_language_known + "','" + emp_area_of_expertise + "','" + (emp_passport_validity_date == "" ? null : emp_passport_validity_date) + "','" + (emp_visa_validity_date == "" ? null : emp_visa_validity_date) + "','" + emp_details_of_handicap + "','" + qualification_1 + "','" + year_of_passing_1 + "','" + qualification_2 + "','" + year_of_passing_2 + "','" + qualification_3 + "','" + year_of_passing_3 + "','" + qualification_4 + "','" + year_of_passing_4 + "','" + qualification_5 + "','" + year_of_passing_5 + "','" + key_skill_1 + "','" + experience_month_1 + "','" + key_skill_2 + "','" + experience_month_2 + "','" + key_skill_3 + "','" + experience_month_3 + "','" + key_skill_4 + "','" + experience_month_4 + "','" + key_skill_5 + "','" + experience_month_5 + "','" + reporting_to + "','" + loandate + "','" + attendance_id + "','" + intime + "','" + pfemployeepercentage + "')");//insert command
            return res;
        }
        catch
        {
            throw;
        }
        finally
        {
        }
    }
    public int EmployeeInsertString(string compcode, string empcode, string empname, string empfathername, string birthdate, string joiningdate, string confirmationdate, string leftdate, string gender, string maritalstatus, string qualification, string religion, string bloodgroup, float weight, float height, string hobbies, string presentaddress, string presentcity, string presentstate, string pressentpincode, string permanantaddress, string permanantcity, string permanantstate, string permanantpincode, string mobilenumber, string residencecontactnumber, string status, string location, float basicpay, string pannumber, string pfdeductionflag, string pfnumber, string esicdeductionflag, string esicnumber, string ptaxdeductionflag, string ptaxnumber, string bankempaccode, string bankcode, string gradecode, string unitcode, string emp_nationality, string emp_identitymark, string emp_mothertongue, string emp_passportnumber, string emp_visa_country, string emp_driving_liscence, string emp_mise, string emp_place_of_birth, string emp_language_known, string emp_area_of_expertise, string emp_passport_validity_date, string emp_visa_validity_date, string emp_details_of_handicap, string qualification_1, string year_of_passing_1, string qualification_2, string year_of_passing_2, string qualification_3, string year_of_passing_3, string qualification_4, string year_of_passing_4, string qualification_5, string year_of_passing_5, string key_skill_1, string experience_month_1, string key_skill_2, string experience_month_2, string key_skill_3, string experience_month_3, string key_skill_4, string experience_month_4, string key_skill_5, string experience_month_5)//insert agrs
    {
        int res = 0;
        try
        {
            //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         comp_code,            EMP_CODE,       EMP_NAME,           EMP_FATHER_NAME,                    BIRTH_DATE,                                      JOINING_DATE,                     GENDER,          UNIT_CODE,             DESIGNATION_CODE,       GRADE_CODE,         BANK_BRANCH,              BANK_EMP_AC_CODE,   EMP_CURRENT_ADDRESS,     EMP_CURRENT_CITY,       EMP_CURRENT_STATE,          EMP_CURRENT_PIN,       EMP_PERM_ADDRESS,           EMP_PERM_CITY,          EMP_PERM_STATE,         EMP_PERM_PIN,           EMP_MOBILE_NO,              EMP_EMAIL_ID,          EMP_MARRITAL_STATUS,        EMP_BLOOD_GROUP,    EMP_HOBBIES,    EMP_QUALIFICATION,      EMP_WEIGHT,     EMP_RELIGION,       EMP_HEIGHT,         STATUS,             LOCATION,       BASIC_PAY,          PAN_NUMBER,         PF_DEDUCTION_FLAG,          PF_NUMBER,          ESIC_DEDUCTION_FLAG,      ESIC_NUMBER,       P_TAX_DEDUCTION_FLAG,          P_TAX_NUMBER
            res = de.operation("INSERT INTO pay_employee_master(comp_code, EMP_CODE, EMP_NAME, EMP_FATHER_NAME, BIRTH_DATE, JOINING_DATE,CONFIRMATION_DATE, GENDER, UNIT_CODE,GRADE_CODE, BANK_BRANCH, BANK_EMP_AC_CODE, EMP_CURRENT_ADDRESS, EMP_CURRENT_CITY, EMP_CURRENT_STATE, EMP_CURRENT_PIN,EMP_PERM_ADDRESS, EMP_PERM_CITY, EMP_PERM_STATE, EMP_PERM_PIN, EMP_MOBILE_NO, EMP_EMAIL_ID, EMP_MARRITAL_STATUS,EMP_BLOOD_GROUP, EMP_HOBBIES, EMP_QUALIFICATION, EMP_WEIGHT, EMP_RELIGION, EMP_HEIGHT, STATUS, LOCATION, BASIC_PAY, PAN_NUMBER,PF_DEDUCTION_FLAG, PF_NUMBER, ESIC_DEDUCTION_FLAG, ESIC_NUMBER, P_TAX_DEDUCTION_FLAG, P_TAX_NUMBER,EMP_NATIONALITY,EMP_IDENTITYMARK,EMP_MOTHERTONGUE,EMP_PASSPORTNUMBER,EMP_VISA_COUNTRY,EMP_DRIVING_LICENSCE,EMP_MISE,EMP_PLACE_OF_BIRTH,EMP_LANGUAGE_KNOWN,EMP_AREA_OF_EXPERTISE,EMP_PASSPORT_VALIDITY_DATE,EMP_VISA_VALIDITY_DATE,EMP_DETAILS_OF_HANDICAP,QUALIFICATION_1,YEAR_OF_PASSING_1,QUALIFICATION_2,YEAR_OF_PASSING_2,QUALIFICATION_3,YEAR_OF_PASSING_3,QUALIFICATION_4,YEAR_OF_PASSING_4,QUALIFICATION_5,YEAR_OF_PASSING_5,KEY_SKILL_1,EXPERIENCE_MONTH_1,KEY_SKILL_2,EXPERIENCE_MONTH_2,KEY_SKILL_3,EXPERIENCE_MONTH_3,KEY_SKILL_4,EXPERIENCE_MONTH_4,KEY_SKILL_5,EXPERIENCE_MONTH_5) VALUES('" + compcode + "', '" + empcode + "', '" + empname + "', '" + empfathername + "', '" + birthdate + "',  '" + joiningdate + "','" + confirmationdate + "', '" + gender + "', '" + unitcode + "', '" + gradecode + "', '" + bankcode + "', '" + bankempaccode + "', '" + presentaddress + "', '" + presentcity + "', '" + presentstate + "', '" + pressentpincode + "', '" + permanantaddress + "', '" + permanantcity + "', '" + permanantstate + "','" + permanantpincode + "','" + mobilenumber + "','" + residencecontactnumber + "','" + maritalstatus + "','" + bloodgroup + "', '" + hobbies + "', '" + qualification + "'," + weight + ", '" + religion + "', " + height + ", '" + status + "', '" + location + "', " + basicpay + ", '" + pannumber + "', '" + pfdeductionflag + "', '" + pfnumber + "','" + esicdeductionflag + "','" + esicnumber + "','" + ptaxdeductionflag + "','" + ptaxnumber + "','" + emp_nationality + "','" + emp_identitymark + "','" + emp_mothertongue + "','" + emp_passportnumber + "','" + emp_visa_country + "','" + emp_driving_liscence + "','" + emp_mise + "','" + emp_place_of_birth + "','" + emp_language_known + "','" + emp_area_of_expertise + "','" + emp_passport_validity_date + "','" + emp_visa_validity_date + "','" + emp_details_of_handicap + "','" + qualification_1 + "','" + year_of_passing_1 + "','" + qualification_2 + "','" + year_of_passing_2 + "','" + qualification_3 + "','" + year_of_passing_3 + "','" + qualification_4 + "','" + year_of_passing_4 + "','" + qualification_5 + "','" + year_of_passing_5 + "','" + key_skill_1 + "','" + experience_month_1 + "','" + key_skill_2 + "','" + experience_month_2 + "','" + key_skill_3 + "','" + experience_month_3 + "','" + key_skill_4 + "','" + experience_month_4 + "','" + key_skill_5 + "','" + experience_month_5 + "')");//insert command
            return res;
        }
        catch
        {
            throw;
        }
        finally
        {
        }
    }
    //public int EmployeeLeaveInsert(string compcode, string empcode, string typeofleave, float openingbalance, float allocatedleave, float takenleave, float balanceleave)//insert agrs
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = de.operation("INSERT INTO PAY_EMP_LEAVE_MASTER (comp_code, EMP_CODE, TYPE_OF_LEAVE, OPENING_BALANCE, ALLOCATED_LEAVE, TAKEN_LEAVE, BALANCE_LEAVE) VALUES ('" + compcode + "','" + empcode + "', '" + typeofleave + "','" + openingbalance + "','" + allocatedleave + "','" + takenleave + "','" + balanceleave + "')");//insert command
    //        return res;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {
    //    }
    //}
    //public int EmployeeRateInsert(string compcode, string empcode,float ehead1,float ehead2,float ehead3,float ehead4,float ehead5,float ehead6,float ehead7,float ehead8,float ehead9,float ehead10,float ehead11,float ehead12,float ehead13,float ehead14,float ehead15, float lshead1,float lshead2,float lshead3,float lshead4,float lshead5,float dhead1,float dhead2,float dhead3,float dhead4,float dhead5,float dhead6,float dhead7,float dhead8,float dhead9,float dhead10)//insert agrs
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = de.operation("INSERT INTO PAY_EMP_RATE_MASTER (comp_code, EMP_CODE, E_HEAD01, E_HEAD02, E_HEAD03, E_HEAD04, E_HEAD05, E_HEAD06, E_HEAD07, E_HEAD08, E_HEAD09, E_HEAD10,E_HEAD11,E_HEAD12,E_HEAD13,E_HEAD14,E_HEAD15, LS_HEAD01, LS_HEAD02, LS_HEAD03, LS_HEAD04, LS_HEAD05, D_HEAD01, D_HEAD02, D_HEAD03, D_HEAD04, D_HEAD05, D_HEAD06, D_HEAD07, D_HEAD08, D_HEAD09, D_HEAD010) VALUES('"+compcode +"','"+empcode +"',"+ehead1 +","+ehead2 +","+ehead3 +","+ehead4 +","+ehead5 +","+ehead6 +","+ehead7 +","+ehead8 +", "+ehead9 +","+ehead10 +","+ehead11 +","+ehead12 +","+ehead13 +","+ehead14 +","+ehead15 +","+lshead1 +", "+lshead2 +","+lshead3 +","+lshead4 +","+lshead5 +","+dhead1 +","+dhead2 +", "+dhead3 +","+dhead4 +","+dhead5 +", "+dhead6 +","+dhead7 +","+dhead8 +","+dhead9 +","+dhead10 +")");//insert command
    //        return res;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {
    //    }
    //}
    //---------------update query-------------------------------------------------
    public int EmployeeUpdate(string compcode, string empcode, string empname, string empfathername, string birthdate, string joiningdate, string confirmationdate, string leftdate, string gender, string maritalstatus, string qualification, string religion, string bloodgroup, float weight, float height, string hobbies, string presentaddress, string presentcity, string presentstate, string pressentpincode, string permanantaddress, string permanantcity, string permanantstate, string permanantpincode, string mobilenumber, string residencecontactnumber, string status, string location, float basicpay, string pannumber, string pfdeductionflag, string pfnumber, string esicdeductionflag, string esicnumber, string ptaxdeductionflag, string ptaxnumber, string bankempaccode, string bankcode, string gradecode, string unitcode, string deptcode, float ehead1, float ehead2, float ehead3, float ehead4, float ehead5, float ehead6, float ehead7, float ehead8, float ehead9, float ehead10, float ehead11, float ehead12, float ehead13, float ehead14, float ehead15, float lshead1, float lshead2, float lshead3, float lshead4, float lshead5, float dhead1, float dhead2, float dhead3, float dhead4, float dhead5, float dhead6, float dhead7, float dhead8, float dhead9, float dhead10, string leftreason, string pfsheet, float earntotal, string relation, string enteruserid, string entrydatestmp, string txt_pfbankname, string txt_pfifsccode, string txt_pfnomineename, string txt_pfnomineerelation, string txt_pfbdate, string txt_emp_new_pan, string txt_advance_payment, string txtrefname1, string txtref1mob, string txtrefname2, string txtref2mob, string emp_nationality, string emp_identitymark, string emp_mothertongue, string emp_passportnumber, string emp_visa_country, string emp_driving_liscence, string emp_mise, string emp_place_of_birth, string emp_language_known, string emp_area_of_expertise, string emp_passport_validity_date, string emp_visa_validity_date, string emp_details_of_handicap, string qualification_1, string year_of_passing_1, string qualification_2, string year_of_passing_2, string qualification_3, string year_of_passing_3, string qualification_4, string year_of_passing_4, string qualification_5, string year_of_passing_5, string key_skill_1, string experience_month_1, string key_skill_2, string experience_month_2, string key_skill_3, string experience_month_3, string key_skill_4, string experience_month_4, string key_skill_5, string experience_month_5, string reporting_to, string loandate, string attendanceid, string intime,string pfemployeepercentage)//updt args 
    {
        int res = 0;
        try
        {
            res = de.operation("UPDATE pay_employee_master SET EMP_NAME = '" + empname + "',EMP_FATHER_NAME = '" + empfathername + "',BIRTH_DATE =  str_to_date('" + birthdate + "','%d/%m/%Y'), JOINING_DATE = str_to_date('" + joiningdate + "','%d/%m/%Y'),CONFIRMATION_DATE= str_to_date('" + confirmationdate + "','%d/%m/%Y'),LEFT_DATE='" + leftdate + "',GENDER = '" + gender + "',UNIT_CODE = '" + unitcode + "',GRADE_CODE = '" + gradecode + "', BANK_BRANCH = '" + bankcode + "', BANK_EMP_AC_CODE = '" + bankempaccode + "', EMP_CURRENT_ADDRESS = '" + presentaddress + "', EMP_CURRENT_CITY = '" + presentcity + "', EMP_CURRENT_STATE = '" + presentstate + "',  EMP_CURRENT_PIN = '" + pressentpincode + "', EMP_PERM_ADDRESS = '" + permanantaddress + "', EMP_PERM_CITY = '" + permanantcity + "', EMP_PERM_STATE = '" + permanantstate + "', EMP_PERM_PIN = '" + permanantpincode + "',EMP_MOBILE_NO = '" + mobilenumber + "',EMP_EMAIL_ID = '" + residencecontactnumber + "', EMP_MARRITAL_STATUS = '" + maritalstatus + "',EMP_BLOOD_GROUP = '" + bloodgroup + "', EMP_HOBBIES = '" + hobbies + "', EMP_QUALIFICATION = '" + qualification + "', EMP_WEIGHT = " + weight + ",  EMP_RELIGION = '" + religion + "', EMP_HEIGHT = " + height + ", STATUS = '" + status + "', LOCATION = '" + location + "', BASIC_PAY = " + basicpay + ", PAN_NUMBER = '" + pannumber + "',PF_DEDUCTION_FLAG = '" + pfdeductionflag + "',PF_NUMBER = '" + pfnumber + "',ESIC_DEDUCTION_FLAG = '" + esicdeductionflag + "',ESIC_NUMBER = '" + esicnumber + "', P_TAX_DEDUCTION_FLAG = '" + ptaxdeductionflag + "', P_TAX_NUMBER = '" + ptaxnumber + "',DEPT_CODE='" + deptcode + "',E_HEAD01=" + ehead1 + ", E_HEAD02=" + ehead2 + ", E_HEAD03=" + ehead3 + ", E_HEAD04=" + ehead4 + ",E_HEAD05=" + ehead5 + ",E_HEAD06=" + ehead6 + ",E_HEAD07=" + ehead7 + ",E_HEAD08=" + ehead8 + ",E_HEAD09=" + ehead9 + ",E_HEAD10=" + ehead10 + ",E_HEAD11=" + ehead11 + ",E_HEAD12=" + ehead12 + ",E_HEAD13=" + ehead13 + ",E_HEAD14=" + ehead14 + ",E_HEAD15=" + ehead15 + ",LS_HEAD01=" + lshead1 + ",LS_HEAD02=" + lshead2 + ",LS_HEAD03=" + lshead3 + ",LS_HEAD04=" + lshead4 + ",LS_HEAD05=" + lshead5 + ", D_HEAD01=" + dhead1 + ", D_HEAD02=" + dhead2 + ",D_HEAD03=" + dhead3 + ",D_HEAD04=" + dhead4 + ",D_HEAD05=" + dhead5 + ",D_HEAD06=" + dhead6 + ",D_HEAD07=" + dhead7 + ",D_HEAD08=" + dhead8 + ",D_HEAD09=" + dhead9 + ",D_HEAD010=" + dhead10 + ",LEFT_REASON='" + leftreason + "',PF_SHEET='" + pfsheet + "', EARN_TOTAL=" + earntotal + " ,FATHER_RELATION='" + relation + "',ENTER_USER_ID='" + enteruserid + "',DATE_STMP= str_to_date('" + entrydatestmp + "','%d/%m/%Y') ,PF_BANK_NAME = '" + txt_pfbankname + "',PF_IFSC_CODE= '" + txt_pfifsccode + "',PF_NOMINEE_NAME='" + txt_pfnomineename + "', PF_NOMINEE_RELATION='" + txt_pfnomineerelation + "',PF_NOMINEE_BDATE = '" + txt_pfbdate + "',EMP_NEW_PAN_NO = '" + txt_emp_new_pan + "',EMP_ADVANCE_PAYMENT = '" + txt_advance_payment + "',REFNAME1 = '" + txtrefname1 + "',REFMOBILE1 = '" + txtref1mob + "',REFNAME2 = '" + txtrefname2 + "',REFMOBILE2 = '" + txtref2mob + "',EMP_NATIONALITY ='" + emp_nationality + "',EMP_IDENTITYMARK ='" + emp_identitymark + "',EMP_MOTHERTONGUE ='" + emp_mothertongue + "',EMP_PASSPORTNUMBER ='" + emp_passportnumber + "',EMP_VISA_COUNTRY ='" + emp_visa_country + "',EMP_DRIVING_LICENSCE ='" + emp_driving_liscence + "',EMP_MISE ='" + emp_mise + "',EMP_PLACE_OF_BIRTH ='" + emp_place_of_birth + "',EMP_LANGUAGE_KNOWN ='" + emp_language_known + "',EMP_AREA_OF_EXPERTISE ='" + emp_area_of_expertise + "',EMP_PASSPORT_VALIDITY_DATE ='" + emp_passport_validity_date + "',EMP_VISA_VALIDITY_DATE ='" + emp_visa_validity_date + "',EMP_DETAILS_OF_HANDICAP ='" + emp_details_of_handicap + "',QUALIFICATION_1	='" + qualification_1 + "',YEAR_OF_PASSING_1 ='" + year_of_passing_1 + "',QUALIFICATION_2	='" + qualification_2 + "',YEAR_OF_PASSING_2 ='" + year_of_passing_2 + "',QUALIFICATION_3	='" + qualification_3 + "',YEAR_OF_PASSING_3 ='" + year_of_passing_3 + "',QUALIFICATION_4	='" + qualification_4 + "',YEAR_OF_PASSING_4 ='" + year_of_passing_4 + "',QUALIFICATION_5	='" + qualification_5 + "',YEAR_OF_PASSING_5 ='" + year_of_passing_5 + "',KEY_SKILL_1 ='" + key_skill_1 + "',EXPERIENCE_MONTH_1 ='" + experience_month_1 + "',KEY_SKILL_2 ='" + key_skill_2 + "',EXPERIENCE_MONTH_2 ='" + experience_month_2 + "',KEY_SKILL_3 ='" + key_skill_3 + "',EXPERIENCE_MONTH_3 ='" + experience_month_3 + "',KEY_SKILL_4 ='" + key_skill_4 + "',EXPERIENCE_MONTH_4 ='" + experience_month_4 + "',KEY_SKILL_5 ='" + key_skill_5 + "',EXPERIENCE_MONTH_5 ='" + experience_month_5 + "',reporting_to ='" + reporting_to + "',emistartdate ='" + loandate + "',attendance_id ='" + attendanceid + "', in_time = '" + intime + "' ,pfemployeepercentage='" + pfemployeepercentage + "' WHERE (comp_code = '" + compcode + "') AND (EMP_CODE = '" + empcode + "')");//update command
            return res;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }
    //public int EmployeeLeaveUpdate(string empcode, string typeofleave, float openingbalance, float allocatedleave, float takenleave, float balanceleave)//update args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = de.operation("UPDATE PAY_EMP_LEAVE_MASTER SET TYPE_OF_LEAVE='" + typeofleave + "', OPENING_BALANCE='" + openingbalance + "', ALLOCATED_LEAVE='" + allocatedleave + "', TAKEN_LEAVE='" + takenleave + "', BALANCE_LEAVE='" + balanceleave + "' WHERE EMP_CODE='" + empcode + "'");//update command
    //        return res;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {

    //    }
    //}
    //public int EmployeeRateUpdate(string compcode, string empcode, float ehead1, float ehead2, float ehead3, float ehead4, float ehead5, float ehead6, float ehead7, float ehead8, float ehead9, float ehead10, float ehead11, float ehead12, float ehead13, float ehead14, float ehead15, float lshead1, float lshead2, float lshead3, float lshead4, float lshead5, float dhead1, float dhead2, float dhead3, float dhead4, float dhead5, float dhead6, float dhead7, float dhead8, float dhead9, float dhead10)//update args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = de.operation("UPDATE PAY_EMP_RATE_MASTER SET E_HEAD01=" + ehead1 + ", E_HEAD02=" + ehead2 + ", E_HEAD03=" + ehead3 + ", E_HEAD04=" + ehead4 + ",E_HEAD05="+ehead5 +",E_HEAD06="+ehead6 +",E_HEAD07="+ehead7 +",E_HEAD08="+ehead8 +",E_HEAD09="+ehead9 +",E_HEAD10="+ehead10 +",E_HEAD11="+ehead11 +",E_HEAD12="+ehead12 +",E_HEAD13="+ehead13 +",E_HEAD14="+ehead14 +",E_HEAD15="+ehead15 +",LS_HEAD01="+lshead1 +",LS_HEAD02="+lshead2 +",LS_HEAD03="+lshead3 +",LS_HEAD04="+lshead4 +",LS_HEAD05="+lshead5 +", D_HEAD01=" + dhead1 + ", D_HEAD02=" + dhead2 + ",D_HEAD03=" + dhead3 + ",D_HEAD04="+dhead4 +",D_HEAD05="+dhead5 +",D_HEAD06="+dhead6 +",D_HEAD07="+dhead7 +",D_HEAD08="+dhead8 +",D_HEAD09="+dhead9 +",D_HEAD010="+dhead10 +" WHERE EMP_CODE='" + empcode + "'");//update command
    //        return res;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {

    //    }
    //}
    //-------------delete query---------------------------------------------------
    public int EmployeeDelete(string compcode, string empcode)//delete args
    {
        int res = 0;
        try
        {
            res = de.operation("DELETE FROM pay_employee_master WHERE EMP_CODE='" + empcode + "' AND comp_code='" + compcode + "'");//delete command
            if (res > 0)
            {
                res = de.operation("DELETE FROM pay_user_master WHERE LOGIN_ID='" + empcode + "'");//delete command
            }
            return res;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }
    public int EmployeeLeaveDelete(string compcode, string empcode)//delete args
    {
        int res = 0;
        try
        {
            res = de.operation("DELETE FROM PAY_EMP_LEAVE_MASTER WHERE EMP_CODE='" + empcode + "' AND comp_code='" + compcode + "'");//delete command
            return res;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }
    //public int EmployeeRateDelete(string compcode, string empcode)//delete args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = de.operation("DELETE FROM PAY_EMP_RATE_MASTER WHERE EMP_CODE='" + empcode + "' AND comp_code='" + compcode + "'");//delete command
    //        return res;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {

    //    }
    //}
    //----------select query------------------------------------------
    public DataSet EmployeeSelect()
    {
        DataSet result;
        try
        {
            result = de.getData("SELECT * FROM pay_employee_master");//select command
            return result;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }
    public DataSet EmployeeLeaveSelect()
    {
        DataSet result;
        try
        {
            result = de.getData("SELECT * FROM PAY_EMP_LEAVE_MASTER");//select command
            return result;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }
    public DataSet EmployeeRateSelect()
    {
        DataSet result;
        try
        {
            result = de.getData("SELECT * FROM PAY_EMP_RATE_MASTER");//select command
            return result;
        }
        catch
        {
            throw;
        }
        finally
        {

        }
    }


}
