using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Collections;

public partial class StateDetails : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    StateBAL stbl3 = new StateBAL();
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_delete.Visible = false;
        btn_edit.Visible = false;
		
		if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = false;
            btnexporttoexcelstate.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            btnexporttoexcelstate.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "State Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            btnexporttoexcelstate.Visible = false;
        }
        if (!IsPostBack)
        {
            btn_cancel_Click(null, null);
            txt_statename.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT DISTINCT state_name from pay_state_master order by 1", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    txt_statename.Items.Add(dr_item1[0].ToString());
                    ddl_states.Items.Add(dr_item1[0].ToString());
                }
                dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
                txt_statename.Items.Insert(0, new ListItem("Select", ""));
                ddl_states.Items.Insert(0, new ListItem("ALL", ""));

            }
            System.Data.DataTable dt = new System.Data.DataTable();
            GridView1.DataSource = GetData("select state_name as Country,state_code as ContactName,city as City from pay_state_master group by state_name, city, state_code");
            GridView1.DataBind();
        }

       
        
    }
    private System.Data.DataTable GetData(string query)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        string constr = ConfigurationManager.ConnectionStrings["CELTPAYConnectionString"].ConnectionString;
        using (MySqlConnection con = new MySqlConnection(constr))
        {
            using (MySqlCommand cmd = new MySqlCommand(query))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = GridView1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView1.Rows[i];
            GridViewRow previousRow = GridView1.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (validate_state())
        {
            StateBAL stbl2 = new StateBAL();
            int result = 0;
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                result = stbl2.StateInsert(txt_statecode.Text, txt_statename.Text, txt_city.Text.ToString());
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Record added successfully!!')", true);
                    txt_statecode.Text = "";
                    txt_statename.Text = "";
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
               
                btn_cancel_Click(null, null);
            }
        }
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        if (validate_state())
        {
            int result = 0;
            try
            {
                //System.Web.UI.WebControls.Label lbl_city = (System.Web.UI.WebControls.Label)StateGridView.SelectedRow.Cells[1].Text;
                string l_city = StateGridView.SelectedRow.Cells[1].Text;

                result = stbl3.StateUpdate(ViewState["state_code"].ToString(), txt_statename.SelectedItem.ToString(), txt_statecode.Text, txt_city.Text.ToString(), l_city);
                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record updated successfully!!')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record updation failed...')", true);
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                btn_cancel_Click(null, null);
            }
        }
    }  

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        d.con1.Open();
        try
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM pay_state_master", d.con1);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            StateGridView.DataSource = ds1.Tables[0];
            StateGridView.DataBind();
            txt_statecode.Text = "";
            txt_statename.Text = "";
            txt_city.Text = "";
        }
        catch (Exception ex) 
        {
            throw ex; 
        }
        finally
        {
            d.con1.Close();
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            btn_add.Visible = true;
        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        StateBAL stbl4 = new StateBAL();
        int result = 0;
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("SELECT EMP_CODE FROM pay_employee_master WHERE (EMP_CURRENT_STATE='" + txt_statename.Text + "' OR  EMP_PERM_STATE='" + txt_statename.Text + "') and  (EMP_CURRENT_CITY='" + txt_city.Text + "' OR EMP_PERM_CITY='"+txt_city.Text+"') ", d.con1);
            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee exist can not delete this State');", true);
            }
            else
            {
                result = stbl4.StateDelete(txt_statecode.Text,txt_city.Text);
                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deleted successfully!!')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record deletion failed...')", true);
                }
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();
            btn_cancel_Click(null, null);
        }
    }

    protected void btnexporttoexcelstate_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 20;

        Range rng = ws.get_Range("E1:E1");
        rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange2 = ws.get_Range("E1:E1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange2.Font.Size = 20;


        Range rng2 = ws.get_Range("E2:E2");
        rng2.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange3 = ws.get_Range("E2:E2");
        formateRange3.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRange3.Font.Size = 20;

        Range rng3 = ws.get_Range("A5:C5");
        rng3.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRang4 = ws.get_Range("A5:C5");
        formateRang4.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
        formateRang4.Font.Size = 15;


        ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        ws.Cells[2, 5] = "STATE LIST";
        ws.Cells[5, 1] = "STATE CODE";
        ws.Cells[5, 2] = "CITY NAME";
        ws.Cells[5, 3] = "STATE NAME";
        try
        {
            d.con1.Open();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT STATE_CODE,CITY,STATE_NAME FROM pay_state_master ORDER BY STATE_CODE", d.con1);
            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 6;
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[j, i + 1] = row[i].ToString();
                }
                j++;
            }
            xla.Visible = true;
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();

        }
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        //string currentdate_check = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //string tab_employeecheck = d.getsinglestring("select  * from pay_tab_employee_attendances_details where emp_code='M02421' and date(currdate)=date(str_to_date('" + currentdate_check + "','%d/%m/%Y'))");
        //if (tab_employeecheck.Equals(""))
        //{

        //}
        //else
        //{
        //}

       Response.Redirect("tab_employee_attendance_details.aspx");
       // Response.Redirect("WorkingChecklist.aspx");

      //  Response.Redirect("Home.aspx");
        // my code 
       // this.GetCustomer(int.Parse(txt_statecode.Text.Trim()));
       // this.GetCustomer(txt_statecode.Text.Trim().ToString());
       // this.employeemasterdata(txt_statecode.Text.Trim().ToString());
    }

    private void employeemasterdata(string emp_id) {

        MySqlCommand cmd_1 = new MySqlCommand("employeemaster", d.con1);
        d.con1.Open();
        cmd_1.CommandType = CommandType.StoredProcedure;
        cmd_1.Parameters.AddWithValue("@emp_id", emp_id);
      
        MySqlDataAdapter sda = new MySqlDataAdapter(cmd_1);
     
        System.Data.DataTable dt = new System.Data.DataTable();
        sda.Fill(dt);
        StateGridView.DataSource = dt;
        StateGridView.DataBind();


        d.con1.Close();
    }


    private void GetCustomer(string   customerId)
    {
        string constr = ConfigurationManager.ConnectionStrings["CELTPAYConnectionString"].ConnectionString;
        using (MySqlConnection con = new MySqlConnection(constr))
        {
            using (MySqlCommand cmd = new MySqlCommand("Customers_GetCustomer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustId", customerId);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    StateGridView.DataSource = dt;
                    StateGridView.DataBind();
                }
            }
        }
    }



    protected void btnreportstate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }

    protected void StateGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_statecode.Text = StateGridView.SelectedRow.Cells[0].Text;
        txt_city.Text = StateGridView.SelectedRow.Cells[1].Text;
        txt_statename.Text = StateGridView.SelectedRow.Cells[2].Text;
        btn_add.Visible = false;
        btn_edit.Visible = true;
        btn_delete.Visible = true;

        //txt_statecode.ReadOnly = true;
        //txt_statename.ReadOnly = false;
        ViewState["state_code"] = txt_statecode.Text;
    }

    protected void StateGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.StateGridView, "Select$" + e.Row.RowIndex);
        }
    }

    private bool validate_state()
    {
        try
        {
            MySqlCommand cmd_1 = new MySqlCommand("SELECT state_name FROM pay_state_master", d.con1);
            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            while (dr_1.Read())
            {
                //if (txt_statename.Text.ToUpper().Equals(dr_1.GetValue(0).ToString().ToUpper()))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('State Name already Present.')", true);
                //    return false;
                //}
            }
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d.con1.Close();
        }

        return true;
    }

    protected void StateGridView_PreRender(object sender, EventArgs e)
    {
         try
         {
             StateGridView.UseAccessibleHeader = false;
             StateGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
         }
         catch { }//vinod dont apply catch
    }

    protected void brnallleft_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        while (lstLeft.Items.Count != 0)
        {
            for (int i = 0; i < lstLeft.Items.Count; i++)
            {
                lstRight.Items.Add(lstLeft.Items[i]);
                lstLeft.Items.Remove(lstLeft.Items[i]);
            }
        }


    }

    protected void btnleft_click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int li_select = 0;
        int res = 0;
        foreach (ListItem li in lstRight.Items)
        {
            li_select++;
            res = d.operation("update pay_state_master set tier = "+ddl_tiers.SelectedValue+" where city = '" + li.Value + "'");
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
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
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
    protected void allriht_click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int li_select = 0;
        int res = 0;
        foreach (ListItem li in lstRight.Items)
        {
            li_select++;
            res = d.operation("update pay_state_master set tier = " + ddl_tiers.SelectedValue + " where city = '" + li.Value + "'");
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
    protected void btnSubmit_click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
        int li_select = 0;
        try
        {
            foreach (ListItem li in lstRight.Items)
            {
                li_select++;
                res = d.operation("update pay_state_master set tier = " + ddl_tiers.SelectedValue + " where city = '" + li.Value + "'");

            }
            if (res > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('City added successfully to Tier.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('City added unsuccessfully to Tier.');", true);
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
            ddl_states.SelectedValue = "";
            lstRight.Items.Clear();
            ddl_tiers.SelectedValue = "Select";
            
            //lstLeft.Text = "";
            lstLeft.Items.Clear();


        }
    }
    protected void ddl_states_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        fill_list();
    }
    
    protected void ddl_tiers_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        fill_list();
    }
    private void fill_list()
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        try
        {
            string where = "where city is not null and tier = 0";
            if (!ddl_states.SelectedItem.ToString().ToUpper().Equals("ALL"))
            {
                where = where + " and state_name = '" + ddl_states.SelectedItem.ToString() + "'";
            }

            d.con.Open();
            MySqlCommand cmd_1 = new MySqlCommand("select City from pay_state_master " + where + " order by city", d.con);
            MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
            DataSet ds1 = new DataSet();
            cad1.Fill(ds1);
            lstLeft.DataSource = ds1.Tables[0];
            lstLeft.DataTextField = "City";
            lstLeft.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
        }
        try
        {
            if (!ddl_tiers.SelectedValue.ToUpper().Equals("SELECT"))
            {
                string where = "where city is not null and tier = " + ddl_tiers.SelectedValue;
                if (!ddl_states.SelectedItem.ToString().ToUpper().Equals("ALL"))
                {
                    where = where + " and state_name = '" + ddl_states.SelectedItem.ToString() + "'";
                }

                d.con.Open();
                MySqlCommand cmd_1 = new MySqlCommand("select City from pay_state_master "+where+" order by city", d.con);
                MySqlDataAdapter cad1 = new MySqlDataAdapter(cmd_1);
                DataSet ds1 = new DataSet();
                cad1.Fill(ds1);
                lstRight.DataSource = ds1.Tables[0];
                lstRight.DataTextField = "City";
                lstRight.DataBind();
            }
            else
            {
                lstRight.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            d.con.Close();
        }
    }
}
