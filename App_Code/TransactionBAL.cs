using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransactionBAL
/// </summary>
public class TransactionBAL
{
    DAL ds = new DAL();

	public TransactionBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //----- Insert Transaction -------
    public int TransactionInsert(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double DEDUCTION, double TOTAL, double OUTSTANDING, string BILL_YEAR, string P_O_NO, string TRANSPORT, string FREIGHT, string VEHICLE_NO, double discount, double discounted_price, double final_price,string category,double sgstper,double sgstamount,double igstugstper,double igstugstamount)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTION(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL, DEDUCTION, TOTAL,OUTSTANDING,BILL_YEAR,P_O_NO,TRANSPORT,FREIGHT,VEHICLE_NO,DISCOUNT,DISCOUNTED_PRICE,FINAL_PRICE,category,Sgstper,SgstAmount,Igst_Ugst_per,Igst_Ugst_Amount) VALUES('" + compcode + "','" + taskcode + "','" + docno + "',STR_TO_DATE('" + docdate + "','%d/%m/%Y'),'" + custcode + "','" + refno1 + "','" + refno2 + "','" + narration + "','" + billmonth + "'," + grossamt + "," + ser_per_rec + "," + ser_taxable_rec + "," + ser_tax_per_rec + "," + ser_tax_per_rec_amt + "," + ser_tax_cess_per_rec + "," + ser_tax_cess_rec_amt + "," + ser_tax_hcess_per_rec + "," + ser_tax_hcess_rec_amt + "," + ser_tax_rec_tot + ", " + ser_per_pro + "," + ser_taxable_pro + "," + ser_tax_per_pro + "," + ser_tax_per_pro_amt + "," + ser_tax_cess_per_pro + "," + ser_tax_cess_pro_amt + "," + ser_tax_hcess_per_pro + "," + ser_tax_hcess_pro_amt + "," + ser_tax_pro_tot + "," + net_tot + "," + DEDUCTION + ", " + TOTAL + "," + OUTSTANDING + ",'" + BILL_YEAR + "','" + P_O_NO + "','" + TRANSPORT + "','" + FREIGHT + "','" + VEHICLE_NO + "'," + discount + "," + discounted_price + "," + final_price + ",'" + category + "','" + sgstper + "','" + sgstamount + "','" + igstugstper + "','" + igstugstamount + "')");
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

    //--- INSERT Transaction Details----
    public int TransactionDetailsInsert(string compcode, string taskcode, string docno, int srno, string item_code, string particular, string designation, double quantity, double rate, double tax_rate, double amount,string hsc,string sac)
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE, TASK_CODE, DOC_NO, SR_NO,ITEM_CODE, PARTICULAR, DESIGNATION, QUANTITY, RATE, TAX_RATE,AMOUNT,hsn_number,sac_number) VALUES('" + compcode + "','" + taskcode + "','" + docno + "'," + srno + ",'" + item_code + "','" + particular + "','" + designation + "'," + quantity + "," + rate + "," + tax_rate + "," + amount + ",'" + hsc + "','" + sac + "')");//insert command
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

    //------- Insert Labour Transaction -------
    public int LabourTransactionInsert(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double DEDUCTION, double TOTAL, double OUTSTANDING, string BILL_YEAR, string P_O_NO, string TRANSPORT, string FREIGHT, string VEHICLE_NO, double discount, double discounted_price, double final_price)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTIONL(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL, DEDUCTION, TOTAL,OUTSTANDING,BILL_YEAR,P_O_NO,TRANSPORT,FREIGHT,VEHICLE_NO,DISCOUNT,DISCOUNTED_PRICE,FINAL_PRICE) VALUES('" + compcode + "','" + taskcode + "','" + docno + "',STR_TO_DATE('" + docdate + "','%d/%m/%Y'),'" + custcode + "','" + refno1 + "','" + refno2 + "','" + narration + "','" + billmonth + "'," + grossamt + "," + ser_per_rec + "," + ser_taxable_rec + "," + ser_tax_per_rec + "," + ser_tax_per_rec_amt + "," + ser_tax_cess_per_rec + "," + ser_tax_cess_rec_amt + "," + ser_tax_hcess_per_rec + "," + ser_tax_hcess_rec_amt + "," + ser_tax_rec_tot + ", " + ser_per_pro + "," + ser_taxable_pro + "," + ser_tax_per_pro + "," + ser_tax_per_pro_amt + "," + ser_tax_cess_per_pro + "," + ser_tax_cess_pro_amt + "," + ser_tax_hcess_per_pro + "," + ser_tax_hcess_pro_amt + "," + ser_tax_pro_tot + "," + net_tot + "," + DEDUCTION + ", " + TOTAL + "," + OUTSTANDING + ",'" + BILL_YEAR + "','" + P_O_NO + "','" + TRANSPORT + "','" + FREIGHT + "','" + VEHICLE_NO + "'," + discount + "," + discounted_price + "," + final_price + ")");
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

    //-------- Insert Labour Transaction Details ----------
    public int LabourTransactionDetailsInsert(string compcode, string taskcode, string docno, int srno, string item_code, string particular, string designation, double quantity, double rate, double tax_rate, double amount)
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTIONL_DETAILS(COMP_CODE, TASK_CODE, DOC_NO, SR_NO,ITEM_CODE, PARTICULAR, DESIGNATION, QUANTITY, RATE, TAX_RATE,AMOUNT) VALUES('" + compcode + "','" + taskcode + "','" + docno + "'," + srno + ",'" + item_code + "','" + particular + "','" + designation + "'," + quantity + "," + rate + "," + tax_rate + "," + amount + ")");//insert command
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

    // ----- Update Transaction -----

    public int TransactionUpdate(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double deduction, double total, double outstanding, string bill_year, string pono, string transport, string freight, string vehichle_no, double discount, double discounted_price, double final_price,double sgstper,double sgstamount,double igstugstper,double igstugstamount)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update PAY_TRANSACTION SET COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE=STR_TO_DATE('" + docdate + "','%d/%m/%Y'),CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT=" + grossamt + " ,SER_PER_REC=" + ser_per_rec + ",SER_TAXABLE_REC=" + ser_taxable_rec + " ,SER_TAX_PER_REC=" + ser_tax_per_rec + " ,SER_TAX_PER_REC_AMT=" + ser_tax_per_rec_amt + " ,SER_TAX_CESS_PER_REC=" + ser_tax_cess_per_rec + " ,SER_TAX_CESS_REC_AMT=" + ser_tax_cess_rec_amt + ",SER_TAX_HCESS_PER_REC=" + ser_tax_hcess_per_rec + ",SER_TAX_HCESS_REC_AMT=" + ser_tax_hcess_rec_amt + " ,SER_TAX_REC_TOT=" + ser_tax_rec_tot + " ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO=" + ser_taxable_pro + " ,SER_TAX_PER_PRO=" + ser_tax_per_pro + " ,SER_TAX_PER_PRO_AMT=" + ser_tax_per_pro_amt + " ,SER_TAX_CESS_PER_PRO=" + ser_tax_cess_per_pro + " ,SER_TAX_CESS_PRO_AMT=" + ser_tax_cess_pro_amt + " ,SER_TAX_HCESS_PER_PRO=" + ser_tax_hcess_per_pro + " ,SER_TAX_HCESS_PRO_AMT=" + ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT=" + ser_tax_pro_tot + " ,NET_TOTAL=" + net_tot + " ,DEDUCTION=" + deduction + " ,TOTAL=" + total + ",OUTSTANDING=" + outstanding + " ,BILL_YEAR='" + bill_year + "' ,P_O_NO='" + pono + "' ,TRANSPORT='" + transport + "',FREIGHT='" + freight + "' ,VEHICLE_NO='" + vehichle_no + "', DISCOUNT='" + discount + "', DISCOUNTED_PRICE=" + discounted_price + " ,FINAL_PRICE=" + final_price + ",Sgstper='" + sgstper + "',SgstAmount='" + sgstamount + "',Igst_Ugst_per='" + igstugstper + "',Igst_Ugst_Amount='" + igstugstamount + "' Where DOC_NO='" + docno + "' AND COMP_CODE='" + compcode + "'");//insert command
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

    //---- Update Transaction Detail ----------

    public int TransactionDetailsUpdate(string compcode, string taskcode, string docno, int srno, string item_code, string particular, string designation, double quantity, double rate, double tax_rate, double amount)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update PAY_TRANSACTION_DETAILS SET COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',SR_NO=" + srno + ",ITEM_CODE='" + item_code + "',PARTICULAR='" + particular + "',DESIGNATION='" + designation + "',QUANTITY=" + quantity + ",RATE=" + rate + ",TAX_RATE=" + tax_rate + ",AMOUNT=	" + amount + " Where DOC_NO='"+docno+"' AND COMP_CODE='"+compcode+"'");//insert command
            return res;
        }
        catch
        {
            throw;//COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE='" + docdate + "',CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT="+ grossamt +" ,SER_PER_REC="+ ser_per_rec +",SER_TAXABLE_REC="+ ser_taxable_rec +" ,SER_TAX_PER_REC="+ ser_tax_per_rec +" ,SER_TAX_PER_REC_AMT="+ ser_tax_per_rec_amt +" ,SER_TAX_CESS_PER_REC="+ ser_tax_cess_per_rec +" ,SER_TAX_CESS_REC_AMT="+ ser_tax_cess_rec_amt +",SER_TAX_HCESS_PER_REC="+ ser_tax_hcess_per_rec +",SER_TAX_HCESS_REC_AMT="+ ser_tax_hcess_rec_amt +" ,SER_TAX_REC_TOT="+ ser_tax_rec_tot +" ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO="+ ser_taxable_pro +" ,SER_TAX_PER_PRO="+ ser_tax_per_pro +" ,SER_TAX_PER_PRO_AMT="+ ser_tax_per_pro_amt +" ,SER_TAX_CESS_PER_PRO="+ ser_tax_cess_per_pro +" ,SER_TAX_CESS_PRO_AMT="+ ser_tax_cess_pro_amt +" ,SER_TAX_HCESS_PER_PRO="+ ser_tax_hcess_per_pro +" ,SER_TAX_HCESS_PRO_AMT=:+ ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT="+ ser_tax_pro_tot +" ,NET_TOTAL="+ net_tot +" ,DEDUCTION="+ DEDUCTION +" ,TOTAL=" + TOTAL + ",OUTSTANDING="+ OUTSTANDING +" ,BILL_YEAR="+ BILL_YEAR +" ,P_O_NO="+ P_O_NO +" ,TRANSPORT="+ TRANSPORT +" ,FREIGHT="+ FREIGHT +" ,VEHICLE_NO="+ VEHICLE_NO +" ,DISCOUNT="+ DISCOUNT +" ,DISCOUNTED_PRICE="+ DISCOUNTED_PRICE +" ,FINAL_PRICE="+ FINAL_PRICE +"
        }
        finally
        {
        }
    }

    //---- Update  Labour Transaction ----------

    public int LabourTransactionUpdate(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double deduction, double total, double outstanding, string bill_year, string pono, string transport, string freight, string vehichle_no, double discount, double discounted_price, double final_price)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update PAY_TRANSACTIONL SET COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE=STR_TO_DATE('" + docdate + "','%d/%m/%Y'),CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT=" + grossamt + " ,SER_PER_REC=" + ser_per_rec + ",SER_TAXABLE_REC=" + ser_taxable_rec + " ,SER_TAX_PER_REC=" + ser_tax_per_rec + " ,SER_TAX_PER_REC_AMT=" + ser_tax_per_rec_amt + " ,SER_TAX_CESS_PER_REC=" + ser_tax_cess_per_rec + " ,SER_TAX_CESS_REC_AMT=" + ser_tax_cess_rec_amt + ",SER_TAX_HCESS_PER_REC=" + ser_tax_hcess_per_rec + ",SER_TAX_HCESS_REC_AMT=" + ser_tax_hcess_rec_amt + " ,SER_TAX_REC_TOT=" + ser_tax_rec_tot + " ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO=" + ser_taxable_pro + " ,SER_TAX_PER_PRO=" + ser_tax_per_pro + " ,SER_TAX_PER_PRO_AMT=" + ser_tax_per_pro_amt + " ,SER_TAX_CESS_PER_PRO=" + ser_tax_cess_per_pro + " ,SER_TAX_CESS_PRO_AMT=" + ser_tax_cess_pro_amt + " ,SER_TAX_HCESS_PER_PRO=" + ser_tax_hcess_per_pro + " ,SER_TAX_HCESS_PRO_AMT=" + ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT=" + ser_tax_pro_tot + " ,NET_TOTAL=" + net_tot + " ,DEDUCTION=" + deduction + " ,TOTAL=" + total + ",OUTSTANDING=" + outstanding + " ,BILL_YEAR='" + bill_year + "' ,P_O_NO='" + pono + "' ,TRANSPORT='" + transport + "',FREIGHT='" + freight + "' ,VEHICLE_NO='" + vehichle_no + "', DISCOUNT='" + discount + "', DISCOUNTED_PRICE=" + discounted_price + " ,FINAL_PRICE=" + final_price + " Where DOC_NO='" + docno + "' AND COMP_CODE='" + compcode + "'");//insert command
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

    //---- Update Labour Transaction Detail ----------

    public int LabourTransactionDetailsUpdate(string compcode, string taskcode, string docno, int srno, string item_code, string particular, string designation, double quantity, double rate, double tax_rate, double amount)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update PAY_TRANSACTIONL_DETAILS SET COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',SR_NO=" + srno + ",ITEM_CODE='" + item_code + "',PARTICULAR='" + particular + "',DESIGNATION='" + designation + "',QUANTITY=" + quantity + ",RATE=" + rate + ",TAX_RATE=" + tax_rate + ",AMOUNT=	" + amount + " Where DOC_NO='" + docno + "' AND COMP_CODE='" + compcode + "'");//insert command
            return res;
        }
        catch
        {
            throw;//COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE='" + docdate + "',CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT="+ grossamt +" ,SER_PER_REC="+ ser_per_rec +",SER_TAXABLE_REC="+ ser_taxable_rec +" ,SER_TAX_PER_REC="+ ser_tax_per_rec +" ,SER_TAX_PER_REC_AMT="+ ser_tax_per_rec_amt +" ,SER_TAX_CESS_PER_REC="+ ser_tax_cess_per_rec +" ,SER_TAX_CESS_REC_AMT="+ ser_tax_cess_rec_amt +",SER_TAX_HCESS_PER_REC="+ ser_tax_hcess_per_rec +",SER_TAX_HCESS_REC_AMT="+ ser_tax_hcess_rec_amt +" ,SER_TAX_REC_TOT="+ ser_tax_rec_tot +" ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO="+ ser_taxable_pro +" ,SER_TAX_PER_PRO="+ ser_tax_per_pro +" ,SER_TAX_PER_PRO_AMT="+ ser_tax_per_pro_amt +" ,SER_TAX_CESS_PER_PRO="+ ser_tax_cess_per_pro +" ,SER_TAX_CESS_PRO_AMT="+ ser_tax_cess_pro_amt +" ,SER_TAX_HCESS_PER_PRO="+ ser_tax_hcess_per_pro +" ,SER_TAX_HCESS_PRO_AMT=:+ ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT="+ ser_tax_pro_tot +" ,NET_TOTAL="+ net_tot +" ,DEDUCTION="+ DEDUCTION +" ,TOTAL=" + TOTAL + ",OUTSTANDING="+ OUTSTANDING +" ,BILL_YEAR="+ BILL_YEAR +" ,P_O_NO="+ P_O_NO +" ,TRANSPORT="+ TRANSPORT +" ,FREIGHT="+ FREIGHT +" ,VEHICLE_NO="+ VEHICLE_NO +" ,DISCOUNT="+ DISCOUNT +" ,DISCOUNTED_PRICE="+ DISCOUNTED_PRICE +" ,FINAL_PRICE="+ FINAL_PRICE +"
        }
        finally
        {
        }
    }

    //----- Delete Transaction----------
    public int TransactionDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTION WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

    //----- Delete Transaction Details -----------
    public int TransactionDetailsDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTION_DETAILS WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

    //----- Delete Labour Transaction----------
    public int LabourTransactionDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTIONL WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

    //----- Delete Labour Transaction Details -----------
    public int LabourTransactionDetailsDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTIONL_DETAILS WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

    public int TransactionQDetailsDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTIONQ_DETAILS WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

    public int TransactionQDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_TRANSACTIONQ WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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
    public int TransactionQInsert(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, float grossamt, float ser_per_rec, float ser_taxable_rec, float ser_tax_per_rec, float ser_tax_per_rec_amt, float ser_tax_cess_per_rec, float ser_tax_cess_rec_amt, float ser_tax_hcess_per_rec, float ser_tax_hcess_rec_amt, float ser_tax_rec_tot, float ser_per_pro, float ser_taxable_pro, float ser_tax_per_pro, float ser_tax_per_pro_amt, float ser_tax_cess_per_pro, float ser_tax_cess_pro_amt, float ser_tax_hcess_per_pro, float ser_tax_hcess_pro_amt, float ser_tax_pro_tot, float net_tot)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTION(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL) VALUES('" + compcode + "','" + taskcode + "','" + docno + "','" + docdate + "','" + custcode + "','" + refno1 + "','" + refno2 + "','" + narration + "','" + billmonth + "'," + grossamt + "," + ser_per_rec + "," + ser_taxable_rec + "," + ser_tax_per_rec + "," + ser_tax_per_rec_amt + "," + ser_tax_cess_per_rec + "," + ser_tax_cess_rec_amt + "," + ser_tax_hcess_per_rec + "," + ser_tax_hcess_rec_amt + "," + ser_tax_rec_tot + ", " + ser_per_pro + "," + ser_taxable_pro + "," + ser_tax_per_pro + "," + ser_tax_per_pro_amt + "," + ser_tax_cess_per_pro + "," + ser_tax_cess_pro_amt + "," + ser_tax_hcess_per_pro + "," + ser_tax_hcess_pro_amt + "," + ser_tax_pro_tot + "," + net_tot + ")");//insert command
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

    public int TransactionQDetailsInsert(string compcode, string taskcode, string docno, int srno, string particular, string designation, float quantity, float rate, float amount)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO PAY_TRANSACTION_DETAILS(COMP_CODE, TASK_CODE, DOC_NO, SR_NO, PARTICULAR, DESIGNATION, QUANTITY, RATE, AMOUNT) VALUES('" + compcode + "','" + taskcode + "','" + docno + "'," + srno + ",'" + particular + "','" + designation + "'," + quantity + "," + rate + "," + amount + ")");//insert command
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

    public int TransactionPaymentDetailDelete(string compcode, string docno)//delete args
    {
        int res = 0;
        try
        {
            res = ds.operation("DELETE FROM PAY_PAYMENT_DETAIL WHERE COMP_CODE='" + compcode + "' AND DOC_NO='" + docno + "' ");//delete command
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

////-------------- Polyfilm -----------------------------
    public int PolyfilmTransactionInsert(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double DEDUCTION, double TOTAL, double OUTSTANDING, string BILL_YEAR, string P_O_NO, string TRANSPORT, string FREIGHT, string VEHICLE_NO, double discount, double discounted_price, double final_price)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO POLYFILM_TRANSACTION(COMP_CODE, TASK_CODE, DOC_NO, DOC_DATE, CUST_CODE, REF_NO1, REF_NO2, NARRATION, BILL_MONTH, GROSS_AMOUNT, SER_PER_REC, SER_TAXABLE_REC, SER_TAX_PER_REC, SER_TAX_PER_REC_AMT, SER_TAX_CESS_PER_REC, SER_TAX_CESS_REC_AMT, SER_TAX_HCESS_PER_REC, SER_TAX_HCESS_REC_AMT, SER_TAX_REC_TOT, SER_PER_PRO, SER_TAXABLE_PRO, SER_TAX_PER_PRO, SER_TAX_PER_PRO_AMT, SER_TAX_CESS_PER_PRO, SER_TAX_CESS_PRO_AMT, SER_TAX_HCESS_PER_PRO, SER_TAX_HCESS_PRO_AMT, SER_TAX_PRO_TOT, NET_TOTAL, DEDUCTION, TOTAL,OUTSTANDING,BILL_YEAR,P_O_NO,TRANSPORT,FREIGHT,VEHICLE_NO,DISCOUNT,DISCOUNTED_PRICE,FINAL_PRICE) VALUES('" + compcode + "','" + taskcode + "','" + docno + "',STR_TO_DATE('" + docdate + "','%d/%m/%Y'),'" + custcode + "','" + refno1 + "','" + refno2 + "','" + narration + "','" + billmonth + "'," + grossamt + "," + ser_per_rec + "," + ser_taxable_rec + "," + ser_tax_per_rec + "," + ser_tax_per_rec_amt + "," + ser_tax_cess_per_rec + "," + ser_tax_cess_rec_amt + "," + ser_tax_hcess_per_rec + "," + ser_tax_hcess_rec_amt + "," + ser_tax_rec_tot + ", " + ser_per_pro + "," + ser_taxable_pro + "," + ser_tax_per_pro + "," + ser_tax_per_pro_amt + "," + ser_tax_cess_per_pro + "," + ser_tax_cess_pro_amt + "," + ser_tax_hcess_per_pro + "," + ser_tax_hcess_pro_amt + "," + ser_tax_pro_tot + "," + net_tot + "," + DEDUCTION + ", " + TOTAL + "," + OUTSTANDING + ",'" + BILL_YEAR + "','" + P_O_NO + "','" + TRANSPORT + "','" + FREIGHT + "','" + VEHICLE_NO + "'," + discount + "," + discounted_price + "," + final_price + ")");
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

    //--- INSERT Polyfilm Transaction Details----
    public int PolyfilmTransactionDetailsInsert(string compcode, string taskcode, string docno, int srno, string itemcode, string batchno, string category, string width, string thick, string unit, double qty, double rate, double gross, double mtr, double sqmtr, double kilo)
    {
        int res = 0;
        try
        {
            res = ds.operation("INSERT INTO POLYFILM_TRANSACTION_DETAILS(COMP_CODE,TASK_CODE,DOC_NO,SR_NO,ITEM_CODE,BATCH_NO,CATEGORY,WIDTH,THICKNESS,UNIT,QUANTITY,RATE,GROSS_TOTAL,meter,sq_meter,kg) VALUES('" + compcode + "','" + taskcode + "','" + docno + "','" + srno + "','" + itemcode + "','" + batchno + "','" + category + "','" + width + "','" + thick + "','" + unit + "'," + qty + "," + rate + "," + gross + "," + mtr + "," + sqmtr + "," + kilo + ")");//insert command
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

    // ----- Update  Polyfilm Transaction -----

    public int PolyfilmTransactionUpdate(string compcode, string taskcode, string docno, string docdate, string custcode, string refno1, string refno2, string narration, string billmonth, double grossamt, double ser_per_rec, double ser_taxable_rec, double ser_tax_per_rec, double ser_tax_per_rec_amt, double ser_tax_cess_per_rec, double ser_tax_cess_rec_amt, double ser_tax_hcess_per_rec, double ser_tax_hcess_rec_amt, double ser_tax_rec_tot, double ser_per_pro, double ser_taxable_pro, double ser_tax_per_pro, double ser_tax_per_pro_amt, double ser_tax_cess_per_pro, double ser_tax_cess_pro_amt, double ser_tax_hcess_per_pro, double ser_tax_hcess_pro_amt, double ser_tax_pro_tot, double net_tot, double deduction, double total, double outstanding, string bill_year, string pono, string transport, string freight, string vehichle_no, double discount, double discounted_price, double final_price)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update POLYFILM_TRANSACTION SET COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE=STR_TO_DATE('" + docdate + "','%d/%m/%Y'),CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT=" + grossamt + " ,SER_PER_REC=" + ser_per_rec + ",SER_TAXABLE_REC=" + ser_taxable_rec + " ,SER_TAX_PER_REC=" + ser_tax_per_rec + " ,SER_TAX_PER_REC_AMT=" + ser_tax_per_rec_amt + " ,SER_TAX_CESS_PER_REC=" + ser_tax_cess_per_rec + " ,SER_TAX_CESS_REC_AMT=" + ser_tax_cess_rec_amt + ",SER_TAX_HCESS_PER_REC=" + ser_tax_hcess_per_rec + ",SER_TAX_HCESS_REC_AMT=" + ser_tax_hcess_rec_amt + " ,SER_TAX_REC_TOT=" + ser_tax_rec_tot + " ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO=" + ser_taxable_pro + " ,SER_TAX_PER_PRO=" + ser_tax_per_pro + " ,SER_TAX_PER_PRO_AMT=" + ser_tax_per_pro_amt + " ,SER_TAX_CESS_PER_PRO=" + ser_tax_cess_per_pro + " ,SER_TAX_CESS_PRO_AMT=" + ser_tax_cess_pro_amt + " ,SER_TAX_HCESS_PER_PRO=" + ser_tax_hcess_per_pro + " ,SER_TAX_HCESS_PRO_AMT=" + ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT=" + ser_tax_pro_tot + " ,NET_TOTAL=" + net_tot + " ,DEDUCTION=" + deduction + " ,TOTAL=" + total + ",OUTSTANDING=" + outstanding + " ,BILL_YEAR='" + bill_year + "' ,P_O_NO='" + pono + "' ,TRANSPORT='" + transport + "',FREIGHT='" + freight + "' ,VEHICLE_NO='" + vehichle_no + "', DISCOUNT='" + discount + "', DISCOUNTED_PRICE=" + discounted_price + " ,FINAL_PRICE=" + final_price + " Where DOC_NO='" + docno + "' AND COMP_CODE='" + compcode + "'");//insert command
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

    //---- Update Polyfilm Transaction Detail ----------

    public int PolyfilmTransactionDetailsUpdate(string compcode,string taskcode,string docno,int srno, string itemcode, string batchno,string category,string width,string thick,string unit,double qty,double rate,double gross,double mtr,double sqmtr,double kilo)//insert agrs
    {
        int res = 0;
        try
        {
            res = ds.operation("Update POLYFILM_TRANSACTION_DETAILS SET COMP_CODE='"+compcode+"',TASK_CODE='"+taskcode+"',DOC_NO='"+docno+"',SR_NO='"+srno+"',ITEM_CODE='"+itemcode+"',BATCH_NO='"+batchno+"',CATEGORY='"+category+"',WIDTH='"+width+"',THICKNESS='"+thick+"',UNIT='"+unit+"',QUANTITY="+qty+",RATE="+rate+",GROSS_TOTAL="+gross+",meter="+mtr+",sq_meter="+sqmtr+",kg="+kilo+" Where DOC_NO='" + docno + "' AND COMP_CODE='" + compcode + "'");//insert command
            return res;
        }
        catch
        {
            throw;//COMP_CODE='" + compcode + "',TASK_CODE='" + taskcode + "',DOC_NO='" + docno + "',DOC_DATE='" + docdate + "',CUST_CODE='" + custcode + "',REF_NO1='" + refno1 + "',REF_NO2='" + refno2 + "',NARRATION='" + narration + "',BILL_MONTH='" + billmonth + "',GROSS_AMOUNT="+ grossamt +" ,SER_PER_REC="+ ser_per_rec +",SER_TAXABLE_REC="+ ser_taxable_rec +" ,SER_TAX_PER_REC="+ ser_tax_per_rec +" ,SER_TAX_PER_REC_AMT="+ ser_tax_per_rec_amt +" ,SER_TAX_CESS_PER_REC="+ ser_tax_cess_per_rec +" ,SER_TAX_CESS_REC_AMT="+ ser_tax_cess_rec_amt +",SER_TAX_HCESS_PER_REC="+ ser_tax_hcess_per_rec +",SER_TAX_HCESS_REC_AMT="+ ser_tax_hcess_rec_amt +" ,SER_TAX_REC_TOT="+ ser_tax_rec_tot +" ,SER_PER_PRO=" + ser_per_pro + ",SER_TAXABLE_PRO="+ ser_taxable_pro +" ,SER_TAX_PER_PRO="+ ser_tax_per_pro +" ,SER_TAX_PER_PRO_AMT="+ ser_tax_per_pro_amt +" ,SER_TAX_CESS_PER_PRO="+ ser_tax_cess_per_pro +" ,SER_TAX_CESS_PRO_AMT="+ ser_tax_cess_pro_amt +" ,SER_TAX_HCESS_PER_PRO="+ ser_tax_hcess_per_pro +" ,SER_TAX_HCESS_PRO_AMT=:+ ser_tax_hcess_pro_amt + ",SER_TAX_PRO_TOT="+ ser_tax_pro_tot +" ,NET_TOTAL="+ net_tot +" ,DEDUCTION="+ DEDUCTION +" ,TOTAL=" + TOTAL + ",OUTSTANDING="+ OUTSTANDING +" ,BILL_YEAR="+ BILL_YEAR +" ,P_O_NO="+ P_O_NO +" ,TRANSPORT="+ TRANSPORT +" ,FREIGHT="+ FREIGHT +" ,VEHICLE_NO="+ VEHICLE_NO +" ,DISCOUNT="+ DISCOUNT +" ,DISCOUNTED_PRICE="+ DISCOUNTED_PRICE +" ,FINAL_PRICE="+ FINAL_PRICE +"
        }
        finally
        {
        }
    }


}