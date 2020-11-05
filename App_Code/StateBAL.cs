using System.Data;

/// <summary>
/// Summary description for StateBAL
/// </summary>
public class StateBAL
{
    DAL ds = new DAL();
	public StateBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //insert agrs
    public int StateInsert(string statecode, string statename, string txt_city)
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO pay_state_master(STATE_CODE,STATE_NAME,city) VALUES('" + statecode + "','" + statename + "','" + txt_city + "')");//insert command
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
    public int StateUpdate(string statecode, string statename, string newstatecode, string txt_city, string l_city)//update args
    {
        int res = 0;
        try
        {
            res = ds.operation("UPDATE pay_state_master SET STATE_CODE ='" + newstatecode + "',STATE_NAME='" + statename + "',city='" + txt_city + "' WHERE   STATE_CODE ='" + statecode + "' and city='" + l_city + "'");//update command
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
    public int StateDelete(string statecode,string city)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM pay_state_master WHERE STATE_CODE='"+statecode +"' and city='"+city+"'");//delete command
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
    public DataSet StateSelect()
    {
        DataSet result;
        try
        {
            result = ds.getData("SELECT * FROM pay_state_master");//select command
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

    public DataSet CompanyStateSelection(string compcode)
    {
        DataSet result;
        try
        {
            result = ds.getData("SELECT * FROM pay_state_master");//select command
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
