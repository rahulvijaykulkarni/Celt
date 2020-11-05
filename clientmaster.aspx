<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="clientmaster.aspx.cs" Inherits="clientmaster" Title="Client Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Client Master</title>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Client Master</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
             <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Client Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <asp:Panel ID="reporting_panel" runat="server">
                <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" CssClass="grid-view" class="panel-body">
                    <asp:GridView ID="GridView1" class="table" runat="server" DataKeyNames="id" ForeColor="#333333" Font-Size="X-Small" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnPreRender="GridView1_files_PreRender">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" ShowEditButton="true" />
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />
            <div class="container-fluid">
                <br />
                <asp:Panel ID="reason_panel" runat="server" Visible="false">
                    <div class="panel panel-primary" style="background-color: #f3f1fe; border-radius: 10px; border: 1px solid white">
                        <br />
                        <div class="panel-heading">
                            <div style="color: white; font-weight: bold; font-size: 15px;">Reason for Updation</div>
                        </div>
                        <div class="panel-body container">
                            <div class="row">
                                <asp:ListBox ID="lbx_reason_updation" ReadOnly="true" CssClass="form-control" runat="server" Rows="9" Width="100%"></asp:ListBox>
                                <asp:TextBox ID="txt_reason_updation" runat="server" TextMode="MultiLine" Columns="50" Rows="4" CssClass="form-control" placeholder="Enter Reason for Updation" onKeyPress="return AllowAlphabet_Number1(event)"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

             <div class="col-sm-2 col-xs-12" style="font-size: 12px; margin-left:1100px; margin-top: 1em;">
                
                            <asp:Panel ID="Panel25" runat="server">
                                <a data-toggle="modal" href="#attendance" style="color: red"><li><u><b>Client Start/End Date</b></u></li></a>
                            </asp:Panel>
                        </div>
            <div class="panel-body" style= "border-bottom-left-radius:2px; border-bottom-right-radius:2px">
                <div class="row text-center">
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="l1"
                        CancelControlID="Button2" BackgroundCssClass="Background">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">

                        <iframe style="width: 800px; height: 450px; background-color: #fff;" id="irm1" src="query.aspx" runat="server"></iframe>
                        <div class="row text-center" style="width: 100%;">
                            <asp:Button ID="Button2" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                        </div>
                    </asp:Panel>
                </div>
                </br>
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <b>Client Code :</b>
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:TextBox ID="txt_clientcode" runat="server" MaxLength="10" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Name :</b>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                            <asp:TextBox ID="txt_clientname" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Head Office Address : <span class="text-red">*</span></b>
                            <asp:TextBox ID="txt_client_address1" runat="server" onkeypress="return AllowAlphabet_Number(event)" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                         <b>   Address2 :</b>
                                    <asp:TextBox ID="txt_client_address2" runat="server" onkeypress="return AllowAlphabet_Number(event)"
                                        class="form-control"></asp:TextBox>
                        </div>
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  State :</b>
                                        <span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_client_state" runat="server" DataTextField="STATE_NAME"
                                        DataValueField="STATE_CODE" OnSelectedIndexChanged="ddl_client_state_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                 <%--<div class="col-sm-2 col-xs-12" style="font-size: 12px; margin-top: 1em;">
                            <asp:Panel ID="client_strt_end_date" runat="server">
                                <a data-toggle="modal" href="#attendance" style="color: red"><li><u><b>Client Start/End Date</b></u></li></a>
                            </asp:Panel>
                        </div>--%>

                                <div class="col-sm-2 col-xs-12">
                                    <b>City :</b>
                                         <span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_client_city" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)">
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> File No : <span class="text-red">*</span></b>
                            <asp:TextBox ID="txt_file_no" runat="server" class="form-control text_box" MaxLength="30" onkeypress="return allowAlphaNumericSpace(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Total No Of Employee : <span class="text-red">*</span></b>
                            <asp:TextBox ID="txt_employee_total" runat="server" class="form-control text_box" MaxLength="30" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Registration No :</b>
                        <asp:TextBox ID="txt_reg_no" runat="server" class="form-control text_box" MaxLength="30" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                          <b>  PAN No :</b>
                        <asp:TextBox ID="txt_pan_no" runat="server" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Licence No :  </b>  
                        <asp:TextBox ID="txt_license" runat="server" class="form-control text_box" MaxLength="30" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Bank Account No :</b>
                        <asp:TextBox ID="txt_bank_detail" runat="server" class="form-control text_box" MaxLength="20" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                          <b>  Website :</b>
                        <asp:TextBox ID="txt_website" runat="server" class="form-control text_box" MaxLength="50" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Email Id :</b>
                        <asp:TextBox ID="txt_clientemailid" runat="server" class="form-control text_box" MaxLength="50" onKeyPress="return email(event)"></asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Password :</b>
                        <asp:TextBox ID="txt_password" runat="server" class="form-control text_box" MaxLength="50"></asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> In Time/Out Time Applicable :</b>
                                      
                                <asp:DropDownList ID="ddl_iot_applicable" runat="server" class="form-control">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Desk Phone No :</b>
                        <asp:TextBox ID="txt_phoneno" runat="server" class="form-control text_box" MaxLength="10" onkeypress="return isNumber(event)"></asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12" style="margin-top: 1em">
                            <br />
                            <asp:LinkButton ID="l1" runat="server" Text="OPEN DESIGNATION"></asp:LinkButton>
                        </div>
                        <asp:TextBox ID="txt_start_date" Visible="false" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                        <asp:TextBox ID="txt_end_date" runat="server" Visible="false" class="form-control date-picker2" placeholder="End Date :"></asp:TextBox>
                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txt_branch_count" Style="display: none" runat="server" class="form-control text_box" onkeypress="return isNumber(event)"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Penalty :</b>
                        <asp:TextBox ID="txt_penalty" runat="server" class="form-control text_box" onKeyPress="return isNumber(event)">0</asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> GST Applicable Or Not :</b>
                                      
                                <asp:DropDownList ID="ddl_gst_applicable" runat="server" class="form-control">
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                    <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12" runat="server" id="bill">
                           <b> Billing Amount :</b>
                        <asp:TextBox ID="txt_bill_amount" runat="server" class="form-control text_box" onKeyPress="return isNumber(event)">0</asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                          <b>  Material Calculation :</b>
                                      
                                <asp:DropDownList ID="ddl_material_calc" runat="server" class="form-control" onchange="material();">
                                    <asp:ListItem Value="0">Working Days</asp:ListItem>
                                    <asp:ListItem Value="1">Days</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12" runat="server" id="material">
                            <b>No.Of Days :</b>
                        <asp:TextBox ID="txt_material_days" runat="server" class="form-control text_box" onKeyPress="return isNumber(event)">0</asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b> Machine Rental Policy :</b>
                                <asp:DropDownList ID="ddl_machine_rent_p" runat="server" class="form-control">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                       <%-- <div class="col-sm-2 col-xs-12" style="font-size: 11px; margin-top: 2em;">
                            <asp:Panel ID="client_strt_end_date" runat="server">
                                <a data-toggle="modal" href="#attendance" style="color: red">Client Start/End Date</a>--%>
                            <%--</asp:Panel>--%>
                        <%--</div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12" runat="server">
                           <b> Budget Materials :</b>
                        <asp:TextBox ID="txt_budget_material" runat="server" class="form-control" onKeyPress="return isNumber(event)" MaxLength="6">0</asp:TextBox>

                        </div>

                        <%--komal 23-04-2020 company bank details--%>

                     <%--   <div class="col-sm-2 col-xs-12">
                            Payment Against Bank :
                                      
                                <asp:DropDownList ID="ddl_company_bank" runat="server" class="form-control" OnSelectedIndexChanged="ddl_company_bank_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12" runat="server" id="Div1">
                           <b> Company A/C No:</b>
                        <asp:TextBox ID="txt_comp_ac_no" runat="server" class="form-control text_box" ReadOnly="true"></asp:TextBox>

                        </div>--%>

                        <%--komal 23-04-2020 company bank details end--%>

                        <div class="col-sm-2 col-xs-12">
                           <b> TDS Applicable : </b>
                     <asp:DropDownList ID="ddl_tds_applicable" runat="server" CssClass="form-control text_box" onchange="tds_validation();">
                         <asp:ListItem Value="0">NO</asp:ListItem>
                         <asp:ListItem Value="1">YES</asp:ListItem>

                     </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12 "  >
                           <b> TDS:</b>
                        <asp:DropDownList ID="ddl_tds_persent" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                             <asp:ListItem Value="0.75">TDS 0.75%</asp:ListItem>
                            <asp:ListItem Value="1.5">TDS 1.5%</asp:ListItem>
                            <asp:ListItem Value="1">TDS 1%</asp:ListItem>
                            <asp:ListItem Value="2">TDS 2%</asp:ListItem>
                        </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <b>TDS ON:</b>
                                <asp:DropDownList ID="ddl_tds_on" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Billing Amount</asp:ListItem>
                                    <asp:ListItem Value="2">Taxable Amount</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                     <div class="row">
 					 <div class="col-sm-2 col-xs-12">
                           <b> Client Active/Close:</b>
                                 <asp:DropDownList ID="ddl_client_ac" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Close</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div id="Div2" class="col-sm-2 col-xs-12" runat="server">
                          <b> Android Attendances :</b>
                       <asp:DropDownList ID="ddl_android_attendances_flag" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="select">Select</asp:ListItem>
                                    <asp:ListItem Value="yes">Yes</asp:ListItem>
                                    <asp:ListItem Value="no">No</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                        <div id="Div3" class="col-sm-2 col-xs-12" runat="server">
                            <b>GST To Be Payed :</b>
                            <asp:DropDownList ID="ddl_gst_payed" runat="server" CssClass="form-control">
                                <asp:ListItem Value="SEL">Select</asp:ListItem>
                                <asp:ListItem Value="R">Regular</asp:ListItem>
                                <asp:ListItem Value="SEWP">SEZ supplies with payment</asp:ListItem>
                                <asp:ListItem Value="SEWOP">SEZ supplies without payment</asp:ListItem>
                                <asp:ListItem Value="DE">Deemed Exp</asp:ListItem>
                                <asp:ListItem Value="SCU">Supplies covered under section 7 of IGST Act</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                           <div class="col-lg-2  col-sm-2 col-xs-12">
                                  <b> Employee Payment Bank :</b>
                               <span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_bank" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div id="Div5" class="col-sm-2 col-xs-12" runat="server">
                            <b>Billing Type:</b>
                            <span class="text-red">*</span>
                            <asp:DropDownList ID="ddl_ot_billing" runat="server" CssClass="form-control">
                                <asp:ListItem Value="With OT">With OT</asp:ListItem>
                                <asp:ListItem Value="Without OT">Without OT</asp:ListItem>

                            </asp:DropDownList>
                                </div>
								 <br />
                     <div class="row">
                          <div class="col-sm-2 col-xs-12">
                          <b> R&M Service :</b>
                       <asp:DropDownList ID="ddl_service" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="select">Select</asp:ListItem>
                                    <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                          <b> Administrative Expenses :</b>
                       <asp:DropDownList ID="ddl_admin_expence" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="select">Select</asp:ListItem>
                                    <asp:ListItem Value="0">Not Applicable</asp:ListItem>
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                         </div>
                    <br />
                    <br />
                    <div class="modal fade" id="attendance" role="dialog" data-dismiss="modal">

                        <div class="modal-dialog">
                            <div class="modal-content" style="width: 627px;">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4></h4>
                                </div>

                                <div class="modal-body">
                                    <asp:Panel runat="server" CssClass="grid-view" Width="100%">
                                        <asp:GridView ID="gv_start_end_date_details" class="table" runat="server" ForeColor="#333333" Font-Size="small" OnPreRender="gv_start_end_date_details_PreRender" Width="100%">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" CssClass="text-uppercase" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                                <div class="modal-footer">
                                    <div class="row text-center">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">CLOSE</button>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <br />
                </div>
                <br />
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                    <br />
                    <div id="tabs" style="background:white; width: 100%">
                        <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                        <ul>

                            <li><a href="#item3"><b>Documents</b></a></li>
                            <li><a href="#item8"><b>Services Category</b></a></li>
                            <li><a href="#item1"><b>Designation</b></a></li>
                            <li><a href="#item11"><b>Bank Details</b></a></li>
                            <li><a href="#item4"><b>Head Info</b></a></li>
                            <li><a href="#item5"><b>Zone Head Info</b></a></li>
                            <li><a href="#item6"><b>Region Head Info</b></a></li>
                            <li><a href="#item7"><b>GST Info</b></a></li>
                            <li style="display: none;"><a href="#item10"><b>Add Items</b></a></li>
                            <li style="display: none;"><a href="#item9"><b>Send Email</b></a></li>
                            <li><a href="#item12"><b>Company</b></a></li>
                            <li><a href="#item13"><b>Bill Check List</b></a></li>
                            <li><a href="#item14"><b>Billing/Policy</b></a></li>
                            <li><a href="#item15"><b>ESIC</b></a></li>
                            <li><a href="#item16"><b>Deduction</b></a></li>
                            <li><a href="#item17"><b>EMAIL</b></a></li>
                             <li><a href="#item18"><b>Uniform Advance Deduction</b></a></li>
                            <li><a href="#item19"><b>Company Bank Details</b></a></li>
                            <li><a href="#item20"><b>Fire Extinguisher</b></a></li>
                        </ul>
                        <div id="item14">
                            <div class="row">

                                <div class="col-sm-2 col-xs-12">
                                   <b> OT Applicable:</b>
                                          <asp:DropDownList ID="ddl_ot_applicable" runat="server" class="form-control">
                                              <asp:ListItem Value="0">Select</asp:ListItem>
                                              <asp:ListItem Value="1">Fix_OT</asp:ListItem>
                                              <asp:ListItem Value="2">Daily</asp:ListItem>
                                              <asp:ListItem Value="3">Lamsump</asp:ListItem>
                                          </asp:DropDownList>
                                </div>
                                <%--komal 20-06-19--%>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  Billing Start Day:</b> <span class="text-red">*</span>
                                    <asp:DropDownList ID="txt_start_date_client" runat="server" CssClass="form-control text_box" Width="100%" onchange="return billing_start_date();">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                        <asp:ListItem Value="17">17</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                        <asp:ListItem Value="19">19</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="21">21</asp:ListItem>
                                        <asp:ListItem Value="22">22</asp:ListItem>
                                        <asp:ListItem Value="23">23</asp:ListItem>
                                        <asp:ListItem Value="24">24</asp:ListItem>
                                        <asp:ListItem Value="25">25</asp:ListItem>
                                        <asp:ListItem Value="26">26</asp:ListItem>
                                        <asp:ListItem Value="27">27</asp:ListItem>
                                        <asp:ListItem Value="28">28</asp:ListItem>
                                        <asp:ListItem Value="29">29</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="31">31</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--komal 20-06-19--%>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Billing End Day:</b> <span class="text-red">*</span>
                                    <asp:TextBox ID="txt_end_date_client" runat="server" placeholder="End Date" class="form-control text_box " Width="100%"></asp:TextBox>
                                </div>


                            </div>
                        </div>
                        <%--24-05-19 komal--%>
                        <div id="item15">
                            <br />
                            <%--  <asp:UpdatePanel ID="esic_update" runat="server">
                            <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-sm-3 col-xs-12">
                                  <b>  ESIC State:</b>
                                              <asp:DropDownList ID="ddl_esic_state" runat="server" DataTextField="STATE_NAME" Width="100%"
                                                  DataValueField="STATE_NAME" class=" form-control" onchange="location_hidden();">
                                              </asp:DropDownList>
                                </div>

                                <div class="col-sm-3 col-xs-12">
                                   <b> ESIC Office Address :</b>
                                                <asp:TextBox ID="txt_esic_address" runat="server"
                                                    class="form-control" MaxLength="250" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                </div>


                                <div class="col-sm-3 col-xs-12">
                                   <b> ESIC Registration Number :</b>
                                                <asp:TextBox ID="txt_esicregistrationcode" runat="server"
                                                    class="form-control" MaxLength="17" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>

                                <div class="col-sm-3 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="linkbtn_esic" runat="server" OnClick="linkbtn_esic_Click" OnClientClick="return save_validate1();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                    </asp:LinkButton>
                                </div>

                            </div>
                            <br />
                            <div class="container">
                                <asp:Panel ID="Panel20" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="grid_esic" class="table" runat="server" BackColor="White" OnRowDataBound="grid_esic_RowDataBound"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        AutoGenerateColumns="False" Width="100%" OnPreRender="grid_esic_PreRender">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkbtn_removeitem" runat="server" CausesValidation="false" OnClick="linkbtn_removeitem_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ESIC_STATE" HeaderText="ESIC State" SortExpression="ESIC_STATE" />
                                            <asp:BoundField DataField="ESIC_ADDRESS" HeaderText="ESIC ADDRESS" SortExpression="ESIC_ADDRESS" />
                                            <asp:BoundField DataField="ESIC_CODE" HeaderText="ESIC CODE" SortExpression="ESIC_CODE" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                        <div id="item1">
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                  <b>  State :</b>
                                        <asp:DropDownList ID="ddl_dsg_state" runat="server" class="form-control" OnSelectedIndexChanged="get_city_list" AutoPostBack="true">
                                        </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Location : </b>                
                              <asp:DropDownList ID="ddl_location" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                   <b> Designation List :</b>
                                        <asp:DropDownList ID="ddl_designation" runat="server" class="form-control" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>
                                    <br />
                                    <asp:LinkButton ID="lnk_new_designation" runat="server" Visible="false" Text="Add New Designation" Font-Bold="true" OnClick="lnk_new_designation_Click"></asp:LinkButton>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                       <b> Categories : </b>
                                          <asp:DropDownList ID="ddl_categories" runat="server" class="form-control">
                                              <asp:ListItem Value="0">Select</asp:ListItem>
                                              <asp:ListItem Value="1">Highly Skilled</asp:ListItem>
                                              <asp:ListItem Value="2">Semi Skilled</asp:ListItem>
                                              <asp:ListItem Value="3">Skilled</asp:ListItem>
                                              <asp:ListItem Value="4">Unskilled</asp:ListItem>
                                              <asp:ListItem Value="5">A</asp:ListItem>
                                              <asp:ListItem Value="6">B</asp:ListItem>
                                              <asp:ListItem Value="7">C</asp:ListItem>
                                          </asp:DropDownList>
                               </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Count :</b>
                        <asp:TextBox ID="txt_emp_count" runat="server" class="form-control text_box text_box1" MaxLength="10" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Working Hours:</b>
                        <asp:TextBox ID="txt_working_hrs" runat="server" class="form-control text_box text_box1" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                      <input id="Hidden1" type="hidden" runat="server" />
                                      <input id="Hidden2" type="hidden" runat="server" />
                                   <b> Start Date :</b>
                                        <asp:TextBox ID="txt_satrtdate" runat="server" class="form-control date-picker11"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                   <b> End Date :</b>
                                        <asp:TextBox ID="txt_enddate" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return Req_validation1();" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-9 col-xs-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">

                                            <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                OnRowDataBound="gv_itemslist_RowDataBound"
                                                AutoGenerateColumns="False" OnPreRender="gv_itemslist_files_PreRender" Width="100%">

                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemStyle Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="STATE" HeaderText="State" SortExpression="STATE" />
                                                    <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" SortExpression="DESIGNATION" />
                                                    <asp:BoundField DataField="COUNT" HeaderText="Count" SortExpression="COUNT" />
                                                    <asp:BoundField DataField="HOURS" HeaderText="Hours" SortExpression="HOURS" />
                                                    <asp:BoundField DataField="start_date" HeaderText="START DATE" SortExpression="start_date" />
                                                    <asp:BoundField DataField="end_date" HeaderText="END DATE" SortExpression="end_date" />
                                                    <asp:BoundField DataField="location" HeaderText="LOCATION" SortExpression="location" />
                                                    <asp:BoundField DataField="category" HeaderText="Category" SortExpression="category" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div id="item11">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12">
                                           <b> Bank Name:</b>
                                             <asp:TextBox ID="txt_bank_name" runat="server" class="form-control text_box" MaxLength="50" onKeyPress="return AllowAlphabet(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-3 col-xs-12">
                                           <b> Bank Account No. :</b>
                                                <asp:TextBox ID="txt_account_no" runat="server" class="form-control text_box" MaxLength="30" onKeyPress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                          <b>  IFSC Code :</b>
                                                <asp:TextBox ID="txt_ifsc_code" runat="server" class="form-control text_box" MaxLength="10" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_bankdetails" runat="server" OnClick="lnk_bankdetails_Click" OnClientClick="return save_validate2();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                    <br />

                                    <div class="container">
                                        <asp:Panel ID="Panel17" runat="server" CssClass="grid">
                                            <asp:GridView ID="grd_bank_details" class="table" runat="server" BackColor="White"
                                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                OnRowDataBound="grd_bank_details_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="grd_bank_details_PreRender">
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_remove_bank" runat="server" CausesValidation="false" OnClick="lnk_remove_bank_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Field1" HeaderText="Bank Name" SortExpression="Field1" />
                                                    <asp:BoundField DataField="Field2" HeaderText="Account No." SortExpression="Field2" />
                                                    <asp:BoundField DataField="Field3" HeaderText="IFSC Code" SortExpression="Field3" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item3">
                            <br />

                            <div class="row" id="files_upload" runat="server">

                                <div class="col-sm-2 col-xs-12">
                                  <b>  Description:</b>
                                     <asp:TextBox ID="txt_document1" runat="server" class="form-control text_box" MaxLength="100" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <span><b>File to Upload :</b></span>
                                    <asp:FileUpload ID="document1_file" runat="server" meta:resourcekey="photo_uploadResource1" CssClass="text_box" />
                                    <b style="color: #f00; text-align: center">Note :</b> <span style="font-size: 8px; font-weight: bold;">Only JPG, PNG and PDF files will be uploaded.</span>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <b>Start Date :</b>
                             <asp:TextBox ID="txt_from_date" runat="server" class="form-control date-picker1 text_box" Style="display: inline"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> End Date :</b>
                              <asp:TextBox ID="txt_to_date" runat="server" class="form-control date-picker2 text_box" Style="display: inline"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:Button ID="btn_upload" runat="server" class="btn btn-primary" OnClick="btn_upload_Click" Text="Upload" OnClientClick="return upload_r_validation();" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:Button ID="deployment_report" runat="server" class="btn btn-primary" OnClick="deployment_report_Click" Text="Deployment Report" OnClientClick="return deployment_r_validation();" />
                                </div>
                            </div>

                            <div class="container WrapText">
                                <br />
                                <asp:Panel runat="server" CssClass="grd_company">
                                    <asp:GridView ID="grd_company_files" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        OnRowDeleting="grd_company_files_RowDeleting" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="grd_company_files_files_PreRender">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server"
                                                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                                            <asp:BoundField DataField="description" HeaderText="Description"
                                                SortExpression="description" ItemStyle-Wrap="true" />

                                            <asp:BoundField DataField="start_Date" HeaderText="Start Date"
                                                SortExpression="start_Date" />
                                            <asp:BoundField DataField="end_Date" HeaderText="End Date"
                                                SortExpression="end_Date" />
                                            <asp:TemplateField HeaderText="Download File">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" class="btn btn-primary" Text="Delete" CommandName="Delete" OnRowDataBound="grd_company_files_RowDataBound" OnClientClick="return  R_validation();" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <br />
                            <br />


                        </div>
                        <div id="item4">
                            <br />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="newcontactpanel" runat="server" CssClass="panel panel-default">
                                        <div class="container">
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Type :</b>
                                <asp:DropDownList ID="ddl_head_type" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                    <asp:ListItem Value="Legal">Legal</asp:ListItem>
                                    <asp:ListItem Value="Procurment">Procurment</asp:ListItem>
                                    <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                    <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 col-xs-12">
                                                  <b>  Title :</b>
                                  <asp:DropDownList ID="ddl_head_title" runat="server" class="form-control" Width="80px">
                                      <asp:ListItem Value="0">Select</asp:ListItem>
                                      <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                      <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                      <asp:ListItem Value="Ms">Ms.</asp:ListItem>

                                  </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Head Name :</b>
                                   <asp:TextBox ID="txt_head_name" runat="server" class="form-control text_box" placeholder="Contact Person Name" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Head Mobile :</b>
                                   <asp:TextBox ID="txt_head_mobile" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" MaxLength="10" placeholder="Contact Person Mobile"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Email Id :</b>
                                   <asp:TextBox ID="txt_head_email" runat="server" class="form-control text_box" placeholder="Contact Person Email" onkeypress="return email(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Birth Date :</b>
                                   <asp:TextBox ID="txt_head_birthdate" runat="server" class="form-control date-picker"></asp:TextBox>

                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Marriage Anniversary :</b>
                                   <asp:TextBox ID="txt_anniversary" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Child 1 Name :</b>
                                   <asp:TextBox ID="txt_child1" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Birth Date :</b>
                                   <asp:TextBox ID="txt_ch1bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Child 2 Name :</b>
                                   <asp:TextBox ID="txt_child2" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Birth Date :</b>
                                   <asp:TextBox ID="txt_ch2bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Personal Mobile Number :</b>
                                   <asp:TextBox ID="personal_mobile_no_head" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Personal Email ID :</b>
                                   <asp:TextBox ID="personal_mail_id_head" runat="server" class="form-control text_box" onkeypress="return email(event)"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:LinkButton ID="lnk_add_head" runat="server" OnClientClick="return Req_headvalidation();" OnClick="lnk_add_head_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"   />
                                                    </asp:LinkButton>
                                                </div>


                                            </div>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <asp:Panel ID="Panel10" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="gv_head_type" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_head_type_RowDataBound"
                                                        AutoGenerateColumns="False" OnPreRender="gv_head_type_files_PreRender">

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_remove_head" runat="server" CausesValidation="false" OnClick="lnk_remove_head_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Field10" HeaderText="Head Type"
                                                                SortExpression="Field10" />
                                                            <asp:BoundField DataField="Field2" HeaderText="Head Title"
                                                                SortExpression="Field2" />
                                                            <asp:BoundField DataField="Field1" HeaderText="Head Name"
                                                                SortExpression="Field1" />

                                                            <asp:BoundField DataField="Field3" HeaderText="Head Mobile"
                                                                SortExpression="Field3" />
                                                            <asp:BoundField DataField="Field4" HeaderText="Head Email"
                                                                SortExpression="Field4" />
                                                            <asp:BoundField DataField="Field5" HeaderText="Head Birth date"
                                                                SortExpression="Field5" />
                                                            <asp:BoundField DataField="Field6" HeaderText="Marriage Anniversary"
                                                                SortExpression="Field6" />
                                                            <asp:BoundField DataField="Field7" HeaderText="Child 1 Name"
                                                                SortExpression="Field7" />
                                                            <asp:BoundField DataField="Field8" HeaderText="Child1 Birth Date"
                                                                SortExpression="Field8" />
                                                            <asp:BoundField DataField="Field9" HeaderText="Child 2 Name"
                                                                SortExpression="Field9" />

                                                            <asp:BoundField DataField="Field11" HeaderText="Child2 Birth date"
                                                                SortExpression="Field11" />
                                                            <asp:BoundField DataField="Field12" HeaderText="Personal MobileNo"
                                                                SortExpression="Field12" />
                                                            <asp:BoundField DataField="Field13" HeaderText="Personal EmailID"
                                                                SortExpression="Field13" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>

                                            </div>
                                            <br />
                                        </div>

                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item5">

                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel11" runat="server" CssClass="panel panel-default">
                                        <div class="container">
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Zone Head Type :</b>
                                <asp:DropDownList ID="ddl_zone_head" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                    <asp:ListItem Value="Legal">Legal</asp:ListItem>
                                    <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                    <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                    <asp:ListItem Value="Location">Location</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 col-xs-12">
                                                  <b>  Zone :</b>
                                 <asp:DropDownList ID="ddl_zone" runat="server" class="form-control" Width="85px">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="East">East</asp:ListItem>
                                     <asp:ListItem Value="West">West</asp:ListItem>
                                     <asp:ListItem Value="North">North</asp:ListItem>
                                     <asp:ListItem Value="South">South</asp:ListItem>
                                     <asp:ListItem Value="Central">Central</asp:ListItem>
                                     <asp:ListItem Value="North_East">North-East</asp:ListItem>
                                 </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 col-xs-12">
                                                    <b>Title :</b>
                                  <asp:DropDownList ID="ddl_zn_title" runat="server" class="form-control">
                                      <asp:ListItem Value="0">Select</asp:ListItem>
                                      <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                      <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                      <asp:ListItem Value="Ms">Ms.</asp:ListItem>

                                  </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Name :</b>
                                   <asp:TextBox ID="txt_zn_head_name" runat="server" class="form-control text_box" placeholder="Contact Person Name" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Mobile :</b>
                                   <asp:TextBox ID="txt_zn_head_mobile" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" MaxLength="10" placeholder="Contact Person Mobile"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Email Id :</b>
                                   <asp:TextBox ID="txt_zn_head_email" runat="server" class="form-control text_box" placeholder="Contact Person Email" onkeypress="return email(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Head Birth Date :</b>
                                   <asp:TextBox ID="txt_zn_head_birthdate" runat="server" class="form-control date-picker"></asp:TextBox>

                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                 <b>   Marriage Anniversary :</b>
                                   <asp:TextBox ID="txt_zn_anniversary" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Child 1 Name :</b>
                                   <asp:TextBox ID="txt_zn_child1" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Birth Date :</b>
                                   <asp:TextBox ID="txt_zn_ch1bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Child 2 Name :</b>
                                   <asp:TextBox ID="txt_zn_child2" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Birth Date :</b>
                                   <asp:TextBox ID="txt_zn_ch2bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Personal Mobile Number :</b>
                                   <asp:TextBox ID="personal_mobile_no_zonehead" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Personal Email ID :</b>
                                   <asp:TextBox ID="personal_mail_id_zonehead" runat="server" class="form-control text_box" onkeypress="return email(event)"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:LinkButton ID="lnk_zone_add" runat="server" OnClientClick="return Req_validation_lnkzone();" OnClick="lnk_zone_add_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"   />
                                                    </asp:LinkButton>
                                                </div>


                                            </div>
                                            <br />

                                            <br />
                                            <div class="row">
                                                <asp:Panel ID="Panel5" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="gv_zone_add" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_zone_add_RowDataBound"
                                                        AutoGenerateColumns="False" OnPreRender="gv_zone_add_files_PreRender">

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_remove_zone" runat="server" CausesValidation="false" OnClick="lnk_remove_zone_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="Field1" HeaderText="Head Type"
                                                                SortExpression="Field1" />
                                                            <asp:BoundField DataField="ZONE" HeaderText="Zone"
                                                                SortExpression="ZONE" />
                                                            <asp:BoundField DataField="Field11" HeaderText="Title"
                                                                SortExpression="Field11" />
                                                            <asp:BoundField DataField="Field2" HeaderText="Head Name"
                                                                SortExpression="Field2" />
                                                            <asp:BoundField DataField="Field3" HeaderText="Head Mobile"
                                                                SortExpression="Field3" />
                                                            <asp:BoundField DataField="Field4" HeaderText="Head Email"
                                                                SortExpression="Field4" />
                                                            <asp:BoundField DataField="Field5" HeaderText="Head Birth date"
                                                                SortExpression="Field5" />
                                                            <asp:BoundField DataField="Field6" HeaderText="Marriage Anniversary"
                                                                SortExpression="Field6" />
                                                            <asp:BoundField DataField="Field7" HeaderText="Child 1 Name"
                                                                SortExpression="Field7" />
                                                            <asp:BoundField DataField="Field8" HeaderText="Child1 Birth Date"
                                                                SortExpression="Field8" />
                                                            <asp:BoundField DataField="Field9" HeaderText="Child 2 Name"
                                                                SortExpression="Field9" />
                                                            <asp:BoundField DataField="Field10" HeaderText="Child2 Birth date"
                                                                SortExpression="Field10" />
                                                            <asp:BoundField DataField="Field12" HeaderText="Personal MobileNo"
                                                                SortExpression="Field12" />
                                                            <asp:BoundField DataField="Field13" HeaderText="Personal EmailID"
                                                                SortExpression="Field13" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item6">

                            <br />
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel12" runat="server" CssClass="panel panel-default">
                                        <div class="container">
                                            <br />
                                            <div class="row">

                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Region Head Type :</b>
                                <asp:DropDownList ID="ddl_region_head" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                    <asp:ListItem Value="Legal">Legal</asp:ListItem>
                                    <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                    <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                    <asp:ListItem Value="Location">Location</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 col-xs-12">
                                                  <b>  Zone :</b>
                                 <asp:DropDownList ID="ddl_rgn_zone" runat="server" class="form-control" Width="85px">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="East">East</asp:ListItem>
                                     <asp:ListItem Value="West">West</asp:ListItem>
                                     <asp:ListItem Value="North">North</asp:ListItem>
                                     <asp:ListItem Value="South">South</asp:ListItem>
                                     <asp:ListItem Value="Central">Central</asp:ListItem>
                                     <asp:ListItem Value="North_East">North-East</asp:ListItem>
                                 </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Region :</b>
                                   <asp:TextBox ID="txt_region" runat="server" class="form-control text_box" placeholder="Enter Region" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 col-xs-12">
                                                 <b>   Title :</b>
                                  <asp:DropDownList ID="ddl_rgn_title" runat="server" class="form-control">
                                      <asp:ListItem Value="0">Select</asp:ListItem>
                                      <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                      <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                      <asp:ListItem Value="Ms">Ms.</asp:ListItem>

                                  </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2 col-xs-12">
                                                 <b>   Head Name :</b>
                                   <asp:TextBox ID="txt_rgn_head_name" runat="server" class="form-control text_box" placeholder="Contact Person Name" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                    <b>Head Mobile :</b>
                                   <asp:TextBox ID="txt_rgn_head_mobile" runat="server" class="form-control text_box" onkeypress="return isNumber(event)" MaxLength="10" placeholder="Contact Person Mobile"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Head Email Id:</b>
                                   <asp:TextBox ID="txt_rgn_head_email" runat="server" class="form-control text_box" placeholder="Contact Person Email" onkeypress="return email(event)"></asp:TextBox>
                                                </div>


                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Head Birth Date :</b>
                                   <asp:TextBox ID="txt_rgn_head_birthdate" runat="server" class="form-control date-picker"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Marriage Anniversary :</b>
                                   <asp:TextBox ID="txt_rgn_anniversary" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Child 1 Name :</b>
                                   <asp:TextBox ID="txt_rgn_child1" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Birth Date :</b>
                                   <asp:TextBox ID="txt_rgn_ch1bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Child 2 Name :</b>
                                   <asp:TextBox ID="txt_rgn_child2" runat="server" class="form-control text_box" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                   <b> Birth Date :</b>
                                   <asp:TextBox ID="txt_rgn_ch2bday" runat="server" class="form-control date-picker1"></asp:TextBox>
                                                </div>


                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-2 col-xs-12">
                                                  <b>  Personal Mobile Number :</b>
                                   <asp:TextBox ID="personal_mobile_no_regionhead" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 col-xs-12">
                                                 <b>   Personal Email ID :</b>
                                   <asp:TextBox ID="personal_mail_id_regionhead" runat="server" class="form-control text_box" onkeypress="return email(event)"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-2 col-xs-12">
                                                    <br />
                                                    <asp:LinkButton ID="lnk_regional_zone_add" runat="server" OnClientClick="return Req_validation_Region();" OnClick="lnk_regional_zone_add_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"   />
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Panel ID="Panel6" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="gv_regional_zone" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_regional_zone_RowDataBound" OnPreRender="gv_regional_zone_gst_files_PreRender"
                                                        AutoGenerateColumns="False">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_remove_rgzone" runat="server" CausesValidation="false" OnClick="lnk_remove_rgzone_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Field1" HeaderText="Head Type"
                                                                SortExpression="Field1" />
                                                            <asp:BoundField DataField="ZONE" HeaderText="Zone"
                                                                SortExpression="ZONE" />
                                                            <asp:BoundField DataField="REGION" HeaderText="Region"
                                                                SortExpression="REGION" />
                                                            <asp:BoundField DataField="Field2" HeaderText="Title"
                                                                SortExpression="Field2" />
                                                            <asp:BoundField DataField="Field3" HeaderText="Head Name"
                                                                SortExpression="Field3" />
                                                            <asp:BoundField DataField="Field4" HeaderText="Head Mobile"
                                                                SortExpression="Field4" />
                                                            <asp:BoundField DataField="Field5" HeaderText="Head Email"
                                                                SortExpression="Field5" />
                                                            <asp:BoundField DataField="Field6" HeaderText="Head Birth date"
                                                                SortExpression="Field6" />
                                                            <asp:BoundField DataField="Field7" HeaderText="Marriage Anniversary"
                                                                SortExpression="Field7" />
                                                            <asp:BoundField DataField="Field8" HeaderText="Child 1 Name"
                                                                SortExpression="Field8" />
                                                            <asp:BoundField DataField="Field9" HeaderText="Child1 Birth Date"
                                                                SortExpression="Field9" />
                                                            <asp:BoundField DataField="Field10" HeaderText="Child 2 Name"
                                                                SortExpression="Field10" />
                                                            <asp:BoundField DataField="Field11" HeaderText="Child2 Birth date"
                                                                SortExpression="Field11" />
                                                            <asp:BoundField DataField="Field12" HeaderText="Personal MobileNo"
                                                                SortExpression="Field12" />
                                                            <asp:BoundField DataField="Field13" HeaderText="Personal EmailID"
                                                                SortExpression="Field13" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item7">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client GST State :</b>
                                        <asp:DropDownList ID="ddl_gst_state" runat="server" class="form-control">
                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Client GST Address :</b>
                        <asp:TextBox ID="txt_gst_addr" runat="server" MaxLength="300" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  GST IN :</b>
                        <asp:TextBox ID="txt_gst_no" runat="server" MaxLength="30" class="form-control text_box" onKeyPress="return AllowAlphabet_Number(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_add_gst" runat="server" OnClick="lnk_add_gst_Click" OnClientClick="return Req_gstvalidation();" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:Panel ID="Panel8" runat="server">
                                                <asp:Panel ID="Panel9" runat="server" CssClass="grid-view">

                                                    <asp:GridView ID="gv_statewise_gst" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_statewise_gst_RowDataBound" OnPreRender="gv_statewise_gst_files_PreRender"
                                                        AutoGenerateColumns="False" Width="100%">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtn_gst_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_gst_removeitem_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GST State">
                                                                <ItemStyle Width="100px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_gststate" runat="server" Text='<%# Eval("REGION")%>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GST Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_gst_addr" runat="server" Text='<%# Eval("Field1")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="GST IN">
                                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_gstin" runat="server" ReadOnly="True" Style="text-align: left" Text='<%# Eval("Field2")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item8">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Service Category :</b>
                                        <asp:DropDownList ID="dddl_services" runat="server" class="form-control">
                                            <asp:ListItem Value="0">select</asp:ListItem>
                                            <asp:ListItem Value="HK">HOUSEKEEPING</asp:ListItem>
                                            <asp:ListItem Value="SG">SECURITY GUARD</asp:ListItem>
                                            <asp:ListItem Value="3">TEMPORARY</asp:ListItem>
                                            <asp:ListItem Value="4">R&M</asp:ListItem>
                                            <asp:ListItem Value="5">MATERIAL</asp:ListItem>
                                              <asp:ListItem Value="6">CONVENYCE</asp:ListItem>
                                              <asp:ListItem Value="7">DEEP CLEANING</asp:ListItem>
                                              <asp:ListItem Value="8">MACHINE RENTAL</asp:ListItem>

                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Agreement Start Date  :</b>
                        <asp:TextBox ID="txt_staredate" runat="server" class="form-control date-picker1" placeholder="Start Date :"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                           <b> Agreement End Date  :</b>
                                        <asp:TextBox ID="txt_enddate1" runat="server" class="form-control date-picker2" placeholder="End Date :"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_service_category" runat="server" OnClick="lnk_service_category_Click" AutoPostBack="true" OnClientClick="return Service_r_validation();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:Panel ID="panel_service" runat="server">
                                                <asp:Panel ID="panel_service1" runat="server" CssClass="grid-view">

                                                    <asp:GridView ID="gv_services" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_services_RowDataBound" OnPreRender="gv_services_files_PreRender"
                                                        AutoGenerateColumns="False" Width="100%">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>

                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtn_services_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_services_removeitem_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Services Type">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_servicestype" runat="server" Text='<%# Eval("Field1")%>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_lnkstartdate" runat="server" Text='<%# Eval("Field2")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_enddate" runat="server" Text='<%# Eval("Field3")%>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item9" style="display: none;">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Types :</b>
                                <asp:DropDownList ID="ddl_sendemail_type" runat="server" class="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Head_Info">Head_Info</asp:ListItem>
                                    <asp:ListItem Value="Zone_Head_Info">Zone_Head_Info</asp:ListItem>
                                    <asp:ListItem Value="Region_Head_Info">Region_Head_Info</asp:ListItem>

                                </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Head Type :</b>
                                       <asp:DropDownList ID="ddl_head_type_email" runat="server" class="form-control" OnSelectedIndexChanged="ddl_head_names_email" AutoPostBack="true">
                                           <asp:ListItem Value="Select">Select</asp:ListItem>
                                           <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                           <asp:ListItem Value="Legal">Legal</asp:ListItem>
                                           <asp:ListItem Value="Procurment">Procurment</asp:ListItem>
                                           <asp:ListItem Value="Operation">Operation</asp:ListItem>
                                           <asp:ListItem Value="Finance">Finance</asp:ListItem>
                                           <asp:ListItem Value="Other">Other</asp:ListItem>
                                       </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Head Name  :</b>
                                       <asp:DropDownList ID="ddl_mail_headname" runat="server" class="form-control" OnSelectedIndexChanged="ddl_head_emailid" AutoPostBack="true">
                                       </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Head Email_Id :</b>
                                   <asp:TextBox ID="txt_head_emailid" runat="server" MaxLength="10" onkeypress="return email(event)" class="form-control text_box"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnk_add_emailid_Click" AutoPostBack="true" OnClientClick="return send_email_valid();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:Panel ID="Panel13" runat="server">
                                                <asp:Panel ID="Panel14" runat="server" CssClass="grid-view">

                                                    <asp:GridView ID="gv_emailsend" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_statewise_gst_RowDataBound" OnPreRender="gv_statewise_gst_files_PreRender"
                                                        AutoGenerateColumns="False" Width="100%">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_remove_head" runat="server" CausesValidation="false" OnClick="lnk_remove_mail_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Field1" HeaderText="Head Type"
                                                                SortExpression="Field1" />
                                                            <asp:BoundField DataField="Field2" HeaderText="Head Title"
                                                                SortExpression="Field2" />
                                                            <asp:BoundField DataField="Field3" HeaderText="Head Neme"
                                                                SortExpression="Field3" />
                                                            <asp:BoundField DataField="Field4" HeaderText="Head Email"
                                                                SortExpression="Field4" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item10" style="display: none;">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Item Name :</b>
                                <asp:DropDownList ID="ddl_itemname" runat="server" class="form-control">
                                </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                          <b>  Number Of Quanatity :</b>
                                        <asp:TextBox ID="txt_quantity" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Validity(In Days)  :</b>
                                        <asp:TextBox ID="txt_validity" runat="server" MaxLength="10" onkeypress="return isNumber(event)" class="form-control text_box"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                         <b>   Expiry Date :</b>
                                   <asp:TextBox ID="txt_expirydate" runat="server" class="form-control date-picker1"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnk_add_ClientItems_Click" OnClientClick="return Req_add_items();" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12"></div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:Panel ID="Panel15" runat="server">
                                                <asp:Panel ID="Panel16" runat="server" CssClass="grid-view">

                                                    <asp:GridView ID="gv_clientItems" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="gv_clientItems_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" OnPreRender="gv_clientItems_PreRender">
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk_remove_item" runat="server" CausesValidation="false" OnClick="lnk_remove_item_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sr No.">
                                                                <ItemStyle Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="item_name" HeaderText="Item Name"
                                                                SortExpression="item_name" />
                                                            <asp:BoundField DataField="quantiry" HeaderText="Quantity"
                                                                SortExpression="quantiry" />
                                                            <asp:BoundField DataField="validity" HeaderText="Validity(In Days)"
                                                                SortExpression="validity" />
                                                            <asp:BoundField DataField="expiry_date" HeaderText="Expiry Date"
                                                                SortExpression="expiry_date" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div id="item12">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Company Name:</b>
                                        <asp:TextBox ID="txt_company_name" runat="server" MaxLength="100" class="form-control text_box" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> GST No:</b>
                                        <asp:TextBox ID="txt_comp_gst_no" runat="server" MaxLength="15" class="form-control text_box"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> GST Address  :</b>
                                        <asp:TextBox ID="txt_gst_address" runat="server" MaxLength="300" class="form-control text_box"></asp:TextBox>
                                        </div>


                                        <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lnk_add_Comp_Click" AutoPostBack="true" OnClientClick="return company_validation();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-sm-2 col-xs-12"></div>
                                            <br />
                                            <br />
                                            <div class="col-sm-2 col-xs--12"></div>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:Panel ID="Panel18" runat="server">
                                                    <asp:Panel ID="Panel19" runat="server" CssClass="grid-view">

                                                        <asp:GridView ID="gv_comp_group" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            OnRowDataBound="gv_comp_group_RowDataBound"
                                                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_comp_group_PreRender">
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_comp_group" runat="server" CausesValidation="false" OnClick="lnk_remove_comp_group_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="comp_name" HeaderText="Company Name"
                                                                    SortExpression="comp_name" />
                                                                <asp:BoundField DataField="Companyname_gst_no" HeaderText="Gst No"
                                                                    SortExpression="Companyname_gst_no" />
                                                                <asp:BoundField DataField="gst_address" HeaderText="Address"
                                                                    SortExpression="gst_address" />

                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="item13">
                            <br />
                            <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>--%>
                            <div class="row">
                                <%--<div class="col-sm-2 col-xs-12">
                                       Enter Billing Check List Name:
                                        <asp:TextBox ID="ddl_checklist_name" runat="server" MaxLength="100" class="form-control text_box"  onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                                  
                                </div>--%>
                                <div class="col-lg-2 col-md-2  col-xs-12">
                                  <b>  Enter Billing Check List Name :</b>
                            <asp:DropDownList ID="ddl_checklist_name" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="1">Manpower Billing</asp:ListItem>
                                <asp:ListItem Value="2">Material Billing</asp:ListItem>
                                <asp:ListItem Value="3">Conveyance Billing</asp:ListItem>
                                <asp:ListItem Value="4">Deep Clean Billing</asp:ListItem>
                                <asp:ListItem Value="5">Pest Control Billing</asp:ListItem>
                                <asp:ListItem Value="6">Arrears Billing</asp:ListItem>
                                <asp:ListItem Value="7">Machine Rental</asp:ListItem>
                                <asp:ListItem Value="8">R And M Service</asp:ListItem>
                                <asp:ListItem Value="9">Administrative Expenses</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 col-md-2  col-xs-12">
                                 <b>   Billing :</b>
                            <asp:DropDownList ID="ddl_checklist_billing" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="1">Statewise</asp:ListItem>
                                <asp:ListItem Value="2">Branchwise</asp:ListItem>
                                <asp:ListItem Value="3">Statewisedesignation</asp:ListItem>
                                <asp:ListItem Value="4">Branchwisedesignation</asp:ListItem>
                                <asp:ListItem Value="5">Regionwise</asp:ListItem>
                                 <asp:ListItem Value="6">HO</asp:ListItem>


                            </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> State :</b>
                                        <asp:DropDownList ID="ddl_state" runat="server" class="form-control">
                                        </asp:DropDownList>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                  <b> Invoice Shipping Address :</b>
                        <asp:TextBox ID="inv_shipping_add" runat="server" class="form-control text_box text_box1"  onkeypress="return "></asp:TextBox>
                                        </div>



                                <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="lnk_billlig" runat="server" OnClick="lnk_add_billing_Click" AutoPostBack="true" OnClientClick="return EnterCheckListName_validation();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                            <div class="container" style="width: 60%">
                                <asp:Panel ID="Panel21" runat="server" CssClass="grid-view">

                                    <asp:GridView ID="gv_billing_type" class="table" runat="server" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        OnRowDataBound="gv_comp_group_RowDataBound"
                                        AutoGenerateColumns="False" Width="100%" OnPreRender="gv_billing_type_PreRender">
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_Billing" runat="server" CausesValidation="false" OnClick="lnk_remove_Billing_Type_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="checklist_name" HeaderText="Billing Type"
                                                SortExpression="checklist_name" />
                                            <asp:BoundField DataField="checklist_number" HeaderText="Check List Number"
                                                SortExpression="checklist_number" />

                                            <asp:BoundField DataField="checklist_billing" HeaderText="Billing"
                                                SortExpression="checklist_billing" />

                                            <asp:BoundField DataField="checklist_billingNo" HeaderText="Billing wise"
                                                SortExpression="checklist_billingNo" />
                                            <asp:BoundField DataField="state" HeaderText="State"
                                                SortExpression="state" />

                                            <asp:BoundField DataField="invoice_shipping_address" HeaderText="Invoice Shipping Address"
                                                SortExpression="invoice_shipping_address" />


                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>


                            <br />
                            <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                        <div id="item16">
                            <div class="row">
                                <div class="col-lg-2 col-md-2  col-xs-12">
                                   <b> Deduction Item :</b>
                            <asp:DropDownList ID="ddl_dedu_iteam" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Uniform">Uniform</asp:ListItem>
                                <asp:ListItem Value="Shoes">Shoes</asp:ListItem>
                                <asp:ListItem Value="ID_Card">ID Card</asp:ListItem>
                                <asp:ListItem Value="Sweater">Sweater</asp:ListItem>
                                <asp:ListItem Value="Raincoat">Raincoat </asp:ListItem>
                                <asp:ListItem Value="Torch">Torch</asp:ListItem>
                                <asp:ListItem Value="Whistle">Whistle</asp:ListItem>
                                <asp:ListItem Value="Baton">Baton</asp:ListItem>
                                <asp:ListItem Value="Belt">Belt</asp:ListItem>
                                <asp:ListItem Value="Pantry_Jacket">Pantry_Jacket</asp:ListItem>
                                <asp:ListItem Value="Apron">Apron</asp:ListItem>
                            </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 col-md-2  col-xs-12">
                                   <b> Deduction Amount:</b>
                                        <asp:TextBox ID="txt_deduc_amount" runat="server" MaxLength="100" class="form-control text_box"></asp:TextBox>

                                </div>
                                 <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_deduct" runat="server" OnClick="lnk_deduct_Click" OnClientClick ="return deduction_amount();" AutoPostBack="true" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>
                            </div>
                            <br />
                            <div class="container" style="width:60%">
                             <asp:Panel ID="Panel22" runat="server" CssClass="grid-view">

                                                        <asp:GridView ID="grv_dwduction" class="table" runat="server" BackColor="White"
                                                            BorderColor="#CCCCCC" OnRowDataBound="grv_dwduction_RowDataBound" OnPreRender="grv_dwduction_PreRender" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="100%">
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_deduct" runat="server" CausesValidation="false" OnClick="lnk_remove_deduct_Click" ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                  
                                                                <asp:BoundField DataField="deduction_item" HeaderText="Deduction item Name" 
                                                                    SortExpression="deduction_item"/>
                                                                 <asp:BoundField DataField="deduction_amount" HeaderText="Amount" 
                                                                    SortExpression="deduction_amount" />
                                                             
                                                               

                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                </div>
                        </div>
                        <div id="item17">

                            <asp:Panel ID="Panel23" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="email_grid1" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnRowDataBound="email_grid1_RowDataBound"
                                                        AutoGenerateColumns="False" OnPreRender="gv_zone_add_files_PreRender">

                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />

                                                        <Columns>
                                                             <asp:TemplateField HeaderText="Department">
                                                              <ItemTemplate>
                                                                    <asp:Label ID="lbl_dep" runat="server" Text='<%# Eval("Field1")%>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_email"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field2")%>' onKeyPress="return email(event)" onchange="validateEmail(this);" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Password">
                                                              <ItemTemplate>
                                                                   <asp:TextBox ID="txt_password"    runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field3")%>' Width="150px"></asp:TextBox>
                                                                    
                                                              </ItemTemplate>
                                                                </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Name">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_name"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field4")%>' onkeypress="return AllowAlphabet(event)" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Mobile NUM">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_mobile"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field6")%>' Width="150px" onkeypress="return isNumber(event)"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_deg"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field5")%>'  onkeypress="return AllowAlphabet(event)" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="To-Invoice Mail">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_to"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("field9")%>'  onkeypress="return AllowAlphabet(event)" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CC-Invoice Mail">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_cc"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field7")%>' onKeyPress="return email(event)" onchange="validateEmail(this);" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="BCC-Invoice Mail">
                                                              <ItemTemplate>
                                                                    <asp:TextBox ID="txt_bcc"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("Field8")%>' onKeyPress="return email(event)" onchange="validateEmail(this);" Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                </asp:TemplateField>

                                                        </Columns>
                                                        </asp:GridView>
                                 </asp:Panel>
                           
                        </div>

                        <div id="item18">
                             <div class="row">

                            <div class="col-sm-2 col-xs-12">
                           <b> Deduction:</b>
                                <asp:DropDownList ID="ddl_adv_deduction" runat="server"  CssClass="form-control" onchange="tds_validation();">
                                    
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>

                                </asp:DropDownList>
                        </div>
                                  
                            <div class="col-sm-2 col-xs-12">
                         <b>  No Of Uniform:</b>
                                <asp:DropDownList ID="ddl_adv_no" runat="server" CssClass="form-control">
                                    
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>

                                </asp:DropDownList>
                        </div>

                                 <div class="col-sm-2 col-xs-12">
                         <b>  No Of Shoes:</b>
                                <asp:DropDownList ID="ddl_adv_shoes" runat="server" CssClass="form-control">
                                    
                                    <asp:ListItem Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>

                                </asp:DropDownList>
                        </div>

                                 <div class="col-sm-2 col-xs-12">
                        <b>   No Of Id Card:</b>
                                <asp:DropDownList ID="ddl_adv_id" runat="server" CssClass="form-control">
                                    
                                    <asp:ListItem Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>

                                </asp:DropDownList>
                        </div>





                                 <%--<div class="col-sm-2 col-xs-12">
                              Month / Year :<span style="color:red">*</span>
                                <asp:TextBox ID="txttodate" runat="server" MaxLength="10" class="form-control date-pickerk"></asp:TextBox>
                            </div>--%>

                                 <div class="col-sm-2 col-xs-12">
                       <b> Month / Year :</b><span class="text-red">*</span>
                        <asp:TextBox ID="txttodate" runat="server" Visible="true" class="form-control date-picker1 text_box"></asp:TextBox>

                    </div>

                                  <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="link_adv_deduction" runat="server" OnClick="link_adv_deduction_Click" OnClientClick="return " AutoPostBack="true" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>
                                   </div>
                             <br />
                                    <div class="container" style="width:60%">
                             <asp:Panel ID="Panel24" runat="server" CssClass="grid-view" Style="overflow-x:auto;">

                                                        <asp:GridView ID="gridview_advance_deduction" class="table" runat="server" BackColor="White" OnPreRender="gridview_advance_deduction_PreRender" OnRowDataBound="gridview_advance_deduction_RowDataBound"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                            AutoGenerateColumns="False" Width="100%">
                                                             <FooterStyle BackColor="White" ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnk_remove_adv_deduct" runat="server" CausesValidation="false" OnClick="lnk_remove_adv_deduct_Click" ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr No.">
                                                                    <ItemStyle Width="20px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_srnumber_de" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                                                                                
                                                                <asp:BoundField DataField="deduction" HeaderText="Deduction" SortExpression="deduction"/>
                                                                <asp:BoundField DataField="deduction_no" HeaderText="Deduction" SortExpression="deduction_no"/>
                                                                 <asp:BoundField DataField="no_of_uniform" HeaderText="Uniform Set" SortExpression="no_of_uniform" />
                                                                <asp:BoundField DataField="no_of_shoes" HeaderText="Shoes Set" SortExpression="no_of_shoes" />
                                                                <asp:BoundField DataField="no_of_id" HeaderText="Id Card Set" SortExpression="no_of_shoes" />
                                                               <asp:BoundField DataField="month" HeaderText="Month/Year" SortExpression="month" />
                                                              

                                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="GST IN">
                                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_month" runat="server" ReadOnly="True" Style="text-align: left" Text='<%# Eval("month")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                               

                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                </div>
                               
                             </div>

                       <%-- company bank details 22-04-2020 komal--%>

                         <div id="item19">
                             <div class="row">

                                    <div class="col-sm-2 col-xs-12">
                            <b>Payment Against Bank :</b>
                                      
                                <asp:DropDownList ID="ddl_company_bank" runat="server" class="form-control" OnSelectedIndexChanged="ddl_company_bank_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-xs-12" runat="server" id="Div1">
                            <b>Company A/C No:</b>
                        <asp:TextBox ID="txt_comp_ac_no" runat="server" class="form-control text_box" ReadOnly="true"></asp:TextBox>

                        </div>

                                 <div class="col-sm-2 col-xs-12">
                                            <br />
                                            <asp:LinkButton ID="lnk_company_bank" runat="server" OnClick="lnk_company_bank_Click" OnClientClick="return validation_company();" AutoPostBack="true" >
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>

                                        </div>
                                 <br/>
                                   <br/>

                                </div>
                                <br />

                              <div class="row">

                                   <div class="container-fluid" style="width: 60%">
                                     <asp:Panel ID="Panel_comp_bank" runat="server" BackColor="#f3f1fe"  meta:resourcekey="Panel_comp_bankResource1" CssClass="grid-view" >
                                <asp:GridView ID="gv_company_bank" class="table" runat="server" BackColor="White" OnRowDataBound="gv_company_bank_RowDataBound" OnPreRender="gv_company_bank_PreRender"
                                    BorderColor="#CCCCCC" BorderStyle="None" meta:resourcekey="gv_company_bankResource1" BorderWidth="1px" 
                                    AutoGenerateColumns="false" Width="100%">
                                   <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#000066" BackColor="#ffffff" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_remove_com_bank" runat="server" CausesValidation="false" OnClick="lnk_remove_com_bank_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sr No.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                                        <asp:BoundField DataField="payment_ag_bank" HeaderText="Payment Against Bank " SortExpression="payment_ag_bank" />
                                        <asp:BoundField DataField="company_ac_no" HeaderText="Company A/C No" SortExpression="company_ac_no" />
                                       

                                    </Columns>
                                </asp:GridView>
                                          </asp:Panel>

                           </div>
                              </div>
                                  
                              </div>
						 <%-- company bank details 22-04-2020 komal end--%>
                        <br />
                        <%-- fire extinguisher 20-07-2020 komal--%>
                        <div id="item20">
                            <div class="row">
                                <div class="col-sm-2 col-xs-12">
                                    <b>Fire Extinguisher Applicable :</b>
                                    <asp:DropDownList ID="ddl_fire_ext" runat="server" OnSelectedIndexChanged="ddl_fire_ext_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                   
                                        <asp:ListItem Value="Not Applicable">Not Applicable</asp:ListItem>
                                        <asp:ListItem Value="Applicable">Applicable</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                               <%-- <asp:Panel ID="Panel_no_days" runat="server" class="panel-body">--%>
                                  
                                    <div class="col-sm-2 col-xs-12 no_days" style="display: none">
                            <b>No OF Days : <span class="text-red">*</span></b>
                            <asp:TextBox ID="txt_no_of_day" runat="server" class="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>
                        </div>

                                     <div class="col-sm-2 col-xs-12 interval" style="display: none">
                            <b>Interval : <span class="text-red">*</span></b>
                            <asp:TextBox ID="txt_interval_time" runat="server" class="form-control" onkeypress="return isNumber(event)">0</asp:TextBox>
                        </div>
                          
                                    <%-- </asp:Panel>--%>

                                 <div class="col-sm-2 col-xs-12">
                                    <br />
                                    <asp:LinkButton ID="lnk_fire_ext" runat="server" AutoPostBack="true" OnClick="lnk_fire_ext_Click" OnClientClick="return fire_extinguisher_validation(); ">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                            </asp:LinkButton>
                                </div>


                            </div>
                            <br/>

                                    <div class="container" style="width: 80%">
                                <asp:Panel ID="Panel26" runat="server" CssClass="grid-view">
                                    <asp:GridView ID="gv_fire_ext" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" OnRowDataBound="gv_fire_ext_RowDataBound" OnPreRender="gv_fire_ext_PreRender" BorderStyle="None" BorderWidth="1px" CellPadding="3" class="table"  Width="100%">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_fire" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure You want to  Delete ?') " OnClick="lnk_remove_fire_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>" Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fire_ext_applicable" HeaderText="Fire Extinguisher Applicable" SortExpression="fire_ext_applicable" />
                                            <asp:BoundField DataField="no_of_days" HeaderText="No Of Days" SortExpression="no_of_days" />
                                            <asp:BoundField DataField="txt_interval" HeaderText="Interval" SortExpression="txt_interval" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                           
                                          </div>

                        </div>
                        <%-- fire extinguisher 20-07-2020 komal end --%>
                    </div>
                    <br />
                </div>
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_approval" runat="server" class="btn btn-primary" OnClick="btn_approval_Click" Text=" Approve " />
                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click"
                        Text=" Save " OnClientClick=" return Req_validation();" />

                    <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text=" Update "
                        OnClick="btn_edit_Click" OnClientClick=" return new_validate();" />

                    <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text=" Delete "
                        OnClick="btn_delete_Click" OnClientClick="return R_validation();" />



                    <asp:Button ID="btnclose" runat="server" class="btn btn-danger"
                        Text="Close" OnClick="btnclose_Click" />
                    <br />
                </div>
                <br />

                <br />
              <%--  <div class="panel-heading">
                <div class="row">

                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color: black; font-size: large;"><b>Client Master Details:</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
               <%-- <div class="col-sm-9">
                        <div style="text-align: center; color:black; margin-left:300px; margin-bottom:10px; margin-top:10px; font-size: large;"><u>All Client Master Details</u></div>
                    </div>--%>
                <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                <asp:Panel ID="Panel2" runat="server" class="panel-body">
                    <asp:GridView ID="ClientGridView" class="table" runat="server" HeaderStyle-CssClass="FixedHeader" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanged="ClientGridView_SelectedIndexChanged"
                        OnRowDataBound="ClientGridView_RowDataBound" OnPreRender="ClientGridView_PreRender">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" Width="50" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </asp:Panel>
            </div>
                </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/datetimepicker.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.blockUI.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="Scripts/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        function unblock() {
            $.unblockUI();
        }
        $(document).ready(function () {

            tds_validation();
            


            $('#<%=ClientGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

        });
        $(".date-picker11").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'dd/mm/yy',
            yearRange: '1950',
            onSelect: function (selected) {
                alert("The paragraph was clicked.");
                $(".date-picker22").datepicker("option", "minDate", selected)
            }
        });
    </script>
    <script> $(document).ready(function () {
     var t = false

     $(".text_box1").focus(function () {
         var $this = $(this)

         t = setInterval(

         function () {
             if (($this.val() < 1) && $this.val().length != 0) {
                 if ($this.val() < 1) {

                     $this.val("")
                 }

                 if ($this.val() > 24) {
                     $this.val("")
                 }

             }
         }, 50)
     })

     $(".text_box1").blur(function () {
         if (t != false) {
             window.clearInterval(t)
             t = false;
         }
     })

 });

    </script>

    <style>
        .grd_company {
            height: auto;
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }
        box_model {
  background-color: lightgrey;
  width: 10px;
  border: 15px solid green;
  padding: 20px;
  margin: 20px;
}

        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .text-red {
            color: #f00;
        }

        .WrapText {
            width: 100%;
            word-break: break-all;
        }
    </style>

    <script type="text/javascript">

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
        function AllowAlphabet_address(e) {
            if (null != e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                    (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        //vikas for email
        function validateEmail(emailField) {

            var emails = emailField.split(",");

            for (index = 0; index < emails.length; ++index) {
                  var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                if (reg.test(emails[index].value) == false) {
                    alert('Invalid Email Address');
                    return false;
                }
            }
            return true;

        }
        
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '44'|| (keyEntry == '64')))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function AllowAlphabet_Number1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || keyEntry == '44' || keyEntry == '64' || keyEntry == '59' || (keyEntry == '8'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        function email(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '64') || (keyEntry == '44') || (keyEntry == '59') || (keyEntry == '46'))

                    return true;
                else {
                    return false;
                }
            }
        }

        function Req_validation() {
            var t_StateCode = document.getElementById('<%=txt_clientcode.ClientID %>');
            var t_state_city = document.getElementById('<%=txt_clientname.ClientID %>');
            var t_client_address1 = document.getElementById('<%=txt_client_address1.ClientID %>');

            var d_client_state = document.getElementById('<%=ddl_client_state.ClientID %>');
            var client_state = d_client_state.options[d_client_state.selectedIndex].text;
            var t_file_no = document.getElementById('<%=txt_file_no.ClientID %>');
            var t_emp_total = document.getElementById('<%=txt_employee_total.ClientID %>');

            //komal 20-06-10
            var t_emp_txt_start_date_client = document.getElementById('<%=txt_start_date_client.ClientID %>');
            var selected_t_emp_txt_start_date_client = t_emp_txt_start_date_client.options[t_emp_txt_start_date_client.selectedIndex].text;
            var txt_end_date_client = document.getElementById('<%=txt_end_date_client.ClientID %>');

            ////////////////////////////
            //head
            var dd_bank = document.getElementById('<%=ddl_bank.ClientID %>');
            var bank_type = dd_bank.options[dd_bank.selectedIndex].text;




            if (t_StateCode.value == "") {
                alert("Please Enter Client Code");
                t_StateCode.focus();
                return false;
            }

            if (t_state_city.value == "") {
                alert("Please Enter Client Name");
                t_state_city.focus();
                return false;
            }

            if (t_client_address1.value == "") {
                alert("Please Enter Head Office Address");
                t_client_address1.focus();
                return false;
            }

            if (client_state == "Select") {
                alert("Please Select State !!");
                d_client_state.focus();
                return false;
            }

            if (client_state != "Select") {
                var d_client_city = document.getElementById('<%=ddl_client_city.ClientID %>');
                var client_city = d_client_city.options[d_client_city.selectedIndex].text;
                if (client_city == "Select") {
                    alert("Please Select City !!");
                    d_client_city.focus();
                    return false;
                }
            }

            if (t_file_no.value == "" || t_file_no.value == "0") {
                alert("Please Enter File No. !!");
                t_file_no.focus();
                return false;
            }

            if (t_emp_total.value == "" || t_emp_total.value == "0") {
                alert("Please Enter Total Employee Count !!");
                t_emp_total.focus();
                return false;
            }
            //komal 20-06-19
            if (selected_t_emp_txt_start_date_client == "0") {
                alert("Please Select Billing Start Day in Billing / Policy Tab");
                t_emp_txt_start_date_client.focus();
                return false;
            }
            if (txt_end_date_client.value == "") {
                alert("Please Enter Billing End Day in Billing / Policy Tab");
                txt_end_date_client.focus();
                return false;
            }

            if (bank_type == "Select") {
                alert("Please Select Employee Payment Bank !!");
                dd_bank.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;



        }
        function new_validate() {
            var t_StateCode = document.getElementById('<%=txt_clientcode.ClientID %>');
            var t_state_city = document.getElementById('<%=txt_clientname.ClientID %>');
            var t_client_address1 = document.getElementById('<%=txt_client_address1.ClientID %>');

            var d_client_state = document.getElementById('<%=ddl_client_state.ClientID %>');
            var client_state = d_client_state.options[d_client_state.selectedIndex].text;
            var t_file_no = document.getElementById('<%=txt_file_no.ClientID %>');
            var t_emp_total = document.getElementById('<%=txt_employee_total.ClientID %>');
            //komal 20-06-19
            var t_emp_txt_start_date_client = document.getElementById('<%=txt_start_date_client.ClientID %>');
            var selected_t_emp_txt_start_date_client = t_emp_txt_start_date_client.options[t_emp_txt_start_date_client.selectedIndex].text;

            var txt_end_date_client = document.getElementById('<%=txt_end_date_client.ClientID %>');
            //head




            if (t_StateCode.value == "") {
                alert("Please Enter Client Code");
                t_StateCode.focus();
                return false;
            }

            if (t_state_city.value == "") {
                alert("Please Enter Client Name");
                t_state_city.focus();
                return false;
            }

            if (t_client_address1.value == "") {
                alert("Please Enter Head Office Address");
                t_client_address1.focus();
                return false;
            }

            if (client_state == "Select") {
                alert("Please Select State !!");
                d_client_state.focus();
                return false;
            }

            if (client_state != "Select") {
                var d_client_city = document.getElementById('<%=ddl_client_city.ClientID %>');
                var client_city = d_client_city.options[d_client_city.selectedIndex].text;
                if (client_city == "Select") {
                    alert("Please Select City !!");
                    d_client_city.focus();
                    return false;
                }
            }

            if (t_file_no.value == "" || t_file_no.value == "0") {
                alert("Please Enter File No. !!");
                t_file_no.focus();
                return false;
            }

            if (t_emp_total.value == "" || t_emp_total.value == "0") {
                alert("Please Enter Total Employee Count !!");
                t_emp_total.focus();
                return false;
            }
            //komal 20-06-19
            if (selected_t_emp_txt_start_date_client == "0") {
                alert("Please Select Billing Start Day in Billing / Policy Tab");
                t_emp_txt_start_date_client.focus();
                return false;
            }
            if (txt_end_date_client.value == "") {
                alert("Please Enter Billing End Day in Billing / Policy Tab");
                txt_end_date_client.focus();
                return false;
            }

            var t_emp_txt_start_date_client = document.getElementById('<%=txt_start_date_client.ClientID %>');
            var selected_t_emp_txt_start_date_client = t_emp_txt_start_date_client.options[t_emp_txt_start_date_client.selectedIndex].text;
            var txt_end_date_client = document.getElementById('<%=txt_end_date_client.ClientID %>');




            if (selected_t_emp_txt_start_date_client == "0") {
                alert("Please Select Billing Start Day in Billing / Policy Tab");
                t_emp_txt_start_date_client.focus();
                return false;
            }
            if (txt_end_date_client.value == "") {
                alert("Please Enter Billing End Day in Billing / Policy Tab");
                txt_end_date_client.focus();
                return false;
            }

            var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');
            if (!reason_update.disabled) {
                if (reason_update.value == "") {
                    alert("Please Specify Reason For Updation !!!");
                    reason_update.focus();
                    return false;
                }
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }


        }

        function Req_validation1() {
            var designation = document.getElementById('<%=ddl_designation.ClientID %>');
            var Selecteddesignation = designation.options[designation.selectedIndex].text;
            var t_emp_count = document.getElementById('<%=txt_emp_count.ClientID %>');
            var t_working_hrs = document.getElementById('<%=txt_working_hrs.ClientID %>');
            var t_startdate = document.getElementById('<%=txt_satrtdate .ClientID %>');
            var t_enddate = document.getElementById('<%=txt_enddate.ClientID %>');

            var dsg_state = document.getElementById('<%=ddl_dsg_state.ClientID %>');
            var Selected_state = dsg_state.options[dsg_state.selectedIndex].text;

            if (Selected_state == "Select") {
                alert("Please Select State For Designation !!");
                dsg_state.focus();
                return false;
            }
            var ddl_location = document.getElementById('<%=ddl_location.ClientID %>');
            var Selected_ddl_location = ddl_location.options[ddl_location.selectedIndex].text;

            if (Selected_ddl_location == "Select") {
                alert("Please Select Location");
                ddl_location.focus();
                return false;
            }
            if (Selecteddesignation == "Select") {
                alert("Please Select Designation !!");
                designation.focus();
                return false;
            }
            var ddl_categories = document.getElementById('<%=ddl_categories.ClientID %>');
            var Selected_category = ddl_categories.options[ddl_categories.selectedIndex].text;

            if (Selected_category == "Select") {
                alert("Please Select Category !!");
                ddl_categories.focus();
                return false;
            }

            if (t_emp_count.value == "" || t_emp_count.value == "0") {
                alert("Please Enter Employee Count !!");
                t_emp_count.focus();
                return false;
            }

            if (t_working_hrs.value == "" || t_working_hrs.value == "0") {
                alert("Please Enter Working Hours !!");
                t_working_hrs.focus();
                return false;
            }
            if (t_startdate.value == "") {
                alert("Please Select Start Date !!");
                t_startdate.focus();
                return false;
            }
            //if (t_enddate.value == "") {
            //    alert("Please Add Category First !!");
            //    t_enddate.focus();
            //    return false;
            //}
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function Req_validation_lnkzone() {

            var d_zone = document.getElementById('<%=ddl_zone.ClientID %>');
            var zone = d_zone.options[d_zone.selectedIndex].text;

            var head_type = document.getElementById('<%=ddl_zone_head.ClientID %>');
            var Selected_head_type = head_type.options[head_type.selectedIndex].text;

            var d_title = document.getElementById('<%=ddl_zn_title.ClientID %>');
            var Selected_d_title = d_title.options[d_title.selectedIndex].text;

            var r_head = document.getElementById('<%=ddl_region_head.ClientID %>');
            var Selected_r_head = r_head.options[r_head.selectedIndex].text;

            var t_head_name = document.getElementById('<%=txt_zn_head_name.ClientID %>');
            var t_head_mobile = document.getElementById('<%=txt_zn_head_mobile.ClientID %>');
            var t_head_email = document.getElementById('<%=txt_zn_head_email.ClientID %>');





            //Zone Head Type
            if (Selected_head_type == "Select") {
                alert("Please Select Zone Head Type !!");
                head_type.focus();
                return false;
            }


            // Zone Head
            if (zone == "Select") {
                alert("Please Select Zone !!");
                d_zone.focus();
                return false;
            }

            if (Selected_d_title == "Select") {
                alert("Please Select Zone Head Title !!");
                d_title.focus();
                return false;
            }


            if (t_head_name.value == "") {
                alert("Please Enter Zone Head Name !!");
                t_head_name.focus();
                return false;
            }

            if (t_head_mobile.value == "") {
                alert("Please Enter Zone Head Mobile No !!");
                t_head_mobile.focus();
                return false;
            }

            if (t_head_email.value == "") {
                alert("Please Enter Zone Head Email !!");
                t_head_email.focus();
                return false;
            }
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(t_head_email.value)) {
                alert('Please provide a valid email address');
                t_head_email.focus;
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;


        }


        // region

        function Req_validation_lnkregion() {



            var errmsg1 = "";
            var email = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

            // region Zone Head

        }

        function Req_gstvalidation() {

            var gst_state = document.getElementById('<%=ddl_gst_state.ClientID %>');
            var Selected_gst_state = gst_state.options[gst_state.selectedIndex].text;

            var t_gst_addr = document.getElementById('<%=txt_gst_addr.ClientID %>');
            var t_gst_no = document.getElementById('<%=txt_gst_no.ClientID %>');


            if (Selected_gst_state == "Select") {
                alert("Please Select State For GST !!");
                gst_state.focus();
                return false;
            }


            if (t_gst_addr.value == "") {
                alert("Please Enter GST Office Address !!");
                t_gst_addr.focus();
                return false;
            }

            if (t_gst_no.value == "") {
                alert("Please Enter GSTIN !!");
                t_gst_no.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function deduction_amount() {

            var ddl_dedu_iteam = document.getElementById('<%=ddl_dedu_iteam.ClientID %>');
            var Selected_ddl_dedu_iteam = ddl_dedu_iteam.options[ddl_dedu_iteam.selectedIndex].text;

            var txt_deduc_amount = document.getElementById('<%=txt_deduc_amount.ClientID %>');
           
            if (Selected_ddl_dedu_iteam == "Select") {
                alert("Please Select Deduction Item !!");
                ddl_dedu_iteam.focus();
                return false;
            }

            if (txt_deduc_amount.value == "") {
                alert("Please Enter Deduction Amount !!");
                txt_deduc_amount.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;


        }




       
        function deduction() {

           
         }

        function Req_headvalidation() {




            var head_type = document.getElementById('<%=ddl_head_type.ClientID %>');
            var Selected_head_type = head_type.options[head_type.selectedIndex].text;
            var d_title = document.getElementById('<%=ddl_head_title.ClientID %>');
            var Selected_d_title = d_title.options[d_title.selectedIndex].text;
            var t_head_name = document.getElementById('<%=txt_head_name.ClientID %>');
            var t_head_mobile = document.getElementById('<%=txt_head_mobile.ClientID %>');
            var t_head_email = document.getElementById('<%=txt_head_email.ClientID %>');

            if (Selected_head_type == "Select") {
                alert("Please Select Head Type !!");
                head_type.focus();
                return false;
            }

            if (Selected_d_title == "Select") {
                alert("Please Select Zone Head Title !!");
                d_title.focus();
                return false;
            }
            if (t_head_name.value == "") {
                alert("Please Enter Head Name !!");
                t_head_name.focus();
                return false;
            }

            if (t_head_mobile.value == "") {
                alert("Please Enter Head Mobile No !!");
                t_head_mobile.focus();
                return false;
            }

            if (t_head_email.value == "") {
                alert("Please Enter Head Email !!");
                t_head_email.focus();
                return false;
            }

            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(t_head_email.value)) {
                alert('Please provide a valid email address');
                t_head_email.focus;
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

            if (errmsg1 != "") {
                alert(errmsg1);
                t_head_email.focus();
                return false;
            }

            //if (t_head_birthdate.value == "") {
            //    alert("Please Enter Head Birth Date !!");
            //    t_head_birthdate.focus();
            //    return false;
            //}

            //if (t_anniversary.value == "") {
            //    alert("Please Enter Anniversary Date !!");
            //    t_anniversary.focus();
            //    return false;
            //}
        }

        function fire_extinguisher_validation() {
            var ddl_fire_ext = document.getElementById('<%=ddl_fire_ext.ClientID %>');
            var Selected_ddl_fire_ext = ddl_fire_ext.options[ddl_fire_ext.selectedIndex].text;

            if (Selected_ddl_fire_ext == "Not Applicable") {
                alert("Please Select Fire Extinguisher Applicable");
                ddl_fire_ext.focus();
                return false;

            }

            var txt_no_of_day = document.getElementById('<%=txt_no_of_day.ClientID %>');

            if (txt_no_of_day.value == "0") {
                 alert("Please Enter No Of Days");
                 txt_no_of_day.focus();
                 return false;
             }

            var txt_interval_time = document.getElementById('<%=txt_interval_time.ClientID %>');

            if (txt_interval_time.value == "0") {
                 alert("Please Enter Interval Time");
                 txt_interval_time.focus();
                 return false;
             }

            var txt_clientcode = document.getElementById('<%=txt_clientcode.ClientID %>');

            if (txt_clientcode.value == "") {
                alert("Please Enter Client Code");
                txt_clientcode.focus();
                return false;
            }

            var txt_clientname = document.getElementById('<%=txt_clientname.ClientID %>');

            if (txt_clientname.value == "") {
                alert("Please Enter Client Name");
                txt_clientname.focus();
                return false;
            }



             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;

         }

        function Req_validation_Region() {
            var r_head1 = document.getElementById('<%=ddl_region_head.ClientID %>');
            var Selected_r_head = r_head1.options[r_head1.selectedIndex].text;

            var r_zone = document.getElementById('<%=ddl_rgn_zone.ClientID %>');
            var Selected_r_zone = r_zone.options[r_zone.selectedIndex].text;

            var r_title = document.getElementById('<%=ddl_rgn_title.ClientID %>');
            var Selected_r_title = r_title.options[r_title.selectedIndex].text;

            var t_region = document.getElementById('<%=txt_region.ClientID %>');
            var tr_name = document.getElementById('<%=txt_rgn_head_name.ClientID %>');
            var tr_m = document.getElementById('<%=txt_rgn_head_mobile.ClientID %>');
            var tr_e = document.getElementById('<%=txt_rgn_head_email.ClientID %>');




            if (Selected_r_head == "Select") {
                alert("Please Select Region Head Type !!");
                r_head1.focus();
                return false;
            }

            if (Selected_r_zone == "Select") {
                alert("Please Select Zone !!");
                r_zone.focus();
                return false;
            }

            if (t_region.value == "") {
                alert("Please Select Region !!");
                t_region.focus();
                return false;
            }


            if (Selected_r_title == "Select") {
                alert("Please Select Title !!");
                r_title.focus();
                return false;
            }


            if (tr_name.value == "") {
                alert("Please Select Head Name !!");
                tr_name.focus();
                return false;
            }

            if (tr_m.value == "") {
                alert("Please Select Head Mobile Number !!");
                tr_m.focus();
                return false;
            }

            if (tr_e.value == "") {
                alert("Please Select Head Email Address !!");
                tr_e.focus();
                return false;
            }
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(tr_e.value)) {
                alert('Please provide a valid email address');
                tr_e.focus;
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function material() {
            var d_material_calc = document.getElementById('<%=ddl_material_calc.ClientID %>');
            var Selected_material_calc = d_material_calc.options[d_material_calc.selectedIndex].text;
            var t_material_days = document.getElementById('<%=txt_material_days.ClientID %>');

            if (Selected_material_calc == "Working Days") {
                t_material_days.disabled = true;
            }
            else { t_material_days.disabled = false; }

        }
        function openWindow() {
            window.open("html/State.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>

    <script type="text/javascript">



        function openWindow() {
            window.open("html/Itemmaster1.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


    </script>
    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }

        function callfnc() {
            document.getElementById('<%= Button2.ClientID %>').click();
        }


        function openWindow() {
            window.open("html/ClientMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function R_validation() {
            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }

        // 1-1-2020 TDS Validation

        function tds_validation ()
        {

            var ddl_tds_applicable = document.getElementById('<%=ddl_tds_applicable.ClientID %>');
            var Selected_ddl_tds_applicable = ddl_tds_applicable.options[ddl_tds_applicable.selectedIndex].text

            var ddl_adv_deduction = document.getElementById('<%=ddl_adv_deduction.ClientID %>');
            var Selected_ddl_adv_deduction = ddl_adv_deduction.options[ddl_adv_deduction.selectedIndex].text

            var ddl_adv_no = document.getElementById('<%=ddl_adv_no.ClientID%>');
            var ddl_adv_shoes = document.getElementById('<%=ddl_adv_shoes.ClientID%>')
            var ddl_adv_id = document.getElementById('<%=ddl_adv_id.ClientID%>')

            var txt_handling_percent = document.getElementById('<%=ddl_tds_persent.ClientID %>');
            var txt_handling_amount = document.getElementById('<%=ddl_tds_on.ClientID %>');

            if (Selected_ddl_tds_applicable == "NO") {
                txt_handling_percent.disabled = true;
                txt_handling_amount.disabled = true;
               
            }else
            {
                txt_handling_percent.disabled = false;
                txt_handling_amount.disabled = false;
                
            }

            if (Selected_ddl_adv_deduction == "NO") {
                ddl_adv_no.disabled = true;
                ddl_adv_shoes.disabled = true;
                ddl_adv_id.disabled = true;

            } else {
                ddl_adv_no.disabled = false;
                ddl_adv_shoes.disabled = false;
                ddl_adv_id.disabled = false;

            }

        
        }

        function pageLoad() {
            billing_start_date();

            fire_applicable();
            ///// for deduction


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=grv_dwduction.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=grv_dwduction.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';




            ///////

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gridview_advance_deduction.ClientID%>').DataTable({
                  "responsive": true,
                  "sPaginationType": "full_numbers",
                  buttons: [
                      {
                          extend: 'csv',
                          exportOptions: {
                              columns: ':visible'
                          }
                      },
                      {
                          extend: 'print',
                          exportOptions: {
                              columns: ':visible'
                          }
                      },
                      {
                          extend: 'copyHtml5',
                          exportOptions: {
                              columns: ':visible'
                          }
                      },
                      'colvis'
                  ]

              });

              table.buttons().container()
                 .appendTo('#<%=gridview_advance_deduction.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';






            //////////

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_billing_type.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_billing_type.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_comp_group.ClientID%>').DataTable({
                       "responsive": true,
                       "sPaginationType": "full_numbers",
                       buttons: [
                           {
                               extend: 'csv',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           {
                               extend: 'print',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           {
                               extend: 'copyHtml5',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           'colvis'
                       ]

                   });

                   table.buttons().container()
                      .appendTo('#<%=gv_comp_group.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';


                var table = $('#<%=gv_fire_ext.ClientID%>').DataTable({
                     "responsive": true,
                     "sPaginationType": "full_numbers",
                     buttons: [
                         {
                             extend: 'csv',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         {
                             extend: 'print',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         {
                             extend: 'copyHtml5',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         'colvis'
                     ]

                 });

                 table.buttons().container()
                    .appendTo('#<%=gv_fire_ext.ClientID%>_wrapper .col-sm-6:eq(0)');





            $.fn.dataTable.ext.errMode = 'none';
            ///

            var table = $('#<%=gv_clientItems.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

                 });

                 table.buttons().container()
                    .appendTo('#<%=gv_clientItems.ClientID%>_wrapper .col-sm-6:eq(0)');
                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=grd_bank_details.ClientID%>').DataTable({
                     "responsive": true,
                     "sPaginationType": "full_numbers",
                     buttons: [
                         {
                             extend: 'csv',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         {
                             extend: 'print',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         {
                             extend: 'copyHtml5',
                             exportOptions: {
                                 columns: ':visible'
                             }
                         },
                         'colvis'
                     ]

                 });

                 table.buttons().container()
                    .appendTo('#<%=grd_bank_details.ClientID%>_wrapper .col-sm-6:eq(0)');

                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=grid_esic.ClientID%>').DataTable({
                       "responsive": true,
                       "sPaginationType": "full_numbers",
                       buttons: [
                           {
                               extend: 'csv',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           {
                               extend: 'print',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           {
                               extend: 'copyHtml5',
                               exportOptions: {
                                   columns: ':visible'
                               }
                           },
                           'colvis'
                       ]

                   });

                   table.buttons().container()
                      .appendTo('#<%=grid_esic.ClientID%>_wrapper .col-sm-6:eq(0)');
                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=ClientGridView.ClientID%>').DataTable({
                    scrollY: "220px",
                    buttons: [
                {
                    extend: 'csv',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
                        'colvis'
                    ],
                    fixedHeader: {
                        header: true,
                        footer: true
                    }

                });

                table.buttons().container()
                   .appendTo('#<%=ClientGridView.ClientID%>_wrapper .col-sm-6:eq(0)');



            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_services.ClientID%>').DataTable({
                   "responsive": true,
                   "sPaginationType": "full_numbers",
                   buttons: [
                       {
                           extend: 'csv',
                           exportOptions: {
                               columns: ':visible'
                           }
                       },
                       {
                           extend: 'print',
                           exportOptions: {
                               columns: ':visible'
                           }
                       },
                       {
                           extend: 'copyHtml5',
                           exportOptions: {
                               columns: ':visible'
                           }
                       },
                       'colvis'
                   ]

               });

               table.buttons().container()
                  .appendTo('#<%=gv_services.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_statewise_gst.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_statewise_gst.ClientID%>_wrapper .col-sm-6:eq(0)');
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_regional_zone.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_regional_zone.ClientID%>_wrapper .col-sm-6:eq(0)');
           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=gv_zone_add.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_zone_add.ClientID%>_wrapper .col-sm-6:eq(0)');
           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=gv_head_type.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_head_type.ClientID%>_wrapper .col-sm-6:eq(0)');
           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=grd_company_files.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=grd_company_files.ClientID%>_wrapper .col-sm-6:eq(0)');
           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=gv_itemslist.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_itemslist.ClientID%>_wrapper .col-sm-6:eq(0)');
           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=GridView1.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=GridView1.ClientID%>_wrapper .col-sm-6:eq(0)');

           $.fn.dataTable.ext.errMode = 'none';
           var table = $('#<%=grid_esic.ClientID%>').DataTable({
                 "responsive": true,
                 "sPaginationType": "full_numbers",
                 buttons: [
                     {
                         extend: 'csv',
                         exportOptions: {
                             columns: ':visible'
                         }
                     },
                     {
                         extend: 'print',
                         exportOptions: {
                             columns: ':visible'
                         }
                     },
                     {
                         extend: 'copyHtml5',
                         exportOptions: {
                             columns: ':visible'
                         }
                     },
                     'colvis'
                 ]

             });

             table.buttons().container()
                .appendTo('#<%=grid_esic.ClientID%>_wrapper .col-sm-6:eq(0)');
            material();
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1951',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                    //$(".date-picker2").datepicker("option", "maxDate", selected)
                }
            });

            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                //minDate: 0,
                
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                maxDate: '-18Y',
                yearRange: '-110:-18',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });
            $(".date-picker11").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                minDate: document.getElementById('<%=Hidden1.ClientID %>').value,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
                onSelect: function (selected) {
                    // alert("vikas");
                    $(".date-picker22").datepicker("option", "minDate", selected)
                }
            });


            $(".date-picker22").datepicker({
                changeMonth: true,
                changeYear: true,
                maxDate: document.getElementById('<%=Hidden2.ClientID %>').value,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker11").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker11").attr("readonly", "true");
            $(".date-picker22").attr("readonly", "true");
            $(".date-picker").attr("readonly", "true");
            $(".date-picker1").attr("readonly", "true");
            $(".date-picker2").attr("readonly", "true");

            $('#<%=ddl_dsg_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });
            $('#<%=ClientGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });
            $('#<%=ddl_client_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });
            $('#<%=ddl_head_type_email.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=gv_start_end_date_details.ClientID%>').DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                buttons: [
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    'colvis'
                ]

            });

            table.buttons().container()
               .appendTo('#<%=gv_start_end_date_details.ClientID%>_wrapper .col-sm-6:eq(0)');
            }
            //komal 20-06-19

            function billing_start_date() {
                var start_date = document.getElementById('<%=txt_start_date_client.ClientID %>');
                var start_date_value = start_date.options[start_date.selectedIndex].text;
                var end_date = document.getElementById('<%=txt_end_date_client.ClientID %>');

            end_date.value = (parseInt(start_date_value) - 1);
            if (parseInt(start_date_value) == 1)
            { end_date.value = 31; }
            end_date.readOnly = true;
            return true;
        }





        function Service_r_validation() {
            var dddl_services = document.getElementById('<%=dddl_services.ClientID %>');
             var Selected_dddl_services = dddl_services.options[dddl_services.selectedIndex].text;

             var txt_staredate = document.getElementById('<%=txt_staredate.ClientID %>');
              var txt_enddate1 = document.getElementById('<%=txt_enddate1.ClientID %>');

             if (Selected_dddl_services == "select") {
                 alert("Please Select Service Category");
                 dddl_services.focus();
                 return false;
             }
             if (txt_staredate.value == "") {
                 alert("Please Select Agreement Start Date");
                 txt_staredate.focus();
                 return false;
             }
             if (txt_enddate1.value == "") {
                 alert("Please Select Agreement End Date");
                 txt_enddate1.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }

         function save_validate2() {
             var txt_bank_name = document.getElementById('<%=txt_bank_name.ClientID %>');
              var txt_account_no = document.getElementById('<%=txt_account_no.ClientID %>');
              var txt_ifsc_code = document.getElementById('<%=txt_ifsc_code.ClientID %>');

              if (txt_bank_name.value == "") {
                  alert("Please Enter Bank Name");
                  txt_bank_name.focus();
                  return false;
              }
              if (txt_account_no.value == "") {
                  alert("Please Enter Bank Account Number");
                  txt_account_no.focus();
                  return false;
              }
              if (txt_ifsc_code.value == "") {
                  alert("Please Enter IFSC Code");
                  txt_ifsc_code.focus();
                  return false;
              }
              $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
              return true;


          }

        function fire_applicable()
        {

            var ddl_fire_ext = document.getElementById('<%=ddl_fire_ext.ClientID %>');
            var Selected_ddl_fire_ext = ddl_fire_ext.options[ddl_fire_ext.selectedIndex].value;

            var Selected_ddl_fire_ext = ddl_fire_ext.options[ddl_fire_ext.selectedIndex].text;
            if (Selected_ddl_fire_ext == "Applicable") {
                $(".no_days").show();
                $(".interval").show();
            }

            else {
                $(".no_days").hide();
                $(".interval").hide();
            }

        }

          function Req_add_items() {
              var ddl_itemname = document.getElementById('<%=ddl_itemname.ClientID %>');
            var Selected_ddl_itemname = ddl_itemname.options[ddl_itemname.selectedIndex].text;
            var txt_quantity = document.getElementById('<%=txt_quantity.ClientID %>');
            var txt_validity = document.getElementById('<%=txt_validity.ClientID %>');
            var txt_expirydate = document.getElementById('<%=txt_expirydate.ClientID %>');

            if (Selected_ddl_itemname == "Select") {
                alert("Please Select Item Name");
                ddl_itemname.focus();
                return false;
            }
            if (txt_quantity.value == "") {
                alert("Please Enter Number of Quantity");
                txt_quantity.focus();
                return false;
            }
            if (txt_validity.value == "") {
                alert("Please Enter Validity");
                txt_validity.focus();
                return false;
            }
            if (txt_expirydate.value == "") {
                alert("Please Select Expiry Date");
                txt_expirydate.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function send_email_valid() {

            var ddl_sendemail_type = document.getElementById('<%=ddl_sendemail_type.ClientID %>');
            var Selected_ddl_sendemail_type = ddl_sendemail_type.options[ddl_sendemail_type.selectedIndex].text;
            if (Selected_ddl_sendemail_type == "Select") {
                alert("Please Select Email Type");
                ddl_sendemail_type.focus();
                return false;
            }
            var ddl_head_type_email = document.getElementById('<%=ddl_head_type_email.ClientID %>');
            var Selected_ddl_head_type_email = ddl_head_type_email.options[ddl_head_type_email.selectedIndex].text;

            if (Selected_ddl_head_type_email == "Select") {
                alert("Please Select Head Type");
                ddl_head_type_email.focus();
                return false;
            }
            var ddl_mail_headname = document.getElementById('<%=ddl_mail_headname.ClientID %>');
            var Selected_ddl_mail_headname = ddl_mail_headname.options[ddl_mail_headname.selectedIndex].text;

            if (Selected_ddl_mail_headname == "Select") {
                alert("Please Select Head Name");
                ddl_mail_headname.focus();
                return false;
            }
            if (txt_head_emailid.value == "") {
                alert("Please Enter Head Email Id");
                txt_head_emailid.focus();
                return false;
            }
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(txt_head_emailid.value)) {
                alert('Please Enter valid email address');
                txt_head_emailid.focus;
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function company_validation() {
            var txt_company_name = document.getElementById('<%=txt_company_name.ClientID %>');
            var txt_comp_gst_no = document.getElementById('<%=txt_comp_gst_no.ClientID %>');
            var txt_gst_address = document.getElementById('<%=txt_gst_address.ClientID %>');

            if (txt_company_name.value == "") {
                alert("Please Enter Company Name");
                txt_company_name.focus();
                return false;
            }
            if (txt_comp_gst_no.value == "") {
                alert("Please Enter Company GST Number");
                txt_comp_gst_no.focus();
                return false;
            }
            if (txt_gst_address.value == "") {
                alert("Please Enter Company GST Address");
                txt_gst_address.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function EnterCheckListName_validation() {
            var ddl_checklist_name = document.getElementById('<%=ddl_checklist_name.ClientID %>');
            var Selected_ddl_checklist_name = ddl_checklist_name.options[ddl_checklist_name.selectedIndex].text;

            if (Selected_ddl_checklist_name == "Select") {
                alert("Please Select Check List Name");
                ddl_checklist_name.focus();
                return false;


            }

            var ddl_checklist_billing = document.getElementById('<%=ddl_checklist_billing.ClientID %>');

            var Selected_checklist_billing = ddl_checklist_billing.options[ddl_checklist_billing.selectedIndex].text;

            if (Selected_checklist_billing == "Select") {
                alert("Please Select Billing ");
                ddl_checklist_billing.focus();
                return false;

            }
            var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;

            if (Selected_ddl_state == "Select") {
                alert("Please Select State ");
                ddl_state.focus();
                return false;


                }
                var inv_shipping_add = document.getElementById('<%=inv_shipping_add.ClientID %>');
            if (inv_shipping_add.value == "") {
                alert("Please Enter Shipping Address");
                inv_shipping_add.focus();
                return false;
            }



            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function upload_r_validation() {
            var txt_document1 = document.getElementById('<%=txt_document1.ClientID %>');
            var document1_file = document.getElementById('<%=document1_file.ClientID %>');
            var txt_from_date = document.getElementById('<%=txt_from_date.ClientID %>');
            var txt_to_date = document.getElementById('<%=txt_to_date.ClientID %>');

            if (txt_document1.value == "") {
                alert("Please Enter Description");
                txt_document1.focus();
                return false;
            }

            if (document1_file.value == "") {
                alert("Please Upload File");
                document1_file.focus();
                return false;
            }

            if (txt_from_date.value == "") {
                alert("Please Select Start Date");
                txt_from_date.focus();
                return false;
            }

            if (txt_to_date.value == "") {
                alert("Please Select End Date");
                txt_to_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function deployment_r_validation() {
            var txt_from_date = document.getElementById('<%=txt_from_date.ClientID %>');
            var txt_to_date = document.getElementById('<%=txt_to_date.ClientID %>');


            if (txt_from_date.value == "") {
                alert("Please Select Start Date");
                txt_from_date.focus();
                return false;
            }

            if (txt_to_date.value == "") {
                alert("Please Select End Date");
                txt_to_date.focus();
                return false;
            }

           // $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function save_validate1() {
            var ddl_esic_state = document.getElementById('<%=ddl_esic_state.ClientID %>');
            var Selected_ddl_esic_state = ddl_esic_state.options[ddl_esic_state.selectedIndex].text;

            if (Selected_ddl_esic_state == "Select") {
                alert("Please Select ESIC State");
                ddl_esic_state.focus();
                return false;
            }

            var txt_esic_address = document.getElementById('<%=txt_esic_address.ClientID %>');
            if (txt_esic_address.value == "") {
                alert("Please Enter ESIC Office Address");
                txt_esic_address.focus();
                return false;
            }
            var txt_esicregistrationcode = document.getElementById('<%=txt_esicregistrationcode.ClientID %>');
            if (txt_esicregistrationcode.value == "") {
                alert("Please Enter ESIC Registration Number");
                txt_esicregistrationcode.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
    </script>
</asp:Content>
