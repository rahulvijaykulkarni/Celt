using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Machin_Maintainance_Tracking : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }

       

        if (!IsPostBack)
        {
            client();
            Machine_list();
            service_gv();
        }
    }

    protected void client()
    {
        ddl_client.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT distinct(pay_client_state_role_grade.`client_code`),`client_name` FROM `pay_client_master` INNER JOIN  pay_client_state_role_grade ON pay_client_master.COMP_CODE = pay_client_state_role_grade.COMP_CODE AND pay_client_master.client_code = pay_client_state_role_grade.client_code WHERE pay_client_state_role_grade.`comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") AND material_rental_policy = '1' order by `client_code`", d.con);
        d.con.Open();
        try
        {
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_client.DataSource = dt_item;
                ddl_client.DataTextField = dt_item.Columns[1].ToString();
                ddl_client.DataValueField = dt_item.Columns[0].ToString();
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

    protected void Machine_list()
    {
        ddl_machine.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select ITEM_CODE,`ITEM_NAME` from pay_item_master where `product_service`='Machine'", d.con);
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
                ddl_machine_service.DataSource = dt_item;
                ddl_machine_service.DataValueField = dt_item.Columns[0].ToString();
                ddl_machine_service.DataTextField = dt_item.Columns[1].ToString();
                ddl_machine_service.DataBind();
            }
            dt_item.Dispose();

            d.con.Close();
            ddl_machine.Items.Insert(0, "Select");
            ddl_machine_service.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }
    protected void btn_add_machine_Click(object sender, EventArgs e)
    {
        try { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true); }
        catch { }
        int res = 0;
        res = d.operation("Insert Into pay_machine_rental_details (COMP_CODE,machine_name,machine_code,CLIENT_CODE,STATE,unit_code,rental_from,rental_to,modify_by,modify_date) VALUES ('" + Session["COMP_CODE"].ToString() + "','" + ddl_machine.SelectedValue + "','" + ddl_machine.SelectedItem.Text + "','" + ddl_client.SelectedValue + "','" + ddl_state.SelectedValue + "','" + ddl_location.SelectedValue + "',str_to_date('" + txt_from_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_to_date.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");
        if (res > 0)
        {
         
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records Inserted successfully!!');", true);
          
        }
     string   client_code = ddl_client.SelectedValue.ToString();
        machine_gv(client_code);
        clear();
    }

    protected void lnkbtn_services_removeitem_Click(object sender, EventArgs e)
    {
        int res = 0;
         GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
        string id = row.Cells[1].Text;
        string client_code = d.getsinglestring("select client_code from pay_machine_rental_details where id='" + id + "'");
        res = d.operation("Delete from pay_machine_rental_details Where id ='" + id + "'");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records Deleted successfully!!');", true);
        }
        machine_gv(client_code);
    }
  
    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_location.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' and state_name='" + ddl_state.SelectedValue + "' and UNIT_CODE in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ")  AND client_code='" + ddl_client.SelectedValue + "' AND state_name='" + ddl_state.SelectedValue + "') AND branch_status = 0 ORDER BY UNIT_CODE", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_location.DataSource = dt_item;
                ddl_location.DataTextField = dt_item.Columns[0].ToString();
                ddl_location.DataValueField = dt_item.Columns[1].ToString();

                ddl_location.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_location.Items.Insert(0, "Select");
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
           
        }
    }
    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_state.Items.Clear();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT (`STATE_NAME`) FROM `pay_client_state_role_grade` WHERE `comp_code` = '" + Session["COMP_CODE"].ToString() + "' AND `client_code` = '" + ddl_client.SelectedValue + "' AND `pay_client_state_role_grade`.`emp_code` IN (" + Session["REPORTING_EMP_SERIES"].ToString() + ") order by 1", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                ddl_state.DataSource = dt_item;
                ddl_state.DataTextField = dt_item.Columns[0].ToString();
                ddl_state.DataValueField = dt_item.Columns[0].ToString();
                ddl_state.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();
            ddl_state.Items.Insert(0, "Select");
            string client_code = ddl_client.SelectedValue.ToString();
            machine_gv(client_code);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();
        }
    }

    protected void machine_gv(string client_code)
    {
        gv_services.DataSource = null;
        gv_services.DataBind();
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT pay_machine_rental_details.id,machine_name,`CLIENT_Name`as client_code,`pay_machine_rental_details`.`STATE`, unit_name as unit_code,  DATE_FORMAT(`rental_from`, '%d/%m/%Y') AS 'rental_from', DATE_FORMAT(`rental_to`, '%d/%m/%Y') AS 'rental_to' FROM `pay_machine_rental_details` INNER JOIN `pay_client_master` ON `pay_machine_rental_details`.`client_code` = `pay_client_master`.`client_code` AND `pay_machine_rental_details`.`comp_code` = `pay_client_master`.`comp_code`  INNER JOIN `pay_unit_master` ON `pay_machine_rental_details`.`unit_code` = `pay_unit_master`.`unit_code` AND `pay_machine_rental_details`.`comp_code` = `pay_unit_master`.`comp_code` and `pay_machine_rental_details`.`client_code` = `pay_unit_master`.`client_code` where pay_machine_rental_details.comp_code='" + Session["comp_code"] + "' and pay_machine_rental_details.client_code = '" + client_code + "' ", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                gv_services.DataSource = dt_item;
                gv_services.DataBind();
                ViewState["servicestable"] = dt_item;
            }
            dt_item.Dispose();
            d.con.Close();
           
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();

        }
    }
    protected void btn_save_service_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string str = ddl_machine_service.SelectedValue;
        str = str.Replace(" ", "");
        string date1 = txt_form_date_service.Text.ToString();
        date1 = date1.Replace(@"/", "_");
        int res = 0;
        res = d.operation("insert into pay_machine_servicing_details(machine_code,machine_name,servicing_from_date,servicing_to_date,warranty_type,warranty_in,next_servicing_date,modify_by,modify_date)values('" + ddl_machine_service.SelectedValue + "','" + ddl_machine_service.SelectedItem.Text + "',str_to_date('" + txt_form_date_service.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date_service.Text + "','%d/%m/%Y'),'" + ddl_warranty.SelectedValue + "','" + txt_warranty.Text + "',str_to_date('" + txt_nxt_service.Text + "','%d/%m/%Y'),'" + Session["LOGIN_ID"].ToString() + "',now())");

        string date = txt_form_date_service.Text;
        if (service_document_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(service_document_file.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {
                
                    string fileName = Path.GetFileName(service_document_file.PostedFile.FileName);
                    service_document_file.PostedFile.SaveAs(Server.MapPath("~/machine_sevices/") + fileName);

                    File.Copy(Server.MapPath("~/machine_sevices/") + fileName, Server.MapPath("~/machine_sevices/") + str + "_" + date1 + "" + fileExt, true);
                    File.Delete(Server.MapPath("~/machine_sevices/") + fileName);
                    string id = d.getsinglestring("select max(id) from pay_machine_servicing_details");
                    d.operation("UPDATE pay_machine_servicing_details SET image = '" + str + "_" + date1 + "" + fileExt + "' where id='"+id+"'");
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
                string id = d.getsinglestring("select max(id) from pay_machine_servicing_details");
                d.operation("delete from pay_machine_servicing_details where id='"+id+"' ");
            }

        }
        if (res > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Records Add successfully!!')", true);
            service_gv();
            clear_service();
        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void service_gv()
    {
        System.Data.DataTable dt_item = new System.Data.DataTable();
        MySqlDataAdapter cmd_item = new MySqlDataAdapter("select  id,`machine_name`, date_format(servicing_from_date,'%d/%m/%Y') as `servicing_from_date`, date_format(servicing_to_date,'%d/%m/%Y') as`servicing_to_date`, `warranty_type`, `warranty_in`, date_format(next_servicing_date,'%d/%m/%Y') as`next_servicing_date`,image as que_4_path from pay_machine_servicing_details ", d.con);
        d.con.Open();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            cmd_item.Fill(dt_item);
            if (dt_item.Rows.Count > 0)
            {
                gv_machine_s.DataSource = dt_item;
                gv_machine_s.DataBind();
            }
            dt_item.Dispose();
            d.con.Close();

        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d.con.Close();

        }
    }
   
    protected void gv_machine_s_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        var id = gv_machine_s.SelectedRow.Cells[0].Text;
         d.con1.Open();
         try
         {

             MySqlCommand cmd = new MySqlCommand("select `machine_code`, date_format(servicing_from_date,'%d/%m/%Y') as `servicing_from_date`, date_format(servicing_to_date,'%d/%m/%Y') as`servicing_to_date`, `warranty_type`, `warranty_in`, date_format(next_servicing_date,'%d/%m/%Y') as`next_servicing_date` ,id from pay_machine_servicing_details where id='" + id + "' ", d.con1);
             MySqlDataReader dr = cmd.ExecuteReader();
             if (dr.Read())
             {
                 ddl_machine_service.SelectedValue = dr.GetValue(0).ToString();
                 txt_form_date_service.Text = dr.GetValue(1).ToString();
                 txt_end_date_service.Text = dr.GetValue(2).ToString();
                 ddl_warranty.SelectedValue = dr.GetValue(3).ToString();
                 txt_warranty.Text = dr.GetValue(4).ToString();
                 txt_nxt_service.Text = dr.GetValue(5).ToString();
                 txt_id.Text = dr.GetValue(6).ToString();
             }
         }
         catch (Exception ex)
         {
         }
         finally
         {
         }
    }
    protected void gv_machine_s_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_machine_s, "Select$" + e.Row.RowIndex);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string imageUrl = "";
            if (dr["que_4_path"].ToString() != "")
            {

                imageUrl = "~/machine_sevices/" + dr["que_4_path"];
                (e.Row.FindControl("que_4_path") as Image).ImageUrl = imageUrl;

            }
        }
        e.Row.Cells[0].Visible = false;
    }
    protected void btn_update_service_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
        string str = ddl_machine_service.SelectedValue;
        str = str.Replace(" ", "");
        string date1 = txt_form_date_service.Text.ToString();
        date1 = date1.Replace(@"/", "_");
        if (service_document_file.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(service_document_file.FileName);
            if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".png" || fileExt.ToLower() == ".pdf" || fileExt.ToLower() == ".jpeg")
            {

                string fileName = Path.GetFileName(service_document_file.PostedFile.FileName);
                service_document_file.PostedFile.SaveAs(Server.MapPath("~/machine_sevices/") + fileName);

                File.Copy(Server.MapPath("~/machine_sevices/") + fileName, Server.MapPath("~/machine_sevices/") + str + "_" + date1 + "" + fileExt, true);
                File.Delete(Server.MapPath("~/machine_sevices/") + fileName);
                d.operation("UPDATE pay_machine_servicing_details SET image = '" + str + "_" + date1 + "" + fileExt + "' where id='" + txt_id.Text + "' ");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select only JPG and PNG Images !!!')", true);
            }

        }
        int res=0;
        res = d.operation("UPDATE pay_machine_servicing_details SET machine_code='" + ddl_machine_service.SelectedValue + "', machine_name='" + ddl_machine_service.SelectedItem.Text + "',servicing_from_date=str_to_date('" + txt_form_date_service.Text + "','%d/%m/%Y'),servicing_to_date=str_to_date('" + txt_end_date_service.Text + "','%d/%m/%Y'),warranty_type='" + ddl_warranty.SelectedValue + "',warranty_in='" + txt_warranty.Text + "',next_servicing_date=str_to_date('" + txt_nxt_service.Text + "','%d/%m/%Y') where id='" + txt_id.Text + "' ");
        if (res > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records Update successfully!!');", true);

        }
        service_gv();
        clear_service();                                              
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        hidtab.Value = "1";
      
        int res = 0;
        res = d.operation("Delete from pay_machine_servicing_details Where id ='" + txt_id.Text + "'");
        if (res > 0)
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records Deleted successfully!!');", true);
        }
        clear_service();
        service_gv();
    }
    protected void btn_close1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void gv_services_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_services.UseAccessibleHeader = false;
            gv_services.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void gv_machine_s_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_machine_s.UseAccessibleHeader = false;
            gv_machine_s.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }
    protected void clear()
    {
        ddl_machine.SelectedValue = "Select";
        ddl_client.SelectedValue = "Select";
        ddl_state.SelectedValue = "Select";
        ddl_location.SelectedValue = "Select";
        txt_from_date.Text = "";
        txt_to_date.Text = "";
    }
    protected void clear_service()
    {
        ddl_machine_service.SelectedValue = "Select";
        txt_form_date_service.Text = "";
        txt_end_date_service.Text = "";
        ddl_warranty.SelectedValue = "Select";
        txt_warranty.Text = "";
        txt_nxt_service.Text = "";


    }
    protected void gv_services_RowDataBound(object sender, GridViewRowEventArgs e)
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
}