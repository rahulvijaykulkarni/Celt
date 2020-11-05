using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Vendor_request : System.Web.UI.Page
{
    DAL d1 = new DAL();
    DAL d = new DAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtbillstate.Items.Clear();
          
            MySqlCommand cmd_item1 = new MySqlCommand("SELECT distinct STATE_NAME FROM PAY_STATE_MASTER ORDER BY STATE_NAME", d1.con);
            d1.con.Open();
            try
            {
                MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
                while (dr_item1.Read())
                {
                    txtbillstate.Items.Add(dr_item1[0].ToString());
                   
                }
                dr_item1.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d1.con.Close();
            }

        
        }
    }

         protected void get_city_list(object sender, EventArgs e)
    {

        get_city(txtbillstate.SelectedItem.ToString());

    }
    public void get_city(string state_name) 
    {
        txtbillcity.Items.Clear();
        MySqlCommand cmd_item1 = new MySqlCommand("SELECT city from pay_state_master where state_name='" + state_name + "' order by city", d1.con);
        d1.con.Open();
        try
        {
            MySqlDataReader dr_item1 = cmd_item1.ExecuteReader();
            while (dr_item1.Read())
            {
                txtbillcity.Items.Add(dr_item1[0].ToString());
            }
            dr_item1.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            d1.con.Close();
            txtbillcity.Items.Insert(0, new ListItem("Select", ""));

        }

    }
    
}