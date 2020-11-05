using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
/// <summary>
/// Summary description for ProfitLoss
/// </summary>
public class ProfitLoss
{
    DAL d = new DAL();
    DAL client_d = new DAL();
	public ProfitLoss()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int calculateProfitLoss(string operation, string itemcode,double salesrate, double quantity) 
    {

        double purchaserate = 0;
        string field_profit_loss = "";
        double profit_loss = 0;
        try
        {
            MySqlCommand ItemPurchase = new MySqlCommand("Select PURCHASE_RATE from pay_item_master Where ITEM_CODE ='" + itemcode + "'", d.con1);
            d.con1.Open();
            MySqlDataReader drItemPurchase = ItemPurchase.ExecuteReader();
            if (drItemPurchase.Read())
            {
                purchaserate = Convert.ToDouble(drItemPurchase[0].ToString());
            }
            drItemPurchase.Close();
            ItemPurchase.Dispose();
        }
        catch (Exception e) { }
        finally
        {
            
            d.con1.Close();
        }
       

        if (purchaserate < salesrate)
        {
            profit_loss = salesrate - purchaserate;

            profit_loss = profit_loss * Convert.ToDouble(quantity);

            field_profit_loss = "profit";

        }
        else
        {
            profit_loss = purchaserate - salesrate;
            profit_loss = profit_loss * Convert.ToDouble(quantity);
            field_profit_loss = "loss";
        }
        //string xyz = "update pay_profit_loss SET " + field_profit_loss + "='" + profit_loss + "'  where Item_Code='" + item_code + "'";
        int updateprofitloss = d.operation("update pay_profit_loss SET " + field_profit_loss + "=" + profit_loss + "" + operation + "" + field_profit_loss + "  where Item_Code='" + itemcode + "'");
        return updateprofitloss;
    }

    public void getItemCode(string InvoiceNo,string CompCode , string TableName)
    {
        try
        {
        client_d.con.Open();
        MySqlCommand cmdItem = new MySqlCommand("Select ITEM_CODE,QUANTITY,RATE from " + TableName + " Where DOC_NO ='" + InvoiceNo + "' AND COMP_CODE ='" + CompCode + "'", client_d.con);
        MySqlDataReader drItem = cmdItem.ExecuteReader();
        while (drItem.Read())
        {
            int result = calculateProfitLoss("-", drItem[0].ToString(),Convert.ToDouble(drItem[2].ToString()), Convert.ToDouble(drItem[1].ToString()));
          
         }
             drItem.Close();
            drItem.Dispose();
        }
        catch (Exception e) { }
       finally
            {
      
        client_d.con.Close();
        }
        
    }

}