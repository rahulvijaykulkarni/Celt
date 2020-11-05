using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ItemMasterBAL
/// </summary>
public class ItemMasterBAL
{
    DAL dd = new DAL();

	public ItemMasterBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ItemInsert(string compcode, string itemcode, string itemname, string unit, double wt_per_resource, double vat, string product_service, string item_description, string sac_code, string brand, string hsn_code, string purchse_rate,string sales_rate, string validity,string size)//insert agrs
    {
        int res = 0;
        try
        {
            res = dd.operation("INSERT INTO PAY_ITEM_MASTER(COMP_CODE,ITEM_CODE,ITEM_NAME,unit,unit_per_piece,VAT,product_service,item_description,sac_number,item_brand,hsn_number,PURCHASE_RATE,SALES_RATE,validity,size) VALUES('" + compcode + "','" + itemcode + "','" + itemname + "','" + unit + "'," + wt_per_resource + "," + vat + ",'" + product_service + "','" + item_description + "','" + sac_code + "','" + brand + "','" + hsn_code + "','" + purchse_rate + "','" + sales_rate + "','" + validity + "','"+size+"')");//insert command
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


    public int ItemUpdate(string compcode, string itemcode, string itemname, string unit, double wt_per_resource, double vat, string product_service, string item_description, string san_code, string brand, string hsn_code, string purchase_rate, string sales_rate, string validity,string size)//update args
    {
        int res = 0;
        try
        {
            res = dd.operation("UPDATE PAY_ITEM_MASTER SET ITEM_NAME='" + itemname + "',unit='" + unit + "',unit_per_piece  = " + wt_per_resource + ",VAT=" + vat + ",product_service='" + product_service + "',item_description='" + item_description + "',sac_number='" + san_code + "',item_brand='" + brand + "',hsn_number='" + hsn_code + "',PURCHASE_RATE='" + purchase_rate + "',SALES_RATE='" + sales_rate + "',validity='" + validity + "',size='"+size+"' WHERE ITEM_CODE='" + itemcode + "' AND COMP_CODE='" + compcode + "'");//update command
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
    public int ItemDelete(string compcode, string itemcode)//delete args
    {
        int res = 0;
        try
        {
            res = dd.operation("DELETE FROM PAY_ITEM_MASTER WHERE ITEM_CODE='" + itemcode + "' AND COMP_CODE='" + compcode + "'");//delete command
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
    public DataSet ItemSelect(string compcode)
    {
        DataSet result;
        try
        {
            result = dd.getData("SELECT  ITEM_CODE,ITEM_NAME,unit,unit_per_piece,VAT,product_service,item_description,sac_number,item_brand,hsn_number,PURCHASE_RATE,SALES_RATE,validity,size FROM PAY_ITEM_MASTER  WHERE COMP_CODE= '" + compcode + "' ORDER BY ITEM_CODE");//select command
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