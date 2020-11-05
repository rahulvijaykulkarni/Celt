using System;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

public partial class InvestmentDeclaration : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d1.getaccess(Session["ROLE"].ToString(), "Investment Declaration", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Investment Declaration", Session["COMP_CODE"].ToString()) == "R")
        {
            //btndelete.Visible = false;
            //btnupdate.Visible = false;
            //btnadd.Visible = false;
            //btnnew.Visible = false;
            //btnexporttoexcel.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Investment Declaration", Session["COMP_CODE"].ToString()) == "U")
        {
            //btndelete.Visible = false;
            //btnadd.Visible = false;
            //btnnew.Visible = false;
            //btnexporttoexcel.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Investment Declaration", Session["COMP_CODE"].ToString()) == "C")
        {
            //btndelete.Visible = false;
            //btnexporttoexcel.Visible = false;
        }
        if (!IsPostBack)
        {
           
            policy_fill();
            btn_submit.Visible = false;
            panel2.Visible = true;
            btndelete.Visible = false;
            text_clear();
        }
        txt_app_escalation_level.Visible = false;
        txt_not_approved_emailid.Visible = false;
        chk_app_cancel_if_approved.Visible = false;
        chk_app_cancel_if_ticket_confirmed.Visible = false;
        txt_cancellation_days.Visible = false;
        txt_exception_case_approval_level.Visible = false;
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        String flag1;
        if (chkair.Checked == true)
        {
            flag1 = "1";
        }
        else
        {
            flag1 = "0";
        }
        String flag2;
        string where;
        if (chkbus.Checked == true)
        {
            flag2 = "1";


        }
        else
        {
            flag2 = "0";
        }
        String flag3;
        if (chktraint1.Checked == true)
        {
            flag3 = "1";
        }
        else
        {
            flag3 = "0";
        }
        String flag4;
        if (chktraint2.Checked == true)
        {
            flag4 = "1";
        }
        else
        {
            flag4 = "0";
        }
        String flag5;
        if (chktraint3.Checked == true)
        {
            flag5 = "1";
        }
        else
        {
            flag5 = "0";
        }
        String flag6;
        if (chkcabtaxi.Checked == true)
        {
            flag6 = "1";
        }
        else
        {
            flag6 = "0";
        }
        String flag7;
        if (chkauto.Checked == true)
        {
            flag7 = "1";
        }
        else
        {
            flag7 = "0";
        }
        String flag8;
        string where1;
        if (chkownedvehicle.Checked == true)
        {
            flag8 = "1";

        }
        else
        {
            flag8 = "0";
        }
        String flag9;
        if (chklocalconveyance.Checked == true)
        {
            flag9 = "1";
        }
        else
        {
            flag9 = "0";
        }
        //String flag10;
        //if (chk_auto_approval.Checked == true)
        //{
        //    flag10 = "1";
        //}
        //else
        //{
        //    flag10 = "0";
        //}
        //String flag11;
        //if (chk_escalation_approval.Checked == true)
        //{
        //    flag11 = "1";
        //}
        //else
        //{
        //    flag11 = "0";
        //}
        String flag12;
        if (chk_app_cancel_if_approved.Checked == true)
        {
            flag12 = "1";
        }
        else
        {
            flag12 = "0";
        }
        String flag13;
        if (chk_app_cancel_if_ticket_confirmed.Checked == true)
        {
            flag13 = "1";
        }
        else
        {
            flag13 = "0";
        }
        String flag14;
        if (chk_hotel.Checked == true)
        {
            flag14 = "1";

        }
        else
        {
            flag14 = "0";
        }
        //String flag15;
        //if (chk_laundry.Checked == true)
        //{
        //    flag15 = "1";
        //}
        //else
        //{
        //    flag15 = "0";
        //}
        String flag16;
        if (chk_expenses_allowed.Checked == true)
        {
            flag16 = "1";
        }
        else
        {
            flag16 = "0";
        }
        //String flag17;
        //if (chk_femail_upgrade.Checked == true)
        //{
        //    flag17 = "1";
        //}
        //else
        //{
        //    flag17 = "0";
        //}
        //String flag18;
        //if (chk_payment_air.Checked == true)
        //{
        //    flag18 = "1";
        //}
        //else
        //{
        //    flag18 = "0";
        //}
        String flag19;
        //if (chk_payment_bus.Checked == true)
        //{
        //    flag19 = "1";
        //}
        //else
        //{
        //    flag19 = "0";
        //}
        //String flag20;
        //if (chk_payment_train.Checked == true)
        //{
        //    flag20 = "1";
        //}
        //else
        //{
        //    flag20 = "0";
        //}
        //String flag21;
        //if (chk_payment_taxi.Checked == true)
        //{
        //    flag21 = "1";
        //}
        //else
        //{
        //    flag21 = "0";
        //}

        //String flag22;
        //if (chk_payment_hotel.Checked == true)
        //{
        //    flag22 = "1";
        //}
        //else
        //{
        //    flag22 = "0";
        //}
        string flag23;
        if (hotel_budget.Checked == true)
        {
            flag23 = "1";
        }
        else
        {
            flag23 = "0";
        }
        string flag24;
        if (chk_fooding.Checked == true)
        {
            flag24 = "1";
        }
        else
        {
            flag24 = "0";
        }
        string flag25;
        if (CheckBox_ac.Checked == true)
        {
            flag25 = "1";
        }
        else
        {
            flag25 = "0";
        }
        string flag26;
        if (CheckBox_nonac.Checked == true)
        {
            flag26 = "1";
        }
        else
        {
            flag26 = "0";
        }
        string flag27;
        if (CheckBox_citybus.Checked == true)
        {
            flag27 = "1";
        }
        else
        {
            flag27 = "0";
        }

        string flag28;
        if (CheckBox_car.Checked == true)
        {
            flag28 = "1";
        }
        else
        {
            flag28 = "0";
        }
        string flag29;
        if (CheckBox_bike.Checked == true)
        {
            flag29 = "1";
        }
        else
        {
            flag29 = "0";
        }
        string flag30;
        if (hotel_budget.Checked == true)
        {
            flag30 = "1";
        }
        else
        {
            flag30 = "0";
        }
        string flag31;
        if (hotel_standard.Checked == true)
        {
            flag31 = "1";
        }
        else
        {
            flag31 = "0";
        }
        string flag32;
        if (hotel_twostar.Checked == true)
        {
            flag32 = "1";
        }
        else
        {
            flag32 = "0";
        }
        string flag33;
        if (hotel_threestar.Checked == true)
        {
            flag33 = "1";
        }
        else
        {
            flag33 = "0";
        }
        string flag34;
        if (hotel_fivestar.Checked == true)
        {
            flag34 = "1";
        }
        else
        {
            flag34 = "0";
        }
        string flag35;
        if (CheckBox_breakfast.Checked == true)
        {
            flag35 = "1";
        }
        else
        {
            flag35 = "0";
        }
        string flag36;
        if (CheckBox_lunch.Checked == true)
        {
            flag36 = "1";
        }
        else
        {
            flag36 = "0";
        }
        string flag37;
        if (CheckBox_dinner.Checked == true)
        {
            flag37 = "1";
        }
        else
        {
            flag37 = "0";
        }
        string flag38;
        if (CheckBox_inside.Checked == true)
        {
            flag38 = "1";
        }
        else
        {
            flag38 = "0";
        }
        string flag39;
        if (CheckBox_outside.Checked == true)
        {
            flag39 = "1";
        }
        else
        {
            flag39 = "0";
        }
        if (ddl_Existing_policy_name.SelectedIndex.ToString() == "0")
        {

            int i = d1.operation("insert into pay_travel_policy_master (comp_code,txt_policy_name,txt_start_date,txt_end_date,chkair,chkbus,`CheckBox_ac`,`CheckBox_nonac`,`CheckBox_citybus`,`AC`,`NonAc`,`CityBus`, `chktraint1`, `chktraint2`, `chktraint3`, `chkcabtaxi`, `chkauto`,  chkownedvehicle,CheckBox_car,`Car`,CheckBox_bike,`Bike`,chklocalconveyance,txt_localconveyancelimit, txt_approval_level,txt_approval_days_before,txt_app_escalation_level,txt_not_approved_emailid,chk_app_cancel_if_approved,  chk_app_cancel_if_ticket_confirmed,txt_cancellation_days,txt_exception_case_approval_level,chk_hotel,hotel_budget,hotel_budget_from,`hotel_budget_to`,  hotel_standard,hotel_standard_from,hotel_standard_to,hotel_twostar, hotel_twostar_from, hotel_twostar_to,hotel_threestar, hotel_threestar_from, hotel_threestar_to,hotel_fivestar, hotel_fivestar_from, hotel_fivestar_to,chk_fooding,CheckBox_breakfast,breakfast,CheckBox_lunch,lunch,CheckBox_dinner,dinner, chk_expenses_allowed,CheckBox_inside,CheckBox_outside,txt_per_day_limit, txt_claim_max_days, Payment_process_days, Textbox_outside,`created_by`, `created_date`, `modified_by`, `modified_time`, `unit_code`) values ('" + Session["comp_code"].ToString() + "','" + txt_policy_name1.Text + "', str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'), case when " + txt_end_date.Text.Length + "=0 then null else str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') END, '" + flag1 + "', '" + flag2 + "','" + flag25 + "','" + flag26 + "','" + flag27 + "','" + TextBox_ac.Text + "','" + TextBox_nonac.Text + "','" + TextBox_citybus.Text + "','" + flag3 + "','" + flag4 + "','" + flag5 + "','" + flag6 + "','" + flag7 + "','" + flag8 + "','" + flag28 + "','" + txtownedvehiclekms.Text + "','" + flag29 + "','" + txtownedvehiclekms2.Text + "','" + flag9 + "','" + txt_localconveyancelimit.Text + "','" + txt_approval_level.Text + "','" + txt_approval_days_before.Text + "','" + txt_app_escalation_level.Text + "','" + txt_not_approved_emailid.Text + "','" + flag12 + "','" + flag13 + "','" + txt_cancellation_days.Text + "','" + txt_exception_case_approval_level.Text + "','" + flag14 + "','" + flag30 + "','" + text_hotel_budget.Text + "','" + text_hotel_budget2.Text + "','" + flag31 + "','" + text_hotel_standard.Text + "','" + text_hotel_standard2.Text + "','" + flag32 + "','" + text_hotel_twostar.Text + "','" + text_hotel_twostar2.Text + "','" + flag33 + "','" + text_hotel_threestar.Text + "','" + text_hotel_threestar2.Text + "','" + flag34 + "','" + text_hotel_fivestar.Text + "','" + text_hotel_fivestar2.Text + "','" + flag24 + "','" + flag35 + "','" + TextBox_breakfast.Text + "','" + flag36 + "','" + TextBox_lunch.Text + "','" + flag37 + "','" + TextBox_dinner.Text + "','" + flag16 + "','" + flag38 + "','" + flag39 + "','" + txt_per_day_limit.Text + "','" + txt_claim_max_days.Text + "','" + text_payment_process.Text + "','" + Textbox_outside.Text + "', created_by ,now(),modified_by,now(),'" + Session["UNIT_CODE"].ToString() + "')");
            //int i = d1.operation("insert into pay_travel_policy_master (comp_code,txt_policy_name,txt_start_date,txt_end_date,chkair,chkbus,chktraint1,chktraint2,chktraint3,chkcabtaxi,chkauto,chkownedvehicle,txtownedvehiclekms,chklocalconveyance,txt_approval_level,txt_approval_days_before,chk_auto_approval,txt_app_escalation_level,txt_not_approved_emailid,chk_app_cancel_if_approved,chk_app_cancel_if_ticket_confirmed,txt_cancellation_days,txt_exception_case_approval_level,chk_hotel,chk_expenses_allowed,txt_per_day_limit,txt_claim_max_days,created_by,created_date,modified_by,modified_time,unit_code,Payment_process_days,Ac_bus,Nonac_bus,City_bus,v_car,v_bike,hotel_budget_from,hotel_standard_from,hotel_standard_to, hotel_twostar_from, hotel_twostar_to, hotel_threestar_from, hotel_threestar_to, hotel_fivestar_from, hotel_fivestar_to,breakfast,lunch,dinner,CheckBox_ac,CheckBox_nonac,CheckBox_citybus,CheckBox_car,CheckBox_bike,hotel_budget,hotel_standard,hotel_twostar,hotel_threestar,hotel_fivestar,CheckBox_breakfast,CheckBox_lunch,CheckBox_dinner,CheckBox_inside,CheckBox_outside) values ('" + Session["comp_code"].ToString() + "','" + txt_policy_name1.Text + "', str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'), case when " + txt_end_date.Text.Length + "=0 then null else str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') END, '" + flag1 + "', '" + flag2 + "', '" + flag3 + "','" + flag4 + "', '" + flag5 + "', '" + flag6 + "', '" + flag7 + "','" + flag8 + "', '" + txtownedvehiclekms.Text + "', '" + flag9 + "', '" + txt_localconveyancelimit.Text + "', '" + txt_approval_level.Text + "', '" + txt_approval_days_before.Text + "', '" + txt_app_escalation_level.Text + "', '" + txt_not_approved_emailid.Text + "', '" + flag12 + "', '" + flag13 + "', '" + txt_cancellation_days.Text + "', '" + txt_exception_case_approval_level.Text + "', '" + flag14 + "','" + flag16 + "', '" + txt_per_day_limit.Text + "','" + txt_claim_max_days.Text + "', created_by ,now(),modified_by,now(),'" + Session["UNIT_CODE"].ToString() + "','" + text_payment_process.Text + "','" + TextBox_ac.Text + "','" + CheckBox_nonac.Text + "','" + CheckBox_citybus.Text + "','" + txtownedvehiclekms.Text + "','" + txtownedvehiclekms2.Text + "','" + text_hotel_budget.Text + "','" + text_hotel_standard.Text + "','" + text_hotel_standard2.Text + "','" + text_hotel_twostar.Text + "','" + text_hotel_twostar2.Text + "','" + text_hotel_threestar.Text + "','" + text_hotel_threestar2.Text + "','" + text_hotel_fivestar.Text + "','" + text_hotel_fivestar2.Text + "','" + TextBox_breakfast.Text + "','" + TextBox_lunch.Text + "','" + TextBox_dinner.Text + "','" + flag25 + "','" + flag26 + "','" + flag27 + "','" + flag28 + "','" + flag29 + "','" + flag30 + "','" + flag31 + "','" + flag32 + "','" + flag33 + "','" + flag34 + "','" + flag35 + "','" + flag36 + "','" + flag37 + "','" + flag38 + "','" + flag39 + "')");
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Policy Saved Successfully...');", true);
            }

            policy_fill();
        }
        else
        {
            d.con.Open();
            try
            {
                MySqlCommand cmd1 = new MySqlCommand("Select * from pay_travel_emp_policy Where policy_id in (Select Id from pay_travel_policy_master Where txt_policy_name ='" + ddl_Existing_policy_name.SelectedItem.ToString() + "')", d.con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Policy already assigned you cannot make any Changes......');", true);

                }
                else
                {
                    int i = d.operation("UPDATE pay_travel_policy_master SET txt_policy_name='" + txt_policy_name1.Text + "' ,txt_start_date =str_to_date('" + txt_start_date.Text.ToString() + "','%d/%m/%Y'),txt_end_date =case when " + txt_end_date.Text.Length + "=0 then null else str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') END,chkair='" + flag1 + "',chkbus='" + flag2 + "',chktraint1 ='" + flag3 + "',chktraint2='" + flag4 + "',chktraint3='" + flag5 + "',chkcabtaxi='" + flag6 + "',chkauto='" + flag7 + "',chkownedvehicle='" + flag8 + "',txtownedvehiclekms='" + txtownedvehiclekms.Text + "',chklocalconveyance='" + flag9 + "',txt_localconveyancelimit='" + txt_localconveyancelimit.Text + "',txt_approval_level='" + txt_approval_level.Text + "',txt_approval_days_before='" + txt_approval_days_before.Text + "',chk_auto_approval='" + "',txt_app_escalation_level='" + txt_app_escalation_level.Text + "',txt_not_approved_emailid='" + txt_not_approved_emailid.Text + "',chk_app_cancel_if_approved='" + flag12 + "',chk_app_cancel_if_ticket_confirmed='" + flag13 + "',txt_cancellation_days='" + txt_cancellation_days.Text + "',txt_exception_case_approval_level='" + txt_exception_case_approval_level.Text + "',chk_hotel='" + flag14 + "',chk_expenses_allowed='" + flag16 + "',txt_per_day_limit='" + txt_per_day_limit.Text + "',chk_femail_upgrade='" + "',txt_claim_max_days='" + txt_claim_max_days.Text + "',chk_payment_air='" + "',AC = '" + TextBox_ac.Text + "',NonAc = '" + TextBox_nonac.Text + "',CityBus = '" + TextBox_citybus.Text + "',Car='" + txtownedvehiclekms.Text + "',Bike='" + txtownedvehiclekms2.Text + "',hotel_budget_from='" + text_hotel_budget.Text + "',hotel_standard_from='" + text_hotel_standard.Text + "',hotel_standard_to='" + text_hotel_standard2.Text + "', hotel_twostar_from='" + text_hotel_twostar.Text + "', hotel_twostar_to='" + text_hotel_twostar2.Text + "', hotel_threestar_from='" + text_hotel_threestar.Text + "', hotel_threestar_to='" + text_hotel_threestar2.Text + "', hotel_fivestar_from='" + text_hotel_fivestar.Text + "', hotel_fivestar_to='" + text_hotel_fivestar2.Text + "',chk_fooding='" + flag24 + "',breakfast='" + TextBox_breakfast.Text + "',lunch='" + TextBox_lunch.Text + "',dinner='" + TextBox_dinner.Text + "',CheckBox_ac='" + flag25 + "',CheckBox_nonac='" + flag26 + "',CheckBox_citybus='" + flag27 + "',CheckBox_car='" + flag28 + "',CheckBox_bike='" + flag29 + "',hotel_budget='" + flag30 + "',hotel_standard='" + flag31 + "',hotel_twostar='" + flag32 + "',hotel_threestar='" + flag33 + "',hotel_fivestar='" + flag34 + "',CheckBox_breakfast='" + flag35 + "',CheckBox_lunch='" + flag36 + "',CheckBox_dinner='" + flag37 + "',CheckBox_inside='" + flag38 + "',CheckBox_outside='" + flag39 + "',Textbox_outside='" + Textbox_outside.Text + "' ,hotel_budget_to='" + text_hotel_budget2.Text + "' WHERE  Id=" + ddl_Existing_policy_name.SelectedValue + "");
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Policy Update Successfully...');", true);
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
                policy_fill();
            }
        }
        text_clear();

    }

    public void text_clear()
    {
        txt_policy_name1.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
        if (chkair != null)
            chkair.Checked = false;
        if (chkbus != null)
            chkbus.Checked = false;
        chkbus.Text = "";
        if (chktraint1 != null)
            chktraint1.Checked = false;
        if (chktraint2 != null)
            chktraint2.Checked = false;
        chktraint2.Text = "";
        if (chktraint3 != null)
            chktraint3.Checked = false;
        if (chkcabtaxi != null)
            chkcabtaxi.Checked = false;
        if (chkauto != null)
            chkauto.Checked = false;
        if (chkownedvehicle != null)
            chkownedvehicle.Checked = false;
        txtownedvehiclekms.Text = "";
        if (chklocalconveyance != null)
            chklocalconveyance.Checked = false;

        txt_localconveyancelimit.Text = "";
        txt_approval_level.Text = "";
        txt_approval_days_before.Text = "";
        //if (chk_auto_approval != null)
        //    chk_auto_approval.Checked = false;
        //txt_app_days_before_travel.Text = "";
        //if (chk_escalation_approval != null)
        //    chk_escalation_approval.Checked = false;
        //txt_app_escalation_approcal.Text = "";
        txt_app_escalation_level.Text = "";
        txt_not_approved_emailid.Text = "";
        if (chk_app_cancel_if_approved != null)
            chk_app_cancel_if_approved.Checked = false;
        if (chk_app_cancel_if_ticket_confirmed != null)
            chk_app_cancel_if_ticket_confirmed.Checked = false;
        txt_cancellation_days.Text = "";
        txt_exception_case_approval_level.Text = "";
        if (chk_hotel != null)
            chk_hotel.Checked = false;
        //if (chk_laundry != null)
        //    chk_laundry.Checked = false;
        if (chk_expenses_allowed != null)
            chk_expenses_allowed.Checked = false;
        txt_per_day_limit.Text = "";
        Textbox_outside.Text = "";
        //if (chk_femail_upgrade != null)
        //    chk_femail_upgrade.Checked = false;
        //txt_female_percent.Text = "";
        txt_claim_max_days.Text = "";
        TextBox_ac.Text = "";
        TextBox_nonac.Text = "";
        TextBox_citybus.Text = "";
        txtownedvehiclekms.Text = "";
        txtownedvehiclekms2.Text = "";
        text_hotel_budget.Text = "";
        text_hotel_budget2.Text = "";
        text_hotel_standard.Text = "";
        text_hotel_standard2.Text = "";
        text_hotel_twostar.Text = "";
        text_hotel_twostar2.Text = "";
        text_hotel_threestar.Text = "";
        text_hotel_threestar2.Text = "";
        text_hotel_fivestar.Text = "";
        text_hotel_fivestar2.Text = "";
        TextBox_breakfast.Text = "";
        TextBox_lunch.Text = "";
        TextBox_dinner.Text = "";
        if (CheckBox_ac != null)
            CheckBox_ac.Checked = false;
        if (CheckBox_nonac != null)
            CheckBox_nonac.Checked = false;
        if (CheckBox_citybus != null)
            CheckBox_citybus.Checked = false;
        if (CheckBox_car != null)
            CheckBox_car.Checked = false;
        if (CheckBox_bike != null)
            CheckBox_bike.Checked = false;
        if (hotel_budget != null)
            hotel_budget.Checked = false;
        if (hotel_standard != null)
            hotel_standard.Checked = false;
        if (hotel_twostar != null)
            hotel_twostar.Checked = false;
        if (hotel_threestar != null)
            hotel_threestar.Checked = false;
        if (hotel_fivestar != null)
            hotel_fivestar.Checked = false;
        if (CheckBox_breakfast != null)
            CheckBox_breakfast.Checked = false;
        if (CheckBox_lunch != null)
            CheckBox_lunch.Checked = false;
        if (CheckBox_dinner != null)
            CheckBox_dinner.Checked = false;
        if (CheckBox_inside != null)
            CheckBox_inside.Checked = false;
        if (CheckBox_outside != null)
            CheckBox_outside.Checked = false;
        if (chk_fooding != null)
            chk_fooding.Checked = false;

   

    }

    protected void btncloselow_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        if (ddl_Existing_policy_name.SelectedValue == "Select Policy")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Policy');", true);
            return;
        }
       
        String flag1;
        if (chkair.Checked == true)
        {
            flag1 = "1";
        }
        else
        {
            flag1 = "0";
        }
        String flag2;
        if (chkbus.Checked == true)
        {
            flag2 = "1";
        }
        else
        {
            flag2 = "0";
        }
        String flag3;
        if (chktraint1.Checked == true)
        {
            flag3 = "1";
        }
        else
        {
            flag3 = "0";
        }
        String flag4;
        if (chktraint2.Checked == true)
        {
            flag4 = "1";
        }
        else
        {
            flag4 = "0";
        }
        String flag5;
        if (chktraint3.Checked == true)
        {
            flag5 = "1";
        }
        else
        {
            flag5 = "0";
        }
        String flag6;
        if (chkcabtaxi.Checked == true)
        {
            flag6 = "1";
        }
        else
        {
            flag6 = "0";
        }
        String flag7;
        if (chkauto.Checked == true)
        {
            flag7 = "1";
        }
        else
        {
            flag7 = "0";
        }
        String flag8;
        if (chkownedvehicle.Checked == true)
        {
            flag8 = "1";
        }
        else
        {
            flag8 = "0";
        }
        String flag9;
        if (chklocalconveyance.Checked == true)
        {
            flag9 = "1";
        }
        else
        {
            flag9 = "0";
        }
        //String flag10;
        //if (chk_auto_approval.Checked == true)
        //{
        //    flag10 = "1";
        //}
        //else
        //{
        //    flag10 = "0";
        //}
        //String flag11;
        //if (chk_escalation_approval.Checked == true)
        //{
        //    flag11 = "1";
        //}
        //else
        //{
        //    flag11 = "0";
        //}
        String flag12;
        if (chk_app_cancel_if_approved.Checked == true)
        {
            flag12 = "1";
        }
        else
        {
            flag12 = "0";
        }
        String flag13;
        if (chk_app_cancel_if_ticket_confirmed.Checked == true)
        {
            flag13 = "1";
        }
        else
        {
            flag13 = "0";
        }
        String flag14;
        if (chk_hotel.Checked == true)
        {
            flag14 = "1";
        }
        else
        {
            flag14 = "0";
        }
        //String flag15;
        //if (chk_laundry.Checked == true)
        //{
        //    flag15 = "1";
        //}
        //else
        //{
        //    flag15 = "0";
        //}
        String flag16;
        if (chk_expenses_allowed.Checked == true)
        {
            flag16 = "1";

        }
        else
        {
            flag16 = "0";
        }
        String flag25;
        if (CheckBox_inside.Checked == true)
        {
            flag25 = "1";
        }
        else
        {
            flag25 = "0";
        }
        String flag26;
        if (CheckBox_outside.Checked == true)
        {
            flag26 = "1";
        }
        else
        {
            flag26 = "0";
        }
        //String flag17;
        //if (chk_femail_upgrade.Checked == true)
        //{
        //    flag17 = "1";
        //}
        //else
        //{
        //    flag17 = "0";
        //}
        //String flag18;
        //if (chk_payment_air.Checked == true)
        //{
        //    flag18 = "1";
        //}
        //else
        //{
        //    flag18 = "0";
        //}
        //String flag19;
        //if (chk_payment_bus.Checked == true)
        //{
        //    flag19 = "1";
        //}
        //else
        //{
        //    flag19 = "0";
        //}
        //String flag20;
        //if (chk_payment_train.Checked == true)
        //{
        //    flag20 = "1";
        //}
        //else
        //{
        //    flag20 = "0";
        //}
        //String flag21;
        //if (chk_payment_taxi.Checked == true)
        //{
        //    flag21 = "1";
        //}
        //else
        //{
        //    flag21 = "0";
        //}

        //String flag22;
        //if (chk_payment_hotel.Checked == true)
        //{
        //    flag22 = "1";
        //}
        //else
        //{
        //    flag22 = "0";
        //}

        try
        {
            int i = d.operation("UPDATE pay_travel_policy_master SET txt_policy_name='" + txt_policy_name1.Text + "' ,txt_start_date =str_to_date('" + txt_start_date.Text.ToString() + "','%d/%m/%Y'),txt_end_date =case when " + txt_end_date.Text.Length + "=0 then null else str_to_date('" + txt_end_date.Text + "','%d/%m/%Y') END,chkair='" + flag1 + "',chkbus='" + flag2 + "',chktraint1 ='" + flag3 + "',chktraint2='" + flag4 + "',chktraint3='" + flag5 + "',chkcabtaxi='" + flag6 + "',chkauto='" + flag7 + "',chkownedvehicle='" + flag8 + "',txtownedvehiclekms='" + txtownedvehiclekms.Text + "',chklocalconveyance='" + flag9 + "',txt_localconveyancelimit='" + txt_localconveyancelimit.Text + "',txt_approval_level='" + txt_approval_level.Text + "',txt_approval_days_before='" + txt_approval_days_before.Text + "',txt_app_escalation_level='" + txt_app_escalation_level.Text + "',txt_not_approved_emailid='" + txt_not_approved_emailid.Text + "',chk_app_cancel_if_approved='" + flag12 + "',chk_app_cancel_if_ticket_confirmed='" + flag13 + "',txt_cancellation_days='" + txt_cancellation_days.Text + "',txt_exception_case_approval_level='" + txt_exception_case_approval_level.Text + "',chk_hotel='" + flag14 + "',chk_expenses_allowed='" + flag16 + "',txt_per_day_limit='" + txt_per_day_limit.Text + "',chk_femail_upgrade='" + "',txt_claim_max_days='" + txt_claim_max_days.Text + "',chk_payment_air='" + "',CheckBox_inside='" + flag25 + "',CheckBox_outside='" + flag26 + "' ,Textbox_outside='" + Textbox_outside .Text+ "',submit=1 WHERE comp_code='" + Session["comp_code"].ToString() + "' AND  Id=" + ddl_Existing_policy_name.SelectedValue);
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Policy Submitted Successfully...');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            policy_fill();
        }
        text_clear();
        btnadd.Visible = false;
    }


    protected void ddl_Existing_policy_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        //text_clear1();
        try
        {
            
            btnadd.Visible = true;
            btn_submit.Visible = true;
            if (ddl_Existing_policy_name.SelectedValue.Equals("Select Policy"))
            {
                text_clear();
                btn_submit.Visible = false;
                btndelete.Visible = false;
            }
            else
            {
               
               
                txt_policy_name1.Text = ddl_Existing_policy_name.SelectedItem.Text;

                d.con1.Open();
                try
                {

                    MySqlCommand cmd = new MySqlCommand("select comp_code,'', txt_policy_name,date_format(txt_start_date,'%d/%m/%Y') as txt_start_date ,date_format(txt_end_date,'%d/%m/%Y') as txt_end_date,chkair ,chkbus ,chktraint1 ,chktraint2 ,chktraint3 ,chkcabtaxi ,chkauto ,chkownedvehicle ,txtownedvehiclekms ,chklocalconveyance ,txt_localconveyancelimit ,txt_approval_level ,txt_approval_days_before ,chk_app_cancel_if_ticket_confirmed ,txt_cancellation_days ,txt_exception_case_approval_level ,chk_hotel ,chk_expenses_allowed ,txt_per_day_limit ,txt_claim_max_days,CheckBox_ac,AC,CheckBox_nonac,NonAc,CheckBox_citybus,CityBus,CheckBox_car,Car,CheckBox_bike,Bike,hotel_budget, hotel_budget_from, hotel_budget_to,  hotel_standard ,hotel_standard_from,  hotel_standard_to,hotel_twostar,   hotel_twostar_from , hotel_twostar_to, hotel_threestar,  hotel_threestar_from , hotel_threestar_to, hotel_fivestar,  hotel_fivestar_from,  hotel_fivestar_to,CheckBox_breakfast,breakfast,CheckBox_lunch,lunch,CheckBox_dinner,dinner,chk_fooding,CheckBox_inside,CheckBox_outside,txt_not_approved_emailid,Textbox_outside, modified_time,now(),submit FROM pay_travel_policy_master where id=" + ddl_Existing_policy_name.SelectedValue, d.con1);
                    //MySqlCommand cmd = new MySqlCommand("select comp_code, '', txt_policy_name,date_format(txt_start_date,'%d/%m/%Y') as txt_start_date ,date_format(txt_end_date,'%d/%m/%Y') as txt_end_date,chkair,chkbus,chktraint1,chktraint2,chktraint3,chkcabtaxi,chkauto,chkownedvehicle,txtownedvehiclekms,chklocalconveyance,txt_localconveyancelimit,txt_approval_level,txt_approval_days_before,chk_auto_approval,txt_app_escalation_level,txt_not_approved_emailid,chk_app_cancel_if_approved,chk_app_cancel_if_ticket_confirmed,txt_cancellation_days,txt_exception_case_approval_level,chk_hotel,chk_laundry,chk_expenses_allowed,txt_per_day_limit,chk_femail_upgrade,txt_female_percent,txt_claim_max_days,txt_late_claim_days,txt_claim_exception_case,chk_payment_air,chk_payment_bus,chk_payment_train,chk_payment_taxi,chk_payment_hotel,modified_time,now(),submit FROM pay_travel_policy_master where id=" + ddl_Existing_policy_name.SelectedValue, d.con1);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txt_policy_name1.Text = dr.GetValue(2).ToString();
                        txt_start_date.Text = dr.GetValue(3).ToString();
                        txt_end_date.Text = dr.GetValue(4).ToString();

                        if (dr.GetValue(5).ToString() == "1")
                        {
                            chkair.Checked = true;

                        }
                        else
                        {
                            chkair.Checked = false;

                        }
                        if (dr.GetValue(6).ToString() == "1")
                        {
                            chkbus.Checked = true;

                        }
                        else
                        {
                            chkbus.Checked = false;

                        }

                        if (dr.GetValue(7).ToString() == "1")
                        {
                            chktraint1.Checked = true;

                        }
                        else
                        {
                            chktraint1.Checked = false;

                        }
                        if (dr.GetValue(8).ToString() == "1")
                        {
                            chktraint2.Checked = true;

                        }
                        else
                        {
                            chktraint2.Checked = false;

                        }
                        if (dr.GetValue(9).ToString() == "1")
                        {
                            chktraint3.Checked = true;

                        }
                        else
                        {
                            chktraint3.Checked = false;

                        }
                        if (dr.GetValue(10).ToString() == "1")
                        {
                            chkcabtaxi.Checked = true;

                        }
                        else
                        {
                            chkcabtaxi.Checked = false;

                        }
                        if (dr.GetValue(11).ToString() == "1")
                        {
                            chkauto.Checked = true;

                        }
                        else
                        {
                            chkauto.Checked = false;

                        }
                        if (dr.GetValue(12).ToString() == "1")
                        {
                            chkownedvehicle.Checked = true;


                        }
                        else
                        {
                            chkownedvehicle.Checked = false;

                        }


                       txtownedvehiclekms.Text = dr.GetValue(13).ToString();
                        if (dr.GetValue(14).ToString() == "1")
                        {
                            chklocalconveyance.Checked = true;


                        }
                        else
                        {
                            chklocalconveyance.Checked = false;


                        }
                        txt_localconveyancelimit.Text = dr.GetValue(15).ToString();
                        txt_approval_level.Text = dr.GetValue(16).ToString();
                        txt_approval_days_before.Text = dr.GetValue(17).ToString();
                        //if (dr.GetValue(18).ToString() == "1")
                        //{
                        //    chk_auto_approval.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_auto_approval.Checked = false;

                        //}
                        //txt_app_days_before_travel.Text = dr.GetValue(19).ToString();
                        //if (dr.GetValue(20).ToString() == "1")
                        //{
                        //    chk_escalation_approval.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_escalation_approval.Checked = false;

                        //}
                        //txt_app_escalation_approcal.Text = dr.GetValue(21).ToString();
                        //txt_app_escalation_level.Text = dr.GetValue(22).ToString();
                        //txt_not_approved_emailid.Text = dr.GetValue(23).ToString();
                        //if (dr.GetValue(24).ToString() == "1")
                        //{
                        //    chk_app_cancel_if_approved.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_app_cancel_if_approved.Checked = false;

                        //}
                        if (dr.GetValue(18).ToString() == "1")
                        {
                            chk_app_cancel_if_ticket_confirmed.Checked = true;

                        }
                        else
                        {
                            chk_app_cancel_if_ticket_confirmed.Checked = false;

                        }
                        txt_cancellation_days.Text = dr.GetValue(19).ToString();
                        txt_exception_case_approval_level.Text = dr.GetValue(20).ToString();
                        if (dr.GetValue(21).ToString() == "1")
                        {
                            chk_hotel.Checked = true;

                        }
                        else
                        {
                            chk_hotel.Checked = false;

                        }
                       
                        if (dr.GetValue(22).ToString() == "1")
                        {
                            chk_expenses_allowed.Checked = true;

                        }
                        else
                        {
                            chk_expenses_allowed.Checked = false;

                        }
                        txt_per_day_limit.Text = dr.GetValue(23).ToString();
                        txt_claim_max_days.Text = dr.GetValue(24).ToString();
                      
                        if (dr.GetValue(25).ToString() == "1")
                        {
                            CheckBox_ac.Checked = true;

                        }
                        else
                        {
                            CheckBox_ac.Checked = false;

                        }
                        TextBox_ac.Text = dr.GetValue(26).ToString();
                        if (dr.GetValue(27).ToString() == "1")
                        {
                            CheckBox_nonac.Checked = true;

                        }
                        else
                        {
                            CheckBox_nonac.Checked = false;

                        }
                        TextBox_nonac.Text = dr.GetValue(28).ToString();
                        if (dr.GetValue(29).ToString() == "1")
                        {
                            CheckBox_citybus.Checked = true;

                        }
                        else
                        {
                            CheckBox_citybus.Checked = false;

                        }
                        TextBox_citybus.Text = dr.GetValue(30).ToString();
                        if (dr.GetValue(31).ToString() == "1")
                        {
                            CheckBox_car.Checked = true;

                        }
                        else
                        {
                            CheckBox_car.Checked = false;

                        }
                        txtownedvehiclekms.Text = dr.GetValue(32).ToString();
                        if (dr.GetValue(33).ToString() == "1")
                        {
                            CheckBox_bike.Checked = true;

                        }
                        else
                        {
                            CheckBox_bike.Checked = false;

                        }
                        txtownedvehiclekms2.Text = dr.GetValue(34).ToString();
                        if (dr.GetValue(35).ToString() == "1")
                        {
                            hotel_budget.Checked = true;

                        }
                        else
                        {
                            hotel_budget.Checked = false;

                        }
                        text_hotel_budget.Text = dr.GetValue(36).ToString();
                        text_hotel_budget2.Text = dr.GetValue(37).ToString();
                        if (dr.GetValue(38).ToString() == "1")
                        {
                            hotel_standard.Checked = true;

                        }
                        else
                        {
                            hotel_standard.Checked = false;

                        }
                        text_hotel_standard.Text = dr.GetValue(39).ToString();
                        text_hotel_standard2.Text = dr.GetValue(40).ToString();
                        if (dr.GetValue(41).ToString() == "1")
                        {
                            hotel_twostar.Checked = true;

                        }
                        else
                        {
                            hotel_twostar.Checked = false;

                        }
                        text_hotel_twostar.Text = dr.GetValue(42).ToString();
                        text_hotel_twostar2.Text = dr.GetValue(43).ToString();
                        if (dr.GetValue(44).ToString() == "1")
                        {
                            hotel_threestar.Checked = true;

                        }
                        else
                        {
                            hotel_threestar.Checked = false;

                        }
                        text_hotel_threestar.Text = dr.GetValue(45).ToString();
                        text_hotel_threestar2.Text = dr.GetValue(46).ToString();
                        if (dr.GetValue(47).ToString() == "1")
                        {
                            hotel_fivestar.Checked = true;

                        }
                        else
                        {
                            hotel_fivestar.Checked = false;

                        }
                        text_hotel_fivestar.Text = dr.GetValue(48).ToString();
                        text_hotel_fivestar2.Text = dr.GetValue(49).ToString();
                        if (dr.GetValue(50).ToString() == "1")
                        {
                            CheckBox_breakfast.Checked = true;

                        }
                        else
                        {
                            CheckBox_breakfast.Checked = false;

                        }
                        TextBox_breakfast.Text = dr.GetValue(51).ToString();

                        if (dr.GetValue(52).ToString() == "1")
                        {
                            CheckBox_lunch.Checked = true;

                        }
                        else
                        {
                            CheckBox_lunch.Checked = false;

                        }
                        TextBox_lunch.Text = dr.GetValue(53).ToString();
                        if (dr.GetValue(54).ToString() == "1")
                        {
                            CheckBox_dinner.Checked = true;

                        }
                        else
                        {
                            CheckBox_dinner.Checked = false;

                        }
                        TextBox_dinner.Text = dr.GetValue(55).ToString();
                        if (dr.GetValue(56).ToString() == "1")
                        {
                            chk_fooding.Checked = true;

                        }
                        else
                        {
                            chk_fooding.Checked = false;

                        }
                        if (dr.GetValue(57).ToString() == "1")
                        {
                            CheckBox_inside.Checked = true;

                        }
                        else
                        {
                            CheckBox_inside.Checked = false;

                        }
                        if (dr.GetValue(58).ToString() == "1")
                        {
                            CheckBox_outside.Checked = true;

                        }
                        else
                        {
                            CheckBox_outside.Checked = false;

                        }
                        txt_not_approved_emailid.Text = dr.GetValue(59).ToString();
                        Textbox_outside.Text = dr.GetValue(60).ToString();
                        //if (dr.GetValue(29).ToString() == "1")
                        //{
                        //    chk_laundry.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_laundry.Checked = false;

                        //}
                        //if (dr.GetValue(32).ToString() == "1")
                        //{
                        //    chk_femail_upgrade.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_femail_upgrade.Checked = false;

                        //}
                        //txt_female_percent.Text = dr.GetValue(33).ToString();

                        //txt_late_claim_days.Text = dr.GetValue(35).ToString();
                        //txt_claim_exception_case.Text = dr.GetValue(36).ToString();
                        //if (dr.GetValue(37).ToString() == "1")
                        //{
                        //    chk_payment_air.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_payment_air.Checked = false;

                        //}
                        //if (dr.GetValue(38).ToString() == "1")
                        //{
                        //    chk_payment_bus.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_payment_bus.Checked = false;

                        //}
                        //if (dr.GetValue(39).ToString() == "1")
                        //{
                        //    chk_payment_train.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_payment_train.Checked = false;

                        //}
                        //if (dr.GetValue(40).ToString() == "1")
                        //{
                        //    chk_payment_taxi.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_payment_taxi.Checked = false;

                        //}
                        //if (dr.GetValue(41).ToString() == "1")
                        //{
                        //    chk_payment_hotel.Checked = true;

                        //}
                        //else
                        //{
                        //    chk_payment_hotel.Checked = false;

                        //}
                        //if (dr.GetValue(44).ToString() == "1")
                        //{
                        //    btn_submit.Visible = false;
                        //    btnadd.Visible = false;

                        //}

                    }
                    dr.Close();
                    cmd.Dispose();
                }
                catch (Exception ex) { throw ex; }
                finally { d.con1.Close(); }
                panel2.Visible = true;
                btndelete.Visible = true;
                try
                {
                    d.con.Open();
                    MySqlCommand cmd1 = new MySqlCommand("Select submit from pay_travel_policy_master where comp_code='" + Session["comp_code"].ToString() + "' AND id ='" + ddl_Existing_policy_name.SelectedValue + "'", d.con);
                    MySqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.Read())
                    {
                        if (dr1[0].Equals(0))
                        {
                            btnadd.Visible = true;
                            btn_submit.Visible = true;
                            btndelete.Visible = true;
                        }
                        else
                        {
                            btn_submit.Visible = false;
                            btnadd.Visible = false;
                            btndelete.Visible = false;
                        }
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                }
                finally
                {
                    d.con.Close();
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con.Close();
        }
    }

    protected void policy_fill()
    {
        ddl_Existing_policy_name.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT id,txt_policy_name FROM pay_travel_policy_master where comp_code='" + Session["comp_code"] + "'", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_Existing_policy_name.DataSource = dt_item;
                ddl_Existing_policy_name.DataValueField = dt_item.Columns[0].ToString();
                ddl_Existing_policy_name.DataTextField = dt_item.Columns[1].ToString();
                ddl_Existing_policy_name.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.conclose();
        }


        ddl_Existing_policy_name.Items.Insert(0, new ListItem("Select Policy", "Select Policy"));



    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            d.con.Open();
            MySqlCommand cmd1 = new MySqlCommand("Select id from pay_travel_emp_policy Where policy_id in (Select Id from pay_travel_policy_master Where txt_policy_name ='" + ddl_Existing_policy_name.SelectedItem.ToString() + "')", d.con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Travel Policy already assigned you cannot delete......');", true);
            }
            else
            {
                int res = 0;
                res = d.operation("DELETE FROM pay_travel_policy_master WHERE Id= " + ddl_Existing_policy_name.SelectedValue);
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully !!');", true);
                    text_clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed !!');", true);
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con.Close();
            text_clear();
            policy_fill();
        }
    }
    public void text_clear1()
    {
        txt_policy_name1.Text = "";
        txt_start_date.Text = "";
        txt_end_date.Text = "";
    
    }
}