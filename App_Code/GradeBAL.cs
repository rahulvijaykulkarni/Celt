using System.Data;
/// <summary>
/// Summary description for GradeBAL
/// </summary>
public class GradeBAL
{
    DAL dg=new DAL() ;
	public GradeBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GradeInsert(string compcode, string gradecode, string desc, string reporting_to, string loginperson, string date,string hours,string employee_type)//insert agrs
      {
       
        int res = 0;
        try
  
        {
            res = dg.operation("INSERT INTO pay_grade_master(comp_code,GRADE_CODE,GRADE_DESC, reporting_to,Login_Person,LastModifyDate,Working_Hours,employee_type) VALUES('" + compcode + "','" + gradecode + "','" + desc + "','" + reporting_to + "','" + loginperson + "','" + date + "','" + hours + "','" + employee_type + "')");//insert command
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

    public int GradeInsertlog(string compcode, string gradecode, string desc, string reporting_to, string loginperson, string date)//insert agrs
      {
       
        int res = 0;
        try
  
        {
            res = dg.operation("INSERT INTO pay_grade_master_log(comp_code,GRADE_CODE,GRADE_DESC, reporting_to,Login_Person,LastModifyDate) VALUES('" + compcode + "','" + gradecode + "','" + desc + "','" + reporting_to + "','" + loginperson + "','" + date + "')");//insert command
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
    //payment
    public int PaymentInsert(string compcode, string User_Name, string Amount, string Date,string Paid_Through,string Ref,string Notes)//insert agrs
    {

        int res = 0;
        try
        {                                                                                
            res = dg.operation("INSERT INTO pay_record_advance_payment (comp_code,User_Name,Amount, Date,Paid_Through,Ref,Notes) VALUES('" + compcode + "','" + User_Name + "','" + Amount + "',str_to_date('" + Date + "' ,'%d/%m/%Y'),'"+ Paid_Through+"','"+Ref +"','"+ Notes+"')");//insert command 
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
    //payment
    public DataSet PaymentSelect()
    {
        DataSet result;
        try
        {
            result = dg.getData("SELECT User_Name,Amount,Date ,Paid_Through,Ref,Notes FROM pay_record_advance_payment");//select command
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


    public int shiftInsert(string comp_code, string unit_code, string shift_name, string shift_from, string shift_to,string created_by)//insert agrs
    {

        int res = 0;
        try
        {
            res = dg.operation("INSERT INTO pay_shift_master(comp_code,unit_code,shift_name,shift_from, shift_to,created_by,created_date) VALUES('" + comp_code + "','" + unit_code + "','" + shift_name + "',STR_TO_DATE('" + shift_from + "', '%h:%i %p'),STR_TO_DATE('" + shift_to + "', '%h:%i %p'),'" + created_by + "',now())");//insert command
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


    
    public int shiftInsertrotation(string comp_code, string Schedule_Name, string Schedule_Frequency1, string Schedule_Frequency2, string Time_of_Schedule, string Shift_Span_From, string Applicable_For, string Shift_Rotation1, string Shift_Rotation2)//insert agrs
    {

        int res = 0;
        try
        {

            res = dg.operation("INSERT INTO pay_shiftrotation (comp_code,Schedule_Name,Schedule_Frequency1, Schedule_Frequency2,Time_of_Schedule,Shift_Span_From,Applicable_For,Shift_Rotation1,Shift_Rotation2) VALUES('" + comp_code + "','" + Schedule_Name + "','" + Schedule_Frequency1 + "','" + Schedule_Frequency2 + "',STR_TO_DATE('" + Time_of_Schedule + "', '%h:%i %p'),'" + Shift_Span_From + "','" + Applicable_For + "','" + Shift_Rotation1 + "','" + Shift_Rotation2 + "')");//insert command
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
    //public int GradeUpdate(string compcode, string gradecode, string desc, string reporting_to)//update args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = dg.operation("UPDATE pay_grade_master SET  GRADE_CODE='" + gradecode + "', GRADE_DESC='" + desc + "', REPORTING_TO='" + reporting_to + "' WHERE GRADE_CODE='" + gradecode + "' AND comp_code='" + compcode + "'");//update command
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
    //public int GradeDelete(string compcode, string gradecode)//delete args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = dg.operation("DELETE FROM pay_grade_master WHERE GRADE_CODE='" + gradecode + "' AND comp_code='" + compcode + "'");//delete command
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
    //public DataSet GradeSelect()
    //{
    //    DataSet result;
    //    try
    //    {
    //        result = dg.getData("SELECT GRADE_CODE,GRADE_DESC,Reporting_to FROM pay_grade_master");//select command
    //        return result;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //    finally
    //    {

    //    }
    //}

    public DataSet shiftSelectemapping(string comp_code)//insert agrs
    {
        DataSet result;
        try
        {
            //System.DateTime dt1 = System.DateTime.ParseExact(date_from, "dd/MM/yyyy", null);
            //System.DateTime dt2 = System.DateTime.ParseExact(date_to, "dd/MM/yyyy", null);
            //List<System.DateTime> datelist = new List<System.DateTime>();
            //while (dt1 <= dt2)
            //{
            //    datelist.Add(dt1);
            //    dt1 = dt1.AddDays(1);
            //}
            //System.DateTime[] dates = datelist.ToArray();
            result = dg.getData("SELECT applicable_for, shift_name1,date_from,date_to FROM pay_shiftmapping where comp_code='" + comp_code + "'");//select command

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

    public DataSet shiftSelecterotation(string comp_code)//insert agrs
    {
        DataSet result;
        try
        {

            result = dg.getData("SELECT Schedule_Name, Schedule_Frequency1,Schedule_Frequency2,DATE_FORMAT(Time_of_Schedule,'%h:%i %p')As Time_of_Schedule,Shift_Span_From,Applicable_For,Shift_Rotation1,Shift_Rotation2 FROM pay_shiftrotation where comp_code='" + comp_code + "'");//select command

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
    public string date_from { get; set; }

    public string date_to { get; set; }
}
