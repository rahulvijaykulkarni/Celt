<%@ Page Title="Operation Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Operation_management.aspx.cs" Inherits="Operation_management" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Operation Management</title>
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
    <script src="js/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <script src="datatable/vfs_fonts.js"></script>

    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>
    <script src="datatable/pdfmake.min.js"></script>


    <style type="text/css">
        .tab-section {
            background-color: #fff;
        }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }


        .grid-view {
            height: auto;
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
            width: 100%;
        }

        button, input, optgroup, select, textarea {
            margin: 0 0 0 0px;
            color: inherit;
        }

        body {
            font-size: 14px;
            font-weight: lighter;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #ccc;
            -webkit-box-shadow: 3px 3px 5px 6px #ccc;
            box-shadow: 3px 3px 5px 6px #ccc;
            padding: 20px;
            padding-right: 25px;
            font-size: 10px;
            font-weight: lighter;
            font-family: Verdana;
        }

        .table td {
            padding: 10px;
            width: 200px;
        }
    </style>



    <script type="text/javascript">

        function pageLoad() {
           

            $('#<%=btn_clear.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=btn_submit.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });




            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd-mm-yy',
                minDate: 0,
                yearRange: '1950',
                //onClose: function (dateText, inst) {
                //    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                //}

            });
            $(".date-picker1").attr("readonly", "true");


            $(".js-example-basic-single").select2();


            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=Travelling_Gridview.ClientID%>').DataTable({
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
               .appendTo('#<%=Travelling_Gridview.ClientID%>_wrapper .col-sm-6:eq(0)');

          
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=  Field_ofc_history_gv.ClientID%>').DataTable({
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
                .appendTo('#<%=  Field_ofc_history_gv.ClientID%>_wrapper .col-sm-6:eq(0)');



        }
        $(document).ready(function () {

            var event = null;
            isNumber(event);
            isNumber(event)
            var e = null;
            AllowAlphabet1(e);
            AllowAlphabet11(e);
        });

        function AllowAlphabet11(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }


        function isNumber(event) {
            if (null != event) {
                event = (event) ? event : window.event;

                var charCode = (event.which) ? event.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                    return false;
                }

            }
            return true;
        }

        function isNumbertotal(event) {
            if (null != event) {
                event = (event) ? event : window.event;

                var charCode = (event.which) ? event.which : event.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet3(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31') || (keyEntry < '8'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function isNumberfax(event) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 32 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46 || charCode == 45 || charCode == 40 || charCode == 41) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

        function AllowAlphabet(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry < '31'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }


        function AllowAlphabet1(e) {
            if (null != e) {

                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry < '31'))

                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        //function Req_validation1() {

        //    if (SelectedTextsalu == "Select Salutation") {
        //        alert("Please Select salutation !!!");
        //        lst_simu.focus();
        //        return false;
        //    }
        //    if (txtfirstname.value == "") {
        //        alert("Please Enter First Name   !!!");
        //        txtfirstname.focus();
        //        return false;
        //    }
        //    //if (txtlastname.value == "") {
        //    //    alert("Please Enter Last Name   !!!");
        //    //    txtlastname.focus();
        //    //    return false;
        //    //}

        //    //if (txteaddress.value == "") {
        //    //    errmsg += "Please Enter Person Email Address 1\n";
        //    //    //alert("Please Enter Project Contact Person Name 2");
        //    //    txteaddress.focus();
        //    //    //return false;
        //    //}

        //    //else if (!email.test(txteaddress.value)) {
        //    //    errmsg1 += "Please Enter  valid email address\n";
        //    //    // alert('Please provide a valid email address');
        //    //    txteaddress.focus();
        //    //    //    return false;
        //    //}


        //    //if (errmsg != "") {
        //    //    alert(errmsg);
        //    //    return false;
        //    //}
        //    //if (errmsg1 != "") {
        //    //    alert(errmsg1);
        //    //    return false;
        //    //}
        //    //if (txtworkphonno.value == "") {
        //    //    alert("Please Enter Phone Number   !!!");
        //    //    txtworkphonno.focus();
        //    //    return false;
        //    //}
        //    if (txtmobileno.value == "") {
        //        alert("Please Enter Mobile Number   !!!");
        //        txtmobileno.focus();
        //        return false;
        //    }
        //    //if (txtdesignation1.value == "") {
        //    //    alert("Please Enter Designation  !!!");
        //    //    txtdesignation1.focus();
        //    //    return false;
        //    //}
        //    //if (txtdept.value == "") {
        //    //    alert("Please Enter Department  !!!");
        //    //    txtdept.focus();
        //    //    return false;
        //    //}


        //    return true;

        //}





        function openWindow() {
            window.open("html/Operation_management.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
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
        function req_Validation() {
            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
           var Selected_ddl_client = ddl_client.options[ddl_client.selectedIndex].text;

           var txt_schedule_date = document.getElementById('<%=txt_schedule_date.ClientID %>');

           if (Selected_ddl_client == "Select") {
               alert("Please Select Client Name");
               ddl_client.focus();
               return false;
           }
           if (Selected_ddl_client != "Select") {
               var ddl_state = document.getElementById('<%=ddl_state.ClientID %>');
               var Selected_ddl_state = ddl_state.options[ddl_state.selectedIndex].text;
               if (Selected_ddl_state == "Select") {
                   alert("Please Select State");
                   ddl_state.focus();
                   return false;
               }
           }
           if (txt_schedule_date.value == "") {
               alert("Please Select Date");
               txt_schedule_date.focus();
               return false;
           }

       }
       function R_validation() {
           var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
            var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;

            if (Selected_ddl_employee == "Select") {
                alert("Please Select Employee Name");
                ddl_employee.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function unblock() {
            $.unblockUI();
        }
        function Validation() {
            var ddl_employee = document.getElementById('<%=ddl_employee.ClientID %>');
            var Selected_ddl_employee = ddl_employee.options[ddl_employee.selectedIndex].text;

            if (Selected_ddl_employee == "Select") {
                alert("Please Select Field Officer");
                ddl_employee.focus();
                return false;
            }

            var ddl_state_name = document.getElementById('<%=ddl_state_name.ClientID %>');
            var Selected_ddl_state_name = ddl_state_name.options[ddl_state_name.selectedIndex].text;

            if (Selected_ddl_state_name == "Select") {
                alert("Please Select State");
                ddl_state_name.focus();
                return false;
            }
            var ddl_client_name = document.getElementById('<%=ddl_client_name.ClientID %>');
            var Selected_ddl_client_name = ddl_client_name.options[ddl_client_name.selectedIndex].text;

            if (Selected_ddl_client_name == "Select") {
                alert("Please Select Client Name");
                ddl_client_name.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }

        function req_Validation() {
            var ddl_field_officer = document.getElementById('<%=ddl_field_officer.ClientID %>');
            var Selected_ddl_field_officer = ddl_field_officer.options[ddl_field_officer.selectedIndex].text;

            if (Selected_ddl_field_officer == "Select") {
                alert("Please Select Field Officer");
                ddl_field_officer.focus();
                return false;
            }

            var ddl_client = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_ddl_client_name = ddl_client.options[ddl_client.selectedIndex].text;

            if (Selected_ddl_client_name == "Select") {
                alert("Please Select Client Name");
                ddl_client.focus();
                return false;
            }
            var ddl_state_name = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_ddl_state_name = ddl_state_name.options[ddl_state_name.selectedIndex].text;

            if (Selected_ddl_state_name == "Select") {
                alert("Please Select State");
                ddl_state_name.focus();
                return false;
            }
            var ddl_branch = document.getElementById('<%=ddl_branch.ClientID %>');
            var Selected_ddl_branch = ddl_branch.options[ddl_branch.selectedIndex].text;

            if (Selected_ddl_branch == "Select") {
                alert("Please Select Branch");
                ddl_branch.focus();
                return false;
            }
            var txt_schedule_date = document.getElementById('<%=txt_schedule_date.ClientID %>');


            if (txt_schedule_date.value == "") {
                alert("Please Select Date");
                txt_schedule_date.focus();
                return false;
            }

        }
        function R_validation() {

            var r = confirm("Are you Sure You Want to Delete Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            else {
                alert("Record not Available");
            }
            return r;
        }
    </script>
    <style>
        .row {
            margin-right: -15px;
            margin-left: -15px;
        }
    </style>
    <script>
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
            var tblData = document.getElementById("<%=gv_operation.ClientID %>");
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
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>

        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-12">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>OPERATION MANAGEMENT</b> </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-12 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>


            </div>
            <br />
           <%-- <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-4"></div>
                    <div class="col-sm-9">
                        <div style="text-align: left; color:white; font-size: small;"><b>Operation Management Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>


            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                    <ul class="nav nav-tabs" style="background: #f3f1fe; ">
                        <li id="tabactive1" class="active"><a id="A1" data-toggle="tab" href="#menu1" runat="server"><b>Assign Field Officer</b></a></li>
                        <li id="tabactive2"><a id="A2" data-toggle="tab" href="#menu2" runat="server"><b>Travelling Schedule</b></a></li>
                    </ul>
                    <br />
                    <br />
                    <div class="tab-content">
                        <div id="menu1" class="tab-pane fade in active">

                            <div class="row text-center">
                                <h4><b>ASSIGN FIELD OFFICER</b></h4>
                            </div>
                           <br />
                            <br />
                            <div class="row">

                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Select Field Officer :</b> 
                                        <asp:DropDownList ID="ddl_employee" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                        </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Mobile No :</b>
                                        <asp:TextBox ID="txt_mobile_no" MaxLength="10" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                  <b>  State : </b>
                                        <asp:DropDownList ID="ddl_state_name" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>

                                </div>
                                <div class="col-sm-2 col-xs-12">
                                   <b> Client Name : </b>
                                        <asp:DropDownList ID="ddl_client_name" runat="server" Font-Size="X-Small" class="form-control" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged1" AutoPostBack="true">
                                        </asp:DropDownList>

                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-sm-2 col-xs-12"></div>
                                <div class="col-sm-4 col-xs-12 text-left">
                                   <b> Branch Having Field Officer :</b>
                                        <asp:ListBox ID="ddl_unitcode1" runat="server" class="form-control" SelectionMode="Multiple" Height="150px"></asp:ListBox>
                                </div>
                                <div class="col-sm-4 col-xs-12 text-left">
                                   <b> Branch Not Having Field Officer :</b>
                                        <asp:ListBox ID="ddl_unitcode_without1" runat="server" class="form-control" SelectionMode="Multiple" Height="150px"></asp:ListBox>
                                </div>

                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row text-center">

                                <asp:Button ID="btn_save_op" runat="server" class="btn btn-primary" Text="Save" OnClick="btnadd_Click" OnClientClick=" return Validation()" />
                                <asp:Button ID="btn_delete" runat="server" class="btn btn-primary" Text="Remove" OnClientClick=" return R_validation()" OnClick="btndelete_Click" />
                                <asp:Button ID="btn_clear" runat="server" class="btn btn-primary" Text="Clear" OnClick="btnClear_Click" />



                            </div>

                            
                            <br />

                            <br />

                            <asp:Panel ID="Panel2" CssClass="grid-view" runat="server">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12"></div>
                                    <div class="col-sm-2 col-xs-12">
                                      <b>  Search :</b>
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                    </div>
                                </div>
                                <br />

                                <asp:GridView ID="gv_operation" class="table" runat="server" BackColor="White" OnRowDataBound="gv_operation_OnRowDataBound" OnSelectedIndexChanged="gv_operation_OnSelectedIndexChanged"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
                                    AutoGenerateColumns="False" OnPreRender="gv_operation_PreRender1">
                                    <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                   <%-- <RowStyle BackColor="#EFF3FB" />--%>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <%--<Columns>--%>

                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Id_CODE" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UNIT CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_UNIT_CODE" runat="server" Text='<%# Eval("UNIT_CODE") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:BoundField DataField="COMP_CODE" HeaderText="COMP CODE" SortExpression="COMP_CODE" />
                                        <asp:BoundField DataField="field_officer_name" HeaderText="FIELD OFFICER NAME" SortExpression="field_officer_name" />
                                        <asp:BoundField DataField="MOBILE_NO" HeaderText="MOBILE NO" SortExpression="MOBILE_NO" />
                                        <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                        <asp:BoundField DataField="STATE" HeaderText="STATE NAME" SortExpression="STATE" />
                                        <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                        <%--  <asp:BoundField DataField="CLIENT" HeaderText="CLIENT CODE" SortExpression="CLIENT" />
                                                        <asp:BoundField DataField="UNIT" HeaderText="UNIT CODE" SortExpression="UNIT" />--%>
                                    </Columns>



                                </asp:GridView>

                            </asp:Panel>
                        </div>
                        <div id="menu2" class="tab-pane fade">
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">

                                        <h4 class="text-center"><b>TRAVELLING SCHEDULE</b></h4>
                                        <hr />
                                        <table id="maintable" class="table table-responsive main_table" border="1" runat="server">

                                            <tr style="color: #fff; background-color: #337ab7;">
                                                <th class="text-center">SCHEDULE TYPE</th>
                                                <th class="text-center">FIELD OFFICER</th>
                                                <th class="text-center">CLIENT</th>
                                                <th class="text-center">STATE</th>
                                                <th class="text-center">BRANCH</th>
                                                <th class="text-center">DATE</th>
                                                <th class="text-center">START TIME</th>
                                                <th class="text-center">END TIME</th>
                                                <th class="text-center"></th>
                                            </tr>
                                            <tr>

                                                 
                                 <td>
                                  <asp:DropDownList ID="ddl_schedule_type" runat="server" CssClass="form-control text_box"  Width="100%" onchange="dispatch_bill_validation1();" AutoPostBack ="true">
                                     
                                      <asp:ListItem Value="0">Normal Schedule</asp:ListItem>
                                      <asp:ListItem Value="1">Emergency Schedule</asp:ListItem>
                                      
                                      
                                  </asp:DropDownList>
                           </td>

                                                <td>
                                                    <asp:DropDownList ID="ddl_field_officer" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="ddl_field_officer_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                    </asp:DropDownList>

                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddl_client" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_state" runat="server" Font-Size="X-Small" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                    </asp:DropDownList>

                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddl_branch" runat="server" Font-Size="X-Small" class="form-control">
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_schedule_date" MaxLength="10" runat="server" class="form-control date-picker1" onkeypress="return isNumber(event)"></asp:TextBox>

                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddl_start_time" runat="server" Font-Size="X-Small" class="form-control">
                                                        <asp:ListItem Value="Flexible" Text="Flexible"></asp:ListItem>
                                                        <asp:ListItem Value="0:00" Text="0:00"></asp:ListItem>
                                                        <asp:ListItem Value="0:30" Text="0:30"></asp:ListItem>
                                                        <asp:ListItem Value="1:00" Text="1:00"></asp:ListItem>
                                                        <asp:ListItem Value="1:30" Text="1:30"></asp:ListItem>
                                                        <asp:ListItem Value="2:00" Text="2:00"></asp:ListItem>
                                                        <asp:ListItem Value="2:30" Text="2:30"></asp:ListItem>
                                                        <asp:ListItem Value="3:00" Text="3:00"></asp:ListItem>
                                                        <asp:ListItem Value="3:30" Text="3:30"></asp:ListItem>
                                                        <asp:ListItem Value="4:00" Text="4:00"></asp:ListItem>
                                                        <asp:ListItem Value="4:30" Text="4:30"></asp:ListItem>
                                                        <asp:ListItem Value="5:00" Text="5:00"></asp:ListItem>
                                                        <asp:ListItem Value="5:30" Text="5:30"></asp:ListItem>
                                                        <asp:ListItem Value="6:00" Text="6:00"></asp:ListItem>
                                                        <asp:ListItem Value="6:30" Text="6:30"></asp:ListItem>
                                                        <asp:ListItem Value="7:00" Text="7:00"></asp:ListItem>
                                                        <asp:ListItem Value="7:30" Text="7:30"></asp:ListItem>
                                                        <asp:ListItem Value="8:00" Text="8:00"></asp:ListItem>
                                                        <asp:ListItem Value="8:30" Text="8:30"></asp:ListItem>
                                                        <asp:ListItem Value="9:00" Text="9:00"></asp:ListItem>
                                                        <asp:ListItem Value="9:30" Text="9:30"></asp:ListItem>
                                                        <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                                        <asp:ListItem Value="10:30" Text="10:30"></asp:ListItem>
                                                        <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                                        <asp:ListItem Value="11:30" Text="11:30"></asp:ListItem>
                                                        <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                                        <asp:ListItem Value="12:30" Text="12:30"></asp:ListItem>
                                                        <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                                        <asp:ListItem Value="13:30" Text="13:30"></asp:ListItem>
                                                        <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                                        <asp:ListItem Value="14:30" Text="14:30"></asp:ListItem>
                                                        <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                                        <asp:ListItem Value="15:30" Text="15:30"></asp:ListItem>
                                                        <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                                        <asp:ListItem Value="16:30" Text="16:30"></asp:ListItem>
                                                        <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                                        <asp:ListItem Value="17:30" Text="17:30"></asp:ListItem>
                                                        <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                                        <asp:ListItem Value="18:30" Text="18:30"></asp:ListItem>
                                                        <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                                        <asp:ListItem Value="19:30" Text="19:30"></asp:ListItem>
                                                        <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                                        <asp:ListItem Value="20:30" Text="20:30"></asp:ListItem>
                                                        <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                                        <asp:ListItem Value="21:30" Text="21:30"></asp:ListItem>
                                                        <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                                        <asp:ListItem Value="22:30" Text="22:30"></asp:ListItem>
                                                        <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                                        <asp:ListItem Value="23:30" Text="23:30"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddl_end_time" runat="server" Font-Size="X-Small" class="form-control">
                                                        <asp:ListItem Value="Flexible" Text="Flexible"></asp:ListItem>
                                                        <asp:ListItem Value="0:00" Text="0:00"></asp:ListItem>
                                                        <asp:ListItem Value="0:30" Text="0:30"></asp:ListItem>
                                                        <asp:ListItem Value="1:00" Text="1:00"></asp:ListItem>
                                                        <asp:ListItem Value="1:30" Text="1:30"></asp:ListItem>
                                                        <asp:ListItem Value="2:00" Text="2:00"></asp:ListItem>
                                                        <asp:ListItem Value="2:30" Text="2:30"></asp:ListItem>
                                                        <asp:ListItem Value="3:00" Text="3:00"></asp:ListItem>
                                                        <asp:ListItem Value="3:30" Text="3:30"></asp:ListItem>
                                                        <asp:ListItem Value="4:00" Text="4:00"></asp:ListItem>
                                                        <asp:ListItem Value="4:30" Text="4:30"></asp:ListItem>
                                                        <asp:ListItem Value="5:00" Text="5:00"></asp:ListItem>
                                                        <asp:ListItem Value="5:30" Text="5:30"></asp:ListItem>
                                                        <asp:ListItem Value="6:00" Text="6:00"></asp:ListItem>
                                                        <asp:ListItem Value="6:30" Text="6:30"></asp:ListItem>
                                                        <asp:ListItem Value="7:00" Text="7:00"></asp:ListItem>
                                                        <asp:ListItem Value="7:30" Text="7:30"></asp:ListItem>
                                                        <asp:ListItem Value="8:00" Text="8:00"></asp:ListItem>
                                                        <asp:ListItem Value="8:30" Text="8:30"></asp:ListItem>
                                                        <asp:ListItem Value="9:00" Text="9:00"></asp:ListItem>
                                                        <asp:ListItem Value="9:30" Text="9:30"></asp:ListItem>
                                                        <asp:ListItem Value="10:00" Text="10:00"></asp:ListItem>
                                                        <asp:ListItem Value="10:30" Text="10:30"></asp:ListItem>
                                                        <asp:ListItem Value="11:00" Text="11:00"></asp:ListItem>
                                                        <asp:ListItem Value="11:30" Text="11:30"></asp:ListItem>
                                                        <asp:ListItem Value="12:00" Text="12:00"></asp:ListItem>
                                                        <asp:ListItem Value="12:30" Text="12:30"></asp:ListItem>
                                                        <asp:ListItem Value="13:00" Text="13:00"></asp:ListItem>
                                                        <asp:ListItem Value="13:30" Text="13:30"></asp:ListItem>
                                                        <asp:ListItem Value="14:00" Text="14:00"></asp:ListItem>
                                                        <asp:ListItem Value="14:30" Text="14:30"></asp:ListItem>
                                                        <asp:ListItem Value="15:00" Text="15:00"></asp:ListItem>
                                                        <asp:ListItem Value="15:30" Text="15:30"></asp:ListItem>
                                                        <asp:ListItem Value="16:00" Text="16:00"></asp:ListItem>
                                                        <asp:ListItem Value="16:30" Text="16:30"></asp:ListItem>
                                                        <asp:ListItem Value="17:00" Text="17:00"></asp:ListItem>
                                                        <asp:ListItem Value="17:30" Text="17:30"></asp:ListItem>
                                                        <asp:ListItem Value="18:00" Text="18:00"></asp:ListItem>
                                                        <asp:ListItem Value="18:30" Text="18:30"></asp:ListItem>
                                                        <asp:ListItem Value="19:00" Text="19:00"></asp:ListItem>
                                                        <asp:ListItem Value="19:30" Text="19:30"></asp:ListItem>
                                                        <asp:ListItem Value="20:00" Text="20:00"></asp:ListItem>
                                                        <asp:ListItem Value="20:30" Text="20:30"></asp:ListItem>
                                                        <asp:ListItem Value="21:00" Text="21:00"></asp:ListItem>
                                                        <asp:ListItem Value="21:30" Text="21:30"></asp:ListItem>
                                                        <asp:ListItem Value="22:00" Text="22:00"></asp:ListItem>
                                                        <asp:ListItem Value="22:30" Text="22:30"></asp:ListItem>
                                                        <asp:ListItem Value="23:00" Text="23:00"></asp:ListItem>
                                                        <asp:ListItem Value="23:30" Text="23:30"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td>
                                                    <%--   <a style="cursor:pointer;"><div class="delete">- Remove</div></a>--%>
                                                    <asp:LinkButton ID="lnkbtn_addmoreitem" runat="server" CausesValidation="False" Width="85%"
                                                        OnClick="lnkbtn_addmoreitem_Click" OnClientClick="return req_Validation();"><img alt="Add Item"  
                                                        src="Images/add_icon.png"  /></asp:LinkButton>

                                                </td>

                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">

                                                <asp:GridView ID="gv_itemslist" class="table" runat="server" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                    OnRowDataBound="gv_itemslist_RowDataBound" OnSelectedIndexChanged="gv_itemslist_SelectedIndexChanged"
                                                    AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">

                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />

                                                    <Columns>
                                                        <%--<Columns>--%>
                                                        <asp:TemplateField>
                                                            <ItemStyle Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClick="lnkbtn_removeitem_Click"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemStyle Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="CLIENT_CODE" HeaderText="CLIENT_CODE" SortExpression="CLIENT_CODE" />
                                                        <asp:BoundField DataField="EMP_CODE" HeaderText="EMP_CODE" SortExpression="EMP_CODE_CODE" />
                                                        <asp:BoundField DataField="UNIT_CODE" HeaderText="UNIT" SortExpression="UNIT_CODE" />
                                                        <asp:BoundField DataField="EMP_NAME" HeaderText="FIELD_OFFICER" SortExpression="EMP_NAME" />
                                                        <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                                        <asp:BoundField DataField="STATE" HeaderText="STATE NAME" SortExpression="STATE" />

                                                        <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                        <asp:BoundField DataField="OPERATION_DATE" HeaderText="DATE" SortExpression="OPERATION_DATE" />
                                                        <asp:BoundField DataField="START_TIME" HeaderText="START TIME" SortExpression="START_TIME" />
                                                        <asp:BoundField DataField="END_TIME" HeaderText="END TIME" SortExpression="END_TIME" />
                                                        <%--  <asp:BoundField DataField="CLIENT" HeaderText="CLIENT CODE" SortExpression="CLIENT" />
                                                        <asp:BoundField DataField="UNIT" HeaderText="UNIT CODE" SortExpression="UNIT" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                         <asp:Panel ID="Panel6" CssClass="grid-view" runat="server">

                                        <asp:GridView ID="Field_ofc_history_gv" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
                                            AutoGenerateColumns="False" OnRowDataBound="Field_ofc_history_gv_RowDataBound" OnPreRender="Field_ofc_history_gv_PreRender" Width="100%">
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                             
                                            
                                                <asp:BoundField DataField="field_officer_name" HeaderText="FIELD OFFICER NAME" SortExpression="field_officer_name" />
                                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                                <asp:BoundField DataField="STATE" HeaderText="STATE NAME" SortExpression="STATE" />
                                                <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                <asp:BoundField DataField="OPERATION_DATE" HeaderText="OPERATION DATE" SortExpression="OPERATION_DATE" />
                                                <asp:BoundField DataField="START_TIME" HeaderText="START TIME " SortExpression="START_TIME" />
                                                <asp:BoundField DataField="END_TIME" HeaderText="END TIME " SortExpression="END_TIME" />
                                                  <asp:BoundField DataField="reject" HeaderText="Reject " SortExpression="reject" />
                                                <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                               <asp:Label ID="Complete" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                    </div>


                                    <br />
                                    <div class="row text-center">
                                        <asp:Button ID="btn_submit" runat="server" class="btn btn-primary" Text="Submit" OnClick="btn_submit_Click" />
                                        <asp:Button ID="btn_send_mail" runat="server" class="btn btn-primary" OnClick="btn_send_mail_Click" Text="Send Mail" TabIndex="2" />
                                       <%-- <asp:Button ID="btn_update" runat="server" class="btn btn-primary" Text="Update" OnClick="btn_update_Click" />--%>
                                        <asp:Button ID="btn_traveling_clear" runat="server" class="btn btn-primary" Text="Clear" OnClick="Travelling_item_clear" />

                                        <%-- <asp:Button ID="btndelete" runat="server" class="btn btn-primary"  Text="Delete" TabIndex="3" OnClientClick="return Re_validation" OnClick="btn_delete_Click" />
                                        --%>
                                        <asp:Button ID="btncancel" runat="server" class="btn btn-primary" Visible="false" OnClick="btncancel_Click" Text="Clear" TabIndex="4" />
                                        <asp:Button ID="btnexporttoexcel" runat="server" Visible="false" class="btn btn-primary" OnClick="btnexporttoexcel_Click" Text="Export To Excel" TabIndex="5" />
                                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" TabIndex="6" />
                                    </div>
                                    <br />
                                    <asp:Panel ID="Panel5" CssClass="grid-view" runat="server">

                                        <asp:GridView ID="Travelling_Gridview" class="table" runat="server" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
                                            AutoGenerateColumns="False" OnRowDataBound="Travelling_Gridview_OnRowDataBound" OnSelectedIndexChanged="Travelling_Gridview_OnSelectedIndexChanged" OnPreRender="Travelling_Gridview_PreRender" Width="100%">
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtn_removetravelling" runat="server" CausesValidation="false" OnClick="lnkbtn_removetravelling_Click" OnClientClick="return R_validation()"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Id_CODE" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="UNIT_CODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_travelling_CODE" runat="server" Text='<%# Eval("UNIT_CODE") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="COMP_CODE" HeaderText="COMP CODE" SortExpression="COMP_CODE" />
                                                <asp:BoundField DataField="field_officer_name" HeaderText="FIELD OFFICER NAME" SortExpression="field_officer_name" />
                                                <asp:BoundField DataField="CLIENT_NAME" HeaderText="CLIENT NAME" SortExpression="CLIENT_NAME" />
                                                <asp:BoundField DataField="STATE" HeaderText="STATE NAME" SortExpression="STATE" />
                                                <asp:BoundField DataField="UNIT_NAME" HeaderText="BRANCH NAME" SortExpression="UNIT_NAME" />
                                                <asp:BoundField DataField="DATE" HeaderText="OPERATION DATE" SortExpression="DATE" />
                                                <asp:BoundField DataField="START_TIME" HeaderText="START TIME " SortExpression="START_TIME" />
                                                <asp:BoundField DataField="END_TIME" HeaderText="END TIME " SortExpression="END_TIME" />
                                                 <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                                                 <asp:BoundField DataField="comment" HeaderText="comment" SortExpression="comment" />
                                                   <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                               <asp:Label ID="reschedule" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                          
                                    <br />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>
</asp:Content>

