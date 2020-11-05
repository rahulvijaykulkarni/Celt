using System;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;

public class ServiceAPI : IServiceAPI
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    DAL d = new DAL();
    DAL d1 = new DAL();
    public ServiceAPI()
    {

    }

    string rahul;

    string working_imagepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/Working_Images/"));
    string  site_auditpath= System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/site_audit/"));

    public void CreateNewAccount(string firstName, string lastName, string userName, string password)
    {
        try
        {
            d.operation("INSERT INTO UserDetails (firstName,lastName,userName,password) VALUES ('" + firstName + "','" + lastName + "','" + userName + "','" + password + "')");
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            log.Info("Android - Finally CreateNewUser....");
        }

    }

    // Get Employee Details
    public DataTable GetUserDetails(string userName)
    {
        DataTable userDetailsTable = new DataTable();
        try
        {
            //userDetailsTable.Columns.Add(new DataColumn("firstName", typeof(String)));
            //userDetailsTable.Columns.Add(new DataColumn("lastName", typeof(String)));

            userDetailsTable.Columns.Add(new DataColumn("EMP_NAME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("EMP_CODE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("GRADE_CODE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("COMP_NAME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("UNIT_NAME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("DEPT_CODE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("EMP_MOBILE_NO", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("GENDER", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("SHIFT_TIME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("JOINING_DATE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("BIRTH_DATE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("REPORTING_TO", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("COMP_CODE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("CLIENT_CODE", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("UNIT_CODE", typeof(String)));

            log.Error("Android - Inside User Details..");
            //string query = "SELECT pay_employee_master.EMP_NAME as EMP_NAME,pay_employee_master.Emp_Code as EMP_CODE,(SELECT Grade_desc FROM pay_grade_master g WHERE g.grade_code = pay_employee_master.GRADE_CODE and g.comp_code = pay_employee_master.comp_CODE) AS 'GRADE_CODE',(SELECT company_name FROM pay_company_master c WHERE c.comp_code = pay_employee_master.comp_code) AS 'COMP_NAME',pay_unit_master.UNIT_NAME as UNIT_NAME,(SELECT DEPT_NAME FROM pay_department_master d WHERE d.DEPT_CODE = pay_employee_master.DEPT_CODE) AS 'DEPT_CODE',pay_employee_master.emp_mobile_no AS 'EMP_MOBILE_NO',CASE GENDER WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS 'GENDER',pay_employee_master.in_time AS 'SHIFT_TIME',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', attendance_id AS 'REPORTING_TO' FROM pay_employee_master,pay_unit_master Where pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE and pay_employee_master.EMP_CODE='" + userName + "'";
            //string query = "SELECT pay_employee_master.EMP_NAME as EMP_NAME,pay_employee_master.Emp_Code as EMP_CODE,(SELECT Grade_desc FROM pay_grade_master g WHERE g.grade_code = pay_employee_master.GRADE_CODE and g.comp_code = pay_employee_master.comp_CODE) AS 'GRADE_CODE',(SELECT company_name FROM pay_company_master c WHERE c.comp_code = pay_employee_master.comp_code) AS 'COMP_NAME',pay_unit_master.UNIT_NAME as UNIT_NAME,pay_images_master.EMP_PHOTO  AS 'DEPT_CODE',pay_employee_master.emp_mobile_no AS 'EMP_MOBILE_NO',CASE GENDER WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS 'GENDER',pay_employee_master.in_time AS 'SHIFT_TIME',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', attendance_id AS 'REPORTING_TO' FROM pay_employee_master,pay_unit_master,pay_images_master Where pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE and pay_employee_master.emp_code=pay_images_master.emp_code and pay_employee_master.EMP_CODE='" + userName + "'";
            string query = "select `pay_employee_master`.`EMP_NAME`,`pay_employee_master`.`Emp_Code` AS 'EMP_CODE',pay_grade_master.Grade_desc AS 'GRADE_CODE',pay_company_master.company_name  AS 'COMP_NAME',`pay_unit_master`.`UNIT_NAME`,`pay_images_master`.`EMP_PHOTO` AS 'DEPT_CODE',`pay_employee_master`.`emp_mobile_no` AS 'EMP_MOBILE_NO',CASE `GENDER` WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS 'GENDER',`pay_employee_master`.`in_time` AS 'SHIFT_TIME',DATE_FORMAT(`pay_employee_master`.`JOINING_DATE`, '%d/%m/%Y') AS 'JOINING_DATE',DATE_FORMAT(`pay_employee_master`.`BIRTH_DATE`, '%d/%m/%Y') AS 'BIRTH_DATE', pay_employee_master.`attendance_id` AS 'REPORTING_TO',pay_employee_master.unit_code as 'UNIT_CODE', pay_employee_master.comp_code as 'COMP_CODE',pay_employee_master.client_code as 'CLIENT_CODE' from pay_employee_master inner join pay_company_master on pay_employee_master.comp_code=pay_company_master.comp_code inner join pay_unit_master on `pay_employee_master`.`comp_code` = `pay_unit_master`.`comp_code` and `pay_employee_master`.`UNIT_CODE` = `pay_unit_master`.`UNIT_CODE` inner join pay_grade_master on `pay_employee_master`.`grade_code` = `pay_grade_master`.`GRADE_CODE` and `pay_employee_master`.`comp_code` = `pay_grade_master`.`comp_code` left join pay_images_master on `pay_employee_master`.`comp_code` = `pay_images_master`.`comp_code` and `pay_employee_master`.`emp_code` = `pay_images_master`.`emp_code` where `pay_employee_master`.`EMP_CODE`='" + userName + "'";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);

                    userDetailsTable.Rows.Add(reader["EMP_NAME"].ToString(), reader["EMP_CODE"].ToString(),
                        reader["GRADE_CODE"].ToString(), reader["COMP_NAME"].ToString(), reader["UNIT_NAME"].ToString()
                        , reader["DEPT_CODE"].ToString(), reader["EMP_MOBILE_NO"].ToString(), reader["GENDER"].ToString(), reader["SHIFT_TIME"].ToString(), reader["JOINING_DATE"].ToString(), reader["BIRTH_DATE"].ToString(), reader["REPORTING_TO"].ToString(),
                        reader["UNIT_CODE"].ToString(), reader["COMP_CODE"].ToString(), reader["CLIENT_CODE"].ToString());
                }
            }

            reader.Close();
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetUserDetails....");
        }
        return userDetailsTable;
    }

    // Check User 
    public bool UserAuthentication(string userName, string passsword)
    {
        bool auth = false;
        log.Info("Android - Inside UserAuthentication");
        try
        {
           // string query1 = "SELECT * FROM pay_user_master WHERE Login_id='" + userName + "' AND USER_PASSWORD='" + passsword + "'";
            string query = "SELECT pay_user_master.comp_code,Login_id,user_name,Role,flag,counter,modify_date FROM pay_user_master inner join pay_employee_master on  pay_employee_master.emp_code=pay_user_master.Login_id and pay_employee_master.comp_code=pay_user_master.comp_code WHERE Login_id='" + userName + "' AND USER_PASSWORD='" + passsword + "' and (left_date ='' or left_date is null)";
            
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            //if (reader.HasRows)
            if (reader.Read())
            {
                //if (reader.GetValue(4).ToString() == "L")
                //{
                //    auth = false;
                //}
                //else
                //{
                //    auth = true;
                //    log.Error("Valid Username!!!");
                //}
                auth = true;
                log.Error("Valid Username!!!");
            }
            else
            {
                log.Error("Invalid Username!!!");
            }
            reader.Close();
            d.con.Close();
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
           log.Error("Android - Finally User Authentication....");
        }

        return auth;

    }

    public DataTable GetDepartmentDetails()
    {

        DataTable deptTable = new DataTable();
        deptTable.Columns.Add("no", typeof(String));
        deptTable.Columns.Add("name", typeof(String));
        try
        {
            log.Info("Android - Inside DepartmentDetails..");
            string query = "SELECT comp_code as no,GRADE_CODE as name FROM pay_grade_master;";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    deptTable.Rows.Add(reader["no"], reader["name"]);
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetDepartmentDetails....");
        }

        return deptTable;

    }

    //---- Leave Details of an Employee ---//
    public DataTable GetLeaveDetails(string userName)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("LEAVE_ID", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("LEAVE_NAME", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("MAX_NO_OF_LEAVE", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("BALANCE_LEAVE", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("REPORTING_TO", typeof(String)));

        try
        {

            log.Info("Android - Inside GetLeaveDetails..");
            // old query 
           // string query = "SELECT leave_id as LEAVE_ID, leave_name as LEAVE_NAME, max_no_of_leave as MAX_NO_OF_LEAVE,balance_leave as BALANCE_LEAVE,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_employee_master.REPORTING_TO) AS 'REPORTING_TO' FROM pay_leave_emp_balance ,pay_employee_master WHERE pay_leave_emp_balance.EMP_CODE = pay_employee_master.EMP_CODE AND pay_employee_master.EMP_CODE = '" + userName + "' group by pay_leave_emp_balance.leave_name";
            string query = "select  pay_assign_leave_policy.policy_id,pay_assign_leave_policy.policy_name,pay_leave_master.leave_name AS 'LEAVE_NAME',pay_leave_master.abbreviation as 'LEAVE_ID',max_no_of_leave AS 'MAX_NO_OF_LEAVE',(SELECT `emp_name` FROM `pay_employee_master` a WHERE `a`.`emp_code` = `pay_employee_master`.`REPORTING_TO`) AS 'REPORTING_TO' from  pay_assign_leave_policy inner join pay_leave_master on pay_assign_leave_policy.comp_code=pay_leave_master.comp_code  and pay_assign_leave_policy.policy_name=pay_leave_master.policy_name inner join pay_employee_master on  pay_assign_leave_policy.emp_code=pay_employee_master.emp_code and pay_assign_leave_policy.comp_code=pay_employee_master.comp_code where pay_assign_leave_policy.emp_code='" + userName + "' and pay_assign_leave_policy.comp_code=(select  comp_code from pay_employee_master where emp_code='" + userName + "' )";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["LEAVE_ID"].ToString(), reader["LEAVE_NAME"].ToString(),
                        reader["MAX_NO_OF_LEAVE"].ToString(), reader["REPORTING_TO"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetLeaveDetails....");
        }
        return userDetailsTable;

    }

    // Leave Type Selected send Balance Leave

    public DataTable GetEmployeeLeaveBalance(string EMP_CODE, string Leave_type)
    {
        DataTable userDetailsTable = new DataTable();
        DataTable userDetailsTableabc = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("BALANCE_LEAVE", typeof(String)));
        userDetailsTableabc.Columns.Add(new DataColumn("MAX_NO_OF_LEAVE", typeof(String)));
        try
        {
            
            log.Info("Android - Inside Employee Leave Balances....");

            //string query = "SELECT balance_leave as BALANCE_LEAVE FROM pay_leave_emp_balance WHERE EMP_CODE = '" + EMP_CODE + "' AND leave_name='" + Leave_type + "'";
            string balance_leave = d.getsinglestring("select  max_no_of_leave AS 'MAX_NO_OF_LEAVE' from  pay_assign_leave_policy inner join pay_leave_master on pay_assign_leave_policy.comp_code=pay_leave_master.comp_code  and pay_assign_leave_policy.policy_name=pay_leave_master.policy_name where pay_assign_leave_policy.emp_code='" + EMP_CODE + "' and pay_assign_leave_policy.comp_code=(select  comp_code from pay_employee_master  where emp_code='" + EMP_CODE + "') and pay_leave_master.leave_name='" + Leave_type + "'");

            string query = "select max_no_of_leave as 'BALANCE_LEAVE' from pay_leave_transaction where emp_code='" + EMP_CODE + "'  and leave_type='" + Leave_type + "' order by leave_id desc limit 1 ";

            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);
                    if (reader["BALANCE_LEAVE"].ToString() != null)
                    {
                        userDetailsTable.Rows.Add(reader["BALANCE_LEAVE"].ToString());
                    }
                }
                else {
                    userDetailsTableabc.Rows.Add(balance_leave.ToString());
                }
            }
            userDetailsTable.Merge(userDetailsTableabc);
            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee Leave Balance...." + userDetailsTable);
        }
        userDetailsTable.Merge(userDetailsTableabc);
        return userDetailsTable;
    }


    //private string getname(string EMP_CODE, string Leave_type)
    //{
    //    string emailid = "";
    //    string sql = "select  max_no_of_leave AS 'MAX_NO_OF_LEAVE' from  pay_assign_leave_policy inner join pay_leave_master on pay_assign_leave_policy.comp_code=pay_leave_master.comp_code  and pay_assign_leave_policy.policy_name=pay_leave_master.policy_name where pay_assign_leave_policy.emp_code='" + EMP_CODE + "' and pay_assign_leave_policy.comp_code=(select  comp_code from pay_employee_master  where emp_code='" + EMP_CODE + "') and pay_leave_master.leave_name='" + Leave_type + "'";

    //    MySqlCommand cmd = new MySqlCommand(sql, d.con);
    //    try
    //    {
    //        con.Open();
    //        emailid = (string)cmd.ExecuteScalar();
    //    }
    //    catch
    //    { }
    //    finally
    //    {
    //        con.Close();
    //        con.Dispose();
    //        cmd.Dispose();
    //    }

    //    return emailid;
    //}


    //Leave Apply Details Insert
    public void Employee_Leave_Apply(String EMP_CODE, string LeaveType, string Reporting_to, string Reason_for_leave, string Balance_leave, string Start_date, string End_date, string Number_of_leave)
    {
        int res;
        try
        {

            log.Error("Android - Inside Leave Apply");
            string query1 = "insert into pay_leave_transaction (comp_code,EMP_CODE,EMP_NAME,MANAGER,LEAVE_TYPE,FROM_DATE,TO_DATE,NO_OF_DAYS,LEAVE_REASON,LEAVE_APPLY_DATE) values ( (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),(select reporting_to from pay_employee_master where emp_code='" + EMP_CODE + "'),(select concat(leave_name,'-',abbreviation) from pay_leave_master where leave_name='" + LeaveType + "'),str_to_date('" + Start_date + "','%d/%m/%Y'),str_to_date('" + End_date + "', '%d/%m/%Y'),'" + Number_of_leave + "','" + Reason_for_leave + "',date(now()))";

            MySqlCommand command = new MySqlCommand(query1, d.con);
            d.con.Open();
            command.ExecuteNonQuery();

            d.con.Close();
            res = d.operation("Insert into pay_android_notification (not_read,EMP_CODE,notification,page_name) values ('0',(select reporting_to from pay_employee_master where emp_code='" + EMP_CODE + "'),(select concat('Android:Message from ',emp_name,' -Leave Approval') as emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'Leave_form.aspx')");

        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally LeaveApply....");
        }

    }

    //HOUSEKEEPING Leave Apply Details Insert
    public void Houskeeping_Employee_Leave_Apply(String EMP_CODE, string Reporting_to, string Reason_for_leave, string Start_date, 
        string End_date, string Number_of_leave)
    {
        int res;
        try
        {

            // leave apply notification send to reporting to employee query
            //string query1 = " select  LEAVE_ID,emp_code,emp_name,`GRADE_CODE`,unit_name,client_name,date_format(from_date,'%d/%m/%Y') as 'from_date' ,date_format(to_date,'%d/%m/%Y') as 'to_date',NO_OF_DAYS LEAVE_REASON,LEAVE_STATUS,STATUS_COMMENT from  pay_leave_transaction  INNER JOIN `pay_unit_master` ON `pay_leave_transaction`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_leave_transaction`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_client_master` ON `pay_leave_transaction`.`client_code` = `pay_client_master`.`client_code` AND `pay_leave_transaction`.`comp_code` = `pay_client_master`.`comp_code` where reporing_to='I02701' and (LEAVE_STATUS="" || LEAVE_STATUS=null) and android_flag=0 ";
            // 

            log.Error("Android - Inside Houskeeping_Employee_Leave_Apply");
            string reporting_to_get_operationetails = d.getsinglestring("select  emp_code from pay_op_management where comp_code=(select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "') and client_code=(select client_code from pay_employee_master where emp_code='" + EMP_CODE + "') and unit_code=(select unit_code from pay_employee_master where emp_code='" + EMP_CODE + "') order by pay_op_management.id desc limit 1");
            //string query1 = "insert into pay_leave_transaction(comp_code,unit_code,client_code,emp_code,emp_name,MANAGER,reporing_to,GRADE_CODE,FROM_DATE,TO_DATE,NO_OF_DAYS,LEAVE_REASON,LEAVE_APPLY_DATE) select comp_code,unit_code,client_code,emp_code,emp_name,reporting_to,reporting_to,GRADE_CODE,str_to_date('" + Start_date + "','%d/%m/%Y'),str_to_date('" + End_date + "', '%d/%m/%Y'),'" + Number_of_leave + "','" + Reason_for_leave + "',date(now()) from pay_employee_master where emp_code='"+EMP_CODE+"'";
            string query1 = "insert into pay_leave_transaction(comp_code,unit_code,client_code,emp_code,emp_name,MANAGER,reporing_to,GRADE_CODE,FROM_DATE,TO_DATE,NO_OF_DAYS,LEAVE_REASON,LEAVE_APPLY_DATE) select comp_code,unit_code,client_code,emp_code,emp_name,reporting_to,'" + reporting_to_get_operationetails + "',GRADE_CODE,str_to_date('" + Start_date + "','%d/%m/%Y'),str_to_date('" + End_date + "', '%d/%m/%Y'),'" + Number_of_leave + "','" + Reason_for_leave + "',date(now()) from pay_employee_master where emp_code='" + EMP_CODE + "'";
           
            log.Error("Android - Inside Houskeeping_Employee_Leave_Apply "+query1);
            MySqlCommand command = new MySqlCommand(query1, d.con);
            d.con.Open();
            command.ExecuteNonQuery();

            d.con.Close();
           // res = d.operation("Insert into pay_android_notification (not_read,EMP_CODE,notification,page_name) values ('0',(select reporting_to from pay_employee_master where emp_code='" + EMP_CODE + "'),(select concat('Android:Message from ',emp_name,' -Leave Approval') as emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'Leave_form.aspx')");

        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Houskeeping_Employee_Leave_Apply....");
        }

    }


    // HOUSEKEEPING Apply Leave notification 

    public DataTable GetHKEmployeeLeaveApplyNotification(string EMP_CODE,string leave_status)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("grade_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("from_date", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("to_date", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("no_of_days", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("leave_reason", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("leave_status", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("status_comment", typeof(String)));
        

        try
        {

              // Notes : 0	leave apply
                     //  1	send android notification and update this entry set android_flag='1'
                    //   2	Admin check leave application then set  approve  android_flage='2'
                    //   3	admin reject  leave application then set android_flag='3'
                    //   4	send android notification and update android_flag='4'

            string query="";
            log.Error("Android - Inside HK Employee Leave Notification....");
            if (leave_status == "0") {
                query = "select  LEAVE_ID,emp_code,emp_name,grade_code,unit_name,client_name,date_format(from_date,'%d/%m/%Y') as 'from_date' ,date_format(to_date,'%d/%m/%Y') as 'to_date',NO_OF_DAYS as 'no_of_days', LEAVE_REASON as 'leave_reason',LEAVE_STATUS as 'leave_status',STATUS_COMMENT as 'status_comment' from  pay_leave_transaction  INNER JOIN `pay_unit_master` ON `pay_leave_transaction`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_leave_transaction`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_client_master` ON `pay_leave_transaction`.`client_code` = `pay_client_master`.`client_code` AND `pay_leave_transaction`.`comp_code` = `pay_client_master`.`comp_code` where reporing_to='" + EMP_CODE + "' and (LEAVE_STATUS='' || LEAVE_STATUS=null) and android_flag=0";
            }
            else if (leave_status == "2")
            {
                query = " select  LEAVE_ID,emp_code,emp_name,grade_code,unit_name,client_name,date_format(from_date,'%d/%m/%Y') as 'from_date' ,date_format(to_date,'%d/%m/%Y') as 'to_date',NO_OF_DAYS as 'no_of_days', LEAVE_REASON as 'leave_reason',LEAVE_STATUS as 'leave_status',STATUS_COMMENT as 'status_comment' from  pay_leave_transaction  INNER JOIN `pay_unit_master` ON `pay_leave_transaction`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_leave_transaction`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_client_master` ON `pay_leave_transaction`.`client_code` = `pay_client_master`.`client_code` AND `pay_leave_transaction`.`comp_code` = `pay_client_master`.`comp_code`  where reporing_to='" + EMP_CODE + "'  and android_flag in ('2','3')";

            }
            else if (leave_status == "3")
            {

                query = " select  LEAVE_ID,emp_code,emp_name,grade_code,unit_name,client_name,date_format(from_date,'%d/%m/%Y') as 'from_date' ,date_format(to_date,'%d/%m/%Y') as 'to_date',NO_OF_DAYS as 'no_of_days', LEAVE_REASON as 'leave_reason',LEAVE_STATUS as 'leave_status',STATUS_COMMENT as 'status_comment' from  pay_leave_transaction  INNER JOIN `pay_unit_master` ON `pay_leave_transaction`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_leave_transaction`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_client_master` ON `pay_leave_transaction`.`client_code` = `pay_client_master`.`client_code` AND `pay_leave_transaction`.`comp_code` = `pay_client_master`.`comp_code`  where emp_code='" + EMP_CODE + "'  and app_emp_flag='1'";

            }
            log.Error("Android - Inside HK Employee Leave Notification...."+query);
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            //if (reader.HasRows)
            //{
                while (reader.Read())
                {
                    //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);

                    userDetailsTable.Rows.Add(reader["emp_name"].ToString(), reader["client_name"].ToString(), reader["unit_name"].ToString()
                        , reader["grade_code"].ToString(), reader["from_date"].ToString(), reader["to_date"].ToString(), reader["no_of_days"].ToString(), reader["leave_reason"].ToString(),
                        reader["leave_status"].ToString(), reader["status_comment"].ToString());
                    if (leave_status == "0")
                    {
                    d.operation("Update pay_leave_transaction set android_flag='1' where LEAVE_ID = '" + reader["LEAVE_ID"].ToString() + "'");
                    }
                    else if (leave_status == "2")
                    {
                        d.operation("Update pay_leave_transaction set android_flag='4', app_emp_flag='1' where LEAVE_ID = '" + reader["LEAVE_ID"].ToString() + "'");
                    }
                    else if (leave_status == "3")
                    {
                        d.operation("Update pay_leave_transaction set app_emp_flag='2' where LEAVE_ID = '" + reader["LEAVE_ID"].ToString() + "'");
                    }
                }
            //}

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally HK Employee Leave Notification...." + userDetailsTable);
        }
        return userDetailsTable;
    }


    // Apply Leave notification 

    public DataTable GetEmployeeLeaveApplyNotification(string EMP_CODE)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("NOTIFICATION", typeof(String)));


        try
        {

           // log.Error("Android - Inside Employee Leave Notification....");
            string query = "SELECT notification as NOTIFICATION FROM pay_android_notification WHERE EMP_CODE = '" + EMP_CODE + "' and not_read='0'";

            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);

                    userDetailsTable.Rows.Add(reader["NOTIFICATION"].ToString());
                    d.operation("Update pay_android_notification set not_read='1' where EMP_CODE = '" + EMP_CODE + "'");
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee Leave Notification....");
        }
        return userDetailsTable;
    }


    //---- Attendance Details of an Employee ---//
    public DataTable GetAttendanceDetails(string EMP_CODE, string SELECT_DATE)
    {
        DataTable userDetailsTable = new DataTable();
        //userDetailsTable.Columns.Add(new DataColumn("firstName", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("lastName", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("TOT_DAYS_PRESENT", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("TOT_DAYS_ABSENT", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("TOT_HALF_DAYS", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("TOT_LEAVES", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("WEEKLY_OFF", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("HOLIDAYS", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("TOT_WORKING_DAYS", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("MONTH", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("YEAR", typeof(String)));
        try
        {
            log.Info("Android - Inside GetAttendanceData....");
            string month = SELECT_DATE.Substring(0, 2).ToString();
            string year = SELECT_DATE.Substring(3, 4).ToString();
            string query = "SELECT TOT_DAYS_PRESENT,TOT_DAYS_ABSENT,TOT_HALF_DAYS,TOT_LEAVES,WEEKLY_OFF,HOLIDAYS,TOT_WORKING_DAYS,MONTH,YEAR FROM pay_attendance_muster WHERE EMP_CODE = '" + EMP_CODE + "' AND MONTH='" + month + "' AND YEAR='" + year + "'";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);

                    userDetailsTable.Rows.Add(reader["TOT_DAYS_PRESENT"].ToString(), reader["TOT_DAYS_ABSENT"].ToString(),
                        reader["TOT_HALF_DAYS"].ToString(), reader["TOT_LEAVES"].ToString(), reader["WEEKLY_OFF"].ToString()
                        , reader["HOLIDAYS"].ToString(), reader["TOT_WORKING_DAYS"].ToString(), reader["MONTH"].ToString(),
                        reader["YEAR"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetAttendanceData....");
        }
        return userDetailsTable;
    }

    //----- Get Salary Data of an Employee ---------
    public bool GetSalaryData(string EMP_CODE, string SELECT_DATE)
    {
        string thisMonth = SELECT_DATE.Substring(0, 2).ToString();
        string thisYear = SELECT_DATE.Substring(3);
        bool auth = false;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string month_name = mfi.GetMonthName(int.Parse(thisMonth.ToString()));
         
        d1.con1.Open();
        try
        {
            //MySqlCommand cmd_hol = new MySqlCommand("select emp_code from pay_attendance where emp_code='" + EMP_CODE + "' and month='" + thisMonth + "' and year='" + thisYear + "'", d1.con1);
            //MySqlCommand cmd_hol = new MySqlCommand("select billing_unit_code from pay_billing_master_history where comp_code=(SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') and billing_unit_code=(SELECT `unit_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') and month='" + thisMonth + "'  and year=''" + thisYear + "'", d1.con1);
            //MySqlCommand cmd_hol = new MySqlCommand("select unit_code from pay_salary_unit_rate where unit_code = (SELECT `unit_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') and month='" + thisMonth + "' and year='" + thisYear + "'", d1.con1);
            MySqlCommand cmd_hol = new MySqlCommand("SELECT unit_code,designation,comp_code,client_code,emp_code  FROM `pay_pro_master` WHERE `pay_pro_master`.`month` = '" + thisMonth + "' AND `pay_pro_master`.`year` = '" + thisYear + "' and `pay_pro_master`.`emp_code`='" + EMP_CODE + "' AND payment_status= '1' AND `pay_pro_master`.`Total_Days_Present` > 0 ", d1.con1);
            log.Error("salary cmd_hol ...." + "SELECT unit_code,designation,comp_code,client_code,emp_code  FROM `pay_pro_master` WHERE `pay_pro_master`.`month` = '" + thisMonth + "' AND `pay_pro_master`.`year` = '" + thisYear + "' and `pay_pro_master`.`emp_code`='" + EMP_CODE + "' AND payment_status= '1' AND `pay_pro_master`.`Total_Days_Present` > 0");
            MySqlDataReader dr_hol = cmd_hol.ExecuteReader();

            if (dr_hol.Read())
            {
                if (dr_hol.GetValue(0).ToString() == "" || dr_hol.GetValue(0).ToString() == null)
                {
                    auth = false;
                    log.Error("Android - Inside GetSalaryData....First call" + auth);
                    log.Error("salary cmd_hol ...." + dr_hol.GetValue(0).ToString() + "/" + dr_hol.GetValue(1).ToString() + "/" + dr_hol.GetValue(2).ToString() + "/" + dr_hol.GetValue(3).ToString());
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Month End Process completed, You cannot make changes !!')", true);
                    // d1.con1.Close();
                    //return auth;
                }
                else
                {
                    try
                    {
                        DAL d = new DAL();
                        ReportDocument crystalReport = new ReportDocument();
                        string query = "";


                        log.Error("Android - Inside GetSalaryData....");
                        // crystalReport.Load(HttpContext.Current.Server.MapPath("~/Salary_Slip_peremp.rpt"));
                        //DateTime dt = DateTime.Today;
                        //if (dr_hol.GetValue(2).ToString()=="C02")
                        //{
                        //    crystalReport.Load(HttpContext.Current.Server.MapPath("~/Salary_Slip_C02.rpt"));
                        //}
                        //else
                        //{
                        //    crystalReport.Load(HttpContext.Current.Server.MapPath("~/Salary_Slip.rpt"));
                        //}
                        string path1 = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\");
                        string ot_applicable = d.getsinglestring("SELECT comp_logo from pay_company_master where comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')");
                        string companyimagepath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Images\\" + ot_applicable);

                        crystalReport.Load(HttpContext.Current.Server.MapPath("~/Salary_Slip.rpt"));
                        string month_year = month_name + " " + thisYear;
                        // string month_year = thisMonth + " " + thisYear;
                        crystalReport.DataDefinition.FormulaFields["salary_monthyear"].Text = @"'" + month_year + "'";
                        crystalReport.DataDefinition.FormulaFields["company_image_path"].Text = @"'" + companyimagepath + "'";

                        if (dr_hol.GetValue(2).ToString().Equals("C01"))
                        { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path1 + "C01_stamp.jpg" + "'"; }
                        else
                        { crystalReport.DataDefinition.FormulaFields["stamp"].Text = @"'" + path1 + "C02_stamp.png" + "'"; }

                        // log.Error("Android - Inside GetSalaryData...." + "SELECT `comp_code`, `COMPANY_NAME`, `ADDRESS1`, `ADDRESS2`, `CITY`, `STATE`, `UnitState`, `Unit_City`, `Client`, `grade`, `Unitcode`, `ihms_code`, `Emp_Name`, `Emp_Code`, `Emp_Father`, `Emp_City`, `Joining_Date`, `UAN_No`, `PF_No`, `PAN_No`, `ESI_No`, `PerDayRate`, `Basic`, `Vda`, `emp_basic_vda` AS 'basic_vda', `hra_amount` AS 'hra', `sal_bonus_gross` AS 'Bonus_taxable', `sal_bonus_after_gross` 'bonus', `leave_sal_gross` 'leave_taxable', `leave_sal_after_gross` AS 'leaveDays', `washing_salary` AS 'washing', `travelling_salary` AS 'travelling', `education_salary` AS 'education', `allowances_salary` AS 'special_allo', `cca_salary` AS 'cca', `other_allow` AS 'other_allo', `gratuity_gross` AS 'Gratuity_taxable', `gratuity_after_gross` AS 'Gratuity', (((`emp_basic_vda`) / 100) * `sal_pf_percent`) AS 'PF', (((`emp_basic_vda` + `hra_amount` + `sal_bonus_gross` + `leave_sal_gross` + `washing_salary` + `travelling_salary` + `education_salary` + `allowances_salary` + `cca_salary` + `other_allow` + `gratuity_gross` + `sal_ot`) / 100) * `sal_esic_percent`) AS 'ESIC', `sal_ot` AS 'ot_amount_salary', `lwf_salary` AS 'lwf', `sal_uniform_rate` AS 'Uniform', CASE WHEN `F_PT` = 'Y' THEN CASE WHEN (`emp_basic_vda` + `hra_amount` + `sal_bonus_gross` + `leave_sal_gross` + `washing_salary` + `travelling_salary` + `education_salary` + `allowances_salary` + `cca_salary` + `other_allow` + `gratuity_gross` + `sal_ot`) < 10001 THEN 0 END ELSE `PT` END AS 'pt', `fine`, `EMP_ADVANCE_PAYMENT` AS 'advance', `Total_Days_Present`, `Payment`, `Bank_holder_name`, `BANK_EMP_AC_CODE`, `PF_IFSC_CODE`, `PF_BANK_NAME`, `BANK_BRANCH`, `Working_Days`, `Bonus_Policy` FROM (SELECT `pay_company_master`.`comp_code`, `pay_company_master`.`COMPANY_NAME`, `pay_company_master`.`ADDRESS1`, `pay_company_master`.`ADDRESS2`, `pay_company_master`.`CITY`, `pay_company_master`.`STATE`, `pay_unit_master`.`state_name` AS 'UnitState', `unit_city` AS 'Unit_City', (SELECT `client_name` FROM `pay_client_master` WHERE `comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `client_code` = `pay_unit_master`.`client_code`) AS 'Client', (SELECT `grade_desc` FROM `pay_grade_master` WHERE `comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `grade_code` = `pay_salary_unit_rate`.`designation`) AS 'grade', `pay_unit_master`.`unit_code` AS 'Unitcode', `pay_employee_master`.`ihmscode` AS 'ihms_code', `pay_employee_master`.`emp_name` AS 'Emp_Name', `pay_employee_master`.`emp_code` AS 'Emp_Code', `pay_employee_master`.`EMP_FATHER_NAME` AS 'Emp_Father', `pay_employee_master`.`EMP_CURRENT_CITY` AS 'Emp_City', `pay_employee_master`.`JOINING_DATE` AS 'Joining_Date', `pay_employee_master`.`PAN_NUMBER` AS 'UAN_No', `pay_employee_master`.`PF_NUMBER` AS 'PF_No', `pay_employee_master`.`EMP_NEW_PAN_NO` AS 'PAN_No', `pay_employee_master`.`ESIC_NUMBER` AS 'ESI_No', `pay_billing_master`.`per_rate_salary` AS 'PerDayRate', `pay_billing_master`.`basic_salary` AS 'Basic', `pay_billing_master`.`vda_salary` AS 'Vda', (((`pay_billing_master`.`basic_salary` + `pay_billing_master`.`vda_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'emp_basic_vda', ((`pay_salary_unit_rate`.`hra_amount_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'hra_amount', CASE WHEN `bonus_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN `bonus_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN `leave_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'leave_sal_gross', CASE WHEN `leave_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'leave_sal_after_gross', ((`pay_salary_unit_rate`.`washing_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'washing_salary', ((`pay_salary_unit_rate`.`travelling_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'travelling_salary', ((`pay_salary_unit_rate`.`education_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'education_salary', ((`pay_salary_unit_rate`.`allowances_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'allowances_salary', CASE WHEN `pay_employee_master`.`cca` = 0 THEN ((`pay_salary_unit_rate`.`cca_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`cca` END AS 'cca_salary', CASE WHEN `pay_employee_master`.`special_allow` = 0 THEN ((`pay_salary_unit_rate`.`other_allow` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`special_allow` END AS 'other_allow', CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END AS 'gratuity_gross', CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END AS 'gratuity_after_gross', `pay_billing_master`.`sal_esic` AS 'sal_esic_percent', `pay_billing_master`.`sal_pf` AS 'sal_pf_percent', ((`pay_salary_unit_rate`.`ot_amount` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'sal_ot', `pay_salary_unit_rate`.`lwf_salary`, `pay_salary_unit_rate`.`sal_uniform_rate`, CASE WHEN `pay_employee_master`.`Gender` = 'F' THEN CASE WHEN `pay_unit_master`.`state_name` = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(`SLAB_AMOUNT`) FROM `pay_pt_slab_master` WHERE `STATE_NAME` = `pay_unit_master`.`state_name` AND (((((`pay_billing_master`.`basic_salary` + `pay_billing_master`.`vda_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`hra_amount_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `bonus_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END + CASE WHEN `leave_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END + ((`pay_salary_unit_rate`.`washing_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`travelling_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`education_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`allowances_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `pay_employee_master`.`cca` = 0 THEN ((`pay_salary_unit_rate`.`cca_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`cca` END + ((`pay_salary_unit_rate`.`other_allow` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END + ((`pay_salary_unit_rate`.`ot_amount` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`)) BETWEEN `FROM_AMOUNT` AND `TO_AMOUNT`) AND (STR_TO_DATE('01/" + thisMonth + "/" + thisYear + "', '%d/%m/%Y') BETWEEN STR_TO_DATE('01/" + thisMonth + "/" + thisYear + "','%d/%m/%Y') AND STR_TO_DATE('01/" + thisMonth + "/2019','%d/%m/%Y'))), 0) AS 'PT', `pay_employee_master`.`fine`, `pay_employee_master`.`EMP_ADVANCE_PAYMENT`, CASE WHEN `pay_attendance_muster`.`tot_days_present` IS NULL THEN 0 ELSE `pay_attendance_muster`.`tot_days_present` END AS 'Total_Days_Present', CASE WHEN FLOOR(((`pay_salary_unit_rate`.`total_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) IS NULL THEN 0 ELSE FLOOR(((`pay_salary_unit_rate`.`total_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) END AS 'Payment', `pay_employee_master`.`Bank_holder_name`, `pay_employee_master`.`BANK_EMP_AC_CODE`, `pay_employee_master`.`PF_IFSC_CODE`, `pay_employee_master`.`PF_BANK_NAME`, `pay_employee_master`.`BANK_BRANCH`, `pay_salary_unit_rate`.`month_days` AS 'Working_Days', `pay_billing_master`.`bonus_policy_salary` AS 'Bonus_Policy', `pay_billing_master`.`bonus_taxable_salary` AS 'Bonus_taxable', `pay_billing_master`.`leave_taxable_salary` AS 'leave_taxable', `pay_billing_master`.`gratuity_taxable_salary` AS 'Gratuity_taxable', `pay_billing_master`.`gratuity_salary` AS 'Gratuity_BM', `pay_salary_unit_rate`.`gratuity_salary` AS 'Gratuity', `pay_salary_unit_rate`.`leave_days` AS 'leaveDays', `pay_billing_master`.`leave_days_percent` AS 'Leave_Percent' FROM `pay_employee_master` INNER JOIN `pay_attendance_muster` ON `pay_attendance_muster`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_attendance_muster`.`comp_code` = `pay_employee_master`.`comp_code` INNER JOIN `pay_unit_master` ON `pay_attendance_muster`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_attendance_muster`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_salary_unit_rate` ON `pay_attendance_muster`.`unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_attendance_muster`.`month` = `pay_salary_unit_rate`.`month` AND `pay_attendance_muster`.`year` = `pay_salary_unit_rate`.`year` INNER JOIN `pay_billing_master` ON `pay_billing_master`.`comp_code` = `pay_salary_unit_rate`.`comp_code` AND `pay_billing_master`.`billing_client_code` = `pay_salary_unit_rate`.`client_code` AND `pay_billing_master`.`billing_unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_employee_master`.`grade_code` = `pay_billing_master`.`designation` AND `pay_billing_master`.`designation` = `pay_salary_unit_rate`.`designation` AND `pay_billing_master`.`hours` = `pay_salary_unit_rate`.`hours` INNER JOIN `pay_company_master` ON `pay_employee_master`.`comp_code` = `pay_company_master`.`comp_code` WHERE `pay_company_master`.`comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `pay_attendance_muster`.`unit_code` = (SELECT `unit_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `pay_attendance_muster`.`month` = '" + thisMonth + "' AND `pay_attendance_muster`.`year` = '" + thisYear + "' AND `pay_attendance_muster`.`emp_code` = '" + EMP_CODE + "'	 AND `pay_attendance_muster`.`tot_days_present` > 0) AS payslip ");
                        //query = "SELECT pay_company_master.comp_code, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_company_master.PIN, pay_company_master.PF_REG_NO, pay_company_master.ESIC_REG_NO, pay_company_master.E_HEAD01 AS 'HEAD01', pay_company_master.E_HEAD02 AS 'HEAD02', pay_company_master.E_HEAD03 AS 'HEAD03', pay_company_master.E_HEAD04 AS 'HEAD04', pay_company_master.E_HEAD05 AS 'HEAD05', pay_company_master.E_HEAD06 AS 'HEAD06', pay_company_master.E_HEAD07 AS 'HEAD07', pay_company_master.E_HEAD08 AS 'HEAD08', pay_company_master.E_HEAD09 AS 'HEAD09', pay_company_master.E_HEAD10 AS 'HEAD10', pay_company_master.E_HEAD11 AS 'HEAD11', pay_company_master.E_HEAD12 AS 'HEAD12', pay_company_master.L_HEAD01 AS 'LHEAD01', pay_company_master.L_HEAD02 AS 'LHEAD02', pay_company_master.L_HEAD03 AS 'LHEAD03', pay_company_master.L_HEAD04 AS 'LHEAD04', pay_company_master.D_HEAD01 AS 'DHEAD01', pay_company_master.D_HEAD02 AS 'DHEAD02', pay_company_master.D_HEAD03 AS 'DHEAD03', pay_company_master.D_HEAD04 AS 'DHEAD04', pay_company_master.D_HEAD05 AS 'DHEAD05', pay_attendance.Year As CURRENT_YEAR, pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE', pay_employee_master.GENDER, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_BANK_NAME AS 'BANK_CODE', pay_employee_master.STATUS, pay_employee_master.PF_NUMBER, pay_employee_master.E_HEAD01, pay_employee_master.E_HEAD02, pay_employee_master.E_HEAD03, pay_employee_master.E_HEAD04, pay_employee_master.E_HEAD05, pay_employee_master.E_HEAD06, pay_employee_master.E_HEAD07, pay_employee_master.E_HEAD08, pay_employee_master.E_HEAD09, pay_employee_master.E_HEAD10, pay_employee_master.E_HEAD11, pay_employee_master.E_HEAD12, pay_employee_master.PF_SHEET, pay_employee_master.EARN_TOTAL, pay_employee_master.LEFT_REASON, pay_unit_master.UNIT_NAME, pay_unit_master.UNIT_ADD1, pay_unit_master.UNIT_ADD2, pay_unit_master.UNIT_CITY, pay_attendance.PRESENT_DAYS, pay_attendance.LEAVE_DAYS, pay_attendance.PAYABLE_DAYS, pay_attendance.OT_HRS, pay_attendance.L_HEAD01, pay_attendance.L_HEAD02, pay_attendance.L_HEAD03, pay_attendance.L_HEAD04, pay_attendance.D_HEAD01, pay_attendance.D_LOAN, pay_attendance.D_HEAD02, pay_attendance.D_HEAD03, pay_attendance.D_HEAD04, pay_attendance.D_HEAD05, pay_attendance.INCOMETAX, pay_attendance.C_HEAD01, pay_attendance.C_HEAD02, pay_attendance.C_HEAD03, pay_attendance.C_HEAD04, pay_attendance.C_HEAD05, pay_attendance.C_HEAD06, pay_attendance.C_HEAD07, pay_attendance.C_HEAD08, pay_attendance.C_HEAD09, pay_attendance.C_HEAD10, pay_attendance.C_HEAD11, pay_attendance.C_HEAD12, pay_attendance.PF, pay_attendance.ESIC_TOT, pay_attendance.OT_GROSS, pay_attendance.PTAX, pay_grade_master.GRADE_DESC, pay_employee_master.ESIC_NUMBER, pay_attendance.TOT_ESIC_GROSS, pay_attendance.ESIC_COMP_CONTRI, pay_attendance.MLWF, pay_company_master.D_HEAD06 AS 'DHEAD06', pay_company_master.D_HEAD07 AS 'DHEAD07', pay_company_master.D_HEAD08 AS 'DHEAD08', pay_company_master.D_HEAD09 AS 'DHEAD09', pay_attendance.D_HEAD06, pay_attendance.D_HEAD07, pay_attendance.D_HEAD08, pay_attendance.D_HEAD09, pay_attendance.CGRADE_CODE, '0' AS EXTRA_DAYS, pay_attendance.EXTRA_GROSS, pay_attendance.UNIT_CODE, pay_attendance.CPF_SHEET, pay_department_master.dept_name, pay_attendance.ABSENT_DAYS, pay_employee_master.EMP_NEW_PAN_NO AS 'UAN', pay_attendance.COMP_PF_PEN, pay_attendance.ESIC_COMP_CONTRI, '0' As TOT_CL, '0' As TOT_PL FROM pay_attendance INNER JOIN pay_employee_master ON pay_attendance.comp_code = pay_employee_master.comp_code AND pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE INNER JOIN pay_company_master ON pay_attendance.comp_code = pay_company_master.comp_code INNER JOIN pay_unit_master ON pay_attendance.comp_code = pay_unit_master.comp_code AND pay_attendance.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN pay_grade_master ON pay_attendance.CGRADE_CODE = pay_grade_master.GRADE_CODE AND pay_attendance.comp_code = pay_grade_master.comp_code INNER JOIN pay_department_master ON pay_employee_master.dept_code = pay_department_master.dept_code INNER JOIN pay_attendance_muster ON pay_attendance.emp_code = pay_attendance_muster.emp_code AND pay_attendance.month = pay_attendance_muster.month AND pay_attendance.year = pay_attendance_muster.year WHERE pay_attendance.PAYABLE_DAYS>0 AND pay_company_master.comp_code in (select comp_code from pay_employee_master where emp_code = '" + EMP_CODE + "') AND pay_attendance.MONTH = '" + thisMonth + "' AND pay_attendance.YEAR = '" + thisYear + "' and pay_attendance.emp_code = '" + EMP_CODE + "'";
                        //old query query = "SELECT pay_company_master.comp_code, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_company_master.PIN, pay_company_master.PF_REG_NO, pay_company_master.ESIC_REG_NO, pay_company_master.E_HEAD01 , pay_company_master.E_HEAD02 , pay_company_master.E_HEAD03 , pay_company_master.E_HEAD04, pay_company_master.E_HEAD05, pay_company_master.E_HEAD06 , pay_company_master.E_HEAD07, pay_company_master.E_HEAD08 , pay_company_master.E_HEAD09 , pay_company_master.E_HEAD10 , pay_company_master.E_HEAD11 , pay_company_master.E_HEAD12 , pay_company_master.L_HEAD01 , pay_company_master.L_HEAD02 , pay_company_master.L_HEAD03 , pay_company_master.L_HEAD04, pay_company_master.D_HEAD01 , pay_company_master.D_HEAD02 , pay_company_master.D_HEAD03 , pay_company_master.D_HEAD04 , pay_company_master.D_HEAD05 , pay_attendance.Year , pay_employee_master.EMP_CODE, pay_employee_master.EMP_NAME, pay_employee_master.EMP_FATHER_NAME, pay_employee_master.JOINING_DATE, pay_employee_master.GENDER, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_BANK_NAME, cast(concat(pay_attendance.MONTH,'/',pay_attendance.YEAR) as char) AS 'STATUS', pay_employee_master.PF_NUMBER, (pay_salary_unit_rate.Basic + pay_salary_unit_rate.DA) as E_HEAD01, pay_salary_unit_rate.special_allowance as E_HEAD02, pay_salary_unit_rate.Bonus_rate as E_HEAD03, pay_salary_unit_rate.HRA as E_HEAD04, pay_salary_unit_rate.CCA as E_HEAD05, 0, 0, 0, 0, 0, 0, 0, pay_employee_master.PF_SHEET, pay_employee_master.EARN_TOTAL, pay_employee_master.LEFT_REASON, pay_unit_master.UNIT_NAME, pay_unit_master.UNIT_ADD1, pay_unit_master.UNIT_ADD2, pay_unit_master.UNIT_CITY, pay_attendance.PRESENT_DAYS, pay_attendance.LEAVE_DAYS, pay_attendance.PAYABLE_DAYS, pay_attendance.OT_HRS, pay_attendance.L_HEAD01, pay_attendance.L_HEAD02, pay_attendance.L_HEAD03, pay_attendance.L_HEAD04, pay_attendance.D_HEAD01, pay_attendance.D_LOAN, pay_attendance.D_HEAD02, pay_attendance.D_HEAD03, pay_attendance.D_HEAD04, pay_attendance.D_HEAD05, pay_attendance.INCOMETAX, pay_attendance.C_HEAD01, pay_attendance.C_HEAD02, pay_attendance.C_HEAD03, pay_attendance.C_HEAD04, pay_attendance.C_HEAD05, pay_attendance.C_HEAD06, pay_attendance.C_HEAD07, pay_attendance.C_HEAD08, pay_attendance.C_HEAD09, pay_attendance.C_HEAD10, pay_attendance.C_HEAD11, pay_attendance.C_HEAD12, pay_attendance.PF, pay_attendance.ESIC as ESIC_TOT, pay_attendance.OT_GROSS, pay_attendance.PTAX, pay_grade_master.GRADE_DESC, pay_employee_master.ESIC_NUMBER, pay_attendance.TOT_ESIC_GROSS, pay_attendance.ESIC_COMP_CONTRI, pay_attendance.MLWF, pay_company_master.D_HEAD06 , pay_company_master.D_HEAD07 , pay_company_master.D_HEAD08 , pay_company_master.D_HEAD09 , pay_attendance.D_HEAD06, pay_attendance.D_HEAD07, pay_attendance.D_HEAD08, pay_attendance.D_HEAD09, pay_attendance.CGRADE_CODE, pay_attendance.EXTRA_GROSS, pay_attendance.UNIT_CODE, pay_attendance.CPF_SHEET, pay_department_master.dept_name, pay_attendance.ABSENT_DAYS, pay_employee_master.EMP_NEW_PAN_NO , pay_attendance.COMP_PF_PEN, pay_attendance.ESIC_COMP_CONTRI, pay_company_master.COMPANY_PAN_NO, pay_company_master.COMPANY_TAN_NO, pay_company_master.COMPANY_CIN_NO, pay_employee_master.ihmscode as ihmscode,pay_employee_master.LOCATION,pay_employee_master.LOCATION_CITY,pay_employee_master.client_code FROM pay_attendance INNER JOIN pay_employee_master ON pay_attendance.comp_code = pay_employee_master.comp_code AND pay_attendance.EMP_CODE = pay_employee_master.EMP_CODE INNER JOIN pay_company_master ON pay_attendance.comp_code = pay_company_master.comp_code INNER JOIN pay_unit_master ON pay_attendance.comp_code = pay_unit_master.comp_code AND pay_attendance.UNIT_CODE = pay_unit_master.UNIT_CODE INNER JOIN pay_grade_master ON pay_attendance.CGRADE_CODE = pay_grade_master.GRADE_CODE AND pay_attendance.comp_code = pay_grade_master.comp_code INNER JOIN pay_department_master ON pay_employee_master.dept_code = pay_department_master.dept_code INNER JOIN pay_salary_unit_rate ON pay_salary_unit_rate.UNIT_CODE = pay_attendance.UNIT_CODE and pay_attendance.CGRADE_CODE = pay_salary_unit_rate.grade_code INNER JOIN pay_attendance_muster ON pay_attendance.emp_code = pay_attendance_muster.emp_code AND pay_attendance.month = pay_attendance_muster.month AND pay_attendance.year = pay_attendance_muster.year  WHERE pay_attendance.PAYABLE_DAYS>0 AND pay_company_master.comp_code in (select comp_code from pay_employee_master where emp_code = '" + EMP_CODE + "') AND pay_attendance.UNIT_CODE  in (select unit_code from pay_employee_master where emp_code = '" + EMP_CODE + "') AND pay_attendance.MONTH = '" + thisMonth + "' AND pay_attendance.YEAR = '" + thisYear + "' and pay_attendance.emp_code = '" + EMP_CODE + "'";
                        // query = "SELECT `comp_code`, `COMPANY_NAME`, `ADDRESS1`, `ADDRESS2`, `CITY`, `STATE`, `UnitState`, `Unit_City`, `Client`, `grade`, `Unitcode`, `ihms_code`, `Emp_Name`, `Emp_Code`, `Emp_Father`, `Emp_City`, `Joining_Date`, `UAN_No`, `PF_No`, `PAN_No`, `ESI_No`, `PerDayRate`, `Basic`, `Vda`, `emp_basic_vda` AS 'basic_vda', `hra_amount` AS 'hra', `sal_bonus_gross` AS 'Bonus_taxable', `sal_bonus_after_gross` 'bonus', `leave_sal_gross` 'leave_taxable', `leave_sal_after_gross` AS 'leaveDays', `washing_salary` AS 'washing', `travelling_salary` AS 'travelling', `education_salary` AS 'education', `allowances_salary` AS 'special_allo', `cca_salary` AS 'cca', `other_allow` AS 'other_allo', `gratuity_gross` AS 'Gratuity_taxable', `gratuity_after_gross` AS 'Gratuity', (((`emp_basic_vda`) / 100) * `sal_pf_percent`) AS 'PF', (((`emp_basic_vda` + `hra_amount` + `sal_bonus_gross` + `leave_sal_gross` + `washing_salary` + `travelling_salary` + `education_salary` + `allowances_salary` + `cca_salary` + `other_allow` + `gratuity_gross` + `sal_ot`) / 100) * `sal_esic_percent`) AS 'ESIC', `sal_ot` AS 'ot_amount_salary', `lwf_salary` AS 'lwf', `sal_uniform_rate` AS 'Uniform', CASE WHEN `F_PT` = 'Y' THEN CASE WHEN (`emp_basic_vda` + `hra_amount` + `sal_bonus_gross` + `leave_sal_gross` + `washing_salary` + `travelling_salary` + `education_salary` + `allowances_salary` + `cca_salary` + `other_allow` + `gratuity_gross` + `sal_ot`) < 10001 THEN 0 END ELSE `PT` END AS 'pt', `fine`, `EMP_ADVANCE_PAYMENT` AS 'advance', `Total_Days_Present`, `Payment`, `Bank_holder_name`, `BANK_EMP_AC_CODE`, `PF_IFSC_CODE`, `PF_BANK_NAME`, `BANK_BRANCH`, `Working_Days`, `Bonus_Policy` FROM (SELECT `pay_company_master`.`comp_code`, `pay_company_master`.`COMPANY_NAME`, `pay_company_master`.`ADDRESS1`, `pay_company_master`.`ADDRESS2`, `pay_company_master`.`CITY`, `pay_company_master`.`STATE`, `pay_unit_master`.`state_name` AS 'UnitState', `unit_city` AS 'Unit_City', (SELECT `client_name` FROM `pay_client_master` WHERE `comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `client_code` = `pay_unit_master`.`client_code`) AS 'Client', (SELECT `grade_desc` FROM `pay_grade_master` WHERE `comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `grade_code` = `pay_salary_unit_rate`.`designation`) AS 'grade', `pay_unit_master`.`unit_code` AS 'Unitcode', `pay_employee_master`.`ihmscode` AS 'ihms_code', `pay_employee_master`.`emp_name` AS 'Emp_Name', `pay_employee_master`.`emp_code` AS 'Emp_Code', `pay_employee_master`.`EMP_FATHER_NAME` AS 'Emp_Father', `pay_employee_master`.`EMP_CURRENT_CITY` AS 'Emp_City', `pay_employee_master`.`JOINING_DATE` AS 'Joining_Date', `pay_employee_master`.`PAN_NUMBER` AS 'UAN_No', `pay_employee_master`.`PF_NUMBER` AS 'PF_No', `pay_employee_master`.`EMP_NEW_PAN_NO` AS 'PAN_No', `pay_employee_master`.`ESIC_NUMBER` AS 'ESI_No', `pay_billing_master`.`per_rate_salary` AS 'PerDayRate', `pay_billing_master`.`basic_salary` AS 'Basic', `pay_billing_master`.`vda_salary` AS 'Vda', (((`pay_billing_master`.`basic_salary` + `pay_billing_master`.`vda_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'emp_basic_vda', ((`pay_salary_unit_rate`.`hra_amount_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'hra_amount', CASE WHEN `bonus_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN `bonus_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN `leave_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'leave_sal_gross', CASE WHEN `leave_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END AS 'leave_sal_after_gross', ((`pay_salary_unit_rate`.`washing_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'washing_salary', ((`pay_salary_unit_rate`.`travelling_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'travelling_salary', ((`pay_salary_unit_rate`.`education_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'education_salary', ((`pay_salary_unit_rate`.`allowances_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'allowances_salary', CASE WHEN `pay_employee_master`.`cca` = 0 THEN ((`pay_salary_unit_rate`.`cca_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`cca` END AS 'cca_salary', CASE WHEN `pay_employee_master`.`special_allow` = 0 THEN ((`pay_salary_unit_rate`.`other_allow` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`special_allow` END AS 'other_allow', CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END AS 'gratuity_gross', CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '0' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END AS 'gratuity_after_gross', `pay_billing_master`.`sal_esic` AS 'sal_esic_percent', `pay_billing_master`.`sal_pf` AS 'sal_pf_percent', ((`pay_salary_unit_rate`.`ot_amount` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) AS 'sal_ot', `pay_salary_unit_rate`.`lwf_salary`, `pay_salary_unit_rate`.`sal_uniform_rate`, CASE WHEN `pay_employee_master`.`Gender` = 'F' THEN CASE WHEN `pay_unit_master`.`state_name` = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(`SLAB_AMOUNT`) FROM `pay_pt_slab_master` WHERE `STATE_NAME` = `pay_unit_master`.`state_name` AND (((((`pay_billing_master`.`basic_salary` + `pay_billing_master`.`vda_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`hra_amount_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `bonus_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`sal_bonus` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END + CASE WHEN `leave_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`leave_days` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END + ((`pay_salary_unit_rate`.`washing_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`travelling_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`education_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + ((`pay_salary_unit_rate`.`allowances_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `pay_employee_master`.`cca` = 0 THEN ((`pay_salary_unit_rate`.`cca_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE `pay_employee_master`.`cca` END + ((`pay_salary_unit_rate`.`other_allow` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) + CASE WHEN `pay_employee_master`.`gratuity` = 0 THEN CASE WHEN `gratuity_taxable_salary` = '1' THEN ((`pay_salary_unit_rate`.`gratuity_salary` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) ELSE 0 END ELSE `pay_employee_master`.`gratuity` END + ((`pay_salary_unit_rate`.`ot_amount` / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`)) BETWEEN `FROM_AMOUNT` AND `TO_AMOUNT`) AND (STR_TO_DATE('01/" + thisMonth + "/" + thisYear + "', '%d/%m/%Y') BETWEEN STR_TO_DATE('01/" + thisMonth + "/" + thisYear + "','%d/%m/%Y') AND STR_TO_DATE('01/" + thisMonth + "/2019','%d/%m/%Y'))), 0) AS 'PT', `pay_employee_master`.`fine`, `pay_employee_master`.`EMP_ADVANCE_PAYMENT`, CASE WHEN `pay_attendance_muster`.`tot_days_present` IS NULL THEN 0 ELSE `pay_attendance_muster`.`tot_days_present` END AS 'Total_Days_Present', CASE WHEN FLOOR(((`pay_salary_unit_rate`.`total_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) IS NULL THEN 0 ELSE FLOOR(((`pay_salary_unit_rate`.`total_salary`) / `pay_salary_unit_rate`.`month_days`) * `pay_attendance_muster`.`tot_days_present`) END AS 'Payment', `pay_employee_master`.`Bank_holder_name`, `pay_employee_master`.`BANK_EMP_AC_CODE`, `pay_employee_master`.`PF_IFSC_CODE`, `pay_employee_master`.`PF_BANK_NAME`, `pay_employee_master`.`BANK_BRANCH`, `pay_salary_unit_rate`.`month_days` AS 'Working_Days', `pay_billing_master`.`bonus_policy_salary` AS 'Bonus_Policy', `pay_billing_master`.`bonus_taxable_salary` AS 'Bonus_taxable', `pay_billing_master`.`leave_taxable_salary` AS 'leave_taxable', `pay_billing_master`.`gratuity_taxable_salary` AS 'Gratuity_taxable', `pay_billing_master`.`gratuity_salary` AS 'Gratuity_BM', `pay_salary_unit_rate`.`gratuity_salary` AS 'Gratuity', `pay_salary_unit_rate`.`leave_days` AS 'leaveDays', `pay_billing_master`.`leave_days_percent` AS 'Leave_Percent' FROM `pay_employee_master` INNER JOIN `pay_attendance_muster` ON `pay_attendance_muster`.`emp_code` = `pay_employee_master`.`emp_code` AND `pay_attendance_muster`.`comp_code` = `pay_employee_master`.`comp_code` INNER JOIN `pay_unit_master` ON `pay_attendance_muster`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_attendance_muster`.`comp_code` = `pay_unit_master`.`comp_code` INNER JOIN `pay_salary_unit_rate` ON `pay_attendance_muster`.`unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_attendance_muster`.`month` = `pay_salary_unit_rate`.`month` AND `pay_attendance_muster`.`year` = `pay_salary_unit_rate`.`year` INNER JOIN `pay_billing_master` ON `pay_billing_master`.`comp_code` = `pay_salary_unit_rate`.`comp_code` AND `pay_billing_master`.`billing_client_code` = `pay_salary_unit_rate`.`client_code` AND `pay_billing_master`.`billing_unit_code` = `pay_salary_unit_rate`.`unit_code` AND `pay_employee_master`.`grade_code` = `pay_billing_master`.`designation` AND `pay_billing_master`.`designation` = `pay_salary_unit_rate`.`designation` AND `pay_billing_master`.`hours` = `pay_salary_unit_rate`.`hours` INNER JOIN `pay_company_master` ON `pay_employee_master`.`comp_code` = `pay_company_master`.`comp_code` WHERE `pay_company_master`.`comp_code` = (SELECT `comp_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `pay_attendance_muster`.`unit_code` = (SELECT `unit_code` FROM `pay_employee_master` WHERE `emp_code` = '" + EMP_CODE + "') AND `pay_attendance_muster`.`month` = '" + thisMonth + "' AND `pay_attendance_muster`.`year` = '" + thisYear + "' AND `pay_attendance_muster`.`emp_code` = '" + EMP_CODE + "'	 AND `pay_attendance_muster`.`tot_days_present` > 0) AS payslip ";

                        //query = "SELECT  comp_code ,  COMPANY_NAME ,  ADDRESS1 ,  ADDRESS2 ,  CITY ,  STATE ,  UnitState ,  Unit_City ,  Client ,  grade ,  Unitcode ,  ihms_code, Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, UAN_No,PF_No, PAN_No, ESI_No, PerDayRate, Basic, Vda, emp_basic_vda AS 'basic_vda', hra_amount AS 'hra', sal_bonus_gross AS 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross AS 'leaveDays', washing_salary AS 'washing', travelling_salary AS 'travelling', education_salary AS 'education', allowances_salary AS 'special_allo', cca_salary AS 'cca', other_allow AS 'other_allo', gratuity_gross AS 'Gratuity_taxable', gratuity_after_gross AS 'Gratuity', (((emp_basic_vda) / 100) * sal_pf_percent) AS 'PF', (((emp_basic_vda + hra_amount + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary +IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot + (ot_applicable * ot_hours)) / 100) * sal_esic_percent) AS 'ESIC', sal_ot AS 'ot_amount_salary', IF(employee_type= 'Permanent',lwf_salary,0) AS 'lwf', sal_uniform_rate AS 'Uniform', IF(pt_applicable = 1, IFNULL(CASE WHEN F_PT = 'Y' THEN CASE WHEN (emp_basic_vda + hra_amount + sal_bonus_gross + leave_sal_gross + washing_salary + travelling_salary + education_salary +IF(esic_oa_salary = 1, allowances_salary, 0) + cca_salary + other_allow + gratuity_gross + sal_ot) < 10001 THEN 0 ELSE `PT` END ELSE `PT` END,0), 0) AS 'pt', fine, EMP_ADVANCE_PAYMENT AS 'advance', Total_Days_Present, Payment, Bank_holder_name,original_bank_account_no as 'BANK_EMP_AC_CODE', PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, Working_Days, Bonus_Policy, (ot_applicable) AS 'ot_rate', esic_ot_applicable AS 'ESIC_OT', ot_hours, IF(esic_common_allow = 0, common_allow, 0) AS 'EMP_specialallow',IF(esic_oa_salary = 0, allowances_salary, 0) AS 'PerDayRate' FROM (SELECT pay_company_master.comp_code, pay_company_master.COMPANY_NAME, pay_company_master.ADDRESS1, pay_company_master.ADDRESS2, pay_company_master.CITY, pay_company_master.STATE, pay_unit_master.state_name AS 'UnitState', unit_city AS 'Unit_City', (SELECT client_name FROM pay_client_master WHERE comp_code = '" + dr_hol.GetValue(2).ToString() + "' AND client_code = pay_unit_master.client_code) AS 'Client', (SELECT grade_desc FROM pay_grade_master WHERE comp_code = '" + dr_hol.GetValue(2).ToString() + "' AND grade_code = pay_salary_unit_rate.designation) AS 'grade', pay_unit_master.unit_code AS 'Unitcode', pay_employee_master.ihmscode AS 'ihms_code', pay_employee_master.emp_name AS 'Emp_Name', pay_employee_master.emp_code AS 'Emp_Code', pay_employee_master.EMP_FATHER_NAME AS 'Emp_Father', pay_employee_master.EMP_CURRENT_CITY AS 'Emp_City', pay_employee_master.JOINING_DATE AS 'Joining_Date', pay_employee_master.PAN_NUMBER AS 'UAN_No', pay_employee_master.PF_NUMBER AS 'PF_No', pay_employee_master.EMP_NEW_PAN_NO AS 'PAN_No', pay_employee_master.ESIC_NUMBER AS 'ESI_No', pay_billing_master_history.per_rate_salary AS 'PerDayRate', pay_billing_master_history.basic_salary as 'Basic' , pay_billing_master_history.vda_salary as 'Vda', (((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'emp_basic_vda', ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'hra_amount', CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_gross', CASE WHEN bonus_taxable_salary = '0' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'sal_bonus_after_gross', CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_gross', CASE WHEN leave_taxable_salary = '0' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END AS 'leave_sal_after_gross', ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'washing_salary', ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'travelling_salary', ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'education_salary', ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'allowances_salary', CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE ((pay_employee_master.cca/ pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'cca_salary', CASE WHEN pay_employee_master.special_allow = 0 THEN ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.special_allow END AS 'other_allow', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_gross', CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '0' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END AS 'gratuity_after_gross', pay_billing_master_history.sal_esic AS 'sal_esic_percent', pay_billing_master_history.sal_pf AS 'sal_pf_percent', ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) AS 'sal_ot', pay_salary_unit_rate.lwf_salary, pay_salary_unit_rate.sal_uniform_rate, CASE WHEN pay_employee_master.Gender = 'F' THEN CASE WHEN pay_unit_master.state_name = 'Maharashtra' THEN 'Y' ELSE 'N' END ELSE 'N' END AS 'F_PT', IFNULL((SELECT MIN(SLAB_AMOUNT) FROM pay_pt_slab_master WHERE STATE_NAME = pay_unit_master.state_name AND (((((pay_billing_master_history.basic_salary + pay_billing_master_history.vda_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.hra_amount_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN bonus_taxable_salary = '1' THEN ((pay_salary_unit_rate.sal_bonus / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + CASE WHEN leave_taxable_salary = '1' THEN ((pay_salary_unit_rate.leave_days / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END + ((pay_salary_unit_rate.washing_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.travelling_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.education_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + ((pay_salary_unit_rate.allowances_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.cca = 0 THEN ((pay_salary_unit_rate.cca_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE pay_employee_master.cca END + ((pay_salary_unit_rate.other_allow / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) + CASE WHEN pay_employee_master.gratuity = 0 THEN CASE WHEN gratuity_taxable_salary = '1' THEN ((pay_salary_unit_rate.gratuity_salary / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) ELSE 0 END ELSE pay_employee_master.gratuity END + ((pay_salary_unit_rate.ot_amount / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present)) BETWEEN FROM_AMOUNT AND TO_AMOUNT) AND (STR_TO_DATE('01/" + thisMonth + "/" + thisYear + "', '%d/%m/%Y') BETWEEN STR_TO_DATE(CONCAT('01/" + thisMonth + "/" + thisYear + "'), '%d/%m/%Y') AND STR_TO_DATE(CONCAT('01/01/2019'), '%d/%m/%Y'))), 0) AS 'PT', pay_employee_master.fine, pay_employee_master.EMP_ADVANCE_PAYMENT, CASE WHEN pay_attendance_muster.tot_days_present IS NULL THEN 0 ELSE pay_attendance_muster.tot_days_present END AS 'Total_Days_Present', CASE WHEN FLOOR(((pay_salary_unit_rate.total_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) IS NULL THEN 0 ELSE FLOOR(((pay_salary_unit_rate.total_salary) / pay_salary_unit_rate.month_days) * pay_attendance_muster.tot_days_present) END AS 'Payment', pay_employee_master.Bank_holder_name, pay_employee_master.BANK_EMP_AC_CODE, pay_employee_master.PF_IFSC_CODE, pay_employee_master.PF_BANK_NAME, pay_employee_master.BANK_BRANCH, pay_salary_unit_rate.month_days AS 'Working_Days', pay_billing_master_history.bonus_policy_salary AS 'Bonus_Policy', pay_billing_master_history.bonus_taxable_salary AS 'Bonus_taxable', pay_billing_master_history.leave_taxable_salary AS 'leave_taxable', pay_billing_master_history.gratuity_taxable_salary AS 'Gratuity_taxable', pay_billing_master_history.gratuity_salary AS 'Gratuity_BM', pay_salary_unit_rate.gratuity_salary AS 'Gratuity', pay_salary_unit_rate.leave_days AS 'leaveDays', pay_billing_master_history.leave_days_percent AS 'Leave_Percent',pay_salary_unit_rate.ot_applicable,pay_salary_unit_rate.esic_ot_applicable,pay_attendance_muster.ot_hours, pay_billing_master_history . esic_oa_salary ,  pay_billing_master_history . esic_common_allow , CASE WHEN  pay_employee_master . special_allow  = 0 THEN (( pay_salary_unit_rate . common_allowance  /  pay_salary_unit_rate . month_days ) *  pay_attendance_muster . tot_days_present ) ELSE  pay_employee_master . special_allow  END AS 'common_allow',  pay_billing_master_history . pt_applicable , pay_employee_master.employee_type,pay_employee_master.original_bank_account_no FROM  pay_employee_master  INNER JOIN  pay_attendance_muster  ON  pay_attendance_muster . emp_code  =  pay_employee_master . emp_code  AND  pay_attendance_muster . comp_code  =  pay_employee_master . comp_code  INNER JOIN  pay_unit_master  ON  pay_attendance_muster . unit_code  =  pay_unit_master . unit_code  AND  pay_attendance_muster . comp_code  =  pay_unit_master . comp_code  INNER JOIN  pay_salary_unit_rate  ON  pay_unit_master . comp_code  =  pay_salary_unit_rate . comp_code  AND  pay_attendance_muster . unit_code  =  pay_salary_unit_rate . unit_code  AND  pay_attendance_muster . month  =  pay_salary_unit_rate . month  AND  pay_attendance_muster . year  =  pay_salary_unit_rate . year  INNER JOIN  pay_billing_master_history  ON  pay_billing_master_history . comp_code  =  pay_salary_unit_rate . comp_code  AND  pay_billing_master_history . billing_client_code  =  pay_salary_unit_rate . client_code  AND  pay_billing_master_history . billing_unit_code  =  pay_salary_unit_rate . unit_code  AND  pay_billing_master_history . month  =  pay_salary_unit_rate . month  AND  pay_billing_master_history . year  =  pay_salary_unit_rate . year  AND  pay_employee_master . grade_code  =  pay_billing_master_history . designation  AND  pay_billing_master_history . designation  =  pay_salary_unit_rate . designation  AND  pay_billing_master_history . hours  =  pay_salary_unit_rate . hours  INNER JOIN  pay_company_master  ON  pay_employee_master . comp_code  =  pay_company_master . comp_code  WHERE  pay_attendance_muster.month = '" + thisMonth + "' AND pay_attendance_muster.year = '" + thisYear + "' AND  pay_unit_master.client_code = '" + dr_hol.GetValue(3).ToString() + "'  and pay_unit_master.unit_code = '" + dr_hol.GetValue(0).ToString() + "' AND pay_unit_master.comp_code = '" + dr_hol.GetValue(2).ToString() + "' AND pay_attendance_muster.emp_code = '" + EMP_CODE + "' and pay_attendance_muster.tot_days_present > 0) AS payslip";

                        // add branch_email not in (0,1,2) salary slip not display
                        //query = "SELECT emp_code, `pay_pro_master`.`comp_code`, `COMPANY_NAME`, `COMP_ADDRESS1` AS 'ADDRESS1', `COMP_ADDRESS2` AS 'ADDRESS2', `COMP_CITY` AS 'CITY', `COMP_STATE` AS 'STATE', `state_name` AS 'UnitState', `unit_city` AS 'Unit_City', `client` AS 'Client', `grade`, `unit_code` AS 'Unitcode', `ihms` AS 'ihms_code', `Emp_Name`, `Emp_Code`, `Emp_Father`, `Emp_City`, `Joining_Date`, `PAN_No` AS 'UAN_No', `PF_No`, `EMP_NEW_PAN_No` AS 'PAN_No', `ESI_No`, `PerDayRate`, `Basic`, `Vda`, `emp_basic_vda` AS 'basic_vda', `hra_amount_salary` AS 'hra', `sal_bonus_gross` AS 'Bonus_taxable', `sal_bonus_after_gross` 'bonus', `leave_sal_gross` 'leave_taxable', `leave_sal_after_gross` AS 'leaveDays', `washing_salary` AS 'washing', `travelling_salary` AS 'travelling', `education_salary` AS 'education', `allowances_salary` AS 'special_allo', `cca_salary` AS 'cca', `other_allow` AS 'other_allo', `gratuity_gross` AS 'Gratuity_taxable', `gratuity_after_gross` AS 'Gratuity', `sal_pf` AS 'PF', `sal_esic` AS 'ESIC', `sal_ot` AS 'ot_amount_salary', `lwf_salary` AS 'lwf', `sal_uniform_rate` AS 'Uniform', `PT_AMOUNT` AS 'pt', `fine`, `advance_payment_mode` AS 'advance', `Total_Days_Present`, `Payment`, `Bank_holder_name`, `BANK_EMP_AC_CODE`, `PF_IFSC_CODE`, `PF_BANK_NAME`, `BANK_BRANCH`, `Total_Days_Present` AS 'Working_Days', `Bonus_Policy`, `ot_rate`, `esic_ot_applicable` AS 'ESIC_OT', `ot_hours`, `common_allow` AS 'EMP_specialallow' FROM `pay_pro_master` WHERE `pay_pro_master`.`month` = '" + thisMonth + "' AND `pay_pro_master`.`year` = '" + thisYear + "' and `pay_pro_master`.`emp_code`='" + EMP_CODE + "' AND `pay_pro_master`.`comp_code` = '" + dr_hol.GetValue(2).ToString() + "' and (`pay_pro_master`.payment_approve='2' OR `pay_pro_master`.payment_approve='1') AND `pay_pro_master`.`Total_Days_Present` > 0  AND (`PAN_No`!= '' and PAN_No is not null ) AND (`PF_No`  != '' and PF_No is not null) AND (`ESI_No` != '' and ESI_No is not null) and branch_email not in (0,1,2)";

                        // add payment_status= '1' then dwnload salary
                        query = "SELECT pay_pro_master.comp_code, COMPANY_NAME, COMP_ADDRESS1 as 'ADDRESS1', COMP_ADDRESS2 As 'ADDRESS2', COMP_CITY AS  'CITY', COMP_STATE as 'STATE', state_name as 'UnitState', unit_city as 'Unit_City', client as 'Client', grade, unit_code as 'Unitcode', ihms as 'ihms_code', Emp_Name, Emp_Code, Emp_Father, Emp_City, Joining_Date, if(PAN_No is null or PAN_No='','IN PROCESS',PAN_No) AS 'UAN_No', if(PF_No is null or PF_No='','IN PROCESS',PF_No) AS PF_No,date_format(salary_date,'%d/%m/%Y') as  'PAN_No', if(ESI_No is null or ESI_No='','IN PROCESS',ESI_No) AS ESI_No, PerDayRate, Basic, Vda, emp_basic_vda AS 'basic_vda', hra_amount_salary AS 'hra', sal_bonus_gross AS 'Bonus_taxable', sal_bonus_after_gross 'bonus', leave_sal_gross 'leave_taxable', leave_sal_after_gross AS 'leaveDays', washing_salary AS 'washing', travelling_salary AS 'travelling', education_salary AS 'education', allowances_salary AS 'special_allo', cca_salary AS 'cca', other_allow AS 'other_allo', gratuity_gross AS 'Gratuity_taxable', gratuity_after_gross AS 'Gratuity', sal_pf AS 'PF', sal_esic AS 'ESIC', sal_ot AS 'ot_amount_salary', lwf_salary AS 'lwf', sal_uniform_rate AS 'Uniform', PT_AMOUNT AS 'pt', fine, advance_payment_mode AS 'advance', Total_Days_Present, Payment, Bank_holder_name, BANK_EMP_AC_CODE, PF_IFSC_CODE, PF_BANK_NAME, BANK_BRANCH, month_days as 'Working_Days', Bonus_Policy, ot_rate , esic_ot_applicable AS 'ESIC_OT', ot_hours, common_allow AS 'EMP_specialallow' FROM `pay_pro_master` WHERE `pay_pro_master`.`month` = '" + thisMonth + "' AND `pay_pro_master`.`year` = '" + thisYear + "' and `pay_pro_master`.`emp_code`='" + EMP_CODE + "' AND `pay_pro_master`.`comp_code` = '" + dr_hol.GetValue(2).ToString() + "'  AND payment_status= '1' and employee_type IN ('Temporary', 'Permanent') ";


                        log.Error("salary query ...." + query);
                        DataTable dt = new DataTable();
                        MySqlCommand cmd = new MySqlCommand(query);
                        MySqlDataReader sda = null;
                        cmd.Connection = d.con;
                        d.con.Open();
                        sda = cmd.ExecuteReader();
                        dt.Load(sda);
                        d.con.Close();
                        crystalReport.Refresh();
                        crystalReport.SetDataSource(dt);
                        ExportOptions CrExportOptions;
                        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                        string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Logs\\");
                        path = path.Replace("\\", "\\\\");
                        CrDiskFileDestinationOptions.DiskFileName = path + EMP_CODE + "_" + thisMonth + "_" + thisYear + ".pdf";
                        CrExportOptions = crystalReport.ExportOptions;
                        {
                            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        }
                        crystalReport.Export();
                        CrDiskFileDestinationOptions = null;
                        CrExportOptions = null;
                        CrFormatTypeOptions = null;
                        crystalReport.Dispose();

                        auth = true;
                        log.Error("Android - Inside GetSalaryData....second call" + auth);
                        //return auth;

                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                        log.Error("Line Number : " + new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber());

                        auth = false;
                        log.Error("Android - Inside GetSalaryData....Three call" + auth);
                        //return false;
                    }
                    finally
                    {
                        d.con.Close();
                        log.Error("Android - finally GetSalaryData....");
                    }


                    //auth = true;
                    // return auth;
                }

            }
            dr_hol.Dispose();
            cmd_hol.Dispose();
            d1.con1.Close();
            log.Error("Android - Inside GetSalaryData....final  call" + auth);
            return auth;
        }
        catch { }
        finally { d1.con1.Close(); }

        return auth;

    }


    // -------- Get Unit Lattitude,Longtitude And Distance ---------//
    public DataTable Getunit_Lattitude_Longtitude_Distance(string EMP_CODE)
    {
        DataTable userDetailsTable = new DataTable();
       
        userDetailsTable.Columns.Add(new DataColumn("UNIT_LATTITUDE", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("UNIT_LONGTITUDE", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("UNIT_DISTANCE", typeof(String)));

        try
        {

            log.Error("Android - Inside Getunit_Lattitude_Longtitude_Distance....");

            string query = "select unit_Lattitude as 'UNIT_LATTITUDE',unit_Longtitude as 'UNIT_LONGTITUDE',unit_distance as 'UNIT_DISTANCE' from pay_unit_master inner join pay_employee_master on pay_unit_master.comp_code=pay_employee_master.comp_code and pay_unit_master.unit_code=pay_employee_master.unit_code where pay_employee_master.emp_code='" + EMP_CODE + "'";

            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["UNIT_LATTITUDE"].ToString(), reader["UNIT_LONGTITUDE"].ToString(),
                        reader["UNIT_DISTANCE"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Getunit_Lattitude_Longtitude_Distance....");
        }
        return userDetailsTable;
    }


    //Employee Attendance send Android Mobile old android apk version 14
    public void Employee_Attendance_Apply(string unit_unitlatitude, string unit_unitlongtitude, string unit_distance, string unit_empaddress,
        string unit_emplatitude, string unit_emplongtitude, string emp_code, string mobile_imei, string attendances_click, string Upload_Image_base64)
    {
        int res;
        String query1 = "";
        try
        {

            log.Error("Android - Inside Employee Attendances Apply");
            if (attendances_click == "Attendances_intime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_Intime_" + currentdate2 + ".png";

                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                //time zone change 
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                //query select  only one date
                query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images,indicate_offline_online_intime) select comp_code,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name ,now(),now(),'" + mobile_imei + "','" + image_name + "'.'Online' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";

                string verificationname = "Attendances_intime";
                log.Error("Android - Finally Employee INlocation intimequery ...." + query1);
                Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);


            }
            else if (attendances_click == "Attendances_outtime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_Outtime_" + currentdate2 + ".png";
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_outtime,mobile_imei_no,Attendances_outtime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
                query1 = "UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + image_name + "',indicate_offline_online_outtime='Online' WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                string verificationname = "Attendances_outtime";
                log.Error("Android - Finally Employee INlocation outtimequery ...." + query1);
                Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);

            }
            else if (attendances_click == "Attendances_cameraintime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_cameraIntime_" + currentdate2 + ".png";
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
                // time zone changes
                // query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images,indicate_offline_online_intime) select comp_code ,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name,now(),now(),'" + mobile_imei + "','" + image_name + "','Online' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";


                string verificationname = "Attendances_cameraintime";
                log.Error("Android - Finally Employee outlocation cameraintimequery ...." + query1);
                Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                // log.Error("Android - Inside Employee Attendances Apply" + Upload_Image_base64 + "/////" + verificationname);
            }
            else if (attendances_click == "Attendances_cameraouttime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_camerouttime_" + currentdate2 + ".png";
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_outtime,mobile_imei_no,Camera_outtime) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
                // query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                //tome zone changes
                query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "',indicate_offline_online_outtime='Online'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";

                log.Error("Android - Finally Employee outlocation cameraouttimequery ...." + query1);
                string verificationname = attendances_click;
                Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
            }

            MySqlCommand command = new MySqlCommand(query1, d.con);
            d.con.Open();
            command.ExecuteNonQuery();

            d.con.Close();

            String split_length = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            int date = split_length.ToString().IndexOf('/');
            int day = int.Parse(split_length.ToString().Substring(0, 2));
            int months = int.Parse(split_length.ToString().Substring(3, 2));
            int years = int.Parse(split_length.ToString().Substring(6, 4));
            log.Error("Attendances Date format display split_length:" + split_length + " day:" + day + " months:" + months + " years:" + years);
            log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
            log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 3) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

            // vinod sir said comment that query
            // d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

            string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + emp_code + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + emp_code + "')");

            if (tempflag == "yes")
            {
                // P Falg replays A
                //d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
                log.Error("Android inset =:- " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");
                // d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

                d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

                string temp1 = d.getsinglestring("select trim(attendance_id) from pay_employee_master where emp_code = '" + emp_code + "'");
                log.Error("Android - temp1" + temp1);
                if (attendances_click == "Attendances_outtime")
                {
                    if (temp1 == "8")
                    {
                        //string query = "Android - 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                        //log.Error("Android - 8 hour:" + query);
                        log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        //log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                    }
                    else if (temp1 == "12")
                    {
                        log.Error("Android -Attendances_outtime 12 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                    }
                    else
                    {
                        log.Error("Android -Attendances_outtime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                    }
                }
                else if (attendances_click == "Attendances_cameraouttime")
                {
                    if (temp1 == "8")
                    {
                        //string query12 = "Android - camera 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                        //log.Error("Android - camera 8 hour:" + query12);
                        log.Error("Android -Attendances_cameraouttime 8 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                    }
                    else if (temp1 == "12")
                    {
                        log.Error("Android -Attendances_cameraouttime 12 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                    }
                    else
                    {
                        log.Error("Android -Attendances_cameraouttime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");


                    }

                }

            }

        }

        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee Attendances Apply....");
        }

    }


    //Employee Attendance send Android Mobile new version apk version 15
    public void Employee_Attendance_Apply_New(string unit_unitlatitude, string unit_unitlongtitude, string unit_distance, string unit_empaddress,
        string unit_emplatitude, string unit_emplongtitude, string emp_code, string mobile_imei, string attendances_click, string Upload_Image_base64, 
        string indicate_offline_online,string offline_date )
    {
        int res;
        String query1="";
        try
        {
           
            log.Error("Android - Inside Employee Attendances Apply");
            if (attendances_click == "Attendances_intime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
               string image_name = emp_code + "_Intime_" + currentdate2 + ".png";

                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                //time zone change 
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                if (indicate_offline_online == "Online")
                {
                    //query select  only one date
                    query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images,indicate_offline_online_intime) select comp_code,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name ,now(),now(),'" + mobile_imei + "','" + image_name + "','" + indicate_offline_online + "' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";
                    string verificationname = "Attendances_intime";
                    log.Error("Android - Finally Employee INlocation intimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                }
                else {
                    query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_intime,mobile_imei_no,Attendances_intime_images,indicate_offline_online_intime) select comp_code,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name ,now(),now(),'" + mobile_imei + "','" + image_name + "','" + indicate_offline_online + "' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";
                    string verificationname = "Attendances_intime";
                    log.Error("Android - Finally Employee INlocation intimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                }


            }
            else if (attendances_click == "Attendances_outtime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_Outtime_" + currentdate2 + ".png";

                string offline_currentdate2="", offline_image_name="", offlineDay="", offlineMonths="", offlineYears="";

                if (offline_date != "" && indicate_offline_online == "Offline")
                {
                    offline_currentdate2 = offline_date.ToString().Replace("/", "_");
                    offline_image_name = emp_code + "_Outtime_" + offline_currentdate2 + ".png";

                     offlineDay = offline_date.ToString().Substring(0, 2);
                     offlineMonths = offline_date.ToString().Substring(3, 2);
                     offlineYears = offline_date.ToString().Substring(6, 4);
                }
               
                if (indicate_offline_online == "Online")
                {
                    //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Attendances_outtime,mobile_imei_no,Attendances_outtime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
                    query1 = "UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "' WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                    string verificationname = "Attendances_outtime";
                    log.Error("Android - Finally Employee INlocation outtimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);

                }
                else if (indicate_offline_online == "Offline")
                {
                    query1 = "UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + offline_image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "' WHERE DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offlineYears + "-" + offlineMonths + "-" + offlineDay + "','%Y-%m-%d') and emp_code='" + emp_code + "'";
                    string verificationname = "Attendances_outtime";
                    log.Error("Android - Finally Employee INlocation outtimequery offline query ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, offline_currentdate2);

                }
                else {
                    query1 = "UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "' WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                    string verificationname = "Attendances_outtime";
                    log.Error("Android - Finally Employee INlocation outtimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                }


            }
            else if (attendances_click == "Attendances_cameraintime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_cameraIntime_" + currentdate2 + ".png";
                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
                // time zone changes
               // query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";

                if (indicate_offline_online == "Online")
                {
                    query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images,indicate_offline_online_intime) select comp_code ,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name,now(),now(),'" + mobile_imei + "','" + image_name + "','" + indicate_offline_online + "' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";

                    string verificationname = "Attendances_cameraintime";
                    log.Error("Android - Finally Employee outlocation cameraintimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                    // log.Error("Android - Inside Employee Attendances Apply" + Upload_Image_base64 + "/////" + verificationname);
                
                }
                else {

                    query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_intime,mobile_imei_no,Camera_intime_images,indicate_offline_online_intime) select comp_code ,unit_code ,'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "', emp_name,now(),now(),'" + mobile_imei + "','" + image_name + "','" + indicate_offline_online + "' from pay_employee_master where `emp_code` = '" + emp_code + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + emp_code + "' and date(Date_Time)=curdate()) limit 1";

                    string verificationname = "Attendances_cameraintime";
                    log.Error("Android - Finally Employee outlocation cameraintimequery ...." + query1);
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                    
                }
            }
            else if (attendances_click == "Attendances_cameraouttime")
            {
                string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                string image_name = emp_code + "_camerouttime_" + currentdate2 + ".png";

                string offline_currentdate2 = "", offline_image_name = "", offlineDay = "", offlineMonths = "", offlineYears = "";

                if (offline_date != "" && indicate_offline_online == "Offline")
                {
                    offline_currentdate2 = offline_date.ToString().Replace("/", "_");
                    offline_image_name = emp_code + "_camerouttime_" + offline_currentdate2 + ".png";

                    offlineDay = offline_date.ToString().Substring(0, 2);
                    offlineMonths = offline_date.ToString().Substring(3, 2);
                    offlineYears = offline_date.ToString().Substring(6, 4);
                }
               

                //query1 = "insert into pay_android_attendance_logs (comp_code,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Date_Time,Camera_outtime,mobile_imei_no,Camera_outtime) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + unit_unitlatitude + "','" + unit_unitlongtitude + "','" + unit_emplatitude + "','" + unit_emplongtitude + "','" + unit_distance + "','" + unit_empaddress + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),now(),now(),'" + mobile_imei + "','" + image_name + "')";
               // query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                //tome zone changes

                if(indicate_offline_online == "Online")
                {
                    query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                    log.Error("Android - Finally Employee outlocation cameraouttimequery ...." + query1);
                    string verificationname = attendances_click;
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                }
                else if (indicate_offline_online == "Offline")
                {
                    query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + offline_image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "'  WHERE DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offlineYears + "-" + offlineMonths + "-" + offlineDay + "','%Y-%m-%d') and emp_code='" + emp_code + "'";
                    log.Error("Android - Finally Employee outlocation cameraouttimequery offline query ...." + query1);
                    string verificationname = attendances_click;
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, offline_currentdate2);
                }
                else {
                    query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "',indicate_offline_online_outtime='" + indicate_offline_online + "'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                    log.Error("Android - Finally Employee outlocation cameraouttimequery ...." + query1);
                    string verificationname = attendances_click;
                    Base64ToImage(Upload_Image_base64, emp_code, verificationname, currentdate2);
                
                }
                
                //query1 = "UPDATE pay_android_attendance_logs SET Camera_outtime=now(), Camera_outtime_images='" + image_name + "',indicate_offline_online='" + indicate_offline_online + "'  WHERE id = (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)";
                
            }

            MySqlCommand command = new MySqlCommand(query1, d.con);
            d.con.Open();
            command.ExecuteNonQuery();

            d.con.Close();

            String split_length = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            int date = split_length.ToString().IndexOf('/');
            int  day = int.Parse(split_length.ToString().Substring(0, 2));
            int months = int.Parse(split_length.ToString().Substring(3, 2));
            int  years = int.Parse(split_length.ToString().Substring(6, 4));

            int offDate = 0, offDay = 0, offMonths = 0, offYears = 0;
            string offlineDate = "";
            if (offline_date != "" && indicate_offline_online == "Offline")
            {
                // offline date get

                offlineDate=offline_date.ToString().Substring(0, 10);
                offDate = offline_date.ToString().IndexOf("/");
                offDay = int.Parse(offline_date.ToString().Substring(0, 2));
                offMonths = int.Parse(offline_date.ToString().Substring(3, 2));
                offYears = int.Parse(offline_date.ToString().Substring(6, 4));
            }

            log.Error("Attendances Date format display split_length:" + split_length + " day:" + day + " months:" + months + " years:" + years);
            log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
            log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 3) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
            
            // vinod sir said comment that query
           // d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

            string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + emp_code + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + emp_code + "')");

            if (tempflag == "yes")
            {
                // P Falg replays A
                //d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
                log.Error("Android inset =:- " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");
               // d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

                d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

                string temp1 = d.getsinglestring("select trim(attendance_id) from pay_employee_master where emp_code = '" + emp_code + "'");
                log.Error("Android - temp1"+temp1);

                // indicate_offline_online loop start
                if (indicate_offline_online == "Online")
                {
                    // Attendances_outtime loop start
                    if (attendances_click == "Attendances_outtime")
                    {
                        if (temp1 == "8")
                        {
                            //string query = "Android - 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - 8 hour:" + query);
                            log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            //log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_outtime 12 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_outtime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                    }
                    // Attendances_outtime loop end

                    // Attendances_cameraouttime loop start
                    else if (attendances_click == "Attendances_cameraouttime")
                    {
                        if (temp1 == "8")
                        {
                            //string query12 = "Android - camera 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - camera 8 hour:" + query12);
                            log.Error("Android -Attendances_cameraouttime 8 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_cameraouttime 12 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_cameraouttime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }

                    }
                    // Attendances_cameraouttime loop close
                }
               // indicate_offline_online "Online" loop end

               // indicate_offline_online "Offline" loop start
                else if (indicate_offline_online == "Offline")
                {
                    // Attendances_outtime "Offline" loop start
                    if (attendances_click == "Attendances_outtime")
                    {
                        if (temp1 == "8")
                        {
                            //string query = "Android - 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - 8 hour:" + query);
                            log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            //log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_outtime 12 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_outtime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                    }
                    // Attendances_outtime "Offline" loop end

                    // Attendances_cameraouttime "Offline" loop start
                    else if (attendances_click == "Attendances_cameraouttime")
                    {
                        if (temp1 == "8")
                        {
                            //string query12 = "Android - camera 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - camera 8 hour:" + query12);
                            log.Error("Android -Attendances_cameraouttime 8 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_cameraouttime 12 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_cameraouttime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + offlineDate + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + offYears + "-" + offMonths + "-" + offDay + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(offDay.ToString()) + " = t.attend where month= '" + offMonths + "' AND YEAR='" + offYears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }

                    }
                    // Attendances_cameraouttime "Offline" loop close

                }
                // indicate_offline_online "Offline" loop end
                    // loop use offline and online not use employee start
                else {
                    // Attendances_outtime loop start
                    if (attendances_click == "Attendances_outtime")
                    {
                        if (temp1 == "8")
                        {
                            //string query = "Android - 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - 8 hour:" + query);
                            log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            //log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_outtime 12 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_outtime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                    }
                    // Attendances_outtime loop end

                    // Attendances_cameraouttime loop start
                    else if (attendances_click == "Attendances_cameraouttime")
                    {
                        if (temp1 == "8")
                        {
                            //string query12 = "Android - camera 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                            //log.Error("Android - camera 8 hour:" + query12);
                            log.Error("Android -Attendances_cameraouttime 8 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else if (temp1 == "12")
                        {
                            log.Error("Android -Attendances_cameraouttime 12 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }
                        else
                        {
                            log.Error("Android -Attendances_cameraouttime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                            d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                        }

                    }
                    // Attendances_cameraouttime loop close
                
                }
                // loop use offline and online not use employee end

               
        }

        }

        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee Attendances Apply....");
        }

    }

    //----- Get Police verification report ---------
    public bool GetPoliceVerification(string EMP_CODE)
    {
        try
        {
            DAL d = new DAL();
            ReportDocument crystalReport = new ReportDocument();
            log.Error("Android - Inside Get Police verification report....");
            // crystalReport.Load("E:\\PayRoll\\CeltPayroll\\MonthlySalary_PaySlip_all_GTS.rpt");
            crystalReport.Load(HttpContext.Current.Server.MapPath("~/rpt_police_verify_form.rpt"));
            
            crystalReport.Refresh();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Logs\\");
            path = path.Replace("\\", "\\\\");
            CrDiskFileDestinationOptions.DiskFileName = path + EMP_CODE + "_Police_verification" + ".pdf";
            // log.Error("Android - Inside Get Police verification report...." + CrDiskFileDestinationOptions.DiskFileName);
            CrExportOptions = crystalReport.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                // log.Error("Android - Inside Get Police verification report123...." + CrExportOptions.FormatOptions);

            }
            crystalReport.Export();
            CrDiskFileDestinationOptions = null;
            CrExportOptions = null;
            CrFormatTypeOptions = null;
            crystalReport.Dispose();
            return true;

        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber());
            return false;
        }
        finally
        {
            d.con.Close();
            log.Error("Android - finally Get Police verification ....");
        }
    }


    //----- Get Unitform Form report ---------
    public bool GetUniform_form(string EMP_CODE)
    {
        try
        {
            DAL d = new DAL();
            ReportDocument crystalReport = new ReportDocument();
            string query = "";
            log.Error("Android - Inside Get Uniform  report....");
            // crystalReport.Load("E:\\PayRoll\\CeltPayroll\\MonthlySalary_PaySlip_all_GTS.rpt");
            crystalReport.Load(HttpContext.Current.Server.MapPath("~/uniform.rpt"));
          
            crystalReport.Refresh();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/."), "Logs\\");
            log.Error("Android - Inside Get Uniform  report report...." + CrDiskFileDestinationOptions.DiskFileName);
            path = path.Replace("\\", "\\\\");
            CrDiskFileDestinationOptions.DiskFileName = path + EMP_CODE + "_Employee_Uniform" + ".pdf";

            CrExportOptions = crystalReport.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                log.Error("Android - Inside Get Uniform  report report...." + CrDiskFileDestinationOptions.DiskFileName);
            }
            crystalReport.Export();
            CrDiskFileDestinationOptions = null;
            CrExportOptions = null;
            CrFormatTypeOptions = null;
            crystalReport.Dispose();
            return true;

        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber());
            return false;
        }
        finally
        {
            d.con.Close();
            log.Error("Android - finally Get Uniform ....");
        }
    }

    //police verification form image send 
    public void Police_verification_image_upload(string Upload_Image_base64, string EMP_CODE)
    {

        try
        {
            log.Error("Android - Inside Police verification form image upload");
            String verificationname = "police_verification_form";
            string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);

            string emp_code_id = EMP_CODE + "_22.png";

           // string update = "UPDATE pay_images_master SET POLICE_VERIFICATION_DOC='" + emp_code_id + "' , MODIF_DATE=now() WHERE  comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')  AND EMP_CODE='" + EMP_CODE + "'";
            String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + verificationname + "','" + emp_code_id + "',now())";
            MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
            d.conopen();
            cmd_update.ExecuteNonQuery();
            d.conclose();
             log.Error("Android - query error");
          
        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Police verification form upload ....");
        }

    }

    //Employee Adhar Card image send 
    public void Employee_adhar_card_image_send(string Upload_Image_base64, string EMP_CODE, string adhar_no)
    {

        try
        {
            log.Error("Android - Inside Employee Adhar Card image send");
            String verificationname = "adhar_card_upload";
            string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);

            string emp_code_id = EMP_CODE + "_21.png";

           // string update = "UPDATE pay_images_master SET EMP_BANK_STATEMENT='" + emp_code_id + "' , MODIF_DATE=now() WHERE  comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')  AND EMP_CODE='" + EMP_CODE + "'";
            String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + verificationname + "','" + emp_code_id + "',now())";
            MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
            d.conopen();
            cmd_update.ExecuteNonQuery();
            d.conclose();

           

        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            d.conclose();
            log.Error("Android - Finally Employee Adhar card upload ....");
        }

    }


    //Employee Adhar Card Scanning details  send 
    public void Employee_adhar_card_scanning_details(string EMP_CODE, String scan_uid, String scan_name, String scan_gender, String scan_villageTehsil, String scan_district, String scan_state, String scan_postCode, String scan_dobrith)
    {
        String verificationname = "adhar_card_scanning";
     

                try
                {
                    log.Error("Android - Inside Employee Adhar Card scanning details send");
                   
                    string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                    
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,cur_date,field1,field2,field3,field4,field5,field6,field7,field8) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + verificationname + "',now(),'" + scan_uid + "','" + scan_name + "','" + scan_gender + "','" + scan_villageTehsil + "','" + scan_district + "','" + scan_state + "','" + scan_postCode + "','" + scan_dobrith + "')";
                    log.Error("Android - Inside Employee Adhar Card scanning details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                 
                }
                catch (Exception error_leave_apply)
                {
                    log.Error(error_leave_apply.Message);
                    log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
                }
                finally
                {
                    d.con.Close();
                    d.conclose();
                    log.Error("Android - Finally Employee Adhar card scanning details ....");
                }
            }



    //Employee Pan Card image send 
    public void Employee_pan_card_image_send(string Upload_Image_base64, string EMP_CODE, string pan_no)
    {

        try
        {
            log.Error("Android - Inside Employee Pan Card image send");
            string verificationname = "Pan_card_upload";
            string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);

            string emp_code_id = EMP_CODE + "_2.png";

            //string update = "UPDATE pay_images_master SET EMP_ADHAR_PAN='" + emp_code_id + "' , MODIF_DATE=now() WHERE  comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')  AND EMP_CODE='" + EMP_CODE + "'";
            String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + verificationname + "','" + emp_code_id + "',now())";
            MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
            d.conopen();
            cmd_update.ExecuteNonQuery();
            d.conclose();


        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            d.conclose();
            log.Error("Android - Finally Employee Pan card upload ....");
        }

    }


    //Employee photo send
    
    public void Employee_photo_image_send(string Upload_Image_base64, string EMP_CODE)
    {

        try
        {
            log.Error("Android - Inside Employee Photo image send");
            string verificationname = "Employee_photo_upload";
            string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);

            string emp_code_id = EMP_CODE + "_25.png";

            //string update = "UPDATE pay_images_master SET EMP_ADHAR_PAN='" + emp_code_id + "' , MODIF_DATE=now() WHERE  comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')  AND EMP_CODE='" + EMP_CODE + "'";
            String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "',(select emp_name from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + verificationname + "','" + emp_code_id + "',now())";
            log.Error("Android - Inside Employee Photo image send" + insert);
            MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
            d.conopen();
            cmd_update.ExecuteNonQuery();
            d.conclose();
                        
        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            d.conclose();
            log.Error("Android - Finally Employee Photo upload ....");
        }

    }

    
    //Employee Working image send 
    public void Employee_woring_image_send(string Upload_Image_base64, string EMP_CODE)
    {

        try
        {
            log.Error("Android - Inside Employee working image send");
            String verificationname = "Working_image";
            String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            string currdate = DateTime.Now.ToString("dd-MM-yyyy");
           
            //string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/Working_Images/"));
            if (!Directory.Exists(working_imagepath))
            {
                Directory.CreateDirectory(working_imagepath);
            }
            string monthwise = DateTime.Now.ToString("MMM-yyyy");
            string monthwisepath = working_imagepath + monthwise + "\\";
            if (!Directory.Exists(monthwisepath))
            {
                Directory.CreateDirectory(monthwisepath);
            }

            string newpath = monthwisepath + currdate + "\\";

            if (!Directory.Exists(newpath))
            {
                Directory.CreateDirectory(newpath);
            }

            String working_image_name1 = System.IO.Path.Combine(newpath).Replace("\\", "\\\\")+ EMP_CODE + "_Working_" + currentdate2 + ".png";
            string working_image_name = working_image_name1;

            log.Error("Line Number : " + working_imagepath + " newpath:" + newpath + " working_image_name:" + working_image_name + " working_image_name1:" + working_image_name1);
          // string  query1 = "insert into pay_android_working_image (comp_code,unit_code,emp_code,image_name,current_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "','" + working_image_name + "',now())";
            d.operation("insert into pay_android_working_image (comp_code,unit_code,emp_code,image_name,datecurrent) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "','" + working_image_name.ToString() + "',now())");
           // Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, working_image_name);

        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee working upload ....");
        }

    }

    // Employee Geolocation latitude and longtitude 
    public void Employee_geolocation_lat_long(string emp_code,string emp_latitude,string emp_longtitude,string current_address) {
        log.Error("Android - Inside Employee Geolocation");
        //String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
        try
        {
            d.operation("insert into pay_geolocation_address (comp_code,unit_code,client_code,emp_code,cur_latitude,cur_longtitude,cur_address,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'),(select client_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + emp_latitude + "','" + emp_longtitude + "','" + current_address + "',now())");
           
        }
        catch (Exception error_geolocation)
        {
            log.Error(error_geolocation.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_geolocation, true).GetFrame(0).GetFileLineNumber());
       
        }
        finally {
            log.Error("Android - Finally Employee Geolocation ....");
        }
    }


    //---- Client List Send ---//
    public DataTable GetClientList(string userName,string employeerequirementflag)
    {
        string query="";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_code", typeof(String)));

        try
        {

            log.Info("Android - Inside GetClientList..");
            //string query = "select client_name, pay_designation_count.client_code as client_code, pay_designation_count.designation as designation from pay_designation_count inner join pay_client_master on pay_client_master.comp_code = pay_designation_count.comp_code and pay_client_master.client_code = pay_designation_count.client_code where pay_designation_count.comp_code = (select comp_code from pay_employee_master where emp_code='" + userName + "') and client_name != 'Staff' group by pay_designation_count.designation order by 1";
            //string query = "select DISTINCT client_name,client_code from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='"+userName+"') order by client_name";
            if (employeerequirementflag=="1")
            {
               query  = "select DISTINCT pay_client_master.client_name , pay_client_master.client_code from pay_op_management inner join pay_client_master on pay_client_master.comp_code=pay_op_management.comp_code and pay_client_master.client_code=pay_op_management.client_code where pay_op_management.emp_code='" + userName + "' order by pay_client_master.client_name";
            }else{
                query = "select DISTINCT client_name,client_code from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='"+userName+"') order by client_name";
            }
            
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["client_name"].ToString(), reader["client_code"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetClientList....");
        }
        return userDetailsTable;

    }


    //---- Client Unit List Send ---//
    public DataTable GetClientUnitList(string client_code,string userName)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_code", typeof(String)));

        try
        {

            log.Info("Android - Inside GetClientUnitList..");
            //string query = "Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name, unit_code from pay_unit_master where comp_code=(select comp_code from pay_client_master where client_code='"+client_code+ "') and client_code = '"+client_code+ "' ORDER BY UNIT_CODE";
            string query = "Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name, pay_unit_master.unit_code  from pay_op_management inner join pay_unit_master on pay_unit_master.comp_code=pay_op_management.comp_code and pay_unit_master.unit_code=pay_op_management.unit_code where pay_op_management.client_code='" + client_code + "' and pay_op_management.emp_code='" + userName + "' order by pay_unit_master.UNIT_CODE";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["unit_name"].ToString(), reader["unit_code"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetClientUnitList....");
        }
        return userDetailsTable;

    }

    //---- Employee requirement client wise Unit List Send ---//
    public DataTable GetEmployeeClientUnitList(string client_code)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_code", typeof(String)));

        try
        {

            log.Info("Android - Inside GetClientUnitList..");
            string query = "Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name, unit_code from pay_unit_master where comp_code=(select comp_code from pay_client_master where client_code='"+client_code+ "') and client_code = '"+client_code+ "' ORDER BY UNIT_CODE";
            //string query = "Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name, pay_unit_master.unit_code  from pay_op_management inner join pay_unit_master on pay_unit_master.comp_code=pay_op_management.comp_code and pay_unit_master.unit_code=pay_op_management.unit_code where pay_op_management.client_code='" + client_code + "' and pay_op_management.emp_code='" + userName + "' order by pay_unit_master.UNIT_CODE";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["unit_name"].ToString(), reader["unit_code"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetClientUnitList....");
        }
        return userDetailsTable;

    }

    //---- Client Unit Gradee Send ---//
    public DataTable GetClientUnitGradeList(string unit_code,string client_code )
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("designation", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("head_email_id", typeof(String)));

        try
        {

            log.Info("Android - Inside GetClientUnitGradeList..");
           // string query = "select designation from pay_designation_count where unit_code='" + unit_code + "' and client_code='" + client_code + "' order by designation";
           
            string email_id = d.getsinglestring("select  group_concat(pay_branch_mail_details.`head_email_id`) as head_email_id from pay_branch_mail_details where unit_code= '" + unit_code + "' and client_code = '" + client_code + "'");
           // string query = "SELECT `designation`,group_concat(pay_branch_mail_details.`head_email_id`)  as head_email_id FROM `pay_designation_count`  inner join pay_branch_mail_details on pay_designation_count.client_code=pay_branch_mail_details.client_code and  pay_designation_count.unit_code=pay_branch_mail_details.unit_code WHERE pay_designation_count.`unit_code` = '" + unit_code + "' AND pay_designation_count.`client_code` = '" + client_code + "'  ORDER BY `designation`";
            string query = "select  designation ,'" + email_id + "' as head_email_id from pay_designation_count where pay_designation_count.`unit_code` = '" + unit_code + "' AND pay_designation_count.`client_code` = '" + client_code + "'  ORDER BY `designation`";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["designation"].ToString(), reader["head_email_id"].ToString());
                }
            }

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetClientUnitGradeList....");
        }
        return userDetailsTable;

    }

    // site audit data insert
    public void site_audio_data(string emp_code, string client_code, string unit_code, string grade_name, string ques_1_ans, string ques_2_ans, string ques_3_ans, string ques_4_ans, string ques_5_ans, string ques_6_ans, string remarks, string ques_image_1, string ques_path_1, string ques_image_2, string ques_path_2, string ques_image_3, string ques_path_3, string ques_image_4, string ques_path_4, string ques_image_5, string ques_path_5, string current_address, string ques_image_6, string ques_path_6)
    {
        Image image = null;
        string question_path_1 = "", question_path_2 = "", question_path_3 = "", question_path_4 = "", question_path_5 = "", question_path_6 = "";
        log.Error("Android - Inside Employee site_audit_data");


        // create folder site_audit

        if (!Directory.Exists(site_auditpath)) {

            Directory.CreateDirectory(site_auditpath);
        }


        String maxid = d.getsinglestring("select  max(id)+1  from pay_site_audit order by id desc");
        try
        {
            if (ques_path_1 == "question_1")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_1);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_1 = maxid +"_"+ client_code + "_" + unit_code + "_" + ques_path_1 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_1, image.RawFormat);
                    image.Save(site_auditpath + question_path_1, image.RawFormat);
                    log.Error("Android - Finally question 1 photo save Sucessfull ....");
                }
            }
            if (ques_path_2 == "question_2")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_2);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_2 = maxid + "_" + client_code + "_" + unit_code + "_" + ques_path_2 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_2, image.RawFormat);
                    image.Save(site_auditpath + question_path_2, image.RawFormat);
                    log.Error("Android - Finally question 2 photo save Sucessfull ....");
                }
            }
            if (ques_path_3 == "question_3")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_3);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_3 = maxid + "_" + client_code + "_" + unit_code + "_" + ques_path_3 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_3, image.RawFormat);
                    image.Save(site_auditpath + question_path_3, image.RawFormat);
                    log.Error("Android - Finally question 3 photo save Sucessfull ....");
                }
            }
            if (ques_path_4 == "question_4")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_4);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_4 = maxid + "_" + client_code + "_" + unit_code + "_" + ques_path_4 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_4, image.RawFormat);
                    image.Save(site_auditpath + question_path_4, image.RawFormat);
                   
                    log.Error("Android - Finally question 4 photo save  Sucessfull ....");
                }
            }
            if (ques_path_5 == "question_5")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_5);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_5 = maxid + "_" + client_code + "_" + unit_code + "_" + ques_path_5 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_5, image.RawFormat);
                    image.Save(site_auditpath + question_path_5, image.RawFormat);
                   
                    log.Error("Android - Finally question 5 photo save Sucessfull ....");
                }
            }
            if (ques_path_6 == "question_6")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_6);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_6 = maxid + "_" + client_code + "_" + unit_code + "_" + ques_path_6 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/site_audit/") + question_path_6, image.RawFormat);
                    image.Save(site_auditpath + question_path_6, image.RawFormat);
                   
                    log.Error("Android - Finally question 6 photo save  Sucessfull ....");
                }
            }

            log.Error("Line Number : " + "insert into pay_site_audit (comp_code,client_code,unit_code,grade_name,emp_code,que_1_ans,que_2_ans,que_3_ans,que_4_ans,que_5_ans,que_6_ans,remark,cur_date,que_1_path,que_2_path,que_3_path,que_4_path,que_5_path,que_6_path,location) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + ques_1_ans + "','" + ques_2_ans + "','" + ques_3_ans + "','" + ques_4_ans + "','" + ques_5_ans + "','" + ques_6_ans + "','" + remarks + "',now(),'" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','" + question_path_5 + "','" + question_path_6 + "','" + current_address + "')");
            d.operation("insert into pay_site_audit (comp_code,client_code,unit_code,grade_name,emp_code,que_1_ans,que_2_ans,que_3_ans,que_4_ans,que_5_ans,que_6_ans,remark,cur_date,que_1_path,que_2_path,que_3_path,que_4_path,que_5_path,que_6_path,location) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + ques_1_ans + "','" + ques_2_ans + "','" + ques_3_ans + "','" + ques_4_ans + "','" + ques_5_ans + "','" + ques_6_ans + "','" + remarks + "',now(),'" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','" + question_path_5 + "','" + question_path_6 + "','" + current_address + "')");

        }
        catch (Exception error_geolocation)
        {
            log.Error(error_geolocation.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_geolocation, true).GetFrame(0).GetFileLineNumber());

        }
        finally
        {
            log.Error("Android - Finally Employee site_audit_data ....");
        }
    }

   
    // New Employee Requirement Form insert
    public void new_employee_requirement_form(string emp_code, string client_code, string unit_code, string grade_name,
        string employee_name, string father_name, string dob, string gender, string doj, 
        string blood_group, string working_hours, string address, string mobile_no, string bank_holder_name, 
        string account_no, string branch_name, string ifsc_code, string adhar_no, string adhar_image, string adhar_imgpath,string emp_image,
        string emp_imgpath,string police_image,string police_imgpath,string bankbook_image,string bankbook_imgpath)
    {
        Image image = null;
       string question_path_1="", question_path_2="", question_path_3="", question_path_4="";
        log.Error("Android - Inside new employee reuirement form");
        try
        {
            string temp = d.getsinglestring("SELECT coalesce(MAX(id), 0)+1 as id FROM  pay_new_employee_requirement");
             log.Error("Android - Inside new employee reuirement form"+temp);
            if (adhar_imgpath == "empReuirement_adharcard")
            {
                byte[] imageBytes = Convert.FromBase64String(adhar_image);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_1 =  temp + "_adharcard.png";
                    image.Save(HttpContext.Current.Server.MapPath("~/new_employee_requirement_document/") + question_path_1, image.RawFormat);
                    log.Error("Android - Finally empReuirement_adharcard  photo save Sucessfull ....");
                }
            }
            if (emp_imgpath == "empReuirement_emp")
            {
                byte[] imageBytes = Convert.FromBase64String(emp_image);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_2 = temp + "_employeeimage.png";
                    image.Save(HttpContext.Current.Server.MapPath("~/new_employee_requirement_document/") + question_path_2, image.RawFormat);
                    log.Error("Android - Finally empReuirement_emp photo save Sucessfull ....");
                }
            }
            if (police_imgpath == "empReuirement_policeverification")
            {
                byte[] imageBytes = Convert.FromBase64String(police_image);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_3 =  temp + "_policeverification.png";
                    image.Save(HttpContext.Current.Server.MapPath("~/new_employee_requirement_document/") + question_path_3, image.RawFormat);
                    log.Error("Android - Finally empReuirement_policeverification  photo save Sucessfull ....");
                }
            }
            if (bankbook_imgpath == "empReuirement_bankbook")
            {
                byte[] imageBytes = Convert.FromBase64String(bankbook_image);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    question_path_4 = temp + "_bankbook.png";
                    image.Save(HttpContext.Current.Server.MapPath("~/new_employee_requirement_document/") + question_path_4, image.RawFormat);
                    log.Error("Android - Finally empReuirement_bankbook 4  photo save Sucessfull ....");
                }
            }


            log.Error("Line Number : " + "insert into pay_new_employee_requirement (comp_code,client_name,unit_name,grade,supervisor_code,new_employee_name,father_name,dob,gender,doj,blood_group,working_hours,address,mobile_no,bank_holder_name,account_no,branch_name,ifsc_code,adhar_no,adharcard_imgpath,employee_imgpath,police_imgpath,bank_imgpath,moveto_employeemaster,cur_date) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + employee_name + "','" + father_name + "','" + dob + "','" + gender + "','" + doj + "','" + blood_group + "','" + working_hours + "','" + address + "','" + mobile_no + "','" + bank_holder_name + "','" + account_no + "','" + branch_name + "','" + ifsc_code + "','" + adhar_no + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','0',DATE_ADD(now(),INTERVAL 330 MINUTE))");
           // d.operation("insert into pay_new_employee_requirement (comp_code,client_name,unit_name,grade,supervisor_code,new_employee_name,father_name,dob,gender,doj,blood_group,working_hours,address,mobile_no,bank_holder_name,account_no,branch_name,ifsc_code,adhar_no,adharcard_imgpath,employee_imgpath,police_imgpath,bank_imgpath,moveto_employeemaster,cur_date) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + employee_name + "','" + father_name + "','" + dob + "','" + gender + "','" + doj + "','" + blood_group + "','" + working_hours + "','" + address + "','" + mobile_no + "','" + bank_holder_name + "','" + account_no + "','" + branch_name + "','" + ifsc_code + "','" + adhar_no + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','0',now())");
           
            //time zone changes
            d.operation("insert into pay_new_employee_requirement (comp_code,client_name,unit_name,grade,supervisor_code,new_employee_name,father_name,dob,gender,doj,blood_group,working_hours,address,mobile_no,bank_holder_name,account_no,branch_name,ifsc_code,adhar_no,adharcard_imgpath,employee_imgpath,police_imgpath,bank_imgpath,moveto_employeemaster,cur_date) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + employee_name + "','" + father_name + "','" + dob + "','" + gender + "','" + doj + "','" + blood_group + "','" + working_hours + "','" + address + "','" + mobile_no + "','" + bank_holder_name + "','" + account_no + "','" + branch_name + "','" + ifsc_code + "','" + adhar_no + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','0',now())");
          //  string maxid = d.getsinglestring("insert into pay_new_employee_requirement (comp_code,client_name,unit_name,grade,supervisor_code,new_employee_name,father_name,dob,gender,doj,blood_group,working_hours,address,mobile_no,bank_holder_name,account_no,branch_name,ifsc_code,adhar_no,adharcard_imgpath,employee_imgpath,police_imgpath,bank_imgpath,moveto_employeemaster,cur_date) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + grade_name + "','" + emp_code + "','" + employee_name + "','" + father_name + "','" + dob + "','" + gender + "','" + doj + "','" + blood_group + "','" + working_hours + "','" + address + "','" + mobile_no + "','" + bank_holder_name + "','" + account_no + "','" + branch_name + "','" + ifsc_code + "','" + adhar_no + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','0',now());select max(id)+1 from pay_new_employee_requirement;");
           
        }
        catch (Exception error_geolocation)
        {
            log.Error(error_geolocation.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_geolocation, true).GetFrame(0).GetFileLineNumber());

        }
        finally
        {
            log.Error("Android - Finally Employee site_audit_data ....");
        }
    }


    //---- Adhar card , Police verification And pan card approved comment details ---//
    public DataTable GetAdharPolicePanApprovedComment(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("image_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("comments", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("police_verification_End_date", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetAdharPolicePanApprovedComment..");
             //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            query = "select id,pay_document_verification.emp_name,case `image_name` when 'police_verification_form' then 'Police verification form'	when 'adhar_card_upload' then 'Adhar card' when 'Pan_card_upload' then 'Pan card' when 'Employee_photo_upload' then 'Employee id card'  WHEN 'joining_form' THEN 'Joining Form'  WHEN 'form_11' THEN 'Form 11' WHEN 'form_2' THEN 'Form 2' WHEN 'bank_passbook' THEN 'Bank Passbook' WHEN 'passprt_size_photo' THEN 'Passport size photo' else '' end as image_name,pay_document_verification.comments,(select police_verification_End_date from pay_employee_master where police_verification_End_date > date_format(DATE_ADD(now(), INTERVAL 8 DAY),'%d/%m/%y')) as  police_verification_End_date from pay_document_verification  inner join pay_employee_master on pay_document_verification.emp_code= pay_employee_master.emp_code where pay_document_verification.emp_code='" + userName + "' and android_flag='1'";
             log.Info("Android - Inside GetAdharPolicePanApprovedComment.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_name"].ToString(), reader1["image_name"].ToString(), reader1["comments"].ToString(), reader1["police_verification_End_date"].ToString());
                    log.Info("Android - Inside GetAdharPolicePanApprovedComment..update pay_document_verification set android_flag='2' where id='" + reader1["id"].ToString() + "'");

                    res1 = d.operation("update pay_document_verification set android_flag='2' where id='" + reader1["id"].ToString() + "'");
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetAdharPolicePanApprovedComment....");
        }
        return userDetailsTable;

    }

    //----  Police verification  update notification details ---//
    public DataTable GetPoliceValidDate(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("police_verification_End_date", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetPoliceValidDate..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            query = "select emp_name, police_verification_End_date from pay_employee_master where emp_code='" + userName + "'";
            log.Info("Android - Inside GetPoliceValidDate.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_name"].ToString(), reader1["police_verification_End_date"].ToString());
                   
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetPoliceValidDate....");
        }
        return userDetailsTable;

    }

    // Site Audit Approve And reject comment send notification
    public DataTable GetSuiteAuditComment(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("grade_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("comment", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("cur_date", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetSuiteAuditComment..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            query = "select pay_site_audit.id , pay_site_audit.emp_code, pay_client_master.client_name, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,pay_site_audit.grade_name, case android_flag when 1 then 'Approve site audit' when  2 then concat('Rejected :',`pay_site_audit`.`comment`) when  3 then `pay_site_audit`.`comment` else '' end as 'comment', pay_site_audit.android_flag,date_format(pay_site_audit.cur_date,'%d/%m/%Y') as cur_date from pay_site_audit inner join pay_client_master on pay_site_audit.comp_code=pay_client_master.comp_code and pay_site_audit.client_code=pay_client_master.client_code inner join pay_unit_master on pay_site_audit.comp_code=pay_unit_master.comp_code and pay_site_audit.unit_code=pay_unit_master.unit_code where pay_site_audit.emp_code='" + userName + "' and `android_flag` in('1','2') and `android_notification_flag`= '0'";
            log.Info("Android - Inside GetSuiteAuditComment.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["client_name"].ToString(), reader1["unit_name"].ToString(), reader1["grade_name"].ToString(), reader1["comment"].ToString(), reader1["cur_date"].ToString());
                    log.Info("Android - Inside GetSuiteAuditComment..update pay_site_audit set android_notification_flag='1' where id='" + reader1["id"].ToString() + "'");

                    res1 = d.operation("update pay_site_audit set android_notification_flag='1' where id='" + reader1["id"].ToString() + "'");
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetSuiteAuditComment....");
        }
        log.Info("Android - site audit data send " + userDetailsTable);

        return userDetailsTable;

    }


    // service rating Approve And reject comment send notification
    public DataTable GetServiceRatingComment(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("grade_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("comment", typeof(String)));
       // userDetailsTable.Columns.Add(new DataColumn("cur_date", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetServiceRatingComment..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            query = "SELECT `pay_service_rating`.`id`, `pay_service_rating`.`emp_code`, `pay_client_master`.`client_name`, CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', `UNIT_NAME`) AS 'unit_name', case pay_service_rating.flag when 1 then 'Approve service rating' when  2 then concat('Rejected :',`pay_service_rating`.`comment`) when  3 then `pay_service_rating`.`comment` else '' end  as 'comment', pay_service_rating.flag, `android_notification_flag` FROM `pay_service_rating` INNER JOIN `pay_client_master` ON `pay_service_rating`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_service_rating`.`client_code` = `pay_client_master`.`client_code` INNER JOIN `pay_unit_master` ON `pay_service_rating`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_service_rating`.`unit_code` = `pay_unit_master`.`unit_code` WHERE `pay_service_rating`.`emp_code` = '" + userName + "' AND pay_service_rating.`flag` in('1','2') and `android_notification_flag`= '0'";
            log.Info("Android - Inside GetServiceRatingComment.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                   // userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["client_name"].ToString(), reader1["unit_name"].ToString(), reader1["grade_name"].ToString(), reader1["comment"].ToString(), reader1["cur_date"].ToString());
                    userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["client_name"].ToString(), reader1["unit_name"].ToString(), reader1["comment"].ToString());
				    log.Info("Android - Inside GetServiceRatingComment..update pay_service_rating set android_notification_flag='1' where id='" + reader1["id"].ToString() + "'");

                    res1 = d.operation("update pay_service_rating set android_notification_flag='1' where id='" + reader1["id"].ToString() + "'");
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetServiceRatingComment....");
        }
        log.Info("Android - GetServiceRatingComment send " + userDetailsTable);

        return userDetailsTable;

    }

    // Operation management team schedule employee Site audit 
    public DataTable GetOperationSiteauditschedule(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("state", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("operation_date", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("start_time", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("end_time", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("comment", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetOperationSiteauditschedule..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            //query = "select pay_op_management_details.id, pay_op_management_details.emp_code,pay_client_master.CLIENT_NAME as client_name,pay_op_management_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,date_format(`pay_op_management_details`.`OPERATION_DATE`,'%d/%m/%Y') as operation_date,pay_op_management_details.START_TIME as start_time,pay_op_management_details.END_TIME as end_time from pay_op_management_details inner join pay_client_master on pay_op_management_details.comp_code=pay_client_master.comp_code and pay_op_management_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_op_management_details.comp_code=pay_unit_master.comp_code and pay_op_management_details.unit_code=pay_unit_master.unit_code  where pay_op_management_details.emp_code='" + userName + "' and pay_op_management_details.android_flag='0'";
          
            // add comment field
            query = "select pay_op_management_details.id, pay_op_management_details.emp_code,pay_client_master.CLIENT_NAME as client_name,pay_op_management_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,date_format(`pay_op_management_details`.`OPERATION_DATE`,'%d/%m/%Y') as operation_date,pay_op_management_details.START_TIME as start_time,pay_op_management_details.END_TIME as end_time, ifnull(comment,status) as 'comment' from pay_op_management_details inner join pay_client_master on pay_op_management_details.comp_code=pay_client_master.comp_code and pay_op_management_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_op_management_details.comp_code=pay_unit_master.comp_code and pay_op_management_details.unit_code=pay_unit_master.unit_code  where pay_op_management_details.emp_code='" + userName + "' and pay_op_management_details.android_flag='1' and pay_op_management_details.flag in (1,2)";

            log.Info("Android - Inside GetOperationSiteauditschedule.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["client_name"].ToString(), reader1["state"].ToString(), reader1["unit_name"].ToString(), reader1["operation_date"].ToString(), reader1["start_time"].ToString(), reader1["end_time"].ToString(), reader1["comment"].ToString());
                    log.Info("Android - Inside GetOperationSiteauditschedule..update pay_op_management_details set android_flag='2' where id='" + reader1["id"].ToString() + "'");

                    res1 = d.operation("update pay_op_management_details set android_flag='2' where id='" + reader1["id"].ToString() + "'");
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetOperationSiteauditschedule....");
        }
        log.Info("Android - GetOperationSiteauditschedule send " + userDetailsTable);

        return userDetailsTable;

    }


    // Operation management team schedule GetOperationSiteauditschedule_Notification
    public DataTable GetOperationSiteauditschedule_Notification(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("state", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("operation_date", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("start_time", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("end_time", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("comment", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside GetOperationSiteauditschedule_Notification..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            //query = "select pay_op_management_details.id, pay_op_management_details.emp_code,pay_client_master.CLIENT_NAME as client_name,pay_op_management_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,date_format(`pay_op_management_details`.`OPERATION_DATE`,'%d/%m/%Y') as operation_date,pay_op_management_details.START_TIME as start_time,pay_op_management_details.END_TIME as end_time from pay_op_management_details inner join pay_client_master on pay_op_management_details.comp_code=pay_client_master.comp_code and pay_op_management_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_op_management_details.comp_code=pay_unit_master.comp_code and pay_op_management_details.unit_code=pay_unit_master.unit_code  where pay_op_management_details.emp_code='" + userName + "' and pay_op_management_details.android_flag='0'";

            // add comment field
            query = "select pay_op_management_details.id, pay_op_management_details.emp_code,pay_client_master.CLIENT_NAME as client_name,pay_op_management_details.state,CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as unit_name,date_format(`pay_op_management_details`.`OPERATION_DATE`,'%d/%m/%Y') as operation_date,pay_op_management_details.START_TIME as start_time,pay_op_management_details.END_TIME as end_time, ifnull(comment,status) as 'comment' from pay_op_management_details inner join pay_client_master on pay_op_management_details.comp_code=pay_client_master.comp_code and pay_op_management_details.client_code=pay_client_master.client_code inner join pay_unit_master on pay_op_management_details.comp_code=pay_unit_master.comp_code and pay_op_management_details.unit_code=pay_unit_master.unit_code  where pay_op_management_details.emp_code='" + userName + "' and pay_op_management_details.android_flag='0' and pay_op_management_details.flag = '5' ";

            log.Info("Android - Inside GetOperationSiteauditschedule_Notification.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["client_name"].ToString(), reader1["state"].ToString(), reader1["unit_name"].ToString(), reader1["operation_date"].ToString(), reader1["start_time"].ToString(), reader1["end_time"].ToString(), reader1["comment"].ToString());
                    log.Info("Android - Inside GetOperationSiteauditschedule_Notification..update pay_op_management_details set android_flag='1' where id='" + reader1["id"].ToString() + "'");

                    res1 = d.operation("update pay_op_management_details set android_flag='1' where id='" + reader1["id"].ToString() + "'");
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetOperationSiteauditschedule_Notification....");
        }
        log.Info("Android - GetOperationSiteauditschedule_Notification send " + userDetailsTable);

        return userDetailsTable;

    }

    // get travelling expenses mode list
    public DataTable Gettravellingexpensesmode(string userName)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("chkair", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chkbus", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chktraint1", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chktraint2", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chktraint3", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chkcabtaxi", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chkauto", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("chkownedvehicle", typeof(String)));

        try
        {

            int res1;
            log.Info("Android - Inside Gettravellingexpensesmode..");
            query = "select chkair,chkbus,chktraint1,chktraint2,chktraint3,chkcabtaxi,chkauto,chkownedvehicle from pay_travel_policy_master where id = (select policy_id from pay_travel_emp_policy where emp_code = '" + userName + "')";
            log.Info("Android - Inside Gettravellingexpensesmode.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["chkair"].ToString(), reader1["chkbus"].ToString(), reader1["chktraint1"].ToString(), reader1["chktraint2"].ToString(), reader1["chktraint3"].ToString(), reader1["chkcabtaxi"].ToString(), reader1["chkauto"].ToString(), reader1["chkownedvehicle"].ToString());
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally Gettravellingexpensesmode....");
        }
        log.Info("Android - Gettravellingexpensesmode send " + userDetailsTable);

        return userDetailsTable;
    }

    public DataTable GetUserContactDetails(String client_code, String unit_code, String userName)
    {
        DataTable userDetailsTable = new DataTable();
        try
        {

            userDetailsTable.Columns.Add(new DataColumn("LocationHead_Name", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("LocationHead_mobileno", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("LocationHead_Emailid", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("emp_mobile_no", typeof(String)));

            log.Error("Android - Inside GetUserContactDetails..");
            string query = "select pay_unit_master.LocationHead_Name,pay_unit_master.LocationHead_mobileno,pay_unit_master.LocationHead_Emailid ,(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_client_state_role_grade.emp_code) as 'emp_name',(SELECT EMP_MOBILE_NO FROM pay_employee_master a WHERE a.emp_code = pay_client_state_role_grade.emp_code) as 'emp_mobile_no' from  pay_unit_master inner join pay_client_state_role_grade on pay_unit_master.unit_code=pay_client_state_role_grade.unit_code and pay_unit_master.comp_code=pay_client_state_role_grade.comp_code where pay_unit_master.unit_code='" + unit_code + "' and pay_unit_master.comp_code=(select comp_code from pay_employee_master where emp_code='" + userName + "') and pay_unit_master.client_code='" + client_code + "' and pay_client_state_role_grade.department_type = 'Admin' limit 1";
            
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["LocationHead_Name"].ToString(), reader["LocationHead_mobileno"].ToString(),
                        reader["LocationHead_Emailid"].ToString(), reader["emp_name"].ToString(), reader["emp_mobile_no"].ToString());
                }
            }

            reader.Close();
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetUserContactDetails....");
        }
        return userDetailsTable;
    }


    //for field officer

    public DataTable GetUserContactFieldDetails(string emp_code)
    {
        DataTable userDetailsTable = new DataTable();
        try
        {

            userDetailsTable.Columns.Add(new DataColumn("ADMIN_NAME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("ADMIN_MOBILE_NO", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("FIELD_OFFICER_NAME", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("FEILD_OFFICER_MOBILE_NO", typeof(String)));


            log.Error("Android - Inside GetUserContactFieldDetails..");
            //string query = "SELECT pay_employee_master.EMP_NAME as EMP_NAME,pay_employee_master.Emp_Code as EMP_CODE,(SELECT Grade_desc FROM pay_grade_master g WHERE g.grade_code = pay_employee_master.GRADE_CODE and g.comp_code = pay_employee_master.comp_CODE) AS 'GRADE_CODE',(SELECT company_name FROM pay_company_master c WHERE c.comp_code = pay_employee_master.comp_code) AS 'COMP_NAME',pay_unit_master.UNIT_NAME as UNIT_NAME,(SELECT DEPT_NAME FROM pay_department_master d WHERE d.DEPT_CODE = pay_employee_master.DEPT_CODE) AS 'DEPT_CODE',pay_employee_master.emp_mobile_no AS 'EMP_MOBILE_NO',CASE GENDER WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS 'GENDER',pay_employee_master.in_time AS 'SHIFT_TIME',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', attendance_id AS 'REPORTING_TO' FROM pay_employee_master,pay_unit_master Where pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE and pay_employee_master.EMP_CODE='" + userName + "'";
            //string query = "SELECT pay_employee_master.EMP_NAME as EMP_NAME,pay_employee_master.Emp_Code as EMP_CODE,(SELECT Grade_desc FROM pay_grade_master g WHERE g.grade_code = pay_employee_master.GRADE_CODE and g.comp_code = pay_employee_master.comp_CODE) AS 'GRADE_CODE',(SELECT company_name FROM pay_company_master c WHERE c.comp_code = pay_employee_master.comp_code) AS 'COMP_NAME',pay_unit_master.UNIT_NAME as UNIT_NAME,pay_images_master.EMP_PHOTO  AS 'DEPT_CODE',pay_employee_master.emp_mobile_no AS 'EMP_MOBILE_NO',CASE GENDER WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS 'GENDER',pay_employee_master.in_time AS 'SHIFT_TIME',DATE_FORMAT(pay_employee_master.JOINING_DATE, '%d/%m/%Y') AS 'JOINING_DATE',DATE_FORMAT(pay_employee_master.BIRTH_DATE, '%d/%m/%Y') AS 'BIRTH_DATE', attendance_id AS 'REPORTING_TO' FROM pay_employee_master,pay_unit_master,pay_images_master Where pay_employee_master.comp_code = pay_unit_master.comp_code AND pay_employee_master.UNIT_CODE = pay_unit_master.UNIT_CODE and pay_employee_master.emp_code=pay_images_master.emp_code and pay_employee_master.EMP_CODE='" + userName + "'";
            //string query = "SELECT (SELECT `pay_employee_master`.`EMP_NAME` FROM `pay_employee_master` WHERE `EMP_CODE` IN (SELECT `EMP_CODE` FROM `pay_client_state_role_grade` WHERE `pay_client_state_role_grade`.`comp_code` IN (SELECT `COMP_CODE` FROM `pay_op_management` WHERE `pay_op_management`.`EMP_CODE` = '" + emp_code + "') AND `pay_client_state_role_grade`.`unit_code` IN (SELECT `unit_code` FROM `pay_op_management` WHERE `pay_op_management`.`EMP_CODE` = '" + emp_code + "') AND `pay_client_state_role_grade`.`department_type` = 'Admin')) AS 'ADMIN_NAME', (SELECT `pay_employee_master`.`EMP_MOBILE_NO` FROM `pay_employee_master` WHERE `EMP_CODE` IN (SELECT `EMP_CODE` FROM `pay_client_state_role_grade` WHERE `pay_client_state_role_grade`.`comp_code` IN (SELECT `comp_code` FROM `pay_op_management` WHERE `pay_op_management`.`EMP_CODE` = '" + emp_code + "') AND `pay_client_state_role_grade`.`unit_code` IN (SELECT `unit_code` FROM `pay_op_management` WHERE `pay_op_management`.`EMP_CODE` = '" + emp_code + "') AND `pay_client_state_role_grade`.`department_type` = 'Admin')) AS 'ADMIN_MOBILE_NO', `pay_op_management`.`field_officer_name` AS 'FIELD_OFFICER_NAME', `pay_op_management`.`MOBILE_NO` AS 'FEILD_OFFICER_MOBILE_NO' FROM `pay_unit_master` INNER JOIN `pay_op_management` ON `pay_unit_master`.`comp_code` = `pay_op_management`.`comp_code` AND `pay_unit_master`.`unit_code` = `pay_op_management`.`unit_code` WHERE `pay_op_management`.`emp_code` = '" + emp_code + "'";
            string query = "select pay_op_management.mobile_no as 'FEILD_OFFICER_MOBILE_NO',pay_op_management.field_officer_name as 'FIELD_OFFICER_NAME',	(SELECT emp_name FROM pay_employee_master a WHERE a.emp_code = pay_client_state_role_grade.emp_code) as 'ADMIN_NAME',	(SELECT EMP_MOBILE_NO FROM pay_employee_master a WHERE a.emp_code = pay_client_state_role_grade.emp_code) as 'ADMIN_MOBILE_NO' 	from pay_client_state_role_grade inner join pay_op_management on pay_client_state_role_grade.comp_code=pay_op_management.comp_code and pay_client_state_role_grade.unit_code=pay_op_management.unit_code 	where pay_client_state_role_grade.department_type='Admin' and pay_client_state_role_grade.unit_code=(select unit_code from pay_employee_master where emp_code='"+emp_code+"') and  pay_client_state_role_grade.comp_code=(Select comp_code from pay_employee_master where emp_code='"+emp_code+"') limit 1";
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["ADMIN_NAME"].ToString(), reader["ADMIN_MOBILE_NO"].ToString(),
                        reader["FIELD_OFFICER_NAME"].ToString(), reader["FEILD_OFFICER_MOBILE_NO"]);
                }
            }

            reader.Close();
        }
        catch (Exception error_dept_details)
        {
            log.Error(error_dept_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_dept_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetUserContactFieldDetails....");
        }
        return userDetailsTable;
    }

    // Employee Joining Document List 
    public void Joining_documentList(string emp_code, string ques_image_1, string ques_path_1, string ques_image_2, string ques_path_2, string ques_image_3, string ques_path_3, string ques_image_4, string ques_path_4, string ques_image_5, string ques_path_5, string ques_image_6, string ques_path_6, string ques_image_7, string ques_path_7)
    {
        Image image = null;
        string verificationname = "";
        string emp_code_id = "";
        string question_path_1 = "", question_path_2 = "", question_path_3 = "", question_path_4 = "", question_path_5 = "", question_path_6 = "", question_path_7 = "";
        log.Error("Android - Inside Employee Joining_documentList");
        try
        {
            if (ques_path_1 == "question_1")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_1);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "joining_form";
                    emp_code_id = emp_code + "_26.png";
                    question_path_1 = emp_code + "_"  + ques_path_1 + ".png";

                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_1, image.RawFormat);
                    //log.Error("Android - Finally question 1 photo save  Sucessfull ....");

                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee Adhar Card scanning details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally question 1 photo save  Sucessfull ....");
                }
            }
            if (ques_path_2 == "question_2")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_2);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "form_11";
                    emp_code_id = emp_code + "_16.png";
                    question_path_2 = emp_code + "_" + ques_path_2 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_2, image.RawFormat);
                    //log.Error("Android - Finally question 2 photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside form_11 Card scanning details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally form_11 photo save  Sucessfull ....");
                }
            }
            if (ques_path_3 == "question_3")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_3);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "form_2";
                    emp_code_id = emp_code + "_14.png";
                    question_path_3 = emp_code + "_" + ques_path_3 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_3, image.RawFormat);
                    //log.Error("Android - Finally question 3 photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee form_2 details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally form_2 photo save  Sucessfull ....");
                }
            }
            if (ques_path_4 == "question_4")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_4);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "adhar_card_upload";
                    emp_code_id = emp_code + "_3.png";
                    question_path_4 = emp_code + "_" + ques_path_4 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_4, image.RawFormat);
                    //log.Error("Android - Finally question 4 photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee adhar_card_upload details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally adhar_card_upload photo save  Sucessfull ....");
                }
            }
            if (ques_path_5 == "question_5")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_5);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "bank_passbook";
                    emp_code_id = emp_code + "_24.png";
                    question_path_5 = emp_code + "_" + ques_path_5 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_5, image.RawFormat);
                    //log.Error("Android - Finally question 5 photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee bank_passbook details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally bank_passbook photo save  Sucessfull ....");
                }
            }
            if (ques_path_6 == "question_6")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_6);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "Pan_card_upload";
                    emp_code_id = emp_code + "_2.png";
                    question_path_6 = emp_code + "_" + ques_path_6 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_6, image.RawFormat);
                    //log.Error("Android - Finally question 6 photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee Pan_card_upload details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally Pan_card_upload photo save  Sucessfull ....");
                }
            }
            if (ques_path_7 == "question_7")
            {
                byte[] imageBytes = Convert.FromBase64String(ques_image_7);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    verificationname = "passprt_size_photo";
                    emp_code_id = emp_code + "_1.png";
                    question_path_7 = emp_code + "_" + ques_path_7 + ".png";
                    //image.Save(HttpContext.Current.Server.MapPath("~/joining_documents_kit/") + question_path_7, image.RawFormat);
                    //log.Error("Android - Finally question 7  photo save Sucessfull ....");
                    String insert = "insert into pay_document_verification (comp_code,emp_code,emp_name,image_name,image_path,cur_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "',(select emp_name from pay_employee_master where emp_code='" + emp_code + "'),'" + verificationname + "','" + emp_code_id + "',now())";

                    log.Error("Android - Inside Employee passprt_size_photo details send :- " + insert);
                    MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                    d.conopen();
                    cmd_update.ExecuteNonQuery();
                    d.conclose();

                    image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code_id, image.RawFormat);
                    log.Error("Android - Finally passprt_size_photo photo save  Sucessfull ....");
                }
            }

           // log.Error("Line Number : " + "insert into pay_joining_doc_kit (comp_code,emp_code,joining_form,form_11,form_2,adhar_card,bank_document,pan_card,passport_size_photo,curr_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','" + question_path_5 + "','" + question_path_6 + "','" + question_path_7 + "',now())");
            //d.operation("insert into pay_joining_doc_kit (comp_code,unit_code,client_code,emp_code,joining_form,form_11,form_2,adhar_card,bank_document,pan_card,passport_size_photo,curr_date) values ((select comp_code from pay_employee_master where emp_code='" + emp_code + "'),(select unit_code from pay_employee_master where emp_code='" + emp_code + "'),(select client_code from pay_employee_master where emp_code='" + emp_code + "'),'" + emp_code + "','" + question_path_1 + "','" + question_path_2 + "','" + question_path_3 + "','" + question_path_4 + "','" + question_path_5 + "','" + question_path_6 + "','" + question_path_7 + "',now())");

        }
        catch (Exception error_geolocation)
        {
            log.Error(error_geolocation.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_geolocation, true).GetFrame(0).GetFileLineNumber());

        }
        finally
        {
            d.conclose();
            log.Error("Android - Finally Employee Joining_documentList ....");
        }
    }

    // Tab Employee Attendances details 
     public DataTable GetTabEmployeeAttendancesDetails(string super_emp_code)
    {
        string query = "";
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("grade_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("grade_desc", typeof(String)));
         
        try
        {
           
            log.Info("Android - Inside GetTabEmployeeAttendancesDetails..");
            //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            query = "select  emp_code,emp_name,pay_employee_master.grade_code as 'grade_code',pay_employee_master.employee_type, pay_grade_master.grade_desc as 'grade_desc' from pay_employee_master  inner join pay_grade_master on pay_employee_master.comp_code=pay_grade_master.comp_code  and pay_employee_master.grade_code=pay_grade_master.grade_code where pay_employee_master.comp_code=(select comp_code from pay_employee_master where emp_code='" + super_emp_code + "') and pay_employee_master.client_code=(select client_code from pay_employee_master where emp_code='" + super_emp_code + "')  and pay_employee_master.unit_code=(select  unit_code from pay_employee_master where emp_code='" + super_emp_code + "') and  pay_employee_master.employee_type='Permanent' and (left_date ='' or left_date is null) and pay_employee_master.grade_code !='SAM'";
            log.Info("Android - Inside GetTabEmployeeAttendancesDetails.." + query);
            MySqlCommand command = new MySqlCommand(query, d1.con);
            d1.con.Open();
            MySqlDataReader reader1 = command.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    userDetailsTable.Rows.Add(reader1["emp_code"].ToString(), reader1["emp_name"].ToString(), reader1["grade_code"].ToString(), reader1["grade_desc"].ToString());
                  
                }
            }

            reader1.Close();
            d1.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally GetTabEmployeeAttendancesDetails....");
        }
        log.Info("Android - GetTabEmployeeAttendancesDetails send " + userDetailsTable);

        return userDetailsTable;

    }

    // Tab Employee Attendances send details
     public void TABEmployeeattendances_send(string EmpCode, string EmpName, string EmpDesignation, string EmpDesignation_code, string SupervisorEmpCode,
         string EmpShift, string Emppunctuality, string Empuniforms, string Empcap, string Empshoes, string Empbelt, string Empidcard, 
         string Empshaving, string Emphairs, string Empnails, string Empbriefing, string Empremarks, string Emp_intimeimage,string Emp_location)
     {
         string currentdate_check = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
         string tab_employeecheck = d.getsinglestring("select  * from pay_tab_employee_attendances_details where emp_code='" + EmpCode + "' and date(currdate)=date(str_to_date('" + currentdate_check + "','%d/%m/%Y'))");

         if (tab_employeecheck.Equals(""))
         {
             try
             {
                 log.Error("Android - Inside TABEmployeeattendances_send");
                 String verificationname = "Tab_Employee_Attendances_IntimeImage";
                 string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
                 string image_name = EmpCode + "_Intime_" + currentdate2 + ".png";

                 Base64ToImage(Emp_intimeimage, EmpCode, verificationname, image_name);


                 log.Info("Android - Inside TABEmployeeattendances_send.." + "insert into pay_tab_employee_attendances_details (`comp_code` ,`client_code` ,`unit_code` ,`supervisor_emp_name` ,`supervisor_emp_code` ,`emp_code` ,`emp_name` ,`grade_code` ,`grade_desc` ,`shifttime` ,`punctuality`,`uniforms` ,`cap` ,`shoes` ,`belt` ,`id_card` ,`shaving` ,`hairs` ,`nails` ,`briefing` ,`remarks` ,`intime_imgpath` ,`currdate` ,`location_add` ) values ((select comp_code from pay_employee_master where emp_code='" + EmpCode + "'),(select client_code from pay_employee_master where emp_code='" + EmpCode + "'),(select unit_code from pay_employee_master where emp_code='" + EmpCode + "'),(select emp_name from pay_employee_master where emp_code='" + SupervisorEmpCode + "'),'" + SupervisorEmpCode + "','" + EmpCode + "','" + EmpName + "','" + EmpDesignation_code + "','" + EmpDesignation + "','" + EmpShift + "','" + Emppunctuality + "','" + Empuniforms + "','" + Empcap + "','" + Empshoes + "','" + Empbelt + "','" + Empidcard + "','" + Empshaving + "','" + Emphairs + "','" + Empnails + "','" + Empbriefing + "','" + Empremarks + "','" + image_name + "',now(),'" + Emp_location + "')");
                 // string update = "UPDATE pay_images_master SET POLICE_VERIFICATION_DOC='" + emp_code_id + "' , MODIF_DATE=now() WHERE  comp_code = (select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "')  AND EMP_CODE='" + EMP_CODE + "'";
                 String insert = "insert into pay_tab_employee_attendances_details (`comp_code` ,`client_code` ,`unit_code` ,`supervisor_emp_name` ,`supervisor_emp_code` ,`emp_code` ,`emp_name` ,`grade_code` ,`grade_desc` ,`shifttime` ,`punctuality`,`uniforms` ,`cap` ,`shoes` ,`belt` ,`id_card` ,`shaving` ,`hairs` ,`nails` ,`briefing` ,`remarks` ,`intime_imgpath` ,`currdate` ,`location_add` ) values ((select comp_code from pay_employee_master where emp_code='" + EmpCode + "'),(select client_code from pay_employee_master where emp_code='" + EmpCode + "'),(select unit_code from pay_employee_master where emp_code='" + EmpCode + "'),(select emp_name from pay_employee_master where emp_code='" + SupervisorEmpCode + "'),'" + SupervisorEmpCode + "','" + EmpCode + "','" + EmpName + "','" + EmpDesignation_code + "','" + EmpDesignation + "','" + EmpShift + "','" + Emppunctuality + "','" + Empuniforms + "','" + Empcap + "','" + Empshoes + "','" + Empbelt + "','" + Empidcard + "','" + Empshaving + "','" + Emphairs + "','" + Empnails + "','" + Empbriefing + "','" + Empremarks + "','" + image_name + "',now(),'" + Emp_location + "')";

                 MySqlCommand cmd_update = new MySqlCommand(insert, d.con);
                 d.conopen();
                 cmd_update.ExecuteNonQuery();
                 d.conclose();
                 log.Error("Android - query error");

             }
             catch (Exception error_leave_apply)
             {
                 log.Error(error_leave_apply.Message);
                 log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
             }
             finally
             {
                 d.con.Close();
                 log.Error("Android - Finally TABEmployeeattendances_send ....");
             }
         
         }else{
             log.Error("Android -  Already add employee that date ....");

         }

     }

    // tab employee out time attendaces marks
     public void TABEmployeeattendances_outtimeMark(string EmpCode, string EmpName, string Emp_outimage) 
     {
         int res1;
     string currentdate_check = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

     try { 
         string tab_employeecheck = d.getsinglestring("select  * from pay_tab_employee_attendances_details where emp_code='" + EmpCode + "' and date(currdate)=date(str_to_date('" + currentdate_check + "','%d/%m/%Y'))");

         if (!tab_employeecheck.Equals(""))
         {
             log.Error("Android - Inside TABEmployeeattendances_outtimeMark");
             String verificationname = "Tab_Employee_Attendances_OuttimeImage";
             string currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
             string image_name = EmpCode + "_Outtime_" + currentdate2 + ".png";

             Base64ToImage(Emp_outimage, EmpCode, verificationname, image_name);
             res1 = d.operation("update pay_tab_employee_attendances_details set outtime_imgpath='" + image_name + "' where emp_code='" + EmpCode + "' and date(currdate)=date(str_to_date('" + currentdate_check + "','%d/%m/%Y'))");
         }
     }
     catch (Exception e) { }
     finally {
     
     }
     log.Error("Android - Finally TABEmployeeattendances_outtimeMark ....");
     }

     // Employee Document Details List 
     public DataTable GetEmployeeDocumentList(string emp_code)
     {
         string query = "";
         String url = HttpContext.Current.Server.MapPath("~/EMP_Images/");
         DataTable userDetailsTable = new DataTable();

         userDetailsTable.Columns.Add(new DataColumn("original_photo", typeof(String)));
         userDetailsTable.Columns.Add(new DataColumn("original_adhar_card", typeof(String)));
         userDetailsTable.Columns.Add(new DataColumn("emp_signature", typeof(String)));
         userDetailsTable.Columns.Add(new DataColumn("original_address_proof", typeof(String)));
         userDetailsTable.Columns.Add(new DataColumn("bank_passbook", typeof(String)));
         userDetailsTable.Columns.Add(new DataColumn("emp_adhar_pan", typeof(String)));

         try
         {

             log.Info("Android - Inside GetEmployeeDocumentList..");
             //query = "select id,emp_name,image_name,comments from pay_document_verification where emp_code='"+userName+"' and android_flag='1'";
            // query = "select  concat('" + url + "',original_photo) as 'original_photo',concat('" + url + "',original_adhar_card) as 'original_adhar_card' ,concat('" + url + "',emp_signature) as 'emp_signature',concat('" + url + "',original_address_proof) as 'original_address_proof',concat('" + url + "',bank_passbook) as 'bank_passbook',concat('" + url + "',emp_adhar_pan) as 'emp_adhar_pan' from pay_images_master where emp_code='" + emp_code + "'";
             query = "select  original_photo as 'original_photo',original_adhar_card as 'original_adhar_card' ,emp_signature as 'emp_signature',original_address_proof as 'original_address_proof',bank_passbook as 'bank_passbook',emp_adhar_pan as 'emp_adhar_pan' from pay_images_master where emp_code='" + emp_code + "'";
            
             log.Info("Android - Inside GetEmployeeDocumentList.." + query);
             MySqlCommand command = new MySqlCommand(query, d1.con);
             d1.con.Open();
             MySqlDataReader reader1 = command.ExecuteReader();
             if (reader1.HasRows)
             {
                 while (reader1.Read())
                 {
                     userDetailsTable.Rows.Add(reader1["original_photo"].ToString(), reader1["original_adhar_card"].ToString(), reader1["emp_signature"].ToString(), reader1["original_address_proof"].ToString(), reader1["bank_passbook"].ToString(), reader1["emp_adhar_pan"].ToString());

                 }
             }

             reader1.Close();
             d1.con.Close();
         }
         catch (Exception error_leave_details)
         {
             log.Error(error_leave_details.Message);
             log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
         }
         finally
         {
             d1.con.Close();
             log.Error("Android - Finally GetEmployeeDocumentList....");
         }
         log.Info("Android - GetEmployeeDocumentList send " + userDetailsTable);

         return userDetailsTable;

     }


     // service quality rating  data insert
     public void service_quality_rating_data(string client_code, string unit_code, string emp_code, string quesservice_1_ans, string quesservice_2_ans, string quesservice_3_ans, string quesservice_4_ans, string quesservice_5_ans, string quesservice_6_ans, string quesservice_7_ans, string quesservice_8_ans, string quesservice_9_ans, string quesservice_10_ans, string remarks)
     {
          log.Error("Android - Inside Service quality rating");
         try
         {

             log.Error("Line Number : " + "insert into pay_service_rating (comp_code,client_code,unit_code,emp_code,answer1,answer2,answer3,answer4,answer5,answer6,answer7,answer8,answer9,answer10,remark,state_name,unit_name,cur_date,emp_name) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + emp_code + "','" + quesservice_1_ans + "','" + quesservice_2_ans + "','" + quesservice_3_ans + "','" + quesservice_4_ans + "','" + quesservice_5_ans + "','" + quesservice_6_ans + "','" + quesservice_7_ans + "','" + quesservice_8_ans + "','" + quesservice_9_ans + "','" + quesservice_10_ans + "','" + remarks + "',(select state_name from pay_unit_master where client_code='" + client_code + "' and unit_code='" + unit_code + "'),(select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as 'unit_name' from pay_unit_master where client_code='" + client_code + "' and unit_code='" + unit_code + "'),now(),(select emp_name from pay_employee_master where emp_code='" + emp_code + "'))");
             d.operation("insert into pay_service_rating (comp_code,client_code,unit_code,emp_code,answer1,answer2,answer3,answer4,answer5,answer6,answer7,answer8,answer9,answer10,remark,state_name,unit_name,cur_date,emp_name) values ((select comp_code from pay_client_master where client_code='" + client_code + "'),'" + client_code + "','" + unit_code + "','" + emp_code + "','" + quesservice_1_ans + "','" + quesservice_2_ans + "','" + quesservice_3_ans + "','" + quesservice_4_ans + "','" + quesservice_5_ans + "','" + quesservice_6_ans + "','" + quesservice_7_ans + "','" + quesservice_8_ans + "','" + quesservice_9_ans + "','" + quesservice_10_ans + "','" + remarks + "',(select state_name from pay_unit_master where client_code='" + client_code + "' and unit_code='" + unit_code + "'),(select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as 'unit_name' from pay_unit_master where client_code='" + client_code + "' and unit_code='" + unit_code + "'),now(),(select emp_name from pay_employee_master where emp_code='" + emp_code + "'))");

         }
         catch (Exception error_geolocation)
         {
             log.Error(error_geolocation.Message);
             log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_geolocation, true).GetFrame(0).GetFileLineNumber());

         }
         finally
         {
             log.Error("Android - Finally service quality rating data ....");
         }
     }

    // Android throw forget password
    public DataTable ForgetPassword(string emp_code,string birth_day,string adhar_card){

        string query = "";
        string newpassowrd = "";
        string newPasswordsha256 = "";
        DataTable userDetailsTable = new DataTable();
        DataTable userDetailsTableabc = new DataTable();
        userDetailsTable.Columns.Add(new DataColumn("emp_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("new_password", typeof(String)));
        userDetailsTableabc.Columns.Add(new DataColumn("new_password", typeof(String)));
       

        try
        {

            log.Info("Android - Inside ForgetPassword..");
            query = "select emp_name,date_format(birth_date,'%d/%m/%Y') as 'birth_date',left_date from pay_employee_master where emp_code='" + emp_code + "' and birth_date='" + birth_day + "' and P_TAX_NUMBER='" + adhar_card + "'";

            log.Info("Android - Inside ForgetPassword.." + query);
            MySqlCommand command2 = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    if (reader2["left_date"].ToString().Equals(""))
                    {
                        
                        newpassowrd = reader2["emp_name"].ToString().Substring(0,4).ToUpper() + reader2["birth_date"].ToString().Substring(6,4);
                        newPasswordsha256 = GetSha256FromString(newpassowrd);
                        log.Error("Line Number : update pay_user_master set user_password='" + newPasswordsha256 + "',flag='A',password_changed_date= date(now()) where Login_id='" + emp_code + "'");
                        int result = d1.operation("update pay_user_master set user_password='" + newPasswordsha256 + "',flag='A',password_changed_date= date(now()) where Login_id='" + emp_code + "'");
                        userDetailsTable.Rows.Add(reader2["emp_name"].ToString(), newpassowrd.ToString());

                        log.Error("Line Number : " + userDetailsTable);
                        
                    }
                    else
                    {

                    }
                }

                log.Error("Line Number : " + userDetailsTable);
            }

            reader2.Close();
            d.con.Close();
        }
        catch (Exception error_leave_details)
        {
            log.Error(error_leave_details.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_details, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d1.con.Close();
            log.Error("Android - Finally ForgetPassword....");
        }
        log.Info("Android - ForgetPassword send " + userDetailsTable);

        return userDetailsTable;

    }


    //Employee send pay_fire_extinguisher_photo  image send 
    public void Employee_fire_extinguisher_image_send(string type_extinguisher,string Upload_Image_base64, string EMP_CODE)
    {

        try
        {
            log.Error("Android - Inside Employee Fire Extinguisher image send");
            String verificationname = "Fire_Extinguisher_image";
            String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            String working_image_name = EMP_CODE + "_Fire_Extinguisher_" + currentdate2 + ".png";

            // string  query1 = "insert into pay_android_working_image (comp_code,unit_code,emp_code,image_name,current_date) values ((select comp_code from pay_employee_master where emp_code='" + EMP_CODE + "'),(select UNIT_CODE from pay_employee_master where emp_code='" + EMP_CODE + "'),'" + EMP_CODE + "','" + working_image_name + "',now())";
            d.operation("insert into pay_fire_extinguisher_photo (comp_code,client_code,unit_code,emp_code,emp_name,unit_name,state_name,image_path,type_name,curr_date) select  pay_employee_master.comp_code,pay_unit_master.client_code,pay_employee_master.unit_code,emp_code,emp_name,pay_unit_master.unit_name,pay_unit_master.state_name,'" + working_image_name + "','" + type_extinguisher + "',date(now()) from pay_employee_master inner join pay_unit_master on pay_employee_master.comp_code=pay_unit_master.comp_code and  pay_employee_master.client_code=pay_unit_master.client_code and pay_employee_master.unit_code=pay_unit_master.unit_code where pay_employee_master.emp_code='" + EMP_CODE + "'");
            Base64ToImage(Upload_Image_base64, EMP_CODE, verificationname, currentdate2);

        }
        catch (Exception error_leave_apply)
        {
            log.Error(error_leave_apply.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_leave_apply, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally Employee Employee Fire Extinguisher upload ....");
        }

    }

    // GetFireExtinguisherNotification notification 
    
    public DataTable GetFireExtinguisherNotification(string EMP_CODE)
    {
        DataTable userDetailsTable = new DataTable();

        userDetailsTable.Columns.Add(new DataColumn("emp_code", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("client_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("unit_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("type_name", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("curr_date", typeof(String)));
        userDetailsTable.Columns.Add(new DataColumn("reject_reason", typeof(String)));
        
        //userDetailsTable.Columns.Add(new DataColumn("no_of_days", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("leave_reason", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("leave_status", typeof(String)));
        //userDetailsTable.Columns.Add(new DataColumn("status_comment", typeof(String)));


        try
        {


            string query = "";
            log.Error("Android - Inside GetFireExtinguisherNotification Notification....");

            query = "SELECT pay_fire_extinguisher_photo.id as 'ID', `emp_code`, `client_name`, CONCAT((SELECT DISTINCT (`STATE_CODE`) FROM `pay_state_master` WHERE `STATE_NAME` = `pay_unit_master`.`STATE_NAME`), '_', `UNIT_CITY`, '_', `UNIT_ADD1`, '_', pay_unit_master.`UNIT_NAME`) as 'unit_name', type_name,date_format(curr_date,'%d/%m/%Y') as 'curr_date',reject_reason FROM `pay_fire_extinguisher_photo` INNER JOIN `pay_client_master` ON `pay_fire_extinguisher_photo`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_fire_extinguisher_photo`.`client_code` = `pay_client_master`.`client_code` INNER JOIN `pay_unit_master` ON `pay_fire_extinguisher_photo`.`comp_code` = `pay_unit_master`.`comp_code` AND `pay_fire_extinguisher_photo`.`client_code` = `pay_unit_master`.`client_code` AND `pay_fire_extinguisher_photo`.`unit_code` = `pay_unit_master`.`unit_code` WHERE `emp_code` = '" + EMP_CODE + "' AND `approve_fire` IN (1, 2)";
            
            log.Error("Android - Inside GetFireExtinguisherNotification Notification...." + query);
            MySqlCommand command = new MySqlCommand(query, d.con);
            d.con.Open();
            MySqlDataReader reader = command.ExecuteReader();

            //if (reader.HasRows)
            //{
            while (reader.Read())
            {
                //userDetailsTable.Rows.Add(reader["firstName"], reader["lastName"]);

                userDetailsTable.Rows.Add(reader["emp_code"].ToString(), reader["client_name"].ToString(), reader["unit_name"].ToString()
                    , reader["type_name"].ToString(), reader["curr_date"].ToString(), reader["reject_reason"].ToString());

                d.operation("Update pay_fire_extinguisher_photo set approve_fire='4' where id = '" + reader["ID"].ToString() + "'");
                
            }
            //}

            reader.Close();
            d.con.Close();
        }
        catch (Exception error_attendance)
        {
            log.Error(error_attendance.Message);
            log.Error("Line Number : " + new System.Diagnostics.StackTrace(error_attendance, true).GetFrame(0).GetFileLineNumber());
        }
        finally
        {
            d.con.Close();
            log.Error("Android - Finally GetFireExtinguisherNotification Notification...." + userDetailsTable);
        }
        return userDetailsTable;
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


    public Image Base64ToImage(string base64String, string emp_code, string verification, string currentdate2)
    {
        // Convert base 64 string to byte[]

        Image image = null;
        String currentdate = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
        if (verification == "police_verification_form")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code + "_22" + ".png", image.RawFormat);
                log.Error("Android - Finally police verification form upload  photo save Sucessfull ....");
            }
        }
        else if (verification == "adhar_card_upload")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code + "_21" + ".png", image.RawFormat);
                log.Error("Android - Finally Aadhar card upload  photo save sucessfull ....");
            }
        }
        else if (verification == "Pan_card_upload")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code + "_2" + ".png", image.RawFormat);
                log.Error("Android - Finally Pan card upload photo save sucessfull ....");
            }

        }
        else if (verification == "Employee_photo_upload")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/Temp_images/") + emp_code + "_25" + ".png", image.RawFormat);
                log.Error("Android - Finally Employee Employee_photo_upload Photo upload  sucessfull ....");
            }

        }
        else if (verification == "Working_image")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                //image.Save(HttpContext.Current.Server.MapPath("~/attendance_images/") + emp_code + "_Working_" + currentdate2 + ".png", image.RawFormat);
                log.Error("Android - Finally Employee Employee_photo_upload Photo upload  sucessfull ....");
                image.Save(currentdate2, image.RawFormat);

                log.Error("Android - Finally Employee Working_image photo save  sucessfull ...." + currentdate2);
            }

        }
        else if (verification == "Attendances_intime")
        {
           // String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/attendance_images/") + emp_code + "_Intime_" + currentdate2 + ".png", image.RawFormat);
                log.Error("Android - Finally Employee Intime Attendances_intime photo save  sucessfull ....");

            }

        }
        else if (verification == "Attendances_outtime")
        {
           // String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/attendance_images/") + emp_code + "_Outtime_" + currentdate2 + ".png", image.RawFormat);
                log.Error("Android - Finally Employee Intime Attendance  sucessfull ....");

            }

        }
        else if (verification == "Attendances_cameraintime")
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    image.Save(HttpContext.Current.Server.MapPath("~/attendance_images/") + emp_code + "_cameraIntime_" + currentdate2 + ".png", image.RawFormat);
                    log.Error("Android - Finally Employee Intime Attendances_cameraintime photo save sucessfull ....");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error("Line Number : " + new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber());
            }
            finally
            {
                d.con.Close();
                log.Error("Android - Finally Attendances_cameraintime....");
            }

        }
        else if (verification == "Attendances_cameraouttime")
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    image = Image.FromStream(ms, true);
                    image.Save(HttpContext.Current.Server.MapPath("~/attendance_images/") + emp_code + "_camerouttime_" + currentdate2 + ".png", image.RawFormat);
                    String image_name = emp_code + "_camerouttime_" + currentdate2 + ".png";
                    d.operation("UPDATE pay_android_attendance_logs SET Camera_outtime_images='" + image_name + "' WHERE id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as t)");
                    log.Info("Android - Employee Attendances_cameraouttime photo save Successfull ....");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error("Line Number : " + new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber());
            }
            finally
            {
                d.con.Close();
                log.Error("Android - Finally Attendances_cameraouttime ....");
            }
        }
        else if (verification == "Tab_Employee_Attendances_IntimeImage")
        {
            // String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
               // image.Save(HttpContext.Current.Server.MapPath("~/tab_attendance_images/") + emp_code + "_Intime_" + currentdate2 + ".png", image.RawFormat);
                image.Save(HttpContext.Current.Server.MapPath("~/tab_attendance_images/") +  currentdate2 , image.RawFormat);
                log.Error("Android - Finally Tab_Employee_Attendances_IntimeImage  Attendances_intime photo save  sucessfull ....");

            }

        }
        else if (verification == "Tab_Employee_Attendances_OuttimeImage")
        {
            // String currentdate2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                // image.Save(HttpContext.Current.Server.MapPath("~/tab_attendance_images/") + emp_code + "_Intime_" + currentdate2 + ".png", image.RawFormat);
                image.Save(HttpContext.Current.Server.MapPath("~/tab_attendance_images/") + currentdate2, image.RawFormat);
                log.Error("Android - Finally Tab_Employee_Attendances_IntimeImage  Attendances_intime photo save  sucessfull ....");

            }

        }
        else if (verification == "Fire_Extinguisher_image")
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);
                image.Save(HttpContext.Current.Server.MapPath("~/fire_extinguisher_image/") + emp_code + "_Fire_Extinguisher_" + currentdate2 + ".png", image.RawFormat);
                log.Error("Android - Finally Employee Fire_Extinguisher_image photo save  sucessfull ....");
            }

        }


        return image;
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
        {
            return NumOfDay - NoOfSunday;
        }
        else
        { return NoOfSunday; }
    }

    private string chkday(string day)
    {
        if (day == "1") { return "01"; }
        else if (day == "2") { return "02"; }
        else if (day == "3") { return "03"; }
        else if (day == "4") { return "04"; }
        else if (day == "5") { return "05"; }
        else if (day == "6") { return "06"; }
        else if (day == "7") { return "07"; }
        else if (day == "8") { return "08"; }
        else if (day == "9") { return "09"; }
        return day;

    }
}


