using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Gstronejson
/// </summary>
public class Gstronejson
{
   

	public Gstronejson()
	{
		 
	}
    // private String gstin;
    private String fp;
    private String filingTyp;
    private long gt;
    private long curGt;
    
    //private DocIssue docIssue;
    private String filDt;

    public string gstin { get; set; }
    public B2B[] b2b { get; set; }

   
    public String getFP() { return fp; }
    public void setFP(String value) { this.fp = value; }

    
    public String getFilingTyp() { return filingTyp; }
    public void setFilingTyp(String value) { this.filingTyp = value; }

   
    public long getGt() { return gt; }
    
    public void setGt(long value) { this.gt = value; }

   
    public long getCurGt() { return curGt; }
   
    public void setCurGt(long value) { this.curGt = value; }

   
    //public B2B[] getB2B() { return b2B; }
   
    //public void setB2B(B2B[] value) { this.b2B = value; }

    
    //public DocIssue getDocIssue() { return docIssue; }
   
    //public void setDocIssue(DocIssue value) { this.docIssue = value; }

    
    public String getFilDt() { return filDt; }
    
    public void setFilDt(String value) { this.filDt = value; }
}


//public class Inv {
//    private double val;
//    private Itm[] itms;
//    private InvTyp invTyp;
//    private PurpleFlag flag;
//    private Updby updby;
//    private String pos;
//    private Idt idt;
//    private CfsEnum rchrg;
//    private CfsEnum cflag;
//    private String inum;
//    private String chksum;

//    @JsonProperty("val")
//    public double getVal() { return val; }
//    @JsonProperty("val")
//    public void setVal(double value) { this.val = value; }

//    @JsonProperty("itms")
//    public Itm[] getItms() { return itms; }
//    @JsonProperty("itms")
//    public void setItms(Itm[] value) { this.itms = value; }

//    @JsonProperty("inv_typ")
//    public InvTyp getInvTyp() { return invTyp; }
//    @JsonProperty("inv_typ")
//    public void setInvTyp(InvTyp value) { this.invTyp = value; }

//    @JsonProperty("flag")
//    public PurpleFlag getFlag() { return flag; }
//    @JsonProperty("flag")
//    public void setFlag(PurpleFlag value) { this.flag = value; }

//    @JsonProperty("updby")
//    public Updby getUpdby() { return updby; }
//    @JsonProperty("updby")
//    public void setUpdby(Updby value) { this.updby = value; }

//    @JsonProperty("pos")
//    public String getPos() { return pos; }
//    @JsonProperty("pos")
//    public void setPos(String value) { this.pos = value; }

//    @JsonProperty("idt")
//    public Idt getIdt() { return idt; }
//    @JsonProperty("idt")
//    public void setIdt(Idt value) { this.idt = value; }

//    @JsonProperty("rchrg")
//    public CfsEnum getRchrg() { return rchrg; }
//    @JsonProperty("rchrg")
//    public void setRchrg(CfsEnum value) { this.rchrg = value; }

//    @JsonProperty("cflag")
//    public CfsEnum getCflag() { return cflag; }
//    @JsonProperty("cflag")
//    public void setCflag(CfsEnum value) { this.cflag = value; }

//    @JsonProperty("inum")
//    public String getInum() { return inum; }
//    @JsonProperty("inum")
//    public void setInum(String value) { this.inum = value; }

//    @JsonProperty("chksum")
//    public String getChksum() { return chksum; }
//    @JsonProperty("chksum")
//    public void setChksum(String value) { this.chksum = value; }
//}

//public class DocIssue {
//    private CfsEnum flag;
//    private DocDet[] docDet;
//    private String chksum;

//    @JsonProperty("flag")
//    public CfsEnum getFlag() { return flag; }
//    @JsonProperty("flag")
//    public void setFlag(CfsEnum value) { this.flag = value; }

//    @JsonProperty("doc_det")
//    public DocDet[] getDocDet() { return docDet; }
//    @JsonProperty("doc_det")
//    public void setDocDet(DocDet[] value) { this.docDet = value; }

//    @JsonProperty("chksum")
//    public String getChksum() { return chksum; }
//    @JsonProperty("chksum")
//    public void setChksum(String value) { this.chksum = value; }
//}
