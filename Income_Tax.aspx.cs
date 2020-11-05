
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;





public partial class guest_new : System.Web.UI.Page
{

    DAL d1 = new DAL();
    public MySqlDataReader drmax = null;

    

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d1.getaccess(Session["ROLE"].ToString(), "Income Tax", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Income Tax", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_Delete.Visible = false;
            btn_Update.Visible = false;
            lnkbtn_add_icon.Visible = false;
            btn_exel.Visible = false;
           // btnhelp.Visible = false;
            btn_search.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Income Tax", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_Delete.Visible = false;
            lnkbtn_add_icon.Visible = false;
            btn_exel.Visible = false;
            
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Income Tax", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_Delete.Visible = false;
            btn_exel.Visible = false;
        }
        





        if (!IsPostBack)
        {
            lbl_error.Visible = false;
            Panel1.Visible = false;
        }
    }



    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {


            int TotalRows = gv_itemslist.Rows.Count;


            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                System.Web.UI.WebControls.Label lbl_Sr_No = (System.Web.UI.WebControls.Label)row.FindControl("lbl_Sr_No");
                int Sr_No = Convert.ToInt32(lbl_Sr_No.Text);

                System.Web.UI.WebControls.TextBox lbl_lower_value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_lower_value");
                string Lower_Value = lbl_lower_value.Text;

                System.Web.UI.WebControls.TextBox lbl_Upper_Value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_Upper_Value");
                string Upper_Value = lbl_Upper_Value.Text;

                System.Web.UI.WebControls.TextBox lbl_ITax = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_ITax");
                string ITax = lbl_ITax.Text;

                System.Web.UI.WebControls.TextBox lbl_cess = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_cess");
                string Cess = lbl_cess.Text;

                System.Web.UI.WebControls.TextBox lbl_surcharge = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_surcharge");
                string Surcharge = lbl_surcharge.Text;


                MySqlCommand cmd_tarnsactiondetailsinsert = new MySqlCommand("INSERT INTO pay_income_tax(comp_code,EMP_CODE,type,Effective_Since,Sr_No,Lower_Value,Upper_Value,ITax,Cess,Surcharge) VALUES('" + Session["comp_code"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','" + ddl_type.SelectedValue.ToString() + "','" + txt_Since.Text + "','" + Convert.ToInt32(Sr_No) + "','" + lbl_lower_value.Text + "','" + lbl_Upper_Value.Text + "','" + lbl_ITax.Text + "','" + lbl_cess.Text + "','" + lbl_surcharge.Text + "')", d1.con);
                d1.conopen();
                cmd_tarnsactiondetailsinsert.ExecuteNonQuery();
                d1.conclose();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!')", true);




            d1.conclose();


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record added successfully!')", true);
        }
        finally
        {
            text_Clear();
            d1.reset(this);
        }
    }

    protected void lnkbtn_add_icon_Click(object sender, EventArgs e)
    {
        try {
           
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);

        }
        catch { }
        
        System.Data.DataTable dt = new System.Data.DataTable();
        DataRow dr;
        dt.Columns.Add("type");
        dt.Columns.Add("Effective_Since");
        dt.Columns.Add("Lower_Value");
        dt.Columns.Add("Upper_Value");
        dt.Columns.Add("ITax");
        dt.Columns.Add("Cess");
        dt.Columns.Add("Surcharge");


        int rownum = 0;
        for (rownum = 0; rownum < gv_itemslist.Rows.Count; rownum++)
        {
            if (gv_itemslist.Rows[rownum].RowType == DataControlRowType.DataRow)
            {

                dr = dt.NewRow();
                System.Web.UI.WebControls.Label lbl_Sr_No = (System.Web.UI.WebControls.Label)gv_itemslist.Rows[rownum].Cells[1].FindControl("lbl_Sr_No");
                int Sr_No = Convert.ToInt32(lbl_Sr_No.Text);

                System.Web.UI.WebControls.TextBox lbl_type = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_type");
                dr["type"] = lbl_type.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_since = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_since");
                dr["Effective_Since"] = lbl_since.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_lower_value = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[2].FindControl("lbl_lower_value");
                dr["Lower_Value"] = lbl_lower_value.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_Upper_Value = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[3].FindControl("lbl_Upper_Value");
                dr["Upper_Value"] = lbl_Upper_Value.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_ITax = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[4].FindControl("lbl_ITax");
                dr["ITax"] = lbl_ITax.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_cess = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[5].FindControl("lbl_cess");
                dr["Cess"] = lbl_cess.Text.ToString();

                System.Web.UI.WebControls.TextBox lbl_surcharge = (System.Web.UI.WebControls.TextBox)gv_itemslist.Rows[rownum].Cells[6].FindControl("lbl_surcharge");
                dr["Surcharge"] = lbl_surcharge.Text.ToString();

                dt.Rows.Add(dr);

            }
        }
        dr = dt.NewRow();
        dr["type"] = ddl_type.SelectedItem.Text;
        dr["Effective_Since"] = txt_Since.Text;
        dr["Lower_Value"] = txt_Lower_Value.Text.ToString();
        dr["Upper_Value"] = txt_Upper_Value.Text.ToString();
        dr["ITax"] = txt_ITax.Text.ToString();
        dr["Cess"] = txt_Cess.Text;
        dr["Surcharge"] = txt_Surcharge.Text;

        dt.Rows.Add(dr);
        gv_itemslist.DataSource = dt;
        gv_itemslist.DataBind();
        ViewState["CurrentTable"] = dt;

        txt_Lower_Value.Text = "";
        txt_Upper_Value.Text = "";
        txt_ITax.Text = "";
        txt_Cess.Text = "";
        txt_Surcharge.Text = "";



        foreach (GridViewRow row in gv_itemslist.Rows)
        {

            System.Web.UI.WebControls.TextBox lbl_lower_value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_lower_value");
            dr["Lower_Value"] = lbl_lower_value.Text.ToString();
            System.Web.UI.WebControls.TextBox lbl_Upper_Value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_Upper_Value");
            dr["Upper_Value"] = lbl_Upper_Value.Text.ToString();
            System.Web.UI.WebControls.TextBox lbl_ITax = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_ITax");
            dr["ITax"] = lbl_ITax.Text.ToString();
            System.Web.UI.WebControls.TextBox lbl_cess = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_cess");
            dr["Cess"] = lbl_cess.Text.ToString();
            System.Web.UI.WebControls.TextBox lbl_surcharge = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_surcharge");
            dr["Surcharge"] = lbl_surcharge.Text.ToString();
         
        }

        Panel5.Visible = true;
        btn_Save.Visible = true;
        //********************************

        ddl_type.SelectedValue = "Select";
        txt_Since.Text = "";
        ddl_type.SelectedValue = "Select";
      
      
    }

    protected void lnkbtn_removeitem_Click(object sender, EventArgs e)
    {
        int result1;
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int rowID = row.RowIndex;
        int sr = rowID + 1;
      
        if (ViewState["CurrentTable"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count >= 1)
            {
                if (row.RowIndex < dt.Rows.Count)
                {
                    System.Web.UI.WebControls.Label lbl_Sr_No = (System.Web.UI.WebControls.Label)row.FindControl("lbl_Sr_No");
                    int Sr_No = Convert.ToInt32(lbl_Sr_No.Text);
                    System.Web.UI.WebControls.TextBox lbl_type = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_type");
                    string type = (lbl_type.Text);

                    result1 = d1.operation("DELETE FROM pay_income_tax  WHERE Sr_No = '" + Sr_No + "' AND type = '" + type + "' AND comp_code='" + Session["comp_code"].ToString() + "' ");//delete command
                    if (result1 > 0)
                    {
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected Record Removed successfully!!');", true);
               
                    }
                    else
                    {
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record removation failed...');", true);
                
                     }
                    }

                }
             ViewState["CurrentTable"] = dt;
            gv_itemslist.DataSource = null;
            gv_itemslist.DataBind();
            gv_itemslist.DataSource = dt;
            gv_itemslist.DataBind();
            }
           
        }

    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {

            int result = 0;
            //System.Web.UI.WebControls.Label lbl_Type = (System.Web.UI.WebControls.Label)GridView1.SelectedRow.FindControl("lbl_Type");
            //string l_type = ddl_view_type.SelectedItem.Text;
           

            result = d1.operation("DELETE FROM pay_income_tax  WHERE  type = '" + ddl_type.SelectedValue.ToString() + "' AND comp_code='" + Session["comp_code"].ToString() + "' ");//delete command

           
            foreach (GridViewRow row in gv_itemslist.Rows)
            {
                System.Web.UI.WebControls.Label lbl_Sr_No = (System.Web.UI.WebControls.Label)row.FindControl("lbl_Sr_No");
                int Sr_No = Convert.ToInt32(lbl_Sr_No.Text);

                System.Web.UI.WebControls.TextBox lbl_type = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_type");
                string l_type = lbl_type.Text;

                System.Web.UI.WebControls.TextBox lbl_since = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_since");
                string Effective_Since_txt = lbl_since.Text;

                System.Web.UI.WebControls.TextBox lbl_lower_value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_lower_value");
                string Lower_Value = lbl_lower_value.Text;

                System.Web.UI.WebControls.TextBox lbl_Upper_Value = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_Upper_Value");
                string Upper_Value = lbl_Upper_Value.Text;

                System.Web.UI.WebControls.TextBox lbl_ITax = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_ITax");
                string ITax = lbl_ITax.Text;

                System.Web.UI.WebControls.TextBox lbl_cess = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_cess");
                string Cess = lbl_cess.Text;

                System.Web.UI.WebControls.TextBox lbl_surcharge = (System.Web.UI.WebControls.TextBox)row.FindControl("lbl_surcharge");
                string Surcharge = lbl_surcharge.Text;

                MySqlCommand cmd_tarnsactiondetailsinsert = new MySqlCommand("INSERT INTO pay_income_tax(comp_code,EMP_CODE,type,Effective_Since,Sr_No,Lower_Value,Upper_Value,ITax,Cess,Surcharge) VALUES('" + Session["comp_code"].ToString() + "','" + Session["LOGIN_ID"].ToString() + "','" + l_type.ToString() + "','" + Effective_Since_txt.ToString() + "','" + Convert.ToInt32(Sr_No) + "','" + Lower_Value.ToString() + "','" + Upper_Value.ToString() + "','" + ITax.ToString() + "','" + Cess.ToString() + "','" + Surcharge.ToString() + "')", d1.con);
                    d1.conopen();
                    cmd_tarnsactiondetailsinsert.ExecuteNonQuery();
                    d1.conclose();

                    
                }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);
            }
          
        
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);

        }
        finally
        {
            text_Clear();
            d1.reset(this);
        }


    }

    protected void btndelete_Click(object sender, EventArgs e)
    {


        d1.con1.Open();

        try
        {
            int result = 0;
           
           

            result = d1.operation("DELETE FROM pay_income_tax  WHERE  type = '" + ddl_type.SelectedValue.ToString() + "' AND comp_code='" + Session["comp_code"].ToString() + "' ");//delete command


            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully!!');", true);
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);
                
            }

        }
        catch (Exception ee)
        {

        }
        finally
        {
            text_Clear();
            d1.reset(this);

        }



    }

    public void text_Clear()
    {

        ddl_type.SelectedValue = "Senior Citizen";
            txt_Since.Text="";
            Panel5.Visible = false;

    }

    protected void btnexporttoexceldesignation_Click(object sender, EventArgs e)
    {

        string com_code = Session["comp_code"].ToString();
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 20;

        //Change all cells' alignment to center

        Range rngA = ws.get_Range(ws.Cells[1, 1], ws.Cells[100, 100]);
        rngA.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



        //Cell Color
        Range rng = ws.get_Range("E1:E1");
        rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        //Font Color
        Range formateRange2 = ws.get_Range("E1:E1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        Range rng1 = ws.get_Range("E3:E3");
        rng1.Interior.Color = XlRgbColor.rgbGreen;

        Range formateRange1 = ws.get_Range("E3:E3");
        formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        Range rng3 = ws.get_Range("E5:E6");
        rng3.Interior.Color = XlRgbColor.rgbGreen;

        Range formateRange3 = ws.get_Range("E5:E6");
        formateRange3.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        Range rng2 = ws.get_Range("A7:J7");
        rng2.Interior.Color = XlRgbColor.rgbBlue;

        Range formateRange = ws.get_Range("A7:J7");
        formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //Get the range of cells i.e.., A1:C1.   
        // Range rng5 = ws.get_Range("E5", "E6");
        //Merge the cells.
        // rng5.Merge();

        

        ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        ws.Cells[3, 5] = "GUEST LIST";
        ws.Cells[5, 5] = ddl_type.SelectedValue.ToString();
        ws.Cells[5, 6] = txt_Since.Text;
        ws.Cells[7, 1] = "Sr_No";
        ws.Cells[7, 2] = "Lower_Value";
        ws.Cells[7, 3] = "Upper_Value";
        ws.Cells[7, 4] = "I.Tax %";
        ws.Cells[7, 5] = "Cess";
        ws.Cells[7, 6] = "Surcharge";
       

        d1.con1.Close();

        try
        {
            d1.con1.Open();
            
            DataSet ds2 = new DataSet();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT Sr_No,Lower_Value,Upper_Value,ITax,Cess,Surcharge FROM pay_income_tax where comp_code='" + com_code + "' ORDER BY SR_NO ", d1.con1);

            System.Data.DataTable dt = new System.Data.DataTable();
            adp2.Fill(dt);
            int j = 8;

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
            Response.Write(ee.Message);
        }
        finally
        {
          
        }

    }


    protected void btn_search_Click1(object sender, EventArgs e)
    {
        d1.con1.Open();
        MySqlCommand cmd1 = new MySqlCommand("SELECT type,Effective_Since,Lower_Value,Upper_Value,ITax,Cess,Surcharge FROM pay_income_tax WHERE Effective_Since='"+txt_ref_no.Text+"' AND type='"+ddl_view_type.SelectedItem.Text+"' AND comp_code='"+Session["comp_code"].ToString()+"' ", d1.con1);
      
        DataSet ds1 = new DataSet();
        MySqlDataReader adp1 = cmd1.ExecuteReader();
        if (adp1.HasRows)
        {
            Panel5.Visible = true;
            lbl_error.Visible = false;
            Panel1.Visible = true;
            gv_itemslist.DataSource = adp1;
            gv_itemslist.DataBind();
            ddl_type.SelectedValue = ddl_view_type.SelectedValue.ToString();
            d1.con1.Close();
            cmd1.Dispose();
            adp1.Dispose();
            btn_Update.Text = "Update";
        }
        else 
        { lbl_error.Visible = true;
        Panel1.Visible = false;
        btn_Update.Text = "Save";
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();
        }
    }


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        text_Clear();
        d1.reset(this);
        Panel5.Visible = false;

    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
        }
        catch { }
        Panel1.Visible = true;
        btn_Update.Visible = false;
        btn_Delete.Visible = false;
        lbl_error.Visible = false;
        gv_itemslist.DataSource = null;
        gv_itemslist.DataBind();

    }
}