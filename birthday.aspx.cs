
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;



public partial class birthday : System.Web.UI.Page
{

    DAL d1 = new DAL();
    public MySqlDataReader drmax = null;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Login_Page.aspx");
        }
        if (d1.getaccess(Session["ROLE"].ToString(), "Birthdays", Session["COMP_CODE"].ToString()) == "I")
        {
            Response.Redirect("unauthorised_access.aspx");
        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Birthdays", Session["COMP_CODE"].ToString()) == "R")
        {

        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Birthdays", Session["COMP_CODE"].ToString()) == "U")
        {

        }
        else if (d1.getaccess(Session["ROLE"].ToString(), "Birthdays", Session["COMP_CODE"].ToString()) == "C")
        {

        }





        Panel1.Visible = true;
        d1.con1.Open();
        MySqlCommand cmd11 = new MySqlCommand("SELECT ifnull(concat('~/EMP_Images/',emp_photo),'~/Images/placeholder.PNG') as emp_photo,  CONCAT((SELECT CASE pay_employee_master.`Employee_type` WHEN 'Reliever' THEN CONCAT(pay_employee_master.`emp_name`, '-', 'Reliever') ELSE pay_employee_master.`emp_name` END), ' (', `pay_grade_master`.`Grade_desc`, ')') AS 'Name', concat(date_format(pay_employee_master.birth_date,'%d'),' ',MONTHNAME(birth_date)) as birth_date, pay_employee_master.EMP_CODE  FROM pay_employee_master  left join pay_images_master on pay_images_master.EMP_CODE = pay_employee_master.EMP_CODE inner join pay_grade_master on pay_grade_master.Grade_code = pay_employee_master.Grade_code AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE`  where date_format(BIRTH_DATE,'%d/%m') = date_format(now(),'%d/%m')", d1.con1);
        DataSet ds11 = new DataSet();
        MySqlDataAdapter adp11 = new MySqlDataAdapter(cmd11);
        adp11.Fill(ds11);
        DataList1.DataSource = ds11.Tables[0];
        DataList1.DataBind();
        d1.con1.Close();
        cmd11.Dispose();
        ds11.Dispose();
        adp11.Dispose();


        Panel3.Visible = true;
        d1.con1.Open();
        MySqlCommand cmd1 = new MySqlCommand("select concat('~/EMP_Images/',emp_photo) as emp_photo, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', pay_grade_master.Grade_desc, concat(date_format(pay_employee_master.birth_date,'%d'),' ',MONTHNAME(birth_date)) as birth_date from pay_employee_master left join pay_images_master on pay_images_master.EMP_CODE = pay_employee_master.EMP_CODE inner join pay_grade_master on pay_grade_master.Grade_code = pay_employee_master.Grade_code  AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE` where str_to_date(concat(date_format(birth_date,'%d/%m'),'/',cast(YEAR(now()) as char)),'%d/%m/%Y') BETWEEN NOW() AND NOW() + INTERVAL 8 DAY order by birth_date", d1.con1);
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1 = new MySqlDataAdapter(cmd1);
        adp1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
        d1.con1.Close();
        cmd1.Dispose();
        ds1.Dispose();
        adp1.Dispose();


        Panel4.Visible = true;
        d1.con1.Open();
        MySqlCommand cmd5 = new MySqlCommand("select concat('~/EMP_Images/',emp_photo) as emp_photo, (SELECT CASE `pay_employee_master`.`Employee_type` WHEN 'Reliever' THEN CONCAT(`pay_employee_master`.`emp_name`, '-', 'Reliever') ELSE `pay_employee_master`.`emp_name` END) AS 'EMP_NAME', pay_grade_master.Grade_desc, concat(date_format(pay_employee_master.birth_date,'%d'),' ',MONTHNAME(birth_date)) as birth_date from pay_employee_master left join pay_images_master on pay_images_master.EMP_CODE = pay_employee_master.EMP_CODE inner join pay_grade_master on pay_grade_master.Grade_code = pay_employee_master.Grade_code AND `pay_grade_master`.`COMP_CODE` = `pay_employee_master`.`COMP_CODE`  where str_to_date(concat(date_format(birth_date,'%d/%m'),'/',cast(YEAR(now()) as char)),'%d/%m/%Y') BETWEEN NOW()- INTERVAL 8 DAY AND NOW() - INTERVAL 1 DAY order by birth_date desc", d1.con1);
        
        DataSet ds2 = new DataSet();
        MySqlDataAdapter adp2 = new MySqlDataAdapter(cmd5);
        adp2.Fill(ds2);
        GridView2.DataSource = ds2.Tables[0];
        GridView2.DataBind();
        d1.con1.Close();
        cmd5.Dispose();
        ds2.Dispose();
        adp2.Dispose();
    }



    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }


    

    protected void send_notification(object sender, EventArgs e)
    {
        try
        {

            string enter_message1 = "";
            string brth_emp_code = "";

            foreach (DataListItem item in DataList1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
                    DataListItem item1 = (DataListItem)btn.NamingContainer;

                    System.Web.UI.WebControls.TextBox qtytxtbox = (System.Web.UI.WebControls.TextBox)item1.FindControl("txt_enter_message");
                    enter_message1 = qtytxtbox.Text;
                    System.Web.UI.WebControls.Label lbl_emp_code = (System.Web.UI.WebControls.Label)item1.FindControl("lbl_emp_code");
                    brth_emp_code = lbl_emp_code.Text;
                }
            }

            if (enter_message1 != "")
            {
                d1.operation("Insert into pay_notification_master (not_read,EMP_CODE,notification) values ('0','" + brth_emp_code + "','Message from " + Session["USERNAME"].ToString() + " - " + enter_message1 + "')");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Notification Send successfully!!');", true);
            }
            else {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Messsage!!');", true);
            }
           
        }
        catch (Exception ex)
        {

        }
        finally
        {


        }
    }

    //protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    if (e.Item.DataItem == DataControlRowState.Selected)

    //    if (e.Row.RowType == DataControlRowTypeDataRow)
    //    {
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            if (e.Row.Cells[i].Text == "&nbsp;")
    //            {
    //                e.Row.Cells[i].Text = "";
    //            }
    //        }
    //    }
    //}


    
}
    

 
