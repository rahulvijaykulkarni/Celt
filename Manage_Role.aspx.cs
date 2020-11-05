using System.Web.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

public partial class Manage_Role : System.Web.UI.Page
{
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d.getaccess(Session["ROLE"].ToString(), "Create Role", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create Role", Session["COMP_CODE"].ToString()) == "R")
        {
            btnSubmit.Visible = false;
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create Role", Session["COMP_CODE"].ToString()) == "U")
        {
        }
        else if (d.getaccess(Session["ROLE"].ToString(), "Create Role", Session["COMP_CODE"].ToString()) == "C")
        {
        }

        if (!IsPostBack)
        {
            page_refresh();
           // client_list();
        }

    }
    private void page_refresh()
    {

        // PopulateRootLevel();
        MySqlCommand objCommand = new MySqlCommand(@"select Description, 'A' as permissions from menus where click_evt is not null", d.con1);
        //
        MySqlDataAdapter da = new MySqlDataAdapter(objCommand);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.DataSource = dt;
        GridView2.DataBind();

        ddl_Update_role.Items.Clear();

        MySqlCommand cmd_Update = new MySqlCommand("SELECT distinct role_name FROM pay_role_master where comp_code='" + Session["comp_code"].ToString() + "' ORDER BY role_name", d.con);
        d.conopen();
        MySqlDataReader dr_Update = cmd_Update.ExecuteReader();
        while (dr_Update.Read())
        {
            ddl_Update_role.Items.Add(dr_Update.GetValue(0).ToString());

        }

        ddl_Update_role.Items.Insert(0, "Add New Role");

        dr_Update.Close();
        d.conclose();
    }
    //private void PopulateRootLevel()
    //{

    //    MySqlCommand objCommand = new MySqlCommand(@"select id,Menu_name as title,(select count(*) FROM menus WHERE parent=sc.id) childnodecount FROM menus sc where parent IS NULL", d.con1);
    //    MySqlDataAdapter da = new MySqlDataAdapter(objCommand);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    // PopulateNodes(dt, TreeView1.Nodes);
    //}

    //private void PopulateSubLevel(int parentid, TreeNode parentNode)
    //{

    //    MySqlCommand objCommand = new MySqlCommand(@"select id,Menu_name as title,(select count(*) FROM menus WHERE parent=sc.id) childnodecount FROM menus sc where parent=" + parentid, d.con1);
    //    MySqlDataAdapter da = new MySqlDataAdapter(objCommand);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    PopulateNodes(dt, parentNode.ChildNodes);
    //}


    //protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    //{
    //    PopulateSubLevel(Int32.Parse(e.Node.Value), e.Node);
    //}

    //private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
    //{
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        TreeNode tn = new TreeNode();
    //        tn.Text = dr["title"].ToString();
    //        tn.Value = dr["id"].ToString();
    //        nodes.Add(tn);

    //        tn.PopulateOnDemand = (Int32.Parse((dr["childnodecount"].ToString())) > 0);
    //    }
    //}

    protected void btmSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            d.operation("DELETE FROM pay_role_master  WHERE  role_name='" + txt_roleName.Text + "' and comp_code='" + Session["comp_code"].ToString() + "'");//delete command
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkcreate = (row.Cells[2].FindControl("chk_create") as CheckBox);
                    CheckBox chkupdate = (row.Cells[3].FindControl("chk_update") as CheckBox);
                    CheckBox chkread = (row.Cells[4].FindControl("chk_Read") as CheckBox);
                    CheckBox chkdelete = (row.Cells[1].FindControl("chk_delete") as CheckBox);
                    string name = row.Cells[0].Text;
                    string permission = "I";


                    if (chkdelete.Checked)
                    {
                        //insert permission D pay_role_master
                        permission = "D";

                    }
                    else if (chkcreate.Checked)
                    {
                        //insert permission C
                        permission = "C";
                    }
                    else if (chkupdate.Checked)
                    {
                        //insert permission U
                        permission = "U";
                    }
                    else if (chkread.Checked)
                    {
                        //insert permission R
                        permission = "R";
                    }
                    else
                    {
                        //insert permission I
                        permission = "I";
                    }

                    d.operation("Insert into pay_role_master (role_name, permissions, menu_id,comp_code,approval_level) values ('" + txt_roleName.Text + "','" + permission + "', (select id from menus where description = '" + name + "'),'" + Session["comp_code"].ToString() + "','" + txt_approval_level.Text + "')");
                }
            }

            d.operation("insert into pay_role_master (role_name, menu_id, permissions,comp_code,approval_level) select '" + txt_roleName.Text + "', id, 'D','" + Session["comp_code"].ToString() + "','" + txt_approval_level.Text + "' from menus where click_evt is null");
         //   update_client_state();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Successfull...')", true);
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Adding Failed...')", true);
        }
        finally
        {
            d.con.Close();
            page_refresh();
        }
    }

    private void ClearNodes(TreeNodeCollection tnc)
    {
        foreach (TreeNode n in tnc)
        {
            n.Selected = false;
            ClearNodes(n.ChildNodes);
        }
    }

    //protected void btn_update_Click(object sender, System.EventArgs e)
    //{
    //    try
    //    {
    //        int result = 0;
    //        result = d.operation("DELETE FROM pay_role_master  WHERE  role_name='" + txt_roleName.Text + "'");//delete command



    //        foreach (GridViewRow row in GridView2.Rows)
    //         {
    //              if (row.RowType == DataControlRowType.DataRow)
    //             {
    //            CheckBox chkcreate = (row.Cells[2].FindControl("chk_create") as CheckBox);
    //            CheckBox chkupdate = (row.Cells[3].FindControl("chk_update") as CheckBox);
    //            CheckBox chkread = (row.Cells[4].FindControl("chk_Read") as CheckBox);
    //            CheckBox chkdelete = (row.Cells[1].FindControl("chk_delete") as CheckBox);
    //            string name = row.Cells[0].Text;
    //            string permission = "I";


    //            if (chkdelete.Checked)
    //            {
    //                //insert permission D pay_role_master
    //                permission = "D";

    //            }
    //            else if (chkcreate.Checked)
    //            {
    //                //insert permission C
    //                permission = "C";
    //            }
    //            else if (chkupdate.Checked)
    //            {
    //                //insert permission U
    //                permission = "U";
    //            }
    //            else if (chkread.Checked)
    //            {
    //                //insert permission R
    //                permission = "R";
    //            }
    //            else
    //            {
    //                //insert permission I
    //                permission = "I";
    //            }

    //            MySqlCommand cmd_tarnsactioninsert = new MySqlCommand("Insert into pay_role_master (role_name, permissions, menu_id) values ('" + txt_roleName.Text + "','" + permission + "', (select id from menus where menu_name = '" + name + "'))", d.con);
    //            d.conopen();
    //            cmd_tarnsactioninsert.ExecuteNonQuery();
    //            d.conclose();
    //           }
    //        }
    //            MySqlCommand cmd_tarnsactioninsert1 = new MySqlCommand("insert into pay_role_master (role_name, menu_id, permissions) select '" + txt_roleName.Text + "', id, 'D' from menus where click_evt is null", d.con);
    //            d.conopen();
    //            cmd_tarnsactioninsert1.ExecuteNonQuery();
    //            d.conclose();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ddl_Update_role.SelectedValue + " Updated Successfully');", true);

    //         }
    //    catch (Exception ee)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updating Failed...')", true);

    //    }
    //    finally
    //    {

    //    }


    //}

    protected void btn_close_Click(object sender, EventArgs args)
    {
        Session["ID"] = "";
        Response.Redirect("Home.aspx");
    }

    protected void ddl_Update_role_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_roleName.Text = ddl_Update_role.SelectedItem.Text;
        try
        {
            MySqlCommand cmd1;
            if (ddl_Update_role.SelectedItem.ToString() != "Add New Role")
            {
                cmd1 = new MySqlCommand("select menu_name, permissions, Description from (select distinct menus.menu_name, permissions, Description,menus.id from pay_role_master inner join menus on menus.id = pay_role_master.menu_id where role_name = '" + ddl_Update_role.SelectedItem.ToString() + "' and menus.click_evt is not null and comp_code='" + Session["comp_code"].ToString() + "' UNION select menus.menu_name, 'A' as permissions, Description,menus.id from menus Where menus.click_evt is not null and menus.ID NOT IN(Select menu_id from pay_role_master Where role_name = '" + ddl_Update_role.SelectedItem.ToString() + "' and comp_code='" + Session["comp_code"].ToString() + "' AND menu_id IS NOT NULL)) as G order by id", d.con);
                d.con.Open();
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(dr1);
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.DataSource = dt;
                GridView2.DataBind();
                d.con.Close();
                string temp = d.getsinglestring("Select distinct(approval_level) from pay_role_master where role_name ='" + ddl_Update_role.SelectedItem.ToString() + "' limit 1");
                if (temp != "")
                {
                    txt_approval_level.Text = temp;
                }
            }
            else
            {
                page_refresh();
            }
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Display Failed...!')", true);
        }
        finally
        {
            d.con.Close();
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item = e.Row.Cells[1].Text;

            CheckBox chkdelete = (CheckBox)(e.Row.Cells[2].FindControl("chk_delete") as CheckBox);
            CheckBox chkcreate = (CheckBox)(e.Row.Cells[3].FindControl("chk_create") as CheckBox);
            CheckBox chkupdate = (CheckBox)(e.Row.Cells[4].FindControl("chk_update") as CheckBox);
            CheckBox chkread = (CheckBox)(e.Row.Cells[5].FindControl("chk_Read") as CheckBox);
            chkdelete.Checked = false;
            chkcreate.Checked = false;
            chkupdate.Checked = false;
            chkread.Checked = false;
            if (item == "R")
            {
                chkread.Checked = true;
            }
            else if (item == "U")
            {
                chkupdate.Checked = true;
            }
            else if (item == "C")
            {
                chkcreate.Checked = true;
            }
            else if (item == "D")
            {
                chkdelete.Checked = true;
            }
            e.Row.Cells[1].Visible = false;
            GridView2.HeaderRow.Cells[1].Visible = false;
        }
    }

    //protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    IEnumerable<string> selectedValues = from item in ddl_unit_client.Items.Cast<ListItem>()
    //                                         where item.Selected
    //                                         select item.Value;
    //    string listvalues_ddl_unitclient = string.Join("','", selectedValues);

    //    ddl_clientwisestate.Items.Clear();
    //    MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE in ('" + listvalues_ddl_unitclient + "') and unit_code is null order by 1", d.con);
    //    d.con.Open();
    //    try
    //    {
    //        MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
    //        while (dr_item1.Read())
    //        {
    //            ddl_clientwisestate.Items.Add(dr_item1[0].ToString());

    //        }
    //        dr_item1.Close();
    //       // ddl_clientwisestate.Items.Insert(0, new ListItem("All"));
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con.Close();
    //    }

    //}
    
    //protected void client_list()
    //{
    //    d.con1.Open();
    //    try
    //    {
    //        MySqlDataAdapter cmd = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' ORDER BY client_code", d.con1);
    //        DataTable dt = new DataTable();
    //        cmd.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddl_unit_client.DataSource = dt;
    //            ddl_unit_client.DataTextField = dt.Columns[0].ToString();
    //            ddl_unit_client.DataValueField = dt.Columns[1].ToString();
    //            ddl_unit_client.DataBind();
    //        }
    //    }
    //    catch (Exception ex) { throw ex; }
    //    finally
    //    {
    //        d.con1.Close();
    //       // ddl_unit_client.Items.Insert(0, "All");
    //    }
    //}

    //private void update_client_state()
    //{
    //    IEnumerable<string> selectedValues = from item in ddl_unit_client.Items.Cast<ListItem>()
    //                                         where item.Selected
    //                                         select item.Value;
    //    string listvalues_ddl_unitclient = string.Join(",", selectedValues);

    //    IEnumerable<string> state = from item in ddl_clientwisestate.Items.Cast<ListItem>()
    //                                      where item.Selected
    //                                      select item.Value;
    //    string listvalues_ddl_state = string.Join(",", state);

    //    var elements = listvalues_ddl_unitclient.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
    //    var elements1 = listvalues_ddl_state.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

    //    d.operation("delete from pay_client_state_role_grade where role_name = '" + txt_roleName.Text + "' and comp_code = '" + Session["COMP_CODE"].ToString() + "'");

    //    foreach (string client in elements)
    //    {
    //        foreach (string state1 in elements1)
    //        {
    //            string temp1 = d.getsinglestring("SELECT state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + client + "' and state = '"+ state1 +"' and unit_code is null");
    //            if (temp1 == state1)
    //            {
    //                d.operation("insert into pay_client_state_role_grade (comp_code,role_name,approval_level,client_code,state_name,created_by,created_date) values ('" + Session["COMP_CODE"].ToString() + "','" + txt_roleName.Text + "','" + txt_approval_level.Text + "','" + client + "','" + state1 + "','" + Session["LOGIN_ID"].ToString() + "',now())");
    //            }
    //        }
    //    }
    
    //}

}
