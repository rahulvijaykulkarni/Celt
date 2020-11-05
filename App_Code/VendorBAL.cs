using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for VendorBAL
/// </summary>
public class VendorBAL
{
    DAL dv = new DAL();
	public VendorBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int VendorInsert(string compcode, string vendid, string vendname, string vendadd1, string txtvendorphone1, string txtvendorphone2, string txtvendordesignation, string txt_gst, string txtvendorpan, string txtvendorlst, string txtvendorcst, string txtvendortin, string txtvendorlbt, string txtvendorservictax, string txtvendoradd2, string ddlstatus, string ddltype, string txtvendortotaldues, string txtopeningbalance, string txtbillattention, string txtbilladdress, string txtbillcity, string txtbillstate, string txtbillzipcode, string txtbillcountry, string txtbillfax, string txtsattention, string txtsaddress, string txtscity, string txtssstate, string txtszipcode, string txtscountry, string txtsfax,string vendortype,string contperson1,string contperson2,string emailid,string agrementserve,string startdate,string enddate,string saccode,string hsmcode,string bankaccountname,string accountnum,string ifsccode,string bankname,string credit_period,string registrationno)//insert agrs
    {
        int res = 0;
        try
        {
            //                                                                                                                                                                                                                                                                              
            res = dv.operation("INSERT INTO PAY_VENDOR_MASTER(COMP_CODE,VEND_ID,VEND_NAME,VEND_ADD1,PHONE1,PHONE2,DESIGNATION,GST,PAN_NO,LST_NO,CST_NO,TIN_NO,LBT_NO,SERVICE_TAX_NO,VEND_ADD2,ACTIVE_STATUS,TYPE,TOTAL_DUES,OPENING_BALANCE,txtbillattention,txtbilladdress,txtbillcity,txtbillstate,txtbillzipcode,txtbillcountry,txtbillfax,txtsattention,txtsaddress,txtscity,txtssstate,txtszipcode,txtscountry,txtsfax,vendor_type ,contact_person_1,contact_person_2,vendor_email_id,area_to_served,agrement_start_date,agrement_end_date,sac_code,hsm_code,bank_account_name,account_number,ifsc_code,bank_name,CREDIT_PERIOD,comp_registration_num) VALUES('" + compcode + "','" + vendid + "','" + vendname + "','" + vendadd1 + "','" + txtvendorphone1 + "','" + txtvendorphone2 + "','" + txtvendordesignation + "','" + txt_gst + "','" + txtvendorpan + "','" + txtvendorlst + "','" + txtvendorcst + "','" + txtvendortin + "','" + txtvendorlbt + "','" + txtvendorservictax + "','" + txtvendoradd2 + "','" + ddlstatus + "','" + ddltype + "','" + txtvendortotaldues + "','" + txtopeningbalance + "','" + txtbillattention + "','" + txtbilladdress + "','" + txtbillcity + "','" + txtbillstate + "','" + txtbillzipcode + "','" + txtbillcountry + "','" + txtbillfax + "','" + txtsattention + "','" + txtsaddress + "','" + txtscity + "','" + txtssstate + "','" + txtszipcode + "','" + txtscountry + "','" + txtsfax + "','" + vendortype + "','" + contperson1 + "','" + contperson2 + "','" + emailid + "','" + agrementserve + "','" + startdate + "','" + enddate + "','" + saccode + "','" + hsmcode + "','" + bankaccountname + "','" + accountnum + "','" + ifsccode + "','" + bankname + "','" + credit_period + "','"+registrationno+"')");//insert command
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
    public int VendorUpdate(string compcode, string vendid, string vendname, string vendadd1, string txtvendorphone1, string txtvendorphone2, string txtvendordesignation, string txt_gst, string txtvendorpan, string txtvendorlst, string txtvendorcst, string txtvendortin, string txtvendorlbt, string txtvendorservictax, string txtvendoradd2, string ddlstatus, string ddltype, string txtvendortotaldues, string txtopeningbalance, string txtbillattention, string txtbilladdress, string txtbillcity, string txtbillstate, string txtbillzipcode, string txtbillcountry, string txtbillfax, string txtsattention, string txtsaddress, string txtscity, string txtssstate, string txtszipcode, string txtscountry, string txtsfax, string vendortype, string contperson1, string contperson2, string emailid, string agrementserve, string startdate, string enddate, string saccode, string hsmcode, string bankaccountname, string accountnum, string ifsccode, string bankname, string credit_period, string registrationno, string vandor_type_nation)//update args
    {
        int res = 0;
        try
        {
            res = dv.operation("UPDATE PAY_VENDOR_MASTER SET VEND_NAME='" + vendname + "',VEND_ADD1='" + vendadd1 + "',PHONE1='" + txtvendorphone1 + "',PHONE2='" + txtvendorphone2 + "',DESIGNATION='" + txtvendordesignation + "',GST='" + txt_gst + "',PAN_NO='" + txtvendorpan + "',LST_NO='" + txtvendorlst + "',CST_NO='" + txtvendorcst + "',TIN_NO='" + txtvendortin + "',LBT_NO='" + txtvendorlbt + "',SERVICE_TAX_NO='" + txtvendorservictax + "',VEND_ADD2='" + txtvendoradd2 + "',ACTIVE_STATUS='" + ddlstatus + "',TYPE='" + ddltype + "',TOTAL_DUES='" + txtvendortotaldues + "',OPENING_BALANCE='" + txtopeningbalance + "',txtbillattention='" + txtbillattention + "',txtbilladdress='" + txtbilladdress + "',txtbillcity='" + txtbillcity + "',txtbillstate='" + txtbillstate + "',txtbillzipcode='" + txtbillzipcode + "',txtbillcountry='" + txtbillcountry + "',txtbillfax='" + txtbillfax + "',txtsattention='" + txtsattention + "',txtsaddress='" + txtsaddress + "',txtscity='" + txtscity + "',txtssstate='" + txtssstate + "',txtszipcode='" + txtszipcode + "',txtscountry='" + txtscountry + "',txtsfax='" + txtsfax + "',vendor_type='" + vendortype + "' ,contact_person_1='" + contperson1 + "',contact_person_2='" + contperson2 + "',vendor_email_id='" + emailid + "',area_to_served='" + agrementserve + "',agrement_start_date='" + startdate + "',agrement_end_date='" + enddate + "',sac_code='" + saccode + "',hsm_code='" + hsmcode + "',bank_account_name='" + bankaccountname + "',account_number='" + accountnum + "',ifsc_code='" + ifsccode + "',bank_name='" + bankname + "',CREDIT_PERIOD='" + credit_period + "' ,comp_registration_num='" + registrationno + "' ,vandor_type_nation='" + vandor_type_nation + "' WHERE COMP_CODE='" + compcode + "' AND VEND_ID='" + vendid + "'");//update command
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
    public int VendorDelete(string compcode,string vendid)//delete args
    {
        int res = 0;
        try
        {
            res = dv.operation("DELETE FROM PAY_VENDOR_MASTER WHERE COMP_CODE='"+compcode +"' AND VEND_ID='"+vendid +"'");//delete command
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
    public DataSet VendorSelect()
    {
        DataSet result;
        try
        {
            result = dv.getData("SELECT * FROM PAY_VENDOR_MASTER");//select command
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
    public int ContactPersonDetailsInsert(string compcode, string vendid, string salutation, string FirstName, string LastName, string EmailAddress, string WorkPhoneno, string Mobile_No, string designation, string department)
    {
        int res = 0;
        try
        {
            res = dv.operation("INSERT INTO pay_vendor_contact_person(COMP_CODE,VEND_ID,salutation,txtfirstname,txtlastname,txteaddress,txtworkphonno,txtmobileno,txtdesignation1,txtdept) VALUES('" + compcode + "','" + vendid + "','" + salutation + "','" + FirstName + "','" + LastName + "','" + EmailAddress + "','" + WorkPhoneno + "','" + Mobile_No + "','" + designation + "','" + department + "')");//insert command
            return res;
        }
        catch
        {
            throw;//
        }
        finally
        {
        }
    }
}