
/// <summary>
/// Summary description for AttendanceRegisterBAL
/// </summary>
public class AttendanceRegisterBAL
{
    DAL ds = new DAL();

	public AttendanceRegisterBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int AttendanceInsert(string compcode, string unitcode, string empcode)//insert args
    {
        int res = 0;
        try
        {

            res = ds.operation("INSERT INTO pay_attendance_muster (COMP_CODE, UNIT_CODE, EMP_CODE) VALUES ('" + compcode + "','" + unitcode + "','" + empcode + "')");
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
    public int AttendanceUpdate(string compcode, string unitcode, string empcode, string DAY01, string DAY02, string DAY03, string DAY04, string DAY05, string DAY06, string DAY07, string DAY08, string DAY09, string DAY10, string DAY11, string DAY12, string DAY13, string DAY14, string DAY15, string DAY16, string DAY17, string DAY18, string DAY19, string DAY20, string DAY21, string DAY22, string DAY23, string DAY24, string DAY25, string DAY26, string DAY27, string DAY28, string DAY29, string DAY30, string DAY31, float totdayspresent, float totdaysabsent, float tothalfdays, float weeklyoff, float holidays, float totworkingdays)//update args
    {
        int res = 0;
        try
        {
            res = ds.operation("UPDATE pay_attendance_muster SET DAY01 ='" + DAY01 + "', DAY02 ='" + DAY02 + "', DAY03 ='" + DAY03 + "', DAY04 ='" + DAY04 + "', DAY05 ='" + DAY05 + "', DAY06 ='" + DAY06 + "', DAY07 ='" + DAY07 + "', DAY08 ='" + DAY08 + "', DAY09 ='" + DAY09 + "', DAY10 ='" + DAY10 + "', DAY11 ='" + DAY11 + "', DAY12 ='" + DAY12 + "', DAY13 ='" + DAY13 + "', DAY14 ='" + DAY14 + "', DAY15 ='" + DAY15 + "', DAY16 ='" + DAY16 + "', DAY17 ='" + DAY17 + "', DAY18 ='" + DAY18 + "', DAY19 ='" + DAY19 + "', DAY20 ='" + DAY20 + "', DAY21 ='" + DAY21 + "', DAY22 ='" + DAY22 + "', DAY23 ='" + DAY23 + "', DAY24 ='" + DAY24 + "', DAY25 ='" + DAY25 + "', DAY26 ='" + DAY26 + "', DAY27 ='" + DAY27 + "', DAY28 ='" + DAY28 + "', DAY29 ='" + DAY29 + "', DAY30 ='" + DAY30 + "', DAY31 ='" + DAY31 + "', TOT_DAYS_PRESENT =" + totdayspresent + ", TOT_DAYS_ABSENT =" + totdaysabsent + ", TOT_HALF_DAYS =" + tothalfdays + ", WEEKLY_OFF =" + weeklyoff + ", HOLIDAYS =" + holidays + ", TOT_WORKING_DAYS =" + totworkingdays + " WHERE COMP_CODE = '" + compcode + "' AND UNIT_CODE='" + unitcode + "' AND EMP_CODE='" + empcode + "'");//update command
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
}