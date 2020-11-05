using System;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;

public partial class location_map : System.Web.UI.Page
{
    DAL d =new DAL();
    String getaddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        getlatLon();
        if (!IsPostBack) {
            if (Session["UNIT_NO"] == null || Session["UNIT_NO"] == "")
            {
                if (getaddress != "")
                {
                    btn_close.Visible = true;
                    getaddress = "";
                }
                else {
                    btn_close.Visible = false;
                    getaddress = "";
                }
               // btn_close.Visible = false;

            }
            else
            {
                string unit_code = Session["UNIT_NO"].ToString();
                //btn_close.Visible = false;
                MySqlCommand cmd = new MySqlCommand("SELECT  UNIT_ADD2, unit_Lattitude,unit_Longtitude,unit_distance FROM pay_unit_master WHERE UNIT_CODE='" + unit_code + "' and comp_code='" + Session["comp_code"].ToString() + "'", d.con);
                //MySqlCommand cmd = new MySqlCommand("select hsn_number,sac_number,unit,sales_rate,item_description from pay_item_master where ITEM_NAME='" + txt_particular.SelectedItem.ToString() + "'", d.con);
                // d.con.Close();
                d.con.Open();
                try
                {
                    MySqlDataReader dr_item = cmd.ExecuteReader();
                    while (dr_item.Read())
                    {
                        location.Text = dr_item.GetValue(0).ToString();
                        lat.Text = dr_item.GetValue(1).ToString();
                        // txt_designation.Text = dr_item.GetValue(2).ToString();
                        lng.Text = dr_item.GetValue(2).ToString();
                        txt_area.Text = dr_item.GetValue(3).ToString();

                    }
                    dr_item.Close();
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    d.con.Close();
                    Session["UNIT_NO"] = "";
                    Session["MAP_ADDRESS"] = "";
                    btn_close.Visible = false;
                   
                }
            }
        }
    }


    public void getlatLon() {

        if (Session["MAP_ADDRESS"].ToString() !="")
        {
           location.Text= Session["MAP_ADDRESS"].ToString();
           lng.Text= Session["MAP_LONGITUDE"].ToString() ;
           lat.Text= Session["MAP_LATTITUDE"].ToString();
           txt_area.Text=Session["MAP_AREA"].ToString();
           getaddress = Session["MAP_ADDRESS"].ToString();
           Session["MAP_ADDRESS"] = "";
           Session["MAP_LONGITUDE"] = "";
           Session["MAP_LATTITUDE"] = "";
           Session["MAP_AREA"] = "";
           Session["UNIT_NO"] = "";
           btn_modal_save.Visible = false;

        }

    }



    protected void btn_modal_save_Click(object sender, EventArgs e)
    {
        Session["MAP_ADDRESS"] = location.Text;
        Session["MAP_LONGITUDE"] = lng.Text;
        Session["MAP_LATTITUDE"] = lat.Text;
        Session["MAP_AREA"] = txt_area.Text;
        //getaddress = "";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Saved Successfully!!')", true);
        //Response.Redirect("UnitMaster.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void setlatlng(string address, string lat, string lng)
    {
        // setsetting setsetting1 = new setsetting();
        //setsetting1.setlatlong(address, lat, lng);

    }


  
}
public class setsetting
{
    public void setlatlong(string address, string lat, string lng)
    {

        HttpContext.Current.Session["MAP_LONGITUDE"] = lng;
        HttpContext.Current.Session["MAP_LATTITUDE"] = lat;
        HttpContext.Current.Session["MAP_AREA"] = address;

    }
}