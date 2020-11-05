<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster1" Title="Company Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Company Master</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: white;">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="text-align: center; color: #fff; font-size: small;"><b>COMPANY MASTER</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Company Master Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="row text-center">
               
                <%--Please dont delete this 3 Buttons...Vinod --%>
                <asp:Button ID="Button6" runat="server" CssClass="hidden" />
                <asp:Button ID="Button7" runat="server" CssClass="hidden" />
                <asp:Button ID="Button8" runat="server" CssClass="hidden" OnClick="Button8_Click" />


                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel9" TargetControlID="Button7"
                    CancelControlID="Button9" BackgroundCssClass="Background">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel9" runat="server" CssClass="Popup" Style="display: none">
                    <iframe style="width: 800px; height: 500px; background-color: #fff;" id="Iframe2" src="Approval_reject_reason.aspx" runat="server"></iframe>
                    <div class="row text-center">
                        <asp:Button ID="Button9" CssClass="btn btn-danger" OnClientClick="callfnc2()" runat="server" Text="Close" />
                    </div>

                    <br />

                </asp:Panel>
            </div>
            <div class="container">
                <asp:Panel ID="panel10" runat="server" CssClass="grid-view">
                    <asp:GridView ID="GridView2" class="table" runat="server" ForeColor="#333333" Font-Size="X-Small">
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
                    <br />
                </asp:Panel>

            </div>
            <asp:Panel ID="reporting_panel" runat="server" Style="display: none">
                <div class="panel-heading">
                    <div style="color: white; font-weight: bold; font-size: 15px;">Approvals Required</div>
                </div>
                <asp:Panel ID="panel1" runat="server" CssClass="grid-view">
                    <asp:GridView ID="GridView1" class="table" runat="server" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" ForeColor="#333333" Font-Size="X-Small" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnPreRender="GridView1_files_PreRender">
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
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" CausesValidation="false" ShowEditButton="true" />
                            <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary" CausesValidation="false" ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <div class="container-fluid">
                <asp:Panel ID="reason_panel" runat="server" Visible="false">
                    <div class="panel panel-primary" style="background-color: #f3f1fe; border-radius: 10px; border: 1px solid white">
                        <br />
                        <div class="panel-heading">
                            <div style="color: white; font-weight: bold; font-size: 15px;">Reason for Updation</div>
                        </div>
                        <div class="panel-body container">
                            <div class="row">
                                <asp:ListBox ID="lbx_reason_updation" ReadOnly="true" CssClass="form-control" runat="server" Rows="9" Width="100%"></asp:ListBox>
                                <asp:TextBox ID="txt_reason_updation" runat="server" TextMode="MultiLine" Columns="50" Rows="4" CssClass="form-control" placeholder="Enter Reason for Updation"></asp:TextBox>

                            </div>
                        </div>
                        <br />
                    </div>
                </asp:Panel>
            </div>
            <div class="panel-body" style= "border-bottom-left-radius:2px; border-bottom-right-radius:2px ; background-color: white";>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-fluid" style="background: #f3f1fe; border-radius: 10px; border: 1px solid white">
                            <br />
                            <div class="row">
                                <div class="col-sm-10 col-xs-12">
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12" style="width: 12%">
                                           <b> Company Code </b> 

                                    <asp:TextBox ID="txt_companycode" runat="server" onkeypress="return AllowAlphabet_Number(event);"
                                        class="form-control" ReadOnly="true" xMaxLength="10"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <b>Registered Company Name:</b><span class="text-red">*</span>

                                            <asp:TextBox
                                                ID="txt_companyname" runat="server" Width="100%" class="form-control"
                                                MaxLength="100" onkeypress="return AllowAlphabet_address(event);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            <b>Registered Address1 :</b> <span class="text-red">*</span>
                                            <asp:TextBox ID="txt_companyaddress1" runat="server" onkeypress="return AllowAlphabet_address(event);"
                                                class="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Registered Address2 :</b>

                                    <asp:TextBox
                                        ID="txt_companyaddress2" runat="server" onKeyPress="return AllowAlphabet_address(event)"
                                        class="form-control" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Employee Series :</b>
                                    <span class="text-red">*</span>
                                            <asp:TextBox ID="txtempseriesinit" runat="server" class="form-control"
                                                Width="100%" AutoPostBack="true" OnTextChanged="txtempseriesinit_TextChanged" MaxLength="1" onkeypress="return AllowAlphabet(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                           <b> Registered State :</b>
                                        <span class="text-red">*</span>
                                            <asp:DropDownList ID="ddl_state" runat="server" DataTextField="STATE_NAME"
                                                DataValueField="STATE_NAME" class=" form-control" onchange="location_hidden();"
                                                OnSelectedIndexChanged="get_city_list" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span>
                                                <asp:Label ID="lblstate" runat="server" Font-Size="Small"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Registered City :</b>
                                         <span class="text-red">*</span>
                                            <asp:DropDownList ID="txt_companycity" runat="server"
                                                class=" form-control" onkeypress="return AllowAlphabet(event)"
                                                MaxLength="20">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Registered Pin Code :</b>
                                        <span class="text-red">*</span>
                                            <asp:TextBox ID="txt_pin" runat="server" Width="100%" class="form-control" MaxLength="6" onkeypress="return isNumber(event)" onchange="return pincode_validation();"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Admin User ID :</b>
                                    <span class="text-red">*</span>
                                            <asp:TextBox ID="txt_email_id" runat="server" Width="100%" onkeypress="return AllowAlphabet_address(event)"
                                                class="form-control" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <b>Admin Password :</b>
                                    <span class="text-red">*</span>
                                            <asp:TextBox ID="txt_email_pass" runat="server" class="form-control" TextMode="Password" Width="100%" MaxLength="15" onkeypress="return AllowAlphabet_address1(event)"></asp:TextBox>
                                        </div>
                                          </div>
                                    </br>
                                        <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            <b>File No. :</b><span class="text-red">*</span>
                                            <asp:TextBox ID="txt_file_no" runat="server" class="form-control" Width="100%" MaxLength="50" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                        </div>
                                     <div class=" col-sm-2  col-xs-12">
                                     <b>PayPro No. : </b><span class="text-red">*</span>
                                    <asp:DropDownList ID="ddl_paypro_no" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Disable">Disable</asp:ListItem>
                                        <asp:ListItem Value="Enable">Enable</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                    </div>
                                 
                                </div>
                                <div class="col-sm-2 col-xs-12" style="margin-left: -50px;">

                                    <table class="table table-striped" style="border:double;">
                                        <td>
                                            <asp:Image ID="Image4" runat="server" meta:resourcekey="Image4Resource1" OnClick="image_click"
                                                Width="92px" Height="92px" ImageUrl="~/Images/logo.png" /><br />
                                           <b> Company Logo :</b><br />
                                            <asp:FileUpload ID="Header_photo_upload" runat="server" meta:resourcekey="photo_uploadResource1"
                                                CssClass="text_box" />
                                            <b style="color: #f00;">Note :</b> <b>Only JPG and PNG</b>
                                            <br />
                                           <b> files will be uploaded.</b>

                                        </td>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div id="tabs" style="background: #f3f1fe;">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                        <li class="active"><a data-toggle="tab" href="#home"><b>Files</b></a></li>
                        <li><a data-toggle="tab" href="#menu1" runat="server"><b>ESIC</b></a></li>
                        <li><a  data-toggle="tab" href="#menu10" runat="server"><b>LWF</b></a></li>
                        <li><a data-toggle="tab" href="#menu2"><b>PF</b></a></li>
                        <li><a data-toggle="tab" href="#menu3"><b>Bank Details</b></a></li>
                        <li><a data-toggle="tab" href="#menu4"><b>Offices</b></a></li>
                        <li><a data-toggle="tab" href="#menu5"><b>Info</b></a></li>
                        <li><a data-toggle="tab" href="#item7"><b>GST Info</b></a></li>
                        <li><a data-toggle="tab" href="#item8"><b>SAC Code Details</b></a></li>
                        <li><a data-toggle="tab" href="#item9"><b>Reminder Email Id's</b></a></li>
                         <li><a data-toggle="tab" href="#item11"><b>Mini Bank Other</b></a></li>

                    </ul>

                    <div id="home">
                        <br />
                        <div class="row" id="files_upload" runat="server">
                            <div class="col-sm-2 col-xs-12">
                                <b>Description :</b>
                                      <asp:TextBox ID="txt_document1" runat="server" class="form-control" onkeypress="return  AllowAlphabet_Number_slash(event,this)" MaxLength="200"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <span class="text_margin">File to Upload :</span>

                                <asp:FileUpload ID="document1_file" runat="server" meta:resourcekey="photo_uploadResource1" />
                                <b style="color: #f00; text-align: center">Note :</b> <span style="font-size: 8px; font-weight: bold;">Only JPG, PNG and PDF files will be uploaded.</span>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <b>Start Date :</b>
                                         <asp:TextBox ID="txt_from_date" runat="server" class="form-control date-picker1" Width="100%" Style="display: inline"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                               <b> End Date :</b>
                                        
                                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control date-picker2 text_box" Style="display: inline"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12" style="margin-top: 2em">

                                <asp:Button ID="btn_upload" runat="server" class="btn btn-primary" OnClientClick="return save_validate();" OnClick="btn_upload_Click" Text="Upload" />
                            </div>
                        </div>

                        <br />
                        <br />

                        <br />
                        <br />
                        <br />
                        <br />

                        <div class="container">
                            <asp:Panel runat="server" CssClass="grid-view" Style="overflow-x: hidden;">
                                <asp:GridView ID="grd_company_files" class="table" runat="server" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDeleting="grd_company_files_RowDeleting" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" OnPreRender="grd_company_files_PreRender">
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
                                        <asp:BoundField DataField="Id" HeaderText="ID" />
                                        <asp:BoundField DataField="description" HeaderText="Description"
                                            SortExpression="description" />

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


                    </div>
                    <div id="menu1">
                        <br />
                        <asp:UpdatePanel ID="esic_update" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-3 col-xs-12">
                                        <b>ESIC State:</b>
                                              <asp:DropDownList ID="ddl_esic_state" runat="server" DataTextField="STATE_NAME" Width="100%"
                                                  DataValueField="STATE_NAME" class=" form-control" onchange="location_hidden();">
                                              </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-3 col-xs-12">
                                        <b>ESIC Office Address :</b>
                                                <asp:TextBox ID="txt_esic_address" runat="server"
                                                    class="form-control" MaxLength="250" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-xs-12">
                                       <b> ESIC Registration Code :</b>
                                                <asp:TextBox ID="txt_esicregistrationcode" runat="server"
                                                    class="form-control" MaxLength="30" onkeypress="return  AllowAlphabet_Number_slash(event,this)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return save_validate1();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>

                                </div>

                                <br />

                                <div class="container">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="gv_itemslist_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_itemslist_PreRender">
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
                                                        <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
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

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                     <div id="menu10">
                         <br />
                         <br />
                         <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                       <b> LWF State:</b>
                                              <asp:DropDownList ID="ddl_lwf_state" runat="server" DataTextField="STATE_NAME" Width="100%"
                                                  DataValueField="STATE_NAME" class=" form-control" >
                                              </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <b>LWF Registration No :</b>
                                                <asp:TextBox ID="txt_lwf_reg" runat="server"
                                                    class="form-control" MaxLength="40" onkeypress="return isNumber(event);" ></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  LWF Registration Date :</b>
                                                <asp:TextBox ID="txt_lwf_date" runat="server" class="form-control date-picker4" Width="100%"></asp:TextBox>
                           
                                    </div>
                              <div class="col-sm-2 col-xs-12">
                                      <b> LWF State Office Address  :</b>
                                                <asp:TextBox ID="txt_office" runat="server" class="form-control text_box"  onKeyPress="return AllowAlphabet_address(event)" TextMode="MultiLine" Width="100%"></asp:TextBox>
                           
                                    </div>
                                    <div class="col-sm-3 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnk_lwf" runat="server"  OnClick="lnk_lwf_Click" OnClientClick="return lwf_validation(); ">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>

                                </div>
                         <br /><br />
                          <div class="container">
                                    <asp:Panel ID="Panel11" runat="server">
                                        <asp:GridView ID="gv_lwf" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_lwf_PreRender">
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
                                                        <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click1" ><img alt="" height="15"   src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Field1" HeaderText="LWF STATE" SortExpression="Field1" />
                                                <asp:BoundField DataField="Field2" HeaderText="LWF NUMBER" SortExpression="Field2" />
                                                <asp:BoundField DataField="Field3" HeaderText="LWF DATE" SortExpression="Field3" />
                                                <asp:BoundField DataField="Field4" HeaderText="LWF STATE OFFICE ADDRESS" SortExpression="Field4" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                     </div>
                    <div id="menu2">
                        <br />
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                               <b> PF Registration Code :</b>
                                           
                                                <asp:TextBox ID="txt_pfregistrationcode" runat="server" class="form-control text_box"
                                                    MaxLength="20" onkeypress="return  AllowAlphabet_Number_slash(event,this)"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                                <b>PF Registration Office Address:</b>

                                                <asp:TextBox ID="txt_pfregistrationoffice" runat="server"
                                                    class="form-control text_box" onKeyPress="return AllowAlphabet_address(event)"
                                                    MaxLength="20"></asp:TextBox>
                            </div>
                        </div>

                        <br />
                    </div>
                    <div id="menu3">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-3 col-xs-12">
                                        <b></bv>Bank Name:</b>
                                             <asp:TextBox ID="txt_bank_name" runat="server" class="form-control text_box" MaxLength="50" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-3 col-xs-12">
                                        <b>Bank Account No. :</b>
                                                <asp:TextBox ID="txt_account_no" runat="server" class="form-control text_box" MaxLength="30" onKeyPress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-xs-12">
                                        <b>IFSC Code :</b>
                                                <asp:TextBox ID="txt_ifsc_code" runat="server" class="form-control text_box" MaxLength="11" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
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
                                    <asp:Panel ID="Panel6" runat="server"> 
                                        <asp:GridView ID="grd_bank_details" class="table" runat="server" BackColor="White" 
                                            BorderColor="#CCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
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
                    <div id="menu4">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Office Type:</b>
                                             <asp:TextBox ID="txt_office_type" runat="server" class="form-control text_box" MaxLength="30" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>

                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <b>Office Address :</b>
                                                <asp:TextBox ID="txt_office_address" runat="server" class="form-control text_box" MaxLength="250" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Office Contact No. :</b>
                                                <asp:TextBox ID="txt_office_contact" runat="server" class="form-control text_box" MaxLength="10" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <b>Rent Agreement Start Date :</b>
                                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control date-picker1 text_box" placeholder="Start Date :"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2 col-xs-12">
                                        <b>Rent Agreement End Date :</b>
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control date-picker2 text_box" placeholder="End Date :"></asp:TextBox>


                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnk_branch_details" runat="server" OnClick="lnk_branch_details_Click" OnClientClick="return save_validate3();">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>

                                </div>

                                <br />

                                <div class="container">
                                    <asp:Panel ID="Panel7" runat="server">
                                        <asp:GridView ID="grd_branch" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCC" BorderStyle="none" BorderWidth="1px" CellPadding="3"
                                            OnRowDataBound="grd_branch_RowDataBound" AutoGenerateColumns="False" Width="100%" OnPreRender="grd_branch_PreRender">
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
                                                        <asp:LinkButton ID="lnk_remove_branch" runat="server" CausesValidation="false" OnClick="lnk_remove_branch_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Field1" HeaderText="Office Type" SortExpression="Field1" />
                                                <asp:BoundField DataField="Field2" HeaderText="Office Address" SortExpression="Field2" />
                                                <asp:BoundField DataField="Field3" HeaderText="Office Contact" SortExpression="Field3" />
                                                <asp:BoundField DataField="Field4" HeaderText="Rent Start Date" SortExpression="Field4" />
                                                <asp:BoundField DataField="Field5" HeaderText="Rent End Date" SortExpression="Field5" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="menu5">
                        <br />
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <b>Company Website :</b>

                                                <asp:TextBox ID="txt_website" runat="server" class="form-control text_box"
                                                    onkeypress="return website(event,this)" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> Company Contact No.:</b>
                                <asp:TextBox ID="txt_contact_no" runat="server" class="form-control text_box"
                                    onkeypress="return isNumber(event,this)" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                                <b>Company CIN Number :</b>

                                                <asp:TextBox ID="txt_cin_no" runat="server" class="form-control text_box"
                                                    onkeypress="return AllowAlphabet_Number_slash(event,this)" MaxLength="22"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> Company PAN Number :</b>

                                                <asp:TextBox ID="txt_companypannumber" runat="server" class="form-control text_box"
                                                    onkeypress="return AllowAlphabet_Number_slash(event,this)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <br />
                            <div class="col-sm-3 col-xs-12">
                                <b>Company TAN Number :</b>

                                                <asp:TextBox ID="txt_companytannumber" runat="server" class="form-control text_box"
                                                    onkeypress="return AllowAlphabet_Number_slash(event)" MaxLength="12"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-xs-12">
                               <b> GST No :</b>
                                                <asp:TextBox ID="txtservicetaxregno" runat="server" onkeypress="allowAlphaNumericSpace(event)" class="form-control text_box" MaxLength="16"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div id="item7">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="row">

                                    <div class="col-sm-2 col-xs-12">
                                       <b> GST State :</b>
                                        <asp:DropDownList ID="ddl_gst_state" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> GST Address :</b>
                        <asp:TextBox ID="txt_gst_addr" runat="server" MaxLength="200" class="form-control" onKeyPress="return AllowAlphabet_address(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> GST IN :</b>
                        <asp:TextBox ID="txt_gst_no" runat="server" MaxLength="30" class="form-control" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:LinkButton ID="lnk_add_gst" runat="server" OnClick="lnk_add_gst_Click" OnClientClick="return Req_gstvalidation();" AutoPostBack="true">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>

                                    </div>
                                </div>
                                <br />

                                <div class="container" style="width: 56%">
                                    <asp:Panel ID="Panel8" runat="server" >

                                        <asp:GridView ID="gv_statewise_gst" class="table" runat="server" BackColor="White" 
                                            BorderColor="#CCCCC" BorderWidth="1px" CellPadding="1"
                                            OnRowDataBound="gv_statewise_gst_RowDataBound" AllowPaging="true"
                                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_statewise_gst_PreRender">

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
                                                        <asp:LinkButton ID="lnkbtn_gst_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_gst_removeitem_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
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
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_gstin" runat="server" ReadOnly="True" Style="text-align: left" Text='<%# Eval("Field2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="item8">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="row">


                                    <div class="col-sm-2 col-xs-12">
                                        <b>Housekeeping   SAC Code :</b>
                        <asp:TextBox ID="txt_sac_housekeeping" runat="server" MaxLength="10" class="form-control" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                       <b> Security SAC Code :</b>
                        <asp:TextBox ID="txt_sac_security" runat="server" MaxLength="10" class="form-control" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                    </div>




                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                      <div id="item9">
                        <br />

                            <asp:Panel ID="Panel23" runat="server" CssClass="grid-view" ScrollBars="Auto">

                                                    <asp:GridView ID="email_reminder_grid" class="table" runat="server" BackColor="White"
                                                        BorderColor="#CCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                       
                                                        AutoGenerateColumns="False" >

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
                                                                    <asp:TextBox ID="txt_to"  runat="server" Style="text-align: left" class="form-control" Text='<%# Eval("field9")%>'  onkeypress="return email(event)"  onchange="validateEmail(this);" Width="150px"></asp:TextBox>
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

                    <div id="item11">
                        <div class="row">
                            <div class="col-sm-2 col-xs-12">
                                <b>Description :</b>
                                      <asp:TextBox ID="txt_bb" runat="server" class="form-control" MaxLength="200" ></asp:TextBox>
                            </div>
                             <div class="col-sm-3 col-xs-12">
                                        <br />
                                        <asp:LinkButton ID="lnk_button" runat="server"  onkeypress="return  AllowAlphabet_Number_slash(event,this)" OnClick="lnk_button_Click">
                                      <img alt="Add Item" src="Images/add_icon.png"  />
                                        </asp:LinkButton>
                                    </div>
                            </div>
                        <br />
                        <br />
                         <div class="container">
                                    <asp:Panel ID="Panel4" runat="server" >

                                        <asp:GridView ID="gv_data" class="table" runat="server" BackColor="White" 
                                            BorderColor="#CCCCC" BorderWidth="1px" CellPadding="1"
                                            AllowPaging="true"
                                            AutoGenerateColumns="False" Width="100%" OnPreRender="gv_data_PreRender" OnRowDataBound="gv_data_RowDataBound" >

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
                                                        <asp:LinkButton ID="lnkbtn_removeitem_minibank" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_minibank_Click" ><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="COMP_CODE" HeaderText="CODE"
                                SortExpression="COMP_CODE" /> 
                                                <asp:BoundField DataField="field1" HeaderText="field1"
                                SortExpression="field1" /> 
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                    </div>
                        </div>   
 
                    <br />
                    <div class="row text-center">
                        <asp:Button ID="btn_approval" runat="server" class="btn btn-primary" OnClick="btn_approval_Click" Text=" Approve " />
                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary" OnClick="btn_add_Click" OnClientClick="return Req_validation();" Text="Save" />
                        <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" OnClientClick="return req_vali();" OnClick="btn_edit_Click" Text=" Update " />
                        <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" OnClick="btn_delete_Click" Text=" Delete " OnClientClick="return R_validation();" />
                        <asp:Button ID="btn_Cancel" runat="server" class="btn btn-primary" Text=" Clear " OnClick="btn_Cancel_Click" />
                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" Text="Close" OnClick="btnclose_Click" />
                    </div>
                    <br />

                    
                
                
                <br />

               <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:black; font-size: small;" ><b>All Company Master Details:</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
                --%>
                <%--<asp:Panel ID="Panel4" runat="server">--%>
                <div class="container-fluid" style="background: #f3f1fe; padding:10px; border-radius: 10px; border: 1px solid white">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:GridView ID="companyGridView" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="False" CellPadding="1" Font-Size="X-Small" OnPreRender="companyGridView_PreRender"
                        ForeColor="#333333" OnRowDataBound="companyGridView_RowDataBound"
                        OnSelectedIndexChanged="companyGridView_SelectedIndexChanged">

                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />

                        <Columns>
                            <asp:BoundField DataField="COMP_CODE" HeaderText="CODE"
                                SortExpression="COMP_CODE" />
                            <asp:BoundField DataField="COMPANY_NAME" HeaderText="NAME"
                                SortExpression="COMPANY_NAME" />
                            <asp:BoundField DataField="CITY" HeaderText="CITY" SortExpression="CITY" />
                            <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
                            <asp:BoundField DataField="PF_REG_NO" HeaderText="PF REGISTRATION NO"
                                SortExpression="PF_REG_NO" />
                            <asp:BoundField DataField="PF_REG_OFFICE" HeaderText="PF REGISTRATION OFFICE"
                                SortExpression="PF_REG_OFFICE" />
                            <%--<asp:BoundField DataField="ESIC_REG_NO" HeaderText="ESIC REGISTRATION NO"
                                SortExpression="ESIC_REG_NO" />--%>
                            <asp:BoundField DataField="COMPANY_PAN_NO" HeaderText="PAN NO"
                                SortExpression="COMPANY_PAN_NO" />
                            <asp:BoundField DataField="COMPANY_TAN_NO" HeaderText="TAN NO"
                                SortExpression="COMPANY_TAN_NO" />
                            <asp:BoundField DataField="SERVICE_TAX_REG_NO" HeaderText="GST REGISTRATION NO"
                                SortExpression="SERVICE_TAX_REG_NO" />
                            <asp:BoundField DataField="EMP_SERIES_INIT" HeaderText="EMPLOYEE SERIES INIT"
                                SortExpression="EMP_SERIES_INIT" />
                            <asp:BoundField DataField="COMPANY_WEBSITE" HeaderText="COMPANY WEBSITE"
                                SortExpression="COMPANY_WEBSITE" />
                            <asp:BoundField DataField="COMPANY_CONTACT_NO" HeaderText="COMPANY CONTACT NO"
                                SortExpression="COMPANY_CONTACT_NO" />
                            <asp:BoundField DataField="COMPANY_CIN_NO" HeaderText="company CIN NO"
                                SortExpression="COMPANY_CIN_NO" />
                        </Columns>

                    </asp:GridView>
                </asp:Panel>
                    <%--</asp:Panel>--%>
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
        function pageLoad() {
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                onSelect: function (selected) {
                    $(".date-picker2").datepicker("option", "minDate", selected)
                }
            });

            $(".date-picker2").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });
            $(".date-picker4").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
              //  minDate: 0,
                onSelect: function (selected) {
                    $(".date-picker1").datepicker("option", "maxDate", selected)
                }
            });

            $(".date-picker4").attr("readonly", "true");

            $(".date-picker1").attr("readonly", "true");

            $(".date-picker2").attr("readonly", "true");

            var table = $('#<%=gv_lwf.ClientID%>').DataTable({
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
               .appendTo('#<%=gv_lwf.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=companyGridView.ClientID%>').DataTable({
                scrollY: "220px",
                scrollX: true,
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
               .appendTo('#<%=companyGridView.ClientID%>_wrapper .col-sm-6:eq(0)');

            $.fn.dataTable.ext.errMode = 'none';

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

             var table = $('#<%=grd_branch.ClientID%>').DataTable({
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
               .appendTo('#<%=grd_branch.ClientID%>_wrapper .col-sm-6:eq(0)');

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
            var table = $('#<%=gv_data.ClientID%>').DataTable({
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
             .appendTo('#<%=gv_data.ClientID%>_wrapper .col-sm-6:eq(0)');


             $('#<%=btn_Cancel.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $('#<%=ddl_state.ClientID%>').change(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

            $('#<%=companyGridView.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });

            location_hidden();


        }
        function callfnc() {
            document.getElementById('<%= Button8.ClientID %>').click();
        }


        function save_validate() {

            var document1 = document.getElementById('<%=txt_document1.ClientID %>');
           var t_fromdate = document.getElementById('<%=txt_from_date.ClientID %>');
           var t_todate = document.getElementById('<%=txt_to_date.ClientID %>');
           var filePath = document.getElementById('<%=document1_file.ClientID %>');

           if (document1.value == "") {
               alert("Please enter document name");
               document1.focus();
               return false;
           }
           if (filePath.value == "") {
               alert("Please Upload Document");
               filePath.focus();
               return false;
           }

           //Start Date

           if (t_fromdate.value == "") {
               alert("Please Select from Date");
               t_fromdate.focus();
               return false;
           }

           // End Date

           if (t_todate.value == "") {
               alert("Please Select to Date");
               t_todate.focus();
               return false;
           }

           $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
           return true;
       }


        function lwf_validation() {


            var txt_office = document.getElementById('<%=txt_office.ClientID %>');
            if (txt_office.value == "") {
                alert("Please Enter LWF State Office Address.");
                txt_office.focus();
                return false;
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }




       function save_validate1() {

           var document1 = document.getElementById('<%=ddl_esic_state.ClientID %>');
            var SelectedText2 = document1.options[document1.selectedIndex].text;
            var t_esicaddress = document.getElementById('<%=txt_esic_address.ClientID %>');
            var t_regiscode = document.getElementById('<%=txt_esicregistrationcode.ClientID %>');

            if (SelectedText2 == "Select") {
                alert("Please Select State  !!!");
                document1.focus();
                return false;
            }
            if (t_esicaddress.value == "") {
                alert("Please Enter ESIC Address.");
                t_esicaddress.focus();
                return false;
            }
            if (t_regiscode.value == "") {
                alert("Please Enter ESIC Registration Code.");
                t_regiscode.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function save_validate2() {
            var t_bankname = document.getElementById('<%=txt_bank_name.ClientID %>');
            var t_accountno = document.getElementById('<%=txt_account_no.ClientID %>');
            var t_ifsc = document.getElementById('<%=txt_ifsc_code.ClientID %>');

            if (t_bankname.value == "") {
                alert("Please Enter Bank Name.");
                t_bankname.focus();
                return false;
            }
            if (t_accountno.value == "") {
                alert("Please Enter Bank Account Number.");
                t_accountno.focus();
                return false;
            }
            if (t_ifsc.value == "") {
                alert("Please Enter Bank IFSC Code.");
                t_ifsc.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function save_validate3() {

            var office_type = document.getElementById('<%=txt_office_type.ClientID %>');

            var office_address = document.getElementById('<%=txt_office_address.ClientID %>');
            var office_contact = document.getElementById('<%=txt_office_contact.ClientID %>');
            var txt_start_date = document.getElementById('<%=txt_start_date.ClientID %>');
            var txt_end_date = document.getElementById('<%=txt_end_date.ClientID %>');

            if (office_type.value == "") {
                alert("Please Enter Office Type  !!!");
                office_type.focus();
                return false;
            }
            if (office_address.value == "") {
                alert("Please Enter Office Address.");
                office_address.focus();
                return false;
            }


            if (office_contact.value == "") {
                alert("Please Enter Office Contact Number.");
                office_contact.focus();
                return false;
            }
            if (txt_start_date.value == "") {
                alert("Please Select Start Date.");
                txt_start_date.focus();
                return false;
            }
            if (txt_end_date.value == "") {
                alert("Please Select End Date.");
                txt_end_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }



        $(document).ready(InIEvent);
    </script>
    <style>
        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .padding_margin {
            margin: 5px 5px 5px 5px;
            padding: 5px 5px 5px 5px;
        }

        .text_box {
            margin-top: 7px;
        }

        .text_box_head {
            margin-top: 7px;
            text-transform: uppercase;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 600px;
            overflow-x: auto;
            overflow-y: auto;
            font-family: Verdana;
        }

        .grid {
            max-height: 150px;
            overflow-x: hidden;
            overflow-y: auto;
            font-family: Verdana;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .row {
            margin: 0px;
        }

        .text-red {
            color: #f00;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }

        .text_margin {
            margin-right: 9em;
        }



        html input[type="button"], input[type="reset"], input[type="submit"] {
            cursor: pointer;
            font-size: 10px;
            -webkit-appearance: button;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            var evt = null;
            isNumber(evt);
            //// discount();
            //Req_validation();

        });

        function Req_validation() {
            //alert("hello");
            var t_companycode = document.getElementById('<%=txt_companycode.ClientID %>');
            var t_companyname = document.getElementById('<%=txt_companyname.ClientID %>');
            var t_companyaddress1 = document.getElementById('<%=txt_companyaddress1.ClientID %>');
            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText2 = t_state.options[t_state.selectedIndex].text;
            var t_companycity = document.getElementById('<%=txt_companycity.ClientID %>');
            //  var txt_pin = document.getElementById('<%=txt_pin.ClientID %>');
            var t_empseriesinit = document.getElementById('<%=txtempseriesinit.ClientID %>');


            var file_no = document.getElementById('<%=txt_file_no.ClientID %>');
            var txt_pin = document.getElementById('<%=txt_pin.ClientID %>');

            // Company Code

            //if (t_companycode.value == "") {
            //    alert("Please Enter Company Code");
            //    t_companycode.focus();
            //    return false;
            //}
            // Company Name

            if (t_companyname.value == "") {
                alert("Please Enter The Company Name");
                t_companyname.focus();
                return false;
            }
            //Company Address 1 

            if (t_companyaddress1.value == "") {
                alert("Please Enter Company Address 1");
                t_companyaddress1.focus();
                return false;
            }

            //Employee Series Init

            if (t_empseriesinit.value == "") {
                alert("Please Enter Employee Series");
                t_empseriesinit.focus();
                return false;
            }

            //City
            if (SelectedText2 == "Select") {
                alert("Please Select State  !!!");
                t_state.focus();
                return false;
            }

            if (t_companycity.value == "") {
                alert("Please Enter City");
                t_companycity.focus();
                return false;
            }
            // State:

            if (txt_pin.value.length != 6) {
                alert("Please Ente Pin Number Minimum 6 Digits");
                txt_pin.focus();
                return false;
            }


            if (file_no.value == "") {
                alert("Please Enter The File Number");
                file_no.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;


        }
        function req_vali() {

            var t_companycode = document.getElementById('<%=txt_companycode.ClientID %>');
            var t_companyname = document.getElementById('<%=txt_companyname.ClientID %>');
            var t_companyaddress1 = document.getElementById('<%=txt_companyaddress1.ClientID %>');
            var t_state = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText2 = t_state.options[t_state.selectedIndex].text;
            var t_companycity = document.getElementById('<%=txt_companycity.ClientID %>');
            //  var txt_pin = document.getElementById('<%=txt_pin.ClientID %>');
            var t_empseriesinit = document.getElementById('<%=txtempseriesinit.ClientID %>');

            var txt_email_id = document.getElementById('<%=txt_email_id.ClientID %>');
            var txt_email_pass = document.getElementById('<%=txt_email_pass.ClientID %>');

            var file_no = document.getElementById('<%=txt_file_no.ClientID %>');
            var txt_pin = document.getElementById('<%=txt_pin.ClientID %>');

            // Company Code

            //if (t_companycode.value == "") {
            //    alert("Please Enter Company Code");
            //    t_companycode.focus();
            //    return false;
            //}
            // Company Name

            if (t_companyname.value == "") {
                alert("Please Enter The Company Name");
                t_companyname.focus();
                return false;
            }
            //Company Address 1 

            if (t_companyaddress1.value == "") {
                alert("Please Enter Company Address 1");
                t_companyaddress1.focus();
                return false;
            }

            //Employee Series Init

            if (t_empseriesinit.value == "") {
                alert("Please Enter Employee Series");
                t_empseriesinit.focus();
                return false;
            }

            //City
            if (SelectedText2 == "Select") {
                alert("Please Select State  !!!");
                t_state.focus();
                return false;
            }

            if (t_companycity.value == "") {
                alert("Please Enter City");
                t_companycity.focus();
                return false;
            }
            // State:

            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digits");
                txt_pin.focus();
                return false;
            }
            if (txt_email_id.value == "") {
                alert("Please Enter Admin User Id");
                txt_email_id.focus();
                return false;
            }
            if (txt_email_pass.value == "") {
                alert("Please Enter Admin Password");
                txt_email_pass.focus();
                return false;
            }

            if (file_no.value == "") {
                alert("Please Enter The File Number");
                file_no.focus();
                return false;
            }
            var reason_update = document.getElementById('<%=txt_reason_updation.ClientID %>');
            if (!reason_update.disabled) {
                if (reason_update.value == "") {
                    alert("Please Specify Reason For Updation !!!");
                    reason_update.focus();
                    return false;
                }
            }


            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }


        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || ((keyEntry != '38') && (keyEntry != '42') && (keyEntry != '35') && (keyEntry != '64') && (keyEntry != '36') && (keyEntry != '37')) || (keyEntry == '38'))

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
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || ((keyEntry != '38') && (keyEntry != '42') && (keyEntry != '35') && (keyEntry != '64') && (keyEntry != '36') && (keyEntry != '37')) || (keyEntry == '38'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }

            }
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

        function website(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '47'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }

            }
        }

        function AllowAlphabet_Number_slash(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '47') || (keyEntry == '45'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }

            }
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
        function AllowAlphabet_address1(e) {
            if (null != e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                    (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92') || (keyEntry == '64') || (keyEntry == '35') || (keyEntry == '42'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

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

        function isNumber_dot(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 46)) {

                    return false;

                }

            }
            return true;
        }
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8'))
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
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9') || (keyEntry == '64') || (keyEntry == '46'))

                    return true;
                else {
                    return false;
                }
            }
        }

        $(function () {



            $('body').on('keyup', '.maskedExt', function () {
                var num = $(this).attr("maskedFormat").toString().split(',');
                var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                if (!regex.test(this.value)) {
                    this.value = this.value.substring(0, this.value.length - 1);
                }
            });


        });

        $(document).ready(function () {

            $(".js-example-basic-single").select2();

        });

        function location_hidden() {

            var location = document.getElementById('<%=ddl_state.ClientID %>');
            var SelectedText = location.options[location.selectedIndex].text;

            if (SelectedText == "Select") {

                $(".js-example-basic-single").select2();
            }

            else {

                $(".js-example-basic-single").select2();

            }

        }


        function openWindow() {
            window.open("html/CompanyMaster.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function Req_gstvalidation() {
            var ddl_gst_state = document.getElementById('<%=ddl_gst_state.ClientID %>');
            var Selected_ddl_gst_state = ddl_gst_state.options[ddl_gst_state.selectedIndex].text;

            var txt_gst_addr = document.getElementById('<%=txt_gst_addr.ClientID %>');
            var txt_gst_no = document.getElementById('<%=txt_gst_no.ClientID %>');

            if (Selected_ddl_gst_state == "Select") {
                alert("Please Select GST State");
                ddl_gst_state.focus();
                return false;
            }
            if (txt_gst_addr.value == "") {
                alert("Please Enter GST Address");
                txt_gst_addr.focus();
                return false;
            }
            if (txt_gst_no.value == "") {
                alert("Please Enter GST Number");
                txt_gst_no.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
    </script>
    <script>
        function pincode_validation() {
            var txt_pin = document.getElementById('<%=txt_pin.ClientID %>');

            if (txt_pin.value.length != 6) {
                alert("Please Enter Pin Number Minimum 6 Digit");
                aField = document.getElementById('<%=txt_pin.ClientID %>');
                setTimeout("aField.focus()", 50);
                return false;
            }
        }
        function R_validation() {
            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else { return false; }
        }
        $(document).ready(function () {
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });
        });
        function Rq_validation() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
        function lwf_validation()
        {
            var ddl_lwf_state = document.getElementById('<%=ddl_lwf_state.ClientID %>');
            var Selected_ddl_lwf_state = ddl_lwf_state.options[ddl_lwf_state.selectedIndex].text;

            if (Selected_ddl_lwf_state == "Select")
            {
                alert("Please Select LWF State");
                ddl_lwf_state.focus();
                return false;
            }

            var txt_lwf_reg = document.getElementById('<%=txt_lwf_reg.ClientID %>');

            if (txt_lwf_reg.value == "") {
                alert("Please Enter LWF Registration No");
                txt_lwf_reg.focus();
                return false;
            }
            var txt_lwf_date = document.getElementById('<%=txt_lwf_date.ClientID %>');

            if (txt_lwf_date.value == "") {
                alert("Please Enter LWF Registration Date");
                txt_lwf_date.focus();
                return false;
            }

            var txt_office = document.getElementById('<%=txt_office.ClientID %>');
            if (txt_office.value == "") {
                alert("Please Enter LWF State Office Address.");
                txt_office.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true
        }
    </script>
</asp:Content>
