using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;



public partial class ToDoList : System.Web.UI.Page
{

    DAL d = new DAL();
   

    protected void Page_Load(object sender, EventArgs e)
    {
		if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
		
        if (d.getaccess(Session["ROLE"].ToString(), "TO DO LIST", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "TO DO LIST", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = false;
            btn_export_exel.Visible = false;
          //  btn_new.Visible = false;
            btn_search.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "TO DO LIST", Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            btn_export_exel.Visible = false;
           
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "TO DO LIST", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            btn_export_exel.Visible = false;
        }
        



        if (!IsPostBack)
        {
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            gridview();
        }

        //DataSet ds = new DataSet();
        //ds = d.getData("SELECT Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list");
        //SearchGridView.DataSource = ds.Tables["0"];
        //SearchGridView.DataBind();

       // Panel2.Visible = true;

        }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void gridview()
    {
        d.con.Open();
        MySqlDataAdapter adp = new MySqlDataAdapter("Select client_code,state,unit_code,date_format(OPERATION_DATE,'%d/%m/%Y') as OPERATION_DATE,start_time,end_time From pay_op_management_details where emp_code = '" + Session["LOGIN_ID"].ToString() + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'", d.con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv_itemslist.DataSource = ds.Tables[0];
            gv_itemslist.DataBind();
        }
        else
        {
            gv_itemslist.DataSource = null;
            gv_itemslist.DataBind();
           
        }
        d.con.Close();
    }
    protected void gv_itemslist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[0].Visible = false;
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
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_itemslist, "Select$" + e.Row.RowIndex);
        }
    }
    protected void gv_itemslist_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_itemslist.UseAccessibleHeader = false;
            gv_itemslist.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;

            result = d.operation("Insert into pay_to_do_list (comp_code,EMP_CODE,Task_Name,Task_Description,Start_Date,Remind_Till,Reminder) values ('" + Session["comp_code"] + "','" + Session["LOGIN_ID"] + "','" + txt_Task_Name.Text + "','" + txt_Task_Description.Text + "',STR_TO_DATE('" + txt_Start_Date.Text + "','%d/%m/%Y'),STR_TO_DATE('" + txt_Remind_Till.Text + "','%d/%m/%Y'), '" + ddl_Reminder.SelectedValue + "')");


            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record added successfully!!');", true);
                text_Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record adding failed...');", true);
                text_Clear();
            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
        }
        finally
        {
            //DataSet ds = new DataSet();
            //ds = d.getData("SELECT Id as 'ID',Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list");
            //SearchGridView.DataSource = null;
            //SearchGridView.DataBind();
            //SearchGridView.DataSource = ds.Tables["pay_to_do_list"];
            //SearchGridView.DataBind();

            Panel2.Visible = true;
            d.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT ID as 'ID',Task_Name as 'Task_Name',date_format(Start_Date,'%d/%m/%Y') as 'Start_Date' ,Task_Description as 'Task_Description' ,Reminder as 'Reminder' , Id As 'TASK_ID' FROM pay_to_do_list WHERE ((pay_to_do_list.Task_Name LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Start_Date LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Task_Description LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Reminder LIKE '%" + txt_ref_no.Text + "%')) AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds1.Tables[0];
            SearchGridView.DataBind();
            d.con1.Close();

        }

    }

    public void text_Clear()
    {
        txt_Task_Name.Text = "";
        txt_Task_Description.Text = "";
        txt_Start_Date.Text = "";
        txt_Remind_Till.Text = "";
        ddl_Reminder.SelectedValue = "Select Frequency";


    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        d.reset(this);
        text_Clear();
        btn_edit.Visible = false;
        btn_add.Visible = true;
    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        try
        {


            System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
            string TaskName = lbl_id.Text;

            d.operation("update pay_to_do_list set  Task_Name='" + txt_Task_Name.Text + "',Task_Description= '" + txt_Task_Description.Text + "',Start_Date=STR_TO_DATE('" + txt_Start_Date.Text + "','%d/%m/%Y'),Remind_Till=STR_TO_DATE('" + txt_Remind_Till.Text + "','%d/%m/%Y'),Reminder='" + ddl_Reminder.SelectedValue + "' where ID= " + TaskName);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully...')", true);

        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);

        }
        finally
        {
            //DataSet ds = new DataSet();
            //ds = d.getData("SELECT ID as 'ID',Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list WHERE comp_code='" + Session["comp_code"].ToString() + "'");
            //SearchGridView.DataSource = null;
            //SearchGridView.DataBind();
            //SearchGridView.DataSource = ds.Tables[0];
            //SearchGridView.DataBind();

            Panel2.Visible = true;
            d.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT ID as 'ID',Task_Name as 'Task_Name',date_format(Start_Date,'%d/%m/%Y') as 'Start_Date' ,Task_Description as 'Task_Description' ,Reminder as 'Reminder' , Id As 'TASK_ID' FROM pay_to_do_list WHERE ((pay_to_do_list.Task_Name LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Start_Date LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Task_Description LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Reminder LIKE '%" + txt_ref_no.Text + "%')) AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds1.Tables[0];
            SearchGridView.DataBind();
            d.con1.Close();
            text_Clear();


           // d.reset(this);
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = true;
        }


    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
        string TaskName = lbl_id.Text;

        MySqlCommand cmd_1 = new MySqlCommand("Select Task_Name from pay_to_do_list  where  ID= " + TaskName, d.con1);
        d.con1.Close();
        d.con1.Open();
        int result = 0;
        try
        {
            result = d.operation("DELETE FROM pay_to_do_list  WHERE ID=" + TaskName);//delete command
            d.reset(this);

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(ee);", true);
        }
        finally
        {
           // Panel2.Visible = true;
           // d.con1.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT ID as 'ID',Task_Name as 'Task_Name',date_format(Start_Date,'%d/%m/%Y') as 'Start_Date' ,Task_Description as 'Task_Description' ,Reminder as 'Reminder' , Id As 'TASK_ID' FROM pay_to_do_list WHERE ((pay_to_do_list.Task_Name LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Start_Date LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Task_Description LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Reminder LIKE '%" + txt_ref_no.Text + "%')) AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
            adp1.Fill(ds1);
            SearchGridView.DataSource = null;
            SearchGridView.DataBind();
            SearchGridView.DataSource = ds1.Tables[0];
            SearchGridView.DataBind();
            d.con1.Close();
            text_Clear();

            //DataSet ds = new DataSet();
            //ds = d.getData("SELECT Membership_No,Husband_Name,Wife_Name,Cell_Number as 'Mobile_Number',City FROM enquiry_form ");
            //SearchGridView.DataSource = ds.Tables[0];
            //SearchGridView.DataBind();

            //text_Clear();
            ////d.reset(this);
            btn_delete.Visible = false;
            btn_edit.Visible = false;
            btn_add.Visible = true;
        }



    }

    protected void btn_search_Click1(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        d.con1.Open();
        MySqlCommand cmd1 = new MySqlCommand("SELECT ID as 'ID',Task_Name as 'Task_Name',date_format(Start_Date,'%d/%m/%Y') as 'Start_Date' ,Task_Description as 'Task_Description' ,Reminder as 'Reminder' , Id As 'TASK_ID' FROM pay_to_do_list WHERE ((pay_to_do_list.Task_Name LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Start_Date LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Task_Description LIKE '%" + txt_ref_no.Text + "%') OR (pay_to_do_list.Reminder LIKE '%" + txt_ref_no.Text + "%')) AND comp_code='" + Session["comp_code"].ToString() + "'", d.con1);
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
        adp1.Fill(ds1);
        SearchGridView.DataSource = null;
        SearchGridView.DataBind();
        SearchGridView.DataSource = ds1.Tables[0];
        SearchGridView.DataBind();
        d.con1.Close();
        cmd1.Dispose();
        ds1.Dispose();
        adp1.Dispose();
    }

    protected void btnexporttoexceldesignation_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        Worksheet ws = (Worksheet)xla.ActiveSheet;
        xla.Columns.ColumnWidth = 20;



        //Change all cells' alignment to center
        Range rng12 = ws.get_Range(ws.Cells[1, 1], ws.Cells[200, 300]);
        rng12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


        Range rng = ws.get_Range("C1:C1");
        rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        Range formateRange2 = ws.get_Range("C1:C1");
        formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        Range rng1 = ws.get_Range("C3:C3");
        rng1.Interior.Color = XlRgbColor.rgbGreen;

        Range formateRange1 = ws.get_Range("C3:C3");
        formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        Range rng2 = ws.get_Range("A5:E5");
        rng2.Interior.Color = XlRgbColor.rgbBlue;

        Range formateRange = ws.get_Range("A5:E5");
        formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);


        ws.Cells[1, 3] = Session["COMP_NAME"].ToString();
        ws.Cells[3, 3] = "TO DO LIST";
        ws.Cells[5, 1] = "Task_Name";
        ws.Cells[5, 2] = "Task_Description";
        ws.Cells[5, 3] = "Start_Date";
        ws.Cells[5, 4] = "Remind_Till";
        ws.Cells[5, 5] = "Reminder";
        
        d.con1.Close();

        try
        {
            d.con1.Open();
            //  MySqlCommand cmd2 = new MySqlCommand("SELECT Membership_No,Membership_Name,date_format(Joining_date,'%d/%m/%Y') as Joining_date,NumberOfYearsHolidays,Holiday_Used,Holiday_UnUsed FROM holidays   ORDER BY Membership_No ", d.con1);
            DataSet ds2 = new DataSet();
            MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list WHERE comp_code='" + Session["comp_code"].ToString() + "'", d.con1);

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
            Response.Write(ee.Message);
        }
        finally
        {
        }

    }


    protected void SearchGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            System.Web.UI.WebControls.Label lbl_id = (System.Web.UI.WebControls.Label)SearchGridView.SelectedRow.FindControl("lbl_id");
            string Id = lbl_id.Text;


            d.con1.Open();
            MySqlCommand cmd2 = new MySqlCommand("SELECT Task_Name,Task_Description,date_format(Start_Date,'%d/%m/%Y') as Start_Date,date_format(Remind_Till,'%d/%m/%Y') as Remind_Till,Reminder FROM pay_to_do_list WHERE comp_code='" + Session["comp_code"].ToString() + "' AND Id=" + Id + "", d.con);
            d.conopen();
            MySqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                txt_Task_Name.Text = dr.GetValue(0).ToString();
                txt_Task_Description.Text = dr.GetValue(1).ToString();

                string date = dr.GetValue(2).ToString();
                if (date == "")
                {
                    txt_Start_Date.Text = dr.GetValue(2).ToString();
                }
                else
                {

                    txt_Start_Date.Text = date.ToString();
                }

                string remind = dr.GetValue(3).ToString();
                if (remind == "")
                {
                    txt_Remind_Till.Text = dr.GetValue(3).ToString();
                }
                else
                {

                    txt_Remind_Till.Text = remind.ToString();
                }

             
                ddl_Reminder.Text = dr.GetValue(4).ToString();

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Successfully...!')", true);
            }
            dr.Close();
            d.conclose();
            btn_delete.Visible = true;
            btn_edit.Visible = true;
            btn_add.Visible = false;
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Failed...!')", true);
        }
        finally
        {

            
        }


        //------------------------------------------------------------------------------------------------------------------------------

    }


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.SearchGridView, "Select$" + e.Row.RowIndex);
        }
    }
    protected void SearchGridView_PreRender(object sender, EventArgs e)
    {
          try
        {
            SearchGridView.UseAccessibleHeader = false;
            SearchGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
   
}
