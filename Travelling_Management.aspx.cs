using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

public partial class Travelling_Management : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    int counter = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (!IsPostBack)
        {
            lstLeft.Visible = true;
            ddlpolicies_selected_value(null, null);

        }
        search.Visible = false;
    }
    protected void update_listbox(string listarray1)
    {
        listarray1 = listarray1.Replace(",", ""); lstLeft.ClearSelection();

        for (int i = 0; i <= listarray1.Length - 1; i++)
        {
            lstLeft.Items[int.Parse(listarray1.Substring(i, 1))].Selected = true;
        }
    }
    private int getindex(string item1)
    {
        for (int i = 0; i < lstLeft.Items.Count; i++)
        {
            if (lstLeft.Items[i].ToString() == item1)
            {
                return i;
            }
        }
        return 0;
    }

    //protected void checkemp_click(object sender, EventArgs e)
    //{
    //    if (checkemp.Checked == true && CheckDepartment.Checked == false && ChecUnit.Checked == false)
    //    {
    //        try
    //        {
    //            d.con.Open();
    //            MySqlCommand cmd_1 = new MySqlCommand("SELECT EMP_NAME, emp_code FROM   pay_employee_master INNER JOIN pay_designation_count ON pay_employee_master.comp_code = pay_designation_count.comp_code and   pay_employee_master.unit_code = pay_designation_count.unit_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.Employee_type = 'Staff'  AND pay_designation_count.designation = '" + ddl_designation.SelectedValue + "' and LEFT_DATE is NULL ORDER BY EMP_NAME", d.con);
    //            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
    //            DataSet ds1 = new DataSet();
    //            cad1.Fill(ds1);
    //            counter = 1;
    //            lstLeft.DataSource = ds1.Tables[0];
    //            lstLeft.DataValueField = "emp_code";
    //            lstLeft.DataTextField = "EMP_NAME";
    //            lstLeft.DataBind();

    //            checklistcox.Items.Clear();

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //          // lstLeft.Items.Clear();
    //            d.con.Close();
    //            checklistcox.Items.Clear();
    //        }
    //    }
    //    else
    //    {
    //        lstLeft.Items.Clear();
    //        //checkemp.Checked = true;
    //       // checklistcox.Items.Clear(); 
    //        counter = 0;

    //    }

    //}
    protected void ChecUnit_click(object sender, EventArgs e)
    {
        if (ChecUnit.Checked == true && CheckDepartment.Checked == false)
        {


            try
            {
                d.con.Open();
                checklistcox.Visible = true;
                MySqlCommand cmd_empcode = new MySqlCommand("select UNIT_NAME from pay_unit_master where comp_code = '" + Session["comp_code"].ToString() + "' ", d.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_empcode);
                DataSet ds1 = new DataSet();
                cad1.Fill(ds1);
                checklistcox.Visible = true;
                checklistcox.DataSource = ds1.Tables[0];
                checklistcox.DataValueField = "UNIT_NAME";
                checklistcox.DataTextField = "UNIT_NAME";
                checklistcox.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d.con.Close();
                lstLeft.Items.Clear();
            }
        }
        else
        {
            //  if (counter == 0)
            //  {
            //ChecUnit.Checked = false;
            checklistcox.Items.Clear();
            lstLeft.Items.Clear();
            //  }
        }
    }

    protected void checklistcox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string name = "";
        if (CheckDepartment.Checked == true)
        {
            if (checklistcox.SelectedItem != null)
            {


                try
                {
                    foreach (ListItem lst in checklistcox.Items)
                    {
                        if (lst.Selected == true)
                        {
                            // string abc = lst.Value.ToString();
                            if (name == "")
                            {
                                name = (lst.Value.ToString()) + ",";
                            }
                            else
                            {
                                name = name + (lst.Value.ToString()) + ",";
                            }
                        }
                    }

                    if (name.EndsWith(","))
                    {
                        name = name.Remove(name.Length - 1, 1);
                        name = name.Replace(",", "','");
                    }
                    d.con.Open();
                    MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master,pay_department_master where pay_employee_master.comp_code ='" + Session["comp_code"] + "' and DEPT_NAME in ('" + name + "') and  pay_employee_master.DEPT_CODE= pay_department_master.DEPT_CODE order by emp_name ", d.con);
                    MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                    DataSet ds1 = new DataSet();
                    cad1.Fill(ds1);
                    lstLeft.DataSource = ds1.Tables[0];
                    lstLeft.DataValueField = "emp_code";
                    lstLeft.DataTextField = "emp_name";
                    lstLeft.DataBind();
                    lstLeft.Visible = true;
                    //d.con.Close();
                }
                catch (Exception ex)
                { throw ex; }
                finally
                {
                    d.con.Close();
                }
            }
            else
            {
                lstLeft.Items.Clear();
            }
        }
        else if (ChecUnit.Checked == true)
        {
            if (checklistcox.SelectedItem != null)
            {
                try
                {
                    d.con.Open();
                    MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master,pay_unit_master where pay_employee_master.comp_code ='" + Session["comp_code"] + "' and UNIT_NAME='" + checklistcox.SelectedItem.Text + "' and  pay_employee_master.UNIT_CODE= pay_unit_master.UNIT_CODE order by emp_name ", d.con);
                    MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                    DataSet ds1 = new DataSet();
                    cad1.Fill(ds1);
                    lstLeft.DataSource = ds1.Tables[0];
                    lstLeft.DataValueField = "emp_code";
                    lstLeft.DataTextField = "emp_name";
                    lstLeft.DataBind();
                    lstLeft.Visible = true;
                }
                catch (Exception ex)
                { throw ex; }
                finally
                {
                    d.con.Close();
                }
            }
            // else { lstLeft.Items.Clear(); }
        }
    }


    protected void CheckDepartment_click(object sender, EventArgs e)
    {
        if (CheckDepartment.Checked == true && ChecUnit.Checked == false)
        {
            try
            {
                string com_code = Session["comp_code"].ToString();
                checklistcox.Visible = true;
                d.con.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT DEPT_NAME FROM pay_department_master WHERE comp_code='" + Session["comp_code"].ToString() + "'  ", d.con);
                DataSet ds1 = new DataSet();
                MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
                adp1.Fill(ds1);
                checklistcox.Visible = true;
                checklistcox.DataSource = ds1.Tables[0];
                checklistcox.DataValueField = "DEPT_NAME";
                checklistcox.DataTextField = "DEPT_NAME";
                checklistcox.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d.con.Close();
                lstLeft.Items.Clear();
            }
        }
        else
        {
            //if (counter == 0)
            //{
            //CheckDepartment.Checked = false;
            checklistcox.Items.Clear();
            lstLeft.Items.Clear();
            // }
        }
    }

    protected void allriht_click(object sender, EventArgs e)
    {
        int li_select = 0;
        int res = 0;
        foreach (ListItem li in lstRight.Items)
        {
            li_select++;
            res = d.operation("update pay_travel_emp_policy set policy_id ='0' where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Move Successfully..');", true);
        }
        while (lstRight.Items.Count != 0)
        {
            for (int i = 0; i < lstRight.Items.Count; i++)
            {
                lstLeft.Items.Add(lstRight.Items[i]);
                lstRight.Items.Remove(lstRight.Items[i]);
            }
        }
    }
    protected void brnallleft_Click(object sender, EventArgs e)
    {

        while (lstLeft.Items.Count != 0)
        {
            for (int i = 0; i < lstLeft.Items.Count; i++)
            {
                lstRight.Items.Add(lstLeft.Items[i]);
                lstLeft.Items.Remove(lstLeft.Items[i]);

                //lstRight.Items.Add(lstLeft.Items[i]);
                //lstRight.Items.Remove(lstLeft.Items[i]);
            }
        }


    }

    protected void btnleft_click(object sender, EventArgs e)
    {
        int li_select = 0;
        int res = 0;
        foreach (ListItem li in lstRight.Items)
        {
            li_select++;
            res = d.operation("update pay_travel_emp_policy set policy_id ='0' where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Move Successfully..');", true);

        }
        if (lstRight.SelectedIndex >= 0)
        {
            for (int i = 0; i < lstRight.Items.Count; i++)
            {
                if (lstRight.Items[i].Selected)
                {
                    if (!arraylist1.Contains(lstRight.Items[i]))
                    {
                        arraylist1.Add(lstRight.Items[i]);
                    }
                }
            }
            for (int i = 0; i < arraylist1.Count; i++)
            {
                if (!lstLeft.Items.Contains(((ListItem)arraylist1[i])))
                {
                    lstLeft.Items.Add(((ListItem)arraylist1[i]));
                }
                lstRight.Items.Remove(((ListItem)arraylist1[i]));
            }
            lstLeft.SelectedIndex = -1;
        }

    }

    protected void btnright_click(object sender, EventArgs e)
    {
        if (lstLeft.SelectedIndex > 0 || lstLeft.SelectedIndex == 0)
        {
            for (int i = 0; i < lstLeft.Items.Count; i++)
            {
                if (lstLeft.Items[i].Selected)
                {
                    if (!arraylist2.Contains(lstLeft.Items[i]))
                    {
                        arraylist2.Add(lstLeft.Items[i]);
                    }
                }
            }
            for (int i = 0; i < arraylist2.Count; i++)
            {
                if (!lstRight.Items.Contains(((ListItem)arraylist2[i])))
                {
                    lstRight.Items.Add(((ListItem)arraylist2[i]));
                }
                lstLeft.Items.Remove(((ListItem)arraylist2[i]));
            }
            lstRight.SelectedIndex = -1;
        }
    }
    protected void btnclose_click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    private void update_notification()
    {
        d1.con1.Open();

        try
        {
            if (Application["policy_id"] == null || Application["policy_id"].ToString() == "")
            {
                Application["policy_id"] = Session["system_curr_date"].ToString();
                // MySqlCommand cmd1 = new MySqlCommand("SELECT id,txt_policy_name FROM pay_travel_policy_master  WHERE comp_code=@comp_code and UNIT_CODE=UNIT_CODE", d1.con1);
                MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_CODE,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["policy_id"].ToString() + "','%d/%m'),'%d %b'))as noti from pay_employee_master WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["policy_id"].ToString() + "',1,5)", d1.con1);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    d1.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select  'Travel Polices for " + Session["USERNAME"].ToString() + " -Policy- " + ddlpolicies.SelectedItem.Text.ToString() + "','0',EMP_CODE,'Travelling_Management.aspx' from pay_travel_emp_policy");
                }

            }
            else
            {
                //DateTime policy_id = DateTime.ParseExact(Application["policy_id"].ToString().Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime systemdate = DateTime.ParseExact(Session["system_curr_date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //if (policy_id <= systemdate)
                //{
                //    for (int i = 1; policy_id != systemdate; i++)
                //    {
                MySqlCommand cmd1 = new MySqlCommand("select concat('Its ',EMP_NAME,'s birthday today - ', DATE_FORMAT(str_to_date('" + Application["policy_id"].ToString() + "','%d/%m'),'%d %b'))as noti from pay_employee_master WHERE date_format(BIRTH_DATE,'%d/%m') = substring('" + Application["policy_id"].ToString() + "',1,5)", d1.con1);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    d1.operation("insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select  'Travel Polices for " + Session["USERNAME"].ToString() + " -Policy- " + ddlpolicies.SelectedItem.Text.ToString() + "','0',EMP_CODE,'Travelling_Management.aspx' from pay_travel_emp_policy");
                }
                dr1.Close();
                //policy_id = policy_id.AddDays(1);
                //Application["policy_id"] = policy_id.ToString("dd/MM/yyyy");

                //    }
                //}

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d1.con1.Close();
        }
        //insert into pay_notification_master (notification,not_read,EMP_CODE,page_name) select (select concat('Its ',EMP_NAME,' birthday today - ', DATE_FORMAT(NOW(),'%d %b %y')) from pay_employee_master WHERE str_to_date(BIRTH_DATE,'%d/%m/%Y') = date(now())),'0',EMP_CODE,'birthday.aspx' from pay_employee_master

    }

    protected void btnSubmit_click(object sender, EventArgs e)
    {
        int res = 0;
        int li_select = 0;
        emp_gv.DataSource = null;
        emp_gv.DataBind();
        try
        {
            update_notification();
            d.operation("insert into pay_travel_emp_policy (comp_code,unit_code,emp_code,policy_id) select comp_code,unit_code,emp_code,0 from pay_employee_master where comp_code = '" + Session["comp_code"].ToString() + "'  and emp_code not in ( select emp_code from pay_travel_emp_policy where comp_code = '" + Session["comp_code"].ToString() + "' )");
            res = d.operation("update pay_travel_emp_policy set policy_id = 0 where comp_code = '" + Session["comp_code"].ToString() + "' and unit_code = '" + Session["UNIT_CODE"] + "' and policy_id = " + ddlpolicies.SelectedValue.ToString());

            foreach (ListItem li in lstRight.Items)
            {
                li_select++;
                res = d.operation("update pay_travel_emp_policy set policy_id = " + ddlpolicies.SelectedValue.ToString() + " where comp_code = '" + Session["comp_code"].ToString() + "' and emp_code = '" + li.Value + "'");
            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employees added successfully to Policy.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select At list One Employee For Submition.');", true);
            }
            //}
        }
        catch (Exception ee)
        {

            throw ee;
        }
        finally
        {
            // ddlpolicies.SelectedValue = "0";
            lstRight.Items.Clear();
            lstLeft.Items.Clear();
            ddlpolicies_selected_value(null, null);
            designation_select();

        }
    }
    protected void ddlpolicies_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddlpolicies.SelectedValue.Equals("0"))
        {
            try
            {
                ddl_designation.Items.Clear();
                lstRight.Items.Clear();
                lstLeft.Items.Clear();
                designation_select();
                MySqlCommand cmd_1 = new MySqlCommand("SELECT pay_employee_master.emp_name, pay_travel_emp_policy.emp_code FROM pay_employee_master INNER JOIN pay_travel_emp_policy ON pay_employee_master.emp_code = pay_travel_emp_policy.emp_code   AND pay_employee_master.comp_code = pay_travel_emp_policy.comp_code WHERE pay_travel_emp_policy.policy_id = '" + ddlpolicies.SelectedValue.ToString() + "' AND pay_employee_master.comp_code = '" + Session["comp_code"] + "' ORDER BY EMP_NAME", d.con);
                //MySqlCommand cmd_1 = new MySqlCommand("select emp_name, emp_code from pay_employee_master  where emp_code in (select emp_code from pay_travel_emp_policy where policy_id = '" + ddlpolicies.SelectedValue.ToString() + "') and pay_employee_master.comp_code ='" + Session["comp_code"] + "'  order by EMP_NAME", d.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                DataSet ds1 = new DataSet();
                cad1.Fill(ds1);
                lstRight.DataSource = ds1.Tables[0];
                lstRight.DataValueField = "emp_code";
                lstRight.DataTextField = "emp_name";
                lstRight.DataBind();
                load_emp_gv();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d.con.Close();

            }
        }
        else
        {
            lstRight.Items.Clear();
        }
    }

    protected void ddlpolicies_selected_value(object sender, EventArgs e)
    {
        d.con.Open();
        try
        {
            //vikas13/11
            ddlpolicies.Items.Clear();
            DataTable dt = new DataTable();
            MySqlDataAdapter dr1 = new MySqlDataAdapter("SELECT txt_policy_name,id FROM pay_travel_policy_master  WHERE comp_code ='" + Session["COMP_CODE"].ToString() + "' and submit='1' ", d.con);
            dr1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlpolicies.DataSource = dt;
                ddlpolicies.DataTextField = dt.Columns[0].ToString();
                ddlpolicies.DataValueField = dt.Columns[1].ToString();
                ddlpolicies.DataBind();
            }

            //DataTable dt = new DataTable();
            //MySqlDataAdapter dr = new MySqlDataAdapter("SELECT txt_policy_name,id FROM pay_travel_policy_master  WHERE comp_code ='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            //dr.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    ddlpolicies.DataSource = dt;
            //    ddlpolicies.DataTextField = dt.Columns[0].ToString();
            //    ddlpolicies.DataValueField = dt.Columns[1].ToString();
            //    ddlpolicies.DataBind();
            //}
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            ddlpolicies.Items.Insert(0, new ListItem("Select Policy", "Select Policy"));
        }
    }
    protected void designation_select()
    {
        ddl_designation.Items.Clear();
        d.con.Open();
        try
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter dr1 = new MySqlDataAdapter("SELECT DISTINCT pay_grade_master.GRADE_DESC FROM pay_employee_master INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code  where pay_employee_master.client_code='Staff' and pay_employee_master.comp_code ='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            //MySqlDataAdapter dr1 = new MySqlDataAdapter("select distinct designation from pay_designation_count  where client_code='Staff' and comp_code ='" + Session["COMP_CODE"].ToString() + "' ", d.con);
            dr1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_designation.DataSource = dt;
                ddl_designation.DataTextField = dt.Columns[0].ToString();
                ddl_designation.DataBind();
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
            ddl_designation.Items.Insert(0, new ListItem("Select Designation"));
        }
    }
    protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            search.Visible = true;
            d.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT EMP_NAME,pay_employee_master.emp_code FROM  pay_employee_master INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code inner join pay_travel_emp_policy on pay_employee_master.comp_code=pay_travel_emp_policy.comp_code and pay_employee_master.emp_code=pay_travel_emp_policy.emp_code WHERE pay_employee_master.comp_code = '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = 'Staff'  AND pay_grade_master.GRADE_DESC = '" + ddl_designation.SelectedValue + "'   and pay_travel_emp_policy.policy_id ='0' and LEFT_DATE is NULL ORDER BY EMP_NAME", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            counter = 1;
            lstLeft.DataSource = ds1.Tables[0];
            lstLeft.DataValueField = "emp_code";
            lstLeft.DataTextField = "EMP_NAME";
            lstLeft.DataBind();
            checklistcox.Items.Clear();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // lstLeft.Items.Clear();
            d.con.Close();
            checklistcox.Items.Clear();
        }
    }
    protected void load_emp_gv()
    {
        emp_gv.DataSource = null;
        emp_gv.DataBind();
        try
        {
            search.Visible = true;
            d.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("SELECT DISTINCT EMP_NAME,  pay_grade_master.GRADE_DESC,  (SELECT txt_policy_name FROM pay_travel_policy_master WHERE comp_code =  '" + Session["comp_code"].ToString() + "' AND submit = '1' and txt_policy_name='" + ddlpolicies.SelectedItem + "') AS ' Policy name' FROM pay_employee_master INNER JOIN pay_grade_master ON pay_employee_master.comp_code = pay_grade_master.comp_code AND pay_employee_master.grade_code = pay_grade_master.grade_code INNER JOIN pay_travel_emp_policy ON pay_employee_master.comp_code = pay_travel_emp_policy.comp_code AND pay_employee_master.emp_code = pay_travel_emp_policy.emp_code WHERE  pay_employee_master.comp_code =  '" + Session["comp_code"].ToString() + "' AND pay_employee_master.client_code = 'Staff'  AND pay_travel_emp_policy.policy_id = '" + ddlpolicies.SelectedValue + "' AND LEFT_DATE IS NULL ORDER BY EMP_NAME", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                emp_gv.DataSource = ds1;
                emp_gv.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // lstLeft.Items.Clear();
            d.con.Close();

        }
    }
    protected void emp_gv_PreRender(object sender, EventArgs e)
    {
        try
        {
            emp_gv.UseAccessibleHeader = false;
            emp_gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }

    protected void emp_gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }
}