<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="Leagal Approval" CodeFile="leagal_approval.aspx.cs" Inherits="leagal_approval" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Leagal/Compliances</title>
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
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>
    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/jszip.min.js"></script>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <script>
        function unblock() { $.unblockUI(); }
        function pageLoad() {
            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_unitcode.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_emp_name.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
        }
        function validation() {

            var txt_reject = document.getElementById('<%=txt_reject.ClientID %>');

            if (txt_reject.value == "") {

                alert("Please Enter Reject Reason !!!");
                txt_reject.focus();
                return false;
            }

            var bool = confirm('Are you sure you want to Reject?');

            if (bool == true) {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                return true;
            }
            else {
                return false;
            }


        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Approve Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            return r;
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
        $(function () {
            $("#dialog").dialog({
                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=ImageButton6]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton5]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton4]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton3]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton2]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton1]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton7]").click(function () {
                //alert("hello");
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(397));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
        });

        function R_validation() {
            var r = confirm("Are you Sure You Want to Approve Record");
            if (r == true) {
                ($.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } }))
                return true;
            }
            else {
                return false;
            }
        }
        function openWindow() {

            window.open("html/leagal_compliance.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');

        }
    </script>
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary" Style="background-color: white;">

            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Leagal/Compliances</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Leagal/Compliances Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div id="dialog"></div>
                    <div class="col-sm-1 col-xs-12"></div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Client Name :</b>
                        <asp:DropDownList runat="server" ID="ddl_client" CssClass="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> State Name :</b>
                        <asp:DropDownList runat="server" ID="ddl_state" CssClass="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Branch Name :</b>
                        <asp:DropDownList runat="server" ID="ddl_unitcode" CssClass="form-control" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                      <b>  Employee Name :</b>
                      <asp:DropDownList runat="server" ID="ddl_emp_name" CssClass="form-control" OnSelectedIndexChanged="ddl_emp_name_SelectedIndexChanged" AutoPostBack="true">
                      </asp:DropDownList>
                    </div>
                    <div class="col-sm-1 col-xs-12"></div>
                    <asp:Panel ID="panel_link" runat="server">
                        <div class="col-sm-3 col-sm-12">
                            <table class="table table-striped" border="1" style="border-color: #c7c5c5">
                                <tr>
                                    <th><a data-toggle="modal" href="#remain_admin"><font color="red"><b><%= rem_emp_count %></b></font>Employee Not Approve By Admin </a></th>
                                </tr>
                                <tr>
                                    <th><a data-toggle="modal" href="#approve_admin"><font color="red"><b><%= appro_emp_count %></b></font>Employee Approve  By Admin </a></th>
                                </tr>
                                <tr>
                                    <th><a data-toggle="modal" href="#approve_legal"><font color="red"><b><%= appro_emp_legal %></b></font>Employee  Approve By Legal </a></th>
                                </tr>
                                <tr>
                                    <th><a data-toggle="modal" href="#reject_leagal"><font color="red"><b><%= reject_emp_legal %></b></font>Employee Reject  By Legal </a></th>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>

                </div>
                <br />
                <asp:Panel ID="panel_data" runat="server">
                    <div class="row">
                        <div class="col-sm-1 col-xs-12"></div>
                        <div class="col-sm-2 col-xs-12">
                            Employee Name :
                        <asp:TextBox runat="server" ID="txt_emp_name" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Father Name :
                        <asp:TextBox runat="server" ID="txt_father_name" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Date Of Birth :
                        <asp:TextBox runat="server" ID="txt_dob" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Date Of Joining  :
                        <asp:TextBox runat="server" ID="txt_doj" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Gender  :
                        <asp:TextBox runat="server" ID="txt_gender" CssClass="form-control"></asp:TextBox>
                        </div>


                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-1 col-xs-12"></div>
                        <div class="col-sm-2 col-xs-12">
                            Adhar Number :
                        <asp:TextBox runat="server" ID="txt_adhar_no" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Bank Name :
                        <asp:TextBox runat="server" ID="txt_bank_name" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Bank Account Number :
                        <asp:TextBox runat="server" ID="txt_acc_no" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Bank IFSC Code :
                        <asp:TextBox runat="server" ID="txt_ifsc_no" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Mobile No  :
                        <asp:TextBox runat="server" ID="txt_mb_no" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-1 col-xs-12"></div>
                        <div class="col-sm-2 col-xs-12">
                            UAN Number :
                        <asp:TextBox runat="server" ID="txt_uan_no" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            ESIC Number :
                        <asp:TextBox runat="server" ID="txt_esic_no" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            PF Number :
                        <asp:TextBox runat="server" ID="txt_pf_no" CssClass="form-control"></asp:TextBox>
                        </div>


                        <div class="col-sm-2 col-xs-12">
                            Designation :
                        <asp:TextBox runat="server" ID="txt_designation" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            Reject Reason :
                        <asp:TextBox runat="server" ID="txt_reject" CssClass="form-control" Height="50px" onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <br />
                    <asp:Panel runat="server" CssClass="panel panel-primary">
                        <div class="container-fluid">
                            <div class="row">
                                <br />
                                <div class="col-sm-1 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Profile Photo(Passport Size) :
                                                <br />
                                    <asp:Label ID="photo" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton1" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />

                                    <asp:LinkButton ID="link_originalphoto" Text="Download" runat="server" CommandArgument="_20" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                                <div class="col-sm-1 col-xs-12">
                                    Adhar Photo :
                                                <br />
                                    <asp:Label ID="Label2" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton2" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_adharphoto" Text="Download" runat="server" CommandArgument="_21" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                                <div class="col-sm-1 col-xs-12">
                                    Bank Passbook:
                                                <br />
                                    <asp:Label ID="Label1" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton3" runat="server" Height="100" Width="100" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_joining" Text="Download" runat="server" CommandArgument="_24" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-sm-1 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    <span style="margin-left: 7em;">Joining Kit :</span>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton4" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_signature" Text="Download" runat="server" CommandArgument="_19" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                                <div class="col-sm-1 col-xs-12">
                                    Address Proof Photo :
                                                <br />
                                    <asp:Label ID="Label4" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton5" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_addr_proof" Text="Download" runat="server" CommandArgument="_23" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                                <div class="col-sm-1 col-xs-12">
                                    Police Varification :
                                                <br />
                                    <asp:Label ID="Label5" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton6" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_police" Text="Download" runat="server" CommandArgument="_22" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>

                                

                            </div>
                            <hr />
                            <div class="row">
                                  <div class="col-sm-2 col-xs-12"></div>
                                 <div class="col-sm-1 col-xs-12">
                                    Pan Card :
                                                <br />
                                    <asp:Label ID="Label6" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                </div>

                                <div class="col-sm-2 col-xs-12">
                                    <asp:Image ID="ImageButton7" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                    <br />
                                    <asp:LinkButton ID="link_pan_card" Text="Download" runat="server" CommandArgument="_22" OnCommand="download_document1" Style="margin-left: 24px"></asp:LinkButton>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="row text-center">
                            <asp:Button runat="server" ID="btn_approve" CssClass="btn btn-primary" Text="Approve" OnClick="btn_approve_Click" OnClientClick="return R_validation();" />
                            <asp:Button runat="server" ID="btn_reject" CssClass="btn btn-primary" Text="Reject" OnClick="btn_reject_Click" OnClientClick="return validation(); " />
                            <asp:Button runat="server" ID="btn_exp_excel" CssClass="btn btn-primary" Text="Export Excel" OnClick="btn_exp_excel_Click" />
                        </div>
                        <br />
                    </asp:Panel>
                    <br />

                </asp:Panel>
                <div class="modal fade" id="remain_admin" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 style="text-align: center">Employee Not Approve By Admin </h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:GridView ID="gv_rem_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
                <div class="modal fade" id="approve_admin" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 style="text-align: center">Employee  Approve By Admin</h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:GridView ID="gv_appro_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
                <div class="modal fade" id="approve_legal" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 style="text-align: center">Employee  Approve By Legal</h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:GridView ID="gv_appro_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
                <div class="modal fade" id="reject_leagal" role="dialog" data-dismiss="modal" href="#lost">

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 style="text-align: center">Employee Reject  By Legal</h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-12" style="padding-left: 1%;">
                                        <asp:GridView ID="gv_reject_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gridService_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                            <Columns>
                                                <asp:BoundField DataField="Client_Name" HeaderText="Client Name" SortExpression="Client_Name" />
                                                <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                                <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name" />
                                                <asp:BoundField DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name" />
                                                <asp:BoundField DataField="reject_reason" HeaderText="Reject Reason" SortExpression="reject_reason" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
