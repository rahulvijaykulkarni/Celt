using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for CompanyBAL
/// </summary>
public class CompanyBAL
{
    DAL dco = new DAL();
	public CompanyBAL()
	{
		//
		//
	}
    public int CompanyInsert(string cmpcode, string cmpname, string address1, string address2, string city, string state, string txt_pin, string pfregno, string pfregoffice, string esicregno, string cmppanno, string cmptanno, string servicetaxregno, string empseriesinit, string website, string contact_no, string cin_no, string loginperson, string date, string file_no, string reporting,string sachouskeeping,string sacsecurity,string flag ,string payprono)//insert agrs
    {
        int res = 0;
        try
        {
            if (flag.Equals("0"))
            {
                res = dco.operation("INSERT INTO pay_company_master_approval(comp_code,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,pin,PF_REG_NO,PF_REG_OFFICE,ESIC_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO, SERVICE_TAX_REG_NO, EMP_SERIES_INIT,COMPANY_WEBSITE,COMPANY_CONTACT_NO,COMPANY_CIN_NO,Login_Person,LastModifyDate,file_no,approval,housekeeiing_sac_code,Security_sac_code) VALUES('" + cmpcode + "','" + cmpname + "','" + address1 + "','" + address2 + "','" + city + "','" + state + "','" + txt_pin + "','" + pfregno + "','" + pfregoffice + "','" + esicregno + "','" + cmppanno + "','" + cmptanno + "','" + servicetaxregno + "','" + empseriesinit + "','" + website + "','" + contact_no + "','" + cin_no + "','" + loginperson + "','" + date + "','" + file_no + "', '" + reporting + "','" + sachouskeeping + "','" + sacsecurity + "')");//insert command
            }
            else
            {
                res = dco.operation("INSERT INTO pay_company_master(comp_code,COMPANY_NAME,ADDRESS1,ADDRESS2,CITY,STATE,pin,PF_REG_NO,PF_REG_OFFICE,ESIC_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO, SERVICE_TAX_REG_NO, EMP_SERIES_INIT,COMPANY_WEBSITE,COMPANY_CONTACT_NO,COMPANY_CIN_NO,Login_Person,LastModifyDate,file_no,approval,housekeeiing_sac_code,Security_sac_code,paypro_no) VALUES('" + cmpcode + "','" + cmpname + "','" + address1 + "','" + address2 + "','" + city + "','" + state + "','" + txt_pin + "','" + pfregno + "','" + pfregoffice + "','" + esicregno + "','" + cmppanno + "','" + cmptanno + "','" + servicetaxregno + "','" + empseriesinit + "','" + website + "','" + contact_no + "','" + cin_no + "','" + loginperson + "','" + date + "','" + file_no + "', '" + reporting + "','" + sachouskeeping + "','" + sacsecurity + "','" + payprono + "')");//insert command }
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
    
    //---------------update query-------------------------------------------------
    public int CompanyUpdate(string cmpcode, string cmpname, string address1, string address2, string city, string state, string pfregno, string pfregoffice, string esicregno, string cmppanno, string cmptanno, string servicetaxregno, string website, string contact_no, string cin_no, string txt_pin, string loginperson, string date, string file_no, string reason, string reporting,string sachouskeeping,string sacsecurity,string paypro_no)//update args
    {
        int res = 0; 
        try
        {
            res = dco.operation("UPDATE pay_company_master SET COMPANY_NAME='" + cmpname + "',ADDRESS1='" + address1 + "',ADDRESS2='" + address2 + "',CITY='" + city + "',STATE='" + state + "',PF_REG_NO='" + pfregno + "',PF_REG_OFFICE='" + pfregoffice + "',ESIC_REG_NO='" + esicregno + "',COMPANY_PAN_NO='" + cmppanno + "',COMPANY_TAN_NO='" + cmptanno + "', SERVICE_TAX_REG_NO='" + servicetaxregno + "',COMPANY_WEBSITE='" + website + "',COMPANY_CONTACT_NO='" + contact_no + "',COMPANY_CIN_NO='" + cin_no + "',pin='" + txt_pin + "',Login_Person='" + loginperson + "',LastModifyDate='" + date + "',file_no='" + file_no + "',comments= concat(IFNULL(comments,''),'" + reason + " ON-',now(),'@#$%'), approval='" + reporting + "',housekeeiing_sac_code='" + sachouskeeping + "',Security_sac_code='" + sacsecurity + "' ,paypro_no ='" + paypro_no + "' WHERE comp_code='" + cmpcode + "' and (approval='' || approval is null)");//update command
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

    public int CompanyUpdatelog(string cmpcode,  string loginperson, string date)
    {
        int res = 0; 
        try
        {
            res = dco.operation("UPDATE pay_company_master_log SET Login_Person='" + loginperson + "',LastModifyDate='" + date + "'  WHERE comp_code='" + cmpcode + "'");//update command
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
    public int CompanyDelete(string cmpcode)//delete args
    {
        int res = 0;
        try
        {
            res = dco.operation("DELETE FROM pay_company_master WHERE comp_code='"+cmpcode +"'");//delete command
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
