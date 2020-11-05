using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public partial class new_employee_requirnment : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_insert.Visible = false;
        if (!IsPostBack)
        {
            ddl_clientlist();
        }
        

        load_new_emp_r_grid();

    }

    protected void ddl_clientlist()
{
    ddl_client.Items.Clear();
    System.Data.DataTable dt_item = new System.Data.DataTable();
    MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' and client_active_close='0' ORDER BY client_code", d.con);
    d.con.Open();
    try
    {
        cmd_item.Fill(dt_item);
        if (dt_item.Rows.Count > 0)
        {
            ddl_client.DataSource = dt_item;
            ddl_client.DataTextField = dt_item.Columns[0].ToString();
            ddl_client.DataValueField = dt_item.Columns[1].ToString();
            ddl_client.DataBind();
        }
        dt_item.Dispose();

        d.con.Close();
        ddl_client.Items.Insert(0, "Select");
    }
    catch (Exception ex) { throw ex; }
    finally
    {
        d.con.Close();
    }
}
    public void load_new_emp_r_grid()
    {
        hide_show_panel.Visible = false;
        try
        {

            gv_emp_requirement.Visible = true;
            d1.con1.Open();

            MySqlDataAdapter adp_grid = new MySqlDataAdapter("SELECT `pay_new_employee_requirement`.`Id`,`pay_client_master`.`client_name` as 'client_code',pay_unit_master.`unit_name` as 'unit_code',(SELECT (SELECT CASE `Employee_type` WHEN 'Reliever' THEN CONCAT(`emp_name`, '-', 'Reliever') ELSE `emp_name` END) FROM `pay_employee_master` WHERE `supervisor_code` = `pay_employee_master`.`emp_code`) AS 'supervisor_code',`new_employee_name`,`grade`FROM`pay_new_employee_requirement`INNER JOIN `pay_client_master` ON `pay_new_employee_requirement`.`client_name` = `pay_client_master`.`CLIENT_CODE` AND `pay_new_employee_requirement`.`COMP_CODE` = `pay_client_master`.`COMP_CODE`INNER JOIN `pay_unit_master` ON `pay_new_employee_requirement`.`unit_name` = `pay_unit_master`.`UNIT_CODE` AND `pay_new_employee_requirement`.`COMP_CODE` = `pay_unit_master`.`COMP_CODE` AND branch_status = 0 and client_active_close='0' ORDER BY pay_new_employee_requirement.ID DEsc", d1.con1);
            
            DataSet ds = new DataSet();
            adp_grid.Fill(ds);
            gv_emp_requirement.DataSource = ds;
            gv_emp_requirement.DataBind();
            d1.con1.Close();



            // }
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



    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");

    }

    protected void gv_emp_requirement_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnnew_Click();
        hide_show_panel.Visible = true;
        btn_insert.Visible=true;

        hide_show_panel.Visible = true;


        System.Web.UI.WebControls.Label lbl_GRADE_CODE = (System.Web.UI.WebControls.Label)gv_emp_requirement.SelectedRow.FindControl("lbl_CODE");
        string l_GRADE_CODE = lbl_GRADE_CODE.Text;

        d.con1.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

           // MySqlCommand cmd = new MySqlCommand("SELECT client_name ,unit_name,grade,(select emp_name from pay_employee_master where supervisor_code = pay_employee_master.emp_code) as supervisor_code,new_employee_name,father_name,dob,gender,doj,blood_group,working_hours,address,mobile_no,bank_holder_name,account_no,ifsc_code,adhar_no,adharcard_imgpath,police_imgpath,employee_imgpath,bank_imgpath,branch_name from pay_new_employee_requirement where Id='" + l_GRADE_CODE + "'", d.con1);
            MySqlCommand cmd = new MySqlCommand("SELECT `pay_client_master`.`client_name`,  pay_unit_master.`unit_name`,`grade`,  (SELECT `emp_name` FROM `pay_employee_master` WHERE `supervisor_code` = `pay_employee_master`.`emp_code`) AS 'supervisor_code',  `new_employee_name`,  `father_name`,  `dob`,  `gender`,`doj`,`blood_group`,`working_hours`,`address`,`mobile_no`,`bank_holder_name`,  `account_no`,  `ifsc_code`,`adhar_no`,`adharcard_imgpath`,`police_imgpath`,`employee_imgpath`,`bank_imgpath`,`branch_name`,pay_new_employee_requirement.Id FROM  `pay_new_employee_requirement`  INNER JOIN `pay_client_master` ON `pay_new_employee_requirement`.`client_name` = `pay_client_master`.`CLIENT_CODE` AND `pay_new_employee_requirement`.`COMP_CODE` = `pay_client_master`.`COMP_CODE` INNER JOIN `pay_unit_master` ON `pay_new_employee_requirement`.`unit_name` = `pay_unit_master`.`UNIT_CODE` AND `pay_new_employee_requirement`.`COMP_CODE` = `pay_unit_master`.`COMP_CODE` where pay_new_employee_requirement.Id='" + l_GRADE_CODE + "'", d.con1);

            
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //policy_id.Text = dr.GetValue(0).ToString();
                ddl_clientlist();
              ddl_client.SelectedItem.Text= dr.GetValue(0).ToString();
              ddl_client_SelectedIndexChanged(null, null);
             
               ddl_branch.SelectedItem.Text = dr.GetValue(1).ToString();
               ddl_branch_SelectedIndexChanged(null, null);
              ddl_grade.SelectedItem.Text = dr.GetValue(2).ToString();
              //// ddl_branch_SelectedIndexChanged(null, null);
                txt_suprvisr_name.Text = dr.GetValue(3).ToString();
                //date_picker.Text = dr.GetValue(8).ToString();
                txt_emp_name.Text = dr.GetValue(4).ToString();
                txt_f_h_name.Text = dr.GetValue(5).ToString();
                //ddl_gender.SelectedValue = dr.GetValue(7).ToString();
                if (dr.GetValue(7).ToString() == "Male")
                {
                    ddl_gender.SelectedValue = "M";
                }
                else {
                    ddl_gender.SelectedValue = "F";
                }
                txt_birth_date.Text = dr.GetValue(6).ToString();
                txt_join_date.Text = dr.GetValue(8).ToString();
                ddl_blood_group.SelectedValue = dr.GetValue(9).ToString();
                ddl_working.SelectedValue = dr.GetValue(10).ToString();
                txt_emp_address.Text = dr.GetValue(11).ToString();
                txt_mobileno.Text = dr.GetValue(12).ToString();
                txt_bank_name.Text = dr.GetValue(13).ToString();
                txt_acc_no.Text = dr.GetValue(14).ToString();
                txt_ifsc_code.Text = dr.GetValue(15).ToString();
                txt_aadhar_no.Text = dr.GetValue(16).ToString();

                if (dr.GetValue(17).ToString() != "")
                {

                    Image21.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(17).ToString();
                }
                else
                {
                    Image21.ImageUrl = "~/new_employee_requirement_document/placeholder.png";
                }

                if (dr.GetValue(18).ToString() != "")
                {

                    ImageButton2.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(18).ToString();
                }
                else
                {
                    ImageButton2.ImageUrl = "~/new_employee_requirement_document/placeholder.png";
                }

                if (dr.GetValue(19).ToString() != "")
                {

                    ImageButton1.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(19).ToString();
                }
                else
                {
                    ImageButton1.ImageUrl = "~/new_employee_requirement_document/placeholder.png";
                }
                if (dr.GetValue(20).ToString() != "")
                {

                    ImageButton3.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(20).ToString();
                }
                else
                {
                    ImageButton3.ImageUrl = "~/new_employee_requirement_document/placeholder.png";
                }
                tex_bran_na.Text = dr.GetValue(21).ToString();
                txt_search.Text = dr.GetValue(22).ToString();
            }
           // d.con.Dispose();
            //d.con.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con.Close();

            //    btn_Delet.Visible = true;
            //    btn_Update.Visible = true;
           
        }

        //for images




    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Client Code already Exist....Please Enter other Client Code !!!')", true);
    }

    protected void GradeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_emp_requirement, "Select$" + e.Row.RowIndex);

            //  e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "Select$" + e.Row.RowIndex);
            // e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GradeGridView, "select$" + e.Row.RowIndex);

        }
    }


    protected void lnkView_Click1(object sender, EventArgs e)
    {






    }
    private Boolean chkcount()
    {
        if (ddl_employee_type.SelectedValue == "Reliever")
        {
            string Perment_emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE=(select client_code from pay_client_master where comp_code='"+Session["comp_code"].ToString()+"' and client_name='"+ddl_client.SelectedValue+"') and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE=(select unit_code from pay_unit_master where comp_code='"+Session["comp_code"].ToString()+"' and unit_name='"+ddl_branch.SelectedValue+"') AND pay_employee_master.Employee_type = 'Permanent' and attendance_id = '" + ddl_working.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");

            string reliver_emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE=(select client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_name='" + ddl_client.SelectedValue + "') and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "') AND pay_employee_master.Employee_type = 'Reliever' and attendance_id = '" + ddl_working.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
           

            if (reliver_emp_count == "") { reliver_emp_count = "0"; }
            if (Perment_emp_count == "") { Perment_emp_count = "0"; }
            int count = Convert.ToInt32(Perment_emp_count) * 3;
            if (Convert.ToInt32(reliver_emp_count) > count)
            {
                return true;
            }
        }
        else
        {
            if (ddl_grade.SelectedItem.ToString() != "Select")
            {
                string unit_emp_count = d.getsinglestring("Select sum(COUNT) from pay_designation_count where CLIENT_CODE='" + Session["client_code"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + Session["unit_code_addemp"].ToString() + "' and HOURS = '" + ddl_working.SelectedValue + "'");
                if (unit_emp_count == "")
                {
                    return false;
                }
                string emp_count = d.getsinglestring("Select sum(1) from pay_employee_master where CLIENT_CODE='" + Session["client_code"].ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + Session["unit_code_addemp"].ToString() + "' AND pay_employee_master.`Employee_type` != 'Reliever' and attendance_id = '" + ddl_working.SelectedValue + "' and (pay_employee_master.LEFT_REASON='' || pay_employee_master.LEFT_REASON is null)");
                try
                {
                    if (unit_emp_count != "" && emp_count != "")
                    {
                        if (int.Parse(unit_emp_count) <= int.Parse(emp_count))
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
            }
        }
        return false;
    }
    
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_branch.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT((SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_branch.DataSource = dt_item;
                ddl_branch.DataTextField = dt_item.Columns[0].ToString();
                ddl_branch.DataValueField = dt_item.Columns[1].ToString();

                ddl_branch.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_branch.Items.Insert(0, "Select");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            hide_show_panel.Visible = true;
        }


    }
    protected void btnnew_Click()
    {
        d1.con.Open();
        try
        {
            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(EMP_CODE, 2, 5) AS UNSIGNED))+1 FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);
            MySqlDataReader drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {

            }
            else if (drmax.Read())
            {
                d1.con1.Open();
                try
                {
                    MySqlCommand cmdinit = new MySqlCommand("SELECT EMP_SERIES_INIT FROM pay_company_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
                    MySqlDataReader drinit = cmdinit.ExecuteReader();
                    if (!drinit.HasRows)
                    {
                    }
                    else if (drinit.Read())
                    {
                        string stinit = drinit.GetValue(0).ToString();
                        string initcode = "";
                        if (stinit != "")
                        {
                            initcode = stinit.ToString();
                            string str = drmax.GetValue(0).ToString();
                            if (str == "")
                            {
                                txt_eecode.Text = initcode.ToString() + "00001";
                            }
                            else
                            {
                                int max_empcode = int.Parse(drmax.GetValue(0).ToString());
                                if (max_empcode < 10)
                                {
                                    txt_eecode.Text = initcode.ToString() + "0000" + max_empcode;
                                }
                                else if (max_empcode >= 10 && max_empcode < 100)
                                {
                                    txt_eecode.Text = initcode.ToString() + "000" + max_empcode;
                                }
                                else if (max_empcode >= 100 && max_empcode < 1000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "00" + max_empcode;
                                }
                                else if (max_empcode >= 1000 && max_empcode < 10000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "0" + max_empcode;
                                }
                                else if (max_empcode == 10000)
                                {
                                    txt_eecode.Text = initcode.ToString() + "" + max_empcode;
                                }
                            }
                        }
                    }
                    drinit.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d1.con1.Close();
                }
            }
            drmax.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();

        }
       
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddl_grade.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
       // MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "'  ORDER BY UNIT_CODE", d.con);
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select DISTINCT(DESIGNATION) FROM pay_designation_count where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and unit_code='"+ddl_branch.SelectedValue+"'", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_grade.DataSource = dt_item;
                ddl_grade.DataTextField = dt_item.Columns[0].ToString();
                ddl_grade.DataValueField = dt_item.Columns[0].ToString();

                ddl_grade.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_grade.Items.Insert(0, "Select");
            //show_controls();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
            hide_show_panel.Visible = true;
        }


    }
    protected void btn_insert_Click(object sender, EventArgs e)
    {
        MySqlCommand cmd = new MySqlCommand("select  `moveto_employeemaster` from pay_new_employee_requirement where comp_code='" + Session["comp_code"].ToString() + "' and unit_name=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "') and `new_employee_name`='" + txt_emp_name.Text + "' ", d.con);
        d.con.Open();
        MySqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            string flag = dr.GetValue(0).ToString();
            if (flag == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Employee already Add In Employee Master!!.');", true);
                return;
            }
        }

        if (chkcount())
        {
            if (ddl_employee_type.SelectedValue == "Reliever")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Reliver Employee Count Limit Is FulFill!!.');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Take Approval To Change or to Next Employee!!.');", true);
                return;
            }
        }

       
        try
        {
            int result = 0;
            //   result = d.operation("insert into pay_employee_master(comp_code,client_code,unit_code,Employee_type,`EMP_CODE`,`EMP_NAME`,`GRADE_CODE`,`JOINING_DATE`,`GENDER`,`EMP_BLOOD_GROUP`,EMP_MOBILE_NO,EMP_PERM_ADDRESS,attendance_id,PF_BANK_NAME,BANK_EMP_AC_CODE,PF_IFSC_CODE,BANK_HOLDER_NAME,P_TAX_NUMBER,`BIRTH_DATE`,`EMP_FATHER_NAME`)VALUES('" + Session["comp_code"].ToString() + "','" + ddl_client.SelectedValue + "','" + ddl_branch.SelectedValue + "','" + ddl_employee_type.SelectedValue + "','" + txt_eecode.Text + "','" + txt_emp_name.Text + "','" + ddl_grade.SelectedItem.Text + "',str_to_date( '" + txt_join_date.Text + "','%d/%m/%Y'),'" + ddl_gender.SelectedValue + "','" + ddl_blood_group.SelectedValue + "','" + txt_mobileno.Text + "','" + txt_emp_address.Text + "','" + ddl_working.SelectedValue + "','" + tex_bran_na.Text + "','" + txt_acc_no.Text + "','" + txt_ifsc_code.Text + "','" + txt_bank_name.Text + "','" + txt_aadhar_no.Text + "',str_to_date('" + txt_birth_date.Text + "','%d/%m/%Y'),'" + txt_f_h_name.Text + "')");

            int age = 0;
            DateTime current_date = DateTime.Now;
            DateTime birth = DateTime.ParseExact(txt_birth_date.Text.ToString(), "dd/MM/yyyy", null);
            age = current_date.Year - birth.Year;
            if (current_date < birth.AddYears(age))
            {
                age--;

            }
            if (age < 18)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please check Date of Birth !!! Employee is less than 18 Years old');", true);
                txt_birth_date.Text = "";
                return;
               
            }
            else
            {
                if (ddl_gender.SelectedValue == "Male") { ddl_gender.SelectedValue = "M"; }
                 if (ddl_gender.SelectedValue == "Female") {ddl_gender.SelectedValue = "F";}
                 result = d.operation("insert into pay_employee_master(comp_code,client_code,unit_code,Employee_type,`EMP_CODE`,`EMP_NAME`,`GRADE_CODE`,`JOINING_DATE`,`GENDER`,`EMP_BLOOD_GROUP`,EMP_MOBILE_NO,EMP_CURRENT_ADDRESS,attendance_id,PF_BANK_NAME,BANK_EMP_AC_CODE,PF_IFSC_CODE,BANK_HOLDER_NAME,P_TAX_NUMBER,`BIRTH_DATE`,`EMP_FATHER_NAME`,`EMP_PERM_CITY`,`LOCATION_CITY`,`EMP_PERM_STATE`,`NFD_CODE`,EMP_CURRENT_CITY,`EMP_CURRENT_STATE`,client_wise_state,LOCATION)VALUES('" + Session["comp_code"].ToString() + "',(select client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_name='" + ddl_client.SelectedValue + "'),(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "'),'" + ddl_employee_type.SelectedValue + "','" + txt_eecode.Text + "','" + txt_emp_name.Text + "',(Select grade_code from pay_grade_master where grade_desc ='" + ddl_grade.SelectedValue + "' and comp_code='" + Session["comp_code"].ToString() + "'),str_to_date( '" + txt_join_date.Text + "','%d/%m/%Y'),'" + ddl_gender.SelectedValue + "','" + ddl_blood_group.SelectedValue + "','" + txt_mobileno.Text + "','" + txt_emp_address.Text + "','" + ddl_working.SelectedValue + "','" + tex_bran_na.Text + "','" + txt_acc_no.Text + "','" + txt_ifsc_code.Text + "','" + txt_bank_name.Text + "','" + txt_aadhar_no.Text + "',str_to_date('" + txt_birth_date.Text + "','%d/%m/%Y'),'" + txt_f_h_name.Text + "','Select','Select','Select','Select','Select',(SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE =(select client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_name='" + ddl_client.SelectedValue + "') and unit_code=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "')),(SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE =(select client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_name='" + ddl_client.SelectedValue + "') and unit_code=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "')),(SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE =(select client_code from pay_client_master where comp_code='" + Session["comp_code"].ToString() + "' and client_name='" + ddl_client.SelectedValue + "') and unit_code=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "')  ))");
                d.operation("update pay_new_employee_requirement set emp_code='" + txt_eecode.Text + "',moveto_employeemaster='1' where comp_code='" + Session["comp_code"].ToString() + "' and unit_name=(select unit_code from pay_unit_master where comp_code='" + Session["comp_code"].ToString() + "' and unit_name='" + ddl_branch.SelectedValue + "') and pay_new_employee_requirement.Id='" + txt_search.Text + "' ");
                MySqlCommand cmdmax = new MySqlCommand();
                d1.con.Open();
                if (Session["comp_code"].ToString() == "C01")
                {
                    cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 8, 6)) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                }

                else if (Session["comp_code"].ToString() == "C02")
                {
                    cmdmax = new MySqlCommand("SELECT MAX(SUBSTRING(Id_as_per_dob, 8, 6)) FROM  pay_employee_master WHERE comp_code='" + Session["comp_code"].ToString() + "'", d1.con);

                }

                MySqlDataReader drmax = cmdmax.ExecuteReader();
                if (drmax.Read())
                {
                    int rownum = int.Parse(drmax.GetValue(0).ToString());
                    int rownum1 = int.Parse(drmax.GetValue(0).ToString());
                    if (Session["comp_code"].ToString() == "C02")
                    {
                        d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IHMS-','M0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IHMS-','M000','" + rownum + "') when " + rownum1 + "<999 then concat('IHMS-','M00','" + rownum + "') when " + rownum1 + "<9999 then concat('IHMS-','M0','" + rownum + "') end) where comp_code='C02' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                    }
                    else if (Session["comp_code"].ToString() == "C01")
                    {
                        d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum1 + " < 9 then concat('IH&MS-','I0000','" + rownum + "') when  " + rownum1 + " < 99 then concat('IH&MS-','I000','" + rownum + "') when " + rownum1 + "<999 then concat('IH&MS-','I00','" + rownum + "') when " + rownum1 + "<9999 then concat('IH&MS-','I0','" + rownum + "') end) where comp_code='C01' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                       // d.operation("update pay_employee_master set Id_as_per_dob = (case when " + rownum + " < 9 then concat('IH&MS-','I0000',  " + rownum + "= " + rownum + " + 1) when  " + rownum + " < 99 then concat('IH&MS-','I000',  " + rownum + "= " + rownum + " + 1) when " + rownum + "<999 then concat('IH&MS-','I00'," + rownum + "= " + rownum + " + 1) when " + rownum + "<9999 then concat('IH&MS-','I0',= " + rownum + " + 1) end) where comp_code='C01' and emp_code='" + txt_eecode.Text + "' order by joining_date ");
                    }
                }
                d1.con.Close();

                employee_leave();
                Upload_File(null, null);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Added Successfully !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Added Fail !!!');", true);
                }
            }
        }
        catch (Exception ex) { throw ex; }
     finally
        {
        }
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    public void employee_leave()
    {
        //int result = 0;
        //result = d.operation("INSERT INTO pay_leave_emp_balance(comp_code,unit_code,EMP_CODE,last_update_date,create_user,create_date,leave_name,abbreviation,gender,max_no_of_leave,balance_leave) select '" + Session["comp_code"].ToString() + "', '" + ddl_unitcode.SelectedValue.ToString().Substring(0, 4) + "','" + txt_eecode.Text + "',now(),'" + Session["LOGIN_ID"].ToString() + "',now(),leave_name,abbreviation,gender,max_no_of_leave,max_no_of_leave from pay_leave_master where comp_code='" + Session["comp_code"].ToString() + "' and gender in ('" + ddl_gender.SelectedValue + "','B') ");
        d.operation("INSERT INTO pay_user_master(LOGIN_ID,USER_NAME,USER_PASSWORD,flag,create_user, create_date, password_changed_date,first_login,comp_code) VALUES('" +txt_eecode.Text + "','" +txt_emp_name.Text + "','" + GetSha256FromString(txt_birth_date.Text) + "','A','" + Session["USERID"].ToString() + "',now(),now(),'0','" + Session["comp_code"].ToString() + "')");
    }

    protected void Upload_File(object sender, EventArgs e)
    {
        if (txt_eecode.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
        }
        else if (txt_eecode.Text != "")
        {
            string cnt = "0";
            d.con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) FROM pay_images_master WHERE emp_code = '" + txt_eecode.Text + "' AND  comp_code='" + Session["comp_code"].ToString() + "'", d.con);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnt = dr.GetValue(0).ToString();
                }
            }
            catch (Exception ex) { throw ex; }
            finally { d.con.Close(); }
            if (cnt == "0")
            {
                d.operation("INSERT INTO pay_images_master (EMP_CODE, emp_name, comp_code,MODIF_DATE) values ('" + txt_eecode.Text + "','" + txt_emp_name.Text + "', '" + Session["comp_code"].ToString() + "',curdate())");
            }
        }
        /////////---------------------------------------EMP_PHOTO-------------------------------------------------------------
        if (aadhar_upload.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(aadhar_upload.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(aadhar_upload.PostedFile.FileName);
                    aadhar_upload.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_3" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_BANK_STATEMENT = '" + txt_eecode.Text + "_3" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }

        }
        else
        {
            MySqlCommand cmd = new MySqlCommand("select `adharcard_imgpath` from pay_new_employee_requirement where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_eecode.Text + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                String newpath = "";
                string adhar_card = dr.GetValue(0).ToString();
                string fileext = adhar_card.Substring(adhar_card.Length - 3);

                string imagename = dr.GetValue(0).ToString();
                string path = dr.GetValue(0).ToString();


                String newpath1233 = path.Replace(".png", "");
                var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);
               
                newpath = txt_eecode.Text + "_3" + result;

                System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/new_employee_requirement_document/") + newpath);
                System.IO.File.Delete(Server.MapPath("~/new_employee_requirement_document/") + path);
                File.Copy(Server.MapPath("~/EMP_Images/") + adhar_card, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_3" + result, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + adhar_card);
                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_BANK_STATEMENT  = '" + txt_eecode.Text + "_3" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                d.operation("UPDATE pay_new_employee_requirement SET  adharcard_imgpath = '" + txt_eecode.Text + "_3" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
            }
            dr.Close();
            d.con.Close();
        }

        /////------------------------------------------EMP_Polic station varification doc--------------------------------
        if (policy_document.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(policy_document.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(policy_document.PostedFile.FileName);
                    policy_document.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_13" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);

                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), POLICE_VERIFICATION_DOC = '" + txt_eecode.Text + "_13" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }

        }
        else
        {
            MySqlCommand cmd = new MySqlCommand("select `police_imgpath` from pay_new_employee_requirement where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_eecode.Text + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string POLICE_VERIFICATION_DOC = dr.GetValue(0).ToString();
                Image21.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(0).ToString();
                var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);

                String newpath = "";
                string adhar_card = dr.GetValue(0).ToString();
                string fileext = adhar_card.Substring(adhar_card.Length - 3);

                string imagename = dr.GetValue(0).ToString();
                string path = dr.GetValue(0).ToString();


                String newpath1233 = path.Replace(".png", "");
                

                newpath = txt_eecode.Text + "_13" + result;

                System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/new_employee_requirement_document/") + newpath);
                System.IO.File.Delete(Server.MapPath("~/new_employee_requirement_document/") + path);


                File.Copy(Server.MapPath("~/EMP_Images/") + POLICE_VERIFICATION_DOC, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_13" + result, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + POLICE_VERIFICATION_DOC);
                
                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), POLICE_VERIFICATION_DOC= '" + txt_eecode.Text + "_13" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                d.operation("UPDATE pay_new_employee_requirement SET  police_imgpath = '" + txt_eecode.Text + "_13" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
            }
            dr.Close();
            d.con.Close();
        }
        /////////---------------------------------------EMP_PHOTO-------------------------------------------------------------

        if (passportphoto.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(passportphoto.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(passportphoto.PostedFile.FileName);
                    passportphoto.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_1" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_PHOTO = '" + txt_eecode.Text + "_1" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }

        }
        else
        {
            MySqlCommand cmd = new MySqlCommand("select `employee_imgpath` from pay_new_employee_requirement where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_eecode.Text + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string EMP_PHOTO = dr.GetValue(0).ToString();
                ImageButton1.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(0).ToString();
                var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);

                String newpath = "";
                string adhar_card = dr.GetValue(0).ToString();
                string fileext = adhar_card.Substring(adhar_card.Length - 3);

                string imagename = dr.GetValue(0).ToString();
                string path = dr.GetValue(0).ToString();


                String newpath1233 = path.Replace(".png", "");


                newpath = txt_eecode.Text + "_1" + result;

                System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/new_employee_requirement_document/") + newpath);
                System.IO.File.Delete(Server.MapPath("~/new_employee_requirement_document/") + path);



                File.Copy(Server.MapPath("~/EMP_Images/") + EMP_PHOTO, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_1" + result, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + EMP_PHOTO);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), EMP_PHOTO = '" + txt_eecode.Text + "_1" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                d.operation("UPDATE pay_new_employee_requirement SET  employee_imgpath = '" + txt_eecode.Text + "_1" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
            }
            dr.Close();
            d.con.Close();
        }
        /////////---------------------------------------Bank Passbook-------------------------------------------------------------

        if (FileUpload2.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(FileUpload2.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                if (txt_eecode.Text != "")
                {
                    string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/EMP_Images/") + fileName);

                    File.Copy(Server.MapPath("~/EMP_Images/") + fileName, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_24" + fileExt, true);
                    File.Delete(Server.MapPath("~/EMP_Images/") + fileName);
                    d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), bank_passbook = '" + txt_eecode.Text + "_24" + fileExt + "' where emp_code = '" + txt_eecode.Text + "' and COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select Employee first !!!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }

        }
        else
        {
            MySqlCommand cmd = new MySqlCommand("select `bank_imgpath` from pay_new_employee_requirement where comp_code='" + Session["comp_code"].ToString() + "' and emp_code='" + txt_eecode.Text + "'", d.con);
            d.con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string bank_passbook = dr.GetValue(0).ToString();
                ImageButton3.ImageUrl = "~/new_employee_requirement_document/" + dr.GetValue(0).ToString();
                var result = dr.GetValue(0).ToString().Substring(dr.GetValue(0).ToString().Length - 4);

                String newpath = "";
                string adhar_card = dr.GetValue(0).ToString();
                string fileext = adhar_card.Substring(adhar_card.Length - 3);

                string imagename = dr.GetValue(0).ToString();
                string path = dr.GetValue(0).ToString();
                String newpath1233 = path.Replace(".png", "");
                newpath = txt_eecode.Text + "_24" + result;

                System.IO.File.Delete(Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/EMP_Images/") + path);
                System.IO.File.Copy(Server.MapPath("~/new_employee_requirement_document/") + path, Server.MapPath("~/new_employee_requirement_document/") + newpath);
                System.IO.File.Delete(Server.MapPath("~/new_employee_requirement_document/") + path);




                File.Copy(Server.MapPath("~/EMP_Images/") + bank_passbook, Server.MapPath("~/EMP_Images/") + txt_eecode.Text + "_24" + result, true);
                File.Delete(Server.MapPath("~/EMP_Images/") + bank_passbook);

                d.operation("UPDATE pay_images_master SET MODIF_DATE = curdate(), bank_passbook= '" + txt_eecode.Text + "_24" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
                d.operation("UPDATE pay_new_employee_requirement SET  bank_imgpath = '" + txt_eecode.Text + "_24" + result + "'  where emp_code = '" + txt_eecode.Text + "' AND  COMP_CODE='" + Session["COMP_CODE"].ToString() + "'");
            }
            dr.Close();
            d.con.Close();
        }

    }



    protected void gv_emp_requirement_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_emp_requirement.UseAccessibleHeader = false;
            gv_emp_requirement.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}
