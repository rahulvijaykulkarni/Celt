using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BillingSalary
/// </summary>
public class BillingSalary
{
    DAL d = new DAL();

    public void month_days_calc(string comp_code, string client_code, string state, string unit_code, int month, int year, int start_date, int end_date)
    {
        int date_diff = 0;
        //double present_days = 0;
        //  string client_st_date = d.getsinglestring("select start_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "'");
        // string client_end_date = d.getsinglestring("select end_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "'");
        string days = "", where = "";
        double pcount = 0;
        int actual_date_diff = (end_date - start_date) + 1;
        //date_diff = (end_date - start_date) + 1;
        //if (start_date == 0 || end_date == 0)
        //{
        //    date_diff = 0;
        //}
        // Month Days Calc
        for (int i = start_date; i <= end_date; i++)
        {
            //if (new DateTime(year, month, i).DayOfWeek == DayOfWeek.Sunday)
            //{ date_diff--; }
            if (i < 10)
            {
                days = days + "pay_attendance_muster.DAY" + "0" + i + ",";
            }
            else
            {
                days = days + "pay_attendance_muster.DAY" + i + ",";
            }

        }
        d.operation("Drop table IF EXISTS attendance_muster");
        d.operation("CREATE TABLE attendance_muster SELECT * FROM pay_attendance_muster WHERE 1 = 2");
        int j = 0, working_day = 0;
        // Present days count
        where = " comp_code = '" + comp_code + "' and unit_code = '" + unit_code + "' and month = '" + month + "' and Year = '" + year + "' and tot_days_present > 0";
        if (state == "ALL")
        {
            where = " comp_code = '" + comp_code + "'  and month = '" + month + "' and Year = '" + year + "' and tot_days_present > 0 and unit_code in (Select unit_code from pay_unit_master where comp_code = '" + comp_code + "' and client_code = '" + client_code + "')";
        }
        else if (unit_code == "ALL")
        {
            where = " comp_code = '" + comp_code + "' and month = '" + month + "' and Year = '" + year + "'  and tot_days_present > 0  and unit_code in (Select unit_code from pay_unit_master where comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name  = '" + state + "')";
        }

        MySqlCommand menu_days = new MySqlCommand("Select " + days + " emp_code,unit_code From pay_attendance_muster where " + where, d.con1);
        d.con1.Open();
        try
        {
            MySqlDataReader dr_days = menu_days.ExecuteReader();
            while (dr_days.Read())
            {
                do
                {
                    if (dr_days.GetValue(j).ToString() == "P" || dr_days.GetValue(j).ToString() == "PH")
                    {
                        pcount++;
                    }
                    else if (dr_days.GetValue(j).ToString() == "HD") { pcount += 0.5; }
                    j++;
                    // if (dr_days.GetValue(j).ToString() != "W") { working_day++; }
                }
                while (j < actual_date_diff);
                //present_days = pcount;

                d.operation("Insert into attendance_muster (Select * from pay_attendance_muster where comp_code = '" + comp_code + "' and month = " + month + " and year = " + year + " and emp_code = '" + dr_days.GetValue(j).ToString() + "')");

                d.operation("Update attendance_muster Set TOT_DAYS_PRESENT = " + pcount + "  where comp_code = '" + comp_code + "' and month = '" + month + "' and Year = '" + year + "' and emp_code = '" + dr_days.GetValue(j).ToString() + "'");

                pcount = 0;
                j = 0;
                //working_day = 0;
            }
            dr_days.Dispose();
        }
        catch (Exception e) { }
        finally { d.con1.Close(); }
    }

    public void Billing(string comp_code, string client_code, string state, string unit_code, string month, string year, string login_id, int counter, int from_to_date, int start_date, string from_date, string to_date, string region)
    {
        double bonus = 0, washing = 0, travelling = 0, education = 0, bill_OA = 0, bill_OA_ESIC = 0, cca = 0, common_allowance = 0, bonus_taxable = 0, bonus_non_taxable = 0, leave_taxable = 0, leave_non_taxable = 0, gratuity_taxable = 0, gratuity_non_taxable = 0, hra_amount = 0, special_allowance = 0, gross = 0, national_holiday = 0, pf = 0, esic = 0, uniform_ser = 0, uniform_no_ser = 0, uniform_relieving = 0, lwf = 0, operation_cost = 0, operation_cost_ser = 0, sub_total_A = 0, sub_total_B = 0, sub_total_C = 0, sub_total_AB = 0, total = 0, grand_total = 0, group_insurance_ser = 0, group_insurance = 0, ot_rate = 0, ot_esic = 0, relieving_charge = 0, service_charge = 0, esic_common_allowance = 0;
        MySqlCommand menu;
        string menu_where = "";
        string history_where = "",where ="";
        //vikas add for marging
        string history_where1 = "";
        string pay_billing_unit_rate = "pay_billing_unit_rate";
        string pay_billing_master_history = "pay_billing_master_history";


        if (counter == 1)
        {

            pay_billing_unit_rate = "pay_billing_from_to_unit_rate";
            pay_billing_master_history = "pay_billing_from_to_history";
        }
        //vikas add for merging arrears
        else if (counter == 2)
        {
            pay_billing_unit_rate = "pay_billing_unit_rate_arrears";
            pay_billing_master_history = "pay_billing_master_history_arrears";
        }
        if (state == "ALL")
        {
            d.operation("delete from  " + pay_billing_unit_rate + "  where month='" + month + "' and Year = '" + year + "' and client_code = '" + client_code + "' and comp_code='" + comp_code + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + comp_code + "' and client_code = '" + client_code + "')");
            d.operation("delete from " + pay_billing_master_history + " where month='" + month + "' and Year = '" + year + "' and billing_client_code = '" + client_code + "' and comp_code='" + comp_code + "'  and type='billing'");

            history_where = " billing_client_code = '" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "' ";
            //vikas add for marging
            history_where1 = " pay_billing_master_history.billing_client_code='" + client_code + "' and pay_billing_master_history.comp_code = '" + comp_code + "' and pay_billing_master_history.month='" + month + "' and pay_billing_master_history.year= '" + year + "'  having basic_vda!='0' ";
            where = " client_code='" + client_code + "' and comp_code = '" + comp_code + "' and month='" + month + "' and year= '" + year + "' ";
            menu_where = " month='" + month + "' and Year = '" + year + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.comp_code='" + comp_code + "' and month_end=0 AND (pay_unit_master.branch_close_date is null || pay_unit_master.branch_close_date = '' || pay_unit_master.branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) and type='billing'";
        }
        else if (unit_code == "ALL")
        {
            d.operation("delete from " + pay_billing_unit_rate + "  where month='" + month + "' and Year = '" + year + "' and client_code = '" + client_code + "' and comp_code='" + comp_code + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state + "')");
            d.operation("delete from " + pay_billing_master_history + " where month='" + month + "' and Year = '" + year + "' and billing_client_code = '" + client_code + "' and comp_code='" + comp_code + "' and billing_state= '" + state + "'  and type='billing'");
            where = " client_code = '" + client_code + "' and state_name ='" + state + "' and comp_code='" + comp_code + "' and  month='" + month + "' and Year = '" + year + "'";
            history_where = " billing_client_code = '" + client_code + "' and billing_state='" + state + "' and pay_billing_master.comp_code='" + comp_code + "'";
            //vikas add for marging
            history_where1 = " pay_billing_master_history.billing_client_code='" + client_code + "' and pay_billing_master_history.billing_state ='" + state + "' and pay_billing_master_history.comp_code = '" + comp_code + "' and pay_billing_master_history.month='" + month + "' and pay_billing_master_history.year= '" + year + "'  having basic_vda!='0' ";
            //comment 30/09 menu_where = " month='" + month + "' and Year = '" + year + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state + "' and pay_unit_master.comp_code='" + comp_code + "' and month_end=0 and pay_unit_master.branch_Status=0 and type='billing'";    
            menu_where = " month='" + month + "' and Year = '" + year + "' and pay_unit_master.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state + "' and pay_unit_master.comp_code='" + comp_code + "' and month_end=0 AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) and type='billing'";
        }
        else
        {
            d.operation("delete from " + pay_billing_unit_rate + " where month='" + month + "' and Year = '" + year + "' and client_code = '" + client_code + "' and unit_code = '" + unit_code + "' and comp_code='" + comp_code + "'");
            d.operation("delete from " + pay_billing_master_history + " where month='" + month + "' and Year = '" + year + "' and billing_client_code = '" + client_code + "' and billing_unit_code = '" + unit_code + "' and comp_code='" + comp_code + "' and type='billing'");
            where = " unit_code ='" + unit_code + "' and comp_code='" + comp_code + "' and month='" + month + "' and year= '" + year + "'  ";
            history_where = "billing_unit_code ='" + unit_code + "' and pay_billing_master.comp_code='" + comp_code + "'";
            history_where1 = " pay_billing_master_history.billing_unit_code ='" + unit_code + "' and pay_billing_master_history.comp_code='" + comp_code + "' and pay_billing_master_history.month='" + month + "' and pay_billing_master_history.year= '" + year + "'  having basic_vda!='0' ";
            // coment 30/09 branch_status menu_where = " month='" + month + "' and Year = '" + year + "' and pay_unit_master.unit_code ='" + unit_code + "' and pay_unit_master.comp_code='" + comp_code + "' and month_end=0 and pay_unit_master.branch_Status=0 and type='billing'";
            menu_where = " month='" + month + "' and Year = '" + year + "' and pay_unit_master.unit_code ='" + unit_code + "' and pay_unit_master.comp_code='" + comp_code + "' and month_end=0 AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) and type='billing'";
        }
        string basic_vda_arr = "basic,vda";

        if (counter == 2)
        {
            d.operation("insert into " + pay_billing_master_history + " (comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow,create_user,create_date,month,year,type,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,equmental_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon,diff_basic_vda,diff_basic,diff_vda) SELECT pay_billing_master_history.comp_code,pay_billing_master_history.billing_client_code,pay_billing_master_history.billing_state,pay_billing_master_history.billing_unit_code,pay_billing_master_history.bill_ser_uniform,pay_billing_master_history.bonus_taxable,pay_billing_master_history.designation,pay_billing_master_history.esic_ot,pay_billing_master_history.hours,pay_billing_master_history.lwf_applicable,pay_billing_master_history.month_calc,pay_billing_master_history.ot_applicable,pay_billing_master_history.pt_applicable,pay_billing_master_history.allowances,pay_billing_master.basic,pay_billing_master_history.bonus_policy_aap_billing,pay_billing_master_history.bonus_amount_billing,pay_billing_master_history.bill_bonus_percent,pay_billing_master_history.bill_esic_percent,pay_billing_master_history.bill_ser_operations,pay_billing_master_history.bill_oper_cost_amt,pay_billing_master_history.bill_oper_cost_percent,pay_billing_master_history.bill_pf_percent,pay_billing_master_history.bill_relieving,pay_billing_master_history.bill_service_charge,pay_billing_master_history.bill_uniform_percent,pay_billing_master_history.bill_uniform_rate,(pay_billing_master.cca-pay_billing_master_history.cca),pay_billing_master_history.education,pay_billing_master_history.end_date,pay_billing_master_history.hra_amount,pay_billing_master_history.hra_percent,pay_billing_master_history.leave_days,pay_billing_master_history.leave_days_percent,pay_billing_master_history.other_allow,pay_billing_master_history.per_rate,pay_billing_master_history.policy_name1,pay_billing_master_history.sal_bonus,pay_billing_master_history.sal_bonus_amount,pay_billing_master_history.sal_esic,pay_billing_master_history.sal_pf,pay_billing_master_history.sal_uniform_percent,pay_billing_master_history.sal_uniform_rate,pay_billing_master_history.start_date,pay_billing_master_history.travelling,pay_billing_master.vda,pay_billing_master_history.washing,pay_billing_master.basic_vda,pay_billing_master_history.leave_taxable,pay_billing_master_history.gratuity_percent,pay_billing_master_history.gratuity_taxable,pay_billing_master_history.bonus_policy_salary,pay_billing_master_history.bonus_taxable_salary,pay_billing_master_history.leave_taxable_salary,pay_billing_master_history.gratuity_taxable_salary,pay_billing_master_history.gratuity_salary,pay_billing_master_history.allowances_salary,pay_billing_master_history.national_holidays_count,pay_billing_master_history.basic_salary,pay_billing_master_history.cca_salary,pay_billing_master_history.education_salary,pay_billing_master_history.per_rate_salary,pay_billing_master_history.travelling_salary,pay_billing_master_history.vda_salary,pay_billing_master_history.washing_salary,pay_billing_master_history.leave_days_salary,pay_billing_master_history.leave_days_percent_salary,pay_billing_master_history.start_date_common,pay_billing_master_history.end_date_common,pay_billing_master_history.ot_policy_salary,pay_billing_master_history.ot_amount_salary,pay_billing_master_history.ot_policy_billing,pay_billing_master_history.ot_amount_billing,pay_billing_master_history.hra_amount_salary,pay_billing_master_history.hra_percent_salary,pay_billing_master_history.relieving_uniform_billing,pay_billing_master_history.esic_oa_billing,pay_billing_master_history.esic_oa_salary,pay_billing_master_history.group_insurance_billing,pay_billing_master_history.service_group_insurance_billing,pay_billing_master_history.bill_service_charge_amount,pay_billing_master_history.esic_common_allow,'" + login_id + "',now()," + month + "," + year + ",'billing',pay_billing_master_history.material_contract,pay_billing_master_history.contract_type,pay_billing_master_history.contract_amount,pay_billing_master_history.handling_applicable,pay_billing_master_history.handling_percent,pay_billing_master_history.dc_contract,pay_billing_master_history.dc_type,pay_billing_master_history.dc_rate,pay_billing_master_history.dc_area,pay_billing_master_history.dc_handling_charge,pay_billing_master_history.dc_handling_percent,pay_billing_master_history.conveyance_applicable,pay_billing_master_history.conveyance_type,pay_billing_master_history.conveyance_rate,pay_billing_master_history.conveyance_km,pay_billing_master_history.conveyance_service_charge,pay_billing_master_history.conveyance_service_charge_per,pay_billing_master_history.conveyance_service_amount,pay_billing_master_history.equmental_applicable,pay_billing_master_history.equmental_unit,pay_billing_master_history.equmental_rental_rate,pay_billing_master_history.equmental_handling_applicable,pay_billing_master_history.chemical_applicable,pay_billing_master_history.chemical_unit,pay_billing_master_history.chemical_consumables_rate,pay_billing_master_history.chemical_handling_applicable,pay_billing_master_history.chemical_handling_percent,pay_billing_master_history.dustbin_applicable,pay_billing_master_history.dustbin_unit,pay_billing_master_history.dustbin_liners_rate,pay_billing_master_history.dustbin_handling_applicable,pay_billing_master_history.dustbin_handling_percent,pay_billing_master_history.femina_applicable,pay_billing_master_history.femina_unit,pay_billing_master_history.femina_hygiene_rate,pay_billing_master_history.femina_handling_applicable,pay_billing_master_history.femina_handling_percent,pay_billing_master_history.aerosol_applicable,pay_billing_master_history.aerosol_unit,pay_billing_master_history.aerosol_dispenser_rate,pay_billing_master_history.aerosol_handling_applicable,pay_billing_master_history.aerosol_handling_percent,pay_billing_master_history.tool_applicable,pay_billing_master_history.tool_unit,pay_billing_master_history.tool_tackles_rate,pay_billing_master_history.tool_handling_applicable,pay_billing_master_history.tool_handling_percent,pay_billing_master_history.equmental_handling_percent,pay_billing_master_history.pc_contract,pay_billing_master_history.pc_type,pay_billing_master_history.pc_rate,pay_billing_master_history.pc_area,pay_billing_master_history.pc_handling_charge,pay_billing_master_history.pc_handling_percent,pay_billing_master_history.bonus_applicable,pay_billing_master_history.leave_applicable,pay_billing_master_history.billing_gst_applicable,pay_billing_master_history.pf_cmn_on,pay_billing_master_history.lwf_act_mon,((pay_billing_master.basic+pay_billing_master.vda) - (pay_billing_master_history.basic+pay_billing_master_history.vda)) AS 'basic_vda',(pay_billing_master.basic-pay_billing_master_history.basic),(pay_billing_master.vda-pay_billing_master_history.vda) FROM pay_billing_master_history inner join pay_billing_master on pay_billing_master_history.comp_code = pay_billing_master.comp_code and pay_billing_master_history.billing_unit_code = pay_billing_master.billing_unit_code and pay_billing_master_history.designation = pay_billing_master.designation and pay_billing_master_history.hours = pay_billing_master.hours and type='billing' where " + history_where1);

            basic_vda_arr = "diff_basic as 'basic',diff_vda as 'vda'";

        }
        else
        {
            d.operation("insert into " + pay_billing_master_history + " (comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow,create_user,create_date,month,year,type,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,equmental_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on, lwf_act_mon, conveyance_amount,region) SELECT pay_billing_master.comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, pay_billing_master.designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow, '" + login_id + "', NOW(), " + month + "," + year + ", 'billing', material_contract, contract_type, contract_amount, handling_applicable, handling_percent, dc_contract, dc_type, dc_rate, dc_area, dc_handling_charge, dc_handling_percent, conveyance_applicable, conveyance_type, conveyance_rate, conveyance_km, conveyance_service_charge, conveyance_service_charge_per, conveyance_service_amount, equmental_applicable, equmental_unit, equmental_rental_rate, equmental_handling_applicable, chemical_applicable, chemical_unit, chemical_consumables_rate, chemical_handling_applicable, chemical_handling_percent, dustbin_applicable, dustbin_unit, dustbin_liners_rate, dustbin_handling_applicable, dustbin_handling_percent, femina_applicable, femina_unit, femina_hygiene_rate, femina_handling_applicable, femina_handling_percent, aerosol_applicable, aerosol_unit, aerosol_dispenser_rate, aerosol_handling_applicable, aerosol_handling_percent, tool_applicable, tool_unit, tool_tackles_rate, tool_handling_applicable, tool_handling_percent, equmental_handling_percent, pc_contract, pc_type, pc_rate, pc_area, pc_handling_charge, pc_handling_percent, bonus_applicable, leave_applicable, billing_gst_applicable, pf_cmn_on, lwf_act_mon, conveyance_amount, pay_unit_master.zone FROM pay_billing_master inner join pay_unit_master on pay_unit_master.comp_code = pay_billing_master.comp_code and pay_unit_master.unit_code = pay_billing_master.billing_unit_code WHERE " + history_where);


        }
        menu = new MySqlCommand("select per_rate," + basic_vda_arr + " ,'',washing,travelling,education ,allowances,cca ,other_allow,hra_amount,bill_bonus_percent,leave_days,bill_pf_percent,bill_esic_percent,bill_uniform_rate ,bill_oper_cost_amt,bill_relieving,bill_uniform_percent ,bill_service_charge,pay_billing_master_history.designation,hra_percent,month_calc,leave_days_percent,ot_applicable,esic_ot,bonus_taxable,lwf_applicable,leave_taxable,gratuity_taxable,bill_ser_uniform,bill_oper_cost_percent,basic_vda,hours,gratuity_percent,national_holidays_count,bonus_policy_aap_billing,bonus_amount_billing, pay_unit_master.state_name, pay_unit_master.unit_code,bill_ser_operations, pay_billing_master_history.designation, pay_billing_master_history.hours,pay_billing_master_history.ot_policy_billing,pay_billing_master_history.ot_amount_billing,pay_billing_master_history.relieving_uniform_billing,esic_oa_billing, group_insurance_billing, service_group_insurance_billing,bill_service_charge_amount,esic_common_allow,pf_cmn_on, lwf_act_mon FROM " + pay_billing_master_history + " AS pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE " + menu_where, d.con1);

        d.con1.Open();
        try
        {
            string count = d.getsinglestring("select month_calc  FROM " + pay_billing_master_history + " AS pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE " + menu_where + " limit 1");
            if (count == "")
            {
                count = "0";
            }
            int days = 0;
            if (counter == 2)
            {
                if (region.Equals("month"))
                {
                    days = CountDay(int.Parse(month), int.Parse(year), int.Parse(count), client_code, comp_code, from_to_date);
                }
                else if (region.Equals("policy"))
                {
                    days = CountDay(int.Parse(month), int.Parse(year), int.Parse(count), client_code, comp_code, 1);
                }
            }
            else if (counter == 1)
            {
                days = CountDay(int.Parse(month), int.Parse(year), int.Parse(count), client_code, comp_code, 1);
            }
            else
            {
                days = CountDay(int.Parse(month), int.Parse(year), int.Parse(d.getsinglestring("select month_calc  FROM " + pay_billing_master_history + " AS pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE " + menu_where + " limit 1")), client_code, comp_code, from_to_date);
            }
            MySqlDataReader dr = menu.ExecuteReader();
            while (dr.Read())
            {

                if (int.Parse(month) == 1 && int.Parse(year) == 2019 && client_code == "RLIC HK")
                {
                    days = 27;
                }

                double per_day_rate = double.Parse(dr.GetValue(0).ToString());

                // basic vda
                double basic = double.Parse(dr.GetValue(1).ToString());
                double vda = double.Parse(dr.GetValue(2).ToString());
                double basic_vda = basic + vda;

                //Bonus amount changes
                if (int.Parse(dr.GetValue(36).ToString()).Equals(1))
                {
                    if ((double.Parse(dr.GetValue(1).ToString()) + double.Parse(dr.GetValue(2).ToString())) < double.Parse(dr.GetValue(37).ToString()))
                    {
                        bonus = double.Parse(dr.GetValue(37).ToString());
                    }
                }

                //washing,travelling,education 
                washing = double.Parse(dr.GetValue(4).ToString());
                travelling = double.Parse(dr.GetValue(5).ToString());
                education = double.Parse(dr.GetValue(6).ToString());

                // other allowance billing(before or after gross)
                if (double.Parse(dr.GetValue(46).ToString()).Equals(1))
                {
                    //before gross
                    bill_OA_ESIC = double.Parse(dr.GetValue(7).ToString());
                }
                else
                {
                    //after gross
                    bill_OA = double.Parse(dr.GetValue(7).ToString());
                }

                // cca
                cca = double.Parse(dr.GetValue(8).ToString());

                //commmon allowance(billing+salary)
                if (double.Parse(dr.GetValue(50).ToString()).Equals(1))
                {
                    common_allowance = double.Parse(dr.GetValue(9).ToString());
                }
                else
                {
                    esic_common_allowance = double.Parse(dr.GetValue(9).ToString());
                }
                //Bonus Taxable Yes/No
                if (dr.GetValue(26).ToString().Equals("1"))
                {
                    //before gross
                    if (bonus > basic_vda)
                    {
                        bonus_taxable = (bonus * double.Parse(dr.GetValue(11).ToString())) / 100;
                    }
                    else
                    {
                        bonus_taxable = (basic_vda * double.Parse(dr.GetValue(11).ToString())) / 100;
                    }

                }
                else if (dr.GetValue(26).ToString().Equals("0"))
                {
                    //after gross
                    if (bonus > basic_vda)
                    {
                        bonus_non_taxable = (bonus * double.Parse(dr.GetValue(11).ToString())) / 100;
                    }
                    else
                    {
                        bonus_non_taxable = (basic_vda * double.Parse(dr.GetValue(11).ToString())) / 100;
                    }
                }
                else
                {
                    //Not Applicable
                    bonus_taxable = 0;
                    bonus_non_taxable = 0;
                }


                //Leaves amount/percent
                if (dr.GetValue(12).ToString().Equals("0") && dr.GetValue(23).ToString().Equals("0"))
                {
                    leave_taxable = 0;
                    leave_non_taxable = 0;
                }
                //else if (!dr.GetValue(12).ToString().Equals("0"))
                //{

                //    leave_taxable = (basic_vda * 6.731) / 100;
                //    leave_non_taxable = (basic_vda * 6.731) / 100;
                //}
                else if (!dr.GetValue(12).ToString().Equals("0"))
                {

                    leave_taxable = ((basic_vda / 26) * double.Parse(dr.GetValue(12).ToString())) / 12;
                    leave_non_taxable = ((basic_vda / 26) * double.Parse(dr.GetValue(12).ToString())) / 12;
                }
                else
                {
                    leave_taxable = 0;
                    leave_non_taxable = 0;
                    if (!dr.GetValue(23).ToString().Equals("0"))
                    {
                        leave_taxable = (basic_vda * double.Parse(dr.GetValue(23).ToString())) / 100;
                        leave_non_taxable = (basic_vda * double.Parse(dr.GetValue(23).ToString())) / 100;
                    }
                }
                //Leaves taxable Yes/No
                if (dr.GetValue(28).ToString().Equals("1"))
                {
                    leave_non_taxable = 0;
                }
                else if (dr.GetValue(28).ToString().Equals("0"))
                {
                    leave_taxable = 0;
                }
                else
                {
                    leave_taxable = 0;
                    leave_non_taxable = 0;
                }

                //gratuity TAXABLE YES/NO ---
                if (double.Parse(dr.GetValue(29).ToString()).Equals(1))
                {
                    gratuity_taxable = (basic_vda * double.Parse(dr.GetValue(34).ToString())) / 100;

                }
                else if (double.Parse(dr.GetValue(29).ToString()).Equals(0))
                {
                    gratuity_non_taxable = (basic_vda * double.Parse(dr.GetValue(34).ToString())) / 100;
                }
                else
                {
                    gratuity_taxable = 0;
                    gratuity_non_taxable = 0;
                }

                //HRA
                if (double.Parse(dr.GetValue(10).ToString()).Equals(0))
                {
                    hra_amount = (basic_vda * double.Parse(dr.GetValue(21).ToString())) / 100;
                }
                else
                {
                    hra_amount = double.Parse(dr.GetValue(10).ToString());
                    // START for arrier bill vinod 27/03/2020
                    if (counter == 2)
                    {
                        hra_amount = 0;
                    }
                    // END for arrier bill vinod 27/03/2020
                }

                // Special Allowance
                if (dr.GetValue(43).ToString().Equals("1"))
                {
                    special_allowance = double.Parse(dr.GetValue(44).ToString());
                }
                else { special_allowance = 0; }

                // for arrier bill
                if (counter == 2)
                {
                    bonus_taxable = (basic_vda * double.Parse(dr.GetValue(11).ToString())) / 100;
                    bonus = 0;
                    special_allowance = 0;
                    esic_common_allowance = 0;
                    common_allowance = 0;
                    cca = 0;
                }

                // Gross
                gross = basic_vda + washing + travelling + education + common_allowance + bill_OA_ESIC + cca + bonus_taxable + leave_taxable + gratuity_taxable + hra_amount + special_allowance;

                // National Holiday
                national_holiday = ((basic_vda / 26) * double.Parse(dr.GetValue(35).ToString())) / 12;

                //PF
                if (dr.GetValue(51).ToString().Equals("1"))
                {
                    pf = (gross * double.Parse(dr.GetValue(13).ToString())) / 100;
                }
                else if (dr.GetValue(51).ToString().Equals("2"))
                {
                    pf = ((gross - hra_amount) * double.Parse(dr.GetValue(13).ToString())) / 100;
                }
                else if (dr.GetValue(51).ToString().Equals("3"))
                {
                    pf = ((basic_vda + common_allowance + cca) * double.Parse(dr.GetValue(13).ToString())) / 100;
                }
                else
                {
                    pf = (basic_vda * double.Parse(dr.GetValue(13).ToString())) / 100;
                }
                //ESIC
                esic = (gross * double.Parse(dr.GetValue(14).ToString())) / 100;

                //Uniform
                if (double.Parse(dr.GetValue(15).ToString()).Equals(0))
                {
                    uniform_ser = (basic_vda * double.Parse(dr.GetValue(18).ToString())) / 100;
                    uniform_no_ser = (basic_vda * double.Parse(dr.GetValue(18).ToString())) / 100;
                    uniform_relieving = (basic_vda * double.Parse(dr.GetValue(18).ToString())) / 100;
                }
                else
                {
                    uniform_ser = double.Parse(dr.GetValue(15).ToString());
                    uniform_no_ser = double.Parse(dr.GetValue(15).ToString());
                    uniform_relieving = double.Parse(dr.GetValue(15).ToString());
                    if (counter == 2)//for arrears bill
                    {
                        uniform_ser = 0; uniform_no_ser = 0; uniform_relieving = 0;
                    }
                }

                // uniform service charge & relieving charge
                if (double.Parse(dr.GetValue(30).ToString()).Equals(1))
                {
                    // relieving charge
                    if (double.Parse(dr.GetValue(45).ToString()).Equals(1))
                    {
                        uniform_relieving = 0;
                    }
                    else
                    {
                        uniform_ser = 0;
                    }
                    uniform_no_ser = 0;
                }
                else
                {
                    uniform_ser = 0;
                    uniform_relieving = 0;
                }

                // lwf 
                if (double.Parse(dr.GetValue(27).ToString()).Equals(1))
                {
                    string temp = "";
                    //if (state == "ALL")
                    //{
                    //    temp = d.getsinglestring("SELECT per_month FROM pay_master_lwf WHERE state_name in (select state_name from pay_unit_master where client_code = '" + client_code + "' and comp_code = '" + comp_code + "')");
                    //}
                    if (unit_code == "ALL" || state == "ALL")
                    {
                        if (dr.GetValue(52).ToString().Equals("1"))
                        {
                            temp = d.getsinglestring("SELECT employer_contribution FROM pay_master_lwf WHERE state_name = '" + dr.GetValue(38).ToString() + "' and month like '%" + int.Parse(month) + ",%'");
                        }
                        else
                        {
                            temp = d.getsinglestring("SELECT per_month FROM pay_master_lwf WHERE state_name = '" + dr.GetValue(38).ToString() + "'");
                        }
                    }
                    else
                    {
                        if (dr.GetValue(52).ToString().Equals("1"))
                        {
                            temp = d.getsinglestring("SELECT employer_contribution FROM pay_master_lwf WHERE month like '%" + int.Parse(month) + ",%' and state_name in (select state_name from pay_unit_master where unit_code = '" + unit_code + "' and comp_code = '" + comp_code + "')");
                        }
                        else
                        {
                            temp = d.getsinglestring("SELECT per_month FROM pay_master_lwf WHERE state_name in (select state_name from pay_unit_master where unit_code = '" + unit_code + "' and comp_code = '" + comp_code + "')");
                        }
                    }
                    if (temp != "")
                    {
                        lwf = double.Parse(temp);
                    }
                    else
                    {
                        lwf = 0;
                    }
                }
                else { lwf = 0; }

                // Operational Cost amount/percent
                if (double.Parse(dr.GetValue(16).ToString()).Equals(0))
                {
                    //Operation cost service charge
                    if (dr.GetValue(40).ToString().Equals("1"))
                    {
                        operation_cost_ser = (gross * double.Parse(dr.GetValue(31).ToString())) / 100;
                    }
                    else { operation_cost = (gross * double.Parse(dr.GetValue(31).ToString())) / 100; }
                }
                else
                {
                    if (dr.GetValue(40).ToString().Equals("1"))
                    {
                        operation_cost_ser = double.Parse(dr.GetValue(16).ToString());
                    }
                    else { operation_cost = double.Parse(dr.GetValue(16).ToString()); }
                    if (counter == 2)
                    {
                        operation_cost = 0;
                    }
                }
                //arrear
                if (counter == 2)
                {
                    lwf = 0;
                }

                // double uniform_service_reliev = uniform_ser == 0 ? uniform_relieving : uniform_ser;
                //Sub Total A
                sub_total_A = gross + bonus_non_taxable + leave_non_taxable + gratuity_non_taxable + national_holiday + pf + esic + lwf + uniform_ser + operation_cost_ser + bill_OA + esic_common_allowance;

                //OT
                //change by MD for from to date
                if (start_date == 0 || start_date == 1)
                {
                    if (double.Parse(dr.GetValue(24).ToString()).Equals(1))
                    {
                        double duty_hrs = double.Parse(dr.GetValue(33).ToString());
                        if (!duty_hrs.Equals(0))
                        {
                            ot_rate = (basic_vda / days / duty_hrs) * 1 * 2;

                        }

                    }
                    else
                    {
                        ot_rate = 0;
                    }
                }
                else { ot_rate = 0; }

                //ESIC on OT
                if (double.Parse(dr.GetValue(25).ToString()).Equals(1))
                {
                    ot_esic = (ot_rate * double.Parse(dr.GetValue(14).ToString())) / 100;
                }

                //suraj 30/3/2020 from date to date ot zero
                if (counter == 1)
                {
                    string ot_temp = "";
                    ot_temp = d.getsinglestring("SELECT invoice_flag FROM pay_billing_unit_rate_history WHERE " + where + " and invoice_flag in (1,2) ");
                    if (ot_temp == "1" || ot_temp == "2")
                    {
                        ot_rate = 0; ot_esic = 0; //vinod issues is here
                    }
                }
                // sub total B
                sub_total_B = ot_rate + ot_esic;

                sub_total_AB = sub_total_A + sub_total_B;

                //Relieving Charge
                relieving_charge = (sub_total_AB * double.Parse(dr.GetValue(17).ToString())) / 100;

                //group insurance
                if (dr.GetValue(48).ToString().Equals("1"))
                {
                    //service charge
                    group_insurance_ser = double.Parse(dr.GetValue(47).ToString());
                }
                else
                {
                    group_insurance = double.Parse(dr.GetValue(47).ToString());
                }

                //arrear
                if (counter == 2)
                {
                    group_insurance = 0;
                }

                // Sub Total C
                sub_total_C = sub_total_AB + relieving_charge + uniform_relieving + group_insurance_ser;

                //Service Charge

                if (double.Parse(dr.GetValue(49).ToString()).Equals(0))
                {
                    service_charge = (sub_total_C * double.Parse(dr.GetValue(19).ToString())) / 100;
                }
                else
                {
                    service_charge = double.Parse(dr.GetValue(49).ToString());
                    if (counter == 2)
                    {
                        service_charge = 0;
                    }
                }

                //Total
                total = service_charge + uniform_no_ser + operation_cost + group_insurance;

                // Grand Total
                grand_total = total + sub_total_C;

                if (state.ToString() == "ALL")
                {
                    d.operation("insert into " + pay_billing_unit_rate + " (comp_code,client_code,unit_code,month,year,month_days,designation,hours, per_day_rate,basic,vda,basicvda,bonus_rate,washing,traveling,education,allowances,cca,otherallowance,bonus_amount,leave_amount,grauity_amount,hra,gross,national_holiday_amount,pf_amount,esic_amount,uniform,lwf,operational_cost,sub_total_a,ot_1_hr_amount,esi_on_ot_amount,sub_total_amount_b,sub_total_ab,relieving_amount,sub_total_c,service_charges,total,grand_total,create_user,create_date,ot_amount,common_allowance) values ('" + comp_code + "','" + client_code + "','" + dr.GetValue(39).ToString() + "'," + month + "," + year + "," + days + ",'" + dr.GetValue(41).ToString() + "','" + dr.GetValue(42).ToString() + "'," + Math.Round(per_day_rate, 2) + ", " + Math.Round(basic, 2) + ", " + Math.Round(vda, 2) + ", " + Math.Round(basic_vda, 2) + ", " + Math.Round(bonus, 2) + ", " + Math.Round(washing, 2) + ", " + Math.Round(travelling, 2) + ", " + Math.Round(education, 2) + ", " + Math.Round(bill_OA_ESIC == 0 ? bill_OA : bill_OA_ESIC, 2) + ", " + Math.Round(cca, 2) + ", " + Math.Round(common_allowance, 2) + ", " + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + ", " + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + ", " + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + ", " + Math.Round(hra_amount, 2) + ", " + Math.Round(gross, 2) + "," + Math.Round(national_holiday, 2) + ", " + Math.Round(pf, 2) + ", " + Math.Round(esic, 2) + ", " + Math.Round(uniform_ser == 0 ? uniform_relieving == 0 ? uniform_no_ser : uniform_relieving : uniform_ser, 2) + ", " + Math.Round(lwf, 2) + ", " + Math.Round(operation_cost_ser == 0 ? operation_cost : operation_cost_ser, 2) + ", " + Math.Round(sub_total_A, 2) + ", " + Math.Round(ot_rate, 2) + ", " + Math.Round(ot_esic, 2) + ", " + Math.Round(sub_total_B, 2) + ", " + Math.Round(sub_total_AB, 2) + ", " + Math.Round(relieving_charge, 2) + ", " + Math.Round(sub_total_C, 2) + "," + Math.Round(service_charge, 2) + ", " + Math.Round(total, 2) + ", " + Math.Round(grand_total, 2) + ",'" + login_id + "',now(), " + Math.Round(special_allowance, 2) + "," + Math.Round(esic_common_allowance, 2) + ")");
                }
                else if (unit_code.ToString() == "ALL")
                {
                    d.operation("insert into " + pay_billing_unit_rate + " (comp_code,client_code,unit_code,month,year,month_days,designation,hours, per_day_rate,basic,vda,basicvda,bonus_rate,washing,traveling,education,allowances,cca,otherallowance,bonus_amount,leave_amount,grauity_amount,hra,gross,national_holiday_amount,pf_amount,esic_amount,uniform,lwf,operational_cost,sub_total_a,ot_1_hr_amount,esi_on_ot_amount,sub_total_amount_b,sub_total_ab,relieving_amount,sub_total_c,service_charges,total,grand_total,create_user,create_date,ot_amount,common_allowance) values ('" + comp_code + "','" + client_code + "','" + dr.GetValue(39).ToString() + "'," + month + "," + year + "," + days + ",'" + dr.GetValue(41).ToString() + "','" + dr.GetValue(42).ToString() + "'," + Math.Round(per_day_rate, 2) + ", " + Math.Round(basic, 2) + ", " + Math.Round(vda, 2) + ", " + Math.Round(basic_vda, 2) + ", " + Math.Round(bonus, 2) + ", " + Math.Round(washing, 2) + ", " + Math.Round(travelling, 2) + ", " + Math.Round(education, 2) + ", " + Math.Round(bill_OA_ESIC == 0 ? bill_OA : bill_OA_ESIC, 2) + ", " + Math.Round(cca, 2) + ", " + Math.Round(common_allowance, 2) + ", " + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + ", " + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + ", " + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + ", " + Math.Round(hra_amount, 2) + ", " + Math.Round(gross, 2) + "," + Math.Round(national_holiday, 2) + ", " + Math.Round(pf, 2) + ", " + Math.Round(esic, 2) + ", " + Math.Round(uniform_ser == 0 ? uniform_relieving == 0 ? uniform_no_ser : uniform_relieving : uniform_ser, 2) + ", " + Math.Round(lwf, 2) + ", " + Math.Round(operation_cost_ser == 0 ? operation_cost : operation_cost_ser, 2) + ", " + Math.Round(sub_total_A, 2) + ", " + Math.Round(ot_rate, 2) + ", " + Math.Round(ot_esic, 2) + ", " + Math.Round(sub_total_B, 2) + ", " + Math.Round(sub_total_AB, 2) + ", " + Math.Round(relieving_charge, 2) + ", " + Math.Round(sub_total_C, 2) + "," + Math.Round(service_charge, 2) + ", " + Math.Round(total, 2) + ", " + Math.Round(grand_total, 2) + ",'" + login_id + "',now(), " + Math.Round(special_allowance, 2) + "," + Math.Round(esic_common_allowance, 2) + ")");
                }
                else
                {
                    d.operation("insert into " + pay_billing_unit_rate + " (comp_code,client_code,unit_code,month,year,month_days,designation,hours,per_day_rate,basic,vda,basicvda,bonus_rate,washing,traveling,education,allowances,cca,otherallowance,bonus_amount,leave_amount,grauity_amount,hra,gross,national_holiday_amount,pf_amount,esic_amount,uniform,lwf,operational_cost,sub_total_a,ot_1_hr_amount,esi_on_ot_amount,sub_total_amount_b,sub_total_ab,relieving_amount,sub_total_c,service_charges,total,grand_total,create_user,create_date,ot_amount,common_allowance) values ('" + comp_code + "','" + client_code + "','" + unit_code + "'," + month + "," + year + "," + days + ",'" + dr.GetValue(41).ToString() + "','" + dr.GetValue(42).ToString() + "'," + Math.Round(per_day_rate, 2) + ", " + Math.Round(basic, 2) + ", " + Math.Round(vda, 2) + ", " + Math.Round(basic_vda, 2) + ", " + Math.Round(bonus, 2) + ", " + Math.Round(washing, 2) + ", " + Math.Round(travelling, 2) + ", " + Math.Round(education, 2) + ", " + Math.Round(bill_OA_ESIC == 0 ? bill_OA : bill_OA_ESIC, 2) + ", " + Math.Round(cca, 2) + ", " + Math.Round(common_allowance, 2) + ", " + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + ", " + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + ", " + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + ", " + Math.Round(hra_amount, 2) + ", " + Math.Round(gross, 2) + "," + Math.Round(national_holiday, 2) + ", " + Math.Round(pf, 2) + ", " + Math.Round(esic, 2) + ", " + Math.Round(uniform_ser == 0 ? uniform_relieving == 0 ? uniform_no_ser : uniform_relieving : uniform_ser, 2) + ", " + Math.Round(lwf, 2) + ", " + Math.Round(operation_cost_ser == 0 ? operation_cost : operation_cost_ser, 2) + ", " + Math.Round(sub_total_A, 2) + ", " + Math.Round(ot_rate, 2) + ", " + Math.Round(ot_esic, 2) + ", " + Math.Round(sub_total_B, 2) + ", " + Math.Round(sub_total_AB, 2) + ", " + Math.Round(relieving_charge, 2) + ", " + Math.Round(sub_total_C, 2) + "," + Math.Round(service_charge, 2) + ", " + Math.Round(total, 2) + ", " + Math.Round(grand_total, 2) + ",'" + login_id + "',now(), " + Math.Round(special_allowance, 2) + "," + Math.Round(esic_common_allowance, 2) + ")");
                }
                bonus = 0; washing = 0; travelling = 0; education = 0; bill_OA = 0; bill_OA_ESIC = 0; cca = 0; common_allowance = 0; bonus_taxable = 0; bonus_non_taxable = 0; leave_taxable = 0; leave_non_taxable = 0; gratuity_taxable = 0; gratuity_non_taxable = 0; hra_amount = 0; special_allowance = 0; gross = 0; national_holiday = 0; pf = 0; esic = 0; uniform_ser = 0; uniform_no_ser = 0; uniform_relieving = 0; lwf = 0; operation_cost = 0; operation_cost_ser = 0; sub_total_A = 0; sub_total_B = 0; sub_total_C = 0; sub_total_AB = 0; total = 0; grand_total = 0; group_insurance_ser = 0; group_insurance = 0; ot_rate = 0; ot_esic = 0; relieving_charge = 0; service_charge = 0; esic_common_allowance = 0;
                basic = 0;
                vda = 0;
                basic_vda = 0;
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex) { }
        finally
        {
            menu.Dispose();
            d.con1.Close();
        }


    }
    public void Salary(string comp_code, string client_code, string state, string unit_code, string month_year, string login_id, int from_to_date,string from_date,string to_date)
    {
        string menu_where = "";
        string history_where = "";
        string history_where1 = "";
        double hours = 0, month_calc = 0, per_day_rate = 0, basic = 0, vda = 0, bonus = 0, washing = 0, travelling = 0, education = 0, salary_OA = 0, salary_OA_ESIC = 0, cca = 0, common_allowance = 0, bonus_taxable = 0, bonus_non_taxable = 0, leave_taxable = 0, leave_non_taxable = 0, gratuity_taxable = 0, gratuity_non_taxable = 0, hra_amount = 0, special_allowance = 0, gross = 0, national_holiday = 0, pf = 0, esic = 0, ot_rate = 0, ot_esic = 0, pt = 0, lwf = 0, uniform = 0, advance = 0, fine = 0, total_deduction = 0, take_home_salary = 0, esic_common_allowance = 0, esic_ot_percent = 0;

        string pay_salary_unit_rate = "pay_salary_unit_rate";
        string pay_billing_master_history = "pay_billing_master_history";

        if (from_to_date == 1)
        {
            pay_salary_unit_rate = "pay_salary_from_to_unit_rate";
            pay_billing_master_history = "pay_billing_from_to_history";
        }
        else if (from_to_date == 2 || from_to_date == 3)//arrears
        {
            pay_salary_unit_rate = "pay_salary_unit_rate_arrears";
            pay_billing_master_history = "pay_billing_master_history_arrears";
        }
        string months = month_year.Substring(0, 2);
        string years = month_year.Substring(3);

        MySqlCommand menu;

        if (state == "ALL")
        {
            d.operation("delete from " + pay_salary_unit_rate + "  where month='" + months + "' and Year = '" + years + "' and client_code = '" + client_code + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + comp_code + "' and client_code = '" + client_code + "')");
            d.operation("delete from " + pay_billing_master_history + "  where month='" + months + "' and Year = '" + years + "' and billing_client_code = '" + client_code + "' and type='salary'");

            history_where = " billing_client_code = '" + client_code + "' and comp_code='" + comp_code + "' ";
            history_where1 = " pay_billing_master_history.billing_client_code='" + client_code + "' and pay_billing_master_history.comp_code = '" + comp_code + "' and pay_billing_master_history.month='" + months + "' and pay_billing_master_history.year= '" + years + "' and type='salary'  having basic_salary !='0' or vda_salary !=0";
            // comment 30/09 menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and client_code = '" + client_code + "' and month_end=0 and pay_unit_master.branch_Status=0 and type='salary'";
            menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and client_code = '" + client_code + "' and month_end=0 AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + month_year + "', '%d/%m/%Y')) and type='salary'";
        }
        else if (unit_code == "ALL")
        {
            d.operation("delete from " + pay_salary_unit_rate + " where month='" + months + "' and Year = '" + years + "' and client_code = '" + client_code + "' and comp_code='" + comp_code + "' and unit_code in (select unit_code from pay_unit_master where comp_code='" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state + "')");
            d.operation("delete from " + pay_billing_master_history + " where month='" + months + "' and Year = '" + years + "' and billing_client_code = '" + client_code + "' and comp_code='" + comp_code + "' and billing_state = '" + state + "' and type='salary'");

            history_where = " billing_client_code = '" + client_code + "' and billing_state='" + state + "' and comp_code='" + comp_code + "'";
            history_where1 = " pay_billing_master_history.billing_client_code='" + client_code + "' and pay_billing_master_history.billing_state ='" + state + "' and pay_billing_master_history.comp_code = '" + comp_code + "' and pay_billing_master_history.month='" + months + "' and pay_billing_master_history.year= '" + years + "' and type='salary' having basic_salary !='0' or vda_salary !=0";
            //comment 30/09   menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and client_code = '" + client_code + "' and pay_unit_master.STATE_NAME = '" + state + "' and month_end=0 and pay_unit_master.branch_Status=0 and type='salary'";
            menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and client_code = '" + client_code + "' and pay_unit_master.STATE_NAME = '" + state + "' and month_end=0 AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + month_year + "', '%d/%m/%Y')) and type='salary'";
        }
        else
        {
            d.operation("delete from " + pay_salary_unit_rate + " where month='" + months + "' and Year = '" + years + "' and client_code = '" + client_code + "' and unit_code = '" + unit_code + "' and comp_code='" + comp_code + "'");
            d.operation("delete from " + pay_billing_master_history + " where month='" + months + "' and Year = '" + years + "' and billing_client_code = '" + client_code + "' and billing_unit_code = '" + unit_code + "' and comp_code='" + comp_code + "'  and type='salary'");

            history_where = " billing_unit_code ='" + unit_code + "' and comp_code='" + comp_code + "'";
            history_where1 = " pay_billing_master_history.billing_unit_code ='" + unit_code + "' and pay_billing_master_history.comp_code='" + comp_code + "' and pay_billing_master_history.month='" + months + "' and pay_billing_master_history.year= '" + years + "' and type='salary' having basic_salary !='0' or vda_salary !=0";
            //comment 30/09 menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and pay_unit_master.unit_code  ='" + unit_code + "' and month_end=0 and pay_unit_master.branch_Status=0 and type='salary'";
            menu_where = " month='" + months + "' and Year = '" + years + "' and pay_unit_master.comp_code ='" + comp_code + "' and pay_unit_master.unit_code  ='" + unit_code + "' and month_end=0 AND (branch_close_date is null || branch_close_date = '' ||branch_close_date  >= STR_TO_DATE('01/" + month_year + "', '%d/%m/%Y')) and type='salary'";
        }

        if (from_to_date == 2 || from_to_date == 3)
        {
            d.operation("insert into " + pay_billing_master_history + " (comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow,create_user,create_date,month,year,type,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,equmental_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on,lwf_act_mon,conveyance_amount) SELECT pay_billing_master_history.comp_code, pay_billing_master_history.billing_client_code, pay_billing_master_history.billing_state, pay_billing_master_history.billing_unit_code, pay_billing_master_history.bill_ser_uniform, pay_billing_master_history.bonus_taxable, pay_billing_master_history.designation, pay_billing_master_history.esic_ot, pay_billing_master_history.hours, pay_billing_master_history.lwf_applicable, pay_billing_master_history.month_calc, pay_billing_master_history.ot_applicable, pay_billing_master_history.pt_applicable, pay_billing_master_history.allowances, pay_billing_master_history.basic, pay_billing_master_history.bonus_policy_aap_billing, pay_billing_master_history.bonus_amount_billing, pay_billing_master_history.bill_bonus_percent, pay_billing_master_history.bill_esic_percent, pay_billing_master_history.bill_ser_operations, pay_billing_master_history.bill_oper_cost_amt, pay_billing_master_history.bill_oper_cost_percent, pay_billing_master_history.bill_pf_percent, pay_billing_master_history.bill_relieving, pay_billing_master_history.bill_service_charge, pay_billing_master_history.bill_uniform_percent, pay_billing_master_history.bill_uniform_rate, pay_billing_master_history.cca, pay_billing_master_history.education, pay_billing_master_history.end_date, pay_billing_master_history.hra_amount, pay_billing_master_history.hra_percent, pay_billing_master_history.leave_days, pay_billing_master_history.leave_days_percent, pay_billing_master_history.other_allow, pay_billing_master_history.per_rate, pay_billing_master_history.policy_name1, pay_billing_master_history.sal_bonus, pay_billing_master_history.sal_bonus_amount, pay_billing_master_history.sal_esic, pay_billing_master_history.sal_pf, pay_billing_master_history.sal_uniform_percent, pay_billing_master_history.sal_uniform_rate, pay_billing_master_history.start_date, pay_billing_master_history.travelling, pay_billing_master_history.vda, pay_billing_master_history.washing, pay_billing_master_history.basic_vda, pay_billing_master_history.leave_taxable, pay_billing_master_history.gratuity_percent, pay_billing_master_history.gratuity_taxable, pay_billing_master_history.bonus_policy_salary, pay_billing_master_history.bonus_taxable_salary, pay_billing_master_history.leave_taxable_salary, pay_billing_master_history.gratuity_taxable_salary, pay_billing_master_history.gratuity_salary, pay_billing_master_history.allowances_salary, pay_billing_master_history.national_holidays_count, (`pay_billing_master`.`basic_salary` - `pay_billing_master_history`.`basic_salary`) as basic_salary, pay_billing_master_history.cca_salary, pay_billing_master_history.education_salary, pay_billing_master_history.per_rate_salary, pay_billing_master_history.travelling_salary, (`pay_billing_master`.`vda_salary` - `pay_billing_master_history`.`vda_salary`) as vda_salary, pay_billing_master_history.washing_salary, pay_billing_master_history.leave_days_salary, pay_billing_master_history.leave_days_percent_salary, pay_billing_master_history.start_date_common, pay_billing_master_history.end_date_common, pay_billing_master_history.ot_policy_salary, pay_billing_master_history.ot_amount_salary, pay_billing_master_history.ot_policy_billing, pay_billing_master_history.ot_amount_billing, pay_billing_master_history.hra_amount_salary, pay_billing_master_history.hra_percent_salary, pay_billing_master_history.relieving_uniform_billing, pay_billing_master_history.esic_oa_billing, pay_billing_master_history.esic_oa_salary, pay_billing_master_history.group_insurance_billing, pay_billing_master_history.service_group_insurance_billing, pay_billing_master_history.bill_service_charge_amount, pay_billing_master_history.esic_common_allow,'" + login_id + "',NOW()," + month_year.Substring(0, 2) + "," + month_year.Substring(3) + ",'salary', pay_billing_master_history.material_contract, pay_billing_master_history.contract_type, pay_billing_master_history.contract_amount, pay_billing_master_history.handling_applicable, pay_billing_master_history.handling_percent, pay_billing_master_history.dc_contract, pay_billing_master_history.dc_type, pay_billing_master_history.dc_rate, pay_billing_master_history.dc_area, pay_billing_master_history.dc_handling_charge, pay_billing_master_history.dc_handling_percent, pay_billing_master_history.conveyance_applicable, pay_billing_master_history.conveyance_type, pay_billing_master_history.conveyance_rate, pay_billing_master_history.conveyance_km, pay_billing_master_history.conveyance_service_charge, pay_billing_master_history.conveyance_service_charge_per, pay_billing_master_history.conveyance_service_amount, pay_billing_master_history.equmental_applicable, pay_billing_master_history.equmental_unit, pay_billing_master_history.equmental_rental_rate, pay_billing_master_history.equmental_handling_applicable, pay_billing_master_history.chemical_applicable, pay_billing_master_history.chemical_unit, pay_billing_master_history.chemical_consumables_rate, pay_billing_master_history.chemical_handling_applicable, pay_billing_master_history.chemical_handling_percent, pay_billing_master_history.dustbin_applicable, pay_billing_master_history.dustbin_unit, pay_billing_master_history.dustbin_liners_rate, pay_billing_master_history.dustbin_handling_applicable, pay_billing_master_history.dustbin_handling_percent, pay_billing_master_history.femina_applicable, pay_billing_master_history.femina_unit, pay_billing_master_history.femina_hygiene_rate, pay_billing_master_history.femina_handling_applicable, pay_billing_master_history.femina_handling_percent, pay_billing_master_history.aerosol_applicable, pay_billing_master_history.aerosol_unit, pay_billing_master_history.aerosol_dispenser_rate, pay_billing_master_history.aerosol_handling_applicable, pay_billing_master_history.aerosol_handling_percent, pay_billing_master_history.tool_applicable, pay_billing_master_history.tool_unit, pay_billing_master_history.tool_tackles_rate, pay_billing_master_history.tool_handling_applicable, pay_billing_master_history.tool_handling_percent, pay_billing_master_history.equmental_handling_percent, pay_billing_master_history.pc_contract, pay_billing_master_history.pc_type, pay_billing_master_history.pc_rate, pay_billing_master_history.pc_area, pay_billing_master_history.pc_handling_charge, pay_billing_master_history.pc_handling_percent, pay_billing_master_history.bonus_applicable, pay_billing_master_history.leave_applicable, pay_billing_master_history.billing_gst_applicable, pay_billing_master_history.pf_cmn_on, pay_billing_master_history.lwf_act_mon, pay_billing_master_history.conveyance_amount FROM pay_billing_master_history INNER JOIN pay_billing_master ON pay_billing_master_history.comp_code = pay_billing_master.comp_code AND pay_billing_master_history.billing_unit_code = pay_billing_master.billing_unit_code AND pay_billing_master_history.designation = pay_billing_master.designation AND pay_billing_master_history.hours = pay_billing_master.hours where " + history_where1);
        }
        else
        {
            d.operation("insert into " + pay_billing_master_history + " (comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow,create_user,create_date,month,year,type,material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,equmental_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on,lwf_act_mon,conveyance_amount) select comp_code, billing_client_code, billing_state, billing_unit_code, bill_ser_uniform, bonus_taxable, designation, esic_ot, hours, lwf_applicable, month_calc, ot_applicable, pt_applicable, allowances, basic, bonus_policy_aap_billing, bonus_amount_billing, bill_bonus_percent, bill_esic_percent, bill_ser_operations, bill_oper_cost_amt, bill_oper_cost_percent, bill_pf_percent, bill_relieving, bill_service_charge, bill_uniform_percent, bill_uniform_rate, cca, education, end_date, hra_amount, hra_percent, leave_days, leave_days_percent, other_allow, per_rate, policy_name1, sal_bonus, sal_bonus_amount, sal_esic, sal_pf, sal_uniform_percent, sal_uniform_rate, start_date, travelling, vda, washing, basic_vda, leave_taxable, gratuity_percent, gratuity_taxable, bonus_policy_salary, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, gratuity_salary, allowances_salary, national_holidays_count, basic_salary, cca_salary, education_salary, per_rate_salary, travelling_salary, vda_salary, washing_salary, leave_days_salary, leave_days_percent_salary, start_date_common, end_date_common, ot_policy_salary, ot_amount_salary, ot_policy_billing, ot_amount_billing, hra_amount_salary, hra_percent_salary, relieving_uniform_billing, esic_oa_billing, esic_oa_salary, group_insurance_billing, service_group_insurance_billing, bill_service_charge_amount, esic_common_allow,'" + login_id + "',now()," + month_year.Substring(0, 2) + "," + month_year.Substring(3) + ",'salary',material_contract ,contract_type ,contract_amount ,handling_applicable,handling_percent,dc_contract,dc_type,dc_rate,dc_area,dc_handling_charge,dc_handling_percent,conveyance_applicable,conveyance_type,conveyance_rate,conveyance_km,conveyance_service_charge,conveyance_service_charge_per,conveyance_service_amount,equmental_applicable,equmental_unit,equmental_rental_rate,equmental_handling_applicable,chemical_applicable,chemical_unit,chemical_consumables_rate,chemical_handling_applicable,chemical_handling_percent,dustbin_applicable,dustbin_unit,dustbin_liners_rate,dustbin_handling_applicable,dustbin_handling_percent,femina_applicable,femina_unit,femina_hygiene_rate,femina_handling_applicable,femina_handling_percent,aerosol_applicable,aerosol_unit,aerosol_dispenser_rate,aerosol_handling_applicable,aerosol_handling_percent,tool_applicable,tool_unit,tool_tackles_rate,tool_handling_applicable,tool_handling_percent,equmental_handling_percent,pc_contract,pc_type,pc_rate,pc_area,pc_handling_charge,pc_handling_percent,bonus_applicable,leave_applicable,billing_gst_applicable,pf_cmn_on,lwf_act_mon,conveyance_amount from pay_billing_master where " + history_where);
        }
        menu = new MySqlCommand("SELECT pay_billing_master_history. designation, hours, month_calc, per_rate_salary, basic_salary, vda_salary, bonus_policy_salary, sal_bonus, sal_bonus_amount, washing_salary, travelling_salary, education_salary, allowances_salary, cca_salary, other_allow,  hra_amount_salary, hra_percent_salary, leave_days_salary, leave_days_percent_salary, sal_pf, sal_esic, sal_uniform_percent, sal_uniform_rate, bonus_taxable_salary, leave_taxable_salary, gratuity_taxable_salary, lwf_applicable, pt_applicable , ot_policy_salary,ot_amount_salary,gratuity_salary, pay_unit_master.unit_code, pay_unit_master.state_name,ot_amount_salary,esic_oa_salary,esic_ot,ot_applicable,esic_common_allow,pf_cmn_on,lwf_act_mon,bill_esic_percent FROM " + pay_billing_master_history + " as pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE " + menu_where, d.con1);

        d.con1.Open();
        try
        {
            string start_day = d.getsinglestring("select month_calc  FROM " + pay_billing_master_history + " AS pay_billing_master_history inner join pay_unit_master on pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code and pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE " + menu_where + " limit 1");
            if (start_day == "") { start_day = "1"; }
            int days = CountDay(int.Parse(months), int.Parse(years), int.Parse(start_day), client_code, comp_code, from_to_date);
            MySqlDataReader dr = menu.ExecuteReader();
            while (dr.Read())
            {
                //var query1 = from val in months.Split(',') select (val);
                //foreach (string month in query1)
                //{
                int ptmonth = int.Parse(months);
                int ptyear = 0;
                if (!years.Contains(","))
                {
                    ptyear = int.Parse(years);
                }
                else
                {
                    ptyear = int.Parse(years.Substring(0, years.IndexOf(",")));
                    years = years.Substring(years.IndexOf(",") + 1, years.Length - (years.IndexOf(",") + 1));
                }
                //int days = 0;
                //if (days == 0)
                //{
                //    days = CountDay(ptmonth, ptyear, int.Parse(dr.GetValue(2).ToString()), client_code, comp_code, from_to_date);
                //}
                if (int.Parse(months) == 1 && int.Parse(years) == 2019 && client_code == "RLIC HK")
                {
                    days = 27;
                }
                //Duty Hours
                hours = double.Parse(dr.GetValue(1).ToString());

                //month calculation
                month_calc = days;

                //basic
                basic = double.Parse(dr.GetValue(4).ToString());
                //vda
                vda = double.Parse(dr.GetValue(5).ToString());

                double basic_vda = basic + vda;
                bonus = double.Parse(dr.GetValue(8).ToString());

                //per day rate
                if (double.Parse(dr.GetValue(3).ToString()) > 0)
                {
                    per_day_rate = double.Parse(dr.GetValue(3).ToString());
                    basic_vda = (days * double.Parse(dr.GetValue(3).ToString()));

                }
                else
                {
                    per_day_rate = 0;
                    if (basic_vda > bonus)
                    {
                        bonus = 0;
                    }
                }

                //washing,travelling,education 
                washing = double.Parse(dr.GetValue(9).ToString());
                travelling = double.Parse(dr.GetValue(10).ToString());
                education = double.Parse(dr.GetValue(11).ToString());

                // Allowance (allowances_salary)
                if (int.Parse(dr.GetValue(34).ToString()).Equals(1))
                {
                    salary_OA_ESIC = double.Parse(dr.GetValue(12).ToString());
                    salary_OA = 0;
                }
                else { salary_OA = double.Parse(dr.GetValue(12).ToString());
                salary_OA_ESIC = 0;
                }

                //cca
                cca = double.Parse(dr.GetValue(13).ToString());

                // other allowances (common = other_allow)
                if (int.Parse(dr.GetValue(37).ToString()).Equals(1))
                {
                    common_allowance = double.Parse(dr.GetValue(14).ToString());
                    esic_common_allowance = 0;
                }
                else
                {
                    esic_common_allowance = double.Parse(dr.GetValue(14).ToString());
                    common_allowance = 0;
                }
                //HRA
                if (double.Parse(dr.GetValue(15).ToString()).Equals(0))
                {
                    hra_amount = (basic_vda * double.Parse(dr.GetValue(16).ToString())) / 100;
                }
                else
                {
                    hra_amount = double.Parse(dr.GetValue(15).ToString());
                    // start vinod for arrier bill 2/04/2020
                    if (from_to_date == 2 || from_to_date == 3)
                    {
                        hra_amount = 0;
                    }
                    // End vinod for arrier bill 2/04/2020
                }

                //Bonus Taxable Yes/No
                if (dr.GetValue(23).ToString().Equals("1"))
                {
                    //before gross
                    if (bonus > basic_vda && dr.GetValue(6).ToString().Equals("1"))
                    {
                        bonus_taxable = (bonus * double.Parse(dr.GetValue(7).ToString())) / 100;
                    }
                    else
                    {
                        bonus_taxable = (basic_vda * double.Parse(dr.GetValue(7).ToString())) / 100;
                    }

                }
                else if (dr.GetValue(23).ToString().Equals("0"))
                {
                    //after gross
                    if (bonus > basic_vda && dr.GetValue(6).ToString().Equals("1"))
                    {
                        bonus_non_taxable = (bonus * double.Parse(dr.GetValue(7).ToString())) / 100;
                    }
                    else
                    {
                        bonus_non_taxable = (basic_vda * double.Parse(dr.GetValue(7).ToString())) / 100;
                    }
                }
                else
                {
                    //Not Applicable
                    bonus_taxable = 0;
                    bonus_non_taxable = 0;
                }



                //Leaves taxable Yes/No
                if (dr.GetValue(17).ToString().Equals("0") && dr.GetValue(18).ToString().Equals("0"))
                {
                    leave_taxable = 0;
                    leave_non_taxable = 0;
                }
                else
                {
                    if (dr.GetValue(24).ToString().Equals("1"))
                    {
                        if (!dr.GetValue(18).ToString().Equals("0"))
                        {
                            leave_taxable = (basic_vda * double.Parse(dr.GetValue(18).ToString())) / 100;
                        }
                        else
                        {
                            leave_taxable = ((basic_vda / 26) * double.Parse(dr.GetValue(17).ToString())) / 12;

                            //leave_taxable = (basic_vda * 6.731) / 100; 
                        }
                    }
                    else if (dr.GetValue(24).ToString().Equals("0"))
                    {
                        if (!dr.GetValue(18).ToString().Equals("0"))
                        {
                            leave_non_taxable = (basic_vda * double.Parse(dr.GetValue(18).ToString())) / 100;
                        }
                        else
                        {
                            leave_non_taxable = ((basic_vda / 26) * double.Parse(dr.GetValue(17).ToString())) / 12;
                            //  leave_non_taxable = (basic_vda * 6.731) / 100; 
                        }
                        //leave_non_taxable = (basic_vda * 6.731) / 100;
                    }
                    else
                    {
                        leave_taxable = 0;
                        leave_non_taxable = 0;
                    }
                }
                //gratuity TAXABLE YES/NO ---
                if (double.Parse(dr.GetValue(25).ToString()).Equals(1))
                {
                    gratuity_taxable = (basic_vda * double.Parse(dr.GetValue(30).ToString())) / 100;
                    gratuity_non_taxable = 0;
                }
                else if (double.Parse(dr.GetValue(29).ToString()).Equals(0))
                {
                    gratuity_non_taxable = (basic_vda * double.Parse(dr.GetValue(30).ToString())) / 100;
                    gratuity_taxable = 0;
                }
                else
                {
                    gratuity_taxable = 0;
                    gratuity_non_taxable = 0;
                }


                // Special Allowance (ot_policy_salary & ot_amount_salary)
                if (dr.GetValue(28).ToString().Equals("1"))
                {
                    special_allowance = double.Parse(dr.GetValue(33).ToString());
                }
                else { special_allowance = 0; }

                // for arrier bill
                if (from_to_date == 2 || from_to_date == 3)
                {
                    bonus_taxable = (basic_vda * double.Parse(dr.GetValue(7).ToString())) / 100;
                    bonus = 0;
                    cca = 0;
                    common_allowance = 0;
                    special_allowance = 0;
                    washing = 0;
                    travelling = 0;
                    esic_common_allowance = 0;
                }

                // Gross
                gross = basic_vda + washing + travelling + education + common_allowance + salary_OA_ESIC + cca + bonus_taxable + leave_taxable + gratuity_taxable + hra_amount + special_allowance;

                //PF

                // pf = (basic_vda * double.Parse(dr.GetValue(19).ToString())) / 100;
                if (dr.GetValue(38).ToString().Equals("1"))
                {
                    pf = (gross * double.Parse(dr.GetValue(19).ToString())) / 100;
                }
                else if (dr.GetValue(38).ToString().Equals("2"))
                {
                    pf = ((gross - hra_amount) * double.Parse(dr.GetValue(19).ToString())) / 100;
                }
                else if (dr.GetValue(38).ToString().Equals("3"))
                {
                    pf = ((basic_vda + common_allowance + cca) * double.Parse(dr.GetValue(19).ToString())) / 100;
                }
                else
                {
                    pf = (basic_vda * double.Parse(dr.GetValue(19).ToString())) / 100;
                }


                //ESIC
                esic = (gross * double.Parse(dr.GetValue(20).ToString())) / 100;

                //LWF
                string temp = "";
                double calmlwf = 0;
                if (dr.GetValue(26).ToString().Equals("1") && from_to_date != 2 && from_to_date != 3)
                {
                    //if (state == "ALL")
                    //{
                    //    temp = d.getsinglestring("SELECT employee_contribution FROM pay_master_lwf WHERE STATE_NAME ='" + dr.GetValue(32).ToString() + "'  and month in (0," + ptmonth + ")");//and contract_labours = 'Yes'
                    //}
                    // else 
                    if (unit_code == "ALL" || (state == "ALL"))
                    {
                        temp = d.getsinglestring("SELECT employee_contribution FROM pay_master_lwf WHERE STATE_NAME ='" + dr.GetValue(32).ToString() + "'");//and contract_labours = 'Yes'
                    }
                    else
                    {
                        temp = d.getsinglestring("SELECT employee_contribution FROM pay_master_lwf WHERE STATE_NAME in (Select State_name from pay_unit_master where unit_code = '" + unit_code + "' and comp_code = '" + comp_code + "')");//and contract_labours = 'Yes'
                    }
                    if (temp != "")
                    {
                        calmlwf = float.Parse(temp);
                    }

                    lwf = calmlwf;
                    // }
                }
                else { lwf = 0; }

                // Uniform
                if (dr.GetValue(22).ToString().Equals("0"))
                {
                    uniform = (gross * double.Parse(dr.GetValue(21).ToString())) / 100;
                }
                else
                {
                    uniform = double.Parse(dr.GetValue(22).ToString());
                    // for arrier bill
                    if (from_to_date == 2 || from_to_date == 3)
                    { uniform = 0; }
                }

                total_deduction = pf + esic + uniform + lwf;

                //OT Applicable
                if (double.Parse(dr.GetValue(36).ToString()).Equals(1))
                {


                    ot_rate = hours > 0 ? (basic_vda / days / hours) * 1 * 2 : 0;

                    ot_rate = double.Parse(dr.GetValue(20).ToString()) > 0 ? ot_rate - ((ot_rate * double.Parse(dr.GetValue(20).ToString())) / 100) : ot_rate;

                }
                else
                {
                    ot_rate = 0;
                }

                ot_esic = 0;
                //ESIC on OT
                if (double.Parse(dr.GetValue(35).ToString()).Equals(1))
                {
                    ot_esic = (ot_rate * double.Parse(dr.GetValue(20).ToString())) / 100;
                }

                //ESIC OT Percent
                esic_ot_percent = double.Parse(dr.GetValue(20).ToString());



                take_home_salary = (gross + bonus_non_taxable + leave_non_taxable + gratuity_non_taxable + salary_OA + ot_rate + ot_esic + esic_common_allowance) - total_deduction;

                if (state == "ALL")
                {
                    d.operation("insert into " + pay_salary_unit_rate + " (comp_code,client_code,unit_code,designation,hours,month_days,per_rate_salary ,basic_vda,hra_amount_salary,sal_bonus,leave_days,gross,sal_pf,sal_esic,lwf_salary,pt_salary,sal_uniform_rate,advance_salary,fine_salary,total_deduction,total_salary,month,year,washing_salary,travelling_salary,education_salary,allowances_salary,cca_salary,other_allow,gratuity_salary,ot_amount,ot_applicable,esic_ot_applicable,common_allowance,esic_ot_percent,bonus_taxable,leave_taxable,allowances_salary_original,gratuity_taxable,sal_esic_percent,sal_pf_percent,pf_cmn_on,esic_oa_salary,esic_common_allow,pt_applicable,basic_salary, vda_salary,bonus_policy_salary,ot_amount_salary,bill_esic_percent,start_date,end_date) values ('" + comp_code + "','" + client_code + "','" + dr.GetValue(31).ToString() + "','" + dr.GetValue(0).ToString() + "','" + dr.GetValue(1).ToString() + "'," + days + "," + double.Parse(dr.GetValue(3).ToString()) + "," + Math.Round(basic_vda, 2) + "," + Math.Round(hra_amount, 2) + "," + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + "," + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + "," + Math.Round(gross, 2) + "," + Math.Round(pf, 2) + "," + Math.Round(esic, 2) + "," + Math.Round(lwf, 2) + "," + Math.Round(pt, 2) + "," + Math.Round(uniform, 2) + "," + Math.Round(advance, 2) + "," + Math.Round(fine, 2) + "," + Math.Round(total_deduction, 2) + "," + Math.Round(take_home_salary, 2) + "," + ptmonth + "," + ptyear + "," + Math.Round(washing, 2) + "," + Math.Round(travelling, 2) + "," + Math.Round(education, 2) + "," + Math.Round(salary_OA_ESIC == 0 ? salary_OA : salary_OA_ESIC, 2) + "," + Math.Round(cca, 2) + "," + Math.Round(common_allowance, 2) + "," + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + "," + Math.Round(special_allowance, 2) + "," + Math.Round(ot_rate, 2) + "," + Math.Round(ot_esic, 2) + "," + Math.Round(esic_common_allowance, 2) + "," + Math.Round(esic_ot_percent, 2) + ",'" + dr.GetValue(23).ToString() + "','" + dr.GetValue(24).ToString() + "','" + dr.GetValue(12).ToString() + "','" + dr.GetValue(25).ToString() + "','" + dr.GetValue(20).ToString() + "','" + dr.GetValue(19).ToString() + "','" + dr.GetValue(38).ToString() + "','" + dr.GetValue(34).ToString() + "','" + dr.GetValue(37).ToString() + "','" + dr.GetValue(27).ToString() + "','" + dr.GetValue(4).ToString() + "','" + dr.GetValue(5).ToString() + "','" + dr.GetValue(6).ToString() + "','" + dr.GetValue(29).ToString() + "','" + dr.GetValue(40).ToString() + "','" + from_date + "','" + to_date + "')");
                }
                else if (unit_code == "ALL")
                {
                    d.operation("insert into " + pay_salary_unit_rate + " (comp_code,client_code,unit_code,designation,hours,month_days,per_rate_salary ,basic_vda,hra_amount_salary,sal_bonus,leave_days,gross,sal_pf,sal_esic,lwf_salary,pt_salary,sal_uniform_rate,advance_salary,fine_salary,total_deduction,total_salary,month,year,washing_salary,travelling_salary,education_salary,allowances_salary,cca_salary,other_allow,gratuity_salary,ot_amount,ot_applicable,esic_ot_applicable,common_allowance,esic_ot_percent,bonus_taxable,leave_taxable,allowances_salary_original,gratuity_taxable,sal_esic_percent,sal_pf_percent,pf_cmn_on,esic_oa_salary,esic_common_allow,pt_applicable,basic_salary, vda_salary,bonus_policy_salary,ot_amount_salary,bill_esic_percent,start_date,end_date) values ('" + comp_code + "','" + client_code + "','" + dr.GetValue(31).ToString() + "','" + dr.GetValue(0).ToString() + "','" + dr.GetValue(1).ToString() + "'," + days + "," + double.Parse(dr.GetValue(3).ToString()) + "," + Math.Round(basic_vda, 2) + "," + Math.Round(hra_amount, 2) + "," + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + "," + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + "," + Math.Round(gross, 2) + "," + Math.Round(pf, 2) + "," + Math.Round(esic, 2) + "," + Math.Round(lwf, 2) + "," + Math.Round(pt, 2) + "," + Math.Round(uniform, 2) + "," + Math.Round(advance, 2) + "," + Math.Round(fine, 2) + "," + Math.Round(total_deduction, 2) + "," + Math.Round(take_home_salary, 2) + "," + ptmonth + "," + ptyear + "," + Math.Round(washing, 2) + "," + Math.Round(travelling, 2) + "," + Math.Round(education, 2) + "," + Math.Round(salary_OA_ESIC == 0 ? salary_OA : salary_OA_ESIC, 2) + "," + Math.Round(cca, 2) + "," + Math.Round(common_allowance, 2) + "," + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + "," + Math.Round(special_allowance, 2) + "," + Math.Round(ot_rate, 2) + "," + Math.Round(ot_esic, 2) + "," + Math.Round(esic_common_allowance, 2) + "," + Math.Round(esic_ot_percent, 2) + ",'" + dr.GetValue(23).ToString() + "','" + dr.GetValue(24).ToString() + "','" + dr.GetValue(12).ToString() + "','" + dr.GetValue(25).ToString() + "','" + dr.GetValue(20).ToString() + "','" + dr.GetValue(19).ToString() + "','" + dr.GetValue(38).ToString() + "','" + dr.GetValue(34).ToString() + "','" + dr.GetValue(37).ToString() + "','" + dr.GetValue(27).ToString() + "','" + dr.GetValue(4).ToString() + "','" + dr.GetValue(5).ToString() + "','" + dr.GetValue(6).ToString() + "','" + dr.GetValue(29).ToString() + "','" + dr.GetValue(40).ToString() + "','" + from_date + "','" + to_date + "')");
                }
                else
                {
                    d.operation("insert into " + pay_salary_unit_rate + " (comp_code,client_code,unit_code,designation,hours,month_days,per_rate_salary ,basic_vda,hra_amount_salary,sal_bonus,leave_days,gross,sal_pf,sal_esic,lwf_salary,pt_salary,sal_uniform_rate,advance_salary,fine_salary,total_deduction,total_salary,month,year,washing_salary,travelling_salary,education_salary,allowances_salary,cca_salary,other_allow,gratuity_salary,ot_amount,ot_applicable,esic_ot_applicable,common_allowance,esic_ot_percent,bonus_taxable,leave_taxable,allowances_salary_original,gratuity_taxable,sal_esic_percent,sal_pf_percent,pf_cmn_on,esic_oa_salary,esic_common_allow,pt_applicable,basic_salary, vda_salary,bonus_policy_salary,ot_amount_salary,bill_esic_percent,start_date,end_date) values ('" + comp_code + "','" + client_code + "','" + unit_code + "','" + dr.GetValue(0).ToString() + "','" + dr.GetValue(1).ToString() + "'," + days + "," + double.Parse(dr.GetValue(3).ToString()) + "," + Math.Round(basic_vda, 2) + "," + Math.Round(hra_amount, 2) + "," + Math.Round(bonus_taxable == 0 ? bonus_non_taxable : bonus_taxable, 2) + "," + Math.Round(leave_taxable == 0 ? leave_non_taxable : leave_taxable, 2) + "," + Math.Round(gross, 2) + "," + Math.Round(pf, 2) + "," + Math.Round(esic, 2) + "," + Math.Round(lwf, 2) + "," + Math.Round(pt, 2) + "," + Math.Round(uniform, 2) + "," + Math.Round(advance, 2) + "," + Math.Round(fine, 2) + "," + Math.Round(total_deduction, 2) + "," + Math.Round(take_home_salary, 2) + "," + ptmonth + "," + ptyear + "," + Math.Round(washing, 2) + "," + Math.Round(travelling, 2) + "," + Math.Round(education, 2) + "," + Math.Round(salary_OA_ESIC == 0 ? salary_OA : salary_OA_ESIC, 2) + "," + Math.Round(cca, 2) + "," + Math.Round(common_allowance, 2) + "," + Math.Round(gratuity_taxable == 0 ? gratuity_non_taxable : gratuity_taxable, 2) + "," + Math.Round(special_allowance, 2) + "," + Math.Round(ot_rate, 2) + "," + Math.Round(ot_esic, 2) + "," + Math.Round(esic_common_allowance, 2) + "," + Math.Round(esic_ot_percent, 2) + ",'" + dr.GetValue(23).ToString() + "','" + dr.GetValue(24).ToString() + "','" + dr.GetValue(12).ToString() + "','" + dr.GetValue(25).ToString() + "','" + dr.GetValue(20).ToString() + "','" + dr.GetValue(19).ToString() + "','" + dr.GetValue(38).ToString() + "','" + dr.GetValue(34).ToString() + "','" + dr.GetValue(37).ToString() + "','" + dr.GetValue(27).ToString() + "','" + dr.GetValue(4).ToString() + "','" + dr.GetValue(5).ToString() + "','" + dr.GetValue(6).ToString() + "','" + dr.GetValue(29).ToString() + "','" + dr.GetValue(40).ToString() + "','" + from_date + "','" + to_date + "')");
                }
                //}

            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }
    public int CountDay(int month, int year, int counter, string client_code, string comp_code, int from_to_date)
    {
        int NoOfSunday = 0;
        string start_date_common = "1", end_date_common = "31";
        var firstDay = (dynamic)null;
        if (from_to_date == 1 || from_to_date == 2) { start_date_common = "1"; }
        else
        {
            start_date_common = d.getsinglestring("SELECT IFNULL((SELECT start_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.comp_code = '" + comp_code + "' AND month = '" + month + "' AND year = '" + year + "' and type = 'billing' AND (pay_unit_master.branch_close_date is null || pay_unit_master.branch_close_date = '' || pay_unit_master.branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) limit 1),(SELECT start_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + client_code + "' AND  pay_unit_master.comp_code  = '" + comp_code + "' AND (pay_unit_master.branch_close_date is null || pay_unit_master.branch_close_date = '' || pay_unit_master.branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) limit 1))");
            end_date_common = d.getsinglestring("SELECT IFNULL((SELECT end_date_common FROM pay_billing_master_history INNER JOIN pay_unit_master ON pay_billing_master_history.billing_unit_code = pay_unit_master.unit_code AND pay_billing_master_history.comp_code = pay_unit_master.comp_code WHERE pay_unit_master.client_code = '" + client_code + "' AND pay_unit_master.comp_code = '" + comp_code + "' AND month = '" + month + "' AND year = '" + year + "' and type = 'billing' AND (pay_unit_master.branch_close_date is null || pay_unit_master.branch_close_date = '' || pay_unit_master.branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) limit 1), (SELECT end_date_common  FROM pay_billing_master  INNER JOIN  pay_unit_master  ON  pay_billing_master.billing_unit_code  =  pay_unit_master.unit_code  AND  pay_billing_master.comp_code  =  pay_unit_master.comp_code  WHERE pay_unit_master.client_code  = '" + client_code + "' AND  pay_unit_master.comp_code  = '" + comp_code + "' AND (pay_unit_master.branch_close_date is null || pay_unit_master.branch_close_date = '' || pay_unit_master.branch_close_date  >= STR_TO_DATE('01/" + month + "/" + year + "', '%d/%m/%Y')) limit 1))");

        }

        if (start_date_common != "1")
        {
            // if ((month - 1) == 0) {
            firstDay = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            //}
            //else{
            //firstDay = new DateTime(year, (month-1), int.Parse(start_date_common));
            //}
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
            //int year1 = year;
            //if (month == 12)
            //{
            //    year1 = year - 1;
            //}
            //else { year1 = year; }
            //for (int j = 0; j <= (d.CountDay((month - 1), year1, 1) - 1); j++)
            //{
            //    var date = new DateTime(year1,(month - 1),int.Parse(start_date_common));

            //    var dayscount = date.AddDays(j);
            //}

            var start_date = new DateTime((month == 1 ? year - 1 : year), (month == 1 ? 12 : month - 1), int.Parse(start_date_common));
            var end_date = new DateTime(year, (month), int.Parse(end_date_common));
            if ((end_date.Date - start_date.Date).Days == 28)
            {
                day31 = day30;
            }
            if ((end_date.Date - start_date.Date).Days +1 == 30)
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

        //if (counter == 2)
        //{
        //    if (NumOfDay.Equals(32))
        //    {
        //        NumOfDay = 31;
        //    }
        //    return NumOfDay;
        //}
        //if (counter == 1)
        //{
        //    return NumOfDay - NoOfSunday;
        //}
        //else
        //{ return NoOfSunday; }
        if (counter == 1)
        {//calendar days
            return NumOfDay;
        }
        else
        { //working days
            return NumOfDay - NoOfSunday;
        }
    }
    protected string get_start_date(string client_code, string comp_code)
    {
        return d.getsinglestring("select start_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "' and pay_unit_master.branch_status = 0");
    }
    protected string get_end_date(string client_code, string comp_code)
    {
        return d.getsinglestring("select end_date_common FROM pay_billing_master inner join pay_unit_master on pay_billing_master.billing_unit_code = pay_unit_master.unit_code and pay_billing_master.comp_code = pay_unit_master.comp_code WHERE pay_billing_master.billing_client_code ='" + client_code + "' and pay_billing_master.comp_code='" + comp_code + "' and pay_unit_master.branch_status = 0");
    }

    //MD CHANGE

    /// <summary>
    /// This function is use to get Manpower Invoice Number and Bill Date
    /// </summary>
    /// <param name="comp_code"></param>
    /// <param name="client_code"></param>
    /// <param name="state_name"></param>
    /// <param name="unit_code"></param>
    /// <param name="invoice_type"></param>
    /// <param name="grade_code"></param>
    /// <param name="txt_month_year"></param>
    /// <param name="start_date"></param>
    /// <param name="end_date"></param>
    /// <param name="billing_type"></param>
    /// <returns></returns>
    public string get_invoice_bill_date(string comp_code, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year, int start_date, int end_date, string billing_type, string region, int arrears_invoice, string arrears_month_year, string arrears_type,int ot_invoice)
    {
        string bill_from_to_date = "", invoice_no = " invoice_no ", month = "", year = "",type="";

        if (ot_invoice == 1 )
        {
            type = " AND hdfc_type = 'ot_bill'";
        }
        else if (ot_invoice != 1 && client_code =="HDFC" )
        { type = " AND hdfc_type = 'manpower_bill'"; }
        if (arrears_invoice == 1)
        {
            invoice_type = "3"; type = "";
        }
        if (invoice_type != "3")
        {

            bill_from_to_date = "and start_date = '" + start_date + "' and end_date = '" + end_date + "'" + " " + billing_type + " and invoice_flag != 0";
        }
        if (invoice_type == "4" || invoice_type == "5")
        {

            bill_from_to_date = "and start_date = '" + start_date + "' and end_date = '" + end_date + "'" + "  and invoice_flag != 0";
        }
         string club_invoice = "", where2 = "";
        if (invoice_type == "3")
        {
            if (arrears_type.Equals("policy"))
            {
                month = " and month = '" + arrears_month_year.Substring(3, 2) + "'";
                year = " and year ='" + arrears_month_year.Substring(6) + "'";
            }
            else
            {
                month = " and month = " + txt_month_year.Substring(0, 2);
                year = " and year = " + txt_month_year.Substring(3);
            }
        }
        else
        {
            month = " and month = " + txt_month_year.Substring(0, 2);
            year = " and year = " + txt_month_year.Substring(3);

        }
        if (invoice_type == "2" || invoice_type == "3" && grade_code!="")
        {
            club_invoice = "and GRADE_CODE = '" + grade_code + "'";
        }

        where2 = "where comp_code= '" + comp_code + "' and  client_code='" + client_code + "' " + month + " " + year + type+" " + club_invoice + "  " + bill_from_to_date;
        if (state_name != "ALL")
        {
            where2 = "where comp_code='" + comp_code + "' and  client_code='" + client_code + "' and state_name='" + state_name + "' " + month + " " + year +type+ " " + club_invoice + "  " + bill_from_to_date;
        }
        if (unit_code != "ALL")
        {
            where2 = "where comp_code= '" + comp_code + "' and client_code='" + client_code + "' and state_name='" + state_name + "' and unit_code ='" + unit_code + "' " + month + " " + year +type+ " " + club_invoice + "  " + bill_from_to_date;
        }

        string where_state = "";
        if (state_name.Equals("Maharashtra") && client_code.Equals("BAGIC") && int.Parse(txt_month_year.Replace("/", "")) > 42020) { where_state = " and state='" + state_name + "' and billingwise_id = 5"; }
        if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + client_code + "' " + where_state).Equals("5"))
        {
            where_state = " and pay_billing_unit_rate_history.zone = '" + region + "'";
        }
        else
        { where_state = ""; }

        //check month year less than 04/2019
        if (invoice_type != "3")
        {
            //add 05/12/2019
            string month1 = "" + txt_month_year.Substring(0, 2) + "";
            string year1 = "" + txt_month_year.Substring(3) + "";
            DateTime date1 = new DateTime(int.Parse(year1), int.Parse(month1), 1);
            DateTime date2 = new DateTime(2019, 4, 1);
            int result = DateTime.Compare(date1, date2);
            if (result == 0 || result == 1) { invoice_no = " auto_invoice_no "; }
        }
        string table = "";
        if (invoice_type == "3")
        {
            table = "pay_billing_unit_rate_history_arrears";
            where_state = "";
        }
        else if (invoice_type == "4")
        {
            table = "pay_billing_r_m";
            where_state = "";
        }
        else if (invoice_type == "5")
        {
            table = "pay_billing_admini_expense";
            where_state = "";
        }
        else
        {
            table = "pay_billing_unit_rate_history";
        }


        string invoice = d.getsinglestring("select distinct " + invoice_no + "  from " + table + " " + where2 + where_state);
        string bill_date = d.getsinglestring("select distinct date_format(billing_date , '%d/%m/%Y') from " + table + " " + where2);

        return (invoice == "") ? String.Empty : invoice + "," + bill_date;

    }
    /// <summary>
    /// This function is use to get Conveyance Invoice Number and Bill Date 
    /// Get Conveyance bill then table = 1
    /// Get 
    /// </summary>
    /// <param name="comp_code"></param>
    /// <param name="client_code"></param>
    /// <param name="state_name"></param>
    /// <param name="unit_code"></param>
    /// <param name="invoice_type"></param>
    /// <param name="grade_code"></param>
    /// <param name="txt_month_year"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public string get_invoice_bill_date(string comp_code, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year, int bill_type, string region, string billing_type_man)
    {
        string invoice_no = "invoice_no";
        string invoice_flag = "and invoice_flag != 0";
        string club_invoice = "", where2 = "";
        string month = txt_month_year.Substring(0, 2);
        string year = txt_month_year.Substring(3);

        //get table name

        string billing_type = (bill_type == 1) ? "and type = 'Conveyance' and conveyance_type !='100'" : (bill_type == 6) ? "and type = 'Conveyance' and conveyance_type ='100'" : (bill_type == 2) ? "and type = 'Material'" : (bill_type == 3) ? "and type = 'DeepClean'" : (bill_type == 4) ? "and type = 'PestControl'" : "";
        if (invoice_type == "2")
        {
            club_invoice = "and GRADE_CODE = '" + grade_code + "'";
        }
        club_invoice = club_invoice + " " + billing_type + " " + invoice_flag;
        where2 = "where comp_code= '" + comp_code + "' and  client_code='" + client_code + "' and month='" + month + "' and year='" + year + "' " + club_invoice;
        if (state_name != "ALL")
        {
            where2 = "where comp_code='" + comp_code + "' and  client_code='" + client_code + "' and state_name='" + state_name + "' and month='" + month + "' and year='" + year + "'" + club_invoice;
        }
        if (unit_code != "ALL")
        {

            where2 = "where comp_code= '" + comp_code + "' and client_code='" + client_code + "' and state_name='" + state_name + "' and unit_code ='" + unit_code + "' and month='" + month + "' and year='" + year + "'" + club_invoice;

        }

        //check month year less than 04/2019
        DateTime date1 = new DateTime(int.Parse(year), int.Parse(month), 1);
        DateTime date2 = new DateTime(2019, 4, 1);
        int result = DateTime.Compare(date1, date2);
        if (result == 0 || result == 1) { invoice_no = " auto_invoice_no "; }

        string where_state = "";
        if (state_name.Equals("Maharashtra") && client_code.Equals("BAGIC") && int.Parse(txt_month_year.Replace("/", "")) > 42020 && billing_type_man.Equals("1")) { where_state = " and state='" + state_name + "' and billingwise_id = 5"; }
        if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + client_code + "' " + where_state).Equals("5"))
        {
            where_state = " and pay_billing_unit_rate_history.zone = '" + region + "'";
        }
        else
        { where_state = ""; }

        string invoice = d.getsinglestring("select distinct " + invoice_no + "  from pay_billing_material_history " + where2 + where_state);
        string bill_date = d.getsinglestring("select distinct date_format(billing_date , '%d/%m/%Y') from pay_billing_material_history " + where2);

        return (invoice == "") ? String.Empty : invoice + "," + bill_date;

    }

    public string get_invoice_query(string comp_code, string client_name, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year, int start_date, int end_date, string billing_type, string region, string arrear_start, string arrear_end, string arrear_next_year, int invoice_fl_man, int invoice_arrear,int ot_invoice)
    {
        string bill_from_to_date = "and start_date = '0' and end_date = '0'" + " " + billing_type,bill_from_to_date1 = "and start_date = '0' and end_date = '0'", ed_date = "", month = "", year = "", month_in = "", year_in = "", new_month_in = "", new_year_in = "", sql = "", sac_code = "", delete_where = "", type ="";
        string invoice_flag = " and invoice_flag != 0 ";
        string where = "", club_invoice = "", UNIT_ADD2 = "UNIT_ADD2", gst_applicable = "", where2 = "", where3 = "",ot_where="";
        month = txt_month_year.Substring(0, 2);
        year = txt_month_year.Substring(3);

        string where_state = "";
        if (state_name.Equals("Maharashtra") && client_code.Equals("BAGIC") && int.Parse(txt_month_year.Replace("/", "")) > 42020) { where_state = " and state='" + state_name + "' and billingwise_id = 5"; }
        if (d.getsinglestring("select billingwise_id from pay_client_billing_details where client_code = '" + client_code + "' " + where_state).Equals("5"))
        {
            where_state = " and pay_billing_unit_rate_history.zone = '" + region + "'";
        }
        else
        { where_state = ""; }
        if (billing_type.Contains("Arrears_bill"))
        {
            string month1 = txt_month_year.Substring(0, txt_month_year.IndexOf("/") - 0);
            string year1 = txt_month_year.Substring(month1.Length + 1);
            month_in = "in (" + month1 + ") ";
            year_in = "in ('" + year1 + "')";
            //if (arrear_next_year != "")
            //{
            //    string month11 = arrear_next_year.Substring(0, arrear_next_year.IndexOf("/") - 0);
            //    string year11 = arrear_next_year.Substring(month11.Length + 1);
            //    new_month_in = "in (" + month11 + ") ";
            //    new_year_in = "in ('" + year11 + "')";
            //}
            month = txt_month_year.Substring(0, 2);
            year = year1;

            if (arrear_next_year.Equals("policy"))
            {
                month_in = "in (" + arrear_start.Substring(3, 2) + ") ";
                year_in = "in ('" + arrear_start.Substring(6) + "')";
                year = arrear_start.Substring(6);
                month = arrear_start.Substring(3, 2);
            }
        }
        else
        {

            month = txt_month_year.Substring(0, 2);
            year = txt_month_year.Substring(3);
            month_in = "= '" + month + "'";
            year_in = "= '" + year + "'";
        }

        // rahul add Gst_to_be type  start

        string gst_to_be = d.getsinglestring("  select  DISTINCT (`Gst_to_be`) as 'Gst_to_be' from pay_unit_master where comp_code='" + comp_code + "' and client_code='" + client_code + "'");
        // end

        string arrear_flag = "", arrear_bill_type = "";

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string month_name = mfi.GetMonthName(int.Parse(month)).ToString();
        string gststate = state_name;
        string clientname = client_name;
        string start_date_common = get_start_date(client_code, comp_code);
        string invoice_month_name = "concat('" + month_name + "',' ' ,'" + year + "')";
        if (billing_type.Contains("Arrears_bill")) { invoice_month_name = "concat('ARREAR ',' ','" + month_name + "',' ' ,'" + year + "')"; }
        if (start_date != 0 && end_date != 0)
        {
            bill_from_to_date = "and start_date = '" + start_date + "' and end_date = '" + end_date + "'" + " " + billing_type;
            bill_from_to_date1 = "and start_date = '" + start_date + "' and end_date = '" + end_date + "'";
            start_date_common = start_date.ToString();

        }
        if (unit_code == "ALL") { UNIT_ADD2 = "'' AS UNIT_ADD2"; }
        //string month_name_arear = "";
        //string month_name_arear1 = "";
        ////if (billing_type.Contains("Arrears_bill") && arrear_next_year.Equals("policy"))
        //{

        //    string month1 = mfi.GetMonthName(int.Parse(arrear_start.Substring(3, 2))).ToString();
        //    string month2 = mfi.GetMonthName(int.Parse(arrear_end.Substring(3, 2))).ToString();
        //    month_name_arear = "" + arrear_start.Substring(0, 2) + " " + month1 + " " + arrear_start.Substring(6) + "";
        //    month_name_arear1 = " TO " + arrear_end.Substring(0, 2) + " " + month2 + " " + arrear_end.Substring(6) + "";
        //    //arrear_start arrear_end
        //}
        //if (billing_type.Contains("Arrears_bill")) { invoice_month_name = "concat('ARREAR BILL',' ','" + month_name_arear + "',' ' ,'" + month_name_arear1 + "')"; }

        string daterange = "concat(upper(DATE_FORMAT(str_to_date('" + year + "-" + month + "-01','%Y-%m-%d'), '%d %b %Y')),' TO ',upper(DATE_FORMAT(LAST_DAY('" + year + "-" + month + "-01'), '%d %b %Y'))) as start_end_date";
        if (start_date_common != "" && start_date_common != "1" || start_date != 0)
        {

            ed_date = start_date == 0 ? (int.Parse(start_date_common) - 1).ToString() : end_date.ToString();

            daterange = "concat(upper(DATE_FORMAT(str_to_date('" + (int.Parse(month) == 1 ? int.Parse(year) - 1 : int.Parse(year)) + "-" + (int.Parse(month) == 1 ? 12 : int.Parse(month) - 1) + "-" + start_date_common + "','%Y-%m-%d'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + year + "-" + month + "-" + (int.Parse(ed_date)) + "','%Y-%m-%d'), '%D %b %Y'))) as start_end_date";
        }

        if (arrear_next_year.Equals("policy"))
        {
            daterange = "concat(upper(DATE_FORMAT(str_to_date('" + arrear_start + "','%d/%m/%Y'), '%D %b %Y')),' TO ',upper(DATE_FORMAT(str_to_date('" + arrear_end + "','%d/%m/%Y'), '%D %b %Y'))) as start_end_date";
        }

        if (invoice_type == "2")
        {
            club_invoice = "and GRADE_CODE = '" + grade_code + "'";

            if ("'" + grade_code + "'" == "'SG'")
            {
                sac_code = " and sac_code = '998525'";
            }
            else
            {
                sac_code = " and sac_code = '998519'";
            }
        }

         type = "'manpower'";
        if (billing_type.Contains("Arrears_bill"))
        {

            arrear_flag = " `pay_billing_unit_rate_history_arrears` as ";
            arrear_bill_type = "";
            bill_from_to_date = "";
            type = "'arrears_manpower'";
        }
        //if (client_code == "BAGICTM" && state_name == "Maharashtra")
        //{

        //    where = "pay_billing_unit_rate_history.grade_code = '"+grade_code+"' and pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name";
        //    if (state_name == "ALL")
        //    {
        //        where = "pay_billing_unit_rate_history.grade_code = '" + grade_code + "' and pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name";
        //    }
        //    else if (unit_code == "ALL")
        //    {
        //        where = "pay_billing_unit_rate_history.grade_code = '" + grade_code + "' and pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
        //    }
        //    return "SELECT   comp_code , client_code,  case when client_code = 'BAGIC TM' then 'BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD' else client end AS 'other',  COMPANY_NAME ,  COMP_ADDRESS1  AS 'ADDRESS1',  COMP_ADDRESS2  AS 'ADDRESS2',  COMP_CITY  AS 'CITY',  COMP_STATE  AS 'STATE',  PF_REG_NO ,  COMPANY_PAN_NO ,  COMPANY_TAN_NO ,  COMPANY_CIN_NO ,  SERVICE_TAX_REG_NO ,  ESIC_REG_NO ,  state_name  AS 'STATE_NAME',  UNIT_full_ADD1  AS 'UNIT_ADD1',  " + UNIT_ADD2 + " ,  unit_city  AS 'UNIT_CITY',  unit_gst_no ,  grade_desc  AS 'Expr1',  fromtodate  AS 'start_end_date', SUM( tot_days_present ) AS 'TOT_DAYS_PRESENT',  month_days , CASE WHEN  cca_billing  = 0 THEN (SUM( sub_total_c ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) ELSE (SUM( gross ) + ((SUM( gross ) * SUM( esic )) / 100) + SUM( pf ) + SUM( uniform ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) END AS 'grand_total',(SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total',ROUND(SUM(CGST9), 2) as CGST, ROUND(SUM(SGST9), 2) as SGST, ROUND(SUM(IGST18), 2) as IGST,  " + invoice_month_name + "  AS 'month',  housekeeiing_sac_code ,  Security_sac_code,grade_code As 'designation',IFNULL((Select (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost)+ SUM(ot_rate * ot_hours)) from pay_billing_unit_rate_history where  billing_gst_applicable = 1  AND " + where + " ),0) as 'hrs_12_ot' from pay_billing_unit_rate_history  WHERE  " + where;

        //}
        //else
        if (client_name.Contains("BAJAJ") && client_code != "4" && !client_code.Equals("BAGICTM"))
        {


            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + where_state + "  GROUP BY pay_billing_unit_rate_history.state_name";
            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + where_state+ " GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + " ";
            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + where_state+ "  GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + "  ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', ROUND(SUM(CGST9), 2) AS 'CGST', ROUND(SUM(SGST9), 2) AS 'SGST', ROUND(SUM(IGST18), 2) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history  WHERE  " + where;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change in invoice query then also same changes in report query same

                return "SELECT   comp_code , client_code,  case when client_code = 'BAGIC TM' or `client_code` = 'BAGIC_OC' then 'BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD' else client end AS 'other',  COMPANY_NAME ,  COMP_ADDRESS1  AS 'ADDRESS1',  COMP_ADDRESS2  AS 'ADDRESS2',  COMP_CITY  AS 'CITY',  COMP_STATE  AS 'STATE',  PF_REG_NO ,  COMPANY_PAN_NO ,  COMPANY_TAN_NO ,  COMPANY_CIN_NO ,  SERVICE_TAX_REG_NO ,  ESIC_REG_NO ,  state_name  AS 'STATE_NAME',  UNIT_full_ADD1  AS 'UNIT_ADD1',  " + UNIT_ADD2 + " ,  unit_city  AS 'UNIT_CITY',  unit_gst_no ,  grade_desc  AS 'Expr1',  fromtodate  AS 'start_end_date', SUM( tot_days_present ) AS 'TOT_DAYS_PRESENT',  month_days , CASE WHEN  cca_billing  = 0 THEN (SUM( sub_total_c ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) ELSE (SUM( gross ) + ((SUM( gross ) * SUM( esic )) / 100) + SUM( pf ) + SUM( uniform ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) END AS 'grand_total',(SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total',ROUND(SUM(CGST9), 2) as CGST, ROUND(SUM(SGST9), 2) as SGST, ROUND(SUM(IGST18), 2) as IGST,  " + invoice_month_name + "  AS 'month',  housekeeiing_sac_code ,  Security_sac_code,grade_code As 'designation',IFNULL((Select (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost)+ SUM(ot_rate * ot_hours)) from pay_billing_unit_rate_history where  billing_gst_applicable = 1  AND " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " GROUP BY pay_billing_unit_rate_history.state_name),0) as 'hrs_12_ot' from " + arrear_flag + " pay_billing_unit_rate_history  WHERE  " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    string invoice = "";
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', ROUND(SUM(CGST9), 2) AS 'CGST', ROUND(SUM(SGST9), 2) AS 'SGST', ROUND(SUM(IGST18), 2) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history  WHERE  " + where;
                    if (client_code.Equals("BAGIC"))
                    {
                        invoice = d.getsinglestring("select invoice_no from pay_billing_unit_rate_history WHERE  " + where);
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + " and invoice_no= '" + invoice + "' ");
                    }
                    else
                    {
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + " ");
                    }
                   // d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower'  " + bill_from_to_date1 + " ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change in invoice query then also same changes in report query same
                return "SELECT   pay_billing_unit_rate_history.comp_code , pay_billing_unit_rate_history.client_code,  case when pay_billing_unit_rate_history.client_code = 'BAGIC TM' or pay_billing_unit_rate_history.`client_code` = 'BAGIC_OC' then 'BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD' else client end AS 'other',  COMPANY_NAME ,  COMP_ADDRESS1  AS 'ADDRESS1',  COMP_ADDRESS2  AS 'ADDRESS2',  COMP_CITY  AS 'CITY',  COMP_STATE  AS 'STATE',  PF_REG_NO ,  COMPANY_PAN_NO ,  COMPANY_TAN_NO ,  COMPANY_CIN_NO ,  SERVICE_TAX_REG_NO ,  ESIC_REG_NO ,  state_name  AS 'STATE_NAME',  UNIT_full_ADD1  AS 'UNIT_ADD1',  invoice_shipping_address AS 'UNIT_ADD2' ,  unit_city  AS 'UNIT_CITY',  unit_gst_no ,  grade_desc  AS 'Expr1',  fromtodate  AS 'start_end_date', SUM( tot_days_present ) AS 'TOT_DAYS_PRESENT',  month_days , CASE WHEN  cca_billing  = 0 THEN (SUM( sub_total_c ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) ELSE (SUM( gross ) + ((SUM( gross ) * SUM( esic )) / 100) + SUM( pf ) + SUM( uniform ) - IF(SUM( ot_hours ) > 0, 0,  ot_rate )) END AS 'grand_total',(SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total',ROUND(SUM(CGST9), 2) as CGST, ROUND(SUM(SGST9), 2) as SGST, ROUND(SUM(IGST18), 2) as IGST,  " + invoice_month_name + "  AS 'month',  housekeeiing_sac_code ,  Security_sac_code,grade_code As 'designation',IFNULL((Select (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost)+ SUM(ot_rate * ot_hours)) from pay_billing_unit_rate_history where  billing_gst_applicable = 1  AND " + where + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " ),0) as 'hrs_12_ot' from " + arrear_flag + " pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE  " + where;
            }
        }
        else if (client_code == "4")
        {


            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_code,pay_billing_unit_rate_history.emp_code";
            where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_code,pay_billing_unit_rate_history.emp_code";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + " ";

            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_code,pay_billing_unit_rate_history.emp_code";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + "  ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', ROUND(SUM(CGST9), 2) AS 'CGST', ROUND(SUM(SGST9), 2) AS 'SGST', ROUND(SUM(IGST18), 2) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same

                return "SELECT  comp_code  ,client  AS 'other' ,COMPANY_NAME  ,COMP_ADDRESS1 AS 'ADDRESS1'  ,COMP_ADDRESS2 AS 'ADDRESS2'  ,COMP_CITY as 'CITY'  ,COMP_STATE  as 'STATE'  ,PF_REG_NO  ,COMPANY_PAN_NO  ,COMPANY_TAN_NO  ,COMPANY_CIN_NO  ,SERVICE_TAX_REG_NO  ,ESIC_REG_NO  ,state_name as 'STATE_NAME' ,UNIT_full_ADD1 as 'UNIT_ADD1'  ," + UNIT_ADD2 + " ,unit_city as 'UNIT_CITY'  ,unit_gst_no as 'unit_gst_no'  ,grade_desc  AS 'designation' ,fromtodate as 'start_end_date'  ,sum(tot_days_present) as 'TOT_DAYS_PRESENT' ,month_days as 'month_days'  ,CASE WHEN  cca_billing  = 0 THEN ( sum(sub_total_c)  - IF( sum(ot_hours)  > 0, 0,  ot_rate )) ELSE ( sum(gross) + (( sum(gross)  *  sum(esic) ) / 100)+  sum(pf) +  sum(uniform) - IF( sum(ot_hours)  > 0, 0,  ot_rate )) END AS 'grand_total',(SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total',ROUND(SUM(CGST9), 2) as CGST, ROUND(SUM(SGST9), 2) as SGST, ROUND(SUM(IGST18), 2) as IGST, " + invoice_month_name + "  AS 'month'  ,housekeeiing_sac_code  ,Security_sac_code  ,unit_name  ,emp_name  ,unit_code,IF(billing_gst_applicable = 1, (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost)), 0) AS 'hrs_12_ot' from " + arrear_flag + "  pay_billing_unit_rate_history where  " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', ROUND(SUM(CGST9), 2) AS 'CGST', ROUND(SUM(SGST9), 2) AS 'SGST', ROUND(SUM(IGST18), 2) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower'  " + bill_from_to_date1 + "");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT  pay_billing_unit_rate_history.comp_code  ,client  AS 'other' ,COMPANY_NAME  ,COMP_ADDRESS1 AS 'ADDRESS1'  ,COMP_ADDRESS2 AS 'ADDRESS2'  ,COMP_CITY as 'CITY'  ,COMP_STATE  as 'STATE'  ,PF_REG_NO  ,COMPANY_PAN_NO  ,COMPANY_TAN_NO  ,COMPANY_CIN_NO  ,SERVICE_TAX_REG_NO  ,ESIC_REG_NO  ,state_name as 'STATE_NAME' ,UNIT_full_ADD1 as 'UNIT_ADD1'  , invoice_shipping_address AS UNIT_ADD2 ,unit_city as 'UNIT_CITY'  ,unit_gst_no as 'unit_gst_no'  ,grade_desc  AS 'designation' ,fromtodate as 'start_end_date'  ,sum(tot_days_present) as 'TOT_DAYS_PRESENT' ,month_days as 'month_days'  ,CASE WHEN  cca_billing  = 0 THEN ( sum(sub_total_c)  - IF( sum(ot_hours)  > 0, 0,  ot_rate )) ELSE ( sum(gross) + (( sum(gross)  *  sum(esic) ) / 100)+  sum(pf) +  sum(uniform) - IF( sum(ot_hours)  > 0, 0,  ot_rate )) END AS 'grand_total',(SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total',ROUND(SUM(CGST9), 2) as CGST, ROUND(SUM(SGST9), 2) as SGST, ROUND(SUM(IGST18), 2) as IGST, " + invoice_month_name + "  AS 'month'  ,housekeeiing_sac_code  ,Security_sac_code  ,unit_name  ,emp_name  ,unit_code,IF(billing_gst_applicable = 1, (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost)), 0) AS 'hrs_12_ot' from " + arrear_flag + "  pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' where  " + where + "  ORDER BY state_name,unit_name,emp_name";
            }
        }


        else if (client_code == "HDFC")
        {
            //if (billing_type.Contains("Arrears_bill"))
            //{
            //    arrear_bill_type = "";
            //    bill_from_to_date = "";
            //    club_invoice = "";
            //    arrear_flag = " `pay_billing_unit_rate_history_arrears` as ";
            //}
            string hdfc_type="";
            if(invoice_arrear !=1){hdfc_type=" and hdfc_type='manpower_bill'";}
            where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and state_name = '" + state_name + "'  and unit_code = '" + unit_code + "' and month " + month_in + " AND Year " + year_in + " and tot_days_present > 0   " + hdfc_type + club_invoice + " " + bill_from_to_date + " GROUP BY state_name";
            ot_where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and state_name = '" + state_name + "'  and unit_code = '" + unit_code + "' and month " + month_in + " AND Year " + year_in + "  and hdfc_type='ot_bill'  " + club_invoice + " " + bill_from_to_date + " GROUP BY state_name";
            where2 = " comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "'  and unit_code = '" + unit_code + "'  and tot_days_present > 0  " + club_invoice + " " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and month " + month_in + " and Year " + year_in + " and tot_days_present > 0  " + hdfc_type + club_invoice + " " + bill_from_to_date + " GROUP BY state_name";
                ot_where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and month " + month_in + " and Year " + year_in + "  and hdfc_type='ot_bill'  " + club_invoice + " " + bill_from_to_date + " GROUP BY state_name";

                where2 = " comp_code = '" + comp_code + "' and client_code = '" + client_code + "'  and tot_days_present > 0  " + club_invoice + " " + bill_from_to_date + " GROUP BY state_name";
            }
            if (unit_code == "ALL")
            {
                where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and state_name = '" + state_name + "' and month " + month_in + " and Year " + year_in + " and tot_days_present > 0  " + hdfc_type + club_invoice + "  " + bill_from_to_date + " GROUP BY state_name ";
                ot_where = " pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and state_name = '" + state_name + "' and month " + month_in + " and Year " + year_in + "  and hdfc_type='ot_bill'  " + club_invoice + "  " + bill_from_to_date + " GROUP BY state_name ";
                where2 = " comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "' and month " + month_in + " and Year " + year_in + " and tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " GROUP BY state_name ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', SUM(Amount + ot_amount + Service_charge) AS 'total', IF(LOCATE(comp_state, STATE_NAME), (SUM(Amount + ot_amount + Service_charge) * 9) / 100, 0) AS 'SGST', IF(LOCATE(comp_state, STATE_NAME), (SUM(Amount + ot_amount + Service_charge) * 9) / 100, 0) AS 'CGST', IF(LOCATE(comp_state, STATE_NAME) != 1, (SUM(Amount + ot_amount + Service_charge) * 18) / 100, 0) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + "  " + sac_code + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same

                return "Select client  AS 'other', comp_state as 'state', unit_name , comp_code , COMPANY_NAME , COMP_ADDRESS1  AS 'ADDRESS1', COMP_ADDRESS2  AS 'ADDRESS2', COMP_CITY  AS 'CITY', PF_REG_NO , COMPANY_PAN_NO , COMPANY_TAN_NO , COMPANY_CIN_NO , SERVICE_TAX_REG_NO , ESIC_REG_NO , UNIT_full_ADD1  AS 'UNIT_ADD1', " + UNIT_ADD2 + ", unit_gst_no , state_name , grade_desc  AS 'designation', unit_city , client_branch_code , SUM( tot_days_present ) as 'tot_days_present', SUM( Amount +ot_amount +  Service_charge ) AS 'total', IF(LOCATE( comp_state ,  STATE_NAME ), (SUM( Amount +ot_amount +  Service_charge ) * 9) / 100, 0) AS 'SGST', IF(LOCATE( comp_state ,  STATE_NAME ), (SUM( Amount  +ot_amount+  Service_charge ) * 9) / 100, 0) AS 'CGST', IF(LOCATE( comp_state ,  STATE_NAME ) != 1, (SUM( Amount +ot_amount +  Service_charge ) * 18) / 100, 0) AS 'IGST', SUM(IF(EMP_TYPE = 'Permanent' ,1,0)) AS 'Expr1', " + invoice_month_name + "  AS 'month', fromtodate as 'start_end_date',IFNULL((SELECT SUM(Amount + ot_amount + Service_charge) FROM  " + arrear_flag + " pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 AND  " + where2 + " and month " + month_in + " and Year " + year_in + " ),0) as 'hrs_12_ot' From " + arrear_flag + " pay_billing_unit_rate_history Where  " + where2 + " and month " + month_in + " and Year " + year_in;
            }
            else
            {
                if (ot_invoice == 1)
                {
                    //OT invoice
                    //For Report Table
                    if (invoice_fl_man == 1)
                    {
                        sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower_ot' AS 'comp_code', SUM(Amount + Service_charge) AS 'total',  sum(SGST9) AS 'SGST',  sum(CGST9) AS 'CGST',  sum(IGST18) AS 'IGST',start_date,end_date,'" + gst_to_be + "' FROM pay_billing_unit_rate_history WHERE  " + ot_where;
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + "  " + sac_code + " and type = 'manpower_ot' " + bill_from_to_date1 + " ");
                        d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                    }
                    //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                    return "SELECT  client AS 'other', comp_state AS 'state', unit_name, pay_billing_unit_rate_history.comp_code, COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, UNIT_full_ADD1 AS 'UNIT_ADD1', invoice_shipping_address AS 'UNIT_ADD2', unit_gst_no, state_name, grade_desc AS 'designation', unit_city, client_branch_code, SUM(tot_days_present) AS 'tot_days_present', sum(ot_hours) as 'type', ot_rate AS 'ZONE', SUM(Amount + Service_charge) AS 'total', sum(SGST9) AS 'SGST', sum(CGST9) AS 'CGST', sum(IGST18) AS 'IGST', SUM(IF(EMP_TYPE = 'Permanent', 1, 0)) AS 'Expr1', " + invoice_month_name + " AS 'month', fromtodate AS 'start_end_date', IFNULL((SELECT  SUM(Amount  + Service_charge) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 and " + ot_where + "), 0) AS 'hrs_12_ot' FROM pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' Where  " + ot_where;
                
                }
                else
                {
                    //For Report Table
                    if (invoice_fl_man == 1)
                    {
                        sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', SUM(Amount + ot_amount + Service_charge) AS 'total', IF(LOCATE(comp_state, STATE_NAME), (SUM(Amount + ot_amount + Service_charge) * 9) / 100, 0) AS 'SGST', IF(LOCATE(comp_state, STATE_NAME), (SUM(Amount + ot_amount + Service_charge) * 9) / 100, 0) AS 'CGST', IF(LOCATE(comp_state, STATE_NAME) != 1, (SUM(Amount + ot_amount + Service_charge) * 18) / 100, 0) AS 'IGST',start_date,end_date,'" + gst_to_be + "' FROM pay_billing_unit_rate_history WHERE  " + where;
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + "  " + sac_code + " and type = 'manpower' " + bill_from_to_date1 + " ");
                        d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                    }
                    //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                    return "Select client  AS 'other', comp_state as 'state', unit_name , pay_billing_unit_rate_history.comp_code , COMPANY_NAME , COMP_ADDRESS1  AS 'ADDRESS1', COMP_ADDRESS2  AS 'ADDRESS2', COMP_CITY  AS 'CITY', PF_REG_NO , COMPANY_PAN_NO , COMPANY_TAN_NO , COMPANY_CIN_NO , SERVICE_TAX_REG_NO , ESIC_REG_NO , UNIT_full_ADD1  AS 'UNIT_ADD1',invoice_shipping_address AS 'UNIT_ADD2', unit_gst_no , state_name , grade_desc  AS 'designation', unit_city , client_branch_code , SUM( tot_days_present ) as 'tot_days_present', SUM( Amount +ot_amount +  Service_charge ) AS 'total', IF(LOCATE( comp_state ,  STATE_NAME ), (SUM( Amount +ot_amount +  Service_charge ) * 9) / 100, 0) AS 'SGST', IF(LOCATE( comp_state ,  STATE_NAME ), (SUM( Amount  +ot_amount+  Service_charge ) * 9) / 100, 0) AS 'CGST', IF(LOCATE( comp_state ,  STATE_NAME ) != 1, (SUM( Amount +ot_amount +  Service_charge ) * 18) / 100, 0) AS 'IGST', SUM(IF(EMP_TYPE = 'Permanent' ,1,0)) AS 'Expr1', " + invoice_month_name + "  AS 'month', fromtodate as 'start_end_date',IFNULL((SELECT SUM(Amount + ot_amount + Service_charge) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 AND  " + where + "),0) as 'hrs_12_ot' From " + arrear_flag + " pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' Where  " + where;
                }
            }
        }
        else if (client_code == "Credence")
        {
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.unit_code, pay_billing_unit_rate_history.grade_code";

            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_unit_master.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0  " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.unit_code, pay_billing_unit_rate_history.grade_code";

                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0  " + bill_from_to_date + "  ";
            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.unit_code, pay_billing_unit_rate_history.grade_code";

                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  AND   (chemical_applicable = 1 OR tool_applicable = 1) and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + " ";
            }
            if (arrear_next_year != "Select")
            {
                return "SELECT comp_code,COMPANY_NAME,COMP_ADDRESS1 AS 'ADDRESS1',COMP_ADDRESS2 AS 'ADDRESS2',COMP_CITY AS 'CITY',COMP_STATE AS 'STATE',PF_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO,COMPANY_CIN_NO,SERVICE_TAX_REG_NO,ESIC_REG_NO,state_name AS 'STATE_NAME',UNIT_full_ADD1 AS 'UNIT_ADD1'," + UNIT_ADD2 + ",unit_city AS 'UNIT_CITY',unit_gst_no,grade_desc AS 'designation',fromtodate AS 'start_end_date', " + invoice_month_name + "  AS 'month' ,(select sum(tot_days_present)  from  pay_billing_unit_rate_history WHERE pay_billing_unit_rate_history.comp_code = '" + comp_code + "' AND pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND pay_billing_unit_rate_history.month " + month_in + " AND pay_billing_unit_rate_history.Year " + year_in + ") as 'year', (pay_billing_unit_rate_history.emp_count * month_days) AS 'month_days',client AS 'other',housekeeiing_sac_code,Security_sac_code,unit_code,bill_amount AS 'total',month_days,year,chemical_consumables_rate,tool_tackles_rate  from " + arrear_flag + "  pay_billing_unit_rate_history where  " + where + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;
            }
            else
            {
                return "SELECT comp_code,COMPANY_NAME,COMP_ADDRESS1 AS 'ADDRESS1',COMP_ADDRESS2 AS 'ADDRESS2',COMP_CITY AS 'CITY',COMP_STATE AS 'STATE',PF_REG_NO,COMPANY_PAN_NO,COMPANY_TAN_NO,COMPANY_CIN_NO,SERVICE_TAX_REG_NO,ESIC_REG_NO,state_name AS 'STATE_NAME',UNIT_full_ADD1 AS 'UNIT_ADD1'," + UNIT_ADD2 + ",unit_city AS 'UNIT_CITY',unit_gst_no,grade_desc AS 'designation',fromtodate AS 'start_end_date', " + invoice_month_name + "  AS 'month' ,(select sum(tot_days_present)  from  pay_billing_unit_rate_history WHERE pay_billing_unit_rate_history.comp_code = '" + comp_code + "' AND pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND pay_billing_unit_rate_history.month " + month_in + " AND pay_billing_unit_rate_history.Year " + year_in + ") as 'year', (pay_billing_unit_rate_history.emp_count * month_days) AS 'month_days',client AS 'other',housekeeiing_sac_code,Security_sac_code,unit_code,bill_amount AS 'total',month_days,year,chemical_consumables_rate,tool_tackles_rate  from " + arrear_flag + "  pay_billing_unit_rate_history where  " + where;
            }
        }

        else if (client_code == "SUN")
        {

            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name ,  pay_billing_unit_rate_history.GRADE_CODE  ORDER BY  pay_billing_unit_rate_history.GRADE_CODE  = 'HK' ASC";
            where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name ";
            where2 = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "'  and unit_code = '" + unit_code + "'  and tot_days_present > 0  " + bill_from_to_date + "  ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name ,  pay_billing_unit_rate_history.GRADE_CODE  ORDER BY  pay_billing_unit_rate_history.GRADE_CODE  = 'HK' ASC";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name ";
                where2 = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "'  and tot_days_present > 0   " + bill_from_to_date + " ";
            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name ,  pay_billing_unit_rate_history.GRADE_CODE  ORDER BY  pay_billing_unit_rate_history.GRADE_CODE  = 'HK' ASC";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + "  GROUP BY  pay_billing_unit_rate_history.state_name";
                where2 = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "' and tot_days_present > 0   " + bill_from_to_date + "  ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', ROUND(((SUM((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours))))) AS 'total', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'CGST', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'SGST', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME) != 1, (SUM(Amount + Service_charge + uniform + operational_cost) * 18) / 100, 0) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM  " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same

                return "SELECT  comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1'," + UNIT_ADD2 + ", unit_city AS 'UNIT_CITY', unit_gst_no, grade_code AS 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days, ((((amount + uniform + operational_cost) / tot_days_present) * month_days) + ((((amount + uniform + operational_cost) / tot_days_present) * month_days) * bill_service_charge/100)) AS 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours),2))) AS 'total', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'SGST', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'CGST', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME) != 1, (SUM(Amount + Service_charge + uniform + operational_cost) * 18) / 100, 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty AS 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE',0 AS 'equmental_unit',0 AS 'equmental_rental_rate',0 AS 'chemical_unit',0 AS 'chemical_consumables_rate',0 AS 'dustbin_unit',0 AS 'dustbin_liners_rate',0 AS 'femina_unit',0 AS 'femina_hygiene_rate',0 AS 'aerosol_unit',0 AS 'aerosol_dispenser_rate',IF(billing_gst_applicable = 1, (SUM(Amount)+ SUM(Service_charge)+ SUM(uniform)+ SUM(operational_cost)), 0) AS 'equmental_applicable' from pay_billing_unit_rate_history_arrears  WHERE  " + where2 + " and month " + month_in + " and Year " + year_in + "  group by grade_code  ORDER BY  GRADE_CODE  = 'HK' ASC";
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', ROUND(((SUM((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours))) + (chemical_consumables_rate + dustbin_liners_rate + femina_hygiene_rate + aerosol_dispenser_rate + equmental_rental_rate) + (((chemical_consumables_rate + dustbin_liners_rate + femina_hygiene_rate + aerosol_dispenser_rate + equmental_rental_rate) * 10) / 100)), 2) AS 'total', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'CGST', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'SGST', IF(gst_applicable = 1 AND LOCATE(COMP_STATE, STATE_NAME) != 1, (SUM(Amount + Service_charge + uniform + operational_cost) * 18) / 100, 0) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + "");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT  pay_billing_unit_rate_history.comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1',invoice_shipping_address AS 'UNIT_ADD2', unit_city AS 'UNIT_CITY', unit_gst_no, grade_code AS 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days, ((((amount + uniform + operational_cost) / tot_days_present) * month_days) + ((((amount + uniform + operational_cost) / tot_days_present) * month_days) * bill_service_charge/100)) AS 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours),2))) AS 'total', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'SGST', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME), (SUM(Amount + Service_charge + uniform + operational_cost) * 9) / 100, 0) AS 'CGST', IF(gst_applicable = 0 AND LOCATE(COMP_STATE, STATE_NAME) != 1, (SUM(Amount + Service_charge + uniform + operational_cost) * 18) / 100, 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty AS 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE', equmental_unit, equmental_rental_rate, chemical_unit, chemical_consumables_rate, dustbin_unit, dustbin_liners_rate, femina_unit, femina_hygiene_rate, aerosol_unit, aerosol_dispenser_rate,IF(billing_gst_applicable = 1, (SUM(Amount)+ SUM(Service_charge)+ SUM(uniform)+ SUM(operational_cost)), 0) AS 'equmental_applicable' from " + arrear_flag + " pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE  " + where;
            }
        }
        else if (client_code == "RCPL")
        {
            string where1 = null;
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month  " + month_in + " and pay_billing_unit_rate_history.Year  " + year_in + " AND (EMP_CODE IS NULL OR EMP_CODE = '')    " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.auto_invoice_no";
            where1 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month = '" + month_in + "' and pay_billing_unit_rate_history.Year = '" + year_in + "' AND (EMP_CODE IS NULL OR EMP_CODE = '')    " + bill_from_to_date + " and invoice_flag != 0 ";
            if (state_name == "ALL")
            {
                // where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND (EMP_CODE IS NULL OR EMP_CODE = '')    " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_per";
                //rahul
                //  where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND (emp_code = '' OR emp_code IS NULL)   " + bill_from_to_date + " group by pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.companyname_gst_no ORDER BY pay_billing_unit_rate_history.state_name, pay_billing_unit_rate_history.unit_name  ";

                // order by change
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND (emp_code = '' OR emp_code IS NULL)   " + bill_from_to_date + " group by pay_billing_unit_rate_history.auto_invoice_no ORDER BY  pay_billing_unit_rate_history.auto_invoice_no  ";
                where1 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND (emp_code = '' OR emp_code IS NULL)   " + bill_from_to_date + " and invoice_flag != 0 ";

            }
            else if (unit_code != "ALL")
            {
                // where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND ( EMP_CODE  IS NULL OR  EMP_CODE  = '')   " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_per";
                // rahul changes
                //   where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND  pay_billing_unit_rate_history . state_name  = '" + state_name + "' AND  pay_billing_unit_rate_history . unit_code  = '" + unit_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND ( emp_code  = '' OR  emp_code  IS NULL)   " + bill_from_to_date + " group by pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.companyname_gst_no ORDER BY  pay_billing_unit_rate_history . state_name ,  pay_billing_unit_rate_history . unit_name   ";

                //order by change

                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND pay_billing_unit_rate_history.state_name = '" + state_name + "' AND pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND (emp_code = '' OR emp_code IS NULL)   " + bill_from_to_date + " group by pay_billing_unit_rate_history.auto_invoice_no ORDER BY pay_billing_unit_rate_history.auto_invoice_no  ";
                where1 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  AND pay_billing_unit_rate_history.state_name = '" + state_name + "' AND pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND (emp_code = '' OR emp_code IS NULL)   " + bill_from_to_date + " and invoice_flag != 0 ";

            }
            if (arrear_next_year != "Select")
            {
                //For Report Table
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, companyname_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', Amount AS 'total', SUM(ROUND(CGST9, 2)) AS 'CGST', SUM(ROUND(SGST9, 2)) AS 'SGST', SUM(ROUND(IGST18, 2)) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same

                return "SELECT if(invoice_flag != 0,date_format(billing_date,'%d/%m/%Y'),'') as 'bill_date',comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', fromtodate AS 'start_end_date', grade_desc AS 'designation', Amount  AS 'total', bill_amount AS 'equmental_handling_percent', CONCAT('" + month_name + "',' ' ,'" + year + "') AS 'month', '998519' AS 'housekeeiing_sac_code', Security_sac_code, state_per AS 'tool_unit', companyname_gst_no as 'unit_gst_no',   if(invoice_flag != 0,auto_invoice_no,'') as 'Expr1',  gst_address as 'UNIT_ADD1' FROM " + arrear_flag + " pay_billing_unit_rate_history WHERE " + where1 + " and pay_billing_unit_rate_history.month = " + month_in + " and pay_billing_unit_rate_history.Year = " + year_in;
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, companyname_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', Amount AS 'total', SUM(ROUND(CGST9, 2)) AS 'CGST', SUM(ROUND(SGST9, 2)) AS 'SGST', SUM(ROUND(IGST18, 2)) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history WHERE  " + where;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + "");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT if(invoice_flag != 0,date_format(billing_date,'%d/%m/%Y'),'') as 'bill_date',pay_billing_unit_rate_history.comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', fromtodate AS 'start_end_date', grade_desc AS 'designation', Amount  AS 'total', bill_amount AS 'equmental_handling_percent', CONCAT('" + month_name + "',' ' ,'" + year + "') AS 'month', '998519' AS 'housekeeiing_sac_code', Security_sac_code, state_per AS 'tool_unit', companyname_gst_no as 'unit_gst_no',   if(invoice_flag != 0,auto_invoice_no,'') as 'Expr1',  gst_address as 'UNIT_ADD1', invoice_shipping_address AS 'UNIT_ADD2' FROM " + arrear_flag + " pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE " + where;
            }
        }
        else if (client_code.Equals("8"))
        {

            string gst_applicable2 = "";
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
            where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name";
            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " ";
            gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                gst_applicable2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                gst_applicable2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST',grade_code,start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " " + club_invoice + " and type = "+ type +" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,grade_code,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
              
                return "SELECT  comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1', " + UNIT_ADD2 + ", unit_city AS 'UNIT_CITY', unit_gst_no, count(1) AS 'Expr1', grade_code as 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days,((amount + (Service_charge  - (((ot_rate * ot_hours) * bill_service_charge )/100))+uniform+ operational_cost)/ tot_days_present) * month_days as 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) ,2))) AS 'total',  sum((ot_rate * ot_hours)) as 'tool_unit',sum(ot_hours) as 'tool_tackles_rate',max(ot_rate) as 'equmental_handling_percent', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty as 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE',IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 AND " + gst_applicable2 + "), 0) AS 'equmental_applicable' from " + arrear_flag + "  pay_billing_unit_rate_history WHERE  " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST',grade_code,start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " " + club_invoice + " and type = 'manpower' " + bill_from_to_date1 + " ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no, sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,grade_code,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT  pay_billing_unit_rate_history.comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1', invoice_shipping_address AS 'UNIT_ADD2', unit_city AS 'UNIT_CITY', unit_gst_no, count(1) AS 'Expr1', grade_code as 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days,((amount + (Service_charge  - (((ot_rate * ot_hours) * bill_service_charge )/100))+uniform+ operational_cost)/ tot_days_present) * month_days as 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) ,2))) AS 'total',  sum((ot_rate * ot_hours)) as 'tool_unit',sum(ot_hours) as 'tool_tackles_rate',max(ot_rate) as 'equmental_handling_percent', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty as 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE',IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 AND " + gst_applicable + "), 0) AS 'equmental_applicable' from " + arrear_flag + "  pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE  " + where;
            }
        }
        else if (client_code.Equals("BAGICTM") || client_code.Equals("DHFL"))
        {
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month  " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + where_state + " GROUP BY pay_billing_unit_rate_history.state_name";
            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0   " + bill_from_to_date + where_state + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + where_state + " GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0    " + bill_from_to_date + where_state + " ";
            }
            else if (unit_code == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + where_state + "  GROUP BY pay_billing_unit_rate_history.state_name";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0 " + bill_from_to_date + where_state + " ";
            }
            if (arrear_next_year != "Select")
            {
                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code',(SUM(ROUND((amount) + (service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours) + (conveyance_amount), 2))) AS 'total', SUM(ROUND(CGST9 , 2)) AS 'CGST', SUM(ROUND(SGST9, 2)) AS 'SGST', SUM(ROUND(IGST18, 2)) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM " + arrear_flag + " pay_billing_unit_rate_history LEFT OUTER JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.emp_code = pay_billing_unit_rate_history.emp_code AND pay_conveyance_amount_history.comp_code = pay_billing_unit_rate_history.comp_code AND pay_conveyance_amount_history.unit_code = pay_billing_unit_rate_history.unit_code AND pay_conveyance_amount_history.month = pay_billing_unit_rate_history.month AND pay_conveyance_amount_history.year = pay_billing_unit_rate_history.year  WHERE  " + where;
                    d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = "+type+" ");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, CASE WHEN pay_billing_unit_rate_history.client_code = 'BAGIC TM' THEN 'BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD' ELSE client END AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1', '' AS 'UNIT_ADD2', unit_city AS 'UNIT_CITY', unit_gst_no, grade_desc AS 'Expr1', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days, CASE WHEN cca_billing = 0 THEN (SUM(sub_total_c) - IF(SUM(ot_hours) > 0, 0, ot_rate)) ELSE (SUM(gross) + ((SUM(gross) * SUM(esic)) / 100) + SUM(pf) + SUM(uniform) - IF(SUM(ot_hours) > 0, 0, ot_rate)) END AS 'grand_total', (SUM(ROUND((amount) + (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) / 100) * bill_service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours) + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days), 2))) AS 'total', SUM(ROUND((IF(CGST9 > 0, (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) + (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) / 100) * bill_service_charge)) / 100) * 9, 0)), 2)) AS 'CGST', SUM(ROUND((IF(SGST9 > 0, (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) + (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) / 100) * bill_service_charge)) / 100) * 9, 0)), 2)) AS 'SGST', SUM(ROUND((IF(IGST18 > 0, (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) + (((amount + (IFNULL(pay_conveyance_amount_history.conveyance_rate,conveyance_amount) * tot_days_present / month_days)) / 100) * bill_service_charge)) / 100) * 18, 0)), 2)) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, grade_code AS 'designation', IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) from pay_billing_unit_rate_history where  billing_gst_applicable = 1  AND " + where + " ),0) as 'hrs_12_ot' from " + arrear_flag + " pay_billing_unit_rate_history left outer JOIN `pay_conveyance_amount_history` ON `pay_conveyance_amount_history`.`emp_code` = `pay_billing_unit_rate_history`.`emp_code` AND `pay_conveyance_amount_history`.`comp_code` = `pay_billing_unit_rate_history`.`comp_code` AND `pay_conveyance_amount_history`.`unit_code` = `pay_billing_unit_rate_history`.`unit_code` AND `pay_conveyance_amount_history`.`month` = `pay_billing_unit_rate_history`.`month` AND `pay_conveyance_amount_history`.`year` = `pay_billing_unit_rate_history`.`year`  WHERE  " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;
            }
            else
            {
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    string invoice = "";
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no,IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code',(SUM(ROUND((amount) + (service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours) + (conveyance_amount), 2))) AS 'total', SUM(ROUND(CGST9 , 2)) AS 'CGST', SUM(ROUND(SGST9, 2)) AS 'SGST', SUM(ROUND(IGST18, 2)) AS 'IGST',start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history LEFT OUTER JOIN pay_conveyance_amount_history ON pay_conveyance_amount_history.emp_code = pay_billing_unit_rate_history.emp_code AND pay_conveyance_amount_history.comp_code = pay_billing_unit_rate_history.comp_code AND pay_conveyance_amount_history.unit_code = pay_billing_unit_rate_history.unit_code AND pay_conveyance_amount_history.month = pay_billing_unit_rate_history.month AND pay_conveyance_amount_history.year = pay_billing_unit_rate_history.year  WHERE  " + where;
                    if (client_code.Equals("DHFL"))
                    {
                        invoice = d.getsinglestring("select invoice_no from pay_billing_unit_rate_history WHERE  " + where);
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + " and invoice_no= '" + invoice + "' ");
                    }
                    else 
                    {
                        d.operation("delete from pay_report_gst WHERE comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = 'manpower' " + bill_from_to_date1 + " ");
                    }
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, CASE WHEN pay_billing_unit_rate_history.client_code = 'BAGIC TM' THEN 'BAJAJ ALLIANZ GENERAL INSURANCE CO. LTD' ELSE client END AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1', invoice_shipping_address AS 'UNIT_ADD2', unit_city AS 'UNIT_CITY', unit_gst_no, grade_desc AS 'Expr1', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days, CASE WHEN cca_billing = 0 THEN (SUM(sub_total_c) - IF(SUM(ot_hours) > 0, 0, ot_rate)) ELSE (SUM(gross) + ((SUM(gross) * SUM(esic)) / 100) + SUM(pf) + SUM(uniform) - IF(SUM(ot_hours) > 0, 0, ot_rate)) END AS 'grand_total', (SUM(ROUND((amount) + (service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours) + (conveyance_amount), 2))) AS 'total', SUM(ROUND(CGST9 , 2)) AS 'CGST', SUM(ROUND(SGST9, 2)) AS 'SGST', SUM(ROUND(IGST18, 2)) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, grade_code AS 'designation', IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) from pay_billing_unit_rate_history where  billing_gst_applicable = 1  AND " + where + " ),0) as 'hrs_12_ot',invoice_shipping_address from " + arrear_flag + " pay_billing_unit_rate_history left outer JOIN `pay_conveyance_amount_history` ON `pay_conveyance_amount_history`.`emp_code` = `pay_billing_unit_rate_history`.`emp_code` AND `pay_conveyance_amount_history`.`comp_code` = `pay_billing_unit_rate_history`.`comp_code` AND `pay_conveyance_amount_history`.`unit_code` = `pay_billing_unit_rate_history`.`unit_code` AND `pay_conveyance_amount_history`.`month` = `pay_billing_unit_rate_history`.`month` AND `pay_conveyance_amount_history`.`year` = `pay_billing_unit_rate_history`.`year` LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE  " + where;
            }
        }
        else
        {
            string gst_applicable2 = "";
            ////if (client_code.Equals("RLIC HK") && int.Parse(month) == 1)
            ////{
            ////    daterange = "'01 JAN 2019 TO 20 JAN 2019' as fromtodate";
            ////}

            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
            where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.unit_code";
            delete_where = " comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'  AND unit_code = '" + unit_code + "' AND month " + month_in + " AND Year  " + year_in + " and type = "+ type +"";
            where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " ";
            gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " ";
            gst_applicable2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "  " + bill_from_to_date + " ";
            if (state_name == "ALL")
            {
                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                delete_where = " comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "'  AND month " + month_in + " AND Year  " + year_in + " and type = " + type + "";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                gst_applicable2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
            }
            else if (unit_code == "ALL")
            {

                where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name,pay_billing_unit_rate_history.grade_desc";
                where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_name";
                delete_where = " comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'   AND month " + month_in + " AND Year  " + year_in + " and type = " + type + "";
                where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "";
                if (client_code.Equals("7"))
                {
                    where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.unit_name,pay_billing_unit_rate_history.grade_desc";
                    where3 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.unit_name";
                    delete_where = " comp_code =  '" + comp_code + "' AND client_code = '" + client_code + "' AND state_name = '" + state_name + "'  AND month " + month_in + " AND Year  " + year_in + " and type = " + type + "";
                    where2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                }
                gst_applicable = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
                gst_applicable2 = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.tot_days_present > 0  " + club_invoice + "    " + bill_from_to_date + " ";
            }
            if (arrear_next_year != "Select")
            {

                //For Report Table arrears
                if (invoice_arrear == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no, IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'arrears_manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST',unit_code,start_date,end_date,'"+gst_to_be+"' FROM  " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE " + delete_where +" " + bill_from_to_date1 + "");
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,unit_code,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same

                return "SELECT   pay_billing_unit_rate_history.client_code, comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1', " + UNIT_ADD2 + ", unit_city AS 'UNIT_CITY', unit_gst_no, count(1) AS 'Expr1', grade_code as 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days,((amount + (Service_charge  - (((ot_rate * ot_hours) * bill_service_charge )/100))+uniform+ operational_cost)/ tot_days_present) * month_days as 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours),2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty as 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE',IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in + " AND " + gst_applicable2 + " ), 0) AS 'equmental_applicable' from " + arrear_flag + " pay_billing_unit_rate_history WHERE  " + where2 + " and pay_billing_unit_rate_history.month " + month_in + " and pay_billing_unit_rate_history.Year " + year_in;

            }
            else
            {
                //// rahul gstr3b
                //For Report Table
                if (invoice_fl_man == 1)
                {
                    sql = "SELECT pay_billing_unit_rate_history.comp_code, pay_billing_unit_rate_history.client_code, client, state_name, billing_date, auto_invoice_no, unit_gst_no, IF(grade_code = 'SG', Security_sac_code, housekeeiing_sac_code) AS 'sac_code', COUNT(pay_billing_unit_rate_history.emp_code) AS 'emp_count', pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year, 'manpower' AS 'comp_code', (SUM(ROUND((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours), 2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST',unit_code,start_date,end_date,'"+gst_to_be+"' FROM pay_billing_unit_rate_history WHERE  " + where3;
                    d.operation("delete from pay_report_gst WHERE "+delete_where);
                    d.operation("INSERT INTO pay_report_gst (comp_code,client_code,client_name,state_name,invoice_date, invoice_no, gst_no,sac_code,emp_count, month, year,type, Amount, cgst, sgst, igst,unit_code,start_date,end_date,gst_to_be)" + sql);
                }
                //if you change sub_total,cgst,sgst,igst in invoice select query then also same changes in report select query same
                return "SELECT   pay_billing_unit_rate_history.client_code, pay_billing_unit_rate_history.comp_code, client AS 'other', COMPANY_NAME, COMP_ADDRESS1 AS 'ADDRESS1', COMP_ADDRESS2 AS 'ADDRESS2', COMP_CITY AS 'CITY', COMP_STATE AS 'STATE', PF_REG_NO, COMPANY_PAN_NO, COMPANY_TAN_NO, COMPANY_CIN_NO, SERVICE_TAX_REG_NO, ESIC_REG_NO, state_name AS 'STATE_NAME', UNIT_full_ADD1 AS 'UNIT_ADD1',  invoice_shipping_address AS 'UNIT_ADD2', unit_city AS 'UNIT_CITY', unit_gst_no, count(1) AS 'Expr1', grade_code as 'designation', fromtodate AS 'start_end_date', SUM(tot_days_present) AS 'TOT_DAYS_PRESENT', month_days,((amount + (Service_charge  - (((ot_rate * ot_hours) * bill_service_charge )/100))+uniform+ operational_cost)/ tot_days_present) * month_days as 'grand_total', (sum(round((amount) + (Service_charge) + (uniform) + (operational_cost) + (ot_rate * ot_hours),2))) AS 'total', IFNULL(SUM(SGST9), 0) AS 'SGST', IFNULL(SUM(CGST9), 0) AS 'CGST', IFNULL(SUM(IGST18), 0) AS 'IGST', " + invoice_month_name + " AS 'month', housekeeiing_sac_code, Security_sac_code, unit_code, penalty as 'hrs_12_ot', CAST(gst_applicable AS char) AS 'ZONE',IFNULL((SELECT (SUM(Amount) + SUM(Service_charge) + SUM(uniform) + SUM(operational_cost) + SUM(ot_rate * ot_hours)) FROM pay_billing_unit_rate_history WHERE billing_gst_applicable = 1 AND " + gst_applicable + "), 0) AS 'equmental_applicable' from " + arrear_flag + " pay_billing_unit_rate_history LEFT JOIN pay_client_billing_details ON pay_client_billing_details.comp_code = pay_billing_unit_rate_history.comp_code AND pay_client_billing_details.client_code = pay_billing_unit_rate_history.client_code AND pay_client_billing_details.STATE = pay_billing_unit_rate_history.state_name AND billing_name = 'Manpower Billing' WHERE  " + where;
            }
        }

    }

    public bool chk_days_count(string comp_code, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year)
    {
        string count_days = "", emp_count = "", designation_where = "", designation = "";


        string where = "", club_invoice = "";
        string month = txt_month_year.Substring(0, 2);
        string year = txt_month_year.Substring(3);



        string days = d.getsinglestring("select ifnull(month_days,0) from pay_billing_unit_rate  where comp_code = '" + comp_code + "' and client_code = '" + client_code + "'  and month = '" + month + "' and year = '" + year + "'  limit 1");
        days = days == "" ? "0" : days;
        if (invoice_type == "2")
        {
            club_invoice = "and GRADE_CODE = '" + grade_code + "'";
            designation = d.getsinglestring("select GRADE_DESC from pay_grade_master where comp_code = '" + comp_code + "' and grade_code = '" + grade_code + "'");
            designation = "and DESIGNATION = '" + designation + "'";
        }

        designation_where = "comp_code = '" + comp_code + "' and unit_code = '" + unit_code + "' " + designation + " and  (unit_code != ''  || unit_code is not null)";
        where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' " + club_invoice + " and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' and pay_billing_unit_rate_history.tot_days_present > 0   GROUP BY pay_billing_unit_rate_history.unit_code";
        if (state_name == "ALL")
        {
            designation_where = "comp_code = '" + comp_code + "'  and client_code = '" + client_code + "'  " + designation + " and  (unit_code != ''  || unit_code is not null)";
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "'  " + club_invoice + " and pay_billing_unit_rate_history.tot_days_present > 0  GROUP BY pay_billing_unit_rate_history.client_code";
        }
        else if (unit_code == "ALL")
        {
            designation_where = "comp_code = '" + comp_code + "'  and client_code = '" + client_code + "' and state = '" + state_name + "'  " + designation + " and  (unit_code != ''  || unit_code is not null)";
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "'  " + club_invoice + " and pay_billing_unit_rate_history.tot_days_present > 0   GROUP BY pay_billing_unit_rate_history.state_name";
        }

        count_days = d.getsinglestring("Select sum(tot_days_present) from pay_billing_unit_rate_history where " + where);
        count_days = count_days == "" ? "0" : count_days;
        emp_count = d.getsinglestring("select ifnull(sum(count),0) from pay_designation_count where   " + designation_where);


        emp_count = ((double.Parse(emp_count) * double.Parse(days))).ToString();

        // if (double.Parse(count_days) < double.Parse(emp_count)) { return false; } else { return true; }
        return double.Parse(count_days) < double.Parse(emp_count) ? false : true;
    }

    public void insert_arrears_data(string comp_code, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year)
    {


        string where = "", where1 = "", club_invoice = "", delete_where = "", group_by = "group by pay_billing_unit_rate_history.emp_code";
        string month = txt_month_year.Substring(0, 2);
        string year = txt_month_year.Substring(3);
        string sql = "", sql1 = "";
        string where_att = "", where_billing = "";



        if (invoice_type == "2")
        {
            club_invoice = "and GRADE_CODE = '" + grade_code + "'";
        }

        where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' " + club_invoice + " and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' and pay_billing_unit_rate_history.tot_days_present > 0     AND (pay_attendance_muster.TOT_DAYS_PRESENT - pay_billing_unit_rate_history.TOT_DAYS_PRESENT) != 0  ";
        where_att = "a.comp_code = '" + comp_code + "' AND a.unit_code='" + unit_code + "' AND a.month = '" + month + "' AND a.year = '" + year + "' ";
        where_billing = "comp_code = '" + comp_code + "' AND unit_code='" + unit_code + "' AND month = '" + month + "' AND year = '" + year + "' ";
        delete_where = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "' and unit_code = '" + unit_code + "' " + club_invoice + " and month = '" + month + "' and Year = '" + year + "'";
        if (state_name == "ALL")
        {
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "'  " + club_invoice + " and pay_billing_unit_rate_history.tot_days_present > 0    AND (pay_attendance_muster.TOT_DAYS_PRESENT - pay_billing_unit_rate_history.TOT_DAYS_PRESENT) != 0 ";
            delete_where = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "'  " + club_invoice + " and month = '" + month + "' and Year = '" + year + "'";
            where_att = "a.comp_code = '" + comp_code + "' AND e.client_code='" + client_code + "' AND a.month = '" + month + "' AND a.year = '" + year + "' ";
            where_billing = "comp_code = '" + comp_code + "' AND client_code='" + client_code + "' AND month = '" + month + "' AND year = '" + year + "' ";
        }
        else if (unit_code == "ALL")
        {
            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "'  " + club_invoice + " and pay_billing_unit_rate_history.tot_days_present > 0     AND (pay_attendance_muster.TOT_DAYS_PRESENT - pay_billing_unit_rate_history.TOT_DAYS_PRESENT) != 0  ";
            delete_where = "comp_code = '" + comp_code + "' and client_code = '" + client_code + "' and state_name = '" + state_name + "'  " + club_invoice + " and month = '" + month + "' and Year = '" + year + "'";
            where_att = "a.comp_code = '" + comp_code + "' AND e.client_code='" + client_code + "'AND e.client_wise_state='" + state_name + "' AND a.month = '" + month + "' AND a.year = '" + year + "' ";
            where_billing = "comp_code = '" + comp_code + "' AND client_code='" + client_code + "'AND state_name='" + state_name + "' AND month = '" + month + "' AND year = '" + year + "' ";
        }
        try
        {
            d.con1.Open();
            sql = "SELECT  pay_billing_unit_rate_history.comp_code,pay_billing_unit_rate_history.client_code,  pay_billing_unit_rate_history.unit_code,  pay_billing_unit_rate_history.state_name,  pay_billing_unit_rate_history.emp_code,  pay_billing_unit_rate_history.emp_name,  pay_billing_unit_rate_history.emp_type,  pay_billing_unit_rate_history.grade_desc,pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year,  (pay_attendance_muster.TOT_DAYS_PRESENT - sum(pay_billing_unit_rate_history.TOT_DAYS_PRESENT)) AS 'present_attendance' ,pay_attendance_muster.TOT_WORKING_DAYS,'1' as 'flag' FROM  pay_attendance_muster  INNER JOIN pay_billing_unit_rate_history  ON  pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_Code  AND pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code  AND pay_attendance_muster.month = pay_billing_unit_rate_history.month  AND pay_attendance_muster.year = pay_billing_unit_rate_history.year  AND pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code  WHERE  " + where + "  " + group_by;
            sql1 = "SELECT  pay_billing_unit_rate_history.comp_code,pay_billing_unit_rate_history.client_code,  pay_billing_unit_rate_history.unit_code,  pay_billing_unit_rate_history.state_name,  pay_billing_unit_rate_history.emp_code,  pay_billing_unit_rate_history.emp_name,  pay_billing_unit_rate_history.emp_type,  pay_billing_unit_rate_history.grade_desc,pay_billing_unit_rate_history.month, pay_billing_unit_rate_history.year,  (pay_attendance_muster.TOT_DAYS_PRESENT - sum(pay_billing_unit_rate_history.TOT_DAYS_PRESENT)) AS 'present_attendance' ,pay_attendance_muster.TOT_WORKING_DAYS,'1' as 'flag' FROM  pay_attendance_muster  INNER JOIN pay_billing_unit_rate_history  ON  pay_attendance_muster.comp_code = pay_billing_unit_rate_history.comp_Code  AND pay_attendance_muster.unit_code = pay_billing_unit_rate_history.unit_code  AND pay_attendance_muster.month = pay_billing_unit_rate_history.month  AND pay_attendance_muster.year = pay_billing_unit_rate_history.year  AND pay_attendance_muster.emp_code = pay_billing_unit_rate_history.emp_code  WHERE  " + where;

            MySqlCommand cmd = new MySqlCommand(sql, d.con1);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.GetValue(10).ToString() != "0")
                {

                    d.operation("delete from pay_arrears_attendance_muster  where  " + delete_where + " and emp_code = '" + dr.GetValue(4).ToString() + "' ");
                    d.operation(" insert into pay_arrears_attendance_muster(comp_code,client_code,unit_code,state_name,emp_code,emp_name,emp_type,grade_desc,month,year,TOT_DAYS_PRESENT,TOT_WORKING_DAYS,flag) " + sql1 + " and pay_billing_unit_rate_history.emp_code = '" + dr.GetValue(4).ToString() + "' " + group_by);
                }
            }

            d.operation(" insert into pay_arrears_attendance_muster(comp_code,client_code,unit_code,state_name,emp_code,emp_name,emp_type,grade_desc,month,year,TOT_DAYS_PRESENT,ot_hours,TOT_WORKING_DAYS,flag) (SELECT e.comp_code, e.client_code, e.unit_code, client_wise_state AS 'state_name', e.emp_code, e.emp_name, e.GRADE_CODE, e.Employee_type, a.month, a.year, a.tot_days_present, a.ot_hours,a.TOT_WORKING_DAYS, 1 as 'flag' FROM pay_attendance_muster as  a INNER JOIN pay_employee_master as e ON a.comp_code = e.comp_code AND a.unit_code = e.unit_code AND a.emp_code = e.emp_code WHERE " + where_att + " AND a.emp_code NOT IN (SELECT distinct emp_code FROM pay_billing_unit_rate_history WHERE " + where_billing + ") )");
        }
        catch (Exception ex)
        {
            d.con.Close();
            throw ex;

        }
        finally
        {
            d.con.Close();
        }


    }

    //public bool check_arrears_approve(string comp_code, string client_code, string state_name, string unit_code, string invoice_type, string grade_code, string txt_month_year) {
    //    string where = "";
    //    try {
    //        where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "'  and pay_billing_unit_rate_history.unit_code = '" + unit_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND ( EMP_CODE  IS NULL OR  EMP_CODE  = '')    " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_per";
    //        if (state_name == "ALL")
    //        {
    //            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND ( EMP_CODE  IS NULL OR  EMP_CODE  = '')    " + bill_from_to_date + " GROUP BY pay_billing_unit_rate_history.state_per";
    //        }
    //        else if (unit_code == "ALL")
    //        {
    //            where = "pay_billing_unit_rate_history.comp_code = '" + comp_code + "' and pay_billing_unit_rate_history.client_code = '" + client_code + "' and pay_billing_unit_rate_history.state_name = '" + state_name + "' and pay_billing_unit_rate_history.month = '" + month + "' and pay_billing_unit_rate_history.Year = '" + year + "' AND ( EMP_CODE  IS NULL OR  EMP_CODE  = '')   " + bill_from_to_date + "  GROUP BY pay_billing_unit_rate_history.state_per";
    //        }
    //    }
    //    catch()
    //}
}