using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class WorkingChecklist_Assign : System.Web.UI.Page
{
    DAL d1 = new DAL();
   DAL d = new DAL();
   protected void Page_Load(object sender, EventArgs e)
   {
       if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
       {
           Response.Redirect("Login_Page.aspx");
       }
       if (!IsPostBack)
       {
           btnadd.Visible = false;
           btndelete.Visible = false;
           btn_close.Visible = false;
           //btn_UPDATE.Visible = true;
           client_assign();


          //s UnitGridView.Visible = true;
           d.con1.Open();
          

          
       }
       //gridbind();
      
     
   }
   public void gridbind()
   {
       gv_working_checklist1.DataSource = null;
       gv_working_checklist1.DataBind();


       MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time  FROM pay_working_master where pay_working_master.comp_code = '" + Session["comp_code"].ToString() + "' and grade= '" + ddl_grade.SelectedValue + "'", d.con);
       d.con.Open();
       try
       {
           MySqlDataReader dr_hd = cmd_hd.ExecuteReader();
           if (dr_hd.HasRows)
           {
               DataTable dt = new DataTable();
               dt.Load(dr_hd);

               gv_working_checklist1.DataSource = dt;
               gv_working_checklist1.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }

   protected void btnadd_click(object sender, EventArgs e)

   {

       if (ddl_client_assign.SelectedValue=="Select") {

           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Client... !!!');", true);
           return;
       }

       string id = null;
      
       try {
          // MySqlCommand cmd_hd = null;
           int count = 0;
           foreach (GridViewRow row in gv_working_checklist1.Rows)
           {
               var checkbox = row.FindControl("chk_client") as CheckBox;
               if (checkbox.Checked == true)
               {
                   count++;
                    id = row.Cells[1].Text;
                string grade = row.Cells[3].Text;
                string description = row.Cells[4].Text;
                   string type = row.Cells[5].Text;
                    string time = row.Cells[6].Text;

                    string policy_id1 = d.getsinglestring("SELECT `grade`, `description`, `type`, `time`, `client_code`,`id_checklist`,`assign_flag` FROM `pay_client_assign` WHERE `comp_code` = '" + Session["comp_code"].ToString() + "'  AND client_code = '" + ddl_client_assign.SelectedValue + "' AND `grade` = '" + ddl_grade.SelectedValue + "' AND `description` = '" + description + "'  AND `type` = '"+type+"' and id= '"+id+"' ");
                     if (policy_id1 == "")
                     {
                         int client = 0;

                       client =   d.operation("insert into pay_client_assign (comp_code,grade,description,type,time,client_code,assign_flag,id_checklist)values('" + Session["comp_code"].ToString() + "','" + ddl_grade.SelectedValue + "','" + description + "','" + type + "','" + time + "','" + ddl_client_assign.SelectedValue + "','" + 1 + "','" + id + "') ");
                       if (client > 0)
                       {
                           

                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Client Assign Succsefully... !!!');", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Client Assign Faill.... !!!');", true);


                       }

                     }
                     else 
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' This Checklist All Ready Assign to Client... !!!');", true);
                         return;
                     
                     }

                  //ddl_grade.Items.Insert(Select, "Select");
                  //ddl_client_assign.Items.Insert(Select, "Select");

               }
               
            
           }
           if (count == 0)
           {

               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select At List One Record... !!!');", true);
               return;

           }
          
       }
       catch (Exception ee) {
       
       }
       finally {
     //    gridbind();
           client_select();
       }
 }

   protected void btndelete_click(object sender, EventArgs e)

   {
      

            foreach (GridViewRow row in gv_checklist_assign.Rows)
            {
                var checkbox = row.FindControl("chk_client1") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = row.Cells[6].Text;
                    string description = row.Cells[5].Text;

                      string policy_id = d.getsinglestring("Select  Id , type , grade , description , time , 1_time , 2_time , 3_time , 4_time , 5_time , 6_time , 7_time , 8_time , 9_time , 10_time , 11_time , 12_time  from pay_client_gps_policy where  type ='" + type+ "'and comp_code = '" + Session["comp_code"].ToString() + "' and grade ='" + ddl_grade.SelectedValue + "'and description='" + description+ "'");
                      if (policy_id == "")
                      {
                          string id_client = row.Cells[1].Text;

                          int result = 0;
                          result = d.operation("delete from pay_client_assign where comp_code = '" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_client_assign.SelectedValue + "' and assign_flag = '1' and pay_client_assign.Id = '" + id_client + "' ");

                          if (result > 0)
                          {
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted succsefully... !!!');", true);
                          }
                          else
                          {
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  Deleted faill.... !!!');", true);
                          }

                      }
                      else
                      {
                          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Policy All Ready Created You Can not Deleted ... !!!');", true);
                          return;

                      }
   

            }
            client_select();
        }
        
   }

   protected void client_assign() {

       d.con1.Open();
       try
       {
           MySqlDataAdapter cmd = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  ORDER BY client_code", d.con1);//AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
           DataTable dt = new DataTable();
           cmd.Fill(dt);
           if (dt.Rows.Count > 0)
           {
               ddl_client_assign.DataSource = dt;
               ddl_client_assign.DataTextField = dt.Columns[0].ToString();
               ddl_client_assign.DataValueField = dt.Columns[1].ToString();
               ddl_client_assign.DataBind();

           }
       }
       catch (Exception ex) { throw ex; }
       finally
       {

           d.con1.Close();
           ddl_client_assign.Items.Insert(0, "Select");
       }

   }





   protected void gv_working_checklist1_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       for (int i = 0; i < e.Row.Cells.Count; i++)
       {
           if (e.Row.Cells[i].Text == "&nbsp;")
           {
               e.Row.Cells[i].Text = "";
           }
       }
       //if (e.Row.RowType == DataControlRowType.DataRow)
       //{
       //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
       //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
       //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_working_checklist1, "Select$" + e.Row.RowIndex);
          
       //}
       e.Row.Cells[1].Visible = false;

   }

   protected void btnclose_Click(object sender, EventArgs e)
   {
       Response.Redirect("ClientPolicy.aspx");
    
   }



   protected void ddl_grade_SelectedIndexChanged(object sender, EventArgs e)
   {

       client_select();
           gridbind();
           btnadd.Visible = true;
           btn_close.Visible = true;
   }
   protected void ddl_client_assign_SelectedIndexChanged(object sender, EventArgs e)
   {
       try
       {

           ddl_grade.Items.Clear();
           DataTable dt_item = new DataTable();
           MySqlDataAdapter grd = new MySqlDataAdapter("select Distinct `DESIGNATION` from pay_designation_count  where pay_designation_count.comp_code = '" + Session["comp_code"].ToString() + "' and pay_designation_count.client_code = '" + ddl_client_assign.SelectedValue + "' ", d.con1);

           grd.Fill(dt_item);
           if (dt_item.Rows.Count > 0)
           {
               ddl_grade.DataSource = dt_item;
               ddl_grade.DataTextField = dt_item.Columns[0].ToString();
               //ddl_grade.DataValueField = dt_item.Columns[1].ToString();
               ddl_grade.DataBind();
           }
           dt_item.Dispose();

       }
       catch (Exception ex) { throw ex; }
       finally
       {
           d.con1.Close();
           ddl_grade.Items.Insert(0, new ListItem("Select", "Select"));

       }

       client_select();

      
   }
   protected void client_select() 
   {
       string where = "";
       if (ddl_grade.SelectedValue == "" || ddl_grade.SelectedValue !="Select")
       {
           where =" and grade='"+ddl_grade.SelectedValue+"'";
       }

       gv_checklist_assign.DataSource = null;
       gv_checklist_assign.DataBind();
       try
       {
           d.con.Open();
           MySqlCommand cmd_hd = null;
           cmd_hd = new MySqlCommand("select pay_client_assign.id,pay_client_assign.comp_code, grade,description,type,time,client_name from pay_client_assign INNER JOIN `pay_client_master` ON `pay_client_assign`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_client_assign`.`client_code` = `pay_client_master`.`client_code` where pay_client_assign.comp_code = '" + Session["comp_code"].ToString() + "' and pay_client_assign.client_code = '" + ddl_client_assign.SelectedValue + "' and assign_flag='1' " + where + "", d.con);
           MySqlDataAdapter dr_client = new MySqlDataAdapter(cmd_hd);
           DataTable dt_client = new DataTable();
           dr_client.Fill(dt_client);
           if (dt_client.Rows.Count > 0)
           {


               gv_checklist_assign.DataSource = dt_client;
               gv_checklist_assign.DataBind();
               gv_checklist_assign.Visible = true;
               btndelete.Visible = true;

           }
       }
       catch (Exception ex) { throw ex; }
       finally { d.con.Close(); }
   
   
   
   
   }
   protected void gv_checklist_assign_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       for (int i = 0; i < e.Row.Cells.Count; i++)
       {
           if (e.Row.Cells[i].Text == "&nbsp;")
           {
               e.Row.Cells[i].Text = "";
           }
       }
       //if (e.Row.RowType == DataControlRowType.DataRow)
       //{
       //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
       //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
       //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_working_checklist1, "Select$" + e.Row.RowIndex);

       //}
      // e.Row.Cells[1].Visible = false;


   }
}