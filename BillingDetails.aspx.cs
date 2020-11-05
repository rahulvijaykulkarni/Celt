using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


public partial class BillingDeails : System.Web.UI.Page
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
            // Designation_fill();
            // policy_fill();
            btnadd.Visible = true;
            btndelete.Visible = false;
            text_clear();
            client_list();
            pf_account_details1();
            pf_account_details();

            //ddl_cotract_type.Visible = false;
            update_ddl_material_contract(0);
            //vikas machine policy
            Machine_list();
            // ViewState["id"] ="";

            txt_start_date.ReadOnly = true;
            txt_end_date.ReadOnly = true;

        }

    }


    protected void pf_account_details()
    {
        MySqlCommand cmd = new MySqlCommand("select Total from  pay_company_pf_details where type='Employer'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_bill_pf_percent.Text = dr.GetValue(0).ToString();
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            d.con.Close();
            txt_bill_pf_percent.ReadOnly = true;
        }
    }
    protected void pf_account_details1()
    {
        MySqlCommand cmd = new MySqlCommand("select Total from  pay_company_pf_details where type='Employee'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                txt_sal_pf.Text = dr.GetValue(0).ToString();
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            txt_sal_pf.ReadOnly = true;
        }
    }
    protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    {
        string date = d.getsinglestring("select start_date_billing,end_date_billing  from pay_client_master where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'   ");

        if (date == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Set Billing Cycle in Client Master !!');", true);
            ddl_unit_client.SelectedValue = "Select";
            txt_start_date_common.SelectedValue = "0";
            txt_end_date_common.Text = "";
            return;


        }
        else
        {
            ddl_clientwisestate.Items.Clear();
            // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code is null ORDER BY STATE", d1.con);//and state in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "')
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    ddl_clientwisestate.Items.Add(dr_item1[0].ToString());

                }
                dr_item1.Close();
                ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));

                ddl_unitcode.Items.Clear();
                ddl_designation.Items.Clear();
                load_grdview();
                //komal 20-06-19
                billing_dates();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }
        }
        gv_product_details.DataSource = null;
        gv_product_details.DataBind();


    }
    protected void client_list()
    {
        d.con1.Open();
        try
        {
            ddl_unit_client.Items.Clear();
            MySqlDataAdapter cmd = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0'  ORDER BY client_code", d.con1);//AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_unit_client.DataSource = dt;
                ddl_unit_client.DataTextField = dt.Columns[0].ToString();
                ddl_unit_client.DataValueField = dt.Columns[1].ToString();
                ddl_unit_client.DataBind();

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {

            d.con1.Close();
            ddl_unit_client.Items.Insert(0, "Select");
        }




    }

    private void load_grdview()
    {
        d.con1.Open();
        try
        {
            MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("SELECT pay_billing_master.id, policy_name1 AS 'Policy', client_name AS 'Client', unit_name AS 'Branch', billing_state AS 'State', grade_desc AS 'Designation', CAST(CONCAT(hours, ' Hrs') AS char) AS 'Working Hours', DATE_FORMAT(start_date, '%d/%m/%Y') AS 'Policy Start Date', DATE_FORMAT(end_date, '%d/%m/%Y') AS 'Policy End Date' FROM pay_billing_master INNER JOIN pay_client_master ON pay_client_master.client_code = pay_billing_master.billing_client_code AND pay_client_master.comp_code = pay_billing_master.comp_code INNER JOIN pay_unit_master ON pay_unit_master.unit_code = pay_billing_master.billing_unit_code AND pay_unit_master.comp_code = pay_billing_master.comp_code INNER JOIN pay_grade_master ON pay_grade_master.comp_code = pay_billing_master.comp_code AND pay_grade_master.GRADE_CODE = pay_billing_master.Designation where billing_client_code = '" + ddl_unit_client.SelectedValue + "' and pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.branch_status = 0", d.con1);
            DataTable DS1 = new DataTable();
            MySqlDataAdapter1.Fill(DS1);
            grd_policy.DataSource = DS1;
            grd_policy.DataBind();
            d.con1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con1.Close(); }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        try
        {
            string start_date_common = d.getsinglestring("SELECT start_date_common FROM pay_billing_master WHERE billing_client_code = '" + ddl_unit_client.SelectedValue + "' AND comp_code = '" + Session["COMP_CODE"].ToString() + "' limit 1");
            if (!start_date_common.Equals(txt_start_date_common.Text) && start_date_common != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Start date is " + start_date_common + "');", true);
                txt_start_date_common.Focus();
                return;
            }
            IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_unitcode = string.Join(",", selectedValues);

            selectedValues = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                             where item.Selected
                             select item.Value;
            if (listvalues_ddl_unitcode == "") { listvalues_ddl_unitcode = string.Join(",", selectedValues); }
            else
            {
                listvalues_ddl_unitcode = listvalues_ddl_unitcode + "," + string.Join(",", selectedValues);
            }


            IEnumerable<string> designation = from item in ddl_designation.Items.Cast<ListItem>()
                                              where item.Selected
                                              select item.Value;
            string listvalues_ddl_designation = string.Join(",", designation);


            //string temp = d1.getsinglestring("select distinct(POLICY_NAME1) FROM pay_billing_master where POLICY_NAME1 = '" + txt_policy_name1.Text + "'");
            //if (temp == txt_policy_name1.Text)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Name Already Exist !!');", true);
            //    return;
            //}

            //d.operation("delete from pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and POLICY_NAME1 = '" + txt_policy_name1.Text + "'");


            var elements = listvalues_ddl_unitcode.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            var elements1 = listvalues_ddl_designation.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            int res = 0;
            string where = "";
            string delete_where = "";
            string chk_ddlunit = "";
            string chk_ddlunit1 = "";
            if (ddl_policy.SelectedValue == "2")
            {
                if (ddl_unitcode.SelectedIndex != -1)
                {
                    chk_ddlunit = " billing_unit_code = '" + ddl_unitcode.SelectedValue + "' ";
                    chk_ddlunit1 = " unit_code = '" + ddl_unitcode.SelectedValue + "' ";
                }
                if (ddl_unitcode_without.SelectedIndex != -1)
                {
                    chk_ddlunit = " billing_unit_code = '" + ddl_unitcode_without.SelectedValue + "'";
                    chk_ddlunit1 = " unit_code = '" + ddl_unitcode_without.SelectedValue + "' ";
                }

                if (ddl_clientwisestate.SelectedValue == "ALL" && ddl_unitcode_without.SelectedValue == "ALL")
                {

                    where = "where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code is not null ";
                    delete_where = " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "'  ";
                }
                if (ddl_clientwisestate.SelectedValue != "ALL" && ddl_unitcode_without.SelectedValue == "ALL")
                {
                    where = " where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and pay_designation_count.state = '" + ddl_clientwisestate.SelectedValue + "' and unit_code is not null ";
                    delete_where = " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "' and billing_state = '" + ddl_clientwisestate.SelectedValue + "'  ";
                }
                if (ddl_unitcode_without.SelectedValue != "ALL")
                {
                    where = " where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and pay_designation_count." + chk_ddlunit1 + " and unit_code is not null ";
                    delete_where = " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "'";
                }
            }
            string unit = "";

            if (ddl_unitcode_without.SelectedIndex != -1)
            {
                if (ddl_unitcode_without.SelectedValue == "ALL")
                {
                    unit = d.getsinglestring("SELECT GROUP_CONCAT(UNIT_CODE) FROM pay_designation_count where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') and designation = '" + ddl_designation.SelectedItem + "' and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' AND branch_status = 0");
                }
                else
                {
                    unit = " " + ddl_unitcode_without.SelectedValue + " ";
                }
            }
            if (ddl_unitcode.SelectedIndex != -1)
            {
                unit = " " + ddl_unitcode.SelectedValue + " ";
            }
            if (ddl_policy.SelectedValue == "1")
            {
                where = " where pay_designation_count.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_designation_count.CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and pay_designation_count.state = '" + ddl_clientwisestate.SelectedValue + "' and unit_code is not null ";
                delete_where = " where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "' and billing_state = '" + ddl_clientwisestate.SelectedValue + "'  ";
                unit = d.getsinglestring("SELECT GROUP_CONCAT(UNIT_CODE) FROM pay_designation_count where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "'  and designation = '" + ddl_designation.SelectedItem + "' and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' AND branch_status = 0");
            }

            string[] ddd = unit.Split(',');
            elements = ddd;

            foreach (string unit_code in elements)
            {
                foreach (string designation1 in elements1)
                {
                    // string unit1 = "'" + unit_code + "'";
                    string temp1 = d1.getsinglestring("select cast(group_concat(pay_grade_master.grade_code) as char) from pay_designation_count inner join pay_grade_master on pay_grade_master.grade_desc = pay_designation_count.designation and pay_grade_master.comp_code = pay_designation_count.comp_code " + where + " and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' and unit_code is not null ");
                    if (temp1.Contains(designation1))
                    {
                        d.operation("delete from pay_billing_master" + delete_where + " and billing_unit_code = '" + unit_code.Trim() + "' and designation = '" + designation1 + "' and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' ");



                        //    res = d.operation("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount) values ('" + Session["COMP_CODE"] + "','" + ddl_bill_ser_uniform.SelectedValue + "','" + ddl_bonus_taxable_billing.SelectedValue + "','" + designation1 + "','" + ddl_esic_ot.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + ddl_lwf_applicable.SelectedValue + "','" + ddl_month_calc.SelectedValue + "','" + ddl_ot_applicable.SelectedValue + "','" + ddl_pt_applicable.SelectedValue + "','" + txt_allowances_billing.Text + "','" + txt_basic_billing.Text + "','" + txt_bill_bonus_percent.Text + "','" + txt_bill_esic_percent.Text + "','" + txt_bill_oper_cost_amt.Text + "','" + txt_bill_oper_cost_percent.Text + "','" + txt_bill_pf_percent.Text + "','" + txt_bill_relieving.Text + "','" + txt_bill_service_charge.Text + "','" + txt_bill_uniform_percent.Text + "','" + txt_bill_uniform_rate.Text + "','" + txt_cca_billing.Text + "','" + txt_education_billing.Text + "',str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_hra_amount.Text + "','" + txt_hra_percent.Text + "','" + txt_leave_days.Text + "','" + txt_leave_days_percent.Text + "','" + txt_other_allow.Text + "','" + txt_per_rate_billing.Text + "','" + txt_policy_name1.Text + "','" + txt_sal_bonus.Text + "','" + txt_sal_esic.Text + "','" + txt_sal_pf.Text + "','" + txt_sal_uniform_percent.Text + "','" + txt_sal_uniform_rate.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),'" + txt_travelling_billing.Text + "','" + txt_vda_billing.Text + "','" + txt_washing_billing.Text + "','" + txt_basic_vda_billing.Text + "','" + ddl_leave_taxable_billing.SelectedValue + "','" + ddl_gratuity_taxable_billing.SelectedValue + "','" + ddl_bonus_taxable_salary.SelectedValue + "', '" + txt_allowances_salary.Text + "', '" + txt_basic_salary.Text + "', '" + txt_cca_salary.Text + "', '" + txt_education_salary.Text + "', '" + txt_per_rate_salary.Text + "', '" + txt_travelling_salary.Text + "', '" + txt_vda_salary.Text + "', '" + txt_washing_salary.Text + "','" + Session["USERNAME"].ToString() + "',now(),'" + txt_bonus_amount_salary.Text + "','" + ddl_leave_taxable_salary.SelectedValue + "','" + ddl_bonus_policy_aap_salary.SelectedValue + "','" + txt_gratuity_percent_billing.Text + "','" + txt_national_holidays_billing.Text + "','" + ddl_bonus_policy_aap_billing.SelectedValue + "','" + txt_bonus_amount_billing.Text + "','" + ddl_bill_ser_operations.SelectedValue + "','" + ddl_unit_client.SelectedValue + "','" + unit_code + "','" + ddl_clientwisestate.SelectedValue + "','" + txt_sal_graguity_per.Text + "','" + txt_leave_days_salary.Text + "','" + txt_leave_days_percent_salary.Text + "','" + txt_start_date_common.SelectedValue + "','" + txt_end_date_common.Text + "','" + ddl_ot_policy.SelectedValue + "','" + txt_ot_amount.Text + "','" + ddl_gratuity_taxable_salary.SelectedValue + "','" + ddl_ot_policy_billing.SelectedValue + "','" + txt_ot_amount_billing.Text + "','" + txt_hra_amount_salary.Text + "','" + txt_hra_percent_salary.Text + "','" + ddl_relieving_uniform.SelectedValue + "','" + ddl_esic_oa_billing.SelectedValue + "','" + ddl_esic_oa_salary.SelectedValue + "','" + txt_group_insurance.Text + "','" + ddl_service_group_insurance.SelectedValue + "',1,'" + txt_bill_service_charge_amount.Text + "','" + ddl_esic_allow.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + ddl_cotract_type.SelectedValue + "','" + txt_contract_rate.Text + "','" + ddl_handling_charge.SelectedValue + "','" + txt_handling_percent.Text + "','" + ddl_dc_contract.SelectedValue + "','" + ddl_dc_type.SelectedValue + "','" + txt_dc_rate.Text + "','" + txt_dc_area.Text + "','" + ddl_dc_handling_charge.SelectedValue + "','" + txt_dc_handling_percent.Text + "','" + ddl_conveyance_applicable.SelectedValue + "','" + ddl_conveyance_type.SelectedValue + "','" + txt_conveyance_rate.Text + "','" + txt_conveyance_km.Text + "','" + ddl_conveyance_service_charge.SelectedValue + "','" + txt_conveyance_service_charge.Text + "','" + txt_conveyance_service_amount.Text + "')");

                        // vikas 09-01-19
                        if (txt_handling_amount.Text == "") { txt_handling_amount.Text = "0"; }
                        //suraj
                        //res = d.operation();
                        string date_start = "";
                        string date_end = "";
                        if (ddl_policy.SelectedValue == "1")
                        {
                            date_start = d.getsinglestring("select date_format(unit_start_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");
                            date_end = d.getsinglestring("select date_format(unit_end_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");

                        }
                        else if (ddl_policy.SelectedValue == "2")
                        {
                            date_start = txt_start_date.Text;
                            date_end = txt_end_date.Text;

                        }

                        string sql = "select '" + Session["COMP_CODE"] + "','" + ddl_bill_ser_uniform.SelectedValue + "','" + ddl_bonus_taxable_billing.SelectedValue + "','" + designation1 + "','" + ddl_esic_ot.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + ddl_lwf_applicable.SelectedValue + "','" + ddl_month_calc.SelectedValue + "','" + ddl_ot_applicable.SelectedValue + "','" + ddl_pt_applicable.SelectedValue + "','" + txt_allowances_billing.Text + "','" + txt_basic_billing.Text + "','" + txt_bill_bonus_percent.Text + "','" + txt_bill_esic_percent.Text + "','" + txt_bill_oper_cost_amt.Text + "','" + txt_bill_oper_cost_percent.Text + "','" + txt_bill_pf_percent.Text + "','" + txt_bill_relieving.Text + "','" + txt_bill_service_charge.Text + "','" + txt_bill_uniform_percent.Text + "','" + txt_bill_uniform_rate.Text + "','" + txt_cca_billing.Text + "','" + txt_education_billing.Text + "',str_to_date('" + date_end + "','%d/%m/%Y'),'" + txt_hra_amount.Text + "','" + txt_hra_percent.Text + "','" + txt_leave_days.Text + "','" + txt_leave_days_percent.Text + "','" + txt_other_allow.Text + "','" + txt_per_rate_billing.Text + "','" + txt_policy_name1.Text + "','" + txt_sal_bonus.Text + "','" + txt_sal_esic.Text + "','" + txt_sal_pf.Text + "','" + txt_sal_uniform_percent.Text + "','" + txt_sal_uniform_rate.Text + "',str_to_date('" + date_start + "','%d/%m/%Y'),'" + txt_travelling_billing.Text + "','" + txt_vda_billing.Text + "','" + txt_washing_billing.Text + "','" + txt_basic_vda_billing.Text + "','" + ddl_leave_taxable_billing.SelectedValue + "','" + ddl_gratuity_taxable_billing.SelectedValue + "','" + ddl_bonus_taxable_salary.SelectedValue + "', '" + txt_allowances_salary.Text + "', '" + txt_basic_salary.Text + "', '" + txt_cca_salary.Text + "', '" + txt_education_salary.Text + "', '" + txt_per_rate_salary.Text + "', '" + txt_travelling_salary.Text + "', '" + txt_vda_salary.Text + "', '" + txt_washing_salary.Text + "','" + Session["USERNAME"].ToString() + "',now(),'" + txt_bonus_amount_salary.Text + "','" + ddl_leave_taxable_salary.SelectedValue + "','" + ddl_bonus_policy_aap_salary.SelectedValue + "','" + txt_gratuity_percent_billing.Text + "','" + txt_national_holidays_billing.Text + "','" + ddl_bonus_policy_aap_billing.SelectedValue + "','" + txt_bonus_amount_billing.Text + "','" + ddl_bill_ser_operations.SelectedValue + "','" + ddl_unit_client.SelectedValue + "','" + unit_code.Trim() + "', state,'" + txt_sal_graguity_per.Text + "','" + txt_leave_days_salary.Text + "','" + txt_leave_days_percent_salary.Text + "','" + txt_start_date_common.SelectedValue + "','" + txt_end_date_common.Text + "','" + ddl_ot_policy.SelectedValue + "','" + txt_ot_amount.Text + "','" + ddl_gratuity_taxable_salary.SelectedValue + "','" + ddl_ot_policy_billing.SelectedValue + "','" + txt_ot_amount_billing.Text + "','" + txt_hra_amount_salary.Text + "','" + txt_hra_percent_salary.Text + "','" + ddl_relieving_uniform.SelectedValue + "','" + ddl_esic_oa_billing.SelectedValue + "','" + ddl_esic_oa_salary.SelectedValue + "','" + txt_group_insurance.Text + "','" + ddl_service_group_insurance.SelectedValue + "',1,'" + txt_bill_service_charge_amount.Text + "','" + ddl_esic_allow.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + txt_contract_rate.Text + "','" + ddl_handling_charge.SelectedValue + "','" + txt_handling_percent.Text + "','" + ddl_dc_contract.SelectedValue + "','" + ddl_dc_type.SelectedValue + "','" + txt_dc_rate.Text + "','" + txt_dc_area.Text + "','" + ddl_dc_handling_charge.SelectedValue + "','" + txt_dc_handling_percent.Text + "','" + ddl_conveyance_applicable.SelectedValue + "','" + ddl_conveyance_type.SelectedValue + "','" + txt_conveyance_rate.Text + "','" + txt_conveyance_km.Text + "','" + ddl_conveyance_service_charge.SelectedValue + "','" + txt_conveyance_service_charge.Text + "','" + txt_conveyance_service_amount.Text + "','" + ddl_equmental_applicable.SelectedValue + "','" + txt_equment_rate.Text + "','" + txt_equment_rental.Text + "','" + ddl_equmental_charges.SelectedValue + "','" + txt_equmental_percent.Text + "','" + ddl_chemical_applicable.SelectedValue + "','" + txt_chemical_unit.Text + "','" + txt_chemical.Text + "','" + ddl_chemical_charges.SelectedValue + "','" + txt_chemical_percent.Text + "','" + ddl_dustin_applicable.SelectedValue + "','" + txt_dustin_rate.Text + "','" + txt_dustin.Text + "','" + ddl_dustin_charges.SelectedValue + "','" + txt_dustin_percent.Text + "','" + ddl_femina_applicable.SelectedValue + "','" + txt_femina_unit.Text + "','" + txt_femina.Text + "','" + ddl_femina_charges.SelectedValue + "','" + txt_femina_percent.Text + "','" + ddl_aerosol_applicable.SelectedValue + "','" + txt_aerosol_rate.Text + "','" + txt_aerosol.Text + "','" + ddl_aerosol_charges.SelectedValue + "','" + txt_aerosol_percent.Text + "','" + ddl_tool_applicable.SelectedValue + "','" + txt_tool_unit.Text + "','" + txt_tool.Text + "','" + ddl_tool_charges.SelectedValue + "','" + txt_tool_percent.Text + "','" + ddl_pc_contract.SelectedValue + "','" + ddl_pc_type.SelectedValue + "','" + txt_pc_rate.Text + "','" + txt_pc_area.Text + "','" + ddl_pc_handling_charge.SelectedValue + "','" + txt_pc_handling_percent.Text + "','" + ddl_bonus_app.SelectedValue + "','" + ddl_leave_app.SelectedValue + "','" + ddl_gst_applicable.SelectedValue + "','" + ddl_cmn_pf_app.SelectedValue + "','" + ddl_lwf_act_man.SelectedValue + "','" + ddl_machine_rental_app.SelectedValue + "'," + txt_handling_amount.Text + ",'" + ddl_machine_rental_applicable.SelectedValue + "','" + txt_machine_rental_amount.Text + "','" + txt_food_rate.Text + "','" + txt_oc_rate.Text + "','" + txt_os_rate.Text + "','" + txt_nh_rate.Text + "','" + txt_km_rate.Text + "','" + ddl_category.SelectedValue + "' ,'" + txt_conveyance_amount.Text + "','" + ddl_service_charge.SelectedValue + "','" + txt_service_charge_rate.Text + "','" + txt_service_charge.Text + "','" + ddl_service_charge_adm.SelectedValue + "','" + txt_rate_adm.Text + "','" + txt_ser_rate_adm.Text + "' from pay_designation_count " + where + "  AND pay_designation_count.DESIGNATION = '" + ddl_designation.SelectedItem + "' group by unit_code LIMIT 1 ";
                        string id = d.getsinglestring("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent , sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon,machine_rental,handling_charges_amount,machine_rental_applicable,machine_rental_amount,food_allowance_rate,outstation_allowance_rate,outstation_food_allowance_rate,night_halt_rate,km_rate,category,conveyance_amount,service_charge_r_m,rm_service_rate_rs,rm_service_rate_percent,service_charge_admini,admini_service_rate_rs,admini_service_rate_percent) " + sql + ";select max(id) from pay_billing_master;");
                        add(unit_code.Trim(), designation1, id);


                        //string id = d.getsinglestring("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon,machine_rental,handling_charges_amount,machine_rental_applicable,machine_rental_amount,food_allowance_rate,outstation_allowance_rate,outstation_food_allowance_rate,night_halt_rate,km_rate,conveyance_amount) values ('" + Session["COMP_CODE"] + "','" + ddl_bill_ser_uniform.SelectedValue + "','" + ddl_bonus_taxable_billing.SelectedValue + "','" + designation1 + "','" + ddl_esic_ot.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + ddl_lwf_applicable.SelectedValue + "','" + ddl_month_calc.SelectedValue + "','" + ddl_ot_applicable.SelectedValue + "','" + ddl_pt_applicable.SelectedValue + "','" + txt_allowances_billing.Text + "','" + txt_basic_billing.Text + "','" + txt_bill_bonus_percent.Text + "','" + txt_bill_esic_percent.Text + "','" + txt_bill_oper_cost_amt.Text + "','" + txt_bill_oper_cost_percent.Text + "','" + txt_bill_pf_percent.Text + "','" + txt_bill_relieving.Text + "','" + txt_bill_service_charge.Text + "','" + txt_bill_uniform_percent.Text + "','" + txt_bill_uniform_rate.Text + "','" + txt_cca_billing.Text + "','" + txt_education_billing.Text + "',str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + txt_hra_amount.Text + "','" + txt_hra_percent.Text + "','" + txt_leave_days.Text + "','" + txt_leave_days_percent.Text + "','" + txt_other_allow.Text + "','" + txt_per_rate_billing.Text + "','" + txt_policy_name1.Text + "','" + txt_sal_bonus.Text + "','" + txt_sal_esic.Text + "','" + txt_sal_pf.Text + "','" + txt_sal_uniform_percent.Text + "','" + txt_sal_uniform_rate.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),'" + txt_travelling_billing.Text + "','" + txt_vda_billing.Text + "','" + txt_washing_billing.Text + "','" + txt_basic_vda_billing.Text + "','" + ddl_leave_taxable_billing.SelectedValue + "','" + ddl_gratuity_taxable_billing.SelectedValue + "','" + ddl_bonus_taxable_salary.SelectedValue + "', '" + txt_allowances_salary.Text + "', '" + txt_basic_salary.Text + "', '" + txt_cca_salary.Text + "', '" + txt_education_salary.Text + "', '" + txt_per_rate_salary.Text + "', '" + txt_travelling_salary.Text + "', '" + txt_vda_salary.Text + "', '" + txt_washing_salary.Text + "','" + Session["USERNAME"].ToString() + "',now(),'" + txt_bonus_amount_salary.Text + "','" + ddl_leave_taxable_salary.SelectedValue + "','" + ddl_bonus_policy_aap_salary.SelectedValue + "','" + txt_gratuity_percent_billing.Text + "','" + txt_national_holidays_billing.Text + "','" + ddl_bonus_policy_aap_billing.SelectedValue + "','" + txt_bonus_amount_billing.Text + "','" + ddl_bill_ser_operations.SelectedValue + "','" + ddl_unit_client.SelectedValue + "','" + unit_code + "','" + ddl_clientwisestate.SelectedValue + "','" + txt_sal_graguity_per.Text + "','" + txt_leave_days_salary.Text + "','" + txt_leave_days_percent_salary.Text + "','" + txt_start_date_common.SelectedValue + "','" + txt_end_date_common.Text + "','" + ddl_ot_policy.SelectedValue + "','" + txt_ot_amount.Text + "','" + ddl_gratuity_taxable_salary.SelectedValue + "','" + ddl_ot_policy_billing.SelectedValue + "','" + txt_ot_amount_billing.Text + "','" + txt_hra_amount_salary.Text + "','" + txt_hra_percent_salary.Text + "','" + ddl_relieving_uniform.SelectedValue + "','" + ddl_esic_oa_billing.SelectedValue + "','" + ddl_esic_oa_salary.SelectedValue + "','" + txt_group_insurance.Text + "','" + ddl_service_group_insurance.SelectedValue + "',1,'" + txt_bill_service_charge_amount.Text + "','" + ddl_esic_allow.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + ddl_material_contract.SelectedValue + "','" + txt_contract_rate.Text + "','" + ddl_handling_charge.SelectedValue + "','" + txt_handling_percent.Text + "','" + ddl_dc_contract.SelectedValue + "','" + ddl_dc_type.SelectedValue + "','" + txt_dc_rate.Text + "','" + txt_dc_area.Text + "','" + ddl_dc_handling_charge.SelectedValue + "','" + txt_dc_handling_percent.Text + "','" + ddl_conveyance_applicable.SelectedValue + "','" + ddl_conveyance_type.SelectedValue + "','" + txt_conveyance_rate.Text + "','" + txt_conveyance_km.Text + "','" + ddl_conveyance_service_charge.SelectedValue + "','" + txt_conveyance_service_charge.Text + "','" + txt_conveyance_service_amount.Text + "','" + ddl_equmental_applicable.SelectedValue + "','" + txt_equment_rate.Text + "','" + txt_equment_rental.Text + "','" + ddl_equmental_charges.SelectedValue + "','" + txt_equmental_percent.Text + "','" + ddl_chemical_applicable.SelectedValue + "','" + txt_chemical_unit.Text + "','" + txt_chemical.Text + "','" + ddl_chemical_charges.SelectedValue + "','" + txt_chemical_percent.Text + "','" + ddl_dustin_applicable.SelectedValue + "','" + txt_dustin_rate.Text + "','" + txt_dustin.Text + "','" + ddl_dustin_charges.SelectedValue + "','" + txt_dustin_percent.Text + "','" + ddl_femina_applicable.SelectedValue + "','" + txt_femina_unit.Text + "','" + txt_femina.Text + "','" + ddl_femina_charges.SelectedValue + "','" + txt_femina_percent.Text + "','" + ddl_aerosol_applicable.SelectedValue + "','" + txt_aerosol_rate.Text + "','" + txt_aerosol.Text + "','" + ddl_aerosol_charges.SelectedValue + "','" + txt_aerosol_percent.Text + "','" + ddl_tool_applicable.SelectedValue + "','" + txt_tool_unit.Text + "','" + txt_tool.Text + "','" + ddl_tool_charges.SelectedValue + "','" + txt_tool_percent.Text + "','" + ddl_pc_contract.SelectedValue + "','" + ddl_pc_type.SelectedValue + "','" + txt_pc_rate.Text + "','" + txt_pc_area.Text + "','" + ddl_pc_handling_charge.SelectedValue + "','" + txt_pc_handling_percent.Text + "','" + ddl_bonus_app.SelectedValue + "','" + ddl_leave_app.SelectedValue + "','" + ddl_gst_applicable.SelectedValue + "','" + ddl_cmn_pf_app.SelectedValue + "','" + ddl_lwf_act_man.SelectedValue + "','" + ddl_machine_rental_app.SelectedValue + "'," + txt_handling_amount.Text + ",'" + ddl_machine_rental_applicable.SelectedValue + "','" + txt_machine_rental_amount.Text + "','" + txt_food_rate.Text + "','" + txt_oc_rate.Text + "','" + txt_os_rate.Text + "','" + txt_nh_rate.Text + "','" + txt_km_rate.Text + "','" + txt_conveyance_amount.Text + "');select max(id) from pay_billing_master;");



                        // add(unit_code, designation1,id);



                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Added Successfully');", true);
            ddl_service_charge.SelectedValue = "0";
            txt_service_charge_rate.Text = "0";
            txt_service_charge.Text = "0";
            ddl_service_charge_adm.SelectedValue = "0";
            txt_rate_adm.Text = "0";
            txt_ser_rate_adm.Text = "0";
            gv_product_details.DataSource = null;
            gv_product_details.DataBind();
            grd_material_detail.DataSource = null;
            grd_material_detail.DataBind();

            ddl_unitcode_SelectedIndexChanged(null, null);
            ddl_clientwisestate_SelectedIndexChanged(null, null);
            //text_clear();
            load_grdview();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

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
    protected void btndelete_Click(object sender, EventArgs e)
    {
        int res = 0;
        string pn = txt_policy_name1.Text;

        res = d.operation("delete from pay_billing_master WHERE  comp_code = '" + Session["comp_code"].ToString() + "' and billing_client_code= '" + ddl_unit_client.SelectedValue + "' and billing_unit_code = '" + ddl_unitcode.SelectedValue + "' and designation = '" + ddl_designation.SelectedValue + "' and policy_name1='" + pn + "'");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Deleted Successfully');", true);
        }
        ddl_unitcode_SelectedIndexChanged(null, null);
        ddl_clientwisestate_SelectedIndexChanged(null, null);
        text_clear();
        load_grdview();
        //policy_fill();
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
    }
    protected void grd_policy_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_policy.UseAccessibleHeader = false;
            grd_policy.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void grd_policy_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grd_policy, "Select$" + e.Row.RowIndex);

        }
        e.Row.Cells[0].Visible = false;

    }
    protected void grd_policy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  ViewState["id"] = grd_policy.SelectedRow.Cells[0].Text;
        d1.con1.Open();
        try
        {
            ddl_service_charge.SelectedValue = "0";
            txt_service_charge_rate.Text = "0";
            txt_service_charge.Text = "0";
            ddl_service_charge_adm.SelectedValue = "0";
            txt_rate_adm.Text = "0";
            txt_ser_rate_adm.Text = "0";
            if (ddl_policy.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Policy First...');", true);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            //    MySqlCommand cmd2 = new MySqlCommand("select bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, date_format(end_date,'%d/%m/%Y'), hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, date_format(start_date,'%d/%m/%Y'), travelling, vda, washing,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable,leave_days_percent,leave_days,leave_taxable,gratuity_taxable_salary,bonus_policy_salary,leave_taxable_salary,leave_days_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount FROM pay_billing_master where id = " + ViewState["id"].ToString(), d1.con1);
            MySqlCommand cmd2 = new MySqlCommand("select bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, date_format(end_date,'%d/%m/%Y'), hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, date_format(start_date,'%d/%m/%Y'), travelling, vda, washing,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable,leave_days_percent,leave_days,leave_taxable,gratuity_taxable_salary,bonus_policy_salary,leave_taxable_salary,leave_days_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable ,billing_gst_applicable,pf_cmn_on, lwf_act_mon,machine_rental,handling_charges_amount,machine_rental_applicable,machine_rental_amount,food_allowance_rate,outstation_allowance_rate,outstation_food_allowance_rate,night_halt_rate,km_rate,category,conveyance_amount,service_charge_r_m,rm_service_rate_rs,rm_service_rate_percent,service_charge_admini,admini_service_rate_rs,admini_service_rate_percent FROM pay_billing_master where id = " + grd_policy.SelectedRow.Cells[0].Text, d1.con1);
            MySqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                ddl_bill_ser_uniform.SelectedValue = dr2.GetValue(0).ToString();
                ddl_bonus_taxable_billing.SelectedValue = dr2.GetValue(1).ToString();
                ddl_esic_ot.SelectedValue = dr2.GetValue(3).ToString();
                ddl_lwf_applicable.SelectedValue = dr2.GetValue(5).ToString();
                ddl_month_calc.SelectedValue = dr2.GetValue(6).ToString();
                ddl_ot_applicable.SelectedValue = dr2.GetValue(7).ToString();
                ddl_pt_applicable.SelectedValue = dr2.GetValue(8).ToString();
                txt_allowances_billing.Text = dr2.GetValue(9).ToString();
                txt_basic_billing.Text = dr2.GetValue(10).ToString();
                txt_bill_bonus_percent.Text = dr2.GetValue(11).ToString();
                txt_bill_esic_percent.Text = dr2.GetValue(12).ToString();
                txt_bill_oper_cost_amt.Text = dr2.GetValue(13).ToString();
                txt_bill_oper_cost_percent.Text = dr2.GetValue(14).ToString();
                txt_bill_pf_percent.Text = dr2.GetValue(15).ToString();
                txt_bill_relieving.Text = dr2.GetValue(16).ToString();
                txt_bill_service_charge.Text = dr2.GetValue(17).ToString();
                txt_bill_uniform_percent.Text = dr2.GetValue(18).ToString();
                txt_bill_uniform_rate.Text = dr2.GetValue(19).ToString();
                txt_cca_billing.Text = dr2.GetValue(20).ToString();
                txt_education_billing.Text = dr2.GetValue(21).ToString();
                txt_end_date.Text = dr2.GetValue(22).ToString();
                txt_hra_amount.Text = dr2.GetValue(23).ToString();
                txt_hra_percent.Text = dr2.GetValue(24).ToString();
                txt_leave_days.Text = dr2.GetValue(25).ToString();
                txt_leave_days_percent.Text = dr2.GetValue(26).ToString();
                txt_other_allow.Text = dr2.GetValue(27).ToString();
                txt_per_rate_billing.Text = dr2.GetValue(28).ToString();
                txt_policy_name1.Text = dr2.GetValue(29).ToString();
                txt_sal_bonus.Text = dr2.GetValue(30).ToString();
                txt_sal_esic.Text = dr2.GetValue(31).ToString();
                txt_sal_pf.Text = dr2.GetValue(32).ToString();
                txt_sal_uniform_percent.Text = dr2.GetValue(33).ToString();
                txt_sal_uniform_rate.Text = dr2.GetValue(34).ToString();
                txt_start_date.Text = dr2.GetValue(35).ToString();
                txt_travelling_billing.Text = dr2.GetValue(36).ToString();
                txt_vda_billing.Text = dr2.GetValue(37).ToString();
                txt_washing_billing.Text = dr2.GetValue(38).ToString();

                //----------------------------------------------------------------
                ddl_bonus_taxable_salary.SelectedValue = dr2.GetValue(39).ToString();
                txt_allowances_salary.Text = dr2.GetValue(40).ToString();
                txt_basic_salary.Text = dr2.GetValue(41).ToString();
                txt_cca_salary.Text = dr2.GetValue(42).ToString();
                txt_education_salary.Text = dr2.GetValue(43).ToString();
                txt_per_rate_salary.Text = dr2.GetValue(44).ToString();
                txt_travelling_salary.Text = dr2.GetValue(45).ToString();
                txt_vda_salary.Text = dr2.GetValue(46).ToString();
                txt_washing_salary.Text = dr2.GetValue(47).ToString();
                txt_gratuity_percent_billing.Text = dr2.GetValue(48).ToString();
                txt_national_holidays_billing.Text = dr2.GetValue(49).ToString();
                ddl_bonus_policy_aap_billing.SelectedValue = dr2.GetValue(50).ToString();
                txt_bonus_amount_billing.Text = dr2.GetValue(51).ToString();
                ddl_bill_ser_operations.SelectedValue = dr2.GetValue(52).ToString();

                ddl_unit_client.SelectedValue = dr2.GetValue(53).ToString();
                ddl_clientname_SelectedIndexChanged(null, null);
                ddl_designation.SelectedValue = dr2.GetValue(2).ToString();
                ddl_clientwisestate.SelectedValue = dr2.GetValue(55).ToString();
                ddl_clientwisestate_SelectedIndexChanged(null, null);
                ddl_unitcode.SelectedValue = dr2.GetValue(54).ToString();
                ddl_unitcode_SelectedIndexChanged(null, null);
                txt_leave_days_salary.Text = dr2.GetValue(56).ToString();
                txt_leave_days_percent_salary.Text = dr2.GetValue(57).ToString();
                txt_start_date_common.SelectedValue = dr2.GetValue(58).ToString();
                txt_end_date_common.Text = dr2.GetValue(59).ToString();
                txt_policy_name1.Text = dr2.GetValue(29).ToString();
                //txt_policy_name1.ReadOnly = true;
                ddl_designation.SelectedValue = dr2.GetValue(2).ToString();
                ddl_ot_policy.SelectedValue = dr2.GetValue(60).ToString();
                txt_ot_amount.Text = dr2.GetValue(61).ToString();
                ddl_designation_SelectedIndexChanged(null, null);
                try
                {
                    ddl_hours.SelectedValue = dr2.GetValue(4).ToString();
                }
                catch { }
                ddl_gratuity_taxable_billing.SelectedValue = dr2.GetValue(62).ToString();
                txt_leave_days_percent.Text = dr2.GetValue(63).ToString();
                txt_leave_days.Text = dr2.GetValue(64).ToString();
                ddl_leave_taxable_billing.SelectedValue = dr2.GetValue(65).ToString();
                ddl_gratuity_taxable_salary.SelectedValue = dr2.GetValue(66).ToString();
                ddl_bonus_policy_aap_salary.SelectedValue = dr2.GetValue(67).ToString();
                ddl_leave_taxable_salary.SelectedValue = dr2.GetValue(68).ToString();
                txt_leave_days_salary.Text = dr2.GetValue(69).ToString();
                ddl_ot_policy_billing.SelectedValue = dr2.GetValue(70).ToString();
                txt_ot_amount_billing.Text = dr2.GetValue(71).ToString();
                txt_hra_amount_salary.Text = dr2.GetValue(72).ToString();
                txt_hra_percent_salary.Text = dr2.GetValue(73).ToString();
                ddl_relieving_uniform.SelectedValue = dr2.GetValue(74).ToString();
                ddl_esic_oa_billing.SelectedValue = dr2.GetValue(75).ToString();
                ddl_esic_oa_salary.SelectedValue = dr2.GetValue(76).ToString();
                txt_group_insurance.Text = dr2.GetValue(77).ToString();
                ddl_service_group_insurance.SelectedValue = dr2.GetValue(78).ToString();
                txt_bill_service_charge_amount.Text = dr2.GetValue(79).ToString();
                ddl_esic_allow.SelectedValue = dr2.GetValue(80).ToString();
                if (ddl_designation.SelectedValue == "HK")
                {
                    update_ddl_material_contract(1);
                    ddl_material_contract.SelectedValue = dr2.GetValue(81).ToString();
                    //ddl_cotract_type.SelectedValue =  dr2.GetValue(81).ToString();
                    //ddl_cotract_type.Enabled = false;
                }
                else
                {
                    ddl_material_contract.SelectedValue = "0";
                }
                ddl_material_contract.SelectedValue = dr2.GetValue(82).ToString();
                txt_contract_rate.Text = dr2.GetValue(83).ToString();
                ddl_handling_charge.SelectedValue = dr2.GetValue(84).ToString();
                txt_handling_percent.Text = dr2.GetValue(85).ToString();
                //changes akshay 19/12/2018
                ddl_dc_contract.SelectedValue = dr2.GetValue(86).ToString();
                ddl_dc_type.SelectedValue = dr2.GetValue(87).ToString();
                txt_dc_rate.Text = dr2.GetValue(88).ToString();
                txt_dc_area.Text = dr2.GetValue(89).ToString();
                ddl_dc_handling_charge.SelectedValue = dr2.GetValue(90).ToString();
                txt_dc_handling_percent.Text = dr2.GetValue(91).ToString();
                ddl_conveyance_applicable.SelectedValue = dr2.GetValue(92).ToString();
                ddl_conveyance_type.SelectedValue = dr2.GetValue(93).ToString();
                txt_conveyance_rate.Text = dr2.GetValue(94).ToString();
                txt_conveyance_km.Text = dr2.GetValue(95).ToString();
                ddl_conveyance_service_charge.SelectedValue = dr2.GetValue(96).ToString();
                txt_conveyance_service_charge.Text = dr2.GetValue(97).ToString();
                txt_conveyance_service_amount.Text = dr2.GetValue(98).ToString();
                //vikas 09-01-19
                ddl_equmental_applicable.SelectedValue = dr2.GetValue(99).ToString();
                txt_equment_rate.Text = dr2.GetValue(100).ToString();
                txt_equment_rental.Text = dr2.GetValue(101).ToString();
                ddl_equmental_charges.SelectedValue = dr2.GetValue(102).ToString();
                txt_equmental_percent.Text = dr2.GetValue(103).ToString();

                ddl_chemical_applicable.SelectedValue = dr2.GetValue(104).ToString();
                txt_chemical_unit.Text = dr2.GetValue(105).ToString();
                txt_chemical.Text = dr2.GetValue(106).ToString();
                ddl_chemical_charges.SelectedValue = dr2.GetValue(107).ToString();
                txt_chemical_percent.Text = dr2.GetValue(108).ToString();

                ddl_dustin_applicable.SelectedValue = dr2.GetValue(109).ToString();
                txt_dustin_rate.Text = dr2.GetValue(110).ToString();
                txt_dustin.Text = dr2.GetValue(111).ToString();
                ddl_dustin_charges.SelectedValue = dr2.GetValue(112).ToString();
                txt_dustin_percent.Text = dr2.GetValue(113).ToString();

                ddl_femina_applicable.SelectedValue = dr2.GetValue(114).ToString();
                txt_femina_unit.Text = dr2.GetValue(115).ToString();
                txt_femina.Text = dr2.GetValue(116).ToString();
                ddl_femina_charges.SelectedValue = dr2.GetValue(117).ToString();
                txt_femina_percent.Text = dr2.GetValue(118).ToString();

                ddl_aerosol_applicable.SelectedValue = dr2.GetValue(119).ToString();
                txt_aerosol_rate.Text = dr2.GetValue(120).ToString();
                txt_aerosol.Text = dr2.GetValue(121).ToString();
                ddl_aerosol_charges.SelectedValue = dr2.GetValue(122).ToString();
                txt_aerosol_percent.Text = dr2.GetValue(123).ToString();

                ddl_tool_applicable.SelectedValue = dr2.GetValue(124).ToString();
                txt_tool_unit.Text = dr2.GetValue(125).ToString();
                txt_tool.Text = dr2.GetValue(126).ToString();
                ddl_tool_charges.SelectedValue = dr2.GetValue(127).ToString();
                txt_tool_percent.Text = dr2.GetValue(128).ToString();
                //pest control
                ddl_pc_contract.SelectedValue = dr2.GetValue(129).ToString();
                ddl_pc_type.SelectedValue = dr2.GetValue(130).ToString();
                txt_pc_rate.Text = dr2.GetValue(131).ToString();
                txt_pc_area.Text = dr2.GetValue(132).ToString();
                ddl_pc_handling_charge.SelectedValue = dr2.GetValue(133).ToString();
                txt_pc_handling_percent.Text = dr2.GetValue(134).ToString();

                ddl_bonus_app.SelectedValue = dr2.GetValue(135).ToString();

                ddl_bonus_app_SelectedIndexChanged(null, null);
                ddl_leave_app.SelectedValue = dr2.GetValue(136).ToString();
                ddl_leave_app_SelectedIndexChanged(null, null);
                ddl_gst_applicable.SelectedValue = dr2.GetValue(137).ToString();
                ddl_cmn_pf_app.SelectedValue = dr2.GetValue(138).ToString();
                ddl_lwf_act_man.SelectedValue = dr2.GetValue(139).ToString();
                ddl_machine_rental_app.SelectedValue = dr2.GetValue(140).ToString();
                txt_handling_amount.Text = dr2.GetValue(141).ToString();
                ddl_machine_rental_applicable.SelectedValue = dr2.GetValue(142).ToString();
                txt_machine_rental_amount.Text = dr2.GetValue(143).ToString();
                txt_food_rate.Text = dr2.GetValue(144).ToString();
                txt_oc_rate.Text = dr2.GetValue(145).ToString();
                txt_os_rate.Text = dr2.GetValue(146).ToString();
                txt_nh_rate.Text = dr2.GetValue(147).ToString();
                txt_km_rate.Text = dr2.GetValue(148).ToString();
                display_category();
                ddl_category.SelectedValue = dr2.GetValue(149).ToString();
                txt_conveyance_amount.Text = dr2.GetValue(150).ToString();
                ddl_service_charge.SelectedValue = dr2.GetValue(151).ToString();
                txt_service_charge_rate.Text = dr2.GetValue(152).ToString();
                txt_service_charge.Text = dr2.GetValue(153).ToString();
                ddl_service_charge_adm.SelectedValue = dr2.GetValue(154).ToString();
                txt_rate_adm.Text = dr2.GetValue(155).ToString();
                txt_ser_rate_adm.Text = dr2.GetValue(156).ToString();

                //vikas 19/06/2019
                gv_product_details.DataSource = null;
                gv_product_details.DataBind();
                MySqlCommand cmd_hd = new MySqlCommand("SELECT policy_machine_nane,policy_rate_type,policy_m_rate,policy_m_h_charges,policy_in_pre,policy_m_amount,machine_code FROM pay_machine_rental_details WHERE policy_id= " + grd_policy.SelectedRow.Cells[0].Text, d.con);
                d.con.Open();
                try
                {
                    MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
                    if (dr_hd.HasRows)
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Load(dr_hd);
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["headtable"] = dt;
                        }
                        gv_product_details.DataSource = dt;
                        gv_product_details.DataBind();
                        dr_hd.Close();
                    }
                    d.con.Close();
                }
                catch (Exception ex) { throw ex; }
                finally { d.con.Close(); }
            }
            btndelete.Visible = true;
            d1.con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con1.Close();
        }

        load_material_grdview();

        //attached_doc();
        //lbl_print_quote.Text = txt_docno.Text;
        //gv_bynumber_name.Visible = true;
    }
    private void load_material_grdview()
    {


        string unit_code1 = "";
        if (ddl_unitcode.SelectedValue == "")
        {

            unit_code1 = ddl_unitcode_without.SelectedValue.ToString();
        }
        else if (ddl_unitcode_without.SelectedValue == "")
        {
            unit_code1 = ddl_unitcode.SelectedValue.ToString();
        }
        grd_material_detail.DataSource = null;
        grd_material_detail.DataBind();

        MySqlCommand cmd1 = new MySqlCommand("SELECT `id`,  `material_name` AS 'Field1',  `rate` AS 'Field2',   CASE WHEN `handling_charges_amount` ='' THEN '0' ELSE `handling_charges_amount` END AS 'Field3',CASE WHEN `handling_percent` ='' THEN '0' ELSE `handling_percent` END AS 'Field4' FROM `pay_material_details` where client_code = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' and unit_code = '" + unit_code1 + "'  ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr1);
                grd_material_detail.DataSource = dt;
                grd_material_detail.DataBind();
                grd_material_detail.Visible = true;
            }
            else
            {
                grd_material_detail.DataSource = null;
                grd_material_detail.DataBind();
            }
            dr1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    private void text_clear()
    {
        // ddl_unit_client.SelectedValue = "Select";
        client_list();
        ddl_category.SelectedValue = "Select";
        ddl_clientwisestate.SelectedValue = "Select";
        ddl_unitcode.DataSource = null;
        ddl_unitcode.Items.Clear();
        ddl_unitcode_without.DataSource = null;
        ddl_unitcode_without.Items.Clear();
        ddl_bill_ser_uniform.SelectedIndex = 0;
        // ddl_designation.SelectedIndex = 0;
        ddl_esic_ot.SelectedIndex = 0;
        //ddl_hours.SelectedIndex = 
        ddl_lwf_applicable.SelectedIndex = 0;
        ddl_month_calc.SelectedIndex = 0;
        ddl_ot_applicable.SelectedIndex = 0;
        ddl_pt_applicable.SelectedIndex = 0;
        txt_bill_bonus_percent.Text = "8.33";
        txt_bill_esic_percent.Text = "3.25";
        txt_bill_oper_cost_amt.Text = "0";
        txt_bill_oper_cost_percent.Text = "0";
        txt_bill_pf_percent.Text = "0";
        txt_bill_relieving.Text = "0";
        txt_bill_service_charge.Text = "7";
        txt_bill_uniform_percent.Text = "0";
        txt_bill_uniform_rate.Text = "250";
        txt_end_date.Text = "";
        txt_hra_amount.Text = "0";
        txt_hra_percent.Text = "0";
        txt_leave_days.Text = "0";
        txt_leave_days_percent.Text = "0";
        txt_other_allow.Text = "0";
        txt_policy_name1.Text = "";
        txt_sal_bonus.Text = "8.33";
        txt_sal_esic.Text = "0.75";
        txt_sal_pf.Text = "0";
        txt_sal_uniform_percent.Text = "0";
        txt_sal_uniform_rate.Text = "0";
        txt_start_date.Text = "";
        txt_policy_name1.ReadOnly = false;
        txt_start_date.ReadOnly = false;
        txt_end_date.ReadOnly = false;
        txt_allowances_billing.Text = "0";
        txt_basic_billing.Text = "0";
        txt_per_rate_billing.Text = "0";
        txt_travelling_billing.Text = "0";
        txt_vda_billing.Text = "0";
        txt_washing_billing.Text = "0";
        ddl_bonus_taxable_billing.SelectedIndex = 0;
        txt_cca_billing.Text = "0";
        txt_education_billing.Text = "0";
        ddl_bonus_taxable_salary.SelectedIndex = 0;
        txt_allowances_salary.Text = "0";
        txt_basic_salary.Text = "0";
        txt_cca_salary.Text = "0";
        txt_education_salary.Text = "0";
        txt_per_rate_salary.Text = "0";
        txt_travelling_salary.Text = "0";
        txt_vda_salary.Text = "0";
        txt_washing_salary.Text = "0";
        txt_sal_graguity_per.Text = "4.81";
        txt_leave_days_salary.Text = "0";
        txt_leave_days_percent_salary.Text = "0";
        txt_start_date_common.SelectedValue = "0";
        txt_end_date_common.Text = "";
        ddl_ot_policy.SelectedValue = "No";
        txt_ot_amount.Text = "0";
        ddl_gratuity_taxable_salary.SelectedIndex = 0;
        ddl_ot_policy_billing.SelectedValue = "No";
        txt_ot_amount_billing.Text = "0";
        txt_hra_amount_salary.Text = "0";
        txt_hra_percent_salary.Text = "0";
        txt_group_insurance.Text = "0";
        ddl_service_group_insurance.SelectedValue = "1";

        ddl_dc_contract.SelectedValue = "0";
        ddl_dc_type.SelectedValue = "0";
        txt_dc_rate.Text = "0";
        txt_dc_area.Text = "0";
        ddl_dc_handling_charge.SelectedValue = "0";
        txt_dc_handling_percent.Text = "0";
        ddl_conveyance_applicable.SelectedValue = "0";
        ddl_conveyance_type.SelectedValue = "0";
        txt_conveyance_rate.Text = "0";
        txt_conveyance_km.Text = "0";
        ddl_conveyance_service_charge.SelectedValue = "0";
        txt_conveyance_service_charge.Text = "0";
        txt_conveyance_service_amount.Text = "0";
    }
    protected void ddl_clientwisestate_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            string where = "";
            ddl_unitcode.Items.Clear();
            ddl_category.Items.Clear();
            if (ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and  comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else
            {
                where = " where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master " + where, d.con);//and billing_unit_code in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "' AND state_name='" + ddl_clientwisestate.SelectedValue + "')

            MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            ddl_unitcode.DataSource = ds1.Tables[0];
            ddl_unitcode.DataValueField = "UNIT_CODE";
            ddl_unitcode.DataTextField = "CUNIT";
            ddl_unitcode.DataBind();
            //ddl_unitcode.Items.Insert(0, new ListItem("Select"));

            // ddl_Existing_policy_name.Items.Clear();
            ddl_designation.Items.Clear();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

        d.con.Open();
        try
        {
            string where = "";
            if (ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and  comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else
            {
                where = " where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            ddl_unitcode_without.Items.Clear();
            MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master " + where, d.con);
            MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            ddl_unitcode_without.DataSource = ds1.Tables[0];
            ddl_unitcode_without.DataValueField = "UNIT_CODE";
            ddl_unitcode_without.DataTextField = "CUNIT";
            ddl_unitcode_without.DataBind();
            ddl_unitcode_without.Items.Insert(0, new ListItem("ALL"));
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); display_category(); }


    }
    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_category.Items.Clear();
        IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Value;
        string listvalues_ddl_unitcode = string.Join("','", selectedValues);
        IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                                                     where item.Selected
                                                     select item.Value;
        listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);

        //ddl_Existing_policy_name.Items.Clear();
        //System.Data.DataTable dt_item = new System.Data.DataTable();
        //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(POLICY_NAME1) FROM pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "' and billing_unit_code in ('" + listvalues_ddl_unitcode + "')", d.con1);
        //d.con.Open();
        //try
        //{
        //    cmd_item.Fill(dt_item);
        //    if (dt_item.Rows.Count > 0)
        //    {
        //        ddl_Existing_policy_name.DataSource = dt_item;
        //        ddl_Existing_policy_name.DataValueField = dt_item.Columns[0].ToString();
        //        ddl_Existing_policy_name.DataTextField = dt_item.Columns[0].ToString();
        //        ddl_Existing_policy_name.DataBind();
        //    }
        //    ddl_Existing_policy_name.Items.Insert(0, new ListItem("NEW", ""));
        //    d.con.Close();
        //}
        //catch (Exception ex) { throw ex; }
        //finally
        //{
        //    d.con.Close();
        //}
        string where = "";

        if (ddl_unitcode.SelectedIndex != -1)
        {
            if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = "  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select unit_code from pay_unit_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' AND branch_status = 0) AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select unit_code from pay_unit_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' AND branch_status = 0) AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }

        }
        if (ddl_unitcode_without.SelectedIndex != -1)
        {
            if (ddl_unitcode_without.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = "  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code NOT IN  (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code NOT IN  (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
        }

        string sql = "SELECT UNIT_CODE FROM pay_unit_master " + where + "";
        string unit = "";
        unit = " and unit_code in (" + sql + ")";



        d.con.Open();
        ddl_designation.Items.Clear();
        DataTable dt_item = new System.Data.DataTable();
        //cmd_item = new MySqlDataAdapter("select GRADE_CODE, GRADE_DESC FROM pay_grade_master where COMP_CODE='" + Session["COMP_CODE"] + "'", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' " + unit + " ", d.con);
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_designation.DataSource = dt_item;
                ddl_designation.DataValueField = dt_item.Columns[0].ToString();
                ddl_designation.DataTextField = dt_item.Columns[1].ToString();
                ddl_designation.DataBind();

            }
            //ddl_designation.Items.Insert(0, new ListItem("Select", ""));
            d.con.Close();
            ddl_hours.Items.Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //swaranjal
        //string Check = ddl_designation.SelectedItem.Text;
        //string Check1 = ddl_unit_client.SelectedItem.Text;
        //if (Check == "RUNNER")
        //{
        //    ddl_conveyance_applicable.Enabled = true;
        //}
        //else if (Check1 == "RBL Bank Ltd")
        //{
        //    ddl_conveyance_applicable.Enabled = true;
        //}
        //else
        //{
        //    ddl_conveyance_applicable.SelectedValue = "0";
        //    ddl_conveyance_applicable.Enabled = false;
        //}
        IEnumerable<string> selectedValues = from item in ddl_designation.Items.Cast<ListItem>()
                                             where item.Selected
                                             select item.Text;
        string listvalues_ddl_designation = string.Join("','", selectedValues);

        selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
                         where item.Selected
                         select item.Value;
        string listvalues_ddl_unitcode = string.Join("','", selectedValues);
        IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                                                     where item.Selected
                                                     select item.Value;
        listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);
        string unit1 = "";
        string state = "";
        //unit
        if (ddl_clientwisestate.SelectedValue != "" && ddl_clientwisestate.SelectedValue != "ALL")
        {
            state = "and billing_state = '" + ddl_clientwisestate.SelectedValue + "'";
        }

        string chk_unit = d.getsinglestring("select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' " + state + " ");
        if (chk_unit != "")
        {
            unit1 = "unit_code in";
        }
        else
        {
            unit1 = "unit_code not in";
        }
        string where = "";
        if (ddl_unitcode.SelectedIndex != -1)
        {
            if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = "  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and " + unit1 + " (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and " + unit1 + " (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
            else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
        }
        if (ddl_unitcode_without.SelectedIndex != -1)
        {
            if (ddl_unitcode_without.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = "  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
            else if (ddl_unitcode_without.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
            {
                where = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in ('" + listvalues_ddl_unitcode + "') AND branch_status = 0";
            }
        }
        string sql = "SELECT UNIT_CODE FROM pay_unit_master " + where + "";
        string unit = "";
        unit = " and unit_code in (" + sql + ")";

        //where

        string where1 = "";
        if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
        {
            where1 = "  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' " + unit + " and designation in ('" + listvalues_ddl_designation + "')";
        }
        else if (ddl_unitcode.SelectedValue == "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
        {
            where1 = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state= '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' " + unit + " and designation in ('" + listvalues_ddl_designation + "')";
        }
        else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue != "ALL")
        {
            where1 = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "'" + unit + " and designation in ('" + listvalues_ddl_designation + "')";
        }
        else if (ddl_unitcode.SelectedValue != "ALL" && ddl_clientwisestate.SelectedValue == "ALL")
        {
            where1 = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' " + unit + " and designation in ('" + listvalues_ddl_designation + "')";
        }
        if (ddl_policy.SelectedValue == "1")
        {
            where1 = " WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "'  AND comp_code='" + Session["comp_code"].ToString() + "' and state = '" + ddl_clientwisestate.SelectedValue + "' and designation in ('" + listvalues_ddl_designation + "')";
        }
        d.con.Open();
        ddl_hours.Items.Clear();
        DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(hours) from pay_designation_count " + where1 + "", d.con);
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_hours.DataSource = dt_item;
                ddl_hours.DataValueField = dt_item.Columns[0].ToString();
                ddl_hours.DataTextField = dt_item.Columns[0].ToString();
                ddl_hours.DataBind();

            }
            d.con.Close();

            ddl_material_contract.Items.Clear();
            ddl_material_contract.Items.Add(new ListItem("No", "0"));
            agreement_date();
            txt_start_date.ReadOnly = true;
            txt_end_date.ReadOnly = true;

            if (listvalues_ddl_designation.Equals("HOUSEKEEPING"))
            {

                ddl_material_contract.Items.Add(new ListItem("Fix", "1"));
                ddl_material_contract.Items.Add(new ListItem("Sqr.Ft", "2"));
                ddl_material_contract.Items.Add(new ListItem("Fix material", "3"));
                ddl_material_contract.Items.Add(new ListItem("Employeewise", "4"));

            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            if (ddl_policy.SelectedValue != "1") { display_category(); }
        }
    }
    protected void agreement_date()
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            d.con.Open();
            string unit_code2 = "";
            if (ddl_unitcode.SelectedValue == "")
            {

                unit_code2 = ddl_unitcode_without.SelectedValue.ToString();
            }
            else if (ddl_unitcode_without.SelectedValue == "")
            {
                unit_code2 = ddl_unitcode.SelectedValue.ToString();
            }
            string unit_code = null;

            unit_code = (" SELECT group_concat( unit_code) FROM pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "'");
            // unit_code = "'" + unit_code + "'";
            // unit_code = unit_code.Replace(",", "','");
            string unit = null;
            if (ddl_policy.SelectedValue == "1")
            {
                unit = unit_code;
            }
            else if (ddl_policy.SelectedValue == "2")
            {
                unit = unit_code2;
                unit = "'" + unit_code2 + "'";
            }


            MySqlCommand cmd_item = null;
            if (ddl_policy.SelectedValue == "1")
            {
                cmd_item = new MySqlCommand("select  DATE_FORMAT(unit_start_date, '%d/%m/%Y'), DATE_FORMAT(unit_end_date, '%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION='" + ddl_designation.SelectedItem + "'  AND `unit_start_date` IS NOT NULL AND `unit_end_date` IS NOT NULL", d.con);
            }
            else
            {
                cmd_item = new MySqlCommand("select  DATE_FORMAT(unit_start_date, '%d/%m/%Y'), DATE_FORMAT(unit_end_date, '%d/%m/%Y') from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and client_code='" + ddl_unit_client.SelectedValue + "' and unit_code IN(" + unit + ")and STATE='" + ddl_clientwisestate.SelectedValue + "' and DESIGNATION='" + ddl_designation.SelectedItem + "'  AND `unit_start_date` IS NOT NULL AND `unit_end_date` IS NOT NULL ", d.con);
            }
            MySqlDataReader dr = cmd_item.ExecuteReader();
            if (dr.Read())
            {
                txt_start_date.Text = dr.GetValue(0).ToString();
                txt_end_date.Text = dr.GetValue(1).ToString();
            }

        }
        catch (Exception ex) { }
        finally
        {

            d.con.Close();
        }
    }



    //komal 2-05-19
    protected void lnk_btn_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        string unit_code1 = "";
        if (txt_handling_percent.Text == "")
        {
            txt_handling_percent.Text = "0";

        }

        if (txt_handling_amount.Text == "")
        {
            txt_handling_amount.Text = "0";

        }
        if (ddl_unitcode.SelectedValue == "")
        {

            unit_code1 = ddl_unitcode_without.SelectedValue.ToString();
        }
        else if (ddl_unitcode_without.SelectedValue == "")
        {
            unit_code1 = ddl_unitcode.SelectedValue.ToString();
        }
        int result = d.operation("insert into pay_material_details(material_name,rate,handling_charges_amount,handling_percent,client_code,state,unit_code,new_policy_name,start_date,end_date,designation,comp_code,handling_applicable)values('" + txt_material_name.Text.ToString() + "','" + txt_contract_rate.Text.ToString() + "','" + txt_handling_amount.Text.ToString() + "','" + txt_handling_percent.Text.ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + unit_code1 + "','" + txt_policy_name1.Text + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + ddl_designation.SelectedValue + "','" + Session["COMP_CODE"].ToString() + "','" + ddl_handling_charge.SelectedValue + "')");

        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Successfully Added... !!!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Material Addition Failed... !!!');", true);
        }


        txt_material_name.Text = "";
        txt_contract_rate.Text = "0";
        ddl_handling_charge.SelectedValue = "0";

        txt_handling_percent.Text = "0";
        txt_handling_amount.Text = "0";
        if (txt_handling_percent.Text == "")
        {

            txt_handling_percent.Text = "0";
        }

        if (txt_handling_amount.Text == "")
        {

            txt_handling_amount.Text = "0";
        }

        load_material_grdview();



        // hidtab.Value = "4";


        //grd_material_detail.Visible = true;
        //DataTable dt = new DataTable();
        //DataRow dr;
        //dt.Columns.Add("Field1");
        //dt.Columns.Add("Field2");
        //dt.Columns.Add("Field3");
        //dt.Columns.Add("Field4");

        //int rownum = 0;
        //for (rownum = 0; rownum < grd_material_detail.Rows.Count; rownum++)
        //{
        //    if (grd_material_detail.Rows[rownum].RowType == DataControlRowType.DataRow)
        //    {
        //        dr = dt.NewRow();
        //        Label lbl_material_name = (Label)grd_material_detail.Rows[rownum].Cells[1].FindControl("lbl_material_name");
        //        dr["Field1"] = lbl_material_name.Text.ToString();

        //        Label lbl_rate = (Label)grd_material_detail.Rows[rownum].Cells[2].FindControl("lbl_rate");
        //        dr["Field2"] = lbl_rate.Text.ToString();

        //        Label lbl_handling_charges = (Label)grd_material_detail.Rows[rownum].Cells[3].FindControl("lbl_handling_charges");
        //        dr["Field3"] = lbl_handling_charges.Text.ToString();

        //        Label lbl_handling = (Label)grd_material_detail.Rows[rownum].Cells[4].FindControl("lbl_handling");
        //        dr["Field4"] = lbl_handling.Text.ToString();


        //        //dr["Field1"] = grd_material_detail.Rows[rownum].Cells[0].Text;
        //        //dr["Field2"] = grd_material_detail.Rows[rownum].Cells[1].Text;
        //        //dr["Field3"] = grd_material_detail.Rows[rownum].Cells[2].Text;
        //        //dr["Field4"] = grd_material_detail.Rows[rownum].Cells[3].Text;
        //        dt.Rows.Add(dr);
        //    }
        //}

        //dr = dt.NewRow();
        //dr["Field1"] = txt_material_name.Text;
        //dr["Field2"] = txt_contract_rate.Text;
        //dr["Field3"] = ddl_handling_charge.SelectedItem;
        //dr["Field4"] = txt_handling_percent.Text;

        //dt.Rows.Add(dr);
        //grd_material_detail.DataSource = dt;
        //grd_material_detail.DataBind();
        //grd_material_detail.Visible = true;
        //ViewState["grd_material_detail"] = dt;
        //txt_material_name.Text = "";
        //txt_contract_rate.Text = "";
        //ddl_handling_charge.Text = "";
        //txt_handling_percent.Text = "";
        //ddl_cotract_type.SelectedValue = "0";


    }

    //komal 3-05-19
    //public void material_detail_save()

    //{


    //try {
    //    d.con.Open();
    //   foreach (GridViewRow row in grd_material_detail.Rows)
    //    {

    //        Label lbl_material_name = (Label)row.FindControl("lbl_material_name");
    //        string lbl_material_name_1 = (lbl_material_name.Text);

    //        Label lbl_rate = (Label)row.FindControl("lbl_rate");
    //        string lbl_rate_1 = (lbl_rate.Text);

    //        Label lbl_handling_charges = (Label)row.FindControl("lbl_handling_charges");
    //        string lbl_handling_charges_1 = (lbl_handling_charges.Text);

    //        Label lbl_handling = (Label)row.FindControl("lbl_handling");
    //        string lbl_handling_1 = (lbl_handling.Text);
    //        if (lbl_handling_charges_1 == "Yes")
    //        {
    //            lbl_handling_charges_1 = "1";
    //        }
    //        else
    //        {
    //            lbl_handling_charges_1 = "0";
    //        }

    //        int result = 0;
    //        string id = d.getsinglestring("select max(id)from pay_billing_master ");
    //        result = d.operation("insert into pay_material_details(material_name,rate,handling_charges_applicable,handling_percent,client_name,state,branch_having_policy,brach_not_having_policy,new_policy_name,start_date,end_date,designation,material_id)values('" + lbl_material_name_1.ToString() + "','" + lbl_rate_1.ToString() + "','" + lbl_handling_charges_1.ToString() + "','" + lbl_handling_1.ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + ddl_unitcode_without.SelectedValue + "','" + txt_policy_name1.Text + "','" + txt_start_date.Text + "','" + txt_end_date.Text + "','" + ddl_designation.SelectedValue + "','" + id + "')");

    //        if (result > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
    //        }

    //    }

    //}

    //catch (Exception ex) { throw ex; }
    //finally { d.con.Close(); }
    //}

    protected void lnk_remove_bank_Click1(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        // string cell_1_Value = grd_material_detail.Rows[gr.RowIndex].Cells[0].Text;
        d.operation("delete from pay_material_details where id = '" + grdrow.Cells[1].Text + "'");

        load_material_grdview();
    }
    //protected void btn_update_Click(object sender, EventArgs e)
    //{



    //}
    //protected void grd_policy_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    try
    //    {

    //        d.con.Open();

    //        string id = grd_material_detail.SelectedRow.Cells[0].Text.ToString();

    //        MySqlCommand master_daily = new MySqlCommand(" Select material_name,rate,handling_charges_applicable,handling_percentfrom pay_material_details where ", d.con);
    //        MySqlDataAdapter dr_master1 = new MySqlDataAdapter(master_daily);
    //        DataTable dt_master1 = new DataTable();
    //        dr_master1.Fill(dt_master1);
    //        if (dt_master1.Rows.Count > 0)
    //        {
    //            grd_material_detail.DataSource = dt_master1;
    //            grd_material_detail.DataBind();
    //            //gv_client_policy.Visible = false;

    //        }


    //        // gv_client_policy.Visible = true;
    //        //btn_update.Visible = true;


    //    }

    //    catch (Exception ex) { }
    //    finally { d.con.Close(); }

    //}

    protected void grd_material_detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
    }

    protected void grd_material_detail_PreRender(object sender, EventArgs e)
    {
        try
        {
            grd_material_detail.UseAccessibleHeader = false;
            grd_material_detail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    private void update_ddl_material_contract(int counter)
    {
        ddl_material_contract.Items.Clear();
        if (counter == 0)
        {
            ddl_material_contract.Items.Insert(0, new ListItem("No"));
        }
        else if (counter == 1)
        {
            ddl_material_contract.Items.Insert(0, new ListItem("No", "0"));
            ddl_material_contract.Items.Insert(1, new ListItem("Fix", "1"));
            ddl_material_contract.Items.Insert(2, new ListItem("Sqr.Ft", "2"));
            ddl_material_contract.Items.Insert(3, new ListItem("Fix material", "3"));
            ddl_material_contract.Items.Insert(4, new ListItem("Employeewise", "4"));

        }
    }
    //vikas code for machine policy
    protected void Machine_list()
    {
        ddl_machine.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select ITEM_code,ITEM_NAME from pay_item_master where `product_service`='Machine'", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_machine.DataSource = dt_item;
                ddl_machine.DataValueField = dt_item.Columns[0].ToString();
                ddl_machine.DataTextField = dt_item.Columns[1].ToString();
                ddl_machine.DataBind();

            }
            dt_item.Dispose();

            d.con.Close();
            ddl_machine.Items.Insert(0, "Select");

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_add_machine_Click(object sender, EventArgs e)
    {
        hidtab.Value = "7";
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        foreach (GridViewRow row in gv_product_details.Rows)
        {

            string machine_code = gv_product_details.Rows[row.RowIndex].Cells[8].Text;
            if (ddl_machine.SelectedValue.Equals(machine_code))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Machine Already Added!!')", true);
                return;
            }
        }
        //try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        //catch { }
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("policy_machine_nane");
        dt.Columns.Add("policy_rate_type");
        dt.Columns.Add("policy_m_rate");
        dt.Columns.Add("policy_m_h_charges");
        dt.Columns.Add("policy_in_pre");
        dt.Columns.Add("policy_m_amount");
        dt.Columns.Add("machine_code");
        int rownum = 0;
        for (rownum = 0; rownum < gv_product_details.Rows.Count; rownum++)
        {
            if (gv_product_details.Rows[rownum].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                dr["policy_machine_nane"] = gv_product_details.Rows[rownum].Cells[2].Text;
                dr["policy_rate_type"] = gv_product_details.Rows[rownum].Cells[3].Text;
                dr["policy_m_rate"] = gv_product_details.Rows[rownum].Cells[4].Text;
                dr["policy_m_h_charges"] = gv_product_details.Rows[rownum].Cells[5].Text;
                dr["policy_in_pre"] = gv_product_details.Rows[rownum].Cells[6].Text;
                dr["policy_m_amount"] = gv_product_details.Rows[rownum].Cells[7].Text;
                dr["machine_code"] = gv_product_details.Rows[rownum].Cells[8].Text;
                dt.Rows.Add(dr);
            }
        }

        dr = dt.NewRow();

        dr["policy_machine_nane"] = ddl_machine.SelectedItem.Text;
        dr["policy_rate_type"] = ddl_rate_type.SelectedValue;
        dr["policy_m_rate"] = txt_machine_amount.Text;
        dr["policy_m_h_charges"] = ddl_h_c_applicable.SelectedValue;
        dr["policy_in_pre"] = txt_in_per.Text;
        dr["policy_m_amount"] = txt_in_amt.Text;
        dr["machine_code"] = ddl_machine.SelectedValue;

        dt.Rows.Add(dr);
        gv_product_details.DataSource = dt;
        gv_product_details.DataBind();

        ViewState["headtable"] = dt;
        Panel4.Visible = true;
        clear_machine();
    }
    protected void gv_product_details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;")
                {
                    e.Row.Cells[i].Text = "";
                }
            }

        }
        e.Row.Cells[8].Visible = false;
    }
    protected void lnk_remove_product_Click(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        if (ViewState["headtable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["headtable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["headtable"] = dt;
            gv_product_details.DataSource = dt;
            gv_product_details.DataBind();
        }
    }

    protected void add(string unit_code, string designation1, string id)
    {
        if (ddl_designation.SelectedValue == "HK")
        {
            d.operation("delete from pay_machine_rental_details where `comp_code`='" + Session["COMP_CODE"] + "' and `billing_unit_code` = '" + unit_code + "'");
            if (id == "")
            {
                //string policy_id = d.getsinglestring("select max(id) from pay_billing_master ");
                foreach (GridViewRow row in gv_product_details.Rows)
                {
                    d.operation("INSERT INTO pay_machine_rental_details (comp_code,policy_machine_nane,policy_rate_type,policy_m_rate,policy_m_h_charges,policy_in_pre,policy_m_amount,billing_client_code,billing_state,billing_unit_code,policy_id,machine_code)VALUES('" + Session["COMP_CODE"].ToString() + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + id + "','" + row.Cells[8].Text + "')");
                }
            }
            else
            {

                d.operation("delete from pay_machine_rental_details where policy_id= " + id);
                //  string policy_id = d.getsinglestring("select max(id)  from pay_billing_master ");
                foreach (GridViewRow row in gv_product_details.Rows)
                {
                    d.operation("INSERT INTO pay_machine_rental_details (comp_code,policy_machine_nane,policy_rate_type,policy_m_rate,policy_m_h_charges,policy_in_pre,policy_m_amount,billing_client_code,billing_state,billing_unit_code,policy_id,machine_code)VALUES('" + Session["COMP_CODE"].ToString() + "','" + row.Cells[2].Text + "','" + row.Cells[3].Text + "','" + row.Cells[4].Text + "','" + row.Cells[5].Text + "','" + row.Cells[6].Text + "','" + row.Cells[7].Text + "','" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','" + id + "','" + row.Cells[8].Text + "')");
                }
            }
        }

    }
    protected void clear_machine()
    {
        ddl_machine.SelectedValue = "Select";
        ddl_rate_type.SelectedValue = "Select";
        txt_machine_amount.Text = "0";
        ddl_h_c_applicable.SelectedValue = "No";
        txt_in_per.Text = "0";
        txt_in_amt.Text = "0";
    }
    //komal 20-06-19
    protected void billing_dates()
    {



        MySqlCommand cmd_billing = new MySqlCommand("select start_date_billing,end_date_billing from pay_client_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE ='" + ddl_unit_client.SelectedValue + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_billing.ExecuteReader();
            if (dr_item1.Read())
            {
                txt_start_date_common.SelectedValue = dr_item1.GetValue(0).ToString();
                txt_end_date_common.Text = dr_item1.GetValue(1).ToString();

            }


        }

        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }





    }

    protected void ddl_bonus_app_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_bonus_app.SelectedValue == "0")
        {
            ddl_bonus_policy_aap_billing.Enabled = false;
            ddl_bonus_taxable_billing.Enabled = false;
            txt_bonus_amount_billing.ReadOnly = true;
            txt_bill_bonus_percent.ReadOnly = true;
            ddl_bonus_policy_aap_salary.Enabled = false;
            ddl_bonus_taxable_salary.Enabled = false;
            txt_bonus_amount_salary.ReadOnly = true;
            txt_sal_bonus.ReadOnly = true;

        }
        else
        {
            ddl_bonus_policy_aap_billing.Enabled = true;
            ddl_bonus_taxable_billing.Enabled = true;
            txt_bonus_amount_billing.ReadOnly = false;
            txt_bill_bonus_percent.ReadOnly = false;
            ddl_bonus_policy_aap_salary.Enabled = true;
            ddl_bonus_taxable_salary.Enabled = true;
            txt_bonus_amount_salary.ReadOnly = false;
            txt_sal_bonus.ReadOnly = false;

        }
    }
    protected void ddl_leave_app_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_leave_app.SelectedValue == "0")
        {
            ddl_leave_taxable_billing.Enabled = false;
            txt_leave_days.ReadOnly = true;
            txt_leave_days_percent.ReadOnly = true;
            ddl_leave_taxable_salary.Enabled = false;
            txt_leave_days_salary.ReadOnly = true;
            txt_leave_days_percent_salary.ReadOnly = true;
        }
        else
        {
            ddl_leave_taxable_billing.Enabled = true;
            txt_leave_days.ReadOnly = false;
            txt_leave_days_percent.ReadOnly = false;
            ddl_leave_taxable_salary.Enabled = true;
            txt_leave_days_salary.ReadOnly = false;
            txt_leave_days_percent_salary.ReadOnly = false;
        }
    }
    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.con.Open();
        ddl_designation.Items.Clear();
        DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' ", d.con);
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_designation.DataSource = dt_item;
                ddl_designation.DataValueField = dt_item.Columns[0].ToString();
                ddl_designation.DataTextField = dt_item.Columns[1].ToString();
                ddl_designation.DataBind();

            }
            //ddl_designation.Items.Insert(0, new ListItem("Select", ""));
            d.con.Close();
            ddl_hours.Items.Clear();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void display_category()
    {
        ddl_category.Items.Clear();
        //Categories
        if (ddl_policy.SelectedValue == "1")
        {
            d.con.Open();
            try
            {

                MySqlCommand cmd2 = new MySqlCommand("select category from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' and unit_code is null group by category", d.con);
                MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                ddl_category.DataSource = ds1.Tables[0];
                ddl_category.DataValueField = "category";
                ddl_category.DataTextField = "category";
                ddl_category.DataBind();

                ddl_category.Items.Insert(0, new ListItem("Select"));
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }
        if (ddl_policy.SelectedValue == "2" && (ddl_unitcode.SelectedValue != "" || ddl_unitcode_without.SelectedValue != ""))
        {
            string unit = "";
            IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
                                                 where item.Selected
                                                 select item.Value;
            string listvalues_ddl_unitcode = string.Join("','", selectedValues);
            IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                                                         where item.Selected
                                                         select item.Value;
            listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);
            if (ddl_unitcode.SelectedValue == "ALL" || ddl_unitcode_without.SelectedValue == "ALL")
            {
                unit = "select unit_code from pay_unit_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND branch_status = 0";
            }
            else
            {
                unit = "'" + listvalues_ddl_unitcode + "'";
            }

            d.con.Open();
            try
            {

                MySqlCommand cmd2 = new MySqlCommand("select category from pay_designation_count where comp_code='" + Session["comp_code"].ToString() + "' and CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code in ( " + unit + ")  AND unit_code IS NOT NULL and designation = '" + ddl_designation.SelectedItem + "' group by designation", d.con);
                MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                ddl_category.DataSource = ds1.Tables[0];
                ddl_category.DataValueField = "category";
                ddl_category.DataTextField = "category";
                ddl_category.DataBind();


                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
        }

    }
    protected void btn_assign_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }

        try
        {
            string unit = "";
            string policy = d.getsinglestring("select * from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and designation = '" + ddl_designation.SelectedValue + "' and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' ");
            if (policy == "")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Policy is not present to Assign!!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy is not present to Assign!!');", true);
                return;
            }
            IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                                                         where item.Selected
                                                         select item.Value;
            string listvalues_ddl_unitcode = string.Join("','", selectedValues_without);
            IEnumerable<string> selectedValues_with = from item in ddl_unitcode.Items.Cast<ListItem>()
                                                      where item.Selected
                                                      select item.Value;
            string listvalues_ddl_unitcode1 = string.Join("','", selectedValues_with);

            if (ddl_unitcode_without.SelectedValue == "ALL")
            {
                unit = d.getsinglestring("SELECT GROUP_CONCAT(UNIT_CODE) FROM pay_designation_count where CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') and designation = '" + ddl_designation.SelectedItem + "' and hours = '" + ddl_hours.SelectedValue + "' and category = '" + ddl_category.SelectedValue + "' AND branch_status = 0");
            }
            else
            {
                unit = "" + listvalues_ddl_unitcode + "";
                if (unit == "") { unit = "" + listvalues_ddl_unitcode1 + ""; }
            }



            string[] ddd = unit.Split(',');
            var elements = ddd;

            foreach (string unit_code in elements)
            {
                string date_start = "";
                string date_end = "";
                if (ddl_policy.SelectedValue == "1")
                {
                    date_start = d.getsinglestring("select date_format(unit_start_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");
                    date_end = d.getsinglestring("select date_format(unit_end_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");

                }
                else if (ddl_policy.SelectedValue == "2")
                {
                    if (ddl_unitcode_without.SelectedValue == "ALL")
                    {
                        date_start = d.getsinglestring("select date_format(unit_start_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");
                        date_end = d.getsinglestring("select date_format(unit_end_date,'%d/%m/%Y') from pay_designation_count  WHERE  `comp_code` = '" + Session["comp_code"].ToString() + "'   AND `client_code` ='" + ddl_unit_client.SelectedValue + "'   AND `STATE` = '" + ddl_clientwisestate.SelectedValue + "'   AND `DESIGNATION` = '" + ddl_designation.SelectedItem + "' and unit_code='" + unit_code + "' ");
                    }
                    else
                    {
                        date_start = txt_start_date.Text;
                        date_end = txt_end_date.Text;
                    }

                }

                string sql = "select '" + Session["COMP_CODE"] + "',bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, str_to_date('" + date_end + "','%d/%m/%Y'), hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, str_to_date('" + date_start + "','%d/%m/%Y'), travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,'" + ddl_unit_client.SelectedValue + "','" + unit_code + "',billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon,machine_rental,handling_charges_amount,machine_rental_applicable,machine_rental_amount,food_allowance_rate,outstation_allowance_rate,outstation_food_allowance_rate,night_halt_rate,km_rate,category from pay_billing_master where billing_client_code = '" + ddl_unit_client.SelectedValue + "'  AND billing_state = '" + ddl_clientwisestate.SelectedValue + "'  AND designation = '" + ddl_designation.SelectedValue + "'  AND category = '" + ddl_category.SelectedValue + "' and hours = '" + ddl_hours.SelectedValue + "' limit 1";
                string id = d.getsinglestring("insert into pay_billing_master (comp_code,bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bill_bonus_percent, bill_esic_percent, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing,basic_vda,leave_taxable,gratuity_taxable,bonus_taxable_salary,allowances_salary,basic_salary,cca_salary,education_salary,per_rate_salary,travelling_salary,vda_salary,washing_salary,create_user,create_date,sal_bonus_amount,leave_taxable_salary,bonus_policy_salary,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing,bill_ser_operations,billing_client_code,billing_unit_code,billing_state,gratuity_salary,leave_days_salary,leave_days_percent_salary,start_date_common,end_date_common,ot_policy_salary,ot_amount_salary,gratuity_taxable_salary,ot_policy_billing,ot_amount_billing,hra_amount_salary,hra_percent_salary,relieving_uniform_billing,esic_oa_billing,esic_oa_salary,group_insurance_billing,service_group_insurance_billing,processed,bill_service_charge_amount,esic_common_allow,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,equmental_handling_percent,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon,machine_rental,handling_charges_amount,machine_rental_applicable,machine_rental_amount,food_allowance_rate,outstation_allowance_rate,outstation_food_allowance_rate,night_halt_rate,km_rate,category) " + sql + "");

            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Policy Assign Successfully');", true);


            ddl_unitcode_SelectedIndexChanged(null, null);
            ddl_clientwisestate_SelectedIndexChanged(null, null);
            //text_clear();
            load_grdview();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch (Exception ex) { throw ex; }
    }
}