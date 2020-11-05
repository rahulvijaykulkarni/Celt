using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    DAL d = new DAL();
    string imageName ;
    static string getUrl;
    protected void Page_Load(object sender, EventArgs e)
    {

        txt_emp_name.Text = Session["USERNAME"].ToString();
        txt_unit_name.Text = Session["UNIT_NAME"].ToString();
        txt_employee_code.Text = Session["LOGIN_ID"].ToString();
     //   btnsave.Visible = false;
        if (!this.IsPostBack)
        {
            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                   
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                  //  string imageName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                    imageName = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                    string imagePath = string.Format("~/Captures/{0}.png", imageName);
                    File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = ResolveUrl(imagePath);
                    getUrl = imagePath.ToString();
                    //if (Session["CapturedImage"].ToString().Equals(""))
                    //{
                    //    btnsave.Visible = false;
                    //}
                    //else
                    //{
                    //    btnsave.Visible = true;
                    //}
                
                   
                }
            }
        }
    }

    private static byte[] ConvertHexToBytes(string hex)
    {
        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    [WebMethod(EnableSession = true)]
    public static string GetCapturedImage()
    {
        string url = HttpContext.Current.Session["CapturedImage"].ToString();
        HttpContext.Current.Session["CapturedImage"] = null;
        return url;
    }

    protected void btn_save_details(object Sender, EventArgs e) {

        string split_length = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        int date = split_length.ToString().IndexOf('/');
        int day = int.Parse(split_length.ToString().Substring(0, 2));
        int months = int.Parse(split_length.ToString().Substring(3, 2));
        int years = int.Parse(split_length.ToString().Substring(6, 4));

        string yesterday = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        int yesterdaydate = yesterday.ToString().IndexOf('/');
        int yesterdayday = int.Parse(yesterday.ToString().Substring(0, 2));
        int yesterdaymonths = int.Parse(yesterday.ToString().Substring(3, 2));
        int yesterdayyears = int.Parse(yesterday.ToString().Substring(6, 4));

        //string emp_code = emp_code1;


        //string unit_code = d.getsinglestring("SELECT unit_code FROM pay_android_attendance_logs WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "'");
        string id_get = d.getsinglestring("select  id from pay_android_attendance_logs where emp_code='" + Session["LOGIN_ID"].ToString() + "' and date(date_time)=CURDATE()");
        string unit_code = d.getsinglestring("SELECT unit_code FROM pay_employee_master WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "'");
        string shiftwisecheck = d.getsinglestring("select id from pay_android_attendance_logs where emp_code = '" + Session["LOGIN_ID"].ToString() + "' and date_time between str_to_date(concat(date_format(DATE_ADD(curdate(), INTERVAL -1 DAY),'%d/%m/%Y'),' 09:00PM'),'%d/%m/%Y %h:%i%p') and curdate() ");
        string shift_Attendances_outtime_images = d.getsinglestring("select `Attendances_outtime` from pay_android_attendance_logs where emp_code = '" + Session["LOGIN_ID"].ToString() + "' and date_time between str_to_date(concat(date_format(DATE_ADD(curdate(), INTERVAL -1 DAY),'%d/%m/%Y'),' 09:00PM'),'%d/%m/%Y %h:%i%p') and curdate() ");

        d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");


        if (id_get == "" && shiftwisecheck =="")
        {
           // string currentdate_out = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss", CultureInfo.InvariantCulture);
             string currentdate_in = DateTime.Now.ToString("dd_MM_yyyy_hh_mm", CultureInfo.InvariantCulture);
             string image_name_intime = txt_employee_code.Text.ToString() + "_Intime_" + currentdate_in + ".png";

            //string image_name_intime1 = txt_employee_code.Text.ToString() + "_Intime_" + currentdate_in + ".png";
           
            // System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
            System.IO.File.Copy(Server.MapPath(getUrl), Server.MapPath("~/attendance_images/") + image_name_intime);
            // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
            System.IO.File.Delete(Server.MapPath(getUrl));

            //d.operation("Insert into pay_android_attendance_logs (COMP_CODE,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Attendances_intime,Date_Time,Attendances_intime_images,Attendances_outtime_images) values ('" + Session["COMP_CODE"].ToString() + "','" + unit_code + "','" + Session["LOGIN_ID"].ToString() + "','123','123','123','123','123','front desk','" + Session["USERNAME"].ToString() + "',now(),now(),'" + image_name_intime + "','')");

            d.operation("Insert into pay_android_attendance_logs (COMP_CODE,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Attendances_intime,Date_Time,Attendances_intime_images,Attendances_outtime_images) select  comp_code ,unit_code , emp_code, '123','123','123','123','123','front desk', emp_name,now(),now(),'" + image_name_intime + "','' from pay_employee_master where `emp_code` = '" + Session["LOGIN_ID"].ToString() + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + Session["LOGIN_ID"].ToString() + "' and date(Date_Time)=curdate()) limit 1");


          //  d.operation("INSERT INTO pay_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintOTSundays(months, years, 2) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_ot_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

        }
        else{

             string currentdate_out = DateTime.Now.ToString("dd_MM_yyyy_hh_mm", CultureInfo.InvariantCulture);
             string image_name_out = txt_employee_code.Text.ToString() + "_Outtime_" + currentdate_out + ".png";
             string image_name_in = txt_employee_code.Text.ToString() + "_Intime_" + currentdate_out + ".png";

             //string image_name_intime1 = txt_employee_code.Text.ToString() + "_Outtime_" + currentdate_out + ".png";
             
           // d.operation("UPDATE pay_android_attendance_logs SET Attendances_outtime=DATE_ADD(now(), INTERVAL 330 MINUTE),Attendances_outtime_images='" + image_name_out + "' WHERE id = '"+id_get+"'");
           
            //time zone change
            if (shiftwisecheck == "")
            {
                System.IO.File.Delete(Server.MapPath("~/attendance_images/") + image_name_out);
                System.IO.File.Copy(Server.MapPath(getUrl), Server.MapPath("~/attendance_images/") + image_name_out);
                // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                System.IO.File.Delete(Server.MapPath(getUrl));

            d.operation("UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + image_name_out + "' WHERE id = '" + id_get + "'");

           // string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");
            //string tempflag12 = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");
            string tempflag = d.getsinglestring("select  android_att_flag from pay_unit_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and unit_code=(select unit_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");


            if (tempflag == "yes")
            {
               setAttendace(Session["LOGIN_ID"].ToString());

                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') AND EMP_CODE='" + Session["LOGIN_ID"].ToString() + "'");

            }
           
            
            }else {
                if (shift_Attendances_outtime_images != "")
                {
                    System.IO.File.Copy(Server.MapPath(getUrl), Server.MapPath("~/attendance_images/") + image_name_in);
                    // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                    System.IO.File.Delete(Server.MapPath(getUrl));

                    d.operation("Insert into pay_android_attendance_logs (COMP_CODE,UNIT_CODE,EMP_CODE,UNIT_LATITUDE,UNIT_LONGTUTDE,EMP_LATITUDE,EMP_LONGTUTDE,DISTANCES,ADDRESS,EMP_NAME,Attendances_intime,Date_Time,Attendances_intime_images,Attendances_outtime_images) select  comp_code ,unit_code , emp_code, '123','123','123','123','123','front desk', emp_name,now(),now(),'" + image_name_in + "','' from pay_employee_master where `emp_code` = '" + Session["LOGIN_ID"].ToString() + "' and emp_code not in (select emp_code from pay_android_attendance_logs where emp_code='" + Session["LOGIN_ID"].ToString() + "' and date(Date_Time)=curdate()) limit 1");

                }else {
                    System.IO.File.Delete(Server.MapPath("~/attendance_images/") + image_name_out);
                    System.IO.File.Copy(Server.MapPath(getUrl), Server.MapPath("~/attendance_images/") + image_name_out);
                    // System.IO.File.Copy(Server.MapPath("~/Temp_images/") + path, Server.MapPath("~/Temp_images/") + newpath);
                    System.IO.File.Delete(Server.MapPath(getUrl));

                    d.operation("UPDATE pay_android_attendance_logs SET Attendances_outtime=now(),Attendances_outtime_images='" + image_name_out + "' WHERE id = '" + shiftwisecheck + "'");

                    string tempflag = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");
                    //string tempflag12 = d.getsinglestring("select  android_att_flag from pay_client_master where comp_code=(select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') and client_code=(select client_code from  pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "')");

                    if (tempflag == "yes")
                    {
                        setAttendace(Session["LOGIN_ID"].ToString());
                        d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + yesterday + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + Session["LOGIN_ID"].ToString() + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + yesterdayyears + "-" + yesterdaymonths + "-" + yesterdayday + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(yesterdayday.ToString()) + " = t.attend where month= '" + yesterdaymonths + "' AND YEAR='" + yesterdayyears + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + Session["LOGIN_ID"].ToString() + "') AND EMP_CODE='" + Session["LOGIN_ID"].ToString() + "'");

                    }
                
                }
              }
            
            
        }
        
        Session["USERID"] = "";
        Session["LOGIN_ID"] = "";
        Session["USERNAME"] = "";
        Session["CHANGE_PASS"] = "";
        Session["ROLE"] = "";
        Session["comp_code"] = "";
        Session["UNIT_CODE"] = "";
        Session["COMP_NAME"] = "";
        Session["UNIT_NAME"] = "";
        Response.Redirect("Marvel_login.aspx", false);

    }


    public void setAttendace(string emp_code1) {

        string split_length = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        int date = split_length.ToString().IndexOf('/');
        int day = int.Parse(split_length.ToString().Substring(0, 2));
        int months = int.Parse(split_length.ToString().Substring(3, 2));
        int years = int.Parse(split_length.ToString().Substring(6, 4));
        string emp_code = emp_code1;
        string attendances_click = "Attendances_outtime";
        // log.Error("Attendances Date format display split_length:" + split_length + " day:" + day + " months:" + months + " years:" + years);
        // log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
        // log.Error("Android - insert query " + "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 1) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

        // vinod sir said comment that query
        // d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 2) + " FROM pay_employee_master WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

        // use only sunday count query
        //d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 3) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");
        //String Query = "INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))";
        // P Falg replays A
        //d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " FROM pay_employee_master inner join pay_attendance_muster on pay_employee_master.emp_code=pay_attendance_muster.emp_code and pay_employee_master.comp_code=pay_attendance_muster.comp_code and pay_employee_master.unit_code=pay_attendance_muster.unit_code WHERE (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND pay_employee_master.EMP_CODE NOT IN (SELECT EMP_CODE FROM pay_attendance_muster  WHERE COMP_CODE in (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_attendance_muster.MONTH = '" + months + "' AND pay_attendance_muster.YEAR='" + years + "') AND pay_employee_master.COMP_CODE in (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') AND pay_employee_master.UNIT_CODE in (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "'))");

        // Attendaces present and absent query
           d.operation("INSERT INTO pay_attendance_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintSundays(months, years, 3) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintSundays(months, years, 4) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_attendance_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");

        // Over Time query

           d.operation("INSERT INTO pay_daily_ot_muster (COMP_CODE,EMP_CODE,UNIT_CODE,MONTH,YEAR,TOT_WORKING_DAYS,WEEKLY_OFF,TOT_DAYS_PRESENT," + d.PrintOTDaySundays(months, years, 1) + ") (SELECT pay_employee_master.COMP_CODE,pay_employee_master.EMP_CODE,pay_employee_master.UNIT_CODE," + months + "," + years + "," + CountDay(months, years, 1) + "," + CountDay(months, years, 3) + "," + CountDay(months, years, 1) + "," + d.PrintOTDaySundays(months, years, 2) + " from `pay_employee_master` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code = (select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') and employee_type = 'Permanent' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null) AND `EMP_CODE` NOT IN (SELECT `emp_code` FROM `pay_daily_ot_muster` WHERE `comp_code` = (select comp_CODE from pay_employee_master where emp_code='" + emp_code + "') and unit_code=(select unit_CODE from pay_employee_master where emp_code='" + emp_code + "') AND `MONTH` = '" + months + "' AND `YEAR` = '" + years + "'))");


        string temp1 = d.getsinglestring("select trim(attendance_id) from pay_employee_master where emp_code = '" + emp_code + "'");
        //log.Error("Android - temp1" + temp1);
        if (attendances_click == "Attendances_outtime")
        {
            if (temp1 == "8")
            {
                //string query = "Android - 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                //log.Error("Android - 8 hour:" + query);
                // log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
               
                
                //d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                
                
                //log.Error("Android -Attendances_outtime 8 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                //add attendances_intime and attendances_outtime
              //attendaces query
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN '1' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN '2' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN '3' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN '4' ELSE '5' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                //OT query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
               
                // old query
                //d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR))*1000) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR))*1000) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                //time_to_sec convert query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR),'%H.%i') ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

            }
            else if (temp1 == "12")
            {
                // log.Error("Android -Attendances_outtime 12 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
              //aatendances query
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('6:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                // OT query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 12 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 12 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                
                //old query
                //d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 12 HOUR))*1000) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('12:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 12 HOUR))*1000) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                ////time_to_sec convert query
                //d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 12 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('12:00', '%H:%i') THEN (time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 12 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 12 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('12:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 12 HOUR),'%H.%i') ELSE '0' END as attend  from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

            
            }
            else
            {
                // log.Error("Android -Attendances_outtime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                //Attendances query
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(Attendances_outtime,Attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                // OT Query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                //old query
                //d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR))*1000) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN (UNIX_TIMESTAMP(DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR))*1000) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                //time_to_sec convert query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR),'%H.%i') ELSE '0' END as attend  from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
            
           
            }
        }
        else if (attendances_click == "Attendances_cameraouttime")
        {
            if (temp1 == "8")
            {
                //string query12 = "Android - camera 8 hour:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'";
                //log.Error("Android - camera 8 hour:" + query12);
                // log.Error("Android -Attendances_cameraouttime 8 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                //attendances query
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                // OT query
               
                // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                //time_to_sec convert query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR),'%H.%i') ELSE '0' END as attend  from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

            }
            else if (temp1 == "12")
            {
                //log.Error("Android -Attendances_cameraouttime 12 hour pay_attendance_muster:" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                // Attendances query
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('6:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                //OT query
              //  d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 12 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 12 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                //time_to_sec convert query
                // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 12 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('12:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 12 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('12:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 12 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('12:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 12 HOUR),'%H.%i') ELSE '0' END as attend  from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

            }
            else
            {
                //  log.Error("Android -Attendances_cameraouttime 0 hour pay_attendance_muster :" + "update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'F' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
                // attendances quey
                d.operation("update pay_attendance_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN 'W' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('4:00', '%H:%i') THEN 'HD' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN 'P' WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('4:00', '%H:%i') THEN 'HD' ELSE 'A' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_attendance_muster.DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                // Ot query
               // d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '00:00:00' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(CAMERA_outtime,CAMERA_intime), INTERVAL 8 HOUR) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN DATE_SUB(TIMEDIFF(attendances_outtime,attendances_intime), INTERVAL 8 HOUR) ELSE '00:00:00' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");
              
                //time_to_sec convert query
                //d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR)) WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_to_sec((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR)) ELSE '0' END as attend from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

                d.operation("update pay_daily_ot_muster, (select CASE WHEN dayofweek(str_to_date('" + split_length + "','%d/%m/%Y')) = 1 THEN '0' WHEN TIMEDIFF(CAMERA_outtime,CAMERA_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`CAMERA_outtime`, `CAMERA_intime`) - INTERVAL 8 HOUR), '%H.%i') WHEN TIMEDIFF(attendances_outtime,attendances_intime) > time_format('8:00', '%H:%i') THEN time_format((TIMEDIFF(`attendances_outtime`, `attendances_intime`) - INTERVAL 8 HOUR),'%H.%i') ELSE '0' END as attend  from pay_android_attendance_logs where id IN (SELECT max(id) FROM (SELECT id FROM pay_android_attendance_logs WHERE emp_code='" + emp_code + "') as a) and DATE_FORMAT(Date_time,'%Y-%m-%d') = DATE_FORMAT('" + years + "-" + months + "-" + day + "','%Y-%m-%d')) t set pay_daily_ot_muster.OT_DAILY_DAY" + chkday(day.ToString()) + " = t.attend where month= '" + months + "' AND YEAR='" + years + "' AND COMP_CODE = (select comp_code from pay_employee_master where emp_code='" + emp_code + "') AND UNIT_CODE=(select UNIT_CODE from pay_employee_master where emp_code='" + emp_code + "') AND EMP_CODE='" + emp_code + "'");

            }

        }
    }


    protected void btn_close_details(object sender , EventArgs e) {

        Session["USERID"] = "";
        Session["LOGIN_ID"] = "";
        Session["USERNAME"] = "";
        Session["CHANGE_PASS"] = "";
        Session["ROLE"] = "";
        Session["comp_code"] = "";
        Session["UNIT_CODE"] = "";
        Session["COMP_NAME"] = "";
        Session["UNIT_NAME"] = "";
        Response.Redirect("Marvel_login.aspx", false);
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

    protected void btn_checking_details(object sender, EventArgs e) {
       
        string intime = d.getsinglestring("select  TIMEDIFF(Attendances_outtime,Attendances_intime) as tmdiff from pay_android_attendance_logs where emp_code='" + Session["LOGIN_ID"].ToString() + "'  and date(Date_Time)=curdate()");

       // TimeSpan t1 = TimeSpan.Parse(intime);
       // TimeSpan t2 = TimeSpan.Parse("08:00:00");
        //TimeSpan t3 = t1.Subtract(t2);

        string bb = "1571362245000";
        string aa="07:00:45";

        TimeSpan d1 = TimeSpan.Parse(aa.ToString());
        long nMilliseconds = 
                     d1.Hours * 60 * 60 * 1000 +
                     d1.Minutes * 60 * 1000 +
                     d1.Seconds * 1000 +
                     d1.Milliseconds;

        // string aa = +year1 + "-" + month1 + "-0" + (datecommon + i) + " " + txt_day1;

        //if (t3 < TimeSpan.Parse("08:00:00") && t3.Ticks > -1 )
        //{

            

        //}
        //else {

        //    string t12 = "0";
        //}



    }
}