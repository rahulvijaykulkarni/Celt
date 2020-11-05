using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Unit_grade_rate_updation : System.Web.UI.Page
{
    DAL d = new DAL();
    DAL d1 = new DAL();
    CompanyBAL cbal = new CompanyBAL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["comp_code"] == null || Session["comp_code"].ToString() == "")
        {
            Response.Redirect("Home.aspx");
        }

        if (!IsPostBack)
        {
            ddl_client.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select client_name, client_code from pay_client_master where comp_code='" + Session["comp_code"] + "' ORDER BY client_code", d.con);
            d.con.Open();
            try
            {
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_client.DataSource = dt_item;
                    ddl_client.DataTextField = dt_item.Columns[0].ToString();
                    ddl_client.DataValueField = dt_item.Columns[1].ToString();
                    ddl_client.DataBind();
                }
                dt_item.Dispose();
                hide_controls();
                d.con.Close();
                ddl_client.Items.Insert(0, "Select");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }

        }
    }

    protected void btn_process_Click(object sender, EventArgs e)
    {

        d.operation("INSERT INTO pay_unit_grade_rate_master (comp_code,client_code,UNIT_CODE, GRADE_CODE) (SELECT pay_employee_master.comp_code,pay_unit_master.client_code,pay_employee_master.UNIT_CODE,pay_employee_master.GRADE_CODE FROM pay_employee_master inner join pay_grade_master on pay_employee_master.comp_code=pay_grade_master.comp_code AND pay_employee_master.GRADE_CODE=pay_grade_master.GRADE_CODE  inner join pay_unit_master on pay_employee_master.Unit_code = pay_unit_master.unit_code WHERE concat(`pay_employee_master`.`UNIT_CODE`,'+', `pay_employee_master`.`GRADE_CODE`) NOT IN(SELECT concat(`UNIT_CODE` ,'+', `GRADE_CODE`)  FROM pay_unit_grade_rate_master WHERE comp_code='" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + ddl_unitcode.SelectedValue + "') AND pay_employee_master.comp_code='" + Session["comp_code"].ToString() + "' AND pay_employee_master.UNIT_CODE='" + ddl_unitcode.SelectedValue + "' AND LEFT_REASON='' group by grade_code)");

        d1.con1.Open();
        DataSet ds1 = new DataSet();
        MySqlDataAdapter adp1;

        adp1 = new MySqlDataAdapter("SELECT pay_unit_grade_rate_master.comp_code,pay_unit_grade_rate_master.UNIT_CODE, pay_unit_grade_rate_master.GRADE_CODE,pay_grade_master.GRADE_DESC, pay_unit_master.unit_name,pay_unit_grade_rate_master.E_HEAD01,pay_unit_grade_rate_master.E_HEAD02,pay_unit_grade_rate_master.E_HEAD03,pay_unit_grade_rate_master.E_HEAD04,pay_unit_grade_rate_master.E_HEAD05,pay_unit_grade_rate_master.E_HEAD06,pay_unit_grade_rate_master.E_HEAD07,pay_unit_grade_rate_master.E_HEAD08,pay_unit_grade_rate_master.E_HEAD09,pay_unit_grade_rate_master.E_HEAD10,pay_unit_grade_rate_master.E_HEAD11,pay_unit_grade_rate_master.E_HEAD12,pay_unit_grade_rate_master.E_HEAD13,pay_unit_grade_rate_master.E_HEAD14,pay_unit_grade_rate_master.E_HEAD15,L_HEAD01,L_HEAD02,L_HEAD03,L_HEAD04,L_HEAD05,D_HEAD01,D_HEAD02,D_HEAD03,D_HEAD04,D_HEAD05,D_HEAD06,D_HEAD07,D_HEAD08,D_HEAD09,D_HEAD10 FROM  pay_unit_grade_rate_master inner join pay_unit_master on pay_unit_grade_rate_master.comp_code=pay_unit_master.comp_code AND  pay_unit_grade_rate_master.UNIT_CODE=pay_unit_master.UNIT_CODE  inner join pay_grade_master on pay_unit_grade_rate_master.comp_code=pay_grade_master.comp_code and pay_unit_grade_rate_master.GRADE_CODE = pay_grade_master.GRADE_CODE WHERE pay_unit_grade_rate_master.comp_code ='" + Session["comp_code"].ToString() + "' AND pay_unit_grade_rate_master.UNIT_CODE ='" + ddl_unitcode.SelectedValue + "'  ORDER BY pay_unit_grade_rate_master.UNIT_CODE,pay_unit_grade_rate_master.GRADE_CODE", d1.con1);
        adp1.Fill(ds1);
        gv_fullmonthot.DataSource = ds1.Tables[0];
        gv_fullmonthot.DataBind();
        d1.con1.Close();

        if (ds1.Tables[0].Rows.Count > 0)
        {
            MySqlCommand cmd_heads1 = new MySqlCommand("SELECT ue_head1,ue_head2,ue_head3,ue_head4,ue_head5,ue_head6,ue_head7,ue_head8,ue_head9,ue_head10,ue_head11,ue_head12,ue_head13,ue_head14,ue_head15,ul_head1,ul_head2,ul_head3,ul_head4,ul_head5,ud_head1,ud_head2,ud_head3,ud_head4,ud_head5,ud_head6,ud_head7,ud_head8,ud_head9,ud_head10 from pay_unit_master Where unit_code='" + ddl_unitcode.SelectedValue + "'  and client_code='" + ddl_client.SelectedValue + "' and  comp_code ='" + Session["comp_code"].ToString() + "'", d.con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd_heads1);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                System.Web.UI.WebControls.Label lbl_EHEAD01 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD01");
                if (dr["ue_head1"].ToString() == "") { RemoveEmptyColumns(4); }
                else
                {
                    lbl_EHEAD01.Text = dr["ue_head1"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD02 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD02");
                if (dr["ue_head2"].ToString() == "") { RemoveEmptyColumns(5); }
                else
                {
                    lbl_EHEAD02.Text = dr["ue_head2"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD03 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD03");
                if (dr["ue_head3"].ToString() == "") { RemoveEmptyColumns(6); }
                else
                {
                    lbl_EHEAD03.Text = dr["ue_head3"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD04 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD04");
                if (dr["ue_head4"].ToString() == "") { RemoveEmptyColumns(7); }
                else
                {
                    lbl_EHEAD04.Text = dr["ue_head4"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD05 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD05");
                if (dr["ue_head5"].ToString() == "") { RemoveEmptyColumns(8); }
                else
                {
                    lbl_EHEAD05.Text = dr["ue_head5"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD06 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD06");
                if (dr["ue_head6"].ToString() == "") { RemoveEmptyColumns(9); }
                else
                {
                    lbl_EHEAD06.Text = dr["ue_head6"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD07 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD07");
                if (dr["ue_head7"].ToString() == "") { RemoveEmptyColumns(10); }
                else
                {
                    lbl_EHEAD07.Text = dr["ue_head7"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD08 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD08");
                if (dr["ue_head8"].ToString() == "") { RemoveEmptyColumns(11); }
                else
                {
                    lbl_EHEAD08.Text = dr["ue_head8"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD09 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD09");
                if (dr["ue_head9"].ToString() == "") { RemoveEmptyColumns(12); }
                else
                {
                    lbl_EHEAD09.Text = dr["ue_head9"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD10 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD10");
                if (dr["ue_head10"].ToString() == "") { RemoveEmptyColumns(13); }
                else
                {
                    lbl_EHEAD10.Text = dr["ue_head10"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD11 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD11");
                if (dr["ue_head11"].ToString() == "") { RemoveEmptyColumns(14); }
                else
                {
                    lbl_EHEAD11.Text = dr["ue_head11"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD12 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD12");
                if (dr["ue_head12"].ToString() == "") { RemoveEmptyColumns(15); }
                else
                {
                    lbl_EHEAD12.Text = dr["ue_head12"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD13 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD13");
                if (dr["ue_head13"].ToString() == "") { RemoveEmptyColumns(16); }
                else
                {
                    lbl_EHEAD13.Text = dr["ue_head13"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD14 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD14");
                if (dr["ue_head14"].ToString() == "") { RemoveEmptyColumns(17); }
                else
                {
                    lbl_EHEAD14.Text = dr["ue_head14"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_EHEAD15 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_EHEAD15");
                if (dr["ue_head15"].ToString() == "") { RemoveEmptyColumns(18); }
                else
                {
                    lbl_EHEAD15.Text = dr["ue_head15"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_LHEAD01 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_LHEAD01");
                if (dr["ul_head1"].ToString() == "") { RemoveEmptyColumns(19); }
                else
                {
                    lbl_LHEAD01.Text = dr["ul_head1"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_LHEAD02 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_LHEAD02");
                if (dr["ul_head2"].ToString() == "") { RemoveEmptyColumns(20); }
                else
                {
                    lbl_LHEAD02.Text = dr["ul_head2"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_LHEAD03 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_LHEAD03");
                if (dr["ul_head3"].ToString() == "") { RemoveEmptyColumns(21); }
                else
                {
                    lbl_LHEAD03.Text = dr["ul_head3"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_LHEAD04 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_LHEAD04");
                if (dr["ul_head4"].ToString() == "") { RemoveEmptyColumns(22); }
                else
                {
                    lbl_LHEAD04.Text = dr["ul_head4"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_LHEAD05 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_LHEAD05");
                if (dr["ul_head5"].ToString() == "") { RemoveEmptyColumns(23); }
                else
                {
                    lbl_LHEAD05.Text = dr["ul_head5"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD01 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD01");
                if (dr["ud_head1"].ToString() == "") { RemoveEmptyColumns(24); }
                else
                {
                    lbl_DHEAD01.Text = dr["ud_head1"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD02 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD02");
                if (dr["ud_head2"].ToString() == "") { RemoveEmptyColumns(25); }
                else
                {
                    lbl_DHEAD02.Text = dr["ud_head2"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD03 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD03");
                if (dr["ud_head3"].ToString() == "") { RemoveEmptyColumns(26); }
                else
                {
                    lbl_DHEAD03.Text = dr["ud_head3"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD04 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD04");
                if (dr["ud_head4"].ToString() == "") { RemoveEmptyColumns(27); }
                else
                {
                    lbl_DHEAD04.Text = dr["ud_head4"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD05 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD05");
                if (dr["ud_head5"].ToString() == "") { RemoveEmptyColumns(28); }
                else
                {
                    lbl_DHEAD05.Text = dr["ud_head5"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD06 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD06");
                if (dr["ud_head6"].ToString() == "") { RemoveEmptyColumns(29); }
                else
                {
                    lbl_DHEAD06.Text = dr["ud_head6"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD07 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD07");
                if (dr["ud_head7"].ToString() == "") { RemoveEmptyColumns(30); }
                else
                {
                    lbl_DHEAD07.Text = dr["ud_head7"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD08 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD08");
                if (dr["ud_head8"].ToString() == "") { RemoveEmptyColumns(31); }
                else
                {
                    lbl_DHEAD08.Text = dr["ud_head8"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD09 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD09");
                if (dr["ud_head9"].ToString() == "") { RemoveEmptyColumns(32); }
                else
                {
                    lbl_DHEAD09.Text = dr["ud_head9"].ToString();
                }
                System.Web.UI.WebControls.Label lbl_DHEAD10 = (System.Web.UI.WebControls.Label)gv_fullmonthot.HeaderRow.FindControl("lbl_DHEAD10");
                if (dr["ud_head10"].ToString() == "") { RemoveEmptyColumns(33); }
                else
                {
                    lbl_DHEAD10.Text = dr["ud_head10"].ToString();
                }
            }

            btn_save.Visible = true;
        }

    }
    protected void bntclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try {
           


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Unit Grade Rate Saved successfully!');", true);
        }
        catch { }
        finally
        {
            ddl_client.SelectedValue = "Select";
            ddl_unitcode.SelectedValue = "Select";
            txt_state.Text = "";
            txt_city.Text = "";
            txt_location.Text = "";

        }

       
        int TotalRows = gv_fullmonthot.Rows.Count;

        for (int i = 0; i < TotalRows; i++)
        {
            System.Web.UI.WebControls.Label lbl_unitcode = (System.Web.UI.WebControls.Label)gv_fullmonthot.Rows[i].Cells[1].FindControl("lblunitcode");
            System.Web.UI.WebControls.Label lbl_gradecode = (System.Web.UI.WebControls.Label)gv_fullmonthot.Rows[i].Cells[2].FindControl("lblgradecode");
            System.Web.UI.WebControls.Label lblgradename = (System.Web.UI.WebControls.Label)gv_fullmonthot.Rows[i].Cells[3].FindControl("lblgradename");

            System.Web.UI.WebControls.TextBox txt_day1 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[4].FindControl("txtday01");
            System.Web.UI.WebControls.TextBox txt_day2 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[5].FindControl("txtday02");
            System.Web.UI.WebControls.TextBox txt_day3 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[6].FindControl("txtday03");
            System.Web.UI.WebControls.TextBox txt_day4 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[7].FindControl("txtday04");
            System.Web.UI.WebControls.TextBox txt_day5 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[8].FindControl("txtday05");
            System.Web.UI.WebControls.TextBox txt_day6 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[9].FindControl("txtday06");
            System.Web.UI.WebControls.TextBox txt_day7 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[10].FindControl("txtday07");
            System.Web.UI.WebControls.TextBox txt_day8 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[11].FindControl("txtday08");
            System.Web.UI.WebControls.TextBox txt_day9 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[12].FindControl("txtday09");
            System.Web.UI.WebControls.TextBox txt_day10 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[13].FindControl("txtday10");
            System.Web.UI.WebControls.TextBox txt_day11 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[14].FindControl("txtday11");
            System.Web.UI.WebControls.TextBox txt_day12 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[15].FindControl("txtday12");
            System.Web.UI.WebControls.TextBox txt_day13 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[16].FindControl("txtday13");
            System.Web.UI.WebControls.TextBox txt_day14 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[17].FindControl("txtday14");
            System.Web.UI.WebControls.TextBox txt_day15 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[18].FindControl("txtday15");

            System.Web.UI.WebControls.TextBox txt_day16 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[19].FindControl("txtday16");
            System.Web.UI.WebControls.TextBox txt_day17 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[20].FindControl("txtday17");

            System.Web.UI.WebControls.TextBox txt_day18 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[21].FindControl("txtday18");
            System.Web.UI.WebControls.TextBox txt_day19 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[22].FindControl("txtday19");
            System.Web.UI.WebControls.TextBox txt_day20 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[23].FindControl("txtday20");
            System.Web.UI.WebControls.TextBox txt_day21 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[24].FindControl("txtday21");

            System.Web.UI.WebControls.TextBox txt_day22 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[25].FindControl("txtday22");
            System.Web.UI.WebControls.TextBox txt_day23 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[26].FindControl("txtday23");
            System.Web.UI.WebControls.TextBox txt_day24 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[27].FindControl("txtday24");

            System.Web.UI.WebControls.TextBox txt_day25 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[28].FindControl("txtday25");
            System.Web.UI.WebControls.TextBox txt_day26 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[29].FindControl("txtday26");
            System.Web.UI.WebControls.TextBox txt_day27 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[30].FindControl("txtday27");

            System.Web.UI.WebControls.TextBox txt_day28 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[31].FindControl("txtday28");
            System.Web.UI.WebControls.TextBox txt_day29 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[32].FindControl("txtday29");
            System.Web.UI.WebControls.TextBox txt_day30 = (System.Web.UI.WebControls.TextBox)gv_fullmonthot.Rows[i].Cells[33].FindControl("txtday30");

            d.operation("UPDATE pay_unit_grade_rate_master SET E_HEAD01 ='" + txt_day1.Text + "', E_HEAD02 ='" + txt_day2.Text + "', E_HEAD03 ='" + txt_day3.Text + "', E_HEAD04 ='" + txt_day4.Text + "', E_HEAD05 ='" + txt_day5.Text + "', E_HEAD06 ='" + txt_day6.Text + "', E_HEAD07 ='" + txt_day7.Text + "', E_HEAD08 ='" + txt_day8.Text + "', E_HEAD09 ='" + txt_day9.Text + "', E_HEAD10 ='" + txt_day10.Text + "', E_HEAD11 ='" + txt_day11.Text + "', E_HEAD12 ='" + txt_day12.Text + "', E_HEAD13 ='" + txt_day13.Text + "', E_HEAD14 ='" + txt_day14.Text + "', E_HEAD15 ='" + txt_day15.Text + "', L_HEAD01 ='" + txt_day16.Text + "',L_HEAD02 ='" + txt_day17.Text + "',L_HEAD03 ='" + txt_day18.Text + "',L_HEAD04 ='" + txt_day19.Text + "',L_HEAD05 ='" + txt_day20.Text + "',D_HEAD01 ='" + txt_day21.Text + "',D_HEAD02 ='" + txt_day22.Text + "',D_HEAD03 ='" + txt_day23.Text + "',D_HEAD04 ='" + txt_day24.Text + "',D_HEAD05 ='" + txt_day25.Text + "',D_HEAD06 ='" + txt_day26.Text + "',D_HEAD07 ='" + txt_day27.Text + "',D_HEAD08 ='" + txt_day28.Text + "',D_HEAD09 ='" + txt_day29.Text + "',D_HEAD10 ='" + txt_day30.Text + "' WHERE comp_code = '" + Session["comp_code"].ToString() + "' AND UNIT_CODE='" + lbl_unitcode.Text + "' AND GRADE_CODE='" + lbl_gradecode.Text + "'");

        }
       
    }

    
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }


     protected void gv_fullmonthot_PreRender(object sender, EventArgs e)
    {
        try
        {
            gv_fullmonthot.UseAccessibleHeader = false;
            gv_fullmonthot.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch { }//vinod dont apply catch
    }

    protected void ddl_client_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_client.SelectedValue != "Select")
        {
            ddl_unitcode.Items.Clear();
            System.Data.DataTable dt_item = new System.Data.DataTable();
            MySqlDataAdapter cmd_item = new MySqlDataAdapter("Select CONCAT( (SELECT DISTINCT(STATE_CODE) FROM pay_state_master WHERE STATE_NAME = pay_unit_master.STATE_NAME),'_',`UNIT_NAME`,'_',UNIT_ADD1) as UNIT_NAME, unit_code from pay_unit_master where comp_code='" + Session["comp_code"] + "' and client_code = '" + ddl_client.SelectedValue + "' ORDER BY UNIT_CODE", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                cmd_item.Fill(dt_item);
                if (dt_item.Rows.Count > 0)
                {
                    ddl_unitcode.DataSource = dt_item;
                    ddl_unitcode.DataTextField = dt_item.Columns[0].ToString();
                    ddl_unitcode.DataValueField = dt_item.Columns[1].ToString();
                    ddl_unitcode.DataBind();
                }
                dt_item.Dispose();
                d.con.Close();
                ddl_unitcode.Items.Insert(0, "Select");
                show_controls();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {
            ddl_unitcode.Items.Clear();
            hide_controls();
        }
    }

    private void show_controls()
    {
        unit_panel.Visible = true;
        //  btn_Export.Visible = true;
        //  btn_process.Visible = true;
        btn_save.Visible = true;
        //  FileUpload1.Visible = true;
        //btn_upload.Visible = true;
    }

    private void hide_controls()
    {
        unit_panel.Visible = false;
        //  btn_Export.Visible = false;
        //  btn_process.Visible = false;
        btn_save.Visible = false;
        // FileUpload1.Visible = false;
        //btn_upload.Visible = false;
    }

    protected void ddl_unitcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_unitcode.SelectedValue != "ALL")
        {
            MySqlCommand cmd_unitcode = new MySqlCommand("select state_name, unit_city, unit_add1 from pay_unit_master where unit_code = '" + ddl_unitcode.SelectedValue + "'and comp_code='"+Session["comp_code"]+"'", d.con);
            d.con.Open();
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CallMyFunction", "unblock()", true);
                MySqlDataReader dr_unitcode = cmd_unitcode.ExecuteReader();
                while (dr_unitcode.Read())
                {
                    txt_state.Text = dr_unitcode.GetValue(0).ToString();
                    txt_city.Text = dr_unitcode.GetValue(1).ToString();
                    txt_location.Text = dr_unitcode.GetValue(2).ToString();

                }
                dr_unitcode.Close();
                d.con.Close();
                btn_process_Click(null, null);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                d.con.Close();
            }
        }
        else
        {

            btn_process_Click(null, null);
            txt_state.Text = "";
            txt_city.Text = "";
            txt_location.Text = "";

        }
    }
    protected void gv_fullmonthot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            if (e.Row.Cells[i].Text == "&nbsp;")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        e.Row.Cells[6].Visible = false;

    }

    public void RemoveEmptyColumns(int i)
    {

        // Hide the target header cell
        gv_fullmonthot.HeaderRow.Cells[i].Visible = false;

        // Hide the target cell of each row
        foreach (GridViewRow row in gv_fullmonthot.Rows)
            row.Cells[i].Visible = false;

    }

}


