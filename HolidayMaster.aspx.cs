
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

public partial class HolidayMaster : System.Web.UI.Page
{

    DAL d1 = new DAL();
    DAL d = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d1.getaccess(Session["ROLE"].ToString(), "Holiday Master", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Holiday Master", Session["COMP_CODE"].ToString()) == "R")
        {
            btn_delete.Visible = false;
            btn_add.Visible = false;
            //btn_exel.Visible = false;
           // btnhelp.Visible = false;
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Holiday Master",Session["COMP_CODE"].ToString()) == "U")
        {
            btn_delete.Visible = false;
            
            //btn_exel.Visible = false;
            
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Holiday Master", Session["COMP_CODE"].ToString()) == "C")
        {
            btn_delete.Visible = false;
            //btn_exel.Visible = false;
        }
       
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        btn_add.Visible = false;
        btn_delete.Visible = false;
        btn_update.Visible = false;
        if (!IsPostBack)
        {
            ViewState["id"] = "";
            try
            {
                d.con.Open();
                ddl_department.Items.Clear();
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter("SELECT DEPT_NAME as dept, DEPT_CODE  from pay_department_master WHERE comp_code='" + Session["comp_code"].ToString() + "' ORDER BY DEPT_CODE", d.con);
                adp.Fill(ds);
                ddl_department.DataSource = ds.Tables[0];
                ddl_department.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            ddl_department.Items.Insert(0, "All");

            try
            {
                d.con.Open();
                ddl_unit.Items.Clear();
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter("SELECT UNIT_NAME, UNIT_CODE  from pay_unit_master WHERE comp_code='" + Session["comp_code"].ToString() + "' ORDER BY UNIT_CODE", d.con);
                adp.Fill(ds);
                ddl_unit.DataSource = ds.Tables[0];
                ddl_unit.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
            ddl_unit.Items.Insert(0, "All");
            btn_cancel_Click(null,null);
        }
    }

    protected void save_update(object sender, EventArgs e)
    {
        if (btn_add.Text == "Save" || btn_add.Text == "SAVE")
        {
            btn_add_Click(sender, e);
        }
        else
        {
            btnupdate_Click(sender, e);
        }
    }

    protected void gvholiday_SelectedIndexChanged(object sender, EventArgs e)
    {
         d1.con1.Open();
         try
         {
             MySqlCommand cmdmax = new MySqlCommand("SELECT OCCASSION,DATE_FORMAT(HOLIDAY_DATE,'%d/%m/%Y'),dept_code,unit_code from pay_holiday_master Where comp_code='" + Session["comp_code"].ToString() + "' and id=" + gvholiday.SelectedRow.Cells[0].Text, d1.con1);
             MySqlDataReader drmax = cmdmax.ExecuteReader();
             if (drmax.Read())
             {
                 ViewState["id"] = gvholiday.SelectedRow.Cells[0].Text;
                 txt_occassion.Text = drmax.GetValue(0).ToString();
                 txt_date.Text = drmax.GetValue(1).ToString();
                 ddl_department.SelectedValue = drmax.GetValue(2).ToString(); 
                 ddl_unit.SelectedValue= drmax.GetValue(3).ToString();
                 btn_update.Visible = true;
                 btn_delete.Visible = true;
             }
         }
         catch (Exception ex) { throw ex; }
         finally { d1.con1.Close(); }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {
            d.operation("INSERT INTO pay_holiday_master(comp_code,DEPT_CODE,OCCASSION,HOLIDAY_DATE,UNIT_CODE) VALUES('" + Session["comp_code"].ToString() + "','" + ddl_department.SelectedValue.ToString() + "','" + txt_occassion.Text + "',STR_TO_DATE('" + txt_date.Text + "','%d/%m/%Y'),'" + ddl_unit.SelectedValue.ToString() + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Holiday added successfully!')", true);
            btn_cancel_Click(null, null);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            d.operation("update pay_holiday_master set OCCASSION ='" + txt_occassion.Text + "',HOLIDAY_DATE = STR_TO_DATE('" + txt_date.Text + "','%d/%m/%Y'), DEPT_CODE = '" + ddl_department.SelectedValue + "', UNIT_CODE = '" + ddl_unit.SelectedValue + "' Where comp_code='" + Session["comp_code"].ToString() + "' and id = " + ViewState["id"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Holiday Updated Successfully...')", true);
            btn_cancel_Click(null, null);
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
           
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            result = d1.operation("DELETE FROM pay_holiday_master WHERE comp_code='" + Session["comp_code"].ToString() + "' and id = " + ViewState["id"].ToString());//delete command
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Holiday deleted successfully!!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);
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
    
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    public void text_Clear()
    {
           
    }

    protected void btnexporttoexceldesignation_Click(object sender, EventArgs e)
    {
        //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
        //Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
        //Worksheet ws = (Worksheet)xla.ActiveSheet;
        //xla.Columns.ColumnWidth = 20;

        ////Change all cells' alignment to center

        //Range rngA = ws.get_Range(ws.Cells[1, 1], ws.Cells[100, 100]);
        //rngA.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



        ////Cell Color
        //Range rng = ws.get_Range("E1:E1");
        //rng.Interior.Color = XlRgbColor.rgbDarkGreen;

        ////Font Color
        //Range formateRange2 = ws.get_Range("E1:E1");
        //formateRange2.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //Range rng1 = ws.get_Range("E3:E3");
        //rng1.Interior.Color = XlRgbColor.rgbGreen;

        //Range formateRange1 = ws.get_Range("E3:E3");
        //formateRange1.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //Range rng3 = ws.get_Range("E5:E6");
        //rng3.Interior.Color = XlRgbColor.rgbGreen;

        //Range formateRange3 = ws.get_Range("E5:E6");
        //formateRange3.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        //Range rng2 = ws.get_Range("A7:J7");
        //rng2.Interior.Color = XlRgbColor.rgbBlue;

        //Range formateRange = ws.get_Range("A7:J7");
        //formateRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

        ////Get the range of cells i.e.., A1:C1.   
        //// Range rng5 = ws.get_Range("E5", "E6");
        ////Merge the cells.
        //// rng5.Merge();

        

        //ws.Cells[1, 5] = Session["COMP_NAME"].ToString();
        //ws.Cells[3, 5] = "GUEST LIST";
        //ws.Cells[5, 5] = ddl_type.SelectedValue.ToString();
        //ws.Cells[5, 6] = txt_Since.Text;
        //ws.Cells[7, 1] = "Sr_No";
        //ws.Cells[7, 2] = "Lower_Value";
        //ws.Cells[7, 3] = "Upper_Value";
        //ws.Cells[7, 4] = "I.Tax %";
        //ws.Cells[7, 5] = "Cess";
        //ws.Cells[7, 6] = "Surcharge";
       

        //d1.con1.Close();

        //try
        //{
        //    d1.con1.Open();
            
        //    DataSet ds2 = new DataSet();
        //    MySqlDataAdapter adp2 = new MySqlDataAdapter("SELECT Sr_No,Lower_Value,Upper_Value,ITax,Cess,Surcharge FROM pay_income_tax  ORDER BY SR_NO ", d1.con1);

        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    adp2.Fill(dt);
        //    int j = 8;

        //    foreach (System.Data.DataRow row in dt.Rows)
        //    {

        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            ws.Cells[j, i + 1] = row[i].ToString();
        //        }
        //        j++;
        //    }
        //    xla.Visible = true;
        //}
        //catch (Exception ee)
        //{
        //    Response.Write(ee.Message);
        //}
        //finally
        //{


        //}

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvholiday, "Select$" + e.Row.RowIndex);
        }
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        try
        {
            d1.con1.Open();
            DataSet ds1 = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT ID, OCCASSION As Occassion,DATE_FORMAT(HOLIDAY_DATE,'%d/%m/%Y') As 'Date',(SELECT DEPT_NAME from pay_department_master b WHERE b.dept_code =a.dept_code and a.comp_code = b.comp_code) as Department,(SELECT UNIT_NAME from pay_unit_master c WHERE a.unit_code = c.UNIT_CODE and a.comp_code = c.comp_code) as Unit from pay_holiday_master a Where a.comp_code='" + Session["comp_code"].ToString() + "'", d1.con1);
            adp.Fill(ds1);
            gvholiday.DataSource = ds1;
            gvholiday.DataBind();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            d1.con1.Close();
        }
        ddl_department.SelectedIndex = 0;
        ddl_unit.SelectedIndex = 0;
        txt_occassion.Text = "";
        txt_date.Text = "";
        btn_add.Visible = true;
    }
    protected void gvholiday_PreRender(object sender, EventArgs e)
    {
        try
        {
            gvholiday.UseAccessibleHeader = false;
            gvholiday.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
}