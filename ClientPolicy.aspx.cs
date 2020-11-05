using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

public partial class ClientPolicy : System.Web.UI.Page
{
    DAL d1 = new DAL();
   DAL d = new DAL();
   DAL d3 = new DAL();
   DAL d4 = new DAL();
   DAL d5 = new DAL();
   DAL d6 = new DAL();
   DAL d7 = new DAL();
   DAL d8 = new DAL();
   DAL d9 = new DAL();
   protected void Page_Load(object sender, EventArgs e)
   {
       if (Session["COMP_CODE"] == null || Session["COMP_CODE"].ToString() == "")
       {
           Response.Redirect("Login_Page.aspx");
       }
       if (!IsPostBack)
       {

           btn_visble_false();
          
           client_list();
           btn_update.Visible = false;
           btn_add.Visible = false;

       //    gv_client_daily.Visible = false;
         
           GridView_daily_master.Visible=true;

           Grid_view_master_daily();
          // Grid_view_master_weekly();
          // Grid_view_master_fourth_nightly();
           //Grid_view_master_monthly();
           //Grid_view_master_quarterly();
           //Grid_view_master_six_monthly();
           //Grid_view_master_yearly();
          //s UnitGridView.Visible = true;
          
       }
       
     
   }

   public void btn_visble_false()

   {
       //btn_daily_add.Visible = false;
      // btn_daily_close.Visible = false;
      // btn_daily_delete.Visible = false;

       //btn_weekly_add.Visible = false;
       //btn_weekly_delete.Visible = false;
       //btn_weekly_close.Visible = false;

       //btn_fourth_n_add.Visible = false;
       //btn_fourth_n_delete.Visible = false;
       //btn_fourth_n_close.Visible = false;


       //btn_month_add.Visible = false;
       //btn_month_delete.Visible = false;
       //btn_month_close.Visible = false;

       //btn_quarter_add.Visible = false;
       //btn_quarter_delete.Visible = false;
       //btn_quarter_close.Visible = false;

       //btn_six_month_add.Visible = false;
       //btn_six_month_delete.Visible = false;
       //btn_six_month_close.Visible = false;

       //btn_yearly_add.Visible = false;
       //btn_yearly_delete.Visible = false;
       //btn_yearly_close.Visible = false;
           


   
   
   
   }




   public void btn_visble_true()
   {
       //btn_daily_add.Visible = true;
      // btn_daily_close.Visible = true;
      // btn_daily_delete.Visible = true;

       //btn_weekly_add.Visible = true;
       //btn_weekly_delete.Visible = true;
       //btn_weekly_close.Visible = true;

       //btn_fourth_n_add.Visible = true;
       //btn_fourth_n_delete.Visible = true;
       //btn_fourth_n_close.Visible = true;


       //btn_month_add.Visible = true;
       //btn_month_delete.Visible = true;
       //btn_month_close.Visible = true;

       //btn_quarter_add.Visible = true;
       //btn_quarter_delete.Visible = true;
       //btn_quarter_close.Visible = true;

       //btn_six_month_add.Visible = true;
       //btn_six_month_delete.Visible = true;
       //btn_six_month_close.Visible = true;
       //btn_yearly_add.Visible = true;
       //btn_yearly_delete.Visible = true;
       //btn_yearly_close.Visible = true;

   }

protected void btnadd_Click(object sender, EventArgs e)
   {
      
       int count = 0;

       if (d.getsinglestring("select comp_code ,client_code ,state,`branch_having_policy`,state,designation from pay_client_gps_policy_master where comp_code='" + Session["comp_code"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and designation='" + ddl_designation.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "' and branch_having_policy='" + ddl_unitcode_without.SelectedValue + "'") != "")
       {

           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Policy Already Created Please Select Another Branch.. !!!');", true);
           return;
       }

       int rowIndex = -1;

       foreach (GridViewRow gvrow in gv_client_policy.Rows)
       {
           rowIndex = ++rowIndex;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gv_client_policy.Rows[rowIndex].Cells[6].FindControl("lbl_time");
               string id = box1.Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gv_client_policy.Rows[rowIndex].Cells[5].FindControl("ddl_daily_" + i + "_time");

                   string id1 = box.SelectedValue;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }
               }

           }

       }
    ////////////////////////

       //////////////////////////////
    // for weekly

       int rowIndex1 = -1;

       foreach (GridViewRow gvrow in gridview_weekly.Rows)
       {

           rowIndex1 = ++rowIndex1;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_weekly.Rows[rowIndex1].Cells[6].FindControl("lbl_time_weekly");
               string id = box1.Text;


               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_weekly.Rows[rowIndex1].Cells[5].FindControl("ddl_weekly_" + i + "_time");


                   string id1 = box.SelectedValue;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }

               }
           }

       }
    ///////
       ///////////////////

       // for fourth night
       int rowIndex2 = -1;
       foreach (GridViewRow gvrow in gridview_fourth_nightly.Rows)
       {

           rowIndex2 = ++rowIndex2;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_fourth_nightly.Rows[rowIndex2].Cells[6].FindControl("lbl_time_fourth_nightly");
               string id = box1.Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_fourth_nightly.Rows[rowIndex2].Cells[5].FindControl("ddl_fourth_" + i + "_time");

                   string id1 = box.SelectedValue;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }

               }

               //inlist = inlist + "'" + emp_code + "',";
           }

       }


      
       /////////////////////////////////////

       // for monthly

       int rowIndex_month = -1;

       foreach (GridViewRow gvrow in gridview_monthly.Rows)
       {

           rowIndex_month = ++rowIndex_month;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box_m = (TextBox)gridview_monthly.Rows[rowIndex_month].Cells[6].FindControl("lbl_time_monthly");
               string id_month = box_m.Text;


               for (int i = 1; i <= int.Parse(id_month); i++)
               {
                   DropDownList box_month = (DropDownList)gridview_monthly.Rows[rowIndex_month].Cells[5].FindControl("ddl_monthly_" + i + "_time");


                   string id1_9 = box_month.SelectedValue;
                   if (id1_9 != "0")
                   {
                       box_month.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1_9 == "0")
                       {
                           box_month.BackColor = Color.Orange;

                       }

               }
           }

       }

    /////////////////
    //for quarterly

       int rowIndex10 = -1;

       foreach (GridViewRow gvrow in gridview_quarterly.Rows)
       {

           rowIndex10 = ++rowIndex10;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_quarterly.Rows[rowIndex10].Cells[6].FindControl("lbl_time_quarterly");
               string id_1 = box1.Text;


               for (int i = 1; i <= int.Parse(id_1); i++)
               {
                   DropDownList box = (DropDownList)gridview_quarterly.Rows[rowIndex10].Cells[5].FindControl("ddl_quarterly_" + i + "_time");


                   string id1 = box.SelectedValue;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }

               }
           }

       }
    ///////////////

       //////////////////////////////

    // for six month
       int rowIndex_5 = -1;
       foreach (GridViewRow gvrow in gridview_six_monthly.Rows)
       {
           rowIndex_5 = ++rowIndex_5;
           // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
           //string emp_code = gridview_weekly.Rows[gvrow.RowIndex].Cells[1].Text;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_six_monthly.Rows[rowIndex_5].Cells[6].FindControl("lbl_time_six_month");
               string id_3 = box1.Text;
               //lbl_time.Text = gv_client_policy.SelectedRow.Cells[0].Text;
               // string id = gv_client_policy.SelectedRow.Cells[5].Text;

               for (int i = 1; i <= int.Parse(id_3); i++)
               {
                   DropDownList box = (DropDownList)gridview_six_monthly.Rows[rowIndex_5].Cells[5].FindControl("ddl_six_month_" + i + "_time");
                   string id1 = box.Text;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }
               }
               //inlist = inlist + "'" + emp_code + "',";
           }

       }

    ////////////////

    

   // for yearly

       int rowIndex_6 = -1;
       foreach (GridViewRow gvrow in gridview_yearly.Rows)
       {
           rowIndex_6 = ++rowIndex_6;

           // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
           //string emp_code = gridview_weekly.Rows[gvrow.RowIndex].Cells[1].Text;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;
               TextBox box1 = (TextBox)gridview_yearly.Rows[rowIndex_6].Cells[6].FindControl("lbl_time_yearly");
               string id_8 = box1.Text;
               //lbl_time.Text = gv_client_policy.SelectedRow.Cells[0].Text;
               // string id = gv_client_policy.SelectedRow.Cells[5].Text;

               for (int i = 1; i <= int.Parse(id_8); i++)
               {
                   DropDownList box = (DropDownList)gridview_yearly.Rows[rowIndex_6].Cells[5].FindControl("ddl_yearly_" + i + "_time");
                   string id1 = box.Text;
                   if (id1 != "0")
                   {
                       box.BackColor = Color.LimeGreen;
                   }
                   else
                       if (id1 == "0")
                       {
                           box.BackColor = Color.Orange;

                       }
               }
               //inlist = inlist + "'" + emp_code + "',";
           }

       }





    /////////////////////

       //return for daily

       int rowIndex_re = -1;
       foreach (GridViewRow gvrow in gv_client_policy.Rows)
       {
           rowIndex_re = ++rowIndex_re;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gv_client_policy.Rows[rowIndex_re].Cells[6].FindControl("lbl_time");
               string id = box1.Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gv_client_policy.Rows[rowIndex_re].Cells[5].FindControl("ddl_daily_" + i + "_time");

                   string id1 = box.SelectedValue;

                   if (id1 == "0")
                   {

                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);
                       return;
                   }
               }

           }

       }
       // return for weekly

       int rowIndex_2 = -1;

       foreach (GridViewRow gvrow in gridview_weekly.Rows)
       {
           rowIndex_2 = ++rowIndex_2;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               TextBox box1 = (TextBox)gridview_weekly.Rows[rowIndex_2].Cells[6].FindControl("lbl_time_weekly");
               string id = box1.Text;


               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_weekly.Rows[rowIndex_2].Cells[5].FindControl("ddl_weekly_" + i + "_time");
                   string id1 = box.Text;

                   if (id1 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);

                       //string id2 = box.Text;
                       //if (id2 != "0")
                       //{
                       //    box.BackColor = Color.LimeGreen;
                       //}
                       return;
                   }

               }
           }
       }
       //////////////////////

       // return for fourth night

       int rowIndex_re1 = -1;
       foreach (GridViewRow gvrow in gridview_fourth_nightly.Rows)
       {

           rowIndex_re1 = ++rowIndex_re1;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_fourth_nightly.Rows[rowIndex_re1].Cells[6].FindControl("lbl_time_fourth_nightly");
               string id = box1.Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_fourth_nightly.Rows[rowIndex_re1].Cells[5].FindControl("ddl_fourth_" + i + "_time");

                   string id1 = box.SelectedValue;

                   if (id1 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);

                       //string id2 = box.Text;
                       //if (id2 != "0")
                       //{
                       //    box.BackColor = Color.LimeGreen;
                       //}
                       return;

                   }

               }

               //inlist = inlist + "'" + emp_code + "',";
           }

       }
   
   

    ////////////////
       // return for month

       int rowIndex_month1 = -1;

       foreach (GridViewRow gvrow in gridview_monthly.Rows)
       {
           rowIndex_month1 = ++rowIndex_month1;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               TextBox box1_month = (TextBox)gridview_monthly.Rows[rowIndex_month1].Cells[6].FindControl("lbl_time_monthly");
               string id_month = box1_month.Text;


               for (int i = 1; i <= int.Parse(id_month); i++)
               {
                   DropDownList box = (DropDownList)gridview_monthly.Rows[rowIndex_month1].Cells[5].FindControl("ddl_monthly_" + i + "_time");
                   string id1_month2 = box.Text;

                   if (id1_month2 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);

                       //string id2 = box.Text;
                       //if (id2 != "0")
                       //{
                       //    box.BackColor = Color.LimeGreen;
                       //}
                       return;
                   }

               }
           }
       }
  

    
   

     
       // return for quarterly

       int rowIndex_re3 = -1;

       foreach (GridViewRow gvrow in gridview_quarterly.Rows)
       {
           rowIndex_re3 = ++rowIndex_re3;
           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               TextBox box1 = (TextBox)gridview_quarterly.Rows[rowIndex_re3].Cells[6].FindControl("lbl_time_quarterly");
               string id = box1.Text;


               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_quarterly.Rows[rowIndex_re3].Cells[5].FindControl("ddl_quarterly_" + i + "_time");
                   string id1 = box.Text;

                   if (id1 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);

                       //string id2 = box.Text;
                       //if (id2 != "0")
                       //{
                       //    box.BackColor = Color.LimeGreen;
                       //}
                       return;
                   }

               }
           }
       }


     
       // return for six month

       int rowIndex_re4 = -1;
       foreach (GridViewRow gvrow in gridview_six_monthly.Rows)
       {
           rowIndex_re4 = ++rowIndex_re4;
           // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
           //string emp_code = gridview_weekly.Rows[gvrow.RowIndex].Cells[1].Text;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;

               TextBox box1 = (TextBox)gridview_six_monthly.Rows[rowIndex_re4].Cells[6].FindControl("lbl_time_six_month");
               string id = box1.Text;
               //lbl_time.Text = gv_client_policy.SelectedRow.Cells[0].Text;
               // string id = gv_client_policy.SelectedRow.Cells[5].Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_six_monthly.Rows[rowIndex_re4].Cells[5].FindControl("ddl_six_month_" + i + "_time");
                   string id1 = box.Text;
                   if (id1 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);


                       return;

                   }
               }
               //inlist = inlist + "'" + emp_code + "',";
           }

       }



      
       //return for yearly

       int rowIndex_re5 = -1;
       foreach (GridViewRow gvrow in gridview_yearly.Rows)
       {
           rowIndex_re5 = ++rowIndex_re5;

           // string emp_code = (string)gv_checklist_uniform.DataKeys[gvrow.RowIndex].Value;
           //string emp_code = gridview_weekly.Rows[gvrow.RowIndex].Cells[1].Text;

           var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
           if (checkbox.Checked == true)
           {
               count++;
               TextBox box1 = (TextBox)gridview_yearly.Rows[rowIndex_re5].Cells[6].FindControl("lbl_time_yearly");
               string id = box1.Text;
               //lbl_time.Text = gv_client_policy.SelectedRow.Cells[0].Text;
               // string id = gv_client_policy.SelectedRow.Cells[5].Text;

               for (int i = 1; i <= int.Parse(id); i++)
               {
                   DropDownList box = (DropDownList)gridview_yearly.Rows[rowIndex_re5].Cells[5].FindControl("ddl_yearly_" + i + "_time");
                   string id1 = box.Text;
                   if (id1 == "0")
                   {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);


                       return;

                   }
               }
               //inlist = inlist + "'" + emp_code + "',";
           }

       }


       /////////////////////////////////








  

   

  ///////////////////////////////////////////
    //   int rowIndex4 = -1;
    //   foreach (GridViewRow gvrow in gridview_quarterly.Rows)
    //   {

    //       rowIndex4 = ++rowIndex4;
         
    //       var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
    //       if (checkbox.Checked == true)
    //       {
    //           count++;


    //           TextBox box1 = (TextBox)gridview_quarterly.Rows[rowIndex4].Cells[6].FindControl("lbl_time_quarterly");
    //           string id = box1.Text;
           

    //           for (int i = 1; i <= int.Parse(id); i++)
    //           {
    //               DropDownList box = (DropDownList)gridview_quarterly.Rows[rowIndex4].Cells[5].FindControl("ddl_quarterly_"+i+"_time");
    //               string id1 = box.Text;
    //               if (id1 != "0")
    //               {
    //                   box.BackColor = Color.LimeGreen;
    //               }
    //               else
    //                   if (id1 == "0")
    //                   {
    //                       box.BackColor = Color.Orange;

    //                   }
    //               //inlist = inlist + "'" + emp_code + "',";
    //           }

    //       }
    //   }

    //// for return

    //   int rowIndex_re3 = -1;
    //   foreach (GridViewRow gvrow in gridview_quarterly.Rows)
    //   {

    //       rowIndex_re3 = ++rowIndex_re3;

    //       var checkbox = gvrow.FindControl("chk_client") as System.Web.UI.WebControls.CheckBox;
    //       if (checkbox.Checked == true)
    //       {
    //           count++;


    //           TextBox box1 = (TextBox)gridview_quarterly.Rows[rowIndex_re3].Cells[6].FindControl("lbl_time_quarterly");
    //           string id = box1.Text;


    //           for (int i = 1; i <= int.Parse(id); i++)
    //           {
    //               DropDownList box = (DropDownList)gridview_quarterly.Rows[rowIndex_re3].Cells[5].FindControl("ddl_quarterly_" + i + "_time");
    //               string id1 = box.Text;
    //               if (id1 == "0")
    //               {
    //                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select Time... !!!');", true);


    //                   return;

    //               }
    //               //inlist = inlist + "'" + emp_code + "',";
    //           }

    //       }
    //   }


   

       if (count == 0)
       {

           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Please Select At List One Record... !!!');", true);
           return;

       }


       
           
           
           DataTable dt = new DataTable();
           MySqlDataAdapter client_gps = new MySqlDataAdapter(" select branch_having_policy,designation,client_code,branch_having_policy,state from pay_client_gps_policy_master where comp_code = '"+Session["comp_code"].ToString()+"' and  `branch_having_policy` ='" + ddl_unitcode.SelectedValue + "'and designation ='" + ddl_designation.SelectedValue + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedValue + "'", d.con);
           d.con.Open();
           try
           {

               client_gps.Fill(dt);
           if (dt.Rows.Count > 0)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  All Ready Existed... !!!');", true);
               return;
           }
           else
           {
               int result = 0;
               result = d.operation("insert into pay_client_gps_policy_master(client_code,state,branch_having_policy,branch_not_having_policy,designation,duty_hours,new_policy_name,start_date,end_date,comp_code,branch_having_policy_1)values('" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','select','" + ddl_designation.SelectedValue + "','" + ddl_hours.SelectedValue + "','" + txt_policy_name1.Text + "',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + Session["comp_code"].ToString() + "' ,'" + ddl_unitcode.SelectedItem + "')");

               string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
               // d.operation("insert into pay_client_gps_policy(id,flag,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time)values()");

              //string id_working = d.getsinglestring("select max(id )from pay_working_master where `branch_having_policy` ='" + ddl_unitcode.SelectedItem + "'");
               if (result > 0)
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
               }
               foreach (GridViewRow row in gv_client_policy.Rows)
               {
                   var checkbox = row.FindControl("chk_client") as CheckBox;
                   if (checkbox.Checked == true)
                   {

                       //Label chk_client = (Label)row.FindControl("chk_client");
                       //string chk_client_1 = (chk_client.Text);

                       //Label lbl_comp_code = (Label)row.FindControl("lbl_comp_code");
                       //string lbl_comp_code_1 = (lbl_comp_code.Text);

                       Label lbl_grade = (Label)row.FindControl("lbl_grade");
                       string lbl_grade_1 = (lbl_grade.Text);

                       Label lbl_description = (Label)row.FindControl("lbl_description");
                       string lbl_description_1 = (lbl_description.Text);

                       Label lbl_type = (Label)row.FindControl("lbl_type");
                       string lbl_type_1 = (lbl_type.Text);

                       TextBox lbl_time = (TextBox)row.FindControl("lbl_time");
                       string lbl_time_1 = (lbl_time.Text);


                       DropDownList ddl_daily_1_time = (DropDownList)row.FindControl("ddl_daily_1_time");
                       string ddl_daily_1_time_1 = ddl_daily_1_time.Text.ToString();

                       DropDownList ddl_daily_2_time = (DropDownList)row.FindControl("ddl_daily_2_time");
                       string ddl_daily_2_time_2 = ddl_daily_2_time.Text.ToString();

                       DropDownList ddl_daily_3_time = (DropDownList)row.FindControl("ddl_daily_3_time");
                       string ddl_daily_3_time_3 = ddl_daily_3_time.Text.ToString();


                       DropDownList ddl_daily_4_time = (DropDownList)row.FindControl("ddl_daily_4_time");
                       string ddl_daily_4_time_4 = ddl_daily_4_time.Text.ToString();

                       DropDownList ddl_daily_5_time = (DropDownList)row.FindControl("ddl_daily_5_time");
                       string ddl_daily_5_time_5 = ddl_daily_5_time.Text.ToString();

                       DropDownList ddl_daily_6_time = (DropDownList)row.FindControl("ddl_daily_6_time");
                       string ddl_daily_6_time_6 = ddl_daily_6_time.Text.ToString();


                       DropDownList ddl_daily_7_time = (DropDownList)row.FindControl("ddl_daily_7_time");
                       string ddl_daily_7_time_7 = ddl_daily_7_time.Text.ToString();


                       DropDownList ddl_daily_8_time = (DropDownList)row.FindControl("ddl_daily_8_time");
                       string ddl_daily_8_time_8 = ddl_daily_8_time.Text.ToString();


                       DropDownList ddl_daily_9_time = (DropDownList)row.FindControl("ddl_daily_9_time");
                       string ddl_daily_9_time_9 = ddl_daily_9_time.Text.ToString();

                       DropDownList ddl_daily_10_time = (DropDownList)row.FindControl("ddl_daily_10_time");
                       string ddl_daily_10_time_10 = ddl_daily_10_time.Text.ToString();


                       DropDownList ddl_daily_11_time = (DropDownList)row.FindControl("ddl_daily_11_time");
                       string ddl_daily_11_time_11 = ddl_daily_11_time.Text.ToString();

                       DropDownList ddl_daily_12_time = (DropDownList)row.FindControl("ddl_daily_12_time");
                       string ddl_daily_12_time_12 = ddl_daily_12_time.Text.ToString();



                       int insert_result = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_1.ToString() + "','" + lbl_grade_1.ToString() + "','" + lbl_description_1.ToString() + "','" + lbl_time_1.ToString() + "','" + ddl_daily_1_time_1.ToString() + "','" + ddl_daily_2_time_2.ToString() + "','" + ddl_daily_3_time_3.ToString() + "','" + ddl_daily_4_time_4.ToString() + "','" + ddl_daily_5_time_5.ToString() + "','" + ddl_daily_6_time_6.ToString() + "','" + ddl_daily_7_time_7.ToString() + "','" + ddl_daily_8_time_8.ToString() + "','" + ddl_daily_9_time_9.ToString() + "','" + ddl_daily_10_time_10.ToString() + "','" + ddl_daily_11_time_11.ToString() + "','" + ddl_daily_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                       if (insert_result > 0)
                       {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                       }
                       else
                       {
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                       }
                   }
                   }
               
               Grid_view_master_daily();

//////////////

               foreach_weekly();
               foreach_fourth_nightly();
               foreach_monthly();
               foreach_quarterly();
               foreach_six_monthly();
               foreach_yearly();
               



           }
     }
       catch (Exception ex) { throw ex; }

       finally
       {
           d.con.Close();
       }
   }


public void foreach_weekly()
{
    d.con.Open();

    try {


        foreach (GridViewRow row in gridview_weekly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {
                Label lbl_grade_weekly = (Label)row.FindControl("lbl_grade_weekly");
                string lbl_grade_weekly_1 = (lbl_grade_weekly.Text);

                Label lbl_description_weekly = (Label)row.FindControl("lbl_description_weekly");
                string lbl_description_weekly_1 = (lbl_description_weekly.Text);

                Label lbl_type_weekly = (Label)row.FindControl("lbl_type_weekly");
                string lbl_type_weekly_1 = (lbl_type_weekly.Text);

                TextBox lbl_time_weekly = (TextBox)row.FindControl("lbl_time_weekly");
                string lbl_time_weekly_1 = (lbl_time_weekly.Text);


                DropDownList ddl_weekly_1_time = (DropDownList)row.FindControl("ddl_weekly_1_time");
                string ddl_weekly_1_time_1 = ddl_weekly_1_time.Text.ToString();

                DropDownList ddl_weekly_2_time = (DropDownList)row.FindControl("ddl_weekly_2_time");
                string ddl_weekly_2_time_2 = ddl_weekly_2_time.Text.ToString();

                DropDownList ddl_weekly_3_time = (DropDownList)row.FindControl("ddl_weekly_3_time");
                string ddl_weekly_3_time_3 = ddl_weekly_3_time.Text.ToString();


                DropDownList ddl_weekly_4_time = (DropDownList)row.FindControl("ddl_weekly_4_time");
                string ddl_weekly_4_time_4 = ddl_weekly_4_time.Text.ToString();

                DropDownList ddl_weekly_5_time = (DropDownList)row.FindControl("ddl_weekly_5_time");
                string ddl_weekly_5_time_5 = ddl_weekly_5_time.Text.ToString();

                DropDownList ddl_weekly_6_time = (DropDownList)row.FindControl("ddl_weekly_6_time");
                string ddl_weekly_6_time_6 = ddl_weekly_6_time.Text.ToString();


                DropDownList ddl_weekly_7_time = (DropDownList)row.FindControl("ddl_weekly_7_time");
                string ddl_weekly_7_time_7 = ddl_weekly_7_time.Text.ToString();


                DropDownList ddl_weekly_8_time = (DropDownList)row.FindControl("ddl_weekly_8_time");
                string ddl_weekly_8_time_8 = ddl_weekly_8_time.Text.ToString();


                DropDownList ddl_weekly_9_time = (DropDownList)row.FindControl("ddl_weekly_9_time");
                string ddl_weekly_9_time_9 = ddl_weekly_9_time.Text.ToString();

                DropDownList ddl_weekly_10_time = (DropDownList)row.FindControl("ddl_weekly_10_time");
                string ddl_weekly_10_time_10 = ddl_weekly_10_time.Text.ToString();


                DropDownList ddl_weekly_11_time = (DropDownList)row.FindControl("ddl_weekly_11_time");
                string ddl_weekly_11_time_11 = ddl_weekly_11_time.Text.ToString();

                DropDownList ddl_weekly_12_time = (DropDownList)row.FindControl("ddl_weekly_12_time");
                string ddl_weekly_12_time_12 = ddl_weekly_12_time.Text.ToString();


                string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_weekly_1.ToString() + "','" + lbl_grade_weekly_1.ToString() + "','" + lbl_description_weekly_1.ToString() + "','" + lbl_time_weekly_1.ToString() + "','" + ddl_weekly_1_time_1.ToString() + "','" + ddl_weekly_2_time_2.ToString() + "','" + ddl_weekly_3_time_3.ToString() + "','" + ddl_weekly_4_time_4.ToString() + "','" + ddl_weekly_5_time_5.ToString() + "','" + ddl_weekly_6_time_6.ToString() + "','" + ddl_weekly_7_time_7.ToString() + "','" + ddl_weekly_8_time_8.ToString() + "','" + ddl_weekly_9_time_9.ToString() + "','" + ddl_weekly_10_time_10.ToString() + "','" + ddl_weekly_11_time_11.ToString() + "','" + ddl_weekly_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                }
            }
        }
    


    
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }



}


public void foreach_fourth_nightly() {

    try {


        foreach (GridViewRow row in gridview_fourth_nightly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {
                Label lbl_grade_fourth_nightly = (Label)row.FindControl("lbl_grade_fourth_nightly");
                string lbl_grade_fourth_nightly_1 = (lbl_grade_fourth_nightly.Text);

                Label lbl_description_fourth_nightly = (Label)row.FindControl("lbl_description_fourth_nightly");
                string lbl_description_fourth_nightly_1 = (lbl_description_fourth_nightly.Text);

                Label lbl_type_fourth_nightly = (Label)row.FindControl("lbl_type_fourth_nightly");
                string lbl_type_fourth_nightly_1 = (lbl_type_fourth_nightly.Text);

                TextBox lbl_time_fourth_nightly = (TextBox)row.FindControl("lbl_time_fourth_nightly");
                string lbl_time_fourth_nightly_1 = (lbl_time_fourth_nightly.Text);


                DropDownList ddl_fourth_1_time = (DropDownList)row.FindControl("ddl_fourth_1_time");
                string ddl_fourth_1_time_1 = ddl_fourth_1_time.Text.ToString();

                DropDownList ddl_fourth_2_time = (DropDownList)row.FindControl("ddl_fourth_2_time");
                string ddl_fourth_2_time_2 = ddl_fourth_2_time.Text.ToString();

                DropDownList ddl_fourth_3_time = (DropDownList)row.FindControl("ddl_fourth_3_time");
                string ddl_fourth_3_time_3 = ddl_fourth_3_time.Text.ToString();


                DropDownList ddl_fourth_4_time = (DropDownList)row.FindControl("ddl_fourth_4_time");
                string ddl_fourth_4_time_4 = ddl_fourth_4_time.Text.ToString();

                DropDownList ddl_fourth_5_time = (DropDownList)row.FindControl("ddl_fourth_5_time");
                string ddl_fourth_5_time_5 = ddl_fourth_5_time.Text.ToString();

                DropDownList ddl_fourth_6_time = (DropDownList)row.FindControl("ddl_fourth_6_time");
                string ddl_fourth_6_time_6 = ddl_fourth_6_time.Text.ToString();


                DropDownList ddl_fourth_7_time = (DropDownList)row.FindControl("ddl_fourth_7_time");
                string ddl_fourth_7_time_7 = ddl_fourth_7_time.Text.ToString();


                DropDownList ddl_fourth_8_time = (DropDownList)row.FindControl("ddl_fourth_8_time");
                string ddl_fourth_8_time_8 = ddl_fourth_8_time.Text.ToString();


                DropDownList ddl_fourth_9_time = (DropDownList)row.FindControl("ddl_fourth_9_time");
                string ddl_fourth_9_time_9 = ddl_fourth_9_time.Text.ToString();

                DropDownList ddl_fourth_10_time = (DropDownList)row.FindControl("ddl_fourth_10_time");
                string ddl_fourth_10_time_10 = ddl_fourth_10_time.Text.ToString();


                DropDownList ddl_fourth_11_time = (DropDownList)row.FindControl("ddl_fourth_11_time");
                string ddl_fourth_11_time_11 = ddl_fourth_11_time.Text.ToString();

                DropDownList ddl_fourth_12_time = (DropDownList)row.FindControl("ddl_fourth_12_time");
                string ddl_fourth_12_time_12 = ddl_fourth_12_time.Text.ToString();


                string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_fourth_nightly_1.ToString() + "','" + lbl_grade_fourth_nightly_1.ToString() + "','" + lbl_description_fourth_nightly_1.ToString() + "','" + lbl_time_fourth_nightly_1.ToString() + "','" + ddl_fourth_1_time_1.ToString() + "','" + ddl_fourth_2_time_2.ToString() + "','" + ddl_fourth_3_time_3.ToString() + "','" + ddl_fourth_4_time_4.ToString() + "','" + ddl_fourth_5_time_5.ToString() + "','" + ddl_fourth_6_time_6.ToString() + "','" + ddl_fourth_7_time_7.ToString() + "','" + ddl_fourth_8_time_8.ToString() + "','" + ddl_fourth_9_time_9.ToString() + "','" + ddl_fourth_10_time_10.ToString() + "','" + ddl_fourth_11_time_11.ToString() + "','" + ddl_fourth_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                }
            }

        }
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }


}

public void foreach_monthly()
{

    try {
        d.con.Open();


        foreach (GridViewRow row in gridview_monthly.Rows)
        {
            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label lbl_grade_monthly = (Label)row.FindControl("lbl_grade_monthly");
                string lbl_grade_monthly_1 = (lbl_grade_monthly.Text);

                Label lbl_description_monthly = (Label)row.FindControl("lbl_description_monthly");
                string lbl_description_monthly_1 = (lbl_description_monthly.Text);

                Label lbl_type_monthly = (Label)row.FindControl("lbl_type_monthly");
                string lbl_type_monthly_1 = (lbl_type_monthly.Text);

                TextBox lbl_time_monthly = (TextBox)row.FindControl("lbl_time_monthly");
                string lbl_time_monthly_1 = (lbl_time_monthly.Text);


                DropDownList ddl_monthly_1_time = (DropDownList)row.FindControl("ddl_monthly_1_time");
                string ddl_monthly_1_time_1 = ddl_monthly_1_time.Text.ToString();

                DropDownList ddl_monthly_2_time = (DropDownList)row.FindControl("ddl_monthly_2_time");
                string ddl_monthly_2_time_2 = ddl_monthly_2_time.Text.ToString();

                DropDownList ddl_monthly_3_time = (DropDownList)row.FindControl("ddl_monthly_3_time");
                string ddl_monthly_3_time_3 = ddl_monthly_3_time.Text.ToString();


                DropDownList ddl_monthly_4_time = (DropDownList)row.FindControl("ddl_monthly_4_time");
                string ddl_monthly_4_time_4 = ddl_monthly_4_time.Text.ToString();

                DropDownList ddl_monthly_5_time = (DropDownList)row.FindControl("ddl_monthly_5_time");
                string ddl_monthly_5_time_5 = ddl_monthly_5_time.Text.ToString();

                DropDownList ddl_monthly_6_time = (DropDownList)row.FindControl("ddl_monthly_6_time");
                string ddl_monthly_6_time_6 = ddl_monthly_6_time.Text.ToString();


                DropDownList ddl_monthly_7_time = (DropDownList)row.FindControl("ddl_monthly_7_time");
                string ddl_monthly_7_time_7 = ddl_monthly_7_time.Text.ToString();


                DropDownList ddl_monthly_8_time = (DropDownList)row.FindControl("ddl_monthly_8_time");
                string ddl_monthly_8_time_8 = ddl_monthly_8_time.Text.ToString();


                DropDownList ddl_monthly_9_time = (DropDownList)row.FindControl("ddl_monthly_9_time");
                string ddl_monthly_9_time_9 = ddl_monthly_9_time.Text.ToString();

                DropDownList ddl_monthly_10_time = (DropDownList)row.FindControl("ddl_monthly_10_time");
                string ddl_monthly_10_time_10 = ddl_monthly_10_time.Text.ToString();


                DropDownList ddl_monthly_11_time = (DropDownList)row.FindControl("ddl_monthly_11_time");
                string ddl_monthly_11_time_11 = ddl_monthly_11_time.Text.ToString();

                DropDownList ddl_monthly_12_time = (DropDownList)row.FindControl("ddl_monthly_12_time");
                string ddl_monthly_12_time_12 = ddl_monthly_12_time.Text.ToString();


                string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_monthly_1.ToString() + "','" + lbl_grade_monthly_1.ToString() + "','" + lbl_description_monthly_1.ToString() + "','" + lbl_time_monthly_1.ToString() + "','" + ddl_monthly_1_time_1.ToString() + "','" + ddl_monthly_2_time_2.ToString() + "','" + ddl_monthly_3_time_3.ToString() + "','" + ddl_monthly_4_time_4.ToString() + "','" + ddl_monthly_5_time_5.ToString() + "','" + ddl_monthly_6_time_6.ToString() + "','" + ddl_monthly_7_time_7.ToString() + "','" + ddl_monthly_8_time_8.ToString() + "','" + ddl_monthly_9_time_9.ToString() + "','" + ddl_monthly_10_time_10.ToString() + "','" + ddl_monthly_11_time_11.ToString() + "','" + ddl_monthly_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                }
            }
        }
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }

}

    public void foreach_quarterly()
    {

        try
        {
            d.con.Open();

            foreach (GridViewRow row in gridview_quarterly.Rows)
            {
                  var checkbox = row.FindControl("chk_client") as CheckBox;
                  if (checkbox.Checked == true)
                  {

                      Label lbl_grade_quarterly = (Label)row.FindControl("lbl_grade_quarterly");
                      string lbl_grade_quarterly_1 = (lbl_grade_quarterly.Text);

                      Label lbl_description_quarterly = (Label)row.FindControl("lbl_description_quarterly");
                      string lbl_description_quarterly_1 = (lbl_description_quarterly.Text);

                      Label lbl_type_quarterly = (Label)row.FindControl("lbl_type_quarterly");
                      string lbl_type_quarterly_1 = (lbl_type_quarterly.Text);

                      TextBox lbl_time_quarterly = (TextBox)row.FindControl("lbl_time_quarterly");
                      string lbl_time_quarterly_1 = (lbl_time_quarterly.Text);


                      DropDownList ddl_quarterly_1_time = (DropDownList)row.FindControl("ddl_quarterly_1_time");
                      string ddl_quarterly_1_time_1 = ddl_quarterly_1_time.Text.ToString();

                      DropDownList ddl_quarterly_2_time = (DropDownList)row.FindControl("ddl_quarterly_2_time");
                      string ddl_quarterly_2_time_2 = ddl_quarterly_2_time.Text.ToString();

                      DropDownList ddl_quarterly_3_time = (DropDownList)row.FindControl("ddl_quarterly_3_time");
                      string ddl_quarterly_3_time_3 = ddl_quarterly_3_time.Text.ToString();


                      DropDownList ddl_quarterly_4_time = (DropDownList)row.FindControl("ddl_quarterly_4_time");
                      string ddl_quarterly_4_time_4 = ddl_quarterly_4_time.Text.ToString();

                      DropDownList ddl_quarterly_5_time = (DropDownList)row.FindControl("ddl_quarterly_5_time");
                      string ddl_quarterly_5_time_5 = ddl_quarterly_5_time.Text.ToString();

                      DropDownList ddl_quarterly_6_time = (DropDownList)row.FindControl("ddl_quarterly_6_time");
                      string ddl_quarterly_6_time_6 = ddl_quarterly_6_time.Text.ToString();


                      DropDownList ddl_quarterly_7_time = (DropDownList)row.FindControl("ddl_quarterly_7_time");
                      string ddl_quarterly_7_time_7 = ddl_quarterly_7_time.Text.ToString();


                      DropDownList ddl_quarterly_8_time = (DropDownList)row.FindControl("ddl_quarterly_8_time");
                      string ddl_quarterly_8_time_8 = ddl_quarterly_8_time.Text.ToString();


                      DropDownList ddl_quarterly_9_time = (DropDownList)row.FindControl("ddl_quarterly_9_time");
                      string ddl_quarterly_9_time_9 = ddl_quarterly_9_time.Text.ToString();

                      DropDownList ddl_quarterly_10_time = (DropDownList)row.FindControl("ddl_quarterly_10_time");
                      string ddl_quarterly_10_time_10 = ddl_quarterly_10_time.Text.ToString();


                      DropDownList ddl_quarterly_11_time = (DropDownList)row.FindControl("ddl_quarterly_11_time");
                      string ddl_quarterly_11_time_11 = ddl_quarterly_11_time.Text.ToString();

                      DropDownList ddl_quarterly_12_time = (DropDownList)row.FindControl("ddl_quarterly_12_time");
                      string ddl_quarterly_12_time_12 = ddl_quarterly_12_time.Text.ToString();


                      string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                      int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_quarterly_1.ToString() + "','" + lbl_grade_quarterly_1.ToString() + "','" + lbl_description_quarterly_1.ToString() + "','" + lbl_time_quarterly_1.ToString() + "','" + ddl_quarterly_1_time_1.ToString() + "','" + ddl_quarterly_2_time_2.ToString() + "','" + ddl_quarterly_3_time_3.ToString() + "','" + ddl_quarterly_4_time_4.ToString() + "','" + ddl_quarterly_5_time_5.ToString() + "','" + ddl_quarterly_6_time_6.ToString() + "','" + ddl_quarterly_7_time_7.ToString() + "','" + ddl_quarterly_8_time_8.ToString() + "','" + ddl_quarterly_9_time_9.ToString() + "','" + ddl_quarterly_10_time_10.ToString() + "','" + ddl_quarterly_11_time_11.ToString() + "','" + ddl_quarterly_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                      if (insert_result1 > 0)
                      {
                          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                      }
                      else
                      {
                          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                      }


                  }
            }
        }
        catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }


    }




    public void foreach_six_monthly()
    {

        try
        {
            d.con.Open();

            foreach (GridViewRow row in gridview_six_monthly.Rows)
            {

                 var checkbox = row.FindControl("chk_client") as CheckBox;
                 if (checkbox.Checked == true)
                 {


                     Label lbl_grade_six_month = (Label)row.FindControl("lbl_grade_six_month");
                     string lbl_grade_six_month_1 = (lbl_grade_six_month.Text);

                     Label lbl_description_six_month = (Label)row.FindControl("lbl_description_six_month");
                     string lbl_description_six_month_1 = (lbl_description_six_month.Text);

                     Label lbl_type_six_month = (Label)row.FindControl("lbl_type_six_month");
                     string lbl_type_six_month_1 = (lbl_type_six_month.Text);

                     TextBox lbl_time_six_month = (TextBox)row.FindControl("lbl_time_six_month");
                     string lbl_time_six_month_1 = (lbl_time_six_month.Text);


                     DropDownList ddl_six_month_1_time = (DropDownList)row.FindControl("ddl_six_month_1_time");
                     string ddl_six_month_1_time_1 = ddl_six_month_1_time.Text.ToString();

                     DropDownList ddl_six_month_2_time = (DropDownList)row.FindControl("ddl_six_month_2_time");
                     string ddl_six_month_2_time_2 = ddl_six_month_2_time.Text.ToString();

                     DropDownList ddl_six_month_3_time = (DropDownList)row.FindControl("ddl_six_month_3_time");
                     string ddl_six_month_3_time_3 = ddl_six_month_3_time.Text.ToString();


                     DropDownList ddl_six_month_4_time = (DropDownList)row.FindControl("ddl_six_month_4_time");
                     string ddl_six_month_4_time_4 = ddl_six_month_4_time.Text.ToString();

                     DropDownList ddl_six_month_5_time = (DropDownList)row.FindControl("ddl_six_month_5_time");
                     string ddl_six_month_5_time_5 = ddl_six_month_5_time.Text.ToString();

                     DropDownList ddl_six_month_6_time = (DropDownList)row.FindControl("ddl_six_month_6_time");
                     string ddl_six_month_6_time_6 = ddl_six_month_6_time.Text.ToString();


                     DropDownList ddl_six_month_7_time = (DropDownList)row.FindControl("ddl_six_month_7_time");
                     string ddl_six_month_7_time_7 = ddl_six_month_7_time.Text.ToString();


                     DropDownList ddl_six_month_8_time = (DropDownList)row.FindControl("ddl_six_month_8_time");
                     string ddl_six_month_8_time_8 = ddl_six_month_8_time.Text.ToString();


                     DropDownList ddl_six_month_9_time = (DropDownList)row.FindControl("ddl_six_month_9_time");
                     string ddl_six_month_9_time_9 = ddl_six_month_9_time.Text.ToString();

                     DropDownList ddl_six_month_10_time = (DropDownList)row.FindControl("ddl_six_month_10_time");
                     string ddl_six_month_10_time_10 = ddl_six_month_10_time.Text.ToString();


                     DropDownList ddl_six_month_11_time = (DropDownList)row.FindControl("ddl_six_month_11_time");
                     string ddl_six_month_11_time_11 = ddl_six_month_11_time.Text.ToString();

                     DropDownList ddl_six_month_12_time = (DropDownList)row.FindControl("ddl_six_month_12_time");
                     string ddl_six_month_12_time_12 = ddl_six_month_12_time.Text.ToString();


                     string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                     int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_six_month_1.ToString() + "','" + lbl_grade_six_month_1.ToString() + "','" + lbl_description_six_month_1.ToString() + "','" + lbl_time_six_month_1.ToString() + "','" + ddl_six_month_1_time_1.ToString() + "','" + ddl_six_month_2_time_2.ToString() + "','" + ddl_six_month_3_time_3.ToString() + "','" + ddl_six_month_4_time_4.ToString() + "','" + ddl_six_month_5_time_5.ToString() + "','" + ddl_six_month_6_time_6.ToString() + "','" + ddl_six_month_7_time_7.ToString() + "','" + ddl_six_month_8_time_8.ToString() + "','" + ddl_six_month_9_time_9.ToString() + "','" + ddl_six_month_10_time_10.ToString() + "','" + ddl_six_month_11_time_11.ToString() + "','" + ddl_six_month_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                     if (insert_result1 > 0)
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                     }
                     else
                     {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                     }


                 }
            }
        }
        catch (Exception ex) { throw ex; }

        finally
        {
            d.con.Close();
        }


    }



    public void foreach_yearly()
    {

        try
        {
            d.con.Open();

            foreach (GridViewRow row in gridview_yearly.Rows)
            {
                var checkbox = row.FindControl("chk_client") as CheckBox;
                if (checkbox.Checked == true)
                {

                    Label lbl_grade_yearly = (Label)row.FindControl("lbl_grade_yearly");
                    string lbl_grade_yearly_1 = (lbl_grade_yearly.Text);

                    Label lbl_description_yearly = (Label)row.FindControl("lbl_description_yearly");
                    string lbl_description_yearly_1 = (lbl_description_yearly.Text);

                    Label lbl_type_yearly = (Label)row.FindControl("lbl_type_yearly");
                    string lbl_type_yearly_1 = (lbl_type_yearly.Text);

                    TextBox lbl_time_yearly = (TextBox)row.FindControl("lbl_time_yearly");
                    string lbl_time_yearly_1 = (lbl_time_yearly.Text);


                    DropDownList ddl_yearly_1_time = (DropDownList)row.FindControl("ddl_yearly_1_time");
                    string ddl_yearly_1_time_1 = ddl_yearly_1_time.Text.ToString();

                    DropDownList ddl_yearly_2_time = (DropDownList)row.FindControl("ddl_yearly_2_time");
                    string ddl_yearly_2_time_2 = ddl_yearly_2_time.Text.ToString();

                    DropDownList ddl_yearly_3_time = (DropDownList)row.FindControl("ddl_yearly_3_time");
                    string ddl_yearly_3_time_3 = ddl_yearly_3_time.Text.ToString();


                    DropDownList ddl_yearly_4_time = (DropDownList)row.FindControl("ddl_yearly_4_time");
                    string ddl_yearly_4_time_4 = ddl_yearly_4_time.Text.ToString();

                    DropDownList ddl_yearly_5_time = (DropDownList)row.FindControl("ddl_yearly_5_time");
                    string ddl_yearly_5_time_5 = ddl_yearly_5_time.Text.ToString();

                    DropDownList ddl_yearly_6_time = (DropDownList)row.FindControl("ddl_yearly_6_time");
                    string ddl_yearly_6_time_6 = ddl_yearly_6_time.Text.ToString();


                    DropDownList ddl_yearly_7_time = (DropDownList)row.FindControl("ddl_yearly_7_time");
                    string ddl_yearly_7_time_7 = ddl_yearly_7_time.Text.ToString();


                    DropDownList ddl_yearly_8_time = (DropDownList)row.FindControl("ddl_yearly_8_time");
                    string ddl_yearly_8_time_8 = ddl_yearly_8_time.Text.ToString();


                    DropDownList ddl_yearly_9_time = (DropDownList)row.FindControl("ddl_yearly_9_time");
                    string ddl_yearly_9_time_9 = ddl_yearly_9_time.Text.ToString();

                    DropDownList ddl_yearly_10_time = (DropDownList)row.FindControl("ddl_yearly_10_time");
                    string ddl_yearly_10_time_10 = ddl_yearly_10_time.Text.ToString();


                    DropDownList ddl_yearly_11_time = (DropDownList)row.FindControl("ddl_yearly_11_time");
                    string ddl_yearly_11_time_11 = ddl_yearly_11_time.Text.ToString();

                    DropDownList ddl_yearly_12_time = (DropDownList)row.FindControl("ddl_yearly_12_time");
                    string ddl_yearly_12_time_12 = ddl_yearly_12_time.Text.ToString();


                    string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                    int insert_result1 = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code,client_code,unit_code,flag)values('" + id + "','" + lbl_type_yearly_1.ToString() + "','" + lbl_grade_yearly_1.ToString() + "','" + lbl_description_yearly_1.ToString() + "','" + lbl_time_yearly_1.ToString() + "','" + ddl_yearly_1_time_1.ToString() + "','" + ddl_yearly_2_time_2.ToString() + "','" + ddl_yearly_3_time_3.ToString() + "','" + ddl_yearly_4_time_4.ToString() + "','" + ddl_yearly_5_time_5.ToString() + "','" + ddl_yearly_6_time_6.ToString() + "','" + ddl_yearly_7_time_7.ToString() + "','" + ddl_yearly_8_time_8.ToString() + "','" + ddl_yearly_9_time_9.ToString() + "','" + ddl_yearly_10_time_10.ToString() + "','" + ddl_yearly_11_time_11.ToString() + "','" + ddl_yearly_12_time_12.ToString() + "','" + Session["comp_code"].ToString() + "','" + ddl_unit_client.SelectedValue + "','" + ddl_unitcode.SelectedValue + "','1')");

                    if (insert_result1 > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                    }

                }
            }
        }
        catch (Exception ex) { throw ex; }

        finally
        {
            d.con.Close();
        }


    }







protected void btn_update_Click(object sender, EventArgs e)
{

    try
    {

        d.con.Open();


        foreach (GridViewRow row in gv_client_policy.Rows)
        {
            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {


                Label id = (Label)row.FindControl("id");
                string id1 = (id.Text);

                Label lbl_grade = (Label)row.FindControl("lbl_grade");
                string lbl_grade_1 = (lbl_grade.Text);

                Label lbl_description = (Label)row.FindControl("lbl_description");
                string lbl_description_1 = (lbl_description.Text);

                Label lbl_type = (Label)row.FindControl("lbl_type");
                string lbl_type_1 = (lbl_type.Text);

                TextBox lbl_time = (TextBox)row.FindControl("lbl_time");
                string lbl_time_1 = (lbl_time.Text);


                DropDownList ddl_daily_1_time = (DropDownList)row.FindControl("ddl_daily_1_time");
                string ddl_daily_1_time_1 = ddl_daily_1_time.Text.ToString();

                DropDownList ddl_daily_2_time = (DropDownList)row.FindControl("ddl_daily_2_time");
                string ddl_daily_2_time_2 = ddl_daily_2_time.Text.ToString();

                DropDownList ddl_daily_3_time = (DropDownList)row.FindControl("ddl_daily_3_time");
                string ddl_daily_3_time_3 = ddl_daily_3_time.Text.ToString();


                DropDownList ddl_daily_4_time = (DropDownList)row.FindControl("ddl_daily_4_time");
                string ddl_daily_4_time_4 = ddl_daily_4_time.Text.ToString();

                DropDownList ddl_daily_5_time = (DropDownList)row.FindControl("ddl_daily_5_time");
                string ddl_daily_5_time_5 = ddl_daily_5_time.Text.ToString();

                DropDownList ddl_daily_6_time = (DropDownList)row.FindControl("ddl_daily_6_time");
                string ddl_daily_6_time_6 = ddl_daily_6_time.Text.ToString();


                DropDownList ddl_daily_7_time = (DropDownList)row.FindControl("ddl_daily_7_time");
                string ddl_daily_7_time_7 = ddl_daily_7_time.Text.ToString();


                DropDownList ddl_daily_8_time = (DropDownList)row.FindControl("ddl_daily_8_time");
                string ddl_daily_8_time_8 = ddl_daily_8_time.Text.ToString();


                DropDownList ddl_daily_9_time = (DropDownList)row.FindControl("ddl_daily_9_time");
                string ddl_daily_9_time_9 = ddl_daily_9_time.Text.ToString();

                DropDownList ddl_daily_10_time = (DropDownList)row.FindControl("ddl_daily_10_time");
                string ddl_daily_10_time_10 = ddl_daily_10_time.Text.ToString();


                DropDownList ddl_daily_11_time = (DropDownList)row.FindControl("ddl_daily_11_time");
                string ddl_daily_11_time_11 = ddl_daily_11_time.Text.ToString();

                DropDownList ddl_daily_12_time = (DropDownList)row.FindControl("ddl_daily_12_time");
                string ddl_daily_12_time_12 = ddl_daily_12_time.Text.ToString();

//string id1 = gv_client_policy.SelectedRow.Cells[1].Text.ToString();

                int insert_result = d.operation("update pay_client_gps_policy set time = '" + lbl_time_1.ToString() + "',`1_time`='" + ddl_daily_1_time_1.ToString() + "',`2_time`='" + ddl_daily_2_time_2.ToString() + "',`3_time` = '" + ddl_daily_3_time_3.ToString() + "' ,`4_time` = '" + ddl_daily_4_time_4.ToString() + "',`5_time`= '" + ddl_daily_5_time_5.ToString() + "',`6_time` = '" + ddl_daily_6_time_6.ToString() + "',`7_time` = '" + ddl_daily_7_time_7.ToString() + "',`8_time` = '" + ddl_daily_8_time_8.ToString() + "',`9_time` = '" + ddl_daily_9_time_9.ToString() + "',`10_time` = '" + ddl_daily_10_time_10.ToString() + "',`11_time` = '" + ddl_daily_11_time_11.ToString() + "',`12_time` = '" + ddl_daily_12_time_12.ToString() + "' where Id = '"+id1+"'");

                if (insert_result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Updated... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record NOt Updated... !!!');", true);
                }
            }
        }


        weekly_update();
        fourth_nightly_update();
        monthly_update();
        quarterly_update();
        update_six_monthly();
        update_yearly();
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }
}



public void weekly_update()
{
    try
    {
        d6.con.Open();



        foreach (GridViewRow row in gridview_weekly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id1 = (id.Text);

                Label lbl_grade_weekly = (Label)row.FindControl("lbl_grade_weekly");
                string lbl_grade_weekly_1 = (lbl_grade_weekly.Text);

                Label lbl_description_weekly = (Label)row.FindControl("lbl_description_weekly");
                string lbl_description_weekly_1 = (lbl_description_weekly.Text);

                Label lbl_type_weekly = (Label)row.FindControl("lbl_type_weekly");
                string lbl_type_weekly_1 = (lbl_type_weekly.Text);

                TextBox lbl_time_weekly = (TextBox)row.FindControl("lbl_time_weekly");
                string lbl_time_weekly_1 = (lbl_time_weekly.Text);


                DropDownList ddl_weekly_1_time = (DropDownList)row.FindControl("ddl_weekly_1_time");
                string ddl_weekly_1_time_1 = ddl_weekly_1_time.Text.ToString();

                DropDownList ddl_weekly_2_time = (DropDownList)row.FindControl("ddl_weekly_2_time");
                string ddl_weekly_2_time_2 = ddl_weekly_2_time.Text.ToString();

                DropDownList ddl_weekly_3_time = (DropDownList)row.FindControl("ddl_weekly_3_time");
                string ddl_weekly_3_time_3 = ddl_weekly_3_time.Text.ToString();


                DropDownList ddl_weekly_4_time = (DropDownList)row.FindControl("ddl_weekly_4_time");
                string ddl_weekly_4_time_4 = ddl_weekly_4_time.Text.ToString();

                DropDownList ddl_weekly_5_time = (DropDownList)row.FindControl("ddl_weekly_5_time");
                string ddl_weekly_5_time_5 = ddl_weekly_5_time.Text.ToString();

                DropDownList ddl_weekly_6_time = (DropDownList)row.FindControl("ddl_weekly_6_time");
                string ddl_weekly_6_time_6 = ddl_weekly_6_time.Text.ToString();


                DropDownList ddl_weekly_7_time = (DropDownList)row.FindControl("ddl_weekly_7_time");
                string ddl_weekly_7_time_7 = ddl_weekly_7_time.Text.ToString();


                DropDownList ddl_weekly_8_time = (DropDownList)row.FindControl("ddl_weekly_8_time");
                string ddl_weekly_8_time_8 = ddl_weekly_8_time.Text.ToString();


                DropDownList ddl_weekly_9_time = (DropDownList)row.FindControl("ddl_weekly_9_time");
                string ddl_weekly_9_time_9 = ddl_weekly_9_time.Text.ToString();

                DropDownList ddl_weekly_10_time = (DropDownList)row.FindControl("ddl_weekly_10_time");
                string ddl_weekly_10_time_10 = ddl_weekly_10_time.Text.ToString();


                DropDownList ddl_weekly_11_time = (DropDownList)row.FindControl("ddl_weekly_11_time");
                string ddl_weekly_11_time_11 = ddl_weekly_11_time.Text.ToString();

                DropDownList ddl_weekly_12_time = (DropDownList)row.FindControl("ddl_weekly_12_time");
                string ddl_weekly_12_time_12 = ddl_weekly_12_time.Text.ToString();


               // string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("update pay_client_gps_policy set time = '" + lbl_time_weekly_1.ToString() + "',`1_time`='" + ddl_weekly_1_time_1.ToString() + "',`2_time`='" + ddl_weekly_2_time_2.ToString() + "',`3_time` = '" + ddl_weekly_3_time_3.ToString() + "' ,`4_time` = '" + ddl_weekly_4_time_4.ToString() + "',`5_time`= '" + ddl_weekly_5_time_5.ToString() + "',`6_time` = '" + ddl_weekly_6_time_6.ToString() + "',`7_time` = '" + ddl_weekly_7_time_7.ToString() + "',`8_time` = '" + ddl_weekly_8_time_8.ToString() + "',`9_time` = '" + ddl_weekly_9_time_9.ToString() + "',`10_time` = '" + ddl_weekly_10_time_10.ToString() + "',`11_time` = '" + ddl_weekly_11_time_11.ToString() + "',`12_time` = '" + ddl_weekly_12_time_12.ToString() + "' where Id = '" + id1 + "'");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Updated... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Updated Failed... !!!');", true);
                }
            }
        }
    
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d6.con.Close();
    }

}


public void fourth_nightly_update()
{

    try {
        d.con.Open();


        foreach (GridViewRow row in gridview_fourth_nightly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id2 = (id.Text);

                Label lbl_grade_fourth_nightly = (Label)row.FindControl("lbl_grade_fourth_nightly");
                string lbl_grade_fourth_nightly_1 = (lbl_grade_fourth_nightly.Text);

                Label lbl_description_fourth_nightly = (Label)row.FindControl("lbl_description_fourth_nightly");
                string lbl_description_fourth_nightly_1 = (lbl_description_fourth_nightly.Text);

                Label lbl_type_fourth_nightly = (Label)row.FindControl("lbl_type_fourth_nightly");
                string lbl_type_fourth_nightly_1 = (lbl_type_fourth_nightly.Text);

                TextBox lbl_time_fourth_nightly = (TextBox)row.FindControl("lbl_time_fourth_nightly");
                string lbl_time_fourth_nightly_1 = (lbl_time_fourth_nightly.Text);


                DropDownList ddl_fourth_1_time = (DropDownList)row.FindControl("ddl_fourth_1_time");
                string ddl_fourth_1_time_1 = ddl_fourth_1_time.Text.ToString();

                DropDownList ddl_fourth_2_time = (DropDownList)row.FindControl("ddl_fourth_2_time");
                string ddl_fourth_2_time_2 = ddl_fourth_2_time.Text.ToString();

                DropDownList ddl_fourth_3_time = (DropDownList)row.FindControl("ddl_fourth_3_time");
                string ddl_fourth_3_time_3 = ddl_fourth_3_time.Text.ToString();


                DropDownList ddl_fourth_4_time = (DropDownList)row.FindControl("ddl_fourth_4_time");
                string ddl_fourth_4_time_4 = ddl_fourth_4_time.Text.ToString();

                DropDownList ddl_fourth_5_time = (DropDownList)row.FindControl("ddl_fourth_5_time");
                string ddl_fourth_5_time_5 = ddl_fourth_5_time.Text.ToString();

                DropDownList ddl_fourth_6_time = (DropDownList)row.FindControl("ddl_fourth_6_time");
                string ddl_fourth_6_time_6 = ddl_fourth_6_time.Text.ToString();


                DropDownList ddl_fourth_7_time = (DropDownList)row.FindControl("ddl_fourth_7_time");
                string ddl_fourth_7_time_7 = ddl_fourth_7_time.Text.ToString();


                DropDownList ddl_fourth_8_time = (DropDownList)row.FindControl("ddl_fourth_8_time");
                string ddl_fourth_8_time_8 = ddl_fourth_8_time.Text.ToString();


                DropDownList ddl_fourth_9_time = (DropDownList)row.FindControl("ddl_fourth_9_time");
                string ddl_fourth_9_time_9 = ddl_fourth_9_time.Text.ToString();

                DropDownList ddl_fourth_10_time = (DropDownList)row.FindControl("ddl_fourth_10_time");
                string ddl_fourth_10_time_10 = ddl_fourth_10_time.Text.ToString();


                DropDownList ddl_fourth_11_time = (DropDownList)row.FindControl("ddl_fourth_11_time");
                string ddl_fourth_11_time_11 = ddl_fourth_11_time.Text.ToString();

                DropDownList ddl_fourth_12_time = (DropDownList)row.FindControl("ddl_fourth_12_time");
                string ddl_fourth_12_time_12 = ddl_fourth_12_time.Text.ToString();


                //string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("update pay_client_gps_policy set time = '" + lbl_time_fourth_nightly_1.ToString() + "',`1_time`='" + ddl_fourth_1_time_1.ToString() + "',`2_time`='" + ddl_fourth_2_time_2.ToString() + "',`3_time` = '" + ddl_fourth_3_time_3.ToString() + "' ,`4_time` = '" + ddl_fourth_4_time_4.ToString() + "',`5_time`= '" + ddl_fourth_5_time_5.ToString() + "',`6_time` = '" + ddl_fourth_6_time_6.ToString() + "',`7_time` = '" + ddl_fourth_7_time_7.ToString() + "',`8_time` = '" + ddl_fourth_8_time_8.ToString() + "',`9_time` = '" + ddl_fourth_9_time_9.ToString() + "',`10_time` = '" + ddl_fourth_10_time_10.ToString() + "',`11_time` = '" + ddl_fourth_11_time_11.ToString() + "',`12_time` = '" + ddl_fourth_12_time_12.ToString() + "' where Id = '" + id2 + "'");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Updated... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Updated Failed... !!!');", true);
                }
            }
        }
    
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }

}


public void monthly_update()

{

    try
    {
        d.con.Open();


        foreach (GridViewRow row in gridview_monthly.Rows)
        {
            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id3 = (id.Text);


                Label lbl_grade_monthly = (Label)row.FindControl("lbl_grade_monthly");
                string lbl_grade_monthly_1 = (lbl_grade_monthly.Text);

                Label lbl_description_monthly = (Label)row.FindControl("lbl_description_monthly");
                string lbl_description_monthly_1 = (lbl_description_monthly.Text);

                Label lbl_type_monthly = (Label)row.FindControl("lbl_type_monthly");
                string lbl_type_monthly_1 = (lbl_type_monthly.Text);

                TextBox lbl_time_monthly = (TextBox)row.FindControl("lbl_time_monthly");
                string lbl_time_monthly_1 = (lbl_time_monthly.Text);


                DropDownList ddl_monthly_1_time = (DropDownList)row.FindControl("ddl_monthly_1_time");
                string ddl_monthly_1_time_1 = ddl_monthly_1_time.Text.ToString();

                DropDownList ddl_monthly_2_time = (DropDownList)row.FindControl("ddl_monthly_2_time");
                string ddl_monthly_2_time_2 = ddl_monthly_2_time.Text.ToString();

                DropDownList ddl_monthly_3_time = (DropDownList)row.FindControl("ddl_monthly_3_time");
                string ddl_monthly_3_time_3 = ddl_monthly_3_time.Text.ToString();


                DropDownList ddl_monthly_4_time = (DropDownList)row.FindControl("ddl_monthly_4_time");
                string ddl_monthly_4_time_4 = ddl_monthly_4_time.Text.ToString();

                DropDownList ddl_monthly_5_time = (DropDownList)row.FindControl("ddl_monthly_5_time");
                string ddl_monthly_5_time_5 = ddl_monthly_5_time.Text.ToString();

                DropDownList ddl_monthly_6_time = (DropDownList)row.FindControl("ddl_monthly_6_time");
                string ddl_monthly_6_time_6 = ddl_monthly_6_time.Text.ToString();


                DropDownList ddl_monthly_7_time = (DropDownList)row.FindControl("ddl_monthly_7_time");
                string ddl_monthly_7_time_7 = ddl_monthly_7_time.Text.ToString();


                DropDownList ddl_monthly_8_time = (DropDownList)row.FindControl("ddl_monthly_8_time");
                string ddl_monthly_8_time_8 = ddl_monthly_8_time.Text.ToString();


                DropDownList ddl_monthly_9_time = (DropDownList)row.FindControl("ddl_monthly_9_time");
                string ddl_monthly_9_time_9 = ddl_monthly_9_time.Text.ToString();

                DropDownList ddl_monthly_10_time = (DropDownList)row.FindControl("ddl_monthly_10_time");
                string ddl_monthly_10_time_10 = ddl_monthly_10_time.Text.ToString();


                DropDownList ddl_monthly_11_time = (DropDownList)row.FindControl("ddl_monthly_11_time");
                string ddl_monthly_11_time_11 = ddl_monthly_11_time.Text.ToString();

                DropDownList ddl_monthly_12_time = (DropDownList)row.FindControl("ddl_monthly_12_time");
                string ddl_monthly_12_time_12 = ddl_monthly_12_time.Text.ToString();


                //string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("update pay_client_gps_policy set time = '" + lbl_time_monthly_1.ToString() + "',`1_time`='" + ddl_monthly_1_time_1.ToString() + "',`2_time`='" + ddl_monthly_2_time_2.ToString() + "',`3_time` = '" + ddl_monthly_3_time_3.ToString() + "' ,`4_time` = '" + ddl_monthly_4_time_4.ToString() + "',`5_time`= '" + ddl_monthly_5_time_5.ToString() + "',`6_time` = '" + ddl_monthly_6_time_6.ToString() + "',`7_time` = '" + ddl_monthly_7_time_7.ToString() + "',`8_time` = '" + ddl_monthly_8_time_8.ToString() + "',`9_time` = '" + ddl_monthly_9_time_9.ToString() + "',`10_time` = '" + ddl_monthly_10_time_10.ToString() + "',`11_time` = '" + ddl_monthly_11_time_11.ToString() + "',`12_time` = '" + ddl_monthly_12_time_12.ToString() + "' where Id = '" + id3 + "'");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Updated... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Updated Failed... !!!');", true);
                }
            }

        }
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }


}




public void quarterly_update() 
{
    try {
        d.con.Open();


        foreach (GridViewRow row in gridview_quarterly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id4 = (id.Text);


                Label lbl_grade_quarterly = (Label)row.FindControl("lbl_grade_quarterly");
                string lbl_grade_quarterly_1 = (lbl_grade_quarterly.Text);

                Label lbl_description_quarterly = (Label)row.FindControl("lbl_description_quarterly");
                string lbl_description_quarterly_1 = (lbl_description_quarterly.Text);

                Label lbl_type_quarterly = (Label)row.FindControl("lbl_type_quarterly");
                string lbl_type_quarterly_1 = (lbl_type_quarterly.Text);

                TextBox lbl_time_quarterly = (TextBox)row.FindControl("lbl_time_quarterly");
                string lbl_time_quarterly_1 = (lbl_time_quarterly.Text);


                DropDownList ddl_quarterly_1_time = (DropDownList)row.FindControl("ddl_quarterly_1_time");
                string ddl_quarterly_1_time_1 = ddl_quarterly_1_time.Text.ToString();

                DropDownList ddl_quarterly_2_time = (DropDownList)row.FindControl("ddl_quarterly_2_time");
                string ddl_quarterly_2_time_2 = ddl_quarterly_2_time.Text.ToString();

                DropDownList ddl_quarterly_3_time = (DropDownList)row.FindControl("ddl_quarterly_3_time");
                string ddl_quarterly_3_time_3 = ddl_quarterly_3_time.Text.ToString();


                DropDownList ddl_quarterly_4_time = (DropDownList)row.FindControl("ddl_quarterly_4_time");
                string ddl_quarterly_4_time_4 = ddl_quarterly_4_time.Text.ToString();

                DropDownList ddl_quarterly_5_time = (DropDownList)row.FindControl("ddl_quarterly_5_time");
                string ddl_quarterly_5_time_5 = ddl_quarterly_5_time.Text.ToString();

                DropDownList ddl_quarterly_6_time = (DropDownList)row.FindControl("ddl_quarterly_6_time");
                string ddl_quarterly_6_time_6 = ddl_quarterly_6_time.Text.ToString();


                DropDownList ddl_quarterly_7_time = (DropDownList)row.FindControl("ddl_quarterly_7_time");
                string ddl_quarterly_7_time_7 = ddl_quarterly_7_time.Text.ToString();


                DropDownList ddl_quarterly_8_time = (DropDownList)row.FindControl("ddl_quarterly_8_time");
                string ddl_quarterly_8_time_8 = ddl_quarterly_8_time.Text.ToString();


                DropDownList ddl_quarterly_9_time = (DropDownList)row.FindControl("ddl_quarterly_9_time");
                string ddl_quarterly_9_time_9 = ddl_quarterly_9_time.Text.ToString();

                DropDownList ddl_quarterly_10_time = (DropDownList)row.FindControl("ddl_quarterly_10_time");
                string ddl_quarterly_10_time_10 = ddl_quarterly_10_time.Text.ToString();


                DropDownList ddl_quarterly_11_time = (DropDownList)row.FindControl("ddl_quarterly_11_time");
                string ddl_quarterly_11_time_11 = ddl_quarterly_11_time.Text.ToString();

                DropDownList ddl_quarterly_12_time = (DropDownList)row.FindControl("ddl_quarterly_12_time");
                string ddl_quarterly_12_time_12 = ddl_quarterly_12_time.Text.ToString();


                //string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("update pay_client_gps_policy set time = '" + lbl_time_quarterly_1.ToString() + "',`1_time`='" + ddl_quarterly_1_time_1.ToString() + "',`2_time`='" + ddl_quarterly_2_time_2.ToString() + "',`3_time` = '" + ddl_quarterly_3_time_3.ToString() + "' ,`4_time` = '" + ddl_quarterly_4_time_4.ToString() + "',`5_time`= '" + ddl_quarterly_5_time_5.ToString() + "',`6_time` = '" + ddl_quarterly_6_time_6.ToString() + "',`7_time` = '" + ddl_quarterly_7_time_7.ToString() + "',`8_time` = '" + ddl_quarterly_8_time_8.ToString() + "',`9_time` = '" + ddl_quarterly_9_time_9.ToString() + "',`10_time` = '" + ddl_quarterly_10_time_10.ToString() + "',`11_time` = '" + ddl_quarterly_11_time_11.ToString() + "',`12_time` = '" + ddl_quarterly_12_time_12.ToString() + "' where Id = '" + id4 + "'");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                }


            }
        }

    
    }

    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }
}


public void update_six_monthly()
{
    try {

        d.con.Open();


        foreach (GridViewRow row in gridview_six_monthly.Rows)
        {

            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id5 = (id.Text);




                Label lbl_grade_six_month = (Label)row.FindControl("lbl_grade_six_month");
                string lbl_grade_six_month_1 = (lbl_grade_six_month.Text);

                Label lbl_description_six_month = (Label)row.FindControl("lbl_description_six_month");
                string lbl_description_six_month_1 = (lbl_description_six_month.Text);

                Label lbl_type_six_month = (Label)row.FindControl("lbl_type_six_month");
                string lbl_type_six_month_1 = (lbl_type_six_month.Text);

                TextBox lbl_time_six_month = (TextBox)row.FindControl("lbl_time_six_month");
                string lbl_time_six_month_1 = (lbl_time_six_month.Text);


                DropDownList ddl_six_month_1_time = (DropDownList)row.FindControl("ddl_six_month_1_time");
                string ddl_six_month_1_time_1 = ddl_six_month_1_time.Text.ToString();

                DropDownList ddl_six_month_2_time = (DropDownList)row.FindControl("ddl_six_month_2_time");
                string ddl_six_month_2_time_2 = ddl_six_month_2_time.Text.ToString();

                DropDownList ddl_six_month_3_time = (DropDownList)row.FindControl("ddl_six_month_3_time");
                string ddl_six_month_3_time_3 = ddl_six_month_3_time.Text.ToString();


                DropDownList ddl_six_month_4_time = (DropDownList)row.FindControl("ddl_six_month_4_time");
                string ddl_six_month_4_time_4 = ddl_six_month_4_time.Text.ToString();

                DropDownList ddl_six_month_5_time = (DropDownList)row.FindControl("ddl_six_month_5_time");
                string ddl_six_month_5_time_5 = ddl_six_month_5_time.Text.ToString();

                DropDownList ddl_six_month_6_time = (DropDownList)row.FindControl("ddl_six_month_6_time");
                string ddl_six_month_6_time_6 = ddl_six_month_6_time.Text.ToString();


                DropDownList ddl_six_month_7_time = (DropDownList)row.FindControl("ddl_six_month_7_time");
                string ddl_six_month_7_time_7 = ddl_six_month_7_time.Text.ToString();


                DropDownList ddl_six_month_8_time = (DropDownList)row.FindControl("ddl_six_month_8_time");
                string ddl_six_month_8_time_8 = ddl_six_month_8_time.Text.ToString();


                DropDownList ddl_six_month_9_time = (DropDownList)row.FindControl("ddl_six_month_9_time");
                string ddl_six_month_9_time_9 = ddl_six_month_9_time.Text.ToString();

                DropDownList ddl_six_month_10_time = (DropDownList)row.FindControl("ddl_six_month_10_time");
                string ddl_six_month_10_time_10 = ddl_six_month_10_time.Text.ToString();


                DropDownList ddl_six_month_11_time = (DropDownList)row.FindControl("ddl_six_month_11_time");
                string ddl_six_month_11_time_11 = ddl_six_month_11_time.Text.ToString();

                DropDownList ddl_six_month_12_time = (DropDownList)row.FindControl("ddl_six_month_12_time");
                string ddl_six_month_12_time_12 = ddl_six_month_12_time.Text.ToString();


                //string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation(" update pay_client_gps_policy set time = '" + lbl_time_six_month_1.ToString() + "',`1_time`='" + ddl_six_month_1_time_1.ToString() + "',`2_time`='" + ddl_six_month_2_time_2.ToString() + "',`3_time` = '" + ddl_six_month_3_time_3.ToString() + "' ,`4_time` = '" + ddl_six_month_4_time_4.ToString() + "',`5_time`= '" + ddl_six_month_5_time_5.ToString() + "',`6_time` = '" + ddl_six_month_6_time_6.ToString() + "',`7_time` = '" + ddl_six_month_7_time_7.ToString() + "',`8_time` = '" + ddl_six_month_8_time_8.ToString() + "',`9_time` = '" + ddl_six_month_9_time_9.ToString() + "',`10_time` = '" + ddl_six_month_10_time_10.ToString() + "',`11_time` = '" + ddl_six_month_11_time_11.ToString() + "',`12_time` = '" + ddl_six_month_12_time_12.ToString() + "' where Id = '" + id5 + "'");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Updated... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Updated Failed... !!!');", true);
                }



            }

        }


    }


    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }


}

public void update_yearly()
{
    try {
        d.con.Open();


        foreach (GridViewRow row in gridview_yearly.Rows)
        {


            var checkbox = row.FindControl("chk_client") as CheckBox;
            if (checkbox.Checked == true)
            {

                Label id = (Label)row.FindControl("id");
                string id6 = (id.Text);

                Label lbl_grade_yearly = (Label)row.FindControl("lbl_grade_yearly");
                string lbl_grade_yearly_1 = (lbl_grade_yearly.Text);

                Label lbl_description_yearly = (Label)row.FindControl("lbl_description_yearly");
                string lbl_description_yearly_1 = (lbl_description_yearly.Text);

                Label lbl_type_yearly = (Label)row.FindControl("lbl_type_yearly");
                string lbl_type_yearly_1 = (lbl_type_yearly.Text);

                TextBox lbl_time_yearly = (TextBox)row.FindControl("lbl_time_yearly");
                string lbl_time_yearly_1 = (lbl_time_yearly.Text);


                DropDownList ddl_yearly_1_time = (DropDownList)row.FindControl("ddl_yearly_1_time");
                string ddl_yearly_1_time_1 = ddl_yearly_1_time.Text.ToString();

                DropDownList ddl_yearly_2_time = (DropDownList)row.FindControl("ddl_yearly_2_time");
                string ddl_yearly_2_time_2 = ddl_yearly_2_time.Text.ToString();

                DropDownList ddl_yearly_3_time = (DropDownList)row.FindControl("ddl_yearly_3_time");
                string ddl_yearly_3_time_3 = ddl_yearly_3_time.Text.ToString();


                DropDownList ddl_yearly_4_time = (DropDownList)row.FindControl("ddl_yearly_4_time");
                string ddl_yearly_4_time_4 = ddl_yearly_4_time.Text.ToString();

                DropDownList ddl_yearly_5_time = (DropDownList)row.FindControl("ddl_yearly_5_time");
                string ddl_yearly_5_time_5 = ddl_yearly_5_time.Text.ToString();

                DropDownList ddl_yearly_6_time = (DropDownList)row.FindControl("ddl_yearly_6_time");
                string ddl_yearly_6_time_6 = ddl_yearly_6_time.Text.ToString();


                DropDownList ddl_yearly_7_time = (DropDownList)row.FindControl("ddl_yearly_7_time");
                string ddl_yearly_7_time_7 = ddl_yearly_7_time.Text.ToString();


                DropDownList ddl_yearly_8_time = (DropDownList)row.FindControl("ddl_yearly_8_time");
                string ddl_yearly_8_time_8 = ddl_yearly_8_time.Text.ToString();


                DropDownList ddl_yearly_9_time = (DropDownList)row.FindControl("ddl_yearly_9_time");
                string ddl_yearly_9_time_9 = ddl_yearly_9_time.Text.ToString();

                DropDownList ddl_yearly_10_time = (DropDownList)row.FindControl("ddl_yearly_10_time");
                string ddl_yearly_10_time_10 = ddl_yearly_10_time.Text.ToString();


                DropDownList ddl_yearly_11_time = (DropDownList)row.FindControl("ddl_yearly_11_time");
                string ddl_yearly_11_time_11 = ddl_yearly_11_time.Text.ToString();

                DropDownList ddl_yearly_12_time = (DropDownList)row.FindControl("ddl_yearly_12_time");
                string ddl_yearly_12_time_12 = ddl_yearly_12_time.Text.ToString();


               // string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master ");
                int insert_result1 = d.operation("update pay_client_gps_policy set time = '" + lbl_time_yearly_1.ToString() + "',`1_time`='" + ddl_yearly_1_time_1.ToString() + "',`2_time`='" + ddl_yearly_2_time_2.ToString() + "',`3_time` = '" + ddl_yearly_3_time_3.ToString() + "' ,`4_time` = '" + ddl_yearly_4_time_4.ToString() + "',`5_time`= '" + ddl_yearly_5_time_5.ToString() + "',`6_time` = '" + ddl_yearly_6_time_6.ToString() + "',`7_time` = '" + ddl_yearly_7_time_7.ToString() + "',`8_time` = '" + ddl_yearly_8_time_8.ToString() + "',`9_time` = '" + ddl_yearly_9_time_9.ToString() + "',`10_time` = '" + ddl_yearly_10_time_10.ToString() + "',`11_time` = '" + ddl_yearly_11_time_11.ToString() + "',`12_time` = '" + ddl_yearly_12_time_12.ToString() + "' where Id = '" + id6 + "' ");

                if (insert_result1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
                }

            }
        }
    
    }
    catch (Exception ex) { throw ex; }

    finally
    {
        d.con.Close();
    }



}












//protected void btn_weekly_add_Click(object sender, EventArgs e)
//{



//    DataTable dt1 = new DataTable();

//    MySqlDataAdapter client_weekly = new MySqlDataAdapter(" select branch_having_policy,designation from pay_client_gps_policy_master where `branch_having_policy` ='" + ddl_unitcode.SelectedItem + "'and 'designation'='" + ddl_designation.SelectedValue + "'", d.con);
//    d.con.Open();

//    try
//    {

//        client_weekly.Fill(dt1);

//        if (dt1.Rows.Count > 0)
//        {

//            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record  All Ready Existed... !!!');", true);
//        }
//        else
//        {
//            int result = 0;
//            result = d.operation("insert into pay_client_gps_policy_master(client_code,state,branch_having_policy,branch_not_having_policy,designation,duty_hours,new_policy_name,start_date,end_date,comp_code)values('" + ddl_unit_client.SelectedValue + "','" + ddl_clientwisestate.SelectedValue + "','" + ddl_unitcode.SelectedItem + "','select','" + ddl_designation.SelectedValue + "','" + ddl_hours.SelectedValue + "','select',str_to_date('" + txt_start_date.Text + "','%d/%m/%Y'),str_to_date('" + txt_end_date.Text + "','%d/%m/%Y'),'" + Session["comp_code"].ToString() + "') ");

//            string id = d.getsinglestring("select max(id )from pay_client_gps_policy_master where `branch_having_policy` ='" + ddl_unitcode.SelectedItem + "'");

//            if (result > 0)
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
//            }
//            else
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
//            }
//            foreach (GridViewRow row in gv_client_policy.Rows)
//           { 
//                 var checkbox = row.FindControl("chk_client") as CheckBox;
//                 if (checkbox.Checked == true)
//                 {

//                     Label lbl_grade_weekly = (Label)row.FindControl("lbl_grade_weekly");
//                     string lbl_grade_weekly_1 = (lbl_grade_weekly.Text);

//                     Label lbl_description_weekly = (Label)row.FindControl("lbl_description_weekly");
//                     string lbl_description_weekly_2 = (lbl_description_weekly.Text);

//                     Label lbl_type_weekly = (Label)row.FindControl("lbl_type_weekly");
//                     string lbl_type_weekly_3 = (lbl_type_weekly.Text);

//                     Label lbl_time_weekly = (Label)row.FindControl("lbl_time_weekly");
//                     string lbl_time_weekly_4 = (lbl_time_weekly.Text);

//                     DropDownList DropDownList_weekly_1 = (DropDownList)row.FindControl("DropDownList_weekly_1");
//                     string DropDownList_weekly_1_time = DropDownList_weekly_1.Text.ToString();


//                     DropDownList DropDownList_weekly_2 = (DropDownList)row.FindControl("DropDownList_weekly_2");
//                     string DropDownList_weekly_2_time = DropDownList_weekly_2.Text.ToString();

//                     DropDownList DropDownList_weekly_3 = (DropDownList)row.FindControl("DropDownList_weekly_3");
//                     string DropDownList_weekly_3_time = DropDownList_weekly_3.Text.ToString();

//                     DropDownList DropDownList_weekly_4 = (DropDownList)row.FindControl("DropDownList_weekly_4");
//                     string DropDownList_weekly_4_time = DropDownList_weekly_4.Text.ToString();

//                     DropDownList DropDownList_weekly_5 = (DropDownList)row.FindControl("DropDownList_weekly_5");
//                     string DropDownList_weekly_5_time = DropDownList_weekly_5.Text.ToString();

//                     DropDownList DropDownList_weekly_6 = (DropDownList)row.FindControl("DropDownList_weekly_6");
//                     string DropDownList_weekly_6_time = DropDownList_weekly_6.Text.ToString();

//                     DropDownList DropDownList_weekly_7 = (DropDownList)row.FindControl("DropDownList_weekly_7");
//                     string DropDownList_weekly_7_time = DropDownList_weekly_7.Text.ToString();

//                     DropDownList DropDownList_weekly_8 = (DropDownList)row.FindControl("DropDownList_weekly_8");
//                     string DropDownList_weekly_8_time = DropDownList_weekly_8.Text.ToString();

//                     DropDownList DropDownList_weekly_9 = (DropDownList)row.FindControl("DropDownList_weekly_9");
//                     string DropDownList_weekly_9_time = DropDownList_weekly_9.Text.ToString();

//                     DropDownList DropDownList_weekly_10 = (DropDownList)row.FindControl("DropDownList_weekly_10");
//                     string DropDownList_weekly_10_time = DropDownList_weekly_10.Text.ToString();

//                     DropDownList DropDownList_weekly_11 = (DropDownList)row.FindControl("DropDownList_weekly_11");
//                     string DropDownList_weekly_11_time = DropDownList_weekly_11.Text.ToString();

//                     DropDownList DropDownList_weekly_12 = (DropDownList)row.FindControl("DropDownList_weekly_12");
//                     string DropDownList_weekly_12_time = DropDownList_weekly_12.Text.ToString();

//                     int int_result = d.operation("insert into pay_client_gps_policy(cgpm_id,type,grade,description,time,1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time,comp_code)values('" + id + "','" + lbl_type_weekly_3.ToString() + "','" + lbl_grade_weekly_1.ToString() + "','" + lbl_description_weekly_2.ToString() + "','" + lbl_time_weekly_4.ToString() + "','" + DropDownList_weekly_1_time.ToString() + "','" + DropDownList_weekly_2_time.ToString() + "','" + DropDownList_weekly_3_time.ToString() + "','" + DropDownList_weekly_4_time.ToString() + "','" + DropDownList_weekly_5_time.ToString() + "','" + DropDownList_weekly_6_time.ToString() + "','" + DropDownList_weekly_7_time.ToString() + "','" + DropDownList_weekly_8_time.ToString() + "','" + DropDownList_weekly_9_time.ToString() + "','" + DropDownList_weekly_10_time.ToString() + "','" + DropDownList_weekly_11_time.ToString() + "','" + DropDownList_weekly_12_time.ToString() + "','" + Session["comp_code"].ToString() + "')");


//                     if (int_result > 0)
//                     {
//                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Successfully Added... !!!');", true);
//                     }
//                     else
//                     {
//                         ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Record Added Failed... !!!');", true);
//                     }
                 
//                 }
            
//             }

//        }
   
//    }

//    catch (Exception ex) { throw ex; }

//    finally
//    {
//        d.con.Close();
//    }

//}





protected void btn_fourth_n_add_Click(object sender, EventArgs e)
{



}







   protected void ddl_clientname_SelectedIndexChanged(object sender, EventArgs e)
   {
       ddl_clientwisestate.Items.Clear();
       // MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM pay_client_master where CLIENT_NAME='" + ddl_unit_client + "' and COMP_CODE='"+Session["COMP_CODE"].ToString()+"'", d1.con);
       MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct state FROM pay_designation_count where COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and unit_code is null ORDER BY STATE", d1.con);//and state in(select state_name from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "')
       d1.con.Open();
       try
       {
           MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
           while (dr_item1.Read())
           {
               ddl_clientwisestate.Items.Add(dr_item1[0].ToString());

           }
           dr_item1.Close();
           ddl_clientwisestate.Items.Insert(0, new ListItem("Select"));

           ddl_unitcode.Items.Clear();
           ddl_designation.Items.Clear();
           ddl_unitcode_without.Items.Clear();

           load_grdview();
          // grid_daily_client_select();
           btn_add.Visible = false;
           

       }
       catch (Exception ex) { throw ex; }
       finally
       {
           d1.con.Close();
       }

   }





   protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
   {
       IEnumerable<string> selectedValues = from item in ddl_unitcode.Items.Cast<ListItem>()
                                            where item.Selected
                                            select item.Value;
       string listvalues_ddl_unitcode = string.Join("','", selectedValues);
       IEnumerable<string> selectedValues_without = from item in ddl_unitcode_without.Items.Cast<ListItem>()
                                                    where item.Selected
                                                    select item.Value;
       listvalues_ddl_unitcode = listvalues_ddl_unitcode + "','" + string.Join("','", selectedValues_without);

       //ddl_Existing_policy_name.Items.Clear();
       //System.Data.DataTable dt_item = new System.Data.DataTable();
       //MySqlDataAdapter cmd_item = new MySqlDataAdapter("select distinct(POLICY_NAME1) FROM pay_billing_master where COMP_CODE='" + Session["COMP_CODE"] + "' and billing_client_code = '" + ddl_unit_client.SelectedValue + "' and billing_unit_code in ('" + listvalues_ddl_unitcode + "')", d.con1);
       //d.con.Open();
       //try
       //{
       //    cmd_item.Fill(dt_item);
       //    if (dt_item.Rows.Count > 0)
       //    {
       //        ddl_Existing_policy_name.DataSource = dt_item;
       //        ddl_Existing_policy_name.DataValueField = dt_item.Columns[0].ToString();
       //        ddl_Existing_policy_name.DataTextField = dt_item.Columns[0].ToString();
       //        ddl_Existing_policy_name.DataBind();
       //    }
       //    ddl_Existing_policy_name.Items.Insert(0, new ListItem("NEW", ""));
       //    d.con.Close();
       //}
       //catch (Exception ex) { throw ex; }
       //finally
       //{
       //    d.con.Close();
       //}

       d9.con.Open();
       ddl_designation.Items.Clear();
       DataTable dt_item = new System.Data.DataTable();
       //cmd_item = new MySqlDataAdapter("select GRADE_CODE, GRADE_DESC FROM pay_grade_master where COMP_CODE='" + Session["COMP_CODE"] + "'", d.con);
       MySqlDataAdapter cmd_item = new MySqlDataAdapter("SELECT DISTINCT(Select grade_code from pay_grade_master where grade_desc = pay_designation_count.designation and comp_code = '" + Session["COMP_CODE"].ToString() + "'),DESIGNATION from pay_designation_count WHERE comp_code ='" + Session["comp_code"].ToString() + "' and CLIENT_CODE='" + ddl_unit_client.SelectedValue + "' and UNIT_CODE in ('" + listvalues_ddl_unitcode + "')", d9.con);
       try
       {
           cmd_item.Fill(dt_item);
           if (dt_item.Rows.Count > 0)
           {
               ddl_designation.DataSource = dt_item;
               ddl_designation.DataValueField = dt_item.Columns[0].ToString();
               ddl_designation.DataTextField = dt_item.Columns[1].ToString();
               ddl_designation.DataBind();

           }
           //ddl_designation.Items.Insert(0, new ListItem("Select", ""));
           d9.con.Close();
           ddl_hours.Items.Clear();
       }
       catch (Exception ex) { throw ex; }
       finally
       {
           d9.con.Close();
       }
   }



   //public void grid_daily_client_select()
   //{
   //    try {
   //        d3.con.Open();

   //        MySqlCommand client_select = new MySqlCommand("SELECT  `type`,  `grade`,  `description`,  `time`,  `1_time`,  `2_time`,  `3_time`,  `4_time`,  `5_time`,  `6_time`,  `7_time`,  `8_time`,  `9_time`,  `10_time`,  `11_time`,'12_time' FROM  `pay_client_gps_policy` WHERE  `client_code` = '" + ddl_unit_client.SelectedValue + "' ", d3.con);
   //        MySqlDataAdapter dr_client = new MySqlDataAdapter(client_select);
   //        DataTable dt_client = new DataTable();
   //        dr_client.Fill(dt_client);

   //        if (dt_client.Rows.Count > 0)
   //        {


   //            gv_client_daily.DataSource = dt_client;
   //            gv_client_daily.DataBind();
   //            gv_client_policy.Visible = false;

   //        }
   //        gv_client_daily.Visible = true;
   //    }
   //    catch (Exception ex) { }
   //    finally { d3.con.Close(); }
   
   //}


   public void Grid_view_master_daily()
   {
       try {
           d.con.Open();

           MySqlCommand master_daily = new MySqlCommand(" Select pay_client_gps_policy_master.`Id`,pay_client_master.`client_name`,pay_client_gps_policy_master.`state`,`branch_having_policy`,`branch_not_having_policy`,pay_client_gps_policy_master.`designation`,pay_client_gps_policy_master.`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,pay_client_gps_policy_master.`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master INNER JOIN `pay_client_master` ON `pay_client_gps_policy_master`.`comp_code` = `pay_client_master`.`comp_code` AND `pay_client_gps_policy_master`.`client_code` = `pay_client_master`.`client_code`", d.con);
           MySqlDataAdapter dr_master1 = new MySqlDataAdapter(master_daily);
           DataTable dt_master1 = new DataTable();
           dr_master1.Fill(dt_master1);

           if (dt_master1.Rows.Count > 0)
           {


               GridView_daily_master.DataSource = dt_master1;
               GridView_daily_master.DataBind();
               GridView_daily_master.Visible = false;

           }
           GridView_daily_master.Visible = true;



       
       }

       catch (Exception ex) { }
       finally { d.con.Close(); }
   
   
   }

   //public void Grid_view_master_weekly()
   //{
   //    try {
   //        d.con.Open();

   //        MySqlCommand master_weekly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_weekly);
   //        DataTable dt_master2= new DataTable();
   //        dr_master2.Fill(dt_master2);

   //        if (dt_master2.Rows.Count > 0)
   //        {


   //            GridView_weekly_master.DataSource = dt_master2;
   //            GridView_weekly_master.DataBind();
   //            GridView_weekly_master.Visible = false;

   //        }
   //        GridView_weekly_master.Visible = true;



       
   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }
   
   //}


   //public void Grid_view_master_fourth_nightly()
   //{

   //    try {

   //        d.con.Open();


   //        MySqlCommand master_weekly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_weekly);
   //        DataTable dt_master2 = new DataTable();
   //        dr_master2.Fill(dt_master2);

   //        if (dt_master2.Rows.Count > 0)
   //        {


   //            GridView_fourth_master.DataSource = dt_master2;
   //            GridView_fourth_master.DataBind();
   //            GridView_fourth_master.Visible = false;

   //        }
   //        GridView_fourth_master.Visible = true;



       
   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }
   //}



   //public void Grid_view_master_monthly()
   //{
   //    try
   //    {
   //        d.con.Open();

   //        MySqlCommand master_monthly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master3 = new MySqlDataAdapter(master_monthly);
   //        DataTable dt_master3 = new DataTable();
   //        dr_master3.Fill(dt_master3);

   //        if (dt_master3.Rows.Count > 0)
   //        {


   //            GridView_monthly_master.DataSource = dt_master3;
   //            GridView_monthly_master.DataBind();
   //            GridView_monthly_master.Visible = false;

   //        }
   //        GridView_monthly_master.Visible = true;




   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }

   //}



   //public void Grid_view_master_quarterly()
   //{
   //    try
   //    {
   //        d.con.Open();

   //        MySqlCommand master_quarterly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master4 = new MySqlDataAdapter(master_quarterly);
   //        DataTable dt_master4 = new DataTable();
   //        dr_master4.Fill(dt_master4);

   //        if (dt_master4.Rows.Count > 0)
   //        {


   //            GridView_quarterly_master.DataSource = dt_master4;
   //            GridView_quarterly_master.DataBind();
   //            GridView_quarterly_master.Visible = false;

   //        }
   //        GridView_quarterly_master.Visible = true;




   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }

   //}


   //public void Grid_view_master_six_monthly()
   //{
   //    try
   //    {
   //        d.con.Open();

   //        MySqlCommand master_monthly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master5 = new MySqlDataAdapter(master_monthly);
   //        DataTable dt_master5 = new DataTable();
   //        dr_master5.Fill(dt_master5);

   //        if (dt_master5.Rows.Count > 0)
   //        {


   //            GridView_six_monthly_master.DataSource = dt_master5;
   //            GridView_six_monthly_master.DataBind();
   //            GridView_six_monthly_master.Visible = false;

   //        }
   //        GridView_six_monthly_master.Visible = true;




   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }

   //}



   //public void Grid_view_master_yearly()
   //{
   //    try
   //    {
   //        d.con.Open();

   //        MySqlCommand master_yearly = new MySqlCommand(" Select `Id`,`client_code`,`state`,`branch_having_policy`,`branch_not_having_policy`,`designation`,`designation`,`new_policy_name`,`start_date`,`end_date`,`duty_hours`,`comp_code`,`branch_having_policy_1` from pay_client_gps_policy_master ", d.con);
   //        MySqlDataAdapter dr_master6 = new MySqlDataAdapter(master_yearly);
   //        DataTable dt_master6 = new DataTable();
   //        dr_master6.Fill(dt_master6);

   //        if (dt_master6.Rows.Count > 0)
   //        {


   //            GridView_yearly_master.DataSource = dt_master6;
   //            GridView_yearly_master.DataBind();
   //            GridView_yearly_master.Visible = false;

   //        }
   //        GridView_yearly_master.Visible = true;




   //    }

   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }

   //}

    


   protected void GridView_daily_master_SelectedIndexChanged(object sender, EventArgs e)
   {

       try
       {
           gv_daily_master_data();
           d.con.Open();
           string id = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_daily = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id + "' and type ='Daily' ", d.con);
           MySqlDataAdapter dr_master1 = new MySqlDataAdapter(master_daily);
           DataTable dt_master1 = new DataTable();
           dr_master1.Fill(dt_master1);
           if (dt_master1.Rows.Count > 0)
           {
               gv_client_policy.DataSource = dt_master1;
               gv_client_policy.DataBind();
               //gv_client_policy.Visible = false;

           }
           weekly_Onselected();
           fourth_nightly_Onselected();
           monthly_Onselected();
           quarterly_Onselected();
           six_monthly_Onselected();
           yearly_Onselected();
           
           // gv_client_policy.Visible = true;
           btn_update.Visible = false;
           btn_add.Visible = false;
       }

       catch (Exception ex) { }
       finally { d.con.Close(); }

    
   }

   protected void gv_daily_master_data()
   {
       string id = GridView_daily_master.SelectedRow.Cells[0].Text;

       try
       {
           d8.con1.Open();
           MySqlCommand cmd = new MySqlCommand("select client_code,state,`branch_having_policy`,designation,`duty_hours`,`new_policy_name`,`start_date`,`end_date` from pay_client_gps_policy_master where comp_code = '" + Session["comp_code"].ToString() + "' and Id='" + id + "'", d8.con1);
           MySqlDataReader dr = cmd.ExecuteReader();
          // DataTable dt_master1 = new DataTable();
           if (dr.Read())
           {
               ddl_unit_client.SelectedValue = dr.GetValue(0).ToString();
               //ddl_clientwisestate_SelectedIndexChanged(null, null);
               ddl_clientname_SelectedIndexChanged(null, null);
               ddl_clientwisestate.SelectedValue = dr.GetValue(1).ToString();
               ddl_clientwisestate_SelectedIndexChanged(null, null);
               

               ddl_unitcode.SelectedValue = dr.GetValue(2).ToString();
               ddl_unitcode_SelectedIndexChanged(null, null);
               ddl_designation.SelectedValue = dr.GetValue(3).ToString();
               ddl_designation_SelectedIndexChanged(null, null);
               ddl_hours.Text = dr.GetValue(4).ToString();
               txt_policy_name1.Text = dr.GetValue(5).ToString();
               txt_start_date.Text = dr.GetValue(6).ToString();
               txt_end_date.Text = dr.GetValue(7).ToString();
           }
           
           cmd.Dispose();
           d8.con1.Close();

       }
       catch (Exception ex) { throw ex; }
       finally {
           d8.con1.Close();
       }
   
   
   }


   public void weekly_Onselected()
   {
       try {
           d5.con.Open();


           string id1 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_weekly = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id1 + "' and type ='Weekly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_weekly);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_weekly.DataSource = dt_master2;
               gridview_weekly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;

       
       }


       catch (Exception ex) { }
       finally { d5.con.Close(); }


   }



   public void fourth_nightly_Onselected()
   {

       try
       {
           d5.con.Open();


           string id2 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_fourth = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id2 + "' and type ='Fourth Nightly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_fourth);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_fourth_nightly.DataSource = dt_master2;
               gridview_fourth_nightly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;


       }


       catch (Exception ex) { }
       finally { d5.con.Close(); }
   
   }

   public void monthly_Onselected()
   {

       try
       {
           d5.con.Open();


           string id3 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_month = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id3 + "' and type ='Monthly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_month);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_monthly.DataSource = dt_master2;
               gridview_monthly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;


       }


       catch (Exception ex) { }
       finally { d5.con.Close(); }

   }


   public void quarterly_Onselected()
   {

       try
       {
           d5.con.Open();


           string id4 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_quarterly = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id4 + "' and type ='Quarterly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_quarterly);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_quarterly.DataSource = dt_master2;
               gridview_quarterly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;

       }
       catch (Exception ex) { }
       finally { d5.con.Close(); }

   }



   public void six_monthly_Onselected()
   {

       try
       {
           d5.con.Open();


           string id5 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_six_month = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id5 + "' and type ='Six Monthly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_six_month);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_six_monthly.DataSource = dt_master2;
               gridview_six_monthly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;

       }
       catch (Exception ex) { }
       finally { d5.con.Close(); }

   }



   public void yearly_Onselected()
   {

       try
       {
           d5.con.Open();


           string id6 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

           MySqlCommand master_yearly = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id6 + "' and type ='Yearly' ", d5.con);
           MySqlDataAdapter dr_master2 = new MySqlDataAdapter(master_yearly);
           DataTable dt_master2 = new DataTable();
           dr_master2.Fill(dt_master2);
           if (dt_master2.Rows.Count > 0)
           {
               gridview_yearly.DataSource = dt_master2;
               gridview_yearly.DataBind();
               //gv_client_policy.Visible = false;

           }
           // gv_client_policy.Visible = true;
           btn_update.Visible = true;

       }
       catch (Exception ex) { }
       finally { d5.con.Close(); }

   }




   //protected void GridView_fourth_master_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
   //{

   //    try {
   //        d.con.Open();

   //        string id2 = GridView_daily_master.SelectedRow.Cells[0].Text.ToString();

   //        MySqlCommand master_fourth = new MySqlCommand(" Select `Id`,`type`,`grade`,`description`,`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` from pay_client_gps_policy where `cgpm_id`='" + id2 + "' and type ='Fourth Nightly' ", d.con);
   //        MySqlDataAdapter dr_master3 = new MySqlDataAdapter(master_fourth);
   //        DataTable dt_master3 = new DataTable();
   //        dr_master3.Fill(dt_master3);
   //        if (dt_master3.Rows.Count > 0)
   //        {
   //            gridview_fourth_nightly.DataSource = dt_master3;
   //            gridview_fourth_nightly.DataBind();
   //            //gv_client_policy.Visible = false;

   //        }
   //        // gv_client_policy.Visible = true;
   //        btn_update.Visible = true;

       
       
   //    }


   //    catch (Exception ex) { }
   //    finally { d.con.Close(); }

   //}


   protected void GridView_daily_master_RowDataBound(object sender, GridViewRowEventArgs e)
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
           e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView_daily_master, "Select$" + e.Row.RowIndex);

       }

       e.Row.Cells[6].Visible = false;
       e.Row.Cells[2].Visible = false;
       e.Row.Cells[3].Visible = false;
   }



   protected void GridView_weekly_master_RowDataBound(object sender, GridViewRowEventArgs e)
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
           e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView_daily_master, "Select$" + e.Row.RowIndex);

       }


   }



   protected void GridView_fourth_master_RowDataBound(object sender, GridViewRowEventArgs e)
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
           e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView_daily_master, "Select$" + e.Row.RowIndex);

       }



   }


   


   //protected void gv_client_daily_SelectedIndexChanged(object sender, EventArgs e)
   //{
   //    try
   //    {
   //        d4.con.Open();

   //        MySqlCommand cmd1 = new MySqlCommand(" SELECT distinct pay_client_gps_policy_master.`client_code`,  `state`,  `branch_having_policy`,  `branch_not_having_policy`,  `designation`,  `duty_hours`,  `new_policy_name`,  `start_date`,  `end_date` 1_time,2_time,3_time,4_time,5_time,6_time,7_time,8_time,9_time,10_time,11_time,12_time FROM  `pay_client_gps_policy_master`  INNER JOIN `pay_client_gps_policy` ON `pay_client_gps_policy_master`.`comp_code` = `pay_client_gps_policy`.`comp_code` AND `pay_client_gps_policy_master`.`client_code` = `pay_client_gps_policy`.`client_code`", d4.con);
   //        MySqlDataReader gv_dr = cmd1.ExecuteReader();

   //        if (gv_dr.Read())
   //        {

   //            ddl_unit_client.SelectedValue = gv_dr.GetValue(0).ToString();

   //            ddl_clientname_SelectedIndexChanged(null, null);
   //            ddl_clientwisestate.SelectedValue = gv_dr.GetValue(1).ToString();

   //            ddl_clientwisestate_SelectedIndexChanged(null, null);
   //            ddl_unitcode.SelectedValue = gv_dr.GetValue(2).ToString();

    
   //            ddl_unitcode_without.SelectedValue = gv_dr.GetValue(3).ToString();

   //            ddl_unitcode_SelectedIndexChanged(null, null);
   //            ddl_designation.SelectedValue = gv_dr.GetValue(4).ToString();

   //            ddl_designation_SelectedIndexChanged(null, null);
   //            ddl_hours.SelectedValue = gv_dr.GetValue(5).ToString();

   //            txt_policy_name1.Text = gv_dr.GetValue(6).ToString();
   //            txt_start_date.Text = gv_dr.GetValue(7).ToString();
   //            txt_end_date.Text = gv_dr.GetValue(8).ToString();

   //            //ddl_daily_1_time.SelectedValue = 

   //        }

   //        gv_dr.Close();
   //        d4.con.Close();
   //    }
   //    catch (Exception ex)
   //    {
   //    }
   //    finally
   //    {
   //    }
   //}


   //protected void gv_client_daily_RowDataBound(object sender, GridViewRowEventArgs e)
   //{

   //    for (int i = 0; i < e.Row.Cells.Count; i++)
   //    {
   //        if (e.Row.Cells[i].Text == "&nbsp;")
   //        {
   //            e.Row.Cells[i].Text = "";
   //        }
   //    }
   //    if (e.Row.RowType == DataControlRowType.DataRow)
   //    {
   //        e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
   //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
   //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_client_daily, "Select$" + e.Row.RowIndex);

   //    }
   //}

   public void grid_daily()
   {
       gv_client_policy.DataSource = null;
       gv_client_policy.DataBind();
       try
       {
           //MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code,GRADE_DESC as grade,description,type,time  FROM `pay_working_master` inner join pay_grade_master on pay_working_master.comp_code=pay_grade_master.comp_code and pay_working_master.grade=pay_grade_master.`GRADE_CODE` where  type = 'Daily' and grade = '" + ddl_designation.SelectedValue + "'", d.con);

           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time,'0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'  FROM `pay_working_master` where  type='Daily' and grade = '" + ddl_designation.SelectedValue + "' and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           //SELECT DISTINCT`pay_working_master`.`id`,`pay_working_master`.`comp_code`,`pay_working_master`.`type`,`pay_working_master`.`grade`,`pay_working_master`.`description`,`pay_working_master`.`time`,`1_time`,`2_time`,`3_time`,`4_time`,`5_time`,`6_time`,`7_time`,`8_time`,`9_time`,`10_time`,`11_time`,`12_time` FROM `pay_working_master` left OUTER JOIN `pay_client_gps_policy_master` ON `pay_working_master`.`comp_code` = `pay_client_gps_policy_master`.`comp_code` AND `pay_working_master`.`grade` = `pay_client_gps_policy_master`.`designation` left OUTER JOIN `pay_client_gps_policy` ON `pay_working_master`.`comp_code` = `pay_client_gps_policy`.`comp_code` AND `pay_working_master`.`grade` = `pay_client_gps_policy`.`grade`
         //  MySqlCommand cmd_hd = new MySqlCommand(" SELECT DISTINCT  `pay_client_gps_policy`.`id`, pay_client_gps_policy.comp_code, pay_client_gps_policy.type, `pay_client_gps_policy`.`grade`,   `pay_client_gps_policy`.`description`,   `pay_client_gps_policy`.`time`,   `1_time`,   `2_time`,   `3_time`,   `4_time`,   `5_time`,   `6_time`,   `7_time`,   `8_time`,   `9_time`,   `10_time`,   `11_time`,   `12_time` FROM   `pay_client_gps_policy` INNER JOIN `pay_client_gps_policy_master` ON `pay_client_gps_policy`.`comp_code` = `pay_client_gps_policy_master`.`comp_code` AND `pay_client_gps_policy`.`cgpm_id` = `pay_client_gps_policy_master`.`id`     INNER JOIN `pay_working_master` ON `pay_client_gps_policy`.`comp_code` = `pay_working_master`.`comp_code` AND `pay_client_gps_policy`.`grade` = `pay_working_master`.`grade` WHERE   `pay_client_gps_policy`.`grade` = '" + ddl_designation.SelectedValue + "' AND `pay_client_gps_policy`.`type` = 'daily' AND `branch_having_policy` = '" + ddl_unitcode.SelectedItem + "'", d.con);
          // MySqlCommand cmd_hd = new MySqlCommand(" SELECT `pay_working_master`.`id`,  `pay_working_master`.`comp_code`,  `pay_working_master`.`type`,  `pay_working_master`.`grade`,  `pay_working_master`.`description`,  `pay_working_master`.`time`,   ifnull(`1_time`,'Set') as 1_time,   ifnull(`2_time`,'Set') as 2_time,   ifnull(`3_time`,'Set') as 3_time,   ifnull(`4_time`,'Set') as 4_time,   ifnull(`5_time`,'Set') as 5_time,  ifnull(`6_time`,'Set') as 6_time,   ifnull(`7_time`,'Set') as 7_time,   ifnull(`8_time`,'Set') as 8_time,   ifnull(`9_time`,'Set') as 9_time,  ifnull(`10_time`,'Set') as 10_time,   ifnull(`11_time`,'Set') as 11_time,   ifnull(`12_time`,'Set') as 12_time FROM pay_working_master left  join   `pay_client_gps_policy`on pay_working_master.grade = pay_client_gps_policy.grade and   pay_working_master.comp_code= pay_client_gps_policy.comp_code WHERE  `pay_working_master`.grade = '" + ddl_designation.SelectedValue + "' AND `pay_working_master`.`type` = 'daily'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {

               gv_client_policy.DataSource = dt;
               gv_client_policy.DataBind();
            //   gv_client_daily.Visible = false;
           }
           else
           {
               gv_client_policy.DataSource = dt;
               gv_client_policy.DataBind();
               //gv_client_daily.Visible = false;
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }



   public void grid_weekly()
   {
       gridview_weekly.DataSource = null;
       gridview_weekly.DataBind();
       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code,grade,description,type,time, '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'   FROM `pay_working_master` where  type = 'Weekly' and grade = '" + ddl_designation.SelectedValue + "' AND comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_weekly.DataSource = dt;
               gridview_weekly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }






   public void grid_fourth_nightly()
   {
       gridview_fourth_nightly.DataSource = null;
       gridview_fourth_nightly.DataBind();

       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code,grade,description,type,time ,'0'as '1_time', '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'   FROM `pay_working_master` where  type = 'Fourth Nightly' and grade = '" + ddl_designation.SelectedValue + "' and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_fourth_nightly.DataSource = dt;
               gridview_fourth_nightly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }



   public void grid_monthly()
   {
       gridview_monthly.DataSource = null;
       gridview_monthly.DataBind();

       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time ,'0'as '1_time', '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'  FROM `pay_working_master`  where  type = 'Monthly' and grade = '" + ddl_designation.SelectedValue + "'and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_monthly.DataSource = dt;
               gridview_monthly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }



   public void grid_quarterly()
   {

       gridview_quarterly.DataSource = null;
       gridview_quarterly.DataBind();

       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time,'0'as '1_time', '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'   FROM `pay_working_master`  where  type = 'Quarterly' and grade = '" + ddl_designation.SelectedValue + "'and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_quarterly.DataSource = dt;
               gridview_quarterly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }

   public void grid_six_monthly()
   {
       gridview_six_monthly.DataSource = null;
       gridview_six_monthly.DataBind();

       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time ,'0'as '1_time', '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'   FROM `pay_working_master`  where  type = 'Six Monthly' and grade = '" + ddl_designation.SelectedValue + "'and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_six_monthly.DataSource = dt;
               gridview_six_monthly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }



   public void grid_yearly()
   {
       gridview_yearly.DataSource = null;
       gridview_yearly.DataBind();

       try
       {
           MySqlCommand cmd_hd = new MySqlCommand("SELECT pay_working_master.Id,pay_working_master.comp_code, grade,description,type,time,'0'as '1_time', '0'as '1_time','0'as '2_time','0' as '3_time','0' as '4_time','0' as '5_time','0' as '6_time','0' as '7_time','0' as '8_time','0' as '9_time','0' as '10_time','0' as '11_time','0' as '12_time'   FROM `pay_working_master`  where  type = 'Yearly' and grade = '" + ddl_designation.SelectedValue + "'and comp_code='"+Session["COMP_CODE"].ToString()+"'", d.con);
           d.con.Open();
           MySqlDataAdapter dr_hd = new MySqlDataAdapter(cmd_hd);
           DataTable dt = new DataTable();
           dr_hd.Fill(dt);

           if (dt.Rows.Count > 0)
           {


               gridview_yearly.DataSource = dt;
               gridview_yearly.DataBind();
           }

       }
       catch (Exception ex) { }
       finally { d.con.Close(); }

   }


    public void duty_hourse()
   {

       try
       {


          // MySqlCommand xyz = new MySqlCommand();





           MySqlCommand cmd = new MySqlCommand("select distinct (hours) from pay_designation_count where comp_code = '" + Session["COMP_CODE"].ToString() + "' and client_code = '" + ddl_unit_client.SelectedValue + "' and state = '" + ddl_clientwisestate.SelectedItem + "'  and `DESIGNATION`= (  select  `GRADE_DESC` from pay_grade_master where grade_code = '" + ddl_designation.SelectedValue + "'  and comp_code = '" + Session["COMP_CODE"].ToString() + "' )", d.con);
           d.con.Open();
           MySqlDataAdapter dr = new MySqlDataAdapter(cmd);
           DataTable dm = new DataTable();
           dr.Fill(dm);


           if
               (dm.Rows.Count > 0)
           {

               ddl_hours.DataSource = dm;
               ddl_hours.DataTextField=dm.Columns[0].ToString();
               ddl_hours.DataBind();

           }
           d.con.Close();
          
       }


       catch (Exception Ex) {
         
       }

        finally{
           d.con.Close();
       }
       
       
       
       }
   

   protected void ddl_clientwisestate_SelectedIndexChanged(object sender, EventArgs e)
   {
       d6.con.Open();
       try
       {
           ddl_unitcode.Items.Clear();
           //MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0", d6.con);//and billing_unit_code in(select UNIT_CODE from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "' AND client_code='" + ddl_unit_client.SelectedValue + "' AND state_name='" + ddl_clientwisestate.SelectedValue + "')
           MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE ,UNIT_NAME , CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_',  UNIT_CITY , '_',  UNIT_ADD1 , '_',  UNIT_NAME ) AS 'CUNIT' FROM pay_unit_master  WHERE CLIENT_CODE  = '" + ddl_unit_client.SelectedValue + "'  AND  state_name  = '" + ddl_clientwisestate.SelectedValue + "' AND  comp_code  ='" + Session["comp_code"].ToString() + "' AND  unit_code  NOT IN (SELECT  branch_having_policy  FROM  pay_client_gps_policy_master  WHERE  comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  client_code  = '" + ddl_unit_client.SelectedValue + "') AND  branch_status  = 0", d6.con);
           MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
           DataSet ds1 = new DataSet();
           sda1.Fill(ds1);
           ddl_unitcode.DataSource = ds1.Tables[0];
           ddl_unitcode.DataValueField = "UNIT_CODE";
           ddl_unitcode.DataTextField = "CUNIT";
           ddl_unitcode.DataBind();
           //ddl_unitcode.Items.Insert(0, new ListItem("Select"));

           // ddl_Existing_policy_name.Items.Clear();
           ddl_designation.Items.Clear();
           d6.con.Close();
       }
       catch (Exception ex) { throw ex; }
       finally { d6.con.Close(); }

       d6.con.Open();
       try
       {
           ddl_unitcode_without.Items.Clear();
         //  MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE,UNIT_NAME, CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',UNIT_CITY,'_',UNIT_ADD1,'_',UNIT_NAME) AS CUNIT FROM pay_unit_master  WHERE CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "' and state_name = '" + ddl_clientwisestate.SelectedValue + "' AND comp_code='" + Session["comp_code"].ToString() + "' and unit_code not in (select billing_unit_code from pay_billing_master where comp_code = '" + Session["COMP_CODE"].ToString() + "' and billing_CLIENT_CODE = '" + ddl_unit_client.SelectedValue + "') AND branch_status = 0 ", d6.con);
           

           MySqlCommand cmd2 = new MySqlCommand("SELECT UNIT_CODE ,UNIT_NAME , CONCAT((SELECT DISTINCT ( STATE_CODE ) FROM  pay_state_master  WHERE  STATE_NAME  =  pay_unit_master . STATE_NAME ), '_', UNIT_CITY , '_', UNIT_ADD1 , '_', UNIT_NAME ) AS 'CUNIT' FROM pay_unit_master WHERE CLIENT_CODE  = '" + ddl_unit_client.SelectedValue + "' AND  state_name  = '" + ddl_clientwisestate.SelectedValue + "' AND  comp_code  = '" + Session["comp_code"].ToString() + "' AND  unit_code  IN (SELECT  branch_having_policy  FROM  pay_client_gps_policy_master  WHERE  comp_code  = '" + Session["COMP_CODE"].ToString() + "' AND  client_code  = '" + ddl_unit_client.SelectedValue + "') AND  branch_status  = 0", d6.con);
           MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd2);
           DataSet ds1 = new DataSet();
           sda1.Fill(ds1);
           ddl_unitcode_without.DataSource = ds1.Tables[0];
           ddl_unitcode_without.DataValueField = "UNIT_CODE";
           ddl_unitcode_without.DataTextField = "CUNIT";
           ddl_unitcode_without.DataBind();
           d6.con.Close();
       }
       catch (Exception ex) { throw ex; }
       finally { d6.con.Close(); }
   }


   protected void ddl_designation_SelectedIndexChanged(object sender, EventArgs e)
   {
       duty_hourse();
       grid_daily();       
       grid_weekly();
       grid_fourth_nightly();
       grid_monthly();
       grid_quarterly();
       grid_six_monthly();
       grid_yearly();
       btn_visble_true();

       
      


       gv_client_policy.Visible = true;
       gridview_weekly.Visible = true;

     //  gv_client_daily.Visible = true;
       btn_add.Visible = true;

      
       
   }



   






   protected void client_list()
   {
       d.con1.Open();
       try
       {
           MySqlDataAdapter cmd = new MySqlDataAdapter("Select CASE WHEN  client_code  = 'BALIK HK' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BALIC SG' THEN CONCAT( client_name , ' SG') WHEN  client_code  = 'BAG' THEN CONCAT( client_name , ' HK') WHEN  client_code  = 'BG' THEN CONCAT( client_name , ' SG') ELSE  client_name  END AS 'client_name', client_code from pay_client_master where comp_code='" + Session["comp_code"] + "'  ORDER BY client_code", d.con1);//AND  client_code in(select distinct(client_code) from pay_client_state_role_grade where  COMP_CODE='" + Session["COMP_CODE"].ToString() + "' AND  EMP_CODE='" + Session["LOGIN_ID"].ToString() + "')
           DataTable dt = new DataTable();
           cmd.Fill(dt);
           if (dt.Rows.Count > 0)
           {
               ddl_unit_client.DataSource = dt;
               ddl_unit_client.DataTextField = dt.Columns[0].ToString();
               ddl_unit_client.DataValueField = dt.Columns[1].ToString();
               ddl_unit_client.DataBind();

           }
       }
       catch (Exception ex) { throw ex; }
       finally
       {

           d.con1.Close();
           ddl_unit_client.Items.Insert(0, "Select");
       }




   }



   private void load_grdview()
   {
       d.con1.Open();
       try
       {
           MySqlDataAdapter MySqlDataAdapter1 = new MySqlDataAdapter("select pay_billing_master.id, policy_name1 as 'Policy',client_name as 'Client',unit_name as 'Branch',billing_state as 'State', (Select grade_desc from pay_grade_master where grade_code= pay_billing_master.Designation and comp_code='" + Session["comp_code"].ToString() + "') as Designation, cast(concat(hours,' Hrs') as char) as 'Working Hours', date_format(start_date,'%d/%m/%Y') as 'Policy Start Date', date_format(end_date,'%d/%m/%Y') as 'Policy End Date' from pay_billing_master inner join pay_client_master on pay_client_master.client_code = pay_billing_master.billing_client_code and pay_client_master.comp_code = pay_billing_master.comp_code inner join pay_unit_master on pay_unit_master.unit_code = pay_billing_master.billing_unit_code and pay_unit_master.comp_code = pay_billing_master.comp_code where billing_client_code = '" + ddl_unit_client.SelectedValue + "' and pay_billing_master.comp_code = '" + Session["COMP_CODE"].ToString() + "' and pay_unit_master.branch_status = 0", d.con1);
           DataTable DS1 = new DataTable();
           MySqlDataAdapter1.Fill(DS1);
          
           d.con1.Close();
       }
       catch (Exception ex) { throw ex; }
       finally { d.con1.Close(); }
   }


   protected void gv_client_policy_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);
           
           DataRowView dr = (DataRowView)e.Row.DataItem;
         
           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());
           for (int i = 1; i <= k; i++)
           {
               
               var i1 = "ddl_daily_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }
           for (int i = k + 1; i <= 12; i++)
           {
               var txtname = "ddl_daily_" + i + "_time";

               DropDownList ot_text = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_text.BackColor = Color.Red;
               ot_text.Enabled = false;

               // e.Row.Cells[5 + i].BackColor = Color.LimeGreen;
           }
       }
       e.Row.Cells[1].Visible = false;
      
   }

   protected void gridview_weekly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }
           
            int k = Convert.ToInt32(dr["time"].ToString());
            for (int i=1; i <= k; i++)
            {

                var i1 = "ddl_weekly_" + i + "_time";
                DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
                k1.BackColor = Color.LimeGreen;
            }
           for (int i = k+1; i <= 12; i++)
           {
               var txtname = "ddl_weekly_" + i.ToString() + "_time";
            
               DropDownList ot_text = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_text.BackColor = Color.Red;
               ot_text.Enabled = false;

              // e.Row.Cells[5 + i].BackColor = Color.LimeGreen;
           }
           
             
       }
       e.Row.Cells[1].Visible = false;


  
     
  }

 protected void gridview_fourth_nightly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());

           for (int i = 1; i <= k; i++)
           {

               var i1 = "ddl_fourth_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }

           for (int i = k + 1; i <= 12; i++)
           {
               var txtname = "ddl_fourth_" + i.ToString() + "_time";
               DropDownList ot_text = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_text.BackColor = Color.Red;
               ot_text.Enabled = false;
           }
       }
       e.Row.Cells[1].Visible = false;
 }
   protected void gridview_monthly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());

           for (int i = 1; i <= k; i++)
           {

               var i1 = "ddl_monthly_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }

           for (int i = k + 1; i <= 12; i++) {

               var txtname = "ddl_monthly_" + i.ToString() + "_time";
               DropDownList ot_txt = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_txt.BackColor = Color.Red;
               ot_txt.Enabled = false;
                  
           }
       }
       e.Row.Cells[1].Visible = false;


   }
   protected void gridview_quarterly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());

           for (int i = 1; i <= k; i++)
           {

               var i1 = "ddl_quarterly_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }



           for (int i = k + 1; i <= 12; i++ )

           {
               var txtname = "ddl_quarterly_" + i.ToString() + "_time";
               DropDownList ot_txt = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_txt.BackColor = Color.Red;
               ot_txt.Enabled = false;
           }
       }

       e.Row.Cells[1].Visible = false;
   }
   protected void gridview_six_monthly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());
           for (int i = 1; i <= k; i++)
           {

               var i1 = "ddl_six_month_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }
           for (int i = k + 1; i <= 12; i++)
           {
               var txtname = "ddl_six_month_" + i + "_time";

               DropDownList ot_text = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_text.BackColor = Color.Red;
               ot_text.Enabled = false;

               // e.Row.Cells[5 + i].BackColor = Color.LimeGreen;
           }
           
       }

       e.Row.Cells[1].Visible = false;
   }
   protected void gridview_yearly_RowDataBound(object sender, GridViewRowEventArgs e)
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
           CheckBox txtName = (e.Row.FindControl("chk_client") as CheckBox);

           DataRowView dr = (DataRowView)e.Row.DataItem;

           if (dr["1_time"].ToString() != "0" || dr["2_time"].ToString() != "0" || dr["3_time"].ToString() != "0" || dr["4_time"].ToString() != "0" || dr["5_time"].ToString() != "0" || dr["6_time"].ToString() != "0" || dr["7_time"].ToString() != "0" || dr["8_time"].ToString() != "0" || dr["9_time"].ToString() != "0" || dr["10_time"].ToString() != "0" || dr["11_time"].ToString() != "0" || dr["12_time"].ToString() != "0")
           {
               txtName.Checked = true;
           }

           int k = Convert.ToInt32(dr["time"].ToString());
           for (int i = 1; i <= k; i++)
           {
              
               var i1 = "ddl_yearly_" + i + "_time";
               DropDownList k1 = (DropDownList)e.Row.FindControl(i1) as DropDownList;
               k1.BackColor = Color.LimeGreen;
           }
           for (int i = k + 1; i <= 12; i++)
           {
               var txtname = "ddl_yearly_" + i + "_time";

               DropDownList ot_text = (DropDownList)e.Row.FindControl(txtname) as DropDownList;
               ot_text.BackColor = Color.Red;
               ot_text.Enabled = false;

               // e.Row.Cells[5 + i].BackColor = Color.LimeGreen;
           }
       }

       e.Row.Cells[1].Visible = false;

   }
   protected void gv_client_policy_PreRender(object sender, EventArgs e)
   {
       try
       {
           gv_client_policy.UseAccessibleHeader = false;
           gv_client_policy.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply catch

   }
   protected void gridview_weekly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_weekly.UseAccessibleHeader = false;
           gridview_weekly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   protected void gridview_fourth_nightly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_fourth_nightly.UseAccessibleHeader = false;
           gridview_fourth_nightly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   protected void gridview_monthly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_monthly.UseAccessibleHeader = false;
           gridview_monthly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   protected void gridview_quarterly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_quarterly.UseAccessibleHeader = false;
           gridview_quarterly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   protected void gridview_six_monthly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_six_monthly.UseAccessibleHeader = false;
           gridview_six_monthly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   protected void gridview_yearly_PreRender(object sender, EventArgs e)
   {
       try
       {
           gridview_yearly.UseAccessibleHeader = false;
           gridview_yearly.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat
   }

   protected void GridView_daily_master_PreRender(object sender, EventArgs e)
   {
       try
       {
           GridView_daily_master.UseAccessibleHeader = false;
           GridView_daily_master.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply cat

   }
   
   //protected void lnk_remove_Checklist_Click(object sender, EventArgs e)
   //{
   //   // hidtab.Value = "11";
   //   // string delete = "";
   //   // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = '" + item_code + "' ");
   //    grid_daily();
       
   // }

  
   //protected void lnk_remove_Checklist_fourth_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_fourth_nightly();

   //}
   //protected void lnk_remove_Checklist_weekly_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_weekly();

   //}
   //protected void lnk_remove_Checklist_monthly_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_monthly();
   //}
   //protected void lnk_remove_Checklist_quarterly_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_quarterly();

   //}
   //protected void lnk_remove_Checklist_six_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_six_monthly();
      

   //}
   //protected void lnk_remove_Checklist_yearly_Click(object sender, EventArgs e)
   //{
   //    // hidtab.Value = "11";
   //    // string delete = "";
   //    // GridViewRow row = (GridViewRow)(((LinkButton)sender)).NamingContainer;
   //    //string emplyee_id = row.Cells[3].Text;
   //    LinkButton lb = (LinkButton)sender;
   //    GridViewRow row = (GridViewRow)lb.NamingContainer;
   //    Label lbl_item_code = (Label)row.FindControl("id");
   //    string item_code = (lbl_item_code.Text);


   //    int result1 = 0;
   //    result1 = d.operation("delete from pay_client_gps_policy where id = " + item_code + " ");
   //    grid_yearly();

   //}

   protected void btn_daily_close_Click(object sender, EventArgs e)
   {
       Response.Redirect("Home.aspx");
   }

   protected void func_color() {
       try {
           int rowIndex1 = 0;
           TextBox box1 = (TextBox)gridview_weekly.Rows[rowIndex1].Cells[6].FindControl("lbl_time_weekly");
           string id = box1.Text;

           for (int i = 1; i <= int.Parse(id); i++)
           {
               DropDownList box = (DropDownList)gridview_weekly.Rows[rowIndex1].Cells[5].FindControl("ddl_weekly_" + i + "_time");
               string id1 = box.Text;
               

           }
       
       
       
       
       }
       catch (Exception ex) { throw ex; }
       finally { }
   
   
   
   }

   protected void GridView_daily_master_PreRender1(object sender, EventArgs e)
   {
       try
       {
           GridView_daily_master.UseAccessibleHeader = false;
           GridView_daily_master.HeaderRow.TableSection = TableRowSection.TableHeader;
       }
       catch { }//vinod dont apply catch
   }
}