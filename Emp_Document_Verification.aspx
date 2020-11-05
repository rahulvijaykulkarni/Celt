<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emp_Document_Verification.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="Emp_Document_Verification" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Emp_Document_Verification</title>
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
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />

   <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",

                buttons: {
                    //'Approve': function () {
                    //     $(this).dialog('close');
                    //},
                    //'Reject': function () {
                    //    var person = prompt("Reason For Rejection!", "");
                    //    if (person != null) {
                    //        alert(person);
                    //    }

                    //    $(this).dialog('close');
                    //},

                    'Close': function () {
                        $(this).dialog('close');
                    }

                }
            });
            // for search grild view v
            $(document).ready(function () {

                $(document).on("Keyup", function () {
                    SearchGrid('<%=txt_search.ClientID%>', '<%=gv_emp_d_varification.ClientID%>');
                 });

                 $("[id*=Camera_image]").click(function () {
                     $('#dialog').html('');
                     $('#dialog').append($(this).clone().width(470).height(400));
                     $('#dialog').dialog('open');
                     //height:200;
                     //width: 200;
                 });

             });
             //vikas delet for des
             $(document).ready(function () {


                 table.buttons().container()
                    .appendTo('#<%=gv_emp_d_varification.ClientID%>_wrapper .col-sm-6:eq(0)');
             });


         });
         //vikas aprovel validation
         function R_validation() {

             var r = confirm("Are you Sure You Want to Approve Record");
             if (r == true) {



             }
             else {
                 alert("Record not Available");
             }
             return r;
         }
         //vikas  validation for reject button


         function Req_validation() {
             var text_doc_comment = document.getElementById('<%=text_doc_comment.ClientID %>');


           if (text_doc_comment.value == "") {

               alert("Please Enter Reason !!");
               text_doc_comment.focus();
               return false;
           }
           var r = confirm("Are you Sure You Want to reject Record");
           if (r == true) {



           }
           else {
               alert("Record not Available");
           }
           return r;
       }
       // function SearchGrid(txtSearch, grd) vikas 
       {
           if ($("[id *=" + txtSearch + " ]").val() != "") {
               $("[id *=" + grd + " ]").children
               ('tbody').children('tr').each(function () {
                   $(this).show();
               });
               $("[id *=" + grd + " ]").children
               ('tbody').children('tr').each(function () {
                   var match = false;
                   $(this).children('td').each(function () {
                       if ($(this).text().toUpperCase().indexOf($("[id *=" +
                   txtSearch + " ]").val().toUpperCase()) > -1) {
                           match = true;
                           return false;
                       }
                   });
                   if (match) {
                       $(this).show();
                       $(this).children('th').show();
                   }
                   else {
                       $(this).hide();
                       $(this).children('th').show();
                   }
               });


               $("[id *=" + grd + " ]").children('tbody').
                       children('tr').each(function (index) {
                           if (index == 0)
                               $(this).show();
                       });
           }
           else {
               $("[id *=" + grd + " ]").children('tbody').
                       children('tr').each(function () {
                           $(this).show();
                       });
           }
       }
       function Search_Gridview(strKey) {
           var strData = strKey.value.toLowerCase().split(" ");
           var tblData = document.getElementById("<%=gv_emp_d_varification.ClientID %>");
             var rowData;
             for (var i = 1; i < tblData.rows.length; i++) {
                 rowData = tblData.rows[i].innerHTML;
                 var styleDisplay = 'none';
                 for (var j = 0; j < strData.length; j++) {
                     if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                         styleDisplay = '';
                     else {
                         styleDisplay = 'none';
                         break;
                     }
                 }
                 tblData.rows[i].style.display = styleDisplay;
             }
         }

        function openWindow() {
            window.open("html/Emp_Document_Verification.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }


    </script>

    <style>
        .dataTables_filter {
            text-align: right;
        }

        #ctl00_cph_righrbody_gv_emp_d_varification_paginate {
            text-align: right;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            width: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>




        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Employee Document Verification</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
           <%-- <div class="panel-heading">

                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Employee Document Verification Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div class="col-sm-3 col-xs-12">
                       <b> Detail:</b>
                                        <span class="text-red" style="color: red">*</span>
                        <br />
                      
                        <asp:DropDownList ID="ddl_adhar_detail" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_adhardetails_Click" AutoPostBack="true" MaxLength="30">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="Document">Document Verification</asp:ListItem>
                            <asp:ListItem Value="Adhar">Adhar Card Details</asp:ListItem>


                        </asp:DropDownList>

                    </div>
                </div>
                <br />
                    </div>
                <asp:Panel ID="docu_reject" runat="server" CssClass="grid-view">

                    <div class="row">

                        <div class="col-sm-2 col-xs-12 text-left">
                            <span id="comment1">Comment :</span>
                            <asp:TextBox ID="text_doc_comment" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="col-sm-3 col-xs-12 text-left" style="padding-top: 1%">
                            <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text=" Reject " DeleteText="Reject" OnClientClick="return Req_validation(event);" OnClick="Button1_Click" />
                        </div>
                        <div class="col-sm-2 col-xs-12 text-left">
                            <%--<span>id :</span>--%>
                            <asp:TextBox ID="text_doc_id" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>


                    </div>
                </asp:Panel>
                <br />

                <div class="row">
                    <div id="dialog">
                    </div>
                    <%--  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel4" TargetControlID="Button3"
                                CancelControlID="Button4" BackgroundCssClass="Background">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel4" runat="server" CssClass="Popup" Style="display: none">
                                <iframe style="width: 1000px; height: 500px; background-color: #fff;" id="Iframe1" src="p_reject_imaes.aspx" runat="server">

                                </iframe>
                                <div class="row text-center">
                                    <asp:Button ID="Button4" CssClass="btn btn-danger" OnClientClick="callfnc()" runat="server" Text="Close" />
                                </div>
                                   <asp:Button ID="Button3" runat="server" CssClass="hidden" Text="Add New Travelling Plan" />

                                <br />

                            </asp:Panel>--%>
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                    <br />
                    <div class="container" style="width: 100%">
                        <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:10px; margin-top:10px">
                        <asp:Panel ID="Panel2" runat="server" class="grid-view">
                            <br />
                            
                            <%-- chane for search box v--%>
                            <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Search :
                                <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                            </div>
                            <br />

                            <asp:GridView ID="gv_emp_d_varification" class="table" runat="server" Font-Size="X-Small"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" DataKeyNames="id" OnRowDataBound="GradeGridView_RowDataBound"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowEditing="gv_emp_d_varification_RowEditing" OnRowDeleting="gv_emp_d_varification_RowDeleting" OnPreRender="gv_emp_d_varification_PreRender" Width="100%">
                                <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                               <%-- <RowStyle BackColor="#EFF3FB" />--%>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMP CODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EMP_CODE" runat="server" Text='<%# Eval("emp_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMP NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EMP_NAME" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_client_name" runat="server" Text='<%# Eval("Client_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_unit_name" runat="server" Text='<%# Eval("Unit_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Working City">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_city_name" runat="server" Text='<%# Eval("Working_city") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IMAGE NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_IMAGE_NAME" runat="server" Text='<%# Eval("image_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMMENTS">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_IMAGE_PATH" runat="server" Text='<%# Eval("comments") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_IMAGE" runat="server" Text='<%# Eval("image_path") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VIEW">
                                        <ItemTemplate>
                                            <asp:Image ID="Camera_image" runat="server" Height="50" Width="50" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Status" HeaderText="STATUS"
                                        SortExpression="Status" />

                                    <%--<asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary"  
                                                        ShowDeleteButton="true" EditText="Approved" DeleteText="Reject" ShowEditButton="true"/>--%>

                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>

                                            <%-- <asp:LinkButton  ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" OnClientClick="return R_validation();" OnClick="gv_emp_d_varification_RowEditing"></asp:LinkButton>   --%>
                                            <asp:LinkButton ID="lnkbtn_edititem" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Approved" Width="50%" OnClientClick="return R_validation();" OnClick="lnkbtn_edititem_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton2" runat="server" ControlStyle-CssClass="btn btn-primary" Text="Reject" OnClick="LinkButton2_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </asp:Panel>
                       
                    </div>

                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
                <br />
                <br />
                    
                <asp:Panel ID="Panel_adhar" runat="server" class="grid-view">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; 
                    border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:10px; margin-top:10px; margin-right:20px; margin-left:20px">
                    <br />
                    <h2 style="text-align: center"><b>Adhar Card Details</b></h2>
                    <br />

                    <asp:GridView ID="gv_adnarcardscanning" class="table" runat="server" Font-Size="X-Small"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" DataKeyNames="id"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowEditing="gv_emp_adhar_RowEditing" OnRowDeleting="gv_emp_adhar_RowDeleting" Width="100%">
                        <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                       <%-- <RowStyle BackColor="#EFF3FB" />--%>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMP CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EMP_CODE" runat="server" Text='<%# Eval("emp_code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMP NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EMP_NAME" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UDI">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_adharnum" runat="server" Text='<%# Eval("field1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_adharname" runat="server" Text='<%# Eval("field2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_gender" runat="server" Text='<%# Eval("field3") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Village Tahasil">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("field4") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_IMAGE_PATH" runat="server" Text='<%# Eval("field5") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_IMAGE" runat="server" Text='<%# Eval("field6") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Post Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_IMAGE" runat="server" Text='<%# Eval("field7") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_IMAGE" runat="server" Text='<%# Eval("field8") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-primary"  
                                                        ShowDeleteButton="true" EditText="Approved" DeleteText="Reject" ShowEditButton="true" />--%>
                        </Columns>
                    </asp:GridView>
                        </div>
                </asp:Panel>
                        

            </div>
        </asp:Panel>
     
       
        <div class="row text-center">
            <asp:Button ID="btn_close" runat="server" CssClass="btn btn-danger" Text="Close" OnClick="btn_close_click" />

        </div>
        <br />
    </div>
</asp:Content>
