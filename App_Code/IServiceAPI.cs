using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public interface IServiceAPI
{
    void CreateNewAccount(string firstName, string lastName, string userName, string password);
    DataTable GetUserDetails(string userName);
    bool UserAuthentication(string userName, string passsword);
    DataTable GetDepartmentDetails();
    DataTable GetLeaveDetails(string userName);
    void Employee_Leave_Apply(String EMP_CODE, string LeaveType, string Reporting_to, string Reason_for_leave, string Balance_leave, 
        string Start_date, string End_date, string Number_of_leave);
    bool GetSalaryData(string EMP_CODE, string SELECT_DATE);
    DataTable GetAttendanceDetails(string userName, string month_year);
    DataTable GetEmployeeLeaveBalance(string EMP_CODE, string Leave_type);
    DataTable GetEmployeeLeaveApplyNotification(string EMP_CODE);
    DataTable Getunit_Lattitude_Longtitude_Distance(string EMP_CODE);

    // old android apk code version 14
    void Employee_Attendance_Apply(string unit_unitlatitude, string unit_unitlongtitude, string unit_distance, string unit_empaddress, 
    string unit_emplatitude, string unit_emplongtitude, string emp_code, string mobile_imei, string attendances_click, string Upload_Image_base64);
    // add new filed indicate_offline_online
    void Employee_Attendance_Apply_New(string unit_unitlatitude, string unit_unitlongtitude, string unit_distance, string unit_empaddress, 
        string unit_emplatitude,string unit_emplongtitude, string emp_code, string mobile_imei, string attendances_click,
        string Upload_Image_base64, string indicate_offline_online, string offline_date);
     void Houskeeping_Employee_Leave_Apply(String EMP_CODE, string Reporting_to, string Reason_for_leave, string Start_date, string End_date,
        string Number_of_leave);
    bool GetPoliceVerification(string EMP_CODE);
    bool GetUniform_form(string EMP_CODE);
   void Police_verification_image_upload(String Upload_Image_base64, string EMP_CODE);
   void Employee_adhar_card_image_send(string Upload_Image_base64, string EMP_CODE, string adhar_no);
   void Employee_adhar_card_scanning_details(string EMP_CODE, String scan_uid, String scan_name, String scan_gender, String scan_villageTehsil, 
       String scan_district, String scan_state, String scan_postCode, String scan_dobrith);
   void Employee_pan_card_image_send(string Upload_Image_base64, string EMP_CODE, string pan_no);
   void Employee_woring_image_send(string Upload_Image_base64, string EMP_CODE);
   void Employee_geolocation_lat_long(string emp_code, string emp_latitude, string emp_longtitude,string current_address);
   DataTable GetClientList(string userName, string employeerequirementflag);
   DataTable GetClientUnitList(string client_code, string userName);
   DataTable GetEmployeeClientUnitList(string client_code);
   DataTable GetClientUnitGradeList(string unit_code, string client_code);
   void site_audio_data(string emp_code, string client_code, string unit_code, string grade_name, string ques_1_ans, string ques_2_ans,
       string ques_3_ans, string ques_4_ans, string ques_5_ans, string ques_6_ans, string remarks, string ques_image_1, string ques_path_1, 
       string ques_image_2, string ques_path_2, string ques_image_3, string ques_path_3, string ques_image_4, string ques_path_4, 
       string ques_image_5, string ques_path_5, string current_address, string ques_image_6, string ques_path_6);
   void new_employee_requirement_form(string emp_code, string client_code, string unit_code, string grade_name,
   string employee_name, string father_name, string dob, string gender, string doj,
   string blood_group, string working_hours, string address, string mobile_no, string bank_holder_name,
   string account_no, string branch_name, string ifsc_code, string adhar_no, string adhar_image, string adhar_imgpath, 
   string emp_image, string emp_imgpath, string police_image,
   string police_imgpath, string bankbook_image, string bankbook_imgpath);
   DataTable GetAdharPolicePanApprovedComment(string userName);
   DataTable GetPoliceValidDate(string userName);
   DataTable GetSuiteAuditComment(string userName);
   DataTable GetOperationSiteauditschedule(string userName);
   DataTable Gettravellingexpensesmode(string userName);
   DataTable GetUserContactDetails(String client_code, String unit_code, String userName);
   DataTable GetUserContactFieldDetails(string emp_code);
   void Employee_photo_image_send(string Upload_Image_base64, string EMP_CODE);
   void Joining_documentList(string emp_code, string ques_image_1, string ques_path_1, string ques_image_2, string ques_path_2, 
       string ques_image_3, string ques_path_3, string ques_image_4, string ques_path_4, string ques_image_5, string ques_path_5, 
       string ques_image_6, string ques_path_6, string ques_image_7, string ques_path_7);
   DataTable GetTabEmployeeAttendancesDetails(string super_emp_code);
   void TABEmployeeattendances_send(string EmpCode, string EmpName, string EmpDesignation, string EmpDesignation_code, 
       string SupervisorEmpCode,string EmpShift, string Emppunctuality, string Empuniforms, string Empcap, string Empshoes,
       string Empbelt, string Empidcard, string Empshaving, string Emphairs, string Empnails, string Empbriefing, string Empremarks,
       string Emp_intimeimage, string Emp_location);
   void TABEmployeeattendances_outtimeMark(string EmpCode, string EmpName, string Emp_outimage);
   void service_quality_rating_data(string client_code, string unit_code, string emp_code, string quesservice_1_ans, string quesservice_2_ans, 
       string quesservice_3_ans, string quesservice_4_ans, string quesservice_5_ans, string quesservice_6_ans, string quesservice_7_ans,
       string quesservice_8_ans, string quesservice_9_ans, string quesservice_10_ans, string remarks);
   DataTable GetServiceRatingComment(string userName);
   DataTable GetHKEmployeeLeaveApplyNotification(string EMP_CODE, string leave_status);
   DataTable GetEmployeeDocumentList(string emp_code);
   DataTable ForgetPassword(string emp_code, string birth_day, string adhar_card);
   void Employee_fire_extinguisher_image_send(string type_extinguisher,string Upload_Image_base64, string EMP_CODE);
   DataTable GetFireExtinguisherNotification(string EMP_CODE);
   DataTable GetOperationSiteauditschedule_Notification(string userName);
}
