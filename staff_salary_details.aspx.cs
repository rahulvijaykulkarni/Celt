using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class staff_salary_details : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d_head = new DAL();
    DAL d_client = new DAL();
    DAL d_unit = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        getlabelname();
        getGrideview();
        getsalarydeduction();
        if (!IsPostBack)
        {
           // employee_list();
            client_list();

        }
    }

    protected void btn_save_click(object sender, EventArgs e)
    {
        int res = 0, res1 = 0;
        try
        {
            string i_fah = d.getsinglestring("select  * from pay_staffsalary_details where emp_code='"+ddl_emp_list.SelectedValue+"' and  comp_code = '" + Session["comp_code"].ToString() + "' ");
            if (i_fah != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee already add')", true);
                return;

            }
            textdefaultvalue();
            //res1 = d.operation("delete from  pay_staffsalary_structure where comp_code='"+Session["COMP_CODE"].ToString()+"'");
            res = d.operation("Insert Into pay_staffsalary_details (`comp_code` ,`unit_code` ,`emp_code` ,`emp_name` ,`job_type`,`status`,`ctc_amount`,`head1`,`head2`,`head3`,`head4`,`head5`,`head6`,`head7`,`head8`,`head9`,`head10`,`head11`,`head12`,`head13`,`head14`,`head15`,`month_total`,`Lhead1` ,`Lhead2` ,`Lhead3` ,`Lhead4` ,`Lhead5` ,`Dhead1`,`Dhead2`,`Dhead3`,`Dhead4`,`Dhead5`,`Dhead6`,`Dhead7` ,`Dhead8`,`Dhead9`,`Dhead10`,`pf_employee_flag`,`pf_employer_flag`,`esic_employee_flag`,`esic_employer_flag`,lwf_flag,lwf_month_act,client_code) VALUES ('" + Session["COMP_CODE"].ToString() + "','"+ddl_unit.SelectedValue.ToString()+"','" + ddl_emp_list.SelectedValue + "','" + ddl_emp_list.SelectedItem + "','" + ddl_Jobtype.SelectedValue.ToString() + "','" + ddl_employmentstatus.SelectedValue.ToString() + "','" + txt_amount.Text.ToString() + "','" + txt_head1.Text.ToString() + "','" + txt_head2.Text.ToString() + "','" + txt_head3.Text.ToString() + "','" + txt_head4.Text.ToString() + "','" + txt_head5.Text.ToString() + "','" + txt_head6.Text.ToString() + "','" + txt_head7.Text.ToString() + "','" + txt_head8.Text.ToString() + "','" + txt_head9.Text.ToString() + "','" + txt_head10.Text.ToString() + "','" + txt_head11.Text.ToString() + "','" + txt_head12.Text.ToString() + "','" + txt_head13.Text.ToString() + "','" + txt_head14.Text.ToString() + "','" + txt_head15.Text.ToString() + "','" + txt_total.Text.ToString() + "','" + txt_lhead1.Text.ToString() + "','" + txt_lhead2.Text.ToString() + "','" + txt_lhead3.Text.ToString() + "','" + txt_lhead4.Text.ToString() + "','" + txt_lhead5.Text.ToString() + "','" + txt_dhead1.Text.ToString() + "','" + txt_dhead2.Text.ToString() + "','" + txt_dhead3.Text.ToString() + "','" + txt_dhead4.Text.ToString() + "','" + txt_dhead5.Text.ToString() + "','" + txt_dhead6.Text.ToString() + "','" + txt_dhead7.Text.ToString() + "','" + txt_dhead8.Text.ToString() + "','" + txt_dhead9.Text.ToString() + "','" + txt_dhead10.Text.ToString() + "','" + ddl_pfemployee.SelectedValue.ToString() + "','" + ddl_pfemployer.SelectedValue.ToString() + "','" + ddl_esicemployee.SelectedValue.ToString() + "','" + ddl_esicemployer.SelectedValue.ToString() + "','" + ddl_lwf_flage.SelectedValue.ToString() + "','" + ddl_lwfmonthact.SelectedValue.ToString() + "','"+ddl_client.SelectedValue.ToString()+"')");
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
        finally
        {
            getGrideview();
            text_clear();
        }
    }

    public void employee_list(string client_code,string unit_code) {
        d_unit.con1.Open();
        try
        {
            DataTable dt_item = new DataTable();
            MySqlDataAdapter grd = new MySqlDataAdapter("SELECT emp_code, emp_name from pay_employee_master  where COMP_CODE ='"+Session["COMP_CODE"].ToString()+"' and client_code='"+client_code+"' and unit_code='"+unit_code+"' and (left_date = '' OR left_date is null) ", d_unit.con1);
            grd.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_emp_list.DataSource = dt_item;
                ddl_emp_list.DataTextField = dt_item.Columns[1].ToString();
                ddl_emp_list.DataValueField = dt_item.Columns[0].ToString();
                ddl_emp_list.DataBind();
            }
            dt_item.Dispose();
            d_unit.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            ddl_emp_list.Items.Insert(0, new ListItem("Select"));
            d_unit.con1.Close();
        }
    
    }

    public void getGrideview()
    {

        MySqlDataAdapter dr = new MySqlDataAdapter("select `comp_code` ,`emp_code`,`emp_name` ,`job_type`,`status`,`ctc_amount`,`head1`,`head2`,`head3`,`head4`,`head5`,`head6`,`head7`,`head8`,`head9`,`head10`,`head11`,`head12`,`head13`,`head14`,`head15`,`month_total`,`Lhead1` ,`Lhead2` ,`Lhead3` ,`Lhead4` ,`Lhead5` ,`Dhead1`,`Dhead2`,`Dhead3`,`Dhead4`,`Dhead5`,`Dhead6`,`Dhead7` ,`Dhead8`,`Dhead9`,`Dhead10`,`pf_employee_flag`,`pf_employer_flag`,`esic_employee_flag`,`esic_employer_flag` from pay_staffsalary_details where comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
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

    protected void getlabelname() {
        d.con1.Open();
        try
        {
            MySqlCommand cmdheads = new MySqlCommand("SELECT * FROM pay_staffsalary_structure WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con1);
            MySqlDataReader drheads = cmdheads.ExecuteReader();
            if (drheads.Read())
            {
                lblhead1.Text = drheads.GetValue(3).ToString();
                lblhead2.Text = drheads.GetValue(5).ToString(); 
               lblhead3.Text = drheads.GetValue(7).ToString(); 
               lblhead4.Text = drheads.GetValue(9).ToString();
               lblhead5.Text = drheads.GetValue(11).ToString();
               lblhead6.Text = drheads.GetValue(13).ToString();
                 lblhead7.Text = drheads.GetValue(15).ToString();
              lblhead8.Text = drheads.GetValue(17).ToString();
              lblhead9.Text = drheads.GetValue(19).ToString();
              lblhead10.Text = drheads.GetValue(21).ToString();
              lblhead11.Text = drheads.GetValue(23).ToString();
               lblhead12.Text = drheads.GetValue(25).ToString();
              lblhead13.Text = drheads.GetValue(27).ToString();
              lblhead14.Text = drheads.GetValue(29).ToString();
              lblhead15.Text = drheads.GetValue(31).ToString();

             lbl_lhead1.Text = drheads.GetValue(33).ToString();
             lbl_lhead2.Text = drheads.GetValue(34).ToString();
             lbl_lhead3.Text = drheads.GetValue(35).ToString();
             lbl_lhead4.Text = drheads.GetValue(36).ToString();
             lbl_lhead5.Text = drheads.GetValue(37).ToString();
            
             lbl_dhead1.Text = drheads.GetValue(38).ToString();
             lbl_dhead2.Text = drheads.GetValue(39).ToString();
             lbl_dhead3.Text = drheads.GetValue(40).ToString();
             lbl_dhead4.Text = drheads.GetValue(41).ToString();
             lbl_dhead5.Text = drheads.GetValue(42).ToString();
             lbl_dhead6.Text = drheads.GetValue(43).ToString();
             lbl_dhead7.Text = drheads.GetValue(44).ToString();
             lbl_dhead8.Text = drheads.GetValue(45).ToString();
             lbl_dhead9.Text = drheads.GetValue(46).ToString();
             lbl_dhead10.Text = drheads.GetValue(47).ToString();   
            }
            if (lblhead1.Text == "") { txt_head1.Visible = false; }
            if (lblhead2.Text == "") { txt_head2.Visible = false; }
            if (lblhead3.Text == "") { txt_head3.Visible = false; }
            if (lblhead4.Text == "") { txt_head4.Visible = false; }
            if (lblhead5.Text == "") { txt_head5.Visible = false; }
            if (lblhead6.Text == "") { txt_head6.Visible = false; }
            if (lblhead7.Text == "") { txt_head7.Visible = false; }
            if (lblhead8.Text == "") { txt_head8.Visible = false; }
            if (lblhead9.Text == "") { txt_head9.Visible = false; }
            if (lblhead10.Text == "") { txt_head10.Visible = false; }
            if (lblhead11.Text == "") { txt_head11.Visible = false; }
            if (lblhead12.Text == "") { txt_head12.Visible = false; }
            if (lblhead13.Text == "") { txt_head13.Visible = false; }
            if (lblhead14.Text == "") { txt_head14.Visible = false; }
            if (lblhead15.Text == "") { txt_head15.Visible = false; }

            if (lbl_lhead1.Text == "") { txt_lhead1.Visible = false; }
            if (lbl_lhead2.Text == "") { txt_lhead2.Visible = false; }
            if (lbl_lhead3.Text == "") { txt_lhead3.Visible = false; }
            if (lbl_lhead4.Text == "") { txt_lhead4.Visible = false; }
            if (lbl_lhead5.Text == "") { txt_lhead5.Visible = false; }

            if (lbl_dhead1.Text == "") { txt_dhead1.Visible = false; }
            if (lbl_dhead2.Text == "") { txt_dhead2.Visible = false; }
            if (lbl_dhead3.Text == "") { txt_dhead3.Visible = false; }
            if (lbl_dhead4.Text == "") { txt_dhead4.Visible = false; }
            if (lbl_dhead5.Text == "") { txt_dhead5.Visible = false; }
            if (lbl_dhead6.Text == "") { txt_dhead6.Visible = false; }
            if (lbl_dhead7.Text == "") { txt_dhead7.Visible = false; }
            if (lbl_dhead8.Text == "") { txt_dhead8.Visible = false; }
            if (lbl_dhead9.Text == "") { txt_dhead9.Visible = false; }
            if (lbl_dhead10.Text == "") { txt_dhead10.Visible = false; }
            //---------------------------------------------      
  
           
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close();
        }

    
    
    }


    protected void calculate(object sender, EventArgs e)
    {
        calculate_salary();
    }

    public void calculate_salary()
    {
        System.Data.DataTable dt_heads = new System.Data.DataTable();
        dt_heads.Columns.Add("head_name", typeof(string));
        int head_count = 0;
        double totalfinal1 = 0;
        if (txt_amount.Text=="") {
            txt_amount.Text = "0";
        }
        d.con.Open();
        MySqlCommand cmd_heads = new MySqlCommand("SELECT head1,head2,head3,head4,head5,head6,head7,head8,head9,head10,head11,head12,head13,head14,head15 from pay_staffsalary_structure Where COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'", d.con);
        MySqlDataReader dr_heads = cmd_heads.ExecuteReader();
        if (dr_heads.Read())
        {
            for (int i = 0; i < 15; i++)
            {
                if (dr_heads[i].ToString() != "")
                {
                    DataRow dr_headName = dt_heads.NewRow();
                    dr_headName["head_name"] = dr_heads[i].ToString().ToUpper();
                    dt_heads.Rows.Add(dr_headName);
                    head_count = head_count + 1;
                }
            }
        }
        dr_heads.Close();
        d.con.Close();
        MySqlCommand cmd_headformula = new MySqlCommand("SELECT head1_per,head2_per,head3_per,head4_per,head5_per,head6_per,head7_per,head8_per,head9_per,head10_per,head11_per,head12_per,head13_per,head14_per,head15_per from pay_staffsalary_structure Where COMP_CODE ='" + Session["COMP_CODE"].ToString() + "'", d.con);
        d.con.Open();
        MySqlDataReader dr_headsformula = cmd_headformula.ExecuteReader();
        if (dr_headsformula.Read())
        {
            double ctc_amount = Convert.ToDouble(txt_amount.Text);
            double ctc_monthly = ctc_amount / 12;

            double basic_amt = Math.Round((ctc_monthly * Convert.ToDouble(dr_headsformula[0].ToString())) / 100, 2);
            txt_head1.Text = Math.Round(basic_amt).ToString();
            string current_head_formula = "";
            string heads_textbox = "txt_head";

            for (int i = 1; i < head_count; i++)
            {
                string EvalResult = "";
                int j = 0;
                if (dr_headsformula[i].ToString() != "")
                {
                    if (Convert.ToString(dr_headsformula[i]).Contains("%"))
                    {
                        current_head_formula = dr_headsformula[i].ToString().Replace("%", "*0.01").ToUpper();
                    }
                    else if (Convert.ToString(dr_headsformula[i]).Contains("MONTHLY INCOME"))
                    {
                        current_head_formula = dr_headsformula[i].ToString().Replace("MONTHLY INCOME", ctc_monthly.ToString()).ToUpper();
                    }
                    else
                    {
                        current_head_formula = dr_headsformula[i].ToString().ToUpper();
                    }
                    while (j < i)
                    {
                        //----- Converting Heads into head amount --------
                        if (current_head_formula.Contains(dt_heads.Rows[j]["head_name"].ToString()))
                        {
                            int controlid = j + 1;
                            string headcontrolId = heads_textbox + controlid.ToString();
                            System.Web.UI.WebControls.TextBox txtheadamt = UpdatePanel3.FindControl(headcontrolId) as System.Web.UI.WebControls.TextBox;
                            string jhead_amt = txtheadamt.Text;
                            current_head_formula = current_head_formula.Replace(dt_heads.Rows[j]["head_name"].ToString(), txtheadamt.Text.ToString());
                        }
                        j++;
                    }
                    System.Data.DataTable dtEvalExpression = new System.Data.DataTable();
                    var v = dtEvalExpression.Compute(current_head_formula, "");
                    EvalResult = v.ToString();
                    //double current_head_amt = Convert.ToDouble(current_head_formula);
                    //double current_head_amt = Math.current_head_formula);
                    string curr_head = heads_textbox + (i + 1).ToString();
                    System.Web.UI.WebControls.TextBox Currtxtheadamt = UpdatePanel3.FindControl(curr_head) as System.Web.UI.WebControls.TextBox;
                    Currtxtheadamt.Text = EvalResult;
                }
            }
        }
        for (int i = 1; i <= head_count; i++)
        {
            string heads_textbox = "txt_head";
            string headcontrolId = heads_textbox + i.ToString();
            System.Web.UI.WebControls.TextBox txtheadamt = UpdatePanel3.FindControl(headcontrolId) as System.Web.UI.WebControls.TextBox;
            string jhead_amt = txtheadamt.Text;
            totalfinal1 = totalfinal1 + Math.Round(Convert.ToDouble(jhead_amt.ToString()), 2);
            txt_total.Text = Convert.ToString(totalfinal1);
        }

        dr_heads.Close();
        d.con.Close();

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
    protected void gv_salary_details_SelectedIndexChanged(object sender, EventArgs e)
    {
         d.con1.Open();
        try
        {
            
              string emp_code=gv_salary_structure.SelectedRow.Cells[1].Text;
            MySqlCommand cmd = new MySqlCommand("SELECT `emp_name` ,emp_code,`job_type`,`status`,`ctc_amount`,`head1`,`head2`,`head3`,`head4`,`head5`,`head6`,`head7`,`head8`,`head9`,`head10`,`head11`,`head12`,`head13`,`head14`,`head15`,`month_total`,`Lhead1` ,`Lhead2` ,`Lhead3` ,`Lhead4` ,`Lhead5` ,`Dhead1`,`Dhead2`,`Dhead3`,`Dhead4`,`Dhead5`,`Dhead6`,`Dhead7` ,`Dhead8`,`Dhead9`,`Dhead10`,`pf_employee_flag`,`pf_employer_flag`,`esic_employee_flag`,`esic_employer_flag`,lwf_flag,lwf_month_act,client_code,unit_code from pay_staffsalary_details where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + emp_code + "'", d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               // ddl_emp_list.Text = dr.GetValue(0).ToString();
                ddl_emp_list.SelectedValue = dr.GetValue(1).ToString();
                ddl_Jobtype.SelectedValue = dr.GetValue(2).ToString();
                ddl_employmentstatus.SelectedValue = dr.GetValue(3).ToString();
                txt_amount.Text = dr.GetValue(4).ToString();
                txt_head1.Text = dr.GetValue(5).ToString();
                txt_head2.Text = dr.GetValue(6).ToString();
                txt_head3.Text = dr.GetValue(7).ToString();
                txt_head4.Text = dr.GetValue(8).ToString();
                txt_head5.Text = dr.GetValue(9).ToString();
                txt_head6.Text = dr.GetValue(10).ToString();
                txt_head7.Text = dr.GetValue(11).ToString();
                txt_head8.Text = dr.GetValue(12).ToString();
                txt_head9.Text = dr.GetValue(13).ToString();
                txt_head10.Text = dr.GetValue(14).ToString();
                txt_head11.Text = dr.GetValue(15).ToString();
                txt_head12.Text = dr.GetValue(16).ToString();
                txt_head13.Text = dr.GetValue(17).ToString();
                txt_head14.Text = dr.GetValue(18).ToString();
                txt_head15.Text = dr.GetValue(19).ToString();
                txt_total.Text = dr.GetValue(20).ToString();
                txt_lhead1.Text = dr.GetValue(21).ToString();
                txt_lhead2.Text = dr.GetValue(22).ToString();
                txt_lhead3.Text = dr.GetValue(23).ToString();
                txt_lhead4.Text = dr.GetValue(24).ToString();
                txt_lhead5.Text = dr.GetValue(25).ToString();
                txt_dhead1.Text = dr.GetValue(26).ToString();
                txt_dhead2.Text = dr.GetValue(27).ToString();
                txt_dhead3.Text = dr.GetValue(28).ToString();
                txt_dhead4.Text = dr.GetValue(29).ToString();
                txt_dhead5.Text = dr.GetValue(30).ToString();
                txt_dhead6.Text = dr.GetValue(31).ToString();
                txt_dhead7.Text = dr.GetValue(32).ToString();
                txt_dhead8.Text = dr.GetValue(33).ToString();
                txt_dhead9.Text = dr.GetValue(34).ToString();
                txt_dhead10.Text = dr.GetValue(35).ToString();
                ddl_pfemployee.SelectedValue = dr.GetValue(36).ToString();
                ddl_pfemployer.SelectedValue = dr.GetValue(37).ToString();
                ddl_esicemployee.SelectedValue = dr.GetValue(38).ToString();
                ddl_esicemployer.SelectedValue = dr.GetValue(39).ToString();

                ddl_lwf_flage.SelectedValue = dr.GetValue(40).ToString();
                ddl_lwfmonthact.SelectedValue = dr.GetValue(41).ToString();
                ddl_client.SelectedValue = dr.GetValue(42).ToString();
                ddl_client_SelectedIndexChanged1(null, null);
                ddl_unit.SelectedValue = dr.GetValue(43).ToString();
                ddl_unit_SelectedIndexChanged(null, null);
                ddl_emp_list.SelectedValue = dr.GetValue(1).ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con1.Close();
            btn_update.Visible = true;
            btn_add.Visible = false;
            

        }
    }


    protected void btn_update_details(object sender, EventArgs e) {
        int result = 0;
        try
        {
            textdefaultvalue();
            result = d.operation("update pay_staffsalary_details set `job_type`='" + ddl_Jobtype.SelectedValue.ToString() + "',`status`='" + ddl_employmentstatus.SelectedValue.ToString() + "',`ctc_amount`='" + txt_amount.Text.ToString() + "',`head1`='" + txt_head1.Text.ToString() + "',`head2`='" + txt_head2.Text.ToString() + "',`head3`='" + txt_head3.Text.ToString() + "',`head4`='" + txt_head4.Text.ToString() + "',`head5`='" + txt_head5.Text.ToString() + "',`head6`='" + txt_head6.Text.ToString() + "',`head7`='" + txt_head7.Text.ToString() + "',`head8`='" + txt_head8.Text.ToString() + "',`head9`='" + txt_head9.Text.ToString() + "',`head10`='" + txt_head10.Text.ToString() + "',`head11`='" + txt_head11.Text.ToString() + "',`head12`='" + txt_head12.Text.ToString() + "',`head13`='" + txt_head13.Text.ToString() + "',`head14`='" + txt_head14.Text.ToString() + "',`head15`='" + txt_head15.Text.ToString() + "',`month_total`='" + txt_total.Text.ToString() + "',`Lhead1` ='" + txt_lhead1.Text.ToString() + "',`Lhead2` ='" + txt_lhead2.Text.ToString() + "',`Lhead3` ='" + txt_lhead3.Text.ToString() + "',`Lhead4` ='" + txt_lhead4.Text.ToString() + "',`Lhead5` ='" + txt_lhead5.Text.ToString() + "',`Dhead1`='" + txt_dhead1.Text.ToString() + "',`Dhead2`='" + txt_dhead2.Text.ToString() + "',`Dhead3`='" + txt_dhead3.Text.ToString() + "',`Dhead4`='" + txt_dhead4.Text.ToString() + "',`Dhead5`='" + txt_dhead5.Text.ToString() + "',`Dhead6`='" + txt_dhead6.Text.ToString() + "',`Dhead7` ='" + txt_dhead7.Text.ToString() + "',`Dhead8`='" + txt_dhead8.Text.ToString() + "',`Dhead9`='" + txt_dhead9.Text.ToString() + "',`Dhead10`='" + txt_dhead10.Text.ToString() + "',`pf_employee_flag`='" + ddl_pfemployee.SelectedValue.ToString() + "',`pf_employer_flag`='" + ddl_pfemployer.SelectedValue.ToString() + "',`esic_employee_flag`='" + ddl_esicemployee.SelectedValue.ToString() + "',`esic_employer_flag`='" + ddl_esicemployer.SelectedValue.ToString() + "',lwf_flag='"+ddl_lwf_flage.SelectedValue.ToString()+"',lwf_month_act='"+ddl_lwfmonthact.SelectedValue.ToString()+"' where comp_code='" + Session["COMP_CODE"].ToString() + "' and emp_code='" + ddl_emp_list.SelectedValue + "'  ");
           if (result > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successfully !!');", true);
            }
       
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally {
            getGrideview();
            btn_add.Visible = true;
            btn_update.Visible = false;
            text_clear();
        }
    }

    public void text_clear() {
        ddl_emp_list.SelectedValue = "Select";
        txt_amount.Text = "";
        txt_head1.Text = "";
        txt_head2.Text = "";
        txt_head3.Text = "";
        txt_head4.Text = "";
        txt_head5.Text = "";
        txt_head6.Text = "";
        txt_head7.Text = "";
        txt_head8.Text = "";
        txt_head9.Text = "";
        txt_head10.Text = "";
        txt_head11.Text = "";
        txt_head12.Text = "";
        txt_head13.Text = "";
        txt_head14.Text = "";
        txt_head15.Text = "";
        txt_total.Text = "";
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

    }

    protected void btn_salary_process_click(object  sender,EventArgs e) {
        hidtab.Value = "1";
        double fi_basic, fi_hra, fi_child_edu, fi_convey, fi_medical, fi_other, fi_bouns;
        double one_day_basic,one_day_hra;
        double head1_oneday, head2_oneday, head3_oneday, head4_oneday, head5_oneday, head6_oneday, head7_oneday, head8_oneday, head9_oneday, head10_oneday, head11_oneday, head12_oneday, head13_oneday, head14_oneday, head15_oneday;
        double final_head1, final_head2, final_head3, final_head4, final_head5, final_head6, final_head7, final_head8, final_head9, final_head10, final_head11, final_head12, final_head13, final_head14, final_head15;

        double final_lhead1, final_lhead2, final_lhead3, final_lhead4, final_lhead5;
        double final_dhead1, final_dhead2, final_dhead3, final_dhead4, final_dhead5, final_dhead6, final_dhead7, final_dhead8, final_dhead9, final_dhead10;



        double final_pf_employee=0, final_pf_employer=0, final_esic_employee=0, final_esic_employer=0,final_ptax=0,final_net_salary=0;

        double tot_ehead,tot_ldhead,tot_ddhead;

        string[] cearn = new string[20];
        string[] ocearn = new string[20];

        string[] eearn = new string[20];

        int month = int.Parse(txt_salarymonth.Text.ToString().Substring(0, 2));
        int year = int.Parse(txt_salarymonth.Text.ToString().Substring(3, 4));

        int days = CountDay(month, year, 0, ddl_unit.SelectedValue.ToString(), Session["COMP_CODE"].ToString(), 1);

        int sundaycount = CountDay(month, year, 1, ddl_unit.SelectedValue.ToString(), Session["COMP_CODE"].ToString(), 1);

        //int days = CountDay(8, 2019, 0, "Staff", "C01", 1);

        try
        {
          //  MySqlCommand cmd2 = new MySqlCommand("select  pay_staffsalary_details.*,pay_staffsalary_structure.head1,pay_staffsalary_structure.head2,pay_staffsalary_structure.head3,pay_staffsalary_structure.head4,pay_staffsalary_structure.head5,pay_staffsalary_structure.head6,pay_staffsalary_structure.head7,pay_staffsalary_structure.head8,pay_staffsalary_structure.head9,pay_staffsalary_structure.head10,pay_staffsalary_structure.head11,pay_staffsalary_structure.head12,pay_staffsalary_structure.head13,pay_staffsalary_structure.head14,pay_staffsalary_structure.head15,pay_staffsalary_structure.Lhead1,pay_staffsalary_structure.Lhead2,pay_staffsalary_structure.Lhead3,pay_staffsalary_structure.Lhead4,pay_staffsalary_structure.Lhead5,pay_staffsalary_structure.Dhead1,pay_staffsalary_structure.Dhead2,pay_staffsalary_structure.Dhead3,pay_staffsalary_structure.Dhead4,pay_staffsalary_structure.Dhead5,pay_staffsalary_structure.Dhead6,pay_staffsalary_structure.Dhead7,pay_staffsalary_structure.Dhead8,pay_staffsalary_structure.Dhead9,pay_staffsalary_structure.Dhead10,pay_staffsalary_structure.pf_employee,pay_staffsalary_structure.pf_employer, pay_staffsalary_structure.esic_employee,pay_staffsalary_structure.esic_employer,pay_attendance_muster.`TOT_DAYS_PRESENT`,pay_attendance_muster.`TOT_DAYS_ABSENT`,pay_attendance_muster.`TOT_HALF_DAYS`,pay_attendance_muster.`TOT_LEAVES`, pay_attendance_muster.`WEEKLY_OFF`,pay_attendance_muster.`HOLIDAYS`,pay_attendance_muster.`TOT_WORKING_DAYS` ,pay_attendance_muster.`MONTH`,pay_attendance_muster.`YEAR` ,pay_attendance_muster.`TOT_CO` from pay_staffsalary_structure inner join pay_staffsalary_details on  pay_staffsalary_structure.comp_code=pay_staffsalary_details.comp_code and pay_staffsalary_structure.unit_code=pay_staffsalary_details.unit_code inner join pay_attendance_muster on pay_staffsalary_details.comp_code=pay_attendance_muster.COMP_CODE  and pay_staffsalary_details.unit_code=pay_attendance_muster.UNIT_CODE where pay_staffsalary_details.emp_code='I00327' and  pay_staffsalary_structure.comp_code='C01' and pay_attendance_muster.month='08' and pay_attendance_muster.year='2019'", d.con);

            string attendaces_flag = d.getsinglestring("select  distinct(flag) from pay_attendance_muster where comp_code='"+Session["COMP_CODE"].ToString()+"' and unit_code='"+ddl_unit.SelectedValue.ToString()+"' and month='"+month+"' and year='"+year+"'");

            if (attendaces_flag == "0" || attendaces_flag == "")
            {
                //string link = "http://localhost:50728/Celtpayroll/emp_sample.aspx";
                //string link1 = Server.MapPath("~/emp_sample.aspx");
                //string link2 = "/Celtpayroll/emp_sample.aspx";
                string link2 = "/emp_sample.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please file first " + month + "th month attendances and approve  !!!');window.location.href ='" + link2 + "';", true);
                return;
            }

            d.operation("delete from pay_staffsalary_pro where  comp_code='"+Session["COMP_CODE"].ToString()+"' and unit_code='"+ddl_unit.SelectedValue.ToString()+"' and month='"+month+"' and year='"+year+"'");

           // MySqlCommand cmd2 = new MySqlCommand("SELECT pay_staffsalary_details.Id,pay_staffsalary_details.comp_code,pay_staffsalary_details.unit_code,pay_staffsalary_details.emp_code,pay_staffsalary_details.emp_name,pay_staffsalary_details.job_type,pay_staffsalary_details.status,pay_staffsalary_details.ctc_amount,pay_staffsalary_details.head1,pay_staffsalary_details.head2,pay_staffsalary_details.head3,pay_staffsalary_details.head4,pay_staffsalary_details.head5,pay_staffsalary_details.head6,pay_staffsalary_details.head7,pay_staffsalary_details.head8,pay_staffsalary_details.head9,pay_staffsalary_details.head10,pay_staffsalary_details.head11,pay_staffsalary_details.head12,pay_staffsalary_details.head13,pay_staffsalary_details.head14,pay_staffsalary_details.head15,pay_staffsalary_details.month_total,pay_staffsalary_details.Lhead1,pay_staffsalary_details.Lhead2,pay_staffsalary_details.Lhead3,pay_staffsalary_details.Lhead4,pay_staffsalary_details.Lhead5,pay_staffsalary_details.Dhead1,pay_staffsalary_details.Dhead2,pay_staffsalary_details.Dhead3,pay_staffsalary_details.Dhead4,pay_staffsalary_details.Dhead5,pay_staffsalary_details.Dhead6,pay_staffsalary_details.Dhead7,pay_staffsalary_details.Dhead8,pay_staffsalary_details.Dhead9,pay_staffsalary_details.Dhead10,pay_staffsalary_details.pf_employee_flag,pay_staffsalary_details.pf_employer_flag,pay_staffsalary_details.esic_employee_flag,pay_staffsalary_details.esic_employer_flag,`pay_staffsalary_structure`.`head1`,`pay_staffsalary_structure`.`head2`,`pay_staffsalary_structure`.`head3`,`pay_staffsalary_structure`.`head4`,`pay_staffsalary_structure`.`head5`,`pay_staffsalary_structure`.`head6`,`pay_staffsalary_structure`.`head7`,`pay_staffsalary_structure`.`head8`,`pay_staffsalary_structure`.`head9`,`pay_staffsalary_structure`.`head10`,`pay_staffsalary_structure`.`head11`,`pay_staffsalary_structure`.`head12`,`pay_staffsalary_structure`.`head13`,`pay_staffsalary_structure`.`head14`,`pay_staffsalary_structure`.`head15`,`pay_staffsalary_structure`.`Lhead1`,`pay_staffsalary_structure`.`Lhead2`,`pay_staffsalary_structure`.`Lhead3`,`pay_staffsalary_structure`.`Lhead4`,`pay_staffsalary_structure`.`Lhead5`,`pay_staffsalary_structure`.`Dhead1`,`pay_staffsalary_structure`.`Dhead2`,`pay_staffsalary_structure`.`Dhead3`,`pay_staffsalary_structure`.`Dhead4`,`pay_staffsalary_structure`.`Dhead5`,`pay_staffsalary_structure`.`Dhead6`,`pay_staffsalary_structure`.`Dhead7`,`pay_staffsalary_structure`.`Dhead8`,`pay_staffsalary_structure`.`Dhead9`,`pay_staffsalary_structure`.`Dhead10`,`pay_staffsalary_structure`.`pf_employee`,`pay_staffsalary_structure`.`pf_employer`,`pay_staffsalary_structure`.`esic_employee`,`pay_staffsalary_structure`.`esic_employer`,`pay_attendance_muster`.`TOT_DAYS_PRESENT`,`pay_attendance_muster`.`TOT_DAYS_ABSENT`,`pay_attendance_muster`.`TOT_HALF_DAYS`,`pay_attendance_muster`.`TOT_LEAVES`,`pay_attendance_muster`.`WEEKLY_OFF`,`pay_attendance_muster`.`HOLIDAYS`,`pay_attendance_muster`.`TOT_WORKING_DAYS`,`pay_attendance_muster`.`MONTH`,`pay_attendance_muster`.`YEAR`,`pay_attendance_muster`.`TOT_CO`,`gender`,`LOCATION`,`JOINING_DATE`,`GRADE_CODE`,`BANK_HOLDER_NAME`,`original_bank_account_no`,`PF_IFSC_CODE`,`UNIT_NAME`,`UNIT_CITY`,`STATE_NAME`,`ESIC_NUMBER`,`P_TAX_NUMBER`,`PF_NUMBER`,`PAN_NUMBER`,`COMPANY_NAME`,`ADDRESS1`,`ADDRESS2`, `CITY`,`STATE`,pay_staffsalary_details.lwf_flag,pay_staffsalary_details.lwf_month_act,pf_employee_formula, pf_employer_formula, esic_employee_formula, esic_employer_formula FROM `pay_staffsalary_structure` INNER JOIN `pay_staffsalary_details` ON `pay_staffsalary_structure`.`comp_code` = `pay_staffsalary_details`.`comp_code` AND `pay_staffsalary_structure`.`unit_code` = `pay_staffsalary_details`.`unit_code` INNER JOIN `pay_attendance_muster` ON `pay_staffsalary_details`.`comp_code` = `pay_attendance_muster`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_attendance_muster`.`UNIT_CODE` and pay_staffsalary_details.emp_code = `pay_attendance_muster`.`emp_CODE` INNER JOIN `pay_employee_master` ON `pay_staffsalary_details`.`comp_code` = `pay_employee_master`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_employee_master`.`UNIT_CODE` AND `pay_staffsalary_details`.`emp_code` = `pay_employee_master`.`emp_code` INNER JOIN `pay_unit_master` ON `pay_staffsalary_details`.`comp_code` = `pay_unit_master`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_unit_master`.`UNIT_CODE` INNER JOIN `pay_company_master` ON `pay_staffsalary_details`.`comp_code` = `pay_company_master`.`COMP_CODE` WHERE `pay_staffsalary_structure`.`comp_code` = 'C01' AND `pay_staffsalary_details`.`unit_code` = 'U110' AND `pay_attendance_muster`.`month` = '08' AND `pay_attendance_muster`.`year` = '2019'", d.con1);

            MySqlCommand cmd2 = new MySqlCommand("SELECT pay_staffsalary_details.Id,pay_staffsalary_details.comp_code,pay_staffsalary_details.unit_code,pay_staffsalary_details.emp_code,pay_staffsalary_details.emp_name,pay_staffsalary_details.job_type,pay_staffsalary_details.status,pay_staffsalary_details.ctc_amount,pay_staffsalary_details.head1,pay_staffsalary_details.head2,pay_staffsalary_details.head3,pay_staffsalary_details.head4,pay_staffsalary_details.head5,pay_staffsalary_details.head6,pay_staffsalary_details.head7,pay_staffsalary_details.head8,pay_staffsalary_details.head9,pay_staffsalary_details.head10,pay_staffsalary_details.head11,pay_staffsalary_details.head12,pay_staffsalary_details.head13,pay_staffsalary_details.head14,pay_staffsalary_details.head15,pay_staffsalary_details.month_total,pay_staffsalary_details.Lhead1,pay_staffsalary_details.Lhead2,pay_staffsalary_details.Lhead3,pay_staffsalary_details.Lhead4,pay_staffsalary_details.Lhead5,pay_staffsalary_details.Dhead1,pay_staffsalary_details.Dhead2,pay_staffsalary_details.Dhead3,pay_staffsalary_details.Dhead4,pay_staffsalary_details.Dhead5,pay_staffsalary_details.Dhead6,pay_staffsalary_details.Dhead7,pay_staffsalary_details.Dhead8,pay_staffsalary_details.Dhead9,pay_staffsalary_details.Dhead10,pay_staffsalary_details.pf_employee_flag,pay_staffsalary_details.pf_employer_flag,pay_staffsalary_details.esic_employee_flag,pay_staffsalary_details.esic_employer_flag,`pay_staffsalary_structure`.`head1`,`pay_staffsalary_structure`.`head2`,`pay_staffsalary_structure`.`head3`,`pay_staffsalary_structure`.`head4`,`pay_staffsalary_structure`.`head5`,`pay_staffsalary_structure`.`head6`,`pay_staffsalary_structure`.`head7`,`pay_staffsalary_structure`.`head8`,`pay_staffsalary_structure`.`head9`,`pay_staffsalary_structure`.`head10`,`pay_staffsalary_structure`.`head11`,`pay_staffsalary_structure`.`head12`,`pay_staffsalary_structure`.`head13`,`pay_staffsalary_structure`.`head14`,`pay_staffsalary_structure`.`head15`,`pay_staffsalary_structure`.`Lhead1`,`pay_staffsalary_structure`.`Lhead2`,`pay_staffsalary_structure`.`Lhead3`,`pay_staffsalary_structure`.`Lhead4`,`pay_staffsalary_structure`.`Lhead5`,`pay_staffsalary_structure`.`Dhead1`,`pay_staffsalary_structure`.`Dhead2`,`pay_staffsalary_structure`.`Dhead3`,`pay_staffsalary_structure`.`Dhead4`,`pay_staffsalary_structure`.`Dhead5`,`pay_staffsalary_structure`.`Dhead6`,`pay_staffsalary_structure`.`Dhead7`,`pay_staffsalary_structure`.`Dhead8`,`pay_staffsalary_structure`.`Dhead9`,`pay_staffsalary_structure`.`Dhead10`,`pay_staffsalary_structure`.`pf_employee`,`pay_staffsalary_structure`.`pf_employer`,`pay_staffsalary_structure`.`esic_employee`,`pay_staffsalary_structure`.`esic_employer`,`pay_attendance_muster`.`TOT_DAYS_PRESENT`,`pay_attendance_muster`.`TOT_DAYS_ABSENT`,`pay_attendance_muster`.`TOT_HALF_DAYS`,`pay_attendance_muster`.`TOT_LEAVES`,`pay_attendance_muster`.`WEEKLY_OFF`,`pay_attendance_muster`.`HOLIDAYS`,`pay_attendance_muster`.`TOT_WORKING_DAYS`,`pay_attendance_muster`.`MONTH`,`pay_attendance_muster`.`YEAR`,`pay_attendance_muster`.`TOT_CO`,`gender`,`LOCATION`,`JOINING_DATE`,`GRADE_CODE`,`BANK_HOLDER_NAME`,`original_bank_account_no`,`PF_IFSC_CODE`,`UNIT_NAME`,`UNIT_CITY`,`STATE_NAME`,`ESIC_NUMBER`,`P_TAX_NUMBER`,`PF_NUMBER`,`PAN_NUMBER`,`COMPANY_NAME`,`ADDRESS1`,`ADDRESS2`, `CITY`,`STATE`,pay_staffsalary_details.lwf_flag,pay_staffsalary_details.lwf_month_act,pf_employee_formula, pf_employer_formula, esic_employee_formula, esic_employer_formula,pay_staffsalary_details. `client_code` FROM `pay_staffsalary_structure` INNER JOIN `pay_staffsalary_details` ON `pay_staffsalary_structure`.`comp_code` = `pay_staffsalary_details`.`comp_code` AND `pay_staffsalary_structure`.`unit_code` = `pay_staffsalary_details`.`unit_code` INNER JOIN `pay_attendance_muster` ON `pay_staffsalary_details`.`comp_code` = `pay_attendance_muster`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_attendance_muster`.`UNIT_CODE` and pay_staffsalary_details.emp_code = `pay_attendance_muster`.`emp_CODE` INNER JOIN `pay_employee_master` ON `pay_staffsalary_details`.`comp_code` = `pay_employee_master`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_employee_master`.`UNIT_CODE` AND `pay_staffsalary_details`.`emp_code` = `pay_employee_master`.`emp_code` INNER JOIN `pay_unit_master` ON `pay_staffsalary_details`.`comp_code` = `pay_unit_master`.`COMP_CODE` AND `pay_staffsalary_details`.`unit_code` = `pay_unit_master`.`UNIT_CODE` INNER JOIN `pay_company_master` ON `pay_staffsalary_details`.`comp_code` = `pay_company_master`.`COMP_CODE` WHERE `pay_staffsalary_structure`.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_staffsalary_details`.`unit_code` = '" + ddl_unit.SelectedValue.ToString() + "' AND `pay_attendance_muster`.`month` = '" + month + "' AND `pay_attendance_muster`.`year` = '" + year + "'", d.con1);
           
            d.con1.Open();
            MySqlDataReader dr2 = cmd2.ExecuteReader();

            while(dr2.Read())
            {


                //head1_oneday = double.Parse(dr2.GetValue(8).ToString()) / days;
                //final_head1 = Math.Round((double.Parse(dr2.GetValue(8).ToString()) / days) * double.Parse(dr2.GetValue(78).ToString()),2);

                //head2_oneday = double.Parse(dr2.GetValue(9).ToString()) / days;
                //final_head2 = Math.Round(head2_oneday * double.Parse(dr2.GetValue(78).ToString()),2);

              //  final_head1 = Math.Round((double.Parse(dr2.GetValue(8).ToString()) / days) * double.Parse(dr2.GetValue(77).ToString()), 2);
               double totalpersentday=
                final_head1 = Math.Round((double.Parse(dr2.GetValue(8).ToString()))/ days * (double.Parse(dr2.GetValue(77).ToString())+sundaycount), 2);
               final_head2 = Math.Round((double.Parse(dr2.GetValue(9).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head3 = Math.Round((double.Parse(dr2.GetValue(10).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head4 = Math.Round((double.Parse(dr2.GetValue(11).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head5 = Math.Round((double.Parse(dr2.GetValue(12).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head6 = Math.Round((double.Parse(dr2.GetValue(13).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head7 = Math.Round((double.Parse(dr2.GetValue(14).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head8 = Math.Round((double.Parse(dr2.GetValue(15).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head9 = Math.Round((double.Parse(dr2.GetValue(16).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head10 = Math.Round((double.Parse(dr2.GetValue(17).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head11 = Math.Round((double.Parse(dr2.GetValue(18).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head12 = Math.Round((double.Parse(dr2.GetValue(19).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head13 = Math.Round((double.Parse(dr2.GetValue(20).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head14 = Math.Round((double.Parse(dr2.GetValue(21).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);
               final_head15 = Math.Round((double.Parse(dr2.GetValue(22).ToString())) / days * (double.Parse(dr2.GetValue(77).ToString()) + sundaycount), 2);

                for (int k = 0; k < 15; k++)
                {
                    cearn[k] = "0";
                    ocearn[k] = "0";
                    eearn[k] = "0";
                }

                try { 
                d_head.con.Open();
                MySqlCommand cmd4 = new MySqlCommand("select `head1`, `head2`, `head3`, `head4`, `head5`, `head6`, `head7`, `head8`, `head9`, `head10`, `head11`, `head12`, `head13`, `head14`, `head15` from pay_staffsalary_details where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND EMP_CODE='" + dr2.GetValue(3).ToString() + "'", d_head.con);
                MySqlDataReader dr4 = cmd4.ExecuteReader();
                //--------------------
                while (dr4.Read())
                {
                    for (int i = 0; i < 15; i++)
                    {
                        if (dr2.GetValue(5).ToString() == "permanent")
                        {
                            cearn[i + 1] = Math.Round(((float.Parse(dr4.GetValue(i).ToString()) / days) * float.Parse(dr2.GetValue(77).ToString())), 0).ToString();
                            //eearn[i + 1] = (((float.Parse(dr4.GetValue(i).ToString()) / cmonthdays) * extradays)).ToString();
                          //  ocearn[i + 1] = ((((float.Parse(dr4.GetValue(i).ToString()) / otcalenderdays) / 8) * cothrs)).ToString();
                        }
                        else
                        {
                            //if (cstatus == "Monthly")
                            //{
                            //    cearn[i + 1] = Math.Round(((float.Parse(dr4.GetValue(i).ToString()) / cmonthdays) * cpayabledays), 0).ToString();
                            //    eearn[i + 1] = (((float.Parse(dr4.GetValue(i).ToString()) / cmonthdays) * extradays)).ToString();
                            //    ocearn[i + 1] = ((((float.Parse(dr4.GetValue(i).ToString()) / otcalenderdays) / 8) * cothrs)).ToString();
                            //}
                            //else
                            //{
                            //    cearn[i + 1] = Math.Round((float.Parse(dr4.GetValue(i).ToString()) * cpayabledays), 0).ToString();
                            //    eearn[i + 1] = ((float.Parse(dr4.GetValue(i).ToString()) * extradays)).ToString();

                            //    ocearn[i + 1] = (((float.Parse(dr4.GetValue(i).ToString()) / 8) * cothrs)).ToString();

                            //}
                        }
                    }


                }
                dr4.Dispose();
                d_head.con.Close();
                }
                catch (Exception ee) { throw ee; }
                finally
                {
                    
                    d_head.con.Close();
                }


                //pf emplpoyee start
                float pfgross = 0;
                string pfformula1 = "";
                float pfslab = 15000;
                float calpf = 0;
                float calcomppf = 0;
                float pfslabpension = 15000;
                float calcomppen = 0;
                if (dr2.GetValue(39).ToString() == "yes")
                {
                    if (dr2.GetValue(108).ToString() == "ALL")
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            pfgross = pfgross + float.Parse(cearn[i]);
                        }
                    }
                    else
                    {
                        pfformula1 = dr2.GetValue(108).ToString();
                        int length = pfformula1.Length;
                        int count1 = 0; int j = 0;
                        string[] str = new string[20];// { }; // { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string[] heads = new string[20];// { };// { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string earning_head;
                        while (pfformula1.IndexOf("+") + 1 > 0)
                        {
                            int index = pfformula1.IndexOf("+");
                            str[j] = pfformula1.Substring(0, index);
                            earning_head = cearn[Convert.ToInt32(str[j].ToString())];
                            pfgross = pfgross + float.Parse(earning_head);// Convert.ToDouble(earning_head);
                            pfformula1 = pfformula1.Remove(0, index + 1);
                            count1++;
                            j++;
                        }
                        str[j] = pfformula1;
                        earning_head = cearn[Convert.ToInt32(str[j].ToString())];
                        pfgross = pfgross + float.Parse(earning_head); 
                    }
                }
                else
                {
                    pfgross = 0;
                }
               // pfgross = pfgross + dadiff;
                Math.Round(pfgross, 0);

                if (pfgross > pfslab)
                {
                    pfgross = pfslab;
                }
                                               // pf percentage
                calpf = pfgross * float.Parse(dr2.GetValue(73).ToString()) / 100;
                //decimal calpf1;
                final_pf_employee = Math.Round(calpf);
                // final_pf_employee
                if (pfgross > pfslab)
                {
                    pfgross = pfslab;
                    //calcomppf = 541;
                    calcomppf = float.Parse(pfslabpension.ToString());

                    //calcomppen = float.Parse(calpf1.ToString()) - calcomppf;
                    calcomppen = (float)final_pf_employee - calcomppf;
                }
                else
                {
                    calcomppf = float.Parse((pfgross * 0.0833).ToString());
                    if (calcomppf > pfslabpension)
                    {
                        //calcomppf = 541;
                        calcomppf = pfslabpension;
                    }

                    //calcomppen = float.Parse(calpf1.ToString()) - calcomppf;
                    calcomppen = (float)final_pf_employee - calcomppf;
                }
                decimal calcomppen1 = decimal.Round(Convert.ToDecimal(calcomppen));
                decimal calcomppf1 = decimal.Round(Convert.ToDecimal(calcomppf));

                //pf employee end


                //pf emplpoyer start
                float pfgrossemplpoyer = 0;
                string pfformulaemplpoyer = "";
                float pfslabemplpoyer = 15000;
                float calpfemplpoyer = 0;
                float calcomppfemplpoyer = 0;
                float pfslabpensionemplpoyer = 15000;
                float calcomppenemplpoyer = 0;
                if (dr2.GetValue(40).ToString() == "yes")
                {
                    if (dr2.GetValue(109).ToString() == "ALL")
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            pfgrossemplpoyer = pfgrossemplpoyer + float.Parse(cearn[i]);
                        }
                    }
                    else
                    {
                        pfformulaemplpoyer = dr2.GetValue(109).ToString();
                        int length = pfformulaemplpoyer.Length;
                        int count1 = 0; int j = 0;
                        string[] str = new string[20];// { }; // { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string[] heads = new string[20];// { };// { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string earning_head;
                        while (pfformulaemplpoyer.IndexOf("+") + 1 > 0)
                        {
                            int index = pfformulaemplpoyer.IndexOf("+");
                            str[j] = pfformulaemplpoyer.Substring(0, index);
                            earning_head = cearn[Convert.ToInt32(str[j].ToString())];
                            pfgrossemplpoyer = pfgrossemplpoyer + float.Parse(earning_head);// Convert.ToDouble(earning_head);
                            pfformulaemplpoyer = pfformulaemplpoyer.Remove(0, index + 1);
                            count1++;
                            j++;
                        }
                        str[j] = pfformulaemplpoyer;
                        earning_head = cearn[Convert.ToInt32(str[j].ToString())];
                        pfgrossemplpoyer = pfgrossemplpoyer + float.Parse(earning_head);
                    }
                }
                else
                {
                    pfgrossemplpoyer = 0;
                }
                // pfgross = pfgross + dadiff;
                Math.Round(pfgrossemplpoyer, 0);

                if (pfgrossemplpoyer > pfslabemplpoyer)
                {
                    pfgrossemplpoyer = pfslabemplpoyer;
                }
                // pf percentage
                calpfemplpoyer = pfgrossemplpoyer * float.Parse(dr2.GetValue(74).ToString()) / 100;
                //decimal calpf1;
                final_pf_employer = Math.Round(calpfemplpoyer);
                // final_pf_employee
                //if (pfgrossemplpoyer > pfslabemplpoyer)
                //{
                //    pfgrossemplpoyer = pfslabemplpoyer;
                //    //calcomppf = 541;
                //    calcomppfemplpoyer = float.Parse(pfslabpensionemplpoyer.ToString());

                //    //calcomppen = float.Parse(calpf1.ToString()) - calcomppf;
                //    calcomppenemplpoyer = (float)final_pf_employer - calcomppfemplpoyer;
                //}
                //else
                //{
                //    calcomppfemplpoyer = float.Parse((pfgrossemplpoyer * 0.0833).ToString());
                //    if (calcomppfemplpoyer > pfslabpensionemplpoyer)
                //    {
                //        //calcomppf = 541;
                //        calcomppfemplpoyer = pfslabpensionemplpoyer;
                //    }

                //    //calcomppen = float.Parse(calpf1.ToString()) - calcomppf;
                //    calcomppenemplpoyer = (float)final_pf_employer - calcomppfemplpoyer;
                //}
                //decimal calcomppenemplpoyer = decimal.Round(Convert.ToDecimal(calcomppenemplpoyer));
                //decimal calcomppfemplpoyer = decimal.Round(Convert.ToDecimal(calcomppf));

                //pf employer end

                 //esic employee calculate start
                float esicgross = 0;
                string esicformula1 = "";
                float esicslab = 21000;
                float calesic = 0;
                float calesicot = 0;
                float calesictot = 0;
                float esiccompcontri = 0;
                double calesictot1 = 0;
                if (dr2.GetValue(41).ToString() == "yes")
                {
                    if (dr2.GetValue(110).ToString() == "ALL")
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            esicgross = esicgross + float.Parse(cearn[i]);
                        }
                    }
                    else
                    {
                        esicformula1 = dr2.GetValue(110).ToString();
                        int lengthesic = esicformula1.Length;
                        int count1esic = 0; int jesic = 0;
                        string[] stresic = new string[20];// { }; // { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string[] headesic = new string[20];// { };// { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string earning_headesic;
                        while (esicformula1.IndexOf("+") > 0)
                        {
                            int indexesic = esicformula1.IndexOf("+");
                            stresic[jesic] = esicformula1.Substring(0, indexesic);
                            earning_headesic = cearn[Convert.ToInt32(stresic[jesic].ToString())];
                            esicgross = esicgross + float.Parse(earning_headesic);
                            //   otgross = otgross + float.Parse(earning_headesic);// Convert.ToDouble(earning_head);
                            esicformula1 = esicformula1.Remove(0, indexesic + 1);
                            count1esic++;
                            jesic++;
                        }
                        stresic[jesic] = esicformula1;
                        earning_headesic = cearn[Convert.ToInt32(stresic[jesic].ToString())];
                        esicgross = esicgross + float.Parse(earning_headesic); ;// Convert.ToDouble(earning_head);formula1 = otformula;
                    }
                    //esicgross = esicgross + cattendancebonus + extragross + dadiff;
                    //esicgross = esicgross ;

                }
                else
                {
                    esicgross = 0;
                }

                Math.Round(esicgross, 0);

                if (esicgross < esicslab)
                {
                    calesic = esicgross * (float.Parse(dr2.GetValue(75).ToString()) / 100);
                    //calesic1 = decimal.Round(Convert.ToDecimal(calesic));
                    final_esic_employee = Math.Round(calesic);
                    //if (esiconotformula == "Y")
                    //{
                    //    if (employee_esic_flag.Equals("Yes") || employee_esic_flag.Equals("YES"))
                    //    {
                    //        esicgross = otgross + esicgross;
                    //        calesicotgross = otgross;
                    //        calesicot = otgross * (esicpercentage / 100);
                    //        calesicot1 = decimal.Round(Convert.ToDecimal(calesicot));
                    //    }
                    //}
                    //employee_esic_flag = "";
                    //calesictot = calesic + calesicot;
                    //if (calesictot - Math.Truncate(calesictot) > 0)
                    //{
                    //    calesictot = Convert.ToInt32(Math.Truncate(calesictot) + 1);
                    //}
                    //calesictot1 = Math.Round(calesictot, 0);
                }
                else {
                    final_esic_employee = 0;
                }


               
                //esiccompcontri = esicgross * (float.Parse(dr2.GetValue(76).ToString())/ 100);


                //esic employee calculate end

                //esic employer calculate start
                float esicgrossemployer = 0;
                string esicformulaemployer = "";
                float esicslabemployer = 21000;
                float calesicemployer = 0;
                float calesicotemployer = 0;
                float calesictotemployer = 0;
                float esiccompcontriemployer = 0;
                
                if (dr2.GetValue(42).ToString() == "yes")
                {
                    if (dr2.GetValue(111).ToString() == "ALL")
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            esicgrossemployer = esicgrossemployer + float.Parse(cearn[i]);
                        }
                    }
                    else
                    {
                        esicformulaemployer = dr2.GetValue(111).ToString();
                        int lengthesic = esicformulaemployer.Length;
                        int count1esic = 0; int jesic = 0;
                        string[] stresic = new string[20];// { }; // { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string[] headesic = new string[20];// { };// { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        string earning_headesic;
                        while (esicformulaemployer.IndexOf("+") > 0)
                        {
                            int indexesic = esicformulaemployer.IndexOf("+");
                            stresic[jesic] = esicformulaemployer.Substring(0, indexesic);
                            earning_headesic = cearn[Convert.ToInt32(stresic[jesic].ToString())];
                            esicgrossemployer = esicgrossemployer + float.Parse(earning_headesic);
                            //   otgross = otgross + float.Parse(earning_headesic);// Convert.ToDouble(earning_head);
                            esicformulaemployer = esicformulaemployer.Remove(0, indexesic + 1);
                            count1esic++;
                            jesic++;
                        }
                        stresic[jesic] = esicformulaemployer;
                        earning_headesic = cearn[Convert.ToInt32(stresic[jesic].ToString())];
                        esicgrossemployer = esicgrossemployer + float.Parse(earning_headesic); ;// Convert.ToDouble(earning_head);formula1 = otformula;
                    }
                    //esicgross = esicgross + cattendancebonus + extragross + dadiff;
                    //esicgross = esicgross ;

                }
                else
                {
                    esicgrossemployer = 0;
                }

                Math.Round(esicgrossemployer, 0);

                if (esicgrossemployer < esicslabemployer)
                {
                    calesicemployer = esicgrossemployer * (float.Parse(dr2.GetValue(76).ToString()) / 100);
                    //calesic1 = decimal.Round(Convert.ToDecimal(calesic));
                    final_esic_employer = Math.Round(calesicemployer);
                    esiccompcontri = esicgross * (float.Parse(dr2.GetValue(76).ToString()) / 100);
                    //if (esiconotformula == "Y")
                    //{
                    //    if (employee_esic_flag.Equals("Yes") || employee_esic_flag.Equals("YES"))
                    //    {
                    //        esicgross = otgross + esicgross;
                    //        calesicotgross = otgross;
                    //        calesicot = otgross * (esicpercentage / 100);
                    //        calesicot1 = decimal.Round(Convert.ToDecimal(calesicot));
                    //    }
                    //}
                    //employee_esic_flag = "";
                    //calesictot = calesic + calesicot;
                    //if (calesictot - Math.Truncate(calesictot) > 0)
                    //{
                    //    calesictot = Convert.ToInt32(Math.Truncate(calesictot) + 1);
                    //}
                    //calesictot1 = Math.Round(calesictot, 0);
                }
                else {
                    final_esic_employer = 0;
                    esiccompcontri = 0;
                }


                //esic employee calculate end


                //total lhead
                tot_ldhead = double.Parse(dr2.GetValue(24).ToString()) + double.Parse(dr2.GetValue(25).ToString()) + double.Parse(dr2.GetValue(26).ToString()) + double.Parse(dr2.GetValue(27).ToString()) + double.Parse(dr2.GetValue(28).ToString());

                //total ddhead
                tot_ddhead = double.Parse(dr2.GetValue(29).ToString()) + double.Parse(dr2.GetValue(30).ToString()) + double.Parse(dr2.GetValue(31).ToString()) + double.Parse(dr2.GetValue(32).ToString()) + double.Parse(dr2.GetValue(33).ToString()) + double.Parse(dr2.GetValue(34).ToString()) + double.Parse(dr2.GetValue(35).ToString()) + double.Parse(dr2.GetValue(36).ToString()) + double.Parse(dr2.GetValue(37).ToString()) + double.Parse(dr2.GetValue(38).ToString());

                // total ehead 
                tot_ehead = final_head1 + final_head2 + final_head3 + final_head4 + final_head5 + final_head6 + final_head7 + final_head8 + final_head9 + final_head10 + final_head11 + final_head12 + final_head13 + final_head14 + final_head15;

                

                // PT Tax Calculation
                string pttaxget = d.getsinglestring("SELECT SLAB_AMOUNT FROM PAY_PT_SLAB_MASTER WHERE (STATE_NAME = 'Maharashtra') AND ( '" + tot_ehead + "' BETWEEN FROM_AMOUNT AND TO_AMOUNT)");
              
                if(pttaxget == "" ){
                final_ptax = 0;
                
                }else {
                final_ptax = double.Parse(pttaxget);
                }
                
              //  final_ptax = double.Parse(pttaxget);
                 // end PT

                    
                    //LWF
                string temp = "";
                double calmlwf = 0;
               string lwfflag = "yes";

               if (dr2.GetValue(106).ToString() == "yes")
                {
                    temp = d.getsinglestring("SELECT employee_contribution FROM pay_master_lwf WHERE STATE_NAME in (Select State_name from pay_unit_master where unit_code = '"+ddl_unit.SelectedValue.ToString()+"' and comp_code = '" + Session["COMP_CODE"].ToString() + "')");
                    if (temp != "")
                    {
                        calmlwf = float.Parse(temp);
                    }
                    else
                    {
                        calmlwf = 0;
                    }
                }
                else {
                    calmlwf = 0;
                }
                
                // final Net Salary
                final_net_salary=tot_ehead - final_pf_employee- final_pf_employer - final_esic_employee - final_esic_employer - final_ptax - calmlwf;


              //  d.operation("Insert into pay_staffsalary_pro (`comp_code`,`unit_code`,`emp_code`,`emp_name`,`designation`,`ctc_amount`,`de_head1`,`de_head2`,`de_head3`,`de_head4`,`de_head5`,`de_head6`,`de_head7`,`de_head8`,`de_head9`,`de_head10`,`de_head11`,`de_head12`,`de_head13`,`de_head14`,`de_head15`,`tot_de_head`,`dl_head1`,`dl_head2`,`dl_head3`,`dl_head4`,`dl_head5`,`tot_dl_head`,`dd_head1`,`dd_head2`,`dd_head3`,`dd_head4`,`dd_head5`,`dd_head6`,`dd_head7`,`dd_head8`,`dd_head9`,`dd_head10`,`tot_dd_head`,`dpf_employee`,`dpf_employer`,`desic_employee`,`desic_employer`,`dlwf`,`dptax`,`dgross`,`net_salary`,`month`,`year`,`grade`,`Bank_holder_name`,`bank_ac_no`,`bank_ifsc_code`,`unit_name`,`unit_city`,`unit_state`,`pan_no`,`pf_no`,`esic_no`,`uan_no`,`comp_name`,`comp_address1`,`comp_address2`,`comp_city`,`comp_state`,`tot_days_persent`,`tot_days_absent`,`tot_half_days`,`tot_leaves`,`weekly_off`,`holidays`,`tot_working_days`,`month_days`,`joining_date`,pf_gross,comp_pf,comp_pf_pension,`esic_gross`,`tot_esic_gross`,`esic_comp_contribution`) values ('" + Session["COMP_CODE"].ToString() + "','" + dr2.GetValue(2).ToString() + "','" + dr2.GetValue(3).ToString() + "','" + dr2.GetValue(4).ToString() + "','" + dr2.GetValue(90).ToString() + "','" + dr2.GetValue(7).ToString() + "','" + final_head1 + "','" + final_head2 + "','" + final_head3 + "','" + final_head4 + "','" + final_head5 + "','" + final_head6 + "','" + final_head7 + "','" + final_head8 + "','" + final_head9 + "','" + final_head10 + "','" + final_head11 + "','" + final_head12 + "','" + final_head13 + "','" + final_head14 + "','" + final_head15 + "', '" + tot_ehead + "','" + dr2.GetValue(24).ToString() + "','" + dr2.GetValue(25).ToString() + "','" + dr2.GetValue(26).ToString() + "','" + dr2.GetValue(27).ToString() + "','" + dr2.GetValue(28).ToString() + "','" + tot_ldhead + "','" + dr2.GetValue(29).ToString() + "','" + dr2.GetValue(30).ToString() + "','" + dr2.GetValue(31).ToString() + "','" + dr2.GetValue(32).ToString() + "','" + dr2.GetValue(33).ToString() + "','" + dr2.GetValue(34).ToString() + "','" + dr2.GetValue(35).ToString() + "','" + dr2.GetValue(36).ToString() + "','" + dr2.GetValue(37).ToString() + "','" + dr2.GetValue(38).ToString() + "','" + tot_ddhead + "','" + final_pf_employee + "','" + final_pf_employer + "','" + final_esic_employee + "','" + final_esic_employer + "','" + calmlwf + "','" + final_ptax + "','','"+final_net_salary+"','8','2019','" + dr2.GetValue(90).ToString() + "','" + dr2.GetValue(91).ToString() + "','" + dr2.GetValue(92).ToString() + "','" + dr2.GetValue(93).ToString() + "','" + dr2.GetValue(94).ToString() + "','" + dr2.GetValue(95).ToString() + "','" + dr2.GetValue(96).ToString() + "','','" + dr2.GetValue(99).ToString() + "','" + dr2.GetValue(97).ToString() + "','" + dr2.GetValue(100).ToString() + "','" + dr2.GetValue(101).ToString() + "','" + dr2.GetValue(102).ToString() + "','" + dr2.GetValue(103).ToString() + "','" + dr2.GetValue(104).ToString() + "','" + dr2.GetValue(105).ToString() + "','" + dr2.GetValue(77).ToString() + "','" + dr2.GetValue(78).ToString() + "','" + dr2.GetValue(79).ToString() + "','" + dr2.GetValue(80).ToString() + "','" + dr2.GetValue(81).ToString() + "','" + dr2.GetValue(82).ToString() + "','" + dr2.GetValue(83).ToString() + "','','" + dr2.GetValue(89).ToString() + "'," + pfgross + "," + calcomppf1 + "," + calcomppen1 + "," + esicgross + "," + esicgross + "," + esiccompcontri + ")");

                d.operation("Insert into pay_staffsalary_pro (`comp_code`,`unit_code`,`emp_code`,`emp_name`,`designation`,`ctc_amount`,`de_head1`,`de_head2`,`de_head3`,`de_head4`,`de_head5`,`de_head6`,`de_head7`,`de_head8`,`de_head9`,`de_head10`,`de_head11`,`de_head12`,`de_head13`,`de_head14`,`de_head15`,`tot_de_head`,`dl_head1`,`dl_head2`,`dl_head3`,`dl_head4`,`dl_head5`,`tot_dl_head`,`dd_head1`,`dd_head2`,`dd_head3`,`dd_head4`,`dd_head5`,`dd_head6`,`dd_head7`,`dd_head8`,`dd_head9`,`dd_head10`,`tot_dd_head`,`dpf_employee`,`dpf_employer`,`desic_employee`,`desic_employer`,`dlwf`,`dptax`,`dgross`,`net_salary`,`month`,`year`,`grade`,`Bank_holder_name`,`bank_ac_no`,`bank_ifsc_code`,`unit_name`,`unit_city`,`unit_state`,`pan_no`,`pf_no`,`esic_no`,`uan_no`,`comp_name`,`comp_address1`,`comp_address2`,`comp_city`,`comp_state`,`tot_days_persent`,`tot_days_absent`,`tot_half_days`,`tot_leaves`,`weekly_off`,`holidays`,`tot_working_days`,`month_days`,`joining_date`,pf_gross,comp_pf,comp_pf_pension,`esic_gross`,`tot_esic_gross`,`esic_comp_contribution`,client_code) values ('" + Session["COMP_CODE"].ToString() + "','" + dr2.GetValue(2).ToString() + "','" + dr2.GetValue(3).ToString() + "','" + dr2.GetValue(4).ToString() + "','" + dr2.GetValue(90).ToString() + "','" + dr2.GetValue(7).ToString() + "','" + final_head1 + "','" + final_head2 + "','" + final_head3 + "','" + final_head4 + "','" + final_head5 + "','" + final_head6 + "','" + final_head7 + "','" + final_head8 + "','" + final_head9 + "','" + final_head10 + "','" + final_head11 + "','" + final_head12 + "','" + final_head13 + "','" + final_head14 + "','" + final_head15 + "', '" + tot_ehead + "','" + dr2.GetValue(24).ToString() + "','" + dr2.GetValue(25).ToString() + "','" + dr2.GetValue(26).ToString() + "','" + dr2.GetValue(27).ToString() + "','" + dr2.GetValue(28).ToString() + "','" + tot_ldhead + "','" + dr2.GetValue(29).ToString() + "','" + dr2.GetValue(30).ToString() + "','" + dr2.GetValue(31).ToString() + "','" + dr2.GetValue(32).ToString() + "','" + dr2.GetValue(33).ToString() + "','" + dr2.GetValue(34).ToString() + "','" + dr2.GetValue(35).ToString() + "','" + dr2.GetValue(36).ToString() + "','" + dr2.GetValue(37).ToString() + "','" + dr2.GetValue(38).ToString() + "','" + tot_ddhead + "','" + final_pf_employee + "','" + final_pf_employer + "','" + final_esic_employee + "','" + final_esic_employer + "','" + calmlwf + "','" + final_ptax + "','','" + final_net_salary + "','" + month + "','" + year + "','" + dr2.GetValue(90).ToString() + "','" + dr2.GetValue(91).ToString() + "','" + dr2.GetValue(92).ToString() + "','" + dr2.GetValue(93).ToString() + "','" + dr2.GetValue(94).ToString() + "','" + dr2.GetValue(95).ToString() + "','" + dr2.GetValue(96).ToString() + "','','" + dr2.GetValue(99).ToString() + "','" + dr2.GetValue(97).ToString() + "','" + dr2.GetValue(100).ToString() + "','" + dr2.GetValue(101).ToString() + "','" + dr2.GetValue(102).ToString() + "','" + dr2.GetValue(103).ToString() + "','" + dr2.GetValue(104).ToString() + "','" + dr2.GetValue(105).ToString() + "','" + dr2.GetValue(77).ToString() + "','" + dr2.GetValue(78).ToString() + "','" + dr2.GetValue(79).ToString() + "','" + dr2.GetValue(80).ToString() + "','" + dr2.GetValue(81).ToString() + "','" + dr2.GetValue(82).ToString() + "','" + dr2.GetValue(83).ToString() + "','','" + dr2.GetValue(89).ToString() + "'," + pfgross + "," + calcomppf1 + "," + calcomppen1 + "," + esicgross + "," + esicgross + "," + esiccompcontri + ",'" + dr2.GetValue(112).ToString() + "')");

            }
            dr2.Close();
            d.con1.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + month + "th month salary calculated successfully ...')", true);
            return;
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally {
            
            d.con1.Close();
            getsalarydeduction();
        }
    }

    public void getsalarydeduction()
    {
         d_head.con1.Open();
         //MySqlCommand cmd4 = new MySqlCommand("select `pay_staffsalary_structure`.`id`,`pay_staffsalary_structure`.`head1`, `pay_staffsalary_structure`.`head2`, `pay_staffsalary_structure`.`head3`, `pay_staffsalary_structure`.`head4`, `pay_staffsalary_structure`.`head5`, `pay_staffsalary_structure`.`head6`, `pay_staffsalary_structure`.`head7`, `pay_staffsalary_structure`.`head8`, `pay_staffsalary_structure`.`head9`, `pay_staffsalary_structure`.`head10`, `pay_staffsalary_structure`.`head11`, `pay_staffsalary_structure`.`head12`, `pay_staffsalary_structure`.`head13`, `pay_staffsalary_structure`.`head14`, `pay_staffsalary_structure`.`head15`, `pay_staffsalary_structure`.`Lhead1`, `pay_staffsalary_structure`.`Lhead2`, `pay_staffsalary_structure`.`Lhead3`, `pay_staffsalary_structure`.`Lhead4`, `pay_staffsalary_structure`.`Lhead5`, `pay_staffsalary_structure`.`Dhead1`, `pay_staffsalary_structure`.`Dhead2`, `pay_staffsalary_structure`.`Dhead3`, `pay_staffsalary_structure`.`Dhead4`, `pay_staffsalary_structure`.`Dhead5`, `pay_staffsalary_structure`.`Dhead6`, `pay_staffsalary_structure`.`Dhead7`, `pay_staffsalary_structure`.`Dhead8`, `pay_staffsalary_structure`.`Dhead9`, `pay_staffsalary_structure`.`Dhead10` from  `pay_staffsalary_structure` where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'  and client_code='"+ddl_client.SelectedValue.ToString()+"' and unit_code='"+ddl_unit.SelectedValue.ToString()+"'", d_head.con1);
         MySqlCommand cmd4 = new MySqlCommand("select `pay_staffsalary_structure`.`id`,`pay_staffsalary_structure`.`head1`, `pay_staffsalary_structure`.`head2`, `pay_staffsalary_structure`.`head3`, `pay_staffsalary_structure`.`head4`, `pay_staffsalary_structure`.`head5`, `pay_staffsalary_structure`.`head6`, `pay_staffsalary_structure`.`head7`, `pay_staffsalary_structure`.`head8`, `pay_staffsalary_structure`.`head9`, `pay_staffsalary_structure`.`head10`, `pay_staffsalary_structure`.`head11`, `pay_staffsalary_structure`.`head12`, `pay_staffsalary_structure`.`head13`, `pay_staffsalary_structure`.`head14`, `pay_staffsalary_structure`.`head15`, `pay_staffsalary_structure`.`Lhead1`, `pay_staffsalary_structure`.`Lhead2`, `pay_staffsalary_structure`.`Lhead3`, `pay_staffsalary_structure`.`Lhead4`, `pay_staffsalary_structure`.`Lhead5`, `pay_staffsalary_structure`.`Dhead1`, `pay_staffsalary_structure`.`Dhead2`, `pay_staffsalary_structure`.`Dhead3`, `pay_staffsalary_structure`.`Dhead4`, `pay_staffsalary_structure`.`Dhead5`, `pay_staffsalary_structure`.`Dhead6`, `pay_staffsalary_structure`.`Dhead7`, `pay_staffsalary_structure`.`Dhead8`, `pay_staffsalary_structure`.`Dhead9`, `pay_staffsalary_structure`.`Dhead10` from  `pay_staffsalary_structure` where COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d_head.con1);
         
        MySqlDataReader dr2 = cmd4.ExecuteReader();
                //--------------------
                while (dr2.Read())
                {
                    MySqlDataAdapter dr = new MySqlDataAdapter("select  * from pay_staffsalary_pro where comp_code='" + Session["COMP_CODE"].ToString() + "'", d.con);
                    d.con.Open();
                    DataTable dt = new DataTable();
                    //DataSet DS1 = new DataSet();
                    dr.Fill(dt);
                    if (dr2.GetValue(1).ToString() == "" || dr2.GetValue(1).ToString() == null)
                    { dt.Columns.Remove("de_head1"); } else 
                    { dt.Columns["de_head1"].ColumnName = dr2.GetValue(1).ToString(); }
                    if (dr2.GetValue(2).ToString() == "" || dr2.GetValue(2).ToString() == null) { dt.Columns.Remove("de_head2"); } else { dt.Columns["de_head2"].ColumnName = dr2.GetValue(2).ToString(); }
                    if (dr2.GetValue(3).ToString() == "" || dr2.GetValue(3).ToString() == null) { dt.Columns.Remove("de_head3"); } else { dt.Columns["de_head3"].ColumnName = dr2.GetValue(3).ToString(); }
                    if (dr2.GetValue(4).ToString() == "" || dr2.GetValue(4).ToString() == null) { dt.Columns.Remove("de_head4"); } else { dt.Columns["de_head4"].ColumnName = dr2.GetValue(4).ToString(); }
                    if (dr2.GetValue(5).ToString() == "" || dr2.GetValue(5).ToString() == null) { dt.Columns.Remove("de_head5"); } else { dt.Columns["de_head5"].ColumnName = dr2.GetValue(5).ToString(); }
                    if (dr2.GetValue(6).ToString() == "" || dr2.GetValue(6).ToString() == null) { dt.Columns.Remove("de_head6"); } else { dt.Columns["de_head6"].ColumnName = dr2.GetValue(6).ToString(); }
                    if (dr2.GetValue(7).ToString() == "" || dr2.GetValue(7).ToString() == null) { dt.Columns.Remove("de_head7"); } else { dt.Columns["de_head7"].ColumnName = dr2.GetValue(7).ToString(); }
                    if (dr2.GetValue(8).ToString() == "" || dr2.GetValue(8).ToString() == null) { dt.Columns.Remove("de_head8"); } else { dt.Columns["de_head8"].ColumnName = dr2.GetValue(8).ToString(); }
                    if (dr2.GetValue(9).ToString() == "" || dr2.GetValue(9).ToString() == null) { dt.Columns.Remove("de_head9"); } else { dt.Columns["de_head9"].ColumnName = dr2.GetValue(9).ToString(); }
                    if (dr2.GetValue(10).ToString() == "" || dr2.GetValue(10).ToString() == null) { dt.Columns.Remove("de_head10"); } else { dt.Columns["de_head10"].ColumnName = dr2.GetValue(10).ToString(); }
                    if (dr2.GetValue(11).ToString() == "" || dr2.GetValue(11).ToString() == null) { dt.Columns.Remove("de_head11"); } else { dt.Columns["de_head11"].ColumnName = dr2.GetValue(11).ToString(); }
                    if (dr2.GetValue(12).ToString() == "" || dr2.GetValue(12).ToString() == null) { dt.Columns.Remove("de_head12"); } else { dt.Columns["de_head12"].ColumnName = dr2.GetValue(12).ToString(); }
                    if (dr2.GetValue(13).ToString() == "" || dr2.GetValue(13).ToString() == null) { dt.Columns.Remove("de_head13"); } else { dt.Columns["de_head13"].ColumnName = dr2.GetValue(13).ToString(); }
                    if (dr2.GetValue(14).ToString() == "" || dr2.GetValue(14).ToString() == null) { dt.Columns.Remove("de_head14"); } else { dt.Columns["de_head14"].ColumnName = dr2.GetValue(14).ToString(); }
                    if (dr2.GetValue(15).ToString() == "" || dr2.GetValue(15).ToString() == null) { dt.Columns.Remove("de_head15"); } else { dt.Columns["de_head15"].ColumnName = dr2.GetValue(15).ToString(); }
                    if (dr2.GetValue(16).ToString() == "" || dr2.GetValue(16).ToString() == null) { dt.Columns.Remove("dl_head1"); } else { dt.Columns["dl_head1"].ColumnName = dr2.GetValue(16).ToString(); }
                    if (dr2.GetValue(17).ToString() == "" || dr2.GetValue(17).ToString() == null) { dt.Columns.Remove("dl_head2"); } else { dt.Columns["dl_head2"].ColumnName = dr2.GetValue(17).ToString(); }
                    if (dr2.GetValue(18).ToString() == "" || dr2.GetValue(18).ToString() == null) { dt.Columns.Remove("dl_head3"); } else { dt.Columns["dl_head3"].ColumnName = dr2.GetValue(18).ToString(); }
                    if (dr2.GetValue(19).ToString() == "" || dr2.GetValue(19).ToString() == null) { dt.Columns.Remove("dl_head4"); } else { dt.Columns["dl_head4"].ColumnName = dr2.GetValue(19).ToString(); }
                    if (dr2.GetValue(20).ToString() == "" || dr2.GetValue(20).ToString() == null) { dt.Columns.Remove("dl_head5"); } else { dt.Columns["dl_head5"].ColumnName = dr2.GetValue(20).ToString(); }
                    if (dr2.GetValue(21).ToString() == "" || dr2.GetValue(21).ToString() == null) { dt.Columns.Remove("dd_head1"); } else { dt.Columns["dd_head1"].ColumnName = dr2.GetValue(21).ToString(); }
                    if (dr2.GetValue(22).ToString() == "" || dr2.GetValue(22).ToString() == null) { dt.Columns.Remove("dd_head2"); } else { dt.Columns["dd_head2"].ColumnName = dr2.GetValue(22).ToString(); }
                    if (dr2.GetValue(23).ToString() == "" || dr2.GetValue(23).ToString() == null) { dt.Columns.Remove("dd_head3"); } else { dt.Columns["dd_head3"].ColumnName = dr2.GetValue(23).ToString(); }
                    if (dr2.GetValue(24).ToString() == "" || dr2.GetValue(24).ToString() == null) { dt.Columns.Remove("dd_head4"); } else { dt.Columns["dd_head4"].ColumnName = dr2.GetValue(24).ToString(); }
                    if (dr2.GetValue(25).ToString() == "" || dr2.GetValue(25).ToString() == null) { dt.Columns.Remove("dd_head5"); } else { dt.Columns["dd_head5"].ColumnName = dr2.GetValue(25).ToString(); }
                    if (dr2.GetValue(26).ToString() == "" || dr2.GetValue(26).ToString() == null) { dt.Columns.Remove("dd_head6"); } else { dt.Columns["dd_head6"].ColumnName = dr2.GetValue(26).ToString(); }
                    if (dr2.GetValue(27).ToString() == "" || dr2.GetValue(27).ToString() == null) { dt.Columns.Remove("dd_head7"); } else { dt.Columns["dd_head7"].ColumnName = dr2.GetValue(27).ToString(); }
                    if (dr2.GetValue(28).ToString() == "" || dr2.GetValue(28).ToString() == null) { dt.Columns.Remove("dd_head8"); } else { dt.Columns["dd_head8"].ColumnName = dr2.GetValue(28).ToString(); }
                    if (dr2.GetValue(29).ToString() == "" || dr2.GetValue(29).ToString() == null) { dt.Columns.Remove("dd_head9"); } else { dt.Columns["dd_head9"].ColumnName = dr2.GetValue(29).ToString(); }
                    if (dr2.GetValue(30).ToString() == "" || dr2.GetValue(30).ToString() == null) { dt.Columns.Remove("dd_head10"); } else { dt.Columns["dd_head10"].ColumnName = dr2.GetValue(30).ToString(); }


                    gv_salary_deuction.DataSource = dt;
                    gv_salary_deuction.DataBind();
                    d.con.Close();
                
                
                }
                dr2.Dispose();
                d_head.con1.Close();

    }

    int CountDay(int month, int year, int counter, string client_code, string comp_code, int from_to_date)
    {

        //string start_date_common = d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.comp_code = '" + comp_code + "' AND month = '" + month + "' AND year = '" + year + "' and type = 'billing' and pay_unit_master.branch_status = 0 limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + client_code + "' AND  pay_unit_master.comp_code  = '" + comp_code + "' and pay_unit_master.branch_status = 0 limit 1))");
        //string end_date_common = d.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.comp_code = '" + comp_code + "' AND month = '" + month + "' AND year = '" + year + "' and type = 'billing' and pay_unit_master.branch_status = 0 limit 1), (SELECT end_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + client_code + "' AND  pay_unit_master.comp_code  = '" + comp_code + "' and pay_unit_master.branch_status = 0 limit 1))");

        string start_date_common = "1";
        string end_date_common = "31";


        int NoOfSunday = 0;

        var firstDay = (dynamic)null;
        
        if (from_to_date == 1) { start_date_common = "1"; }
        if (start_date_common != "1")
        {
            firstDay = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            
        }
        else
        {

            firstDay = new DateTime(year, month, 1);
        }

        var day29 = firstDay.AddDays(28);
        var day30 = firstDay.AddDays(29);
        var day31 = firstDay.AddDays(30);
        if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
       || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
       || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
        {
            NoOfSunday = 5;
        }
        else
        {
            NoOfSunday = 4;
        }

        int NumOfDay = 0;
        if (start_date_common != "1")
        {


            var start_date = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            var end_date = new DateTime(year, (month), int.Parse(end_date_common));
            if ((end_date.Date - start_date.Date).Days == 29)
            {
                day31 = day30;
            }
            if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
        || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
        || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
            {
                NoOfSunday = 5;
            }
            else
            {
                NoOfSunday = 4;
            }

            NumOfDay = (end_date.Date - start_date.Date).Days;
            NumOfDay = ++NumOfDay;
        }
        else
        {
            NumOfDay = DateTime.DaysInMonth(year, month);
        }

        
        if (counter == 1)
        {//calendar days
           // return NumOfDay;
            return NoOfSunday;
        }
        else
        { //working days
          //  return NumOfDay - NoOfSunday;
            return NumOfDay ;
        }
    }

    protected void btn_attendace_muster_click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
    }
    protected void gv_salary_deuction_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_salary_deuction.UseAccessibleHeader = false;
            gv_salary_deuction.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void btn_close_click1(object sender, EventArgs e)
    {
       // string newdate = txt_dhead10.Text.Replace(@"/", @"");
        Response.Redirect("Home.aspx");
    }
    // ComputeSha256Hash 
    protected void btn_close_click(object sender ,EventArgs e)
    {
        //string newPassword = sha("Rahul");
        StringBuilder stringbulider = new StringBuilder();
        int result = 0;
        try
        {
            MySqlCommand cmd2 = new MySqlCommand("select emp_code,emp_name,date_format(birth_date,'%d/%m/%Y') from pay_employee_master where comp_code='C01'  and unit_code='U951'", d.con1);
            d.con1.Open();
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
               // string subStringname = dr2.GetValue(1).ToString().Substring(0, 4)+"@1234";
                //string[] dob = dr2.GetValue(2).ToString().Split('/',' ');
                string dob=dr2.GetValue(2).ToString().Replace(@"/",@"");
                string plainData = dob;
                //Console.WriteLine("Raw data: {0}", plainData);
                string hashedData = ComputeSha256Hash(plainData);
               // stringbulider.Append(dr2.GetValue(0).ToString()+":"+hashedData+",");
                stringbulider.Append("update pay_user_master set `user_password`='" + hashedData + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and login_id='" + dr2.GetValue(0).ToString() + "'" + ";");

                result = d.operation("update pay_user_master set `user_password`='" + hashedData + "' where comp_code='" + Session["COMP_CODE"].ToString() + "' and login_id='" + dr2.GetValue(0).ToString() + "'  ");
          
            }
            dr2.Close();
            d.con1.Close();
            string newpasswordadd = stringbulider.ToString();
        }
        catch (Exception e1)
        {
            throw e1;
        }
        finally {
            d.con1.Close();
        }
        //Console.WriteLine("Hash {0}", hashedData);
        //Console.WriteLine(ComputeSha256Hash("Mahesh"));
        //Console.ReadLine();  
    }

    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
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

        ddl_unit.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and  branch_status = 0 ORDER BY UNIT_NAME", d_client.con);
        d_client.con.Open();
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
            d_client.con.Close();
            //ddl_branch_increment.Items.Insert(0, "Select");
            ddl_unit.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d_client.con.Close();
        }
    }

    protected void ddl_unit_SelectedIndexChanged(object s,EventArgs e) {

        employee_list(ddl_client.SelectedValue.ToString(),ddl_unit.SelectedValue.ToString());

    }

    public void textdefaultvalue() {

        if (txt_head1.Text.ToString() == "") { txt_head1.Text = "0"; }
        if (txt_head2.Text.ToString() == "") { txt_head2.Text = "0"; }
        if (txt_head3.Text.ToString() == "") { txt_head3.Text = "0"; }
        if (txt_head4.Text.ToString() == "") { txt_head4.Text = "0"; }
        if (txt_head5.Text.ToString() == "") { txt_head5.Text = "0"; }
        if (txt_head6.Text.ToString() == "") { txt_head6.Text = "0"; }
        if (txt_head7.Text.ToString() == "") { txt_head7.Text = "0"; }
        if (txt_head8.Text.ToString() == "") { txt_head8.Text = "0"; }
        if (txt_head9.Text.ToString() == "") { txt_head9.Text = "0"; }
        if (txt_head10.Text.ToString() == "") { txt_head10.Text = "0"; }
        if (txt_head11.Text.ToString() == "") { txt_head11.Text = "0"; }
        if (txt_head12.Text.ToString() == "") { txt_head12.Text = "0"; }
        if (txt_head13.Text.ToString() == "") { txt_head13.Text = "0"; }
        if (txt_head14.Text.ToString() == "") { txt_head14.Text = "0"; }
        if (txt_head15.Text.ToString() == "") { txt_head15.Text = "0"; }
        if (txt_total.Text.ToString() == "") { txt_total.Text = "0"; }
        if (txt_lhead1.Text.ToString() == "") { txt_lhead1.Text = "0"; }
        if (txt_lhead2.Text.ToString() == "") { txt_lhead2.Text = "0"; }
        if (txt_lhead3.Text.ToString() == "") { txt_lhead3.Text = "0"; }
        if (txt_lhead4.Text.ToString() == "") { txt_lhead4.Text = "0"; }
        if (txt_lhead5.Text.ToString() == "") { txt_lhead5.Text = "0"; }
        if (txt_dhead1.Text.ToString() == "") { txt_dhead1.Text = "0"; }
        if (txt_dhead2.Text.ToString() == "") { txt_dhead2.Text = "0"; }
        if (txt_dhead3.Text.ToString() == "") { txt_dhead3.Text = "0"; }
        if (txt_dhead4.Text.ToString() == "") { txt_dhead4.Text = "0"; }
        if (txt_dhead5.Text.ToString() == "") { txt_dhead5.Text = "0"; }
        if (txt_dhead6.Text.ToString() == "") { txt_dhead6.Text = "0"; }
        if (txt_dhead7.Text.ToString() == "") { txt_dhead7.Text = "0"; }
        if (txt_dhead8.Text.ToString() == "") { txt_dhead8.Text = "0"; }
        if (txt_dhead9.Text.ToString() == "") { txt_dhead9.Text = "0"; }
        if (txt_dhead10.Text.ToString() == "") { txt_dhead10.Text = "0"; }

    }

    //public static String sha(string string1) {
    //    /*if (TextUtils.isEmpty(string)) {
    //        return "";
    //    }*/
    //    MessageDigest md5 = null;
    //    try {
    //        md5 = MessageDigest.getInstance("sha-256");
    //        byte[] bytes = md5.digest((string1).getBytes());
    //        String result = "";
    //        for (byte b : bytes) {
    //            string temp = Integer.toHexString(b & 0xff);
    //            if (temp.length() == 1) {
    //                temp = "0" + temp;
    //            }
    //            result += temp;
    //        }
    //        return result;
    //    } catch (NoSuchAlgorithmException e) {
    //        e.printStackTrace();
    //    }
    //    return "";
    //}
}