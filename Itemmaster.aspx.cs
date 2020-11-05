using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public partial class Itemmaster : System.Web.UI.Page
{
    DAL d = new DAL();
    ItemMasterBAL imbl = new ItemMasterBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

        if (!IsPostBack)
        {
            // gst_rate();//vikas16/11
           hidde();//vikas 30/03
            DataSet ds = new DataSet();
            ds = imbl.ItemSelect(Session["COMP_CODE"].ToString());
            ItemGridView.DataSource = ds.Tables[0];
            ItemGridView.DataBind();

            btn_add.Visible = true;
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            txt_itemcode.ReadOnly = true;

            txt_itemcode.Text = "";
            txt_itemname.Text = "";
            txt_purchaserate.Text = "0";

            //   txt_salesrate.Text = "0";
            // txt_salesrate.Text = "0";

            ddl_unit.SelectedIndex = 0;
            // txt_vat.SelectedIndex = 0;
            btn_add.Visible = false;
            btn_new_Click(sender, e);

            ddl_unit.Items.Clear();
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT item_unit_name FROM item_unit_master ORDER BY item_unit_name", d.con);
            d.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    ddl_unit.Items.Add(dr_item1[0].ToString());

                }
                dr_item1.Close();
                d.con.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

            // txt_particular.Items.Insert(0, "Select");
            ddl_unit.Items.Insert(0, new ListItem("Select Unit", ""));
            ddl_hsn_code();

        }

    }

    protected void txt_hsn_OnSelectedIndexChanged(object sender, EventArgs e)
    {
          try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
            catch { }
          txt_vat.Items.Clear();
        MySqlCommand cmd = new MySqlCommand("select gst_rate from pay_hsn_master where hsn_code='" + txt_hsn.Text + "' ", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_vat.Items.Add(dr[0].ToString());
                //txt_vat.Text = dr.GetValue(0).ToString();
                //txt_vat.ReadOnly = true;
            }
            dr.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }
    protected void btnnewclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        btn_add.Visible = true;
        btn_cancel_Click(sender, e);
        btn_add.Visible = true;
        btn_edit.Visible = false;
        btn_delete.Visible = false;
        txt_itemname.Text = "";
        txt_itembrand.Text = "";
        txt_itembrand.Text = "";
        //-----------------------------------------------
        d.con1.Open();
        try
        {

            MySqlCommand cmdmax = new MySqlCommand("SELECT MAX(CAST(SUBSTRING(ITEM_CODE, 2, 4) AS UNSIGNED))+1 FROM  pay_item_master WHERE COMP_CODE='" + Session["COMP_CODE"].ToString() + "'", d.con1);
            drmax = cmdmax.ExecuteReader();
            if (!drmax.HasRows)
            {
            }
            else if (drmax.Read())
            {
                string str = drmax.GetValue(0).ToString();
                if (str == "")
                {
                    txt_itemcode.Text = "I0001";
                }
                else
                {
                    int max_itemcode = int.Parse(drmax.GetValue(0).ToString());
                    if (max_itemcode < 10)
                    {
                        txt_itemcode.Text = "I000" + max_itemcode;
                    }
                    else if (max_itemcode > 9 && max_itemcode < 100)
                    {
                        txt_itemcode.Text = "I00" + max_itemcode;
                    }
                    else if (max_itemcode > 99 && max_itemcode < 1000)
                    {
                        txt_itemcode.Text = "I0" + max_itemcode;
                    }

                    else
                    {
                    }
                }
            }
            drmax.Close();
            d.con1.Close();
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            d.con1.Close();
        }
    }

    protected void update_listbox(ListBox listbox1, string listarray1)
    {
        List<string> tagIds = listarray1.Split(',').ToList();
        listbox1.ClearSelection();

        for (int i = 0; i < listbox1.Items.Count; i++)
        {
            foreach (var item in tagIds)
            {
                if (item.Equals(listbox1.Items[i].Value))
                {
                    listbox1.Items[i].Selected = true;
                }
            }
        }
    }

    protected void ddl_hsn_code()
    {
        MySqlDataAdapter adp = new MySqlDataAdapter("select hsn_code from pay_hsn_master  group by hsn_code ", d.con);
        d.con.Open();
        try
        {
            DataTable dt = new DataTable();
            adp.Fill(dt);

            txt_hsn.DataSource = dt;
            txt_hsn.DataTextField = dt.Columns[0].ToString();
            txt_hsn.DataValueField = dt.Columns[0].ToString();
            txt_hsn.DataBind();
            txt_hsn.Items.Insert(0, new ListItem("Select"));
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally { d.con.Close(); }

    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        string cmpcode = Session["COMP_CODE"].ToString();

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            int result = 0;
            if (txt_one_resource_wt.Text == "")
            {
                txt_one_resource_wt.Text = "0";
            }
            if (ddl_uniform_size.SelectedValue == "")
            {
                ddl_uniform_size.SelectedValue = "Select";
            }
            result = imbl.ItemInsert(cmpcode, txt_itemcode.Text, txt_itemname.Text.ToString(), ddl_unit.SelectedValue, Convert.ToDouble(txt_one_resource_wt.Text.ToString()), Convert.ToDouble(txt_vat.SelectedValue), ddl_product.SelectedValue.ToString(), txt_item_description.Text.ToString(), txt_sac.Text.ToString(), txt_itembrand.Text, txt_hsn.SelectedValue, txt_purchaserate.Text, txt_sale_rate.Text, txt_validity.Text, ddl_uniform_size.SelectedValue);
            string item = txt_itemname.Text;

            if (result > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material (" + item + ") added successfully!!');", true);

                txt_hsn.SelectedValue = "Select";

                txt_itemcode.Text = "";
                txt_itemname.Text = "";
                txt_itembrand.Text = "";
                ddl_unit.SelectedIndex = 0;
              //  txt_vat.Text = "";
                txt_one_resource_wt.Text = "0";
                txt_sale_rate.Text = "0";
                txt_purchaserate.Text = "0";
                txt_validity.Text = "";
                ddl_product.SelectedValue = "Select";
                txt_item_description.Text = "";
                //txt_item_description.Text = "";
                //add new 12/04/2019 6.50
                ddl_uniform_size.SelectedValue = "Select";
                txt_sac.Text = "";
                btn_new_Click(sender, e);

            }
            else
            {
                d.operation("Delete from pay_item_master Where ITEM_CODE ='" + txt_itemcode.Text + "'");
                d.operation("Delete from pay_profit_loss Where ITEM_CODE ='" + txt_itemcode.Text + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material (" + txt_itemname.Text + ") adding failed...');", true);

            }

        }
        catch (Exception ee)
        {
            d.operation("Delete from pay_item_master Where ITEM_CODE ='" + txt_itemcode.Text + "'");
            d.operation("Delete from pay_profit_loss Where ITEM_CODE ='" + txt_itemcode.Text + "'");
        }
        finally
        {
            DataSet ds = new DataSet();
            ds = imbl.ItemSelect(Session["COMP_CODE"].ToString());
            ItemGridView.DataSource = ds.Tables[0];
            ItemGridView.DataBind();

        }
    }
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        string cmpcode = Session["COMP_CODE"].ToString();
        int result = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            if (txt_one_resource_wt.Text == "")
            {
                txt_one_resource_wt.Text = "0";
            }
            result = imbl.ItemUpdate(cmpcode, txt_itemcode.Text, txt_itemname.Text.ToString(), ddl_unit.SelectedValue, Convert.ToDouble(txt_one_resource_wt.Text.ToString()), Convert.ToDouble(txt_vat.SelectedValue), ddl_product.SelectedValue.ToString(), txt_item_description.Text.ToString(), txt_sac.Text.ToString(), txt_itembrand.Text, txt_hsn.SelectedValue, txt_purchaserate.Text, txt_sale_rate.Text, txt_validity.Text,ddl_uniform_size.SelectedValue.ToString());



            if (result > 0)
            {
                string item = txt_itemname.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Material (" + item + ") updated successfully!!');", true);

                txt_itemcode.Text = "";
                txt_itemname.Text = "";
                txt_itembrand.Text = "";
                txt_hsn.SelectedValue = "Select";
                ddl_unit.SelectedIndex = 0;
                ddl_product.SelectedValue = "Select";
                //  txt_vat.Text = "";
                txt_one_resource_wt.Text = "0";
                txt_purchaserate.Text = "0";
                txt_sale_rate.Text = "0";
                txt_validity.Text = "";
                btn_edit.Visible = false;
                btn_new_Click(sender, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material (" + txt_itemname.Text + ") updation failed...');", true);

            }

        }
        catch (Exception ex)
        {
            string error = ex.Message.ToString().Replace("'", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + error + "');", true);

        }
        finally
        {
            DataSet ds = new DataSet();
            ds = imbl.ItemSelect(Session["COMP_CODE"].ToString());
            ItemGridView.DataSource = ds.Tables[0];
            ItemGridView.DataBind();

        }
    }
    MySqlDataReader drdesig = null;
    MySqlDataReader drmax = null;


    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string cmpcode = Session["COMP_CODE"].ToString();
        int result = 0;
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            // MySqlCommand cmd_1 = new MySqlCommand("Select ITEM_CODE from pay_transaction_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' UNION Select ITEM_CODE from pay_transactionp_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' UNION Select ITEM_CODE from pay_transactionq_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' UNION Select ITEM_CODE from pay_cashmemo_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' UNION Select ITEM_CODE from pay_transactionproforma_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "'", d.con1);
            MySqlCommand cmd_1 = new MySqlCommand("Select ITEM_CODE from pay_transaction_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "' UNION Select ITEM_CODE from pay_transactionp_details Where ITEM_CODE ='" + txt_itemcode.Text + "' AND COMP_CODE = '" + Session["COMP_CODE"] + "'   ", d.con1);
            if (d.con1.State == ConnectionState.Open)
            {
                d.con1.Close();
                d.con1.Dispose();
                d.con1.ClearPoolAsync(d.con1);
            }

            d.con1.Open();
            MySqlDataReader dr_1 = cmd_1.ExecuteReader();
            if (dr_1.Read())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material Deletion Failed : Material exists in one of Invoice/Cash Memo/Quotation Bills.');", true);
            }
            else
            {
                result = imbl.ItemDelete(cmpcode, txt_itemcode.Text);
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material Data deleted successfully!!');", true);
                    txt_itemcode.Text = "";
                    txt_itemname.Text = "";
                    txt_itembrand.Text = "";
                    ddl_unit.SelectedIndex = 0;
                   // txt_vat.Text = "0";
                    txt_hsn.SelectedValue = "Select";
                    txt_one_resource_wt.Text = "0";
                    txt_purchaserate.Text = "0";
                    txt_sale_rate.Text = "0";
                    txt_validity.Text = "";
                    btn_delete.Visible = false;
                    ddl_product.SelectedValue = "Select";

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deletion failed...');", true);
                }
            }
            d.con.Close();
        }
        catch (Exception ee)
        {
            //lblerrmsg.ForeColor = System.Drawing.Color.Red;
            //lblerrmsg.Text = ee.Message;
        }
        finally
        {
            DataSet ds = new DataSet();
            ds = imbl.ItemSelect(Session["COMP_CODE"].ToString());
            ItemGridView.DataSource = ds.Tables[0];
            ItemGridView.DataBind();
            d.con1.Close();
            btn_cancel_Click(sender, e);
            btn_new_Click(sender, e);
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
         try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
         catch { }
        txt_itemcode.Text = "";
        txt_itemname.Text = "";
        txt_itembrand.Text = "";
        ddl_unit.SelectedIndex = 0;

        txt_one_resource_wt.Text = "0";
        btn_edit.Visible = false;
        btn_delete.Visible = false;
        txt_purchaserate.Text = "0";
        txt_sac.Text = "";
        txt_purchaserate.Text = "0";
        txt_sale_rate.Text = "0";
        txt_validity.Text = "";
        ddl_product.SelectedValue = "Select";
        txt_item_description.Text = "";
        txt_validity.Text = "";

        //btn_new_Click(sender, e);
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void ItemGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ITEM_CODE,ITEM_NAME,unit,unit_per_piece,VAT,product_service,item_description,sac_number,item_brand,hsn_number

        //   imbl.ItemSelect(Session["COMP_CODE"].ToString());
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);}
        catch { }
        txt_itemcode.Text = ItemGridView.SelectedRow.Cells[1].Text;
        txt_itemname.Text = ItemGridView.SelectedRow.Cells[3].Text;
        // ddl_unit.SelectedValue = ItemGridView.SelectedRow.Cells[3].Text;
        txt_one_resource_wt.Text = ItemGridView.SelectedRow.Cells[7].Text;

        ddl_product.SelectedValue = ItemGridView.SelectedRow.Cells[2].Text;
        ddl_unit.SelectedValue = ItemGridView.SelectedRow.Cells[6].Text;
        txt_item_description.Text = ItemGridView.SelectedRow.Cells[4].Text;
        txt_sac.Text = ItemGridView.SelectedRow.Cells[5].Text;
       
        txt_itembrand.Text = ItemGridView.SelectedRow.Cells[9].Text;
        txt_hsn.SelectedValue = ItemGridView.SelectedRow.Cells[10].Text;
        txt_hsn_OnSelectedIndexChanged(null, null);
        txt_vat.SelectedValue = ItemGridView.SelectedRow.Cells[8].Text;
        txt_purchaserate.Text = ItemGridView.SelectedRow.Cells[11].Text;
        txt_sale_rate.Text = ItemGridView.SelectedRow.Cells[12].Text;
        txt_validity.Text = ItemGridView.SelectedRow.Cells[13].Text;
        ddl_product_SelectedIndexChanged(null, null);
        ddl_uniform_size.SelectedValue = ItemGridView.SelectedRow.Cells[14].Text;

        btn_add.Visible = false;
        btn_edit.Visible = true;
        btn_delete.Visible = true;

        // txt_itemcode.ReadOnly = false;

    }
    protected void ItemGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.ItemGridView, "Select$" + e.Row.RowIndex);

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        if (ItemGridView.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ItemMaster_excel_data.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                ItemGridView.AllowPaging = false;
                foreach (TableCell cell in ItemGridView.HeaderRow.Cells)
                {
                    cell.BackColor = ItemGridView.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in ItemGridView.Rows)
                {

                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = ItemGridView.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = ItemGridView.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }
                ItemGridView.RenderControl(hw);
                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }


    protected void hide_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_unit_click(object sender, EventArgs e)
    {

        MySqlCommand cmd_item1 = new MySqlCommand("SELECT item_pieces FROM item_unit_master where item_unit_name='" + ddl_unit.SelectedValue.ToString() + "'", d.con);
        d.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txt_one_resource_wt.Text = dr_item1.GetValue(0).ToString();

            }
            dr_item1.Close();
            d.con.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void ItemGridView_PreRender(object sender, EventArgs e)
    {
        try
        {
            ItemGridView.UseAccessibleHeader = false;
            ItemGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch

    }

    protected void lnkaddtravelplan_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Show();

    }
    //vikas15/11
    protected void lnk_gst_rate_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    //protected void gst_rate()
    //{
    //    System.Data.DataTable dt_item = new System.Data.DataTable();
    //    MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select gst_rate from pay_gst_rate ORDER BY id", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        cmd_item.Fill(dt_item);
    //        if (dt_item.Rows.Count > 0)
    //        {
    //            txt_vat.DataSource = dt_item;
    //            txt_vat.DataTextField = dt_item.Columns[0].ToString();

    //            txt_vat.DataBind();
    //        }
    //        dt_item.Dispose();

    //        d.con.Close();
    //        txt_vat.Items.Insert(0, "Select");
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }
    //}
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Itemmaster.aspx");
    }
    protected void ddl_product_SelectedIndexChanged(object sender, EventArgs e)
    {
          try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
     catch { }
        hidde();
        if (ddl_product.SelectedValue == "Uniform")
        {
           // d.iteam_size("Uniform");
            ddl_uniform_size.DataSource = d.iteam_size("Uniform");
            ddl_uniform_size.DataBind();
            ddl_uniform_size.Items.Insert(0, "Select");
          
           
        }
        else if (ddl_product.SelectedValue == "pantry_jacket")
        {
            ddl_uniform_size.DataSource = d.iteam_size("pantry_jacket");
            ddl_uniform_size.DataBind();
            ddl_uniform_size.Items.Insert(0, "Select");

        }
        else if (ddl_product.SelectedValue == "Shoes")
        {
            ddl_uniform_size.DataSource = d.iteam_size("Shoes");
            ddl_uniform_size.DataBind();
            ddl_uniform_size.Items.Insert(0, "Select");
        }
        else if (ddl_product.SelectedValue == "Apron")
        {
            ddl_uniform_size.DataSource = d.iteam_size("Apron");
            ddl_uniform_size.DataBind();
            ddl_uniform_size.Items.Insert(0, "Select");
        }
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "ChangeShow()", true); }
        catch { }
    }

    protected void hidde()
    {
        if (ddl_product.SelectedValue == "Uniform")
        {
            lb_uniform_size.Visible = true;
            ddl_uniform_size.Visible = true;
        }
        else if (ddl_product.SelectedValue == "pantry_jacket")
        {
            lb_pantry_jacket_size.Visible = true;
           // ddl_pantry_jacket_size.Visible = true;
            ddl_uniform_size.Visible = true;
        }
        else if (ddl_product.SelectedValue == "Shoes")
        {
            lb_shoes_size.Visible = true;
            //ddl_shoes_size.Visible = true;
            ddl_uniform_size.Visible = true;
        }
        else
        {
            //lb_uniform_size.Visible = false;
            //ddl_uniform_size.Visible = false;
            //lb_uniform_size.Visible = false;
            //ddl_uniform_size.Visible = false;
            //lb_shoes_size.Visible = false;
          
        }
    }
   
   
}