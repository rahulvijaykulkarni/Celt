using System;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;


public class Leave_Apply
{

    DAL dlv = new DAL();
	public Leave_Apply()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Leaveapplyinsert(string leaveId, string compcode, string employeename, string work_location, string manager, string leave_type, string balance_leave, string fromdate, string todate, string noof_leave, string reason, string Status, string status_comment)//insert agrs
    {
        int res = 0;
        try
        {
            res = dlv.operation("INSERT INTO pay_leave_transaction(LEAVE_ID,comp_code,EMP_NAME,WORK_LOCATION,MANAGER,LEAVE_TYPE,BALANCE_LEAVE,FROM_DATE,TO_DATE,NO_OF_DAYS,REASON,STATUS,STATUS_COMMENT) VALUES('" + leaveId + "','" + compcode + "','" + employeename + "','" + work_location + "','" + manager + "','" + leave_type + "','" + balance_leave + "','" + fromdate + "','" + todate + "','" + noof_leave + "','" + reason + "','" + Status + "','" + status_comment + "')");//insert command
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

    public DataSet LeaveSelect()
    {
        DataSet result;
        try
        {
            result = dlv.getData("SELECT * FROM pay_leave_transaction");//select command
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

    public void add_leaves()
    {
        try
        {
            DAL d = new DAL();
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string months = System.DateTime.Today.ToShortDateString();
            DateTime month = DateTime.ParseExact(months, sysFormat, CultureInfo.InvariantCulture);
            string curr_date = month.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string firstday = "01" + curr_date.Substring(2, 8);

            MySqlCommand cmd1 = new MySqlCommand("select emp_code, CL, PL, last_update_date from pay_leave_emp_balance where last_update_date < str_to_date('" + firstday + "','%d/%m/%Y')", dlv.con);
            dlv.conopen();
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                if (curr_date.Substring(3, 2) == "01")
                {
                    d.operation("update pay_leave_emp_balance set CL=6, PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "02")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 1, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "03")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "04")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "05")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 1, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "06")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "07")
                {
                    d.operation("update pay_leave_emp_balance set CL=6, PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "08")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 1, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "09")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "10")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "11")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 2, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }
                else if (curr_date.Substring(3, 2) == "12")
                {
                    d.operation("update pay_leave_emp_balance set PL = PL + 1, last_update_date = now() where emp_code = '" + dr1.GetValue(0).ToString() + "'");
                }

            }
        }
        catch (Exception eee)
        {
            throw eee;
        }
    }
    

}