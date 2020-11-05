<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_employee_requirnment.aspx.cs" MasterPageFile="~/MasterPage.master" Title="New Employee Requirement" Inherits="new_employee_requirnment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>New Employee Requirement</title>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="cph_header" runat="server">
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
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>


    <script>
        function unblock() {
            $.unblockUI();
        }

    </script>
    <style>
        .a {
            display: none;
        }

        .b {
            display: none;
        }

        .c {
            display: none;
        }

        .d {
            display: none;
        }
    </style>
    <script type="text/javascript">





        $(document).ready(function () {

            $(document).on("Keyup", function () {
                SearchGrid('<%=txt_search.ClientID%>', '<%=gv_emp_requirement.ClientID%>');
            });

            $("#dialog").dialog({

                autoOpen: false,
                modal: true,
                height: 500,
                width: 500,
                title: "Zoomed Image",
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=Image21]").click(function () {

                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton2]").click(function () {

                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton1]").click(function () {

                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $("[id*=ImageButton3]").click(function () {

                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
            $(".date-picker").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',

            });
            $(".date-picker").attr("readonly", "true");

            $.fn.dataTable.ext.errMode = 'none';




        });

        table.buttons().container()
           .appendTo('#<%=gv_emp_requirement.ClientID%>_wrapper .col-sm-12:eq(0)');


           

       
     
       
    </script>
    <script>
        function pageLoad() {

            $('#<%=gv_emp_requirement.ClientID%> td').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });



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


        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || (keyEntry == '8') || (keyEntry == '9') || (keyEntry == '46'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        function AllowAlphabet_Number(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '32') || (keyEntry == '9'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        function SearchGrid(txtSearch, grd) {
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
            var tblData = document.getElementById("<%=gv_emp_requirement.ClientID %>");
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
        function Req_validation() {


            var ddl_employee_type = document.getElementById('<%=ddl_employee_type.ClientID %>');
            var Selected_ddl_employee_type = ddl_employee_type.options[ddl_employee_type.selectedIndex].text;

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

            var ddl_branch = document.getElementById('<%=ddl_branch.ClientID %>');
            var Selected_ddl_branch = ddl_branch.options[ddl_branch.selectedIndex].text;

            var ddl_grade = document.getElementById('<%=ddl_grade.ClientID %>');
            var Selected_ddl_grade = ddl_grade.options[ddl_grade.selectedIndex].text;

            var ddl_gender = document.getElementById('<%=ddl_gender.ClientID %>');
            var Selected_ddl_gender = ddl_gender.options[ddl_gender.selectedIndex].text;

            var ddl_blood_group = document.getElementById('<%=ddl_blood_group.ClientID %>');
            var Selected_ddl_blood_group = ddl_blood_group.options[ddl_blood_group.selectedIndex].text;

            var ddl_working = document.getElementById('<%=ddl_working.ClientID %>');
            var Selected_ddl_working = ddl_working.options[ddl_working.selectedIndex].text;



            var txt_eecode = document.getElementById('<%=txt_eecode.ClientID %>');
            var txt_suprvisr_name = document.getElementById('<%=txt_suprvisr_name.ClientID %>');
            var txt_emp_name = document.getElementById('<%=txt_emp_name.ClientID %>');
            var txt_f_h_name = document.getElementById('<%=txt_f_h_name.ClientID %>');
            var txt_birth_date = document.getElementById('<%=txt_birth_date.ClientID %>');
            var txt_join_date = document.getElementById('<%=txt_join_date.ClientID %>');
            var txt_mobileno = document.getElementById('<%=txt_mobileno.ClientID %>');
            var txt_emp_address = document.getElementById('<%=txt_emp_address.ClientID %>');
            var tex_bran_na = document.getElementById('<%=tex_bran_na.ClientID %>');
            var txt_acc_no = document.getElementById('<%=txt_acc_no.ClientID %>');
            var txt_ifsc_code = document.getElementById('<%=txt_ifsc_code.ClientID %>');
            var txt_bank_name = document.getElementById('<%=txt_bank_name.ClientID %>');
            var txt_aadhar_no = document.getElementById('<%=txt_aadhar_no.ClientID %>');

            if (Selected_ddl_employee_type == "Select") {
                alert("Please Select Employee Type");
                ddl_employee_type.focus();
                return false;
            }
            if (Selected_ddl_client == "") {
                alert("Please Select Client Name");
                ddl_client.focus();
                return false;
            }
            if (Selected_ddl_branch == "") {
                alert("Please Select Branch Name");
                ddl_branch.focus();
                return false;
            }
            if (txt_eecode.value == "") {
                alert("Please Enter Employee Code");
                txt_eecode.focus();
                return false;
            }
            if (Selected_ddl_grade == "") {
                alert("Please Select Designation");
                ddl_grade.focus();
                return false;
            }

            if (txt_suprvisr_name.value == "") {
                alert("Please Enter Supervisor Name");
                txt_suprvisr_name.focus();
                return false;
            }
            if (txt_emp_name.value == "") {
                alert("Please Enter New Employee Name");
                txt_emp_name.focus();
                return false;
            }
            if (txt_f_h_name.value == "") {
                alert("Please Enter Father/Husband Name");
                txt_f_h_name.focus();
                return false;
            }
            if (Selected_ddl_gender == "Select") {
                alert("Please Select Gender");
                ddl_gender.focus();
                return false;
            }
            if (txt_birth_date.value == "") {
                alert("Please Enter Date Of Birth");
                txt_birth_date.focus();
                return false;
            }
            if (txt_join_date.value == "") {
                alert("Please Enter Date Of Joining");
                txt_join_date.focus();
                return false;
            }
            if (Selected_ddl_blood_group == "Select") {
                alert("Please Select Blood Group");
                ddl_blood_group.focus();
                return false;
            }
            if (Selected_ddl_working == "Select") {
                alert("Please Select Working Hour");
                ddl_working.focus();
                return false;
            }
            if (txt_mobileno.value == "") {
                alert("Please Enter Mobile Number");
                txt_mobileno.focus();
                return false;
            }
            if (txt_emp_address.value == "") {
                alert("Please Enter Employee Address");
                txt_emp_address.focus();
                return false;
            }
            if (tex_bran_na.value == "") {
                alert("Please Enter Branch Name");
                tex_bran_na.focus();
                return false;
            }
            if (txt_acc_no.value == "") {
                alert("Please Enter Bank Account Number");
                txt_acc_no.focus();
                return false;
            }
            if (txt_ifsc_code.value == "") {
                alert("Please Enter Bank IFSC Code");
                txt_ifsc_code.focus();
                return false;
            }
            if (txt_bank_name.value == "") {
                alert("Please Enter Bank Holder Name");
                txt_bank_name.focus();
                return false;
            }
            if (txt_aadhar_no.value == "") {
                alert("Please Enter Aadhar Number");
                txt_aadhar_no.focus();
                return false;
            }
        }
        function openWindow() {
            window.open("html/new_employee_requirnment.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
    </script>
    <style type="text/css">
    
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="cph_righrbody" ID="content3" runat="server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>New Employee Recruitment </b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>New Employee Recruitment Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <%-- div use for dialog popup window--%>
                <div id="dialog"></div>
                <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; border-radius: 10px;margin-bottom:20px;margin-top:20px">
                <br />
                <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                    <div class="row">
                        <div class="col-sm-10 col-xs-12"></div>
                        <div class="col-sm-2 col-xs-12">
                            Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="gv_emp_requirement" class="table" runat="server" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                        OnRowDataBound="GradeGridView_RowDataBound"
                        OnSelectedIndexChanged="gv_emp_requirement_SelectedIndexChanged"
                        AutoGenerateColumns="False" Width="100%" DataKeyNames="Id">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" Width="50" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                       <%-- <RowStyle BackColor="#ffffff" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />--%>
                        <SortedAscendingCellStyle BackColor="#E9E7E2" CssClass="a" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" CssClass="b" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" CssClass="c" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" CssClass="d" />

                        <Columns>
                            <asp:TemplateField HeaderText="Policy_code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_CODE" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Id" HeaderText="Id"
                                SortExpression="Id"  />--%>
                            <asp:BoundField DataField="client_code" HeaderText="Client Name" SortExpression="client_code" />


                            <asp:BoundField DataField="unit_code" HeaderText="Unit Name" SortExpression="unit_code" />
                            <asp:BoundField DataField="supervisor_code" HeaderText="Supervisor Name"
                                ItemStyle-CssClass="u_name" />


                            <asp:BoundField DataField="new_employee_name" HeaderText="New Employee Name"
                                ItemStyle-CssClass="n_e_name" />
                            <asp:BoundField DataField="grade" HeaderText="Designation"
                                ItemStyle-CssClass="g_name" />
                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton Text="Edit" ID="lnkView" CssClass="display" runat="server" OnClick="lnkView_Click1"  OnSelectedIndexChanged="gv_emp_requirement_SelectedIndexChanged1"   />

                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>

                    </asp:GridView>
                    <%--<hr />--%>
                    <asp:Panel runat="server" ID="hide_show_panel">
                       
                        <div class="container" style="width: 100%">
                            <ul class="nav nav-tabs">
                                <li id="tabactive1" class="active"><a id="A1" data-toggle="tab" href="#home" runat="server">Employee Details</a></li>
                                <li id="tabactive2"><a id="A2" data-toggle="tab" href="#menu1" runat="server">Bank Details</a></li>
                                <li id="tabactive10"><a id="A3" data-toggle="tab" href="#menu2" runat="server">Document Details</a></li>
                            </ul>
                            <div class="tab-content">
                                <div id="home" class="tab-pane fade in active">
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12 text-left">
                                            Employee Type :<span style="color: red"> *</span>
                                            <asp:DropDownList ID="ddl_employee_type" runat="server" class="form-control">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="Permanent">Permanent</asp:ListItem>
                                                <asp:ListItem Value="PermanentReliever">Permanent Reliever</asp:ListItem>
                                                <asp:ListItem Value="Reliever">Reliever</asp:ListItem>
                                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                                <asp:ListItem Value="Temporary">Temporary</asp:ListItem>
                                                <asp:ListItem Value="RM">Repair & Maintenance</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-3 col-xs-12">
                                            Client Name :<span style="color: red"> *</span>
                                            <asp:DropDownList ID="ddl_client" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Branch Name :<span style="color: red"> *</span>

                                            <asp:DropDownList ID="ddl_branch" runat="server" class="form-control" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged" AutoPostBack="true" onkeypress="return AllowAlphabet(event)">
                                            </asp:DropDownList>


                                        </div>

                                        <div class="col-sm-3 col-xs-12 text-left">
                                            Employee Code :<span style="color: red"> *</span>
                                            <asp:TextBox ID="txt_eecode" runat="server" class="form-control text_box"
                                                onkeypress="return Alphabet_Number(event,this);" MaxLength="20" meta:resourcekey="txt_eecodeResource1" ReadOnly="true" disabled></asp:TextBox>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12">
                                            Designation:<span style="color: red"> *</span>

                                            <asp:DropDownList ID="ddl_grade" runat="server" class="form-control" onkeypress="return AllowAlphabet(event)">
                                            </asp:DropDownList>


                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Supervisor Name :<span style="color: red"> *</span>

                                            <asp:TextBox runat="server" ID="txt_suprvisr_name" CssClass=" form-control" onkeypress="return AllowAlphabet(event)" />

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            New Employee Name :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_emp_name" CssClass="form-control" onkeypress="return AllowAlphabet(event)" />

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Father/Husband Name :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_f_h_name" CssClass="form-control" onkeypress="return AllowAlphabet(event)" />

                                        </div>


                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12">
                                            Gender :<span style="color: red"> *</span>
                                            <asp:DropDownList ID="ddl_gender" runat="server" class="form-control">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Date Of Birth :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_birth_date" CssClass="form-control date-picker" ReadOnly="true" />

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Date Of Joining :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_join_date" CssClass="form-control date-picker" ReadOnly="true" />

                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Blood Group :<span style="color: red"> *</span>
                                            <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-control">
                                                <asp:ListItem Value="Select" Text="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="O+" Text="Select">O+</asp:ListItem>
                                                <asp:ListItem Value="O-" Text="Select">O-</asp:ListItem>
                                                <asp:ListItem Value="A+" Text="Select">A+</asp:ListItem>
                                                <asp:ListItem Value="A-" Text="Select">A-</asp:ListItem>
                                                <asp:ListItem Value="B+" Text="Select">B+</asp:ListItem>
                                                <asp:ListItem Value="B-" Text="Select">B-</asp:ListItem>
                                                <asp:ListItem Value="AB+" Text="Select">AB+</asp:ListItem>
                                                <asp:ListItem Value="AB-" Text="Select">AB-</asp:ListItem>
                                                <asp:ListItem Value="O-" Text="Select">O-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-3 col-xs-12">
                                            Working Hours :<span style="color: red"> *</span>

                                            <asp:DropDownList ID="ddl_working" runat="server" class="form-control">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Mobile No. :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_mobileno" CssClass="form-control" MaxLength="10" onkeypress="return isNumber(event);" />
                                        </div>
                                        <div class="col-sm-3 col-xs-12">
                                            Employee Address :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_emp_address" CssClass="form-control" TextMode="MultiLine" Rows="4" onkeypress="return AllowAlphabet_Number(event,this);" />
                                        </div>

                                    </div>
                                </div>
                                <div id="menu1" class="tab-pane fade">
                                    <br />

                                    <div class="row">
                                        <div class="col-sm-1 col-xs-12"></div>
                                        <div class="col-sm-2 col-xs-12">
                                            Branch Name :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="tex_bran_na" CssClass="form-control" onkeypress="return AllowAlphabet_Number(event,this);" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Account No. :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_acc_no" CssClass="form-control" onkeypress="return isNumber(event);" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            IFSC Code :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_ifsc_code" CssClass="form-control" onkeypress="return AllowAlphabet_Number(event,this);" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Bank Holder Name :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_bank_name" CssClass="form-control" onkeypress="return AllowAlphabet(event)" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Aadhar No. :<span style="color: red"> *</span>
                                            <asp:TextBox runat="server" ID="txt_aadhar_no" CssClass="form-control" onkeypress="return isNumber(event);" />
                                        </div>
                                    </div>

                                </div>
                                <div id="menu2" class="tab-pane fade">
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-2 col-xs-12">
                                            Aadhar Card :
                                                <br />
                                            <asp:Label ID="l_aadhar_upload" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="aadhar_upload" runat="server" meta:resourcekey="bank_uploadResource1" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Image ID="Image21" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Police Verification :
                                                <br />
                                            <asp:Label ID="Label2" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="policy_document" runat="server" meta:resourcekey="bank_uploadResource1" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Image ID="ImageButton2" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                        </div>


                                    </div>

                                    <br />
                                    <div class="row">

                                        <%-- div use for dialog popup window--%>

                                        <div class="col-sm-2 col-xs-12">
                                            Employee Photo :
                                                <br />
                                            <asp:Label ID="lbl_passportphoto" runat="server" Text="Employee Photo Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="passportphoto" runat="server" meta:resourcekey="photo_uploadResource1" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Image ID="ImageButton1" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            Bank Passbook :
                                                <br />
                                            <asp:Label ID="Label3" runat="server" Text="Employee AADHAR Card Uploaded" ForeColor="#00ff00" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:FileUpload ID="FileUpload2" runat="server" meta:resourcekey="bank_uploadResource1" />
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <asp:Image ID="ImageButton3" runat="server" Width="100px" Height="100px" ImageUrl="~/Images/placeholder.png" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                </div>
                                <div class="row text-center">
                                    <%--   <asp:Button ID="btn_update" runat="server" class="btn btn-primary"  Text="Save "  />
                        <asp:Button ID="Button1" runat="server" class="btn btn-danger"  Text="Close " />--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

               

                </asp:Panel>
        <br />
        </div>
                
                <br />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <div class="row text-center">
                    <asp:Button ID="btn_insert" runat="server" class="btn btn-primary" OnClick="btn_insert_Click"
                        Text="MoveToEmployeeMaster" OnClientClick="return Req_validation();" />
                    <asp:Button ID="btn_close" runat="server" class="btn btn-danger"
                        Text="Close" OnClick="btn_close_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
