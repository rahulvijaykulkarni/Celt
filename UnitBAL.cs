using System.Data;

/// <summary>
/// Summary description for UnitBAL
/// </summary>
public class UnitBAL
{
    DAL du = new DAL();
	public UnitBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    //-----------insert log query-----------------------------------------------------

    public int UnitInsert(string compcode, string unitcode, string unitname, string unitaddress1, string unitaddress2, string unitcity, string statename, string lattitude, string longitude, string area, string zone, string clientcode, string invoice, string emailid, string opertionname, string opertionmobile, string opertionemailid, string financename, string financemobile, string financemailid, string locationname, string locationmobile, string locationmailid, string othername, string othermobile, string othermailid, string designation, string txt_zone1, string reporting, string pincode, string admin_name, string admin_mob, string admin_email, string location_head_title, string operation_head_title, string finance_head_title, string admin_head_title, string other_head_title, string clienrbranchcode, string opuscode, string pmobileno1, string pmobileno2, string pmobileno3, string pmobileno4, string pmobileno5, string pemailid1, string pemailid2, string pemaili3, string pemailid4, string pemailid5, string pbirthdate1, string pbirthdate2, string pbirthdate3, string pbirthdate4, string pbirthdate5, string abirthdate1, string abirthdate2, string abirthdate3, string abirthdate4, string abirthdate5, string count, string branchcost_centre_code, string locationtype, string pccode, string distictivecode, string branch_type, string material_area,string branch_status,string labour_office,string Budget_Materials)//insert agrs
    {
        int res = 0;

   
        try
        {
            //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              float leavepercentage,string leaveformula,int headcode,         string monthcalc,string attbonus,float skilled,float unskilled,float semiskilled,float supervisor,float other,int attheadcode )//insert agrs
            res = du.operation("INSERT INTO pay_unit_master(comp_code, UNIT_CODE, UNIT_NAME, UNIT_ADD1, UNIT_ADD2, UNIT_CITY, STATE_NAME,unit_Lattitude,unit_Longtitude,unit_distance,ZONE,Client_code,file_no,UNIT_EMAIL_ID,OperationHead_Name,OperationHead_Mobileno,OperationHead_EmailId,FinanceHead_Name,FinanceHead_Mobileno,FinanceHead_EmailId,LocationHead_Name,LocationHead_mobileno,LocationHead_Emailid,OtherHead_Name,OtherHead_Monileno,OtherHead_Emailid,Designation,txt_zone,approval,PINCODE,adminhead_name ,adminhead_mobile,adminhead_email,location_head_title,operation_head_title,finance_head_title,admin_head_title,other_head_title,Client_branch_code,OPus_NO,p_mobileno1,p_mobileno2,p_mobileno3,p_mobileno4,p_mobileno5,p_emailid1,p_emailid2,p_emailid3,p_emailid4,p_emailid5,p_birthdate1,p_birthdate2,p_birthdate3,p_birthdate4,p_birthdate5,p_anniversaryate1,p_anniversaryate2,p_anniversaryate3,p_anniversaryate4,p_anniversaryate5,Emp_count,branch_cost_centre_code,location_type,pc_code,distictive_code,branch_type,material_area,branch_status,labour_office,Budget_Materials) VALUES ('" + compcode + "','" + unitcode + "','" + unitname + "','" + unitaddress1 + "','" + unitaddress2 + "','" + unitcity + "','" + statename + "','" + lattitude + "','" + longitude + "','" + area + "','" + zone + "','" + clientcode + "','" + invoice + "','" + emailid + "','" + opertionname + "','" + opertionmobile + "','" + opertionemailid + "','" + financename + "','" + financemobile + "','" + financemailid + "','" + locationname + "','" + locationmobile + "','" + locationmailid + "','" + othername + "','" + othermobile + "','" + othermailid + "','" + designation + "','" + txt_zone1 + "','" + reporting + "','" + pincode + "','" + admin_name + "','" + admin_mob + "','" + admin_email + "','" + location_head_title + "','" + operation_head_title + "','" + finance_head_title + "','" + admin_head_title + "','" + other_head_title + "','" + clienrbranchcode + "','" + opuscode + "','" + pmobileno1 + "','" + pmobileno2 + "','" + pmobileno3 + "','" + pmobileno4 + "','" + pmobileno5 + "','" + pemailid1 + "','" + pemailid2 + "','" + pemaili3 + "','" + pemailid4 + "','" + pemailid5 + "','" + pbirthdate1 + "','" + pbirthdate2 + "','" + pbirthdate3 + "','" + pbirthdate4 + "','" + pbirthdate5 + "','" + abirthdate1 + "','" + abirthdate2 + "','" + abirthdate3 + "','" + abirthdate4 + "','" + abirthdate5 + "','" + count + "','" + branchcost_centre_code + "','" + locationtype + "','" + pccode + "','" + distictivecode + "','" + branch_type + "','" + material_area + "','" + branch_status + "','" + labour_office + "','" + Budget_Materials + "')");//insert command
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
    //---------------update query-------------------------------------------------

    public int UnitUpdate(string compcode, string unitcode, string unitname, string unitaddress1, string unitaddress2, string unitcity, string statename, string lattitude, string longitude, string area, string zone, string clientcode, string invoice, string emailid, string opertionname, string opertionmobile, string opertionemailid, string financename, string financemobile, string financemailid, string locationname, string locationmobile, string locationmailid, string othername, string othermobile, string othermailid, string designation, string txt_zone1, string reason, string reporting, string pincode, string admin_name, string admin_mob, string admin_email, string location_head_title, string operation_head_title, string finance_head_title, string admin_head_title, string other_head_title, string clienrbranchcode, string opuscode, string pmobileno1, string pmobileno2, string pmobileno3, string pmobileno4, string pmobileno5, string pemailid1, string pemailid2, string pemailid3, string pemailid4, string pemailid5, string pbirthdate1, string pbirthdate2, string pbirthdate3, string pbirthdate4, string pbirthdate5, string abirthdate1, string abirthdate2, string abirthdate3, string abirthdate4, string abirthdate5, string branchcost_centre_code, string locationtype, string pccode, string distictivecode, string branch_type, string material_area, string status_flag, string labour_office,string Budget_Materials)//update args
    {
        int res = 0;
        try
        {
            res = du.operation("UPDATE pay_unit_master SET  UNIT_NAME = '" + unitname + "', UNIT_ADD1 = '" + unitaddress1 + "',UNIT_ADD2 = '" + unitaddress2 + "', UNIT_CITY = '" + unitcity + "', STATE_NAME = '" + statename + "', unit_Lattitude = '" + lattitude + "',unit_Longtitude='" + longitude + "',unit_distance='" + area + "' ,ZONE='" + zone + "',client_code='" + clientcode + "' ,file_no=' " + invoice + "',UNIT_EMAIL_ID='" + emailid + "',OperationHead_Name='" + opertionname + "',OperationHead_Mobileno='" + opertionmobile + "',OperationHead_EmailId='" + opertionemailid + "',FinanceHead_Name='" + financename + "',FinanceHead_Mobileno='" + financemobile + "',FinanceHead_EmailId='" + financemailid + "',LocationHead_Name='" + locationname + "',LocationHead_mobileno='" + locationmobile + "',LocationHead_Emailid='" + locationmailid + "',OtherHead_Name='" + othername + "',OtherHead_Monileno='" + othermobile + "',OtherHead_Emailid='" + othermailid + "',Designation='" + designation + "' ,txt_zone='" + txt_zone1 + "',comments= concat(IFNULL(comments,''),'" + reason + " ON-',now(),'@#$%'), approval='" + reporting + "',PINCODE='" + pincode + "',adminhead_name='" + admin_name + "',adminhead_mobile='" + admin_mob + "',adminhead_email='" + admin_email + "',location_head_title='" + location_head_title + "',operation_head_title='" + operation_head_title + "',finance_head_title='" + finance_head_title + "',admin_head_title='" + admin_head_title + "',other_head_title='" + other_head_title + "',Client_branch_code='" + clienrbranchcode + "',OPus_NO='" + opuscode + "',p_mobileno1='" + pmobileno1 + "',p_mobileno2='" + pmobileno2 + "',p_mobileno3='" + pmobileno3 + "',p_mobileno4='" + pmobileno4 + "',p_mobileno5='" + pmobileno5 + "',p_emailid1='" + pemailid1 + "',p_emailid2='" + pemailid2 + "',p_emailid3='" + pemailid3 + "',p_emailid4='" + pemailid4 + "',p_emailid5='" + pemailid5 + "',p_birthdate1='" + pbirthdate1 + "',p_birthdate2='" + pbirthdate2 + "',p_birthdate3='" + pbirthdate3 + "',p_birthdate4='" + pbirthdate4 + "',p_birthdate5='" + pbirthdate5 + "',p_anniversaryate1='" + abirthdate1 + "',p_anniversaryate2='" + abirthdate2 + "',p_anniversaryate3='" + abirthdate3 + "',p_anniversaryate4='" + abirthdate4 + "',p_anniversaryate5='" + abirthdate5 + "',branch_cost_centre_code='" + branchcost_centre_code + "',location_type='" + locationtype + "',pc_code='" + pccode + "',distictive_code='" + distictivecode + "',branch_type='" + branch_type + "',material_area='" + material_area + "',branch_status='" + status_flag + "',labour_office='" + labour_office + "',Budget_Materials = '" + Budget_Materials + "' WHERE UNIT_CODE = '" + unitcode + "' AND comp_code='" + compcode + "' ");//update command
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
    public int UnitUpdatelog(string compcode, string unitcode, string loginperson, string date)//update args
    {
        int res = 0;
        try
        {
            res = du.operation("UPDATE pay_unit_master_log SET Login_Person='" + loginperson + "',LastModifyDate='" + date + "'  WHERE UNIT_CODE = '" + unitcode + "' AND comp_code='" + compcode + "' ");//update command
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
    //-------------delete query---------------------------------------------------
    public int UnitDelete(string compcode, string unitcode)//delete args
    {
        int res = 0;
        try
        {
            res = du.operation("DELETE FROM pay_unit_master WHERE UNIT_CODE='" + unitcode + "' AND comp_code='" + compcode + "'");//delete command
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
    //----------select query------------------------------------------
    public DataSet UnitSelect(string comp_code, string client_name,string emp_code)
    {
        DataSet result;
        try
        {
            
          //  result = du.getData("SELECT pay_unit_master.comp_code, pay_unit_master.UNIT_CODE,(pay_client_master.CLIENT_NAME) as client_code,STATE_NAME,Concat(STATE_NAME,'-',UNIT_NAME,'-',UNIT_ADD1) as UNIT_NAME, UNIT_ADD1, UNIT_ADD2,  UNIT_CITY,Emp_count FROM pay_unit_master INNER JOIN pay_client_master ON pay_unit_master.CLIENT_CODE = pay_client_master.CLIENT_CODE AND pay_unit_master.comp_code = pay_client_master.comp_code where pay_unit_master.comp_code = '" + comp_code + "'");//select command
          //  result = du.getData("SELECT `comp_code`,`UNIT_CODE`,`client_code`,`STATE_NAME`,`UNIT_NAME`,`UNIT_ADD1`,`UNIT_ADD2`,`UNIT_CITY`, IFNULL(`Emp_count`,0) as 'Emp_count', IFNULL((`Emp_count`- employee_assign),0) as 'File_No' FROM (SELECT `pay_unit_master`.`comp_code`, `pay_unit_master`.`UNIT_CODE`,(`pay_client_master`.`CLIENT_NAME`) AS 'client_code',`STATE_NAME`, CONCAT(`STATE_NAME`, '-', `UNIT_NAME`, '-', `UNIT_ADD1`) AS 'UNIT_NAME',`UNIT_ADD1`,`UNIT_ADD2`,`UNIT_CITY`,`Emp_count`,`total_employee`,(`total_employee` - SUM(`emp_count`)) AS 'diff',(SELECT COUNT(`EMP_CODE`) FROM `pay_employee_master` WHERE `unit_code` = `pay_unit_master`.`unit_code` AND `comp_code` = '" + comp_code + "' AND `Employee_type` != 'Reliever'   AND `LEFT_DATE` is NUll ) AS 'employee_assign'  FROM `pay_unit_master` INNER JOIN `pay_client_master` ON `pay_unit_master`.`CLIENT_CODE` = `pay_client_master`.`CLIENT_CODE` AND `pay_unit_master`.`comp_code` = `pay_client_master`.`comp_code` WHERE `pay_unit_master`.`comp_code` = '" + comp_code + "' GROUP BY `pay_unit_master`.`unit_code`) AS branch_grid");//select command
            result = du.getData("SELECT `comp_code`, `UNIT_CODE`, `client_code`, STATE_NAME, `UNIT_NAME`, `UNIT_ADD1`, `UNIT_ADD2`, `UNIT_CITY`, IFNULL(`Emp_count`, 0) AS 'Emp_count', IFNULL((`Emp_count` - `employee_assign`), 0) AS 'File_No',branch_status as 'branch_status'  FROM (SELECT `pay_unit_master`.`comp_code`, `pay_unit_master`.`UNIT_CODE`, (`pay_client_master`.`CLIENT_NAME`) AS 'client_code', pay_unit_master.`STATE_NAME` AS 'STATE_NAME', CONCAT(pay_unit_master.`STATE_NAME`, '-', pay_unit_master.`UNIT_NAME`, '-', pay_unit_master.`UNIT_ADD1`) AS 'UNIT_NAME', `UNIT_ADD1`, `UNIT_ADD2`, `UNIT_CITY`, `Emp_count`, `total_employee`, (`total_employee` - SUM(`emp_count`)) AS 'diff',case when branch_status=0 then 'Active' else 'Close' end as 'branch_status', (SELECT COUNT(`EMP_CODE`) FROM `pay_employee_master` WHERE pay_unit_master.`branch_status`='0' and `unit_code` = `pay_unit_master`.`unit_code` AND `comp_code` = '" + comp_code + "' AND `Employee_type` != 'Reliever' AND `LEFT_DATE` IS NULL) AS 'employee_assign' FROM `pay_unit_master` INNER JOIN `pay_client_master` ON `pay_unit_master`.`CLIENT_CODE` = `pay_client_master`.`CLIENT_CODE` AND `pay_unit_master`.`comp_code` = `pay_client_master`.`comp_code` INNER JOIN `pay_client_state_role_grade` ON `pay_unit_master`.`CLIENT_CODE` = `pay_client_state_role_grade`.`CLIENT_CODE` AND `pay_unit_master`.`comp_code` = `pay_client_master`.`comp_code` AND pay_unit_master.UNIT_CODE = pay_client_state_role_grade.UNIT_CODE  WHERE  `pay_unit_master`.`comp_code` = '" + comp_code + "' AND (`pay_client_state_role_grade`.`emp_code` IN (" + emp_code + ")) GROUP BY `pay_unit_master`.`unit_code`) AS branch_grid");//select  MD change command
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
