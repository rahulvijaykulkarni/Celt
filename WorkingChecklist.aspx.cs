using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class WorkingChecklist : System.Web.UI.Page
{
    DAL d1 = new DAL();
   DAL d = new DAL();
   protected void Page_Load(object sender, EventArgs e)
   {
       if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
       {
           Response.Redirect("Login_Page.aspx");
       }
       if (!IsPostBack)
       {
           btnadd.Visible = true;
           btndelete.Visible = false;
           btn_close.Visible = true;
           btn_UPDATE.Visible = false;



          //s UnitGridView.Visible = true;
           d.con1.Open();
           try
           {

               ddl_grade.Items.Clear();
               DataTable dt_item = new DataTable();
               MySqlDataAdapter grd = new MySqlDataAdapter("SELECT GRADE_DESC,GRADE_CODE from pay_grade_master WHERE comp_code ='" + Session["comp_code"].ToString() + "' ", d.con1);

               grd.Fill(dt_item);
               if (dt_item.Rows.Count > 0)
               {
                   ddl_grade.DataSource = dt_item;
                   ddl_grade.DataTextField = dt_item.Columns[0].ToString();
                   ddl_grade.DataValueField = dt_item.Columns[1].ToString();
                   ddl_grade.DataBind();
               }
               dt_item.Dispose();
              
           }
           catch (Exception ex) { throw ex; }
           finally
           {
               d.con1.Close();
               ddl_grade.Items.Insert(0, new ListItem("Select", "Select"));

           }

          
       }
       gridbind();
      
     
   }
   public void gridbind()
   {
       gv_working_checklist.DataSource = null;
       gv_working_checklist.DataBind();

       MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, GRADE_DESC  AS 'grade',description,type,time  FROM pay_working_master   INNER JOIN  pay_grade_master  ON  pay_working_master . comp_code  =  pay_grade_master . comp_code  AND  pay_working_master . grade  =  pay_grade_master . GRADE_CODE    where pay_working_master.comp_code = '" + Session["comp_code"].ToString() + "'", d.con);
       d.con.Open();
       try
       {
           MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
           if (dr_hd.HasRows)
           {
               DataTable dt = new DataTable();
               dt.Load(dr_hd);

               gv_working_checklist.DataSource = dt;
               gv_working_checklist.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }


   



   protected void btnadd_click(object sender, EventArgs e)

   {

       try { 
       int result = 0;
       //result = d.operation("insert into pay_working_master(comp_code,grade,description,type,time)values('" + Session["comp_code"].ToString() + "','"+ddl_grade.SelectedValue +"','"+ txt_description.Text+"','"+ddl_type.SelectedValue+"','"+ txt_time.Text+"')");


       result = d.operation("insert into pay_working_master(comp_code,grade,description,type,time)values('" + Session["comp_code"].ToString() + "','" + ddl_grade.SelectedValue + "','" + txt_description.Text + "','" + ddl_type.SelectedValue + "','" + txt_time.Text + "')");
       
       
       if (result > 0)
       {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Succsefully... !!!');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Faill.... !!!');", true);
       }
       
       }
       catch (Exception ee) {
       
       }
       finally {
           gridbind();
           //ddl_grade.Items.Clear();
           txt_description.Text = "";
           //ddl_type.Items.Clear();
           txt_time.Text = "";
       }
 }


   protected void fill_gridview_OnSelectedIndexChanged(object sender, EventArgs e)
   {
       string id = gv_working_checklist.SelectedRow.Cells[0].Text;
       d.con.Open();
       MySqlCommand cmd = new MySqlCommand("select comp_code,grade,description,type,time,Id from pay_working_master where Id='" + id + "'", d.con);
       MySqlDataReader dr = cmd.ExecuteReader();
       if (dr.Read())
       {
           ddl_grade.SelectedValue = dr.GetValue(1).ToString();
           txt_description.Text = dr.GetValue(2).ToString();
           ddl_type.SelectedValue = dr.GetValue(3).ToString();
           txt_time.Text = dr.GetValue(4).ToString();
           txt_id.Text = dr.GetValue(5).ToString();
       }
       dr.Close();
       d.con.Close();
       btndelete.Visible = true;
       btn_UPDATE.Visible = true;
       btnadd.Visible = false;
       // txt_clear();
   }




   protected void btn_UPDATE_click(object sender, EventArgs e)

   {
       try {
           string id_client = "";

           id_client = d.getsinglestring("Select group_concat(Id) from pay_client_gps_policy where comp_code = '" + Session["comp_code"].ToString() + "' and type ='" + ddl_type.SelectedItem + "' and grade = '" + ddl_grade.SelectedValue + "' and description='" + txt_description.Text+ "'");
           id_client = "'" + id_client + "'";
           string policy_idd = d.getsinglestring("Select  Id , type , grade , description , time , 1_time , 2_time , 3_time , 4_time , 5_time , 6_time , 7_time , 8_time , 9_time , 10_time , 11_time , 12_time  from pay_client_gps_policy where comp_code = '" + Session["comp_code"].ToString() + "' and type ='" + ddl_type.SelectedItem + "' and grade = '" + ddl_grade.SelectedValue + "' and Id = " + id_client + " ");
           if (policy_idd.Trim() == "")
           {

               string id = gv_working_checklist.SelectedRow.Cells[0].Text;

               int result = 0;
               result = d.operation("update pay_working_master set  grade='" + ddl_grade.SelectedValue + "', description='" + txt_description.Text + "', type='" + ddl_type.SelectedValue + "',time='" + txt_time.Text + "' where Id ='" + id + "' ");

               if (result > 0)
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Update succsefully... !!!');", true);
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Update faill.... !!!');", true);

               }
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Policy All Ready Created You Can not Updated ... !!!');", true);
               return;
           }
       }
       catch (Exception ee) { }
       finally {
           gridbind();
           //ddl_grade.Items.Clear();
           ddl_grade.SelectedValue = "Select";
           txt_description.Text = "";
           //ddl_type.Items.Clear();
           txt_time.Text = "";
       }
      
   
   }


   protected void btndelete_click(object sender, EventArgs e)

   {
       //MySqlCommand master_daily = new MySqlCommand(" Select  Id , type , grade , description , time , 1_time , 2_time , 3_time , 4_time , 5_time , 6_time , 7_time , 8_time , 9_time , 10_time , 11_time , 12_time  from pay_client_gps_policy where  cgpm_id ='" + id + "' and type ='Daily' ", d.con);
     //  string checklist_policy = "";
      // checklist_policy =
       string policy_id = d.getsinglestring("Select  Id , type , grade , description , time , 1_time , 2_time , 3_time , 4_time , 5_time , 6_time , 7_time , 8_time , 9_time , 10_time , 11_time , 12_time  from pay_client_gps_policy where  type ='" + ddl_type.SelectedItem + "'and comp_code = '" + Session["comp_code"].ToString() + "' and grade ='" + ddl_grade.SelectedValue + "'and description='" + txt_description.Text+ "'");
       if (policy_id == "")
       {

           int result = 0;
           result = d.operation("delete from pay_working_master where Id='" + txt_id.Text + "' ");
           if (result > 0)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted succsefully... !!!');", true);
           }
           else
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted faill.... !!!');", true);
           }

           gridbind();
           //ddl_grade.Items.Clear();
           ddl_grade.SelectedValue = "Select";
           txt_description.Text = "";
           //ddl_type.Items.Clear();
           txt_time.Text = "";
       }
       else {
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Policy All Ready Created You Can not Deleted ... !!!');", true);
           return;
       
       }
   }







   protected void gv_working_checklist_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       for (int i = 0; i < e.Row.Cells.Count; i++)
       {
           if (e.Row.Cells[i].Text == "&nbsp;")
           {
               e.Row.Cells[i].Text = "";
           }
       }
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
           e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
           e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_working_checklist, "Select$" + e.Row.RowIndex);
          
       }
       e.Row.Cells[0].Visible = false;

   }

   protected void btnclose_Click(object sender, EventArgs e)
   {
       Response.Redirect("ClientPolicy.aspx");
     
   }
   

    //protected void pf_account_details()
    //{
        //MySqlCommand cmd = new MySqlCommand("select Total from  pay_company_pf_details where type='Employer'", d.con);
        //d.con.Open();
        //try
        //{
        //    MySqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {

        //        //txt_bill_pf_percent.Text = dr.GetValue(0).ToString();
        //        dr.Close();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //finally
        //{

        //    d.con.Close();
            //txt_bill_pf_percent.ReadOnly = true;
    //    }
    //}
    //protected void pf_account_details1()
    //{
    //    MySqlCommand cmd = new MySqlCommand("select Total from  pay_company_pf_details where type='Employee'", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.Read())
    //        {

    //            //txt_sal_pf.Text = dr.GetValue(0).ToString();
    //            dr.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        d.con.Close();
    //        //txt_sal_pf.ReadOnly = true;
    //    }
    //}
    //protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    //{
        //ddl_clientwisestate.Items.Clear();
        // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
    //    MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code is null ORDER BY STATE", d1.con);//and state in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "')
    //    d1.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
    //        while (dr_item1.Read())
    //        {
    //            //ddl_clientwisestate.Items.Add(dr_item1[0].ToString());

    //        }
    //        dr_item1.Close();
    //       // ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));

    //        //ddl_unitcode.Items.Clear();
    //        //ddl_designation.Items.Clear();
    //        load_grdview();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d1.con.Close();
    //    }

       


    //}
    //protected void client_list()
    //{
    //    d.con1.Open();
    //    try
    //    {
    //        MySqlDataAdapter cmd = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  ORDER BY client_code", d.con1);//AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
    //        DataTable dt = new DataTable();
    //        cmd.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            //ddl_unit_client.DataSource = dt;
    //            //ddl_unit_client.DataTextField = dt.Columns[0].ToString();
    //            //ddl_unit_client.DataValueField = dt.Columns[1].ToString();
    //            //ddl_unit_client.DataBind();

    //        }
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {

    //        d.con1.Close();
    //        //ddl_unit_client.Items.Insert(0, "Select");
    //    }




    //}

    //private void load_grdview()
    //{
    //    //07-01-19
    //    d.con1.Open();
    //    try
    //    {
    //        MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select pay_billing_master.id, policy_name1 as 'Policy',client_name as 'Client',unit_name as 'Branch',billing_state as 'State', (Select grade_desc from pay_grade_master where grade_code= pay_billing_master.Designation and comp_code='" + Session["comp_code"].ToString() + "') as Designation, cast(concat(hours,' Hrs') as char) as 'Working Hours', date_format(start_date,'%d/%m/%Y') as 'Policy Start Date', date_format(end_date,'%d/%m/%Y') as 'Policy End Date' from pay_billing_master inner join pay_client_master on pay_client_master.client_code = pay_billing_master.billing_client_code and pay_client_master.comp_code = pay_billing_master.comp_code inner join pay_unit_master on pay_unit_master.unit_code = pay_billing_master.billing_unit_code and pay_unit_master.comp_code = pay_billing_master.comp_code where billing_client_code = '" + ddl_unit_client.SelectedValue + "' and pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.branch_status = 0", d.con1);
    //        DataTable DS1 = new DataTable();
    //        MySqlDataAdapter1.Fill(DS1);
    //        //grd_policy.DataSource = DS1;
    //        //grd_policy.DataBind();
    //        d.con1.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { d.con1.Close(); }
    //}

    //protected void btnadd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //        //IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
            //                                     where item.Selected
            //                                     select item.Value;
            //string listvalues_ddl_unitcode = string.Join(",", selectedValues);

            //selectedValues = from item in ddl_unitcode_without.Items.Cast<ListItem>()
            //                 where item.Selected
            //                 select item.Value;
            //if (listvalues_ddl_unitcode == "") { listvalues_ddl_unitcode = string.Join(",", selectedValues); }
            //else
            //{
            //    listvalues_ddl_unitcode = listvalues_ddl_unitcode + "," + string.Join(",", selectedValues);
            //}


            //IEnumerable<string> designation = from item in ddl_designation.Items.Cast<ListItem>()
            //                                  where item.Selected
            //                                  select item.Value;
            //string listvalues_ddl_designation = string.Join(",", designation);


            //string temp = d1.getsinglestring("select distinct(POLICY_NAME1) FROM pay_billing_master where POLICY_NAME1 = '" + txt_policy_name1.Text + "'");
            //if (temp == txt_policy_name1.Text)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Name Already Exist !!');", true);
            //    return;
            //}

            //d.operation("delete from pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and POLICY_NAME1 = '" + txt_policy_name1.Text + "'");


            //var elements = listvalues_ddl_unitcode.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            //var elements1 = listvalues_ddl_designation.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

    //        int res = 0;
    //        //foreach (string unit_code in elements)
    //        {
    //            foreach (string designation1 in elements1)
    //            {
    //                string temp1 = d1.getsinglestring("select cast(group_concat(pay_grade_master.grade_code) as char) from pay_designation_count inner join pay_grade_master on pay_grade_master.grade_desc = pay_designation_count.designation and pay_grade_master.comp_code = pay_designation_count.comp_code where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.client_code= '" + ddl_unit_client.SelectedValue + "' and pay_designation_count.unit_code = '" + unit_code + "'");
    //                if (temp1.Contains(designation1))
    //                {
    //                    d.operation("delete from pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and billing_unit_code = '" + unit_code + "' and designation = '" + designation1 + "'");
    //                //    res = d.operation("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount) values ('" + Session["COMP_CODE"] + "','" + ddl_bill_ser_uniform.SelectedValue + "','" + ddl_bonus_taxable_billing.SelectedValue + "','" + designation1 + "','" + ddl_esic_ot.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + ddl_lwf_applicable.SelectedValue + "','" + ddl_month_calc.SelectedValue + "','" + ddl_ot_applicable.SelectedValue + "','" + ddl_pt_applicable.SelectedValue + "','" + txt_allowances_billing.Text + "','" + txt_basic_billing.Text + "','" + txt_bill_bonus_percent.Text + "','" + txt_bill_esic_percent.Text + "','" + txt_bill_oper_cost_amt.Text + "','" + txt_bill_oper_cost_percent.Text + "','" + txt_bill_pf_percent.Text + "','" + txt_bill_relieving.Text + "','" + txt_bill_service_charge.Text + "','" + txt_bill_uniform_percent.Text + "','" + txt_bill_uniform_rate.Text + "','" + txt_cca_billing.Text + "','" + txt_education_billing.Text + "',str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_hra_amount.Text + "','" + txt_hra_percent.Text + "','" + txt_leave_days.Text + "','" + txt_leave_days_percent.Text + "','" + txt_other_allow.Text + "','" + txt_per_rate_billing.Text + "','" + txt_policy_name1.Text + "','" + txt_sal_bonus.Text + "','" + txt_sal_esic.Text + "','" + txt_sal_pf.Text + "','" + txt_sal_uniform_percent.Text + "','" + txt_sal_uniform_rate.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),'" + txt_travelling_billing.Text + "','" + txt_vda_billing.Text + "','" + txt_washing_billing.Text + "','" + txt_basic_vda_billing.Text + "','" + ddl_leave_taxable_billing.SelectedValue + "','" + ddl_gratuity_taxable_billing.SelectedValue + "','" + ddl_bonus_taxable_salary.SelectedValue + "', '" + txt_allowances_salary.Text + "', '" + txt_basic_salary.Text + "', '" + txt_cca_salary.Text + "', '" + txt_education_salary.Text + "', '" + txt_per_rate_salary.Text + "', '" + txt_travelling_salary.Text + "', '" + txt_vda_salary.Text + "', '" + txt_washing_salary.Text + "','" + Session["USERNAME"].ToString() + "',now(),'" + txt_bonus_amount_salary.Text + "','" + ddl_leave_taxable_salary.SelectedValue + "','" + ddl_bonus_policy_aap_salary.SelectedValue + "','" + txt_gratuity_percent_billing.Text + "','" + txt_national_holidays_billing.Text + "','" + ddl_bonus_policy_aap_billing.SelectedValue + "','" + txt_bonus_amount_billing.Text + "','" + ddl_bill_ser_operations.SelectedValue + "','" + ddl_unit_client.SelectedValue + "','" + unit_code + "','" + ddl_clientwisestate.SelectedValue + "','" + txt_sal_graguity_per.Text + "','" + txt_leave_days_salary.Text + "','" + txt_leave_days_percent_salary.Text + "','" + txt_start_date_common.SelectedValue + "','" + txt_end_date_common.Text + "','" + ddl_ot_policy.SelectedValue + "','" + txt_ot_amount.Text + "','" + ddl_gratuity_taxable_salary.SelectedValue + "','" + ddl_ot_policy_billing.SelectedValue + "','" + txt_ot_amount_billing.Text + "','" + txt_hra_amount_salary.Text + "','" + txt_hra_percent_salary.Text + "','" + ddl_relieving_uniform.SelectedValue + "','" + ddl_esic_oa_billing.SelectedValue + "','" + ddl_esic_oa_salary.SelectedValue + "','" + txt_group_insurance.Text + "','" + ddl_service_group_insurance.SelectedValue + "',1,'" + txt_bill_service_charge_amount.Text + "','" + ddl_esic_allow.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + ddl_cotract_type.SelectedValue + "','" + txt_contract_rate.Text + "','" + ddl_handling_charge.SelectedValue + "','" + txt_handling_percent.Text + "','" + ddl_dc_contract.SelectedValue + "','" + ddl_dc_type.SelectedValue + "','" + txt_dc_rate.Text + "','" + txt_dc_area.Text + "','" + ddl_dc_handling_charge.SelectedValue + "','" + txt_dc_handling_percent.Text + "','" + ddl_conveyance_applicable.SelectedValue + "','" + ddl_conveyance_type.SelectedValue + "','" + txt_conveyance_rate.Text + "','" + txt_conveyance_km.Text + "','" + ddl_conveyance_service_charge.SelectedValue + "','" + txt_conveyance_service_charge.Text + "','" + txt_conveyance_service_amount.Text + "')");
    //                // vikas 09-01-19
    //                    //res = d.operation("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable) values ('" + Session["COMP_CODE"] + "','" + ddl_bill_ser_uniform.SelectedValue + "','" + ddl_bonus_taxable_billing.SelectedValue + "','" + designation1 + "','" + ddl_esic_ot.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + ddl_lwf_applicable.SelectedValue + "','" + ddl_month_calc.SelectedValue + "','" + ddl_ot_applicable.SelectedValue + "','" + ddl_pt_applicable.SelectedValue + "','" + txt_allowances_billing.Text + "','" + txt_basic_billing.Text + "','" + txt_bill_bonus_percent.Text + "','" + txt_bill_esic_percent.Text + "','" + txt_bill_oper_cost_amt.Text + "','" + txt_bill_oper_cost_percent.Text + "','" + txt_bill_pf_percent.Text + "','" + txt_bill_relieving.Text + "','" + txt_bill_service_charge.Text + "','" + txt_bill_uniform_percent.Text + "','" + txt_bill_uniform_rate.Text + "','" + txt_cca_billing.Text + "','" + txt_education_billing.Text + "',str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_hra_amount.Text + "','" + txt_hra_percent.Text + "','" + txt_leave_days.Text + "','" + txt_leave_days_percent.Text + "','" + txt_other_allow.Text + "','" + txt_per_rate_billing.Text + "','" + txt_policy_name1.Text + "','" + txt_sal_bonus.Text + "','" + txt_sal_esic.Text + "','" + txt_sal_pf.Text + "','" + txt_sal_uniform_percent.Text + "','" + txt_sal_uniform_rate.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),'" + txt_travelling_billing.Text + "','" + txt_vda_billing.Text + "','" + txt_washing_billing.Text + "','" + txt_basic_vda_billing.Text + "','" + ddl_leave_taxable_billing.SelectedValue + "','" + ddl_gratuity_taxable_billing.SelectedValue + "','" + ddl_bonus_taxable_salary.SelectedValue + "', '" + txt_allowances_salary.Text + "', '" + txt_basic_salary.Text + "', '" + txt_cca_salary.Text + "', '" + txt_education_salary.Text + "', '" + txt_per_rate_salary.Text + "', '" + txt_travelling_salary.Text + "', '" + txt_vda_salary.Text + "', '" + txt_washing_salary.Text + "','" + Session["USERNAME"].ToString() + "',now(),'" + txt_bonus_amount_salary.Text + "','" + ddl_leave_taxable_salary.SelectedValue + "','" + ddl_bonus_policy_aap_salary.SelectedValue + "','" + txt_gratuity_percent_billing.Text + "','" + txt_national_holidays_billing.Text + "','" + ddl_bonus_policy_aap_billing.SelectedValue + "','" + txt_bonus_amount_billing.Text + "','" + ddl_bill_ser_operations.SelectedValue + "','" + ddl_unit_client.SelectedValue + "','" + unit_code + "','" + ddl_clientwisestate.SelectedValue + "','" + txt_sal_graguity_per.Text + "','" + txt_leave_days_salary.Text + "','" + txt_leave_days_percent_salary.Text + "','" + txt_start_date_common.SelectedValue + "','" + txt_end_date_common.Text + "','" + ddl_ot_policy.SelectedValue + "','" + txt_ot_amount.Text + "','" + ddl_gratuity_taxable_salary.SelectedValue + "','" + ddl_ot_policy_billing.SelectedValue + "','" + txt_ot_amount_billing.Text + "','" + txt_hra_amount_salary.Text + "','" + txt_hra_percent_salary.Text + "','" + ddl_relieving_uniform.SelectedValue + "','" + ddl_esic_oa_billing.SelectedValue + "','" + ddl_esic_oa_salary.SelectedValue + "','" + txt_group_insurance.Text + "','" + ddl_service_group_insurance.SelectedValue + "',1,'" + txt_bill_service_charge_amount.Text + "','" + ddl_esic_allow.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + ddl_cotract_type.SelectedValue + "','" + txt_contract_rate.Text + "','" + ddl_handling_charge.SelectedValue + "','" + txt_handling_percent.Text + "','" + ddl_dc_contract.SelectedValue + "','" + ddl_dc_type.SelectedValue + "','" + txt_dc_rate.Text + "','" + txt_dc_area.Text + "','" + ddl_dc_handling_charge.SelectedValue + "','" + txt_dc_handling_percent.Text + "','" + ddl_conveyance_applicable.SelectedValue + "','" + ddl_conveyance_type.SelectedValue + "','" + txt_conveyance_rate.Text + "','" + txt_conveyance_km.Text + "','" + ddl_conveyance_service_charge.SelectedValue + "','" + txt_conveyance_service_charge.Text + "','" + txt_conveyance_service_amount.Text + "','" + ddl_equmental_applicable.SelectedValue + "','" + txt_equment_rate.Text + "','" + txt_equment_rental.Text + "','" + ddl_equmental_charges.SelectedValue + "','" + txt_equmental_percent.Text + "','" + ddl_chemical_applicable.SelectedValue + "','" + txt_chemical_unit.Text + "','" + txt_chemical.Text + "','" + ddl_chemical_charges.SelectedValue + "','" + txt_chemical_percent.Text + "','" + ddl_dustin_applicable.SelectedValue + "','" + txt_dustin_rate.Text + "','" + txt_dustin.Text + "','" + ddl_dustin_charges.SelectedValue + "','" + txt_dustin_percent.Text + "','" + ddl_femina_applicable.SelectedValue + "','" + txt_femina_unit.Text + "','" + txt_femina.Text + "','" + ddl_femina_charges.SelectedValue + "','" + txt_femina_percent.Text + "','" + ddl_aerosol_applicable.SelectedValue + "','" + txt_aerosol_rate.Text + "','" + txt_aerosol.Text + "','" + ddl_aerosol_charges.SelectedValue + "','" + txt_aerosol_percent.Text + "','" + ddl_tool_applicable.SelectedValue + "','" + txt_tool_unit.Text + "','" + txt_tool.Text + "','" + ddl_tool_charges.SelectedValue + "','" + txt_tool_percent.Text + "','" + ddl_pc_contract.SelectedValue + "','" + ddl_pc_type.SelectedValue + "','" + txt_pc_rate.Text + "','" + txt_pc_area.Text + "','" + ddl_pc_handling_charge.SelectedValue + "','" + txt_pc_handling_percent.Text + "','" + ddl_bonus_app.SelectedValue + "','" + ddl_leave_app.SelectedValue + "')");
    //                }
    //            }
    //        }
    //        if (res > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Added Successfully');", true);
    //        }
    //        ddl_unitcode_SelectedIndexChanged(null, null);
    //        ddl_clientwisestate_SelectedIndexChanged(null, null);
    //        text_clear();
    //        load_grdview();
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    }
    //    catch (Exception ex) { throw ex; }
    //}
    //protected void btn_close_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Home.aspx");
    //}

    //protected void btn_UPDATE_Click(object sender, EventArgs e)
    //{
    //    int res = 0;
    //    res = d.operation("UPDATE pay_billing_master SET bill_ser_uniform = '" + ddl_bill_ser_uniform.SelectedValue + "', bonus_taxable = '" + ddl_bonus_taxable_billing.SelectedValue + "', designation = '" + ddl_designation.SelectedValue + "', esic_ot = '" + ddl_esic_ot.SelectedValue + "', hours = '" + ddl_hours.SelectedValue + "', lwf_applicable = '" + ddl_lwf_applicable.SelectedValue + "', month_calc = '" + ddl_month_calc.SelectedValue + "', ot_applicable = '" + ddl_ot_applicable.SelectedValue + "', pt_applicable = '" + ddl_pt_applicable.SelectedValue + "', allowances = '" + txt_allowances_billing.Text + "', basic = '" + txt_basic_billing.Text + "', bill_bonus_percent = '" + txt_bill_bonus_percent.Text + "', bill_esic_percent = '" + txt_bill_esic_percent.Text + "', bill_oper_cost_amt = '" + txt_bill_oper_cost_amt.Text + "', bill_oper_cost_percent = '" + txt_bill_oper_cost_percent.Text + "', bill_pf_percent = '" + txt_bill_pf_percent.Text + "', bill_relieving = '" + txt_bill_relieving.Text + "', bill_service_charge = '" + txt_bill_service_charge.Text + "', bill_uniform_percent = '" + txt_bill_uniform_percent.Text + "', bill_uniform_rate = '" + txt_bill_uniform_rate.Text + "', cca = '" + txt_cca_billing.Text + "', education = '" + txt_education_billing.Text + "', end_date = str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'), hra_amount = '" + txt_hra_amount.Text + "', hra_percent = '" + txt_hra_percent.Text + "', leave_days = '" + txt_leave_days.Text + "', leave_days_percent = '" + txt_leave_days_percent.Text + "', other_allow = '" + txt_other_allow.Text + "', per_rate = '" + txt_per_rate_billing.Text + "', policy_name1 = '" + txt_policy_name1.Text + "', sal_bonus = '" + txt_sal_bonus.Text + "', sal_esic = '" + txt_sal_esic.Text + "', sal_pf = '" + txt_sal_pf.Text + "', sal_uniform_percent = '" + txt_sal_uniform_percent.Text + "', sal_uniform_rate = '" + txt_sal_uniform_rate.Text + "', start_date = str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'), travelling = '" + txt_travelling_billing.Text + "', vda = '" + txt_vda_billing.Text + "', washing ='" + txt_washing_billing.Text + "', basic_vda = '" + txt_basic_vda_billing.Text + "', leave_taxable ='" + ddl_leave_taxable_billing.SelectedValue + "',gratuity_taxable ='" + ddl_gratuity_taxable_billing.SelectedValue + "',bonus_taxable_salary = '" + ddl_bonus_taxable_salary.SelectedValue + "', allowances_salary = '" + txt_allowances_salary.Text + "', basic_salary = '" + txt_basic_salary.Text + "', cca_salary = '" + txt_cca_salary.Text + "', education_salary = '" + txt_education_salary.Text + "', per_rate_salary = '" + txt_per_rate_salary.Text + "', travelling_salary = '" + txt_travelling_salary.Text + "', vda_salary = '" + txt_vda_salary.Text + "', washing_salary = '" + txt_washing_salary.Text + "', update_user = '" + Session["USERNAME"].ToString() + "',update_date=now(),sal_bonus_amount='" + txt_bonus_amount_salary.Text + "',leave_taxable_salary='" + ddl_leave_taxable_salary.SelectedValue + "',bonus_policy_salary='" + ddl_bonus_policy_aap_salary.SelectedValue + "',gratuity_percent = '" + txt_gratuity_percent_billing.Text + "',national_holidays_count = '" + txt_national_holidays_billing.Text + "',bonus_policy_aap_billing='" + ddl_bonus_policy_aap_billing.SelectedValue + "',bonus_amount_billing='" + txt_bonus_amount_billing.Text + "', bill_ser_operations = '" + ddl_bill_ser_operations.SelectedValue + "',billing_client_code='" + ddl_unit_client.SelectedValue + "',billing_unit_code='" + ddl_unitcode.SelectedValue + "',billing_state='" + ddl_clientwisestate.SelectedValue + "',gratuity_salary='" + txt_sal_graguity_per.Text + "',leave_days_salary='" + txt_leave_days_salary.Text + "',leave_days_percent_salary='" + txt_leave_days_percent_salary.Text + "',start_date_common=str_to_date('" + txt_start_date_common + "','%d/%m/%Y'),end_date_common=str_to_date('" + txt_end_date_common + "','%d/%m/%Y') where id = " + ViewState["id"].ToString());
    //    if (res > 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Updated Successfully');", true);
    //    }
    //    // policy_fill();

    //}
    //protected void btndelete_Click(object sender, EventArgs e)
    //{
    //    int res = 0;
    //    //string pn = txt_policy_name1.Text;
    //    res = d.operation("delete from pay_billing_master WHERE policy_name1='" + pn + "'");
    //    if (res > 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Deleted Successfully');", true);
    //    }
    //    ddl_unitcode_SelectedIndexChanged(null, null);
    //    ddl_clientwisestate_SelectedIndexChanged(null, null);
    //    text_clear();
    //    load_grdview();
    //    //policy_fill();
    //    try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
    //    catch { }
    //}
    //protected void grd_policy_PreRender(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //grd_policy.UseAccessibleHeader = false;
    //        //grd_policy.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //    catch { }//vinod dont apply catch
    //}
    //protected void grd_policy_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    for (int i = 0; i < e.Row.Cells.Count; i++)
    //    {
    //        if (e.Row.Cells[i].Text == "&nbsp;")
    //        {
    //            e.Row.Cells[i].Text = "";
    //        }
    //    }
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
    //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grd_policy, "Select$" + e.Row.RowIndex);

    //    }
    //    e.Row.Cells[0].Visible = false;

    //}
    //protected void grd_policy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //ViewState["id"] = grd_policy.SelectedRow.Cells[0].Text;
    //    d1.con1.Open();
    //    try
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
    //    //    MySqlCommand cmd2 = new MySqlCommand("select bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, date_format(end_date,'%d/%m/%Y'), hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, date_format(start_date,'%d/%m/%Y'), travelling, vda, washing,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable,leave_days_percent,leave_days,leave_taxable,gratuity_taxable_salary,bonus_policy_salary,leave_taxable_salary,leave_days_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount FROM pay_billing_master where id = " + ViewState["id"].ToString(), d1.con1);
    //        MySqlCommand cmd2 = new MySqlCommand("select bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, date_format(end_date,'%d/%m/%Y'), hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, date_format(start_date,'%d/%m/%Y'), travelling, vda, washing,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable,leave_days_percent,leave_days,leave_taxable,gratuity_taxable_salary,bonus_policy_salary,leave_taxable_salary,leave_days_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable  FROM pay_billing_master where id = " + ViewState["id"].ToString(), d1.con1);
    //        MySqlDataReader dr2 = cmd2.ExecuteReader();
    //        if (dr2.Read())
    //        {
                //ddl_bill_ser_uniform.SelectedValue = dr2.GetValue(0).ToString();
                //ddl_bonus_taxable_billing.SelectedValue = dr2.GetValue(1).ToString();
                //ddl_esic_ot.SelectedValue = dr2.GetValue(3).ToString();
                //ddl_lwf_applicable.SelectedValue = dr2.GetValue(5).ToString();
                //ddl_month_calc.SelectedValue = dr2.GetValue(6).ToString();
                //ddl_ot_applicable.SelectedValue = dr2.GetValue(7).ToString();
               // ddl_pt_applicable.SelectedValue = dr2.GetValue(8).ToString();
               // txt_allowances_billing.Text = dr2.GetValue(9).ToString();
               // txt_basic_billing.Text = dr2.GetValue(10).ToString();
               // txt_bill_bonus_percent.Text = dr2.GetValue(11).ToString();
               // txt_bill_esic_percent.Text = dr2.GetValue(12).ToString();
               // txt_bill_oper_cost_amt.Text = dr2.GetValue(13).ToString();
               // txt_bill_oper_cost_percent.Text = dr2.GetValue(14).ToString();
               // txt_bill_pf_percent.Text = dr2.GetValue(15).ToString();
               // txt_bill_relieving.Text = dr2.GetValue(16).ToString();
               // txt_bill_service_charge.Text = dr2.GetValue(17).ToString();
               // txt_bill_uniform_percent.Text = dr2.GetValue(18).ToString();
               // txt_bill_uniform_rate.Text = dr2.GetValue(19).ToString();
               // txt_cca_billing.Text = dr2.GetValue(20).ToString();
               // txt_education_billing.Text = dr2.GetValue(21).ToString();
               // txt_end_date.Text = dr2.GetValue(22).ToString();
               // txt_hra_amount.Text = dr2.GetValue(23).ToString();
               // txt_hra_percent.Text = dr2.GetValue(24).ToString();
               // txt_leave_days.Text = dr2.GetValue(25).ToString();
               // txt_leave_days_percent.Text = dr2.GetValue(26).ToString();
               // //txt_other_allow.Text = dr2.GetValue(27).ToString();
               // txt_per_rate_billing.Text = dr2.GetValue(28).ToString();
               // txt_policy_name1.Text = dr2.GetValue(29).ToString();
               // txt_sal_bonus.Text = dr2.GetValue(30).ToString();
               // txt_sal_esic.Text = dr2.GetValue(31).ToString();
               // txt_sal_pf.Text = dr2.GetValue(32).ToString();
               // txt_sal_uniform_percent.Text = dr2.GetValue(33).ToString();
               // txt_sal_uniform_rate.Text = dr2.GetValue(34).ToString();
               // txt_start_date.Text = dr2.GetValue(35).ToString();
               // txt_travelling_billing.Text = dr2.GetValue(36).ToString();
               // txt_vda_billing.Text = dr2.GetValue(37).ToString();
               // txt_washing_billing.Text = dr2.GetValue(38).ToString();

               // //----------------------------------------------------------------
               // ddl_bonus_taxable_salary.SelectedValue = dr2.GetValue(39).ToString();
               // txt_allowances_salary.Text = dr2.GetValue(40).ToString();
               // txt_basic_salary.Text = dr2.GetValue(41).ToString();
               // txt_cca_salary.Text = dr2.GetValue(42).ToString();
               // txt_education_salary.Text = dr2.GetValue(43).ToString();
               // txt_per_rate_salary.Text = dr2.GetValue(44).ToString();
               // txt_travelling_salary.Text = dr2.GetValue(45).ToString();
               // txt_vda_salary.Text = dr2.GetValue(46).ToString();
               // txt_washing_salary.Text = dr2.GetValue(47).ToString();
               // txt_gratuity_percent_billing.Text = dr2.GetValue(48).ToString();
               // txt_national_holidays_billing.Text = dr2.GetValue(49).ToString();
               // ddl_bonus_policy_aap_billing.SelectedValue = dr2.GetValue(50).ToString();
               // txt_bonus_amount_billing.Text = dr2.GetValue(51).ToString();
               // ddl_bill_ser_operations.SelectedValue = dr2.GetValue(52).ToString();

               // ddl_unit_client.SelectedValue = dr2.GetValue(53).ToString();
               // ddl_clientname_SelectedIndexChanged(null, null);
               // ddl_designation.SelectedValue = dr2.GetValue(2).ToString();
               // ddl_clientwisestate.SelectedValue = dr2.GetValue(55).ToString();
               // ddl_clientwisestate_SelectedIndexChanged(null, null);
               // ddl_unitcode.SelectedValue = dr2.GetValue(54).ToString();
               // ddl_unitcode_SelectedIndexChanged(null, null);
               // txt_leave_days_salary.Text = dr2.GetValue(56).ToString();
               // txt_leave_days_percent_salary.Text = dr2.GetValue(57).ToString();
               //// txt_start_date_common.SelectedValue = dr2.GetValue(58).ToString();
               // //txt_end_date_common.Text = dr2.GetValue(59).ToString();
               // txt_policy_name1.Text = dr2.GetValue(29).ToString();
               // //txt_policy_name1.ReadOnly = true;
               // ddl_designation.SelectedValue = dr2.GetValue(2).ToString();
               // ddl_ot_policy.SelectedValue = dr2.GetValue(60).ToString();
               // txt_ot_amount.Text = dr2.GetValue(61).ToString();
               // ddl_designation_SelectedIndexChanged(null, null);
               // try
               // {
               //     ddl_hours.SelectedValue = dr2.GetValue(4).ToString();
               // }
               // catch { }
               //    ddl_gratuity_taxable_billing.SelectedValue = dr2.GetValue(62).ToString();
               // txt_leave_days_percent.Text = dr2.GetValue(63).ToString();
               // txt_leave_days.Text= dr2.GetValue(64).ToString();
               // ddl_leave_taxable_billing.SelectedValue = dr2.GetValue(65).ToString();
               // ddl_gratuity_taxable_salary.SelectedValue = dr2.GetValue(66).ToString();
               // ddl_bonus_policy_aap_salary.SelectedValue= dr2.GetValue(67).ToString();
               // ddl_leave_taxable_salary.SelectedValue = dr2.GetValue(68).ToString();
               // txt_leave_days_salary.Text = dr2.GetValue(69).ToString();
               // ddl_ot_policy_billing.SelectedValue= dr2.GetValue(70).ToString();
               // txt_ot_amount_billing.Text = dr2.GetValue(71).ToString();
               // txt_hra_amount_salary.Text = dr2.GetValue(72).ToString();
               // txt_hra_percent_salary.Text = dr2.GetValue(73).ToString();
               // ddl_relieving_uniform.SelectedValue = dr2.GetValue(74).ToString();
               // ddl_esic_oa_billing.SelectedValue = dr2.GetValue(75).ToString();
               // ddl_esic_oa_salary.SelectedValue = dr2.GetValue(76).ToString();
               // txt_group_insurance.Text = dr2.GetValue(77).ToString();
               // ddl_service_group_insurance.SelectedValue = dr2.GetValue(78).ToString();
               // txt_bill_service_charge_amount.Text = dr2.GetValue(79).ToString();
               // //ddl_esic_allow.SelectedValue = dr2.GetValue(80).ToString();
               // if (ddl_designation.SelectedValue =="HK") 
               // {
               //     //ddl_material_contract.SelectedValue = dr2.GetValue(81).ToString();
               // }
               // else{
               //     //ddl_material_contract.SelectedValue = "0";
               // }
               // ddl_cotract_type.SelectedValue = dr2.GetValue(82).ToString();
               // txt_contract_rate.Text = dr2.GetValue(83).ToString();
               // ddl_handling_charge.SelectedValue = dr2.GetValue(84).ToString();
               // txt_handling_percent.Text = dr2.GetValue(85).ToString();
               // //changes akshay 19/12/2018
               // //ddl_dc_contract.SelectedValue = dr2.GetValue(86).ToString();
               // ddl_dc_type.SelectedValue = dr2.GetValue(87).ToString();
               // txt_dc_rate.Text = dr2.GetValue(88).ToString();
               // txt_dc_area.Text = dr2.GetValue(89).ToString();
               // ddl_dc_handling_charge.SelectedValue = dr2.GetValue(90).ToString();
               // txt_dc_handling_percent.Text = dr2.GetValue(91).ToString();
               // //ddl_conveyance_applicable.SelectedValue = dr2.GetValue(92).ToString();
               // ddl_conveyance_type.SelectedValue = dr2.GetValue(93).ToString();
               // txt_conveyance_rate.Text = dr2.GetValue(94).ToString();
               // txt_conveyance_km.Text = dr2.GetValue(95).ToString();
               // ddl_conveyance_service_charge.SelectedValue = dr2.GetValue(96).ToString();
               // txt_conveyance_service_charge.Text = dr2.GetValue(97).ToString();
               // txt_conveyance_service_amount.Text = dr2.GetValue(98).ToString();
               // //vikas 09-01-19
               // ddl_equmental_applicable.SelectedValue = dr2.GetValue(99).ToString();
               // txt_equment_rate.Text = dr2.GetValue(100).ToString();
               // txt_equment_rental.Text = dr2.GetValue(101).ToString();
               // ddl_equmental_charges.SelectedValue = dr2.GetValue(102).ToString();
               // txt_equmental_percent.Text = dr2.GetValue(103).ToString();

               // ddl_chemical_applicable.SelectedValue = dr2.GetValue(104).ToString();
               // txt_chemical_unit.Text = dr2.GetValue(105).ToString();
        //        txt_chemical.Text = dr2.GetValue(106).ToString();
        //        ddl_chemical_charges.SelectedValue = dr2.GetValue(107).ToString();
        //        txt_chemical_percent.Text = dr2.GetValue(108).ToString();

        //        ddl_dustin_applicable.SelectedValue = dr2.GetValue(109).ToString();
        //        txt_dustin_rate.Text = dr2.GetValue(110).ToString();
        //        txt_dustin.Text = dr2.GetValue(111).ToString();
        //        ddl_dustin_charges.SelectedValue = dr2.GetValue(112).ToString();
        //        txt_dustin_percent.Text = dr2.GetValue(113).ToString();

        //        ddl_femina_applicable.SelectedValue = dr2.GetValue(114).ToString();
        //        txt_femina_unit.Text = dr2.GetValue(115).ToString();
        //        txt_femina.Text = dr2.GetValue(116).ToString();
        //        ddl_femina_charges.SelectedValue = dr2.GetValue(117).ToString();
        //        txt_femina_percent.Text = dr2.GetValue(118).ToString();

        //        ddl_aerosol_applicable.SelectedValue = dr2.GetValue(119).ToString();
        //        txt_aerosol_rate.Text = dr2.GetValue(120).ToString();
        //        txt_aerosol.Text = dr2.GetValue(121).ToString();
        //        ddl_aerosol_charges.SelectedValue = dr2.GetValue(122).ToString();
        //        txt_aerosol_percent.Text = dr2.GetValue(123).ToString();

        //        ddl_tool_applicable.SelectedValue = dr2.GetValue(124).ToString();
        //        txt_tool_unit.Text = dr2.GetValue(125).ToString();
        //        txt_tool.Text = dr2.GetValue(126).ToString();
        //        ddl_tool_charges.SelectedValue = dr2.GetValue(127).ToString();
        //        txt_tool_percent.Text = dr2.GetValue(128).ToString();
        //        //pest control
        //        //ddl_pc_contract.SelectedValue = dr2.GetValue(129).ToString();
        //        ddl_pc_type.SelectedValue = dr2.GetValue(130).ToString();
        //        txt_pc_rate.Text = dr2.GetValue(131).ToString();
        //        txt_pc_area.Text = dr2.GetValue(132).ToString();
        //        ddl_pc_handling_charge.SelectedValue = dr2.GetValue(133).ToString();
        //        txt_pc_handling_percent.Text = dr2.GetValue(134).ToString();

        //       // ddl_bonus_app.SelectedValue = dr2.GetValue(135).ToString();
        //        //ddl_leave_app.SelectedValue = dr2.GetValue(136).ToString();

        //    }
        //    btndelete.Visible = true;
        //    d1.con1.Close();
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d1.con1.Close();
        //}

        //MySqlCommand cmd = new MySqlCommand("select distinct(date_format(end_date,'%d/%m/%Y')) as 'end_date',(date_format(start_date,'%d/%m/%Y')) AS 'start_date' from pay_billing_master where comp_code='" + Session["comp_code"].ToString() + "' and   billing_client_code ='" + ddl_unit_client.SelectedValue + "' limit 1", d.con);
        //d.con.Open();
        //MySqlDataReader dr = cmd.ExecuteReader();

        //if (dr.Read())
        //{
        //    string start_date = dr.GetValue(1).ToString();
        //    string end_date = dr.GetValue(0).ToString();
        //    DateTime newdate1 = Convert.ToDateTime(System.DateTime.Now);
        //    DateTime start_date1 = DateTime.ParseExact(start_date, "dd/MM/yyyy", null);
        //    DateTime end_date1 = DateTime.ParseExact(end_date, "dd/MM/yyyy", null);
        //    if (newdate1 > end_date1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('End Date Exipre For (" + ddl_unit_client.SelectedItem.Text + ") Please Update Date!!');", true);
        //    }
        //}
        //dr.Close();
        //d.con.Close();
   // }
        
    //private void text_clear()
    //{
    //    ddl_bill_ser_uniform.SelectedIndex = 0;
    //    // ddl_designation.SelectedIndex = 0;
    //    //ddl_esic_ot.SelectedIndex = 0;
    //    //ddl_hours.SelectedIndex = 
    //    //ddl_lwf_applicable.SelectedIndex = 0;
    //    //ddl_month_calc.SelectedIndex = 0;
    //    //ddl_ot_applicable.SelectedIndex = 0;
    //    ddl_pt_applicable.SelectedIndex = 0;
    //    txt_bill_bonus_percent.Text = "8.33";
    //    txt_bill_esic_percent.Text = "4.75";
    //    txt_bill_oper_cost_amt.Text = "0";
    //    txt_bill_oper_cost_percent.Text = "0";
    //    txt_bill_pf_percent.Text = "0";
    //    txt_bill_relieving.Text = "0";
    //    txt_bill_service_charge.Text = "7";
    //    txt_bill_uniform_percent.Text = "0";
    //    txt_bill_uniform_rate.Text = "250";
    //    txt_end_date.Text = "";
    //    txt_hra_amount.Text = "0";
    //    txt_hra_percent.Text = "0";
    //    txt_leave_days.Text = "0";
    //    txt_leave_days_percent.Text = "0";
    //    //txt_other_allow.Text = "0";
    //    txt_policy_name1.Text = "";
    //    txt_sal_bonus.Text = "8.33";
    //    txt_sal_esic.Text = "1.75";
    //    txt_sal_pf.Text = "0";
    //    txt_sal_uniform_percent.Text = "0";
    //    txt_sal_uniform_rate.Text = "0";
    //    txt_start_date.Text = "";
    //    txt_policy_name1.ReadOnly = false;
    //    txt_start_date.ReadOnly = false;
    //    txt_end_date.ReadOnly = false;
    //    txt_allowances_billing.Text = "0";
    //    txt_basic_billing.Text = "0";
    //    txt_per_rate_billing.Text = "0";
    //    txt_travelling_billing.Text = "0";
    //    txt_vda_billing.Text = "0";
    //    txt_washing_billing.Text = "0";
    //    ddl_bonus_taxable_billing.SelectedIndex = 0;
    //    txt_cca_billing.Text = "0";
    //    txt_education_billing.Text = "0";
    //    ddl_bonus_taxable_salary.SelectedIndex = 0;
    //    txt_allowances_salary.Text = "0";
    //    txt_basic_salary.Text = "0";
    //    txt_cca_salary.Text = "0";
    //    txt_education_salary.Text = "0";
    //    txt_per_rate_salary.Text = "0";
    //    txt_travelling_salary.Text = "0";
    //    txt_vda_salary.Text = "0";
    //    txt_washing_salary.Text = "0";
    //    //txt_sal_graguity_per.Text = "4.81";
    //    txt_leave_days_salary.Text = "0";
    //    txt_leave_days_percent_salary.Text = "0";
    //    //txt_start_date_common.SelectedValue = "1";
    //    //txt_end_date_common.Text = "";
    //    ddl_ot_policy.SelectedValue = "No";
    //    txt_ot_amount.Text = "0";
    //    ddl_gratuity_taxable_salary.SelectedIndex = 0;
    //    ddl_ot_policy_billing.SelectedValue = "No";
    //    txt_ot_amount_billing.Text = "0";
    //    txt_hra_amount_salary.Text = "0";
    //    txt_hra_percent_salary.Text = "0";
    //    txt_group_insurance.Text = "0";
    //    ddl_service_group_insurance.SelectedValue = "1";

    //    //ddl_dc_contract.SelectedValue = "0";
    //    ddl_dc_type.SelectedValue = "0";
    //    txt_dc_rate.Text = "0";
    //    txt_dc_area.Text = "0";
    //    ddl_dc_handling_charge.SelectedValue = "0";
    //    txt_dc_handling_percent.Text = "0";
    //    //ddl_conveyance_applicable.SelectedValue = "0";
    //    ddl_conveyance_type.SelectedValue = "0";
    //    txt_conveyance_rate.Text = "0";
    //    txt_conveyance_km.Text = "0";
    //    ddl_conveyance_service_charge.SelectedValue = "0";
    //    txt_conveyance_service_charge.Text = "0";
    //    txt_conveyance_service_amount.Text = "0";
    //}
    //protected void ddl_clientwisestate_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    d.con.Open();
    //    try
    //    {
    //        ddl_unitcode.Items.Clear();
    //        MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0", d.con);//and billing_unit_code in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "' AND state_name='" + ddl_clientwisestate.SelectedValue + "')
    //        MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
    //        DataSet ds1 = new DataSet();
    //        sda1.Fill(ds1);
    //        ddl_unitcode.DataSource = ds1.Tables[0];
    //        ddl_unitcode.DataValueField = "UNIT_CODE";
    //        ddl_unitcode.DataTextField = "CUNIT";
    //        ddl_unitcode.DataBind();
    //        //ddl_unitcode.Items.Insert(0, new ListItem("Select"));

    //        // ddl_Existing_policy_name.Items.Clear();
    //        ddl_designation.Items.Clear();
    //        d.con.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { d.con.Close(); }

    //    d.con.Open();
    //    try
    //    {
    //        ddl_unitcode_without.Items.Clear();
    //        MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0 ", d.con);
    //        MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
    //        DataSet ds1 = new DataSet();
    //        sda1.Fill(ds1);
    //        ddl_unitcode_without.DataSource = ds1.Tables[0];
    //        ddl_unitcode_without.DataValueField = "UNIT_CODE";
    //        ddl_unitcode_without.DataTextField = "CUNIT";
    //        ddl_unitcode_without.DataBind();
    //        d.con.Close();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally { d.con.Close(); }
    //}
    //protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
    //                                         where item.Selected
    //                                         select item.Value;
    //    string listvalues_ddl_unitcode = string.Join("','", selectedValues);
    //    IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
    //                                                 where item.Selected
    //                                                 select item.Value;
    //    listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);

    //    //ddl_Existing_policy_name.Items.Clear();
    //    //System.Data.DataTable dt_item = new System.Data.DataTable();
    //    //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(POLICY_NAME1) FROM pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "' and billing_unit_code in ('" + listvalues_ddl_unitcode + "')", d.con1);
    //    //d.con.Open();
    //    //try
    //    //{
    //    //    cmd_item.Fill(dt_item);
    //    //    if (dt_item.Rows.Count > 0)
    //    //    {
    //    //        ddl_Existing_policy_name.DataSource = dt_item;
    //    //        ddl_Existing_policy_name.DataValueField = dt_item.Columns[0].ToString();
    //    //        ddl_Existing_policy_name.DataTextField = dt_item.Columns[0].ToString();
    //    //        ddl_Existing_policy_name.DataBind();
    //    //    }
    //    //    ddl_Existing_policy_name.Items.Insert(0, new ListItem("NEW", ""));
    //    //    d.con.Close();
    //    //}
    //    //catch (Exception ex) { throw ex; }
    //    //finally
    //    //{
    //    //    d.con.Close();
    //    //}

    //    d.con.Open();
    //    ddl_designation.Items.Clear();
    //    DataTable dt_item = new System.Data.DataTable();
    //    //cmd_item = new MySqlDataAdapter("select GRADE_CODE, GRADE_DESC FROM pay_grade_master where COMP_CODE='" + Session["COMP_CODE"] + "'", d.con);
    //    MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and UNIT_CODE in ('" + listvalues_ddl_unitcode + "')", d.con);
    //    try
    //    {
    //        cmd_item.Fill(dt_item);
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            ddl_designation.DataSource = dt_item;
    //            ddl_designation.DataValueField = dt_item.Columns[0].ToString();
    //            ddl_designation.DataTextField = dt_item.Columns[1].ToString();
    //            ddl_designation.DataBind();

    //        }
    //        //ddl_designation.Items.Insert(0, new ListItem("Select", ""));
    //        d.con.Close();
    //        ddl_hours.Items.Clear();
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //}
//    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        IEnumerable<string> selectedValues = from item in ddl_designation.Items.Cast<ListItem>()
//                                             where item.Selected
//                                             select item.Text;
//        string listvalues_ddl_designation = string.Join("','", selectedValues);

//         selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
//                                             where item.Selected
//                                             select item.Value;
//        string listvalues_ddl_unitcode = string.Join("','", selectedValues);
//        IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
//                                                     where item.Selected
//                                                     select item.Value;
//        listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);

//        d.con.Open();
//        ddl_hours.Items.Clear();
//        DataTable dt_item = new System.Data.DataTable();
//        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(hours) from pay_designation_count where unit_code in ('" + listvalues_ddl_unitcode + "') and comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' and designation in ('" + listvalues_ddl_designation + "')", d.con);
//        try
//        {
//            cmd_item.Fill(dt_item);
//            if (dt_item.Rows.Count > 0)
//            {
//                ddl_hours.DataSource = dt_item;
//                ddl_hours.DataValueField = dt_item.Columns[0].ToString();
//                ddl_hours.DataTextField = dt_item.Columns[0].ToString();
//                ddl_hours.DataBind();

//            }
//            d.con.Close();

//            //ddl_material_contract.Items.Clear();
//            //ddl_material_contract.Items.Add(new ListItem("No", "0"));
//            if (listvalues_ddl_designation.Equals("HOUSEKEEPING"))
//            { 
//               // ddl_material_contract.Items.Add(new ListItem("Yes", "1"));
//            }
//        }
//        catch (Exception ex) { throw ex; }
//        finally
//        {
//            d.con.Close();
//        }
//    }
   protected void gv_working_checklist_PreRender(object sender, EventArgs e)
   {
       try
       {
           gv_working_checklist.UseAccessibleHeader = false;
           gv_working_checklist.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply catch
   }
}