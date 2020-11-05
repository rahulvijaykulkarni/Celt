using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class staff_salary_structure : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        getGrideview();
        if(!IsPostBack){
            client_list();
        }

    }

    protected void btn_save_click(object sender, EventArgs e) {
        int res=0,res1 = 0;
        try
        {
            res1 = d.operation("delete from  pay_staffsalary_structure where comp_code='"+Session["COMP_CODE"].ToString()+"' and unit_code='"+ddl_unit.SelectedValue.ToString()+"'");
           // res = d.operation("Insert Into pay_staffsalary_structure (comp_code,head1,head1_per,head2,head2_per,head3,head3_per,head4,head4_per,head5,head5_per,head6,head6_per,head7,head7_per,head8,head8_per,head9,head9_per,head10,head10_per,head11,head11_per,head12,head12_per,head13,head13_per,head14,head14_per,head15,head15_per,Lhead1,Lhead2,Lhead3,Lhead4,Lhead5,Dhead1,Dhead2,Dhead3,Dhead4,Dhead5,Dhead6,Dhead7,Dhead8,Dhead9,Dhead10,pf_employee,pf_employer,esic_employee,esic_employer) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_head1.Text.ToString() + "','" + txt_head1_per.Text.ToString() + "','" + txt_head2.Text.ToString() + "','" + txt_head2_per.Text.ToString() + "','" + txt_head3.Text.ToString() + "','" + txt_head3_per.Text.ToString() + "','" + txt_head4.Text.ToString() + "','" + txt_head4_per.Text.ToString() + "','" + txt_head5.Text.ToString() + "','" + txt_head5_per.Text.ToString() + "','" + txt_head6.Text.ToString() + "','" + txt_head6_per.Text.ToString() + "','" + txt_head7.Text.ToString() + "','" + txt_head7_per.Text.ToString() + "','" + txt_head8.Text.ToString() + "','" + txt_head8_per.Text.ToString() + "','" + txt_head9.Text.ToString() + "','" + txt_head9_per.Text.ToString() + "','" + txt_head10.Text.ToString() + "','" + txt_head10_per.Text.ToString() + "','" + txt_head11.Text.ToString() + "','" + txt_head11_per.Text.ToString() + "','" + txt_head12.Text.ToString() + "','" + txt_head12_per.Text.ToString() + "','" + txt_head13.Text.ToString() + "','" + txt_head13_per.Text.ToString() + "','" + txt_head14.Text.ToString() + "','" + txt_head14_per.Text.ToString() + "','" + txt_head15.Text.ToString() + "','" + txt_head15_per.Text.ToString() + "','" + txt_lhead1.Text.ToString() + "','" + txt_lhead2.Text.ToString() + "','" + txt_lhead3.Text.ToString() + "','" + txt_lhead4.Text.ToString() + "','" + txt_lhead5.Text.ToString() + "','" + txt_dhead1.Text.ToString() + "','" + txt_dhead2.Text.ToString() + "','" + txt_dhead3.Text.ToString() + "','" + txt_dhead4.Text.ToString() + "','" + txt_dhead5.Text.ToString() + "','" + txt_dhead6.Text.ToString() + "','" + txt_dhead7.Text.ToString() + "','" + txt_dhead8.Text.ToString() + "','" + txt_dhead9.Text.ToString() + "','" + txt_dhead10.Text.ToString() + "','"+txt_pfemployee.Text.ToString()+"','"+txt_pfemployer.Text.ToString()+"','"+txt_esicemployee.Text.ToString()+"','"+txt_esicemployer.Text.ToString()+"')");
            res = d.operation("Insert Into pay_staffsalary_structure (comp_code,head1,head1_per,head2,head2_per,head3,head3_per,head4,head4_per,head5,head5_per,head6,head6_per,head7,head7_per,head8,head8_per,head9,head9_per,head10,head10_per,head11,head11_per,head12,head12_per,head13,head13_per,head14,head14_per,head15,head15_per,Lhead1,Lhead2,Lhead3,Lhead4,Lhead5,Dhead1,Dhead2,Dhead3,Dhead4,Dhead5,Dhead6,Dhead7,Dhead8,Dhead9,Dhead10,pf_employee,pf_employer,esic_employee,esic_employer,pf_employee_formula,pf_employer_formula,esic_employee_formula,esic_employer_formula,unit_code,client_code) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + txt_head1.Text.ToString() + "','" + txt_head1_per.Text.ToString() + "','" + txt_head2.Text.ToString() + "','" + txt_head2_per.Text.ToString() + "','" + txt_head3.Text.ToString() + "','" + txt_head3_per.Text.ToString() + "','" + txt_head4.Text.ToString() + "','" + txt_head4_per.Text.ToString() + "','" + txt_head5.Text.ToString() + "','" + txt_head5_per.Text.ToString() + "','" + txt_head6.Text.ToString() + "','" + txt_head6_per.Text.ToString() + "','" + txt_head7.Text.ToString() + "','" + txt_head7_per.Text.ToString() + "','" + txt_head8.Text.ToString() + "','" + txt_head8_per.Text.ToString() + "','" + txt_head9.Text.ToString() + "','" + txt_head9_per.Text.ToString() + "','" + txt_head10.Text.ToString() + "','" + txt_head10_per.Text.ToString() + "','" + txt_head11.Text.ToString() + "','" + txt_head11_per.Text.ToString() + "','" + txt_head12.Text.ToString() + "','" + txt_head12_per.Text.ToString() + "','" + txt_head13.Text.ToString() + "','" + txt_head13_per.Text.ToString() + "','" + txt_head14.Text.ToString() + "','" + txt_head14_per.Text.ToString() + "','" + txt_head15.Text.ToString() + "','" + txt_head15_per.Text.ToString() + "','" + txt_lhead1.Text.ToString() + "','" + txt_lhead2.Text.ToString() + "','" + txt_lhead3.Text.ToString() + "','" + txt_lhead4.Text.ToString() + "','" + txt_lhead5.Text.ToString() + "','" + txt_dhead1.Text.ToString() + "','" + txt_dhead2.Text.ToString() + "','" + txt_dhead3.Text.ToString() + "','" + txt_dhead4.Text.ToString() + "','" + txt_dhead5.Text.ToString() + "','" + txt_dhead6.Text.ToString() + "','" + txt_dhead7.Text.ToString() + "','" + txt_dhead8.Text.ToString() + "','" + txt_dhead9.Text.ToString() + "','" + txt_dhead10.Text.ToString() + "','" + txt_pfemployee.Text.ToString() + "','" + txt_pfemployer.Text.ToString() + "','" + txt_esicemployee.Text.ToString() + "','" + txt_esicemployer.Text.ToString() + "','" + txt_pfemployee_formula.Text.ToString() + "','" + txt_pfemployer_formula.Text.ToString() + "','" + txt_esicemployee_formula.Text.ToString() + "','" + txt_esicemployer_formula.Text.ToString() + "','"+ddl_unit.SelectedValue.ToString()+"','"+ddl_client.SelectedValue.ToString()+"')");
            if (res > 0)
             {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!!')", true);
              }
               else
               {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record adding failed...')", true);
                }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally {
            getGrideview();
            text_clear();
        }
    }

    public void getGrideview() {

       // MySqlDataAdapter dr = new MySqlDataAdapter("select head1,head1_per,head2,head2_per,head3,head3_per,head4,head4_per,head5,head5_per,head6,head6_per,head7,head7_per,head8,head8_per,head9,head9_per,head10,head10_per,head11,head11_per,head12,head12_per,head13,head13_per,head14,head14_per,head15,head15_per,Lhead1,Lhead2,Lhead3,Lhead4,Lhead5,Dhead1,Dhead2,Dhead3,Dhead4,Dhead5,Dhead6,Dhead7,Dhead8,Dhead9,Dhead10,pf_employee,pf_employer,esic_employee,esic_employer from pay_staffsalary_structure where comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
        MySqlDataAdapter dr = new MySqlDataAdapter("select client_code,unit_code,head1,head1_per,head2,head2_per,head3,head3_per,head4,head4_per,head5,head5_per,head6,head6_per,head7,head7_per,head8,head8_per,head9,head9_per,head10,head10_per,head11,head11_per,head12,head12_per,head13,head13_per,head14,head14_per,head15,head15_per,Lhead1,Lhead2,Lhead3,Lhead4,Lhead5,Dhead1,Dhead2,Dhead3,Dhead4,Dhead5,Dhead6,Dhead7,Dhead8,Dhead9,Dhead10,pf_employee,pf_employer,esic_employee,esic_employer,pf_employee_formula,pf_employer_formula,esic_employee_formula,esic_employer_formula from pay_staffsalary_structure where comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
        d.con.Open();
        DataTable dt = new DataTable();
        //DataSet DS1 = new DataSet();
        dr.Fill(dt);

        gv_salary_structure.DataSource = dt;
        gv_salary_structure.DataBind();
        d.con.Close();
    
    
    }

    protected void GridView1_files_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_salary_structure.UseAccessibleHeader = false;
            gv_salary_structure.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }

    protected void gv_salary_structure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_salary_structure, "Select$" + e.Row.RowIndex);

        }
    }

    protected void gv_salary_structure_SelectedIndexChanged(object sender,EventArgs e)
    {
         d.con1.Open();
         try
        {
            
              //string emp_code=gv_salary_structure.SelectedRow.Cells[1].Text;
              string client_code = gv_salary_structure.SelectedRow.Cells[0].Text;
              String unit_code = gv_salary_structure.SelectedRow.Cells[1].Text;
             // MySqlCommand cmd = new MySqlCommand("SELECT `comp_code`,  `unit_code`,  `head1`,  `head1_per`,  `head2`,  `head2_per`,  `head3`,  `head3_per`,  `head4`,  `head4_per`,  `head5`,  `head5_per`,  `head6`,  `head6_per`,  `head7`,  `head7_per`,  `head8`,  `head8_per`,  `head9`,  `head9_per`,  `head10`,  `head10_per`,  `head11`,  `head11_per`,  `head12`,  `head12_per`,  `head13`,  `head13_per`,  `head14`,  `head14_per`,  `head15`,  `head15_per`,  `Lhead1`,  `Lhead2`,  `Lhead3`,  `Lhead4`,  `Lhead5`,  `Dhead1`,  `Dhead2`,  `Dhead3`,  `Dhead4`,  `Dhead5`,  `Dhead6`,  `Dhead7`,  `Dhead8`,  `Dhead9`,  `Dhead10`,  `pf_employee`,  `pf_employer`,  `esic_employee`,  `esic_employer` from pay_staffsalary_structure where comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con1);
              MySqlCommand cmd = new MySqlCommand("SELECT `comp_code`,  `unit_code`,  `head1`,  `head1_per`,  `head2`,  `head2_per`,  `head3`,  `head3_per`,  `head4`,  `head4_per`,  `head5`,  `head5_per`,  `head6`,  `head6_per`,  `head7`,  `head7_per`,  `head8`,  `head8_per`,  `head9`,  `head9_per`,  `head10`,  `head10_per`,  `head11`,  `head11_per`,  `head12`,  `head12_per`,  `head13`,  `head13_per`,  `head14`,  `head14_per`,  `head15`,  `head15_per`,  `Lhead1`,  `Lhead2`,  `Lhead3`,  `Lhead4`,  `Lhead5`,  `Dhead1`,  `Dhead2`,  `Dhead3`,  `Dhead4`,  `Dhead5`,  `Dhead6`,  `Dhead7`,  `Dhead8`,  `Dhead9`,  `Dhead10`,  `pf_employee`,  `pf_employer`,  `esic_employee`,  `esic_employer` ,pf_employee_formula,pf_employer_formula,esic_employee_formula,esic_employer_formula,client_code,unit_code from pay_staffsalary_structure where comp_code='" + Session["COMP_CODE"].ToString() + "' and client_code='"+client_code+"' and unit_code='"+unit_code+"'", d.con1);
             MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txt_head1.Text = dr.GetValue(2).ToString();
                txt_head1_per.Text = dr.GetValue(3).ToString();
                txt_head2.Text = dr.GetValue(4).ToString();
                txt_head2_per.Text = dr.GetValue(5).ToString();
                txt_head3.Text = dr.GetValue(6).ToString();
                txt_head3_per.Text = dr.GetValue(7).ToString();
                txt_head4.Text = dr.GetValue(8).ToString();
                txt_head4_per.Text = dr.GetValue(9).ToString();
                txt_head5.Text = dr.GetValue(10).ToString();
                txt_head5_per.Text = dr.GetValue(11).ToString();
                txt_head6.Text = dr.GetValue(12).ToString();
                txt_head6_per.Text = dr.GetValue(13).ToString();
                txt_head7.Text = dr.GetValue(14).ToString();
                txt_head7_per.Text = dr.GetValue(15).ToString();
                txt_head8.Text = dr.GetValue(16).ToString();
                txt_head8_per.Text = dr.GetValue(17).ToString();
                txt_head9.Text = dr.GetValue(18).ToString();
                txt_head9_per.Text = dr.GetValue(19).ToString();
                txt_head10.Text = dr.GetValue(20).ToString();
                txt_head10_per.Text = dr.GetValue(21).ToString();
                txt_head11.Text = dr.GetValue(22).ToString();
                txt_head11_per.Text = dr.GetValue(23).ToString();
                txt_head12.Text = dr.GetValue(24).ToString();
                txt_head12_per.Text = dr.GetValue(25).ToString();
                txt_head13.Text = dr.GetValue(26).ToString();
                txt_head13_per.Text = dr.GetValue(27).ToString();
                txt_head14.Text = dr.GetValue(28).ToString();
                txt_head14_per.Text = dr.GetValue(29).ToString();
                txt_head15.Text = dr.GetValue(30).ToString();
                txt_head15_per.Text = dr.GetValue(31).ToString();
                txt_lhead1.Text = dr.GetValue(32).ToString();
                txt_lhead2.Text = dr.GetValue(33).ToString();
                txt_lhead3.Text = dr.GetValue(34).ToString();
                txt_lhead4.Text = dr.GetValue(35).ToString();
                txt_lhead5.Text = dr.GetValue(36).ToString();
                txt_dhead1.Text = dr.GetValue(37).ToString();
                txt_dhead2.Text = dr.GetValue(38).ToString();
                txt_dhead3.Text = dr.GetValue(39).ToString();
                txt_dhead4.Text = dr.GetValue(40).ToString();
                txt_dhead5.Text = dr.GetValue(41).ToString();
                txt_dhead6.Text = dr.GetValue(42).ToString();
                txt_dhead7.Text = dr.GetValue(43).ToString();
                txt_dhead8.Text = dr.GetValue(44).ToString();
                txt_dhead9.Text = dr.GetValue(45).ToString();
                txt_dhead10.Text = dr.GetValue(46).ToString();
                txt_pfemployee.Text = dr.GetValue(47).ToString();
                txt_pfemployer.Text = dr.GetValue(48).ToString();
                txt_esicemployee.Text = dr.GetValue(49).ToString();
                txt_esicemployer.Text = dr.GetValue(50).ToString();
                txt_pfemployee_formula.Text = dr.GetValue(51).ToString();
                txt_pfemployer_formula.Text = dr.GetValue(52).ToString();
                txt_esicemployee_formula.Text = dr.GetValue(53).ToString();
                txt_esicemployer_formula.Text = dr.GetValue(54).ToString();
                ddl_client.SelectedValue = dr.GetValue(55).ToString();
                ddl_client_SelectedIndexChanged1(null,null); 
                ddl_unit.SelectedValue = dr.GetValue(56).ToString();

            }
        }
         catch (Exception ex)
         {
             throw ex;
         }
         finally
         {
             d.con1.Close();
         }
    }


    protected void btn_close_click(object sender, EventArgs e) {
        //Response.Redirect("Home.aspx");

        try { 
        //get the Json filepath  
        string file = Server.MapPath("~/App_Code/GSTRONE.json");
        //deserialize JSON from file  
        string Json = System.IO.File.ReadAllText(file);
        JavaScriptSerializer ser = new JavaScriptSerializer();
        var personlist = ser.Deserialize<List<Gstronejson>>(Json);
        var results = JsonConvert.DeserializeObject<Gstronejson>(Json);
        string gst = results.gstin;
             List<string> list= new List<string>();
            foreach(var abd in results.b2b){
                string sas = abd.ctin;
                list.Add(sas);

            }
           
        Gstronejson abs = JsonConvert.DeserializeObject<Gstronejson>(Json);
        //string dsads = abs.b2b;
        }
        catch (Exception ee) { }


    }

    public void text_clear() {

        txt_head1.Text = "";
        txt_head1_per.Text = "";
        txt_head2.Text = "";
        txt_head2_per.Text = "";
        txt_head3.Text = "";
        txt_head3_per.Text = "";
        txt_head4.Text = "";
        txt_head4_per.Text = "";
        txt_head5.Text = "";
        txt_head5_per.Text = "";
        txt_head6.Text = "";
        txt_head6_per.Text = "";
        txt_head7.Text = "";
        txt_head7_per.Text = "";
        txt_head8.Text = "";
        txt_head8_per.Text = "";
        txt_head9.Text = "";
        txt_head9_per.Text = "";
        txt_head10.Text = "";
        txt_head10_per.Text = "";
        txt_head11.Text = "";
        txt_head11_per.Text = "";
        txt_head12.Text = "";
        txt_head12_per.Text = "";
        txt_head13.Text = "";
        txt_head13_per.Text = "";
        txt_head14.Text = "";
        txt_head14_per.Text = "";
        txt_head15.Text = "";
        txt_head15_per.Text = "";
        txt_lhead1.Text = "";
        txt_lhead2.Text = "";
        txt_lhead3.Text = "";
        txt_lhead4.Text = "";
        txt_lhead5.Text = "";
        txt_dhead1.Text = "";
        txt_dhead2.Text = "";
        txt_dhead3.Text = "";
        txt_dhead4.Text = "";
        txt_dhead5.Text = "";
        txt_dhead6.Text = "";
        txt_dhead7.Text = "";
        txt_dhead8.Text = "";
        txt_dhead9.Text = "";
        txt_dhead10.Text = "";
        txt_pfemployee.Text = "";
        txt_pfemployer.Text = "";
        txt_esicemployee.Text = "";
        txt_esicemployer.Text = "";
        txt_pfemployee_formula.Text = "";
        txt_pfemployer_formula.Text = "";
        txt_esicemployee_formula.Text = "";
        txt_esicemployer_formula.Text = "";
    }

    public void client_list()
    {
        d.con1.Open();
        try
        {
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("select client_code,client_name from pay_client_master where client_code='staff' and  COMP_CODE ='" + Session["COMP_CODE"].ToString() + "' ", d.con1);
            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client.DataSource = dt_item;
                ddl_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_client.DataValueField = dt_item.Columns[0].ToString();
                ddl_client.DataBind();
            }
            dt_item.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_client.Items.Insert(0, new ListItem("Select"));
            d.con1.Close();
        }

    }

    protected void ddl_client_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //d.con1.Open();
        //MySqlCommand cmd = new MySqlCommand("SELECT concat(UNIT_NAME,'-',state_name,'-',unit_city) as UNIT_NAME FROM pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' ORDER BY UNIT_CODE", d.con1);
        //MySqlDataReader dr = cmd.ExecuteReader();
        //while (dr.Read())
        //{
        //    ddlunitselect.Items.Add(dr[0].ToString());//ddl_banklist0.Items.Add(dr_banks[0].ToString());
        //}
        //ddlunitselect.Items.Insert(0, "ALL");
        //d.con1.Close();


        ddl_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and  branch_status = 0 ORDER BY UNIT_NAME", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_unit.DataSource = dt_item;
                ddl_unit.DataTextField = dt_item.Columns[0].ToString();
                ddl_unit.DataValueField = dt_item.Columns[1].ToString();
                ddl_unit.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            //ddl_branch_increment.Items.Insert(0, "Select");
            ddl_unit.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
}