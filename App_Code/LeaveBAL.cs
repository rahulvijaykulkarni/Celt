using System.Data;

/// <summary>
/// Summary description for LeaveBAL
/// </summary>
public class LeaveBAL
{
    DAL dlv = new DAL();
	public LeaveBAL()
	{
		//
        // TODO: Add constructor logic here  
		//
	}

    public int LeaveInsert(string LeaveID, string compcode, string leavetype, string gendar, string description, string no_of_leaves, string year)//insert agrs
    {
        int res = 0;
        try
        {
            res = dlv.operation("INSERT INTO pay_leave_type(Leave_Type_Id,comp_code,TYPE_OF_LEAVE,TYPE_OF_DESG,NO_OF_LEAVE,GENDAR,YEAR) VALUES('" + LeaveID + "','" + compcode + "','" + leavetype + "','" + description + "','" + no_of_leaves + "','" + gendar + "','" + year + "')");//insert command
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
   // public int LeaveUpdate(string compcode, string leavetype, string gendar, string description, string no_of_leaves, string year)//update args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = dlv.operation("UPDATE pay_leave_type SET TYPE_OF_LEAVE='" + leavetype + "',TYPE_OF_DESG='" + description + "' , NO_OF_LEAVE='" + no_of_leaves + "', GENDAR='" + gendar + "',YEAR='" + year + "' WHERE TYPE_OF_LEAVE='" + leavetype + "' AND comp_code='" + compcode + "'");//update command
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

    //public int LeaveDelete(string compcode, string LeaveID)//delete args
    //{
    //    int res = 0;
    //    try
    //    {
    //        res = dlv.operation("DELETE FROM pay_leave_type WHERE Leave_Type_Id='" + LeaveID + "' AND comp_code='" + compcode + "'");//delete command
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
    public DataSet LeaveSelect()
    {
        DataSet result;
        try
        {
            result = dlv.getData("SELECT * FROM pay_leave_type");//select command
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
