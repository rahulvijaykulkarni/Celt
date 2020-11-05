<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="uniform_tracking.aspx.cs" Inherits="uniform_tracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
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
                buttons: [{ text: "Close", click: function () { $(this).dialog("close") } }],
            });
            $("[id*=original_photo]").click(function () {
                $('#dialog').html('');
                $('#dialog').append($(this).clone().width(470).height(400));
                $('#dialog').dialog('open');
                //height:200;
                //width: 200;
            });
        });

        function unblock() {
            $.unblockUI();
        }
        function pageLoad() {
           
            $.fn.dataTable.ext.errMode = 'none';
            var table = $('#<%=grd_document.ClientID%>').DataTable({
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
          .appendTo('#<%=grd_document.ClientID%>_wrapper .col-sm-6:eq(0)');

            $('#<%=ddl_client.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddl_state.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });
            $('#<%=ddlunitselect.ClientID%>').change(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            });





            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm/yy',
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1));
                }


            });

            $(".date-picker").attr("readonly", "true");

            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                yearRange: '1950',
                maxDate: 36,
                onSelect: function (value, ui) {


                }
            }).click(function () {
                $('.ui-datepicker-calendar').show();
            });
            $(".datepicker").attr("readonly", "true");
            var st = $(this).find("input[id*='hidtab']").val();
            if (st == null)
                st = 0;
            $('[id$=tabs]').tabs({ selected: st });


            $(document).ready(function () {
                $(document).on("Keyup", function () {
                    SearchGrid('<%=txt_search.ClientID%>', '<%=gv_checklist_uniform.ClientID%>');
                 });

               
                $('[id*=chk_gv_header]').click(function () {
                    $("[id*='chk_client']").attr('checked', this.checked);
                });

               
            });
           
        }
        // for search grild view v
       
        //function ValidateAll() {
        //    var count = 0;
        //    var count1 = 0;
        //    var count2 = 0;
        //    var count3 = 0;
        //    var count4 = 0;
        //    $('.dummyClass').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count = 1;
        //        }
        //    }, 0);
        //    $('.dummyClass1').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count1 = 1;
        //        }
        //    }, 0);
        //    $('.dummyClass2').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count2 = 1;
        //        }
        //    }, 0);
        //    $('.dummyClass3').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count3 = 1;
        //        }
        //    }, 0);
        //    $('.dummyClass4').each(function (index, item) {
        //        if ($(this).val() != "") {
        //            count4 = 1;
        //        }
        //    }, 0);
        //    if (count == 0)
        //    {
        //        alert("Please Enter Contact Number");
               
        //        return false;
        //    }
            
        //        if (count1 == 0)
        //        {
        //            alert("Please Enter Shoes Size");
        //            return false;
        //        }
            
            
        //        if (count2 == 0) {
        //            alert("Please Enter Uniform Size");
        //            return false;
        //    }
            
        //        if (count3 == 0) {
        //            alert("Please Enter Uniform Sets");
        //            return false;
        //        }
        //        if (count4 == 0) {
        //            alert("Please Enter Apron Sets");
        //            return false;
        //        }
        //    return true;
        //}
    </script>

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .grid-view {
            height: auto;
            max-height: 300px;
            overflow: scroll;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .ui-datepicker-calendar {
            display: none;
        }
        .text_box {
            margin-top:7px;
        }
    </style>
    <script type="text/javascript">

        function Req_validation() {

            var ddl_client_name = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = ddl_client_name.options[ddl_client_name.selectedIndex].text;


            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name.focus();
                return false;
            }

            if (txt_date.value == "") {
                alert("Please Select Month / Year.");
                txt_date.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }
          function Reg_validate_3() {

            var ddl_client_name1 = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client1 = ddl_client_name1.options[ddl_client_name1.selectedIndex].text;


            if (Selected_client1 == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name1.focus();
                return false;
            }
            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please Select to Month / Year.");
                txttodate.focus();
                return false;
            }
            
                var isValid = false;{
                var gridView = document.getElementById('<%= gv_checklist_uniform.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            alert("Please select atleast one Employee");

            return false;
          }
        

        return true;
        

          }

        function resend_btn_validation() {

            var ddl_client_name1 = document.getElementById('<%=ddl_client.ClientID %>');
               var Selected_client1 = ddl_client_name1.options[ddl_client_name1.selectedIndex].text;


               if (Selected_client1 == "Select") {
                   alert("Please Select Client Name ");
                   ddl_client_name1.focus();
                   return false;
               }
               var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please Select to Month / Year.");
                txttodate.focus();
                return false;
            }

            var isValid = false; {
                var gridView = document.getElementById('<%= gv_dublicate_id.ClientID %>');
                    for (var i = 1; i < gridView.rows.length; i++) {
                        var inputs = gridView.rows[i].getElementsByTagName('input');
                        if (inputs != null) {
                            if (inputs[0].type == "checkbox") {
                                if (inputs[0].checked) {
                                    isValid = true;
                                    return true;
                                }
                            }
                        }
                    }
                    alert("Please select atleast one Employee");

                    return false;
                }


                return true;


            }




        function Reg_validate_2() {

            var ddl_client_name1 = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client1 = ddl_client_name1.options[ddl_client_name1.selectedIndex].text;


            if (Selected_client1 == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name1.focus();
                return false;
            }

            if (txt_date.value == "") {
                alert("Please Select to Month / Year.");
                txt_date.focus();
                return false;
            }

            return true;

        }

        function validte_form_D() {

            var ddl_client_name = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = ddl_client_name.options[ddl_client_name.selectedIndex].text;

            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name.focus();
                return false;
            }


            var txt_state = document.getElementById('<%=ddl_state.ClientID %>');
            var Selected_state = txt_state.options[txt_state.selectedIndex].text;



            if (Selected_state == "Select") {
                alert("Please Select State");
                txt_state.focus();
                return false;
            }

            return true;

        }

        function validte_form_B() {

            var ddl_client_name = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = ddl_client_name.options[ddl_client_name.selectedIndex].text;



            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name.focus();
                return false;
            }

            if (txt_from.value == "") {
                alert("Please Select Month / Year.");
                txt_from.focus();
                return false;
            }

            return true;

        }

        function Reg_val_dispatch() {
            var ddl_client_name = document.getElementById('<%=ddl_client.ClientID %>');
            var Selected_client = ddl_client_name.options[ddl_client_name.selectedIndex].text;
            var txt_from = document.getElementById('<%=txt_print_list.ClientID %>');


            if (Selected_client == "Select") {
                alert("Please Select Client Name ");
                ddl_client_name.focus();
                return false;
            }

            var txttodate = document.getElementById('<%=txttodate.ClientID %>');
            if (txttodate.value == "") {
                alert("Please Select to Month / Year.");
                txttodate.focus();
                return false;
            }

            if (txt_from.value == "") {
                alert("Please Select Date.");
                txt_from.focus();
                return false;
            }
            var txt_lot = document.getElementById('<%=txt_lot.ClientID %>');
            if (txt_lot.value == "") {
                alert("Please Enter Lot Number.");
                txt_lot.focus();
                return false;
            }

            return true;

        }

        function openWindow() {
            window.open("html/uniform_tracking.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
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
        function R_validation() {

            var r = confirm("Are you Sure You Want to Reject Record");
            if (r == true) {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            }
            return r;
        }
       
        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gv_checklist_uniform.ClientID %>");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Uniform Tracking</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Uniform Tracking Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>--%>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-left:15px; margin-right:15px; margin-bottom:20px; margin-top:20px">
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                           <b> Client Name:</b>
                        <asp:DropDownList ID="ddl_client" DataValueField="client_code" DataTextField="client_name" OnSelectedIndexChanged="ddl_client_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        </div>
                        <asp:Panel ID="unit_panel" runat="server">
                            <div class="col-sm-2 col-xs-12 ">
                              <b>  Select State :</b>
                            <asp:DropDownList ID="ddl_state" runat="server" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Label ID="Label1" runat="server" Text="Branch Name :"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlunitselect" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlunitselect_SelectedIndexChanged" ></asp:DropDownList>
                            </div>

                            <div class="col-sm-2 col-xs-12 ">
                               <b> Select Designation :</b>
                            <asp:DropDownList ID="ddl_designation" DataValueField="GRADE_CODE" DataTextField="GRADE_DESC" runat="server" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" >
                            </asp:DropDownList>
                            </div>

                            <div class="col-sm-1 col-xs-12">
                                <asp:Label ID="Lbltodate" runat="server" Text="Month/Year :"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" class="form-control date-picker text_box" Width="95%"></asp:TextBox>
                            </div>


                            <asp:Panel ID="panel_link" runat="server">
                                <div class="col-sm-3 col-sm-12">
                                    <table class="table table-striped" border="1" style="border-color: #c7c5c5">

                                        <tr>
                                            <th><a data-toggle="modal" href="#approve_admin"><font color="red"><b><%= appro_emp_count %></b></font>Employee Approve  By Admin </a></th>
                                        </tr>
                                        <tr>
                                            <th><a data-toggle="modal" href="#approve_legal"><font color="red"><b><%= appro_emp_legal %></b></font>Employee  Approve By Legal </a></th>
                                        </tr>
                                        <tr>
                                            <th><a data-toggle="modal" href="#reject_leagal"><font color="red"><b><%= reject_emp_legal %></b></font>Employee Dispatch By Legal </a></th>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <br />
                </div>
                <br />
                <div id="tabs" style="background: #f3f1fe; border: 1px solid #e2e2dd; margin-bottom:25px; margin-top:25px; margin-left:15px; margin-right:15px; border-radius:10px">
                    <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                    <ul>
                         <li id="Li1"><a id="A2" href="#menu5" runat="server"><b>Document Details</b></a></li>
                        <li id="tabactive2"><a id="A1" href="#menu6" runat="server"><b>ID Cards</b></a></li>
                    </ul>
                    <div id="menu5">
                       <asp:Panel ID="Panel4" runat="server" Style="overflow-x:auto;overflow-y:auto;height:300px;">
                            <asp:GridView ID="grd_document" class="table" runat="server" Font-Size="X-Small"
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="id"  OnRowDataBound="grd_document_RowDataBound"  Width="100%" OnPreRender="grd_document_PreRender">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                     <asp:TemplateField HeaderText="No." ItemStyle-Width="65px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_srnumber" runat="server"
                                                Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="ID" HeaderText="ID"/>--%>
                                    <asp:BoundField DataField="EMP_CODE" HeaderText="Employee Code" ItemStyle-Width="109px" />
                                    <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="GRADE_DESC" HeaderText="Designation" SortExpression="GRADE_DESC" ItemStyle-Width="100px" />
                                    <%--<asp:BoundField DataField="EMP_MOBILE_NO" HeaderText="Contact No" SortExpression="EMP_MOBILE_NO" />--%>
                                     <asp:TemplateField HeaderText="Contact No">
                                        <ItemStyle Width="70px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_cont_no" runat="server" onkeypress=" return isNumber(event)" Text='<%# Eval("EMP_MOBILE_NO") %>' class="form-control dummyClass" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="10"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                     <asp:BoundField DataField="Id_card" HeaderText="ID CARD" SortExpression="Id_card" />--%>
<%--                                     <asp:BoundField DataField="Shoes" HeaderText="SHOES" SortExpression="Shoes" />--%>
                                    <asp:TemplateField HeaderText=" Shoes Size">
                                        <ItemStyle Width="55px"  />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_shoes" runat="server" Style="width:55px" Text='<%# Eval("shoes_size") %>' class="form-control dummyClass1" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1" MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                     <asp:BoundField DataField="uniform" HeaderText="UNIFORM" SortExpression="uniform" />--%>
<%--                                    <asp:BoundField DataField="size" HeaderText="Size" SortExpression="size" />--%>
                                    <asp:TemplateField HeaderText="Uniform Size">
                                        <ItemStyle Width="55px"  />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_uniform" runat="server" Style="width:55px" Text='<%# Eval("Uniform_Size") %>' class="form-control dummyClass2" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1" MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="No_of_set" HeaderText="Uniform Sets" SortExpression="Uniform Sets" />--%>
                                    <asp:TemplateField HeaderText="Uniform No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_uniform_set" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("Uniform_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Uniform Set ">
                                        <ItemStyle Width="90px"  />
                                        <ItemTemplate>

                                             <asp:DropDownList ID="ddl_uniform_set" runat="server" Style="width:90px" SelectedValue='<%# Bind("unifrom_set") %>' CssClass="form-control" >
                                           <asp:ListItem Value="0">0</asp:ListItem>
                                           <asp:ListItem Value="1">1</asp:ListItem>
                                           <asp:ListItem Value="2">2</asp:ListItem>
                                          
                                       </asp:DropDownList>
                                         </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Uniform Remaining No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_uniform_set_remainig" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("remaning_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 <%--  for shoes set--%>

                                    <asp:TemplateField HeaderText="Shoes No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_shoes_set" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("Shoes_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Shoes Set ">
                                        <ItemStyle Width="90px"  />
                                        <ItemTemplate>

                                             <asp:DropDownList ID="ddl_shoes_set" runat="server" Style="width:90px" SelectedValue='<%# Bind("shoes_set") %>' CssClass="form-control" >
                                           <asp:ListItem Value="0">0</asp:ListItem>
                                           <asp:ListItem Value="1">1</asp:ListItem>
                                          
                                          
                                       </asp:DropDownList>
                                         </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Shoes Remaining No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_shoes_set_remainig" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("Shoes_remaning_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 <%--   for ID card--%>

                                    <asp:TemplateField HeaderText="Id Card No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_id_card_set" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("id_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="ID CardSet ">
                                        <ItemStyle Width="90px"  />
                                        <ItemTemplate>

                                             <asp:DropDownList ID="ddl_id_card_set" runat="server" Style="width:90px" SelectedValue='<%# Bind("id_card_set") %>' CssClass="form-control" >
                                           <asp:ListItem Value="0">0</asp:ListItem>
                                           <asp:ListItem Value="1">1</asp:ListItem>
                                          
                                          
                                       </asp:DropDownList>
                                         </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" ID Card Remaining No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_id_set_remainig" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("id_remaning_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                      <%--<asp:TemplateField HeaderText="ID_Card No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_id_set" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("id_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <%-- <asp:TemplateField HeaderText="ID Card Set ">
                                        <ItemStyle Width="90px"  />
                                        <ItemTemplate>

                                             <asp:DropDownList ID="ddl_id_card_set" runat="server" Style="width:90px" SelectedValue='<%# Bind("id_card_set") %>' CssClass="form-control" >
                                           <asp:ListItem Value="0">0</asp:ListItem>
                                           <asp:ListItem Value="1">1</asp:ListItem>
                                          
                                       </asp:DropDownList>
                                         </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <%--<asp:TemplateField HeaderText=" ID_Card Remaining No Of Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_id_set_remainig" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("id_remaning_No_of_set") %>' class="form-control dummyClass3" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>



                                     
                                    <asp:TemplateField HeaderText="Apron Size">
                                        <ItemStyle Width="90px"  />
                                        <ItemTemplate>
                                        <%--<asp:TextBox ID="txt_apron" runat="server" Style="width:65px" Text='<%# Eval("Apron_Size") %>' class="form-control dummyClass4" onkeypress="return isNumber_dot(event)" meta:resourceKey="txt_advance_paymentResource1" MaxLength="3"></asp:TextBox>--%>
                                    
                                             <asp:DropDownList ID="txt_apron" runat="server" Style="width:90px" CssClass="form-control" >
                                           <asp:ListItem Value="SMALL">SMALL</asp:ListItem>
                                           <asp:ListItem Value="MEDIUM">MEDIUM</asp:ListItem>
                                           <asp:ListItem Value="LARGE">LARGE</asp:ListItem>
                                           <asp:ListItem Value="XXL">XXL</asp:ListItem>
                                       </asp:DropDownList>
                                         </ItemTemplate>
                                    </asp:TemplateField>

                                   <%-- <asp:BoundField HeaderText="size" DataField="document_type" />--%>
        <%--<asp:TemplateField HeaderText = "Apron Size">
            <ItemTemplate>
                <asp:Label ID="lblapronsize" runat="server" Text='<%# Eval("Apron_Size") %>' Visible = "false" />
                <asp:DropDownList ID="ddlapronsize" runat="server" >
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>--%>
                                   
                                    <asp:TemplateField HeaderText="Apron Sets">
                                        <ItemStyle Width="55px" />
                                        <ItemTemplate>
                                        <asp:TextBox ID="txt_apron_set" runat="server" Style="width:55px" onkeypress=" return isNumber(event)" Text='<%# Eval("Apron_No_of_set") %>' class="form-control dummyClass4" meta:resourceKey="txt_advance_paymentResource1"  MaxLength="3"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                       
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkinv_update" Text="Update" Style="color:white;" runat="server" CssClass="btn btn-primary" OnClick="lnkinv_update_Click"  ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    
                                </Columns>
                            </asp:GridView>

                        </asp:Panel>
                    </div>
                    <div id="menu6">
                        <div class="row">
                            <br />
                        <table class="table table-striped" border="1" style="width:110%"><tr><th>

                              <asp:Button ID="btn_generate_id_card" runat="server" Width="20%"
                                    OnClick="btn_generate_id_card_Click" Text="Generate ID Card"
                                    CssClass="btn btn-primary" OnClientClick="return Reg_validate_2();" Visible="false" />

                              <asp:Button ID="btn_clientwise_all_employee_id" runat="server" Width="40%"
                                    OnClick="btn_clientwise_all_employee_id_Click" Text="Client and Branch Wise Id Card "
                                    CssClass="btn btn-primary" OnClientClick="return Reg_validate_3();" />

                             <asp:Button ID="btn_id_resend" runat="server"
                                    OnClick="btn_id_resend_Click" Text="Resend Id Card" Width="30%"
                                    CssClass="btn btn-primary" OnClientClick="return resend_btn_validation();" />



                             <asp:Button ID="btn_dublicate_id" runat="server" Width="30%"
                                  OnClick="btn_dublicate_id_Click"  Text="Dublicate Id Card Record"
                                    CssClass="btn btn-primary" OnClientClick="return" />

                                   </th>
                            <th>
                                <span style="text-align:center">
                                  Address Date:
                            <asp:TextBox ID="txt_print_list" runat="server" class="form-control datepicker text_box" 
                                Width="43%" style="margin-left: 8em;"></asp:TextBox>
                              </span>
                                     <br />
                                 <asp:Button ID="btn_dispatch_print" runat="server" Width="30%"
                                    OnClick="btn_dispatch_print_Click" Text="Address List"
                                    CssClass="btn btn-primary" OnClientClick="return Reg_val_dispatch();" />
                                <asp:Button ID="btn_uniform" runat="server"
                                    OnClick="btn_uniform_Click" Text="Uniform/Shoes Details" Width="45%"
                                    CssClass="btn btn-primary" OnClientClick="return Reg_val_dispatch();" />
                            </th>
                            <th>
                                  <asp:TextBox ID="txt_lot" runat="server" class="form-control" Text="1" onkeypress=" return isNumber(event)"></asp:TextBox>
                          <br />
                                <asp:Button ID="btn_sales_register" runat="server" Width="50%"
                                    OnClick="btn_sales_register_Click" Text="Sales Register"
                                    CssClass="btn btn-primary" />
                            </th>
                               </tr></table>
                              <br />
                           
                            </div>
                                <asp:Button ID="btn_bank_images" runat="server"
                                    OnClick="btn_bank_images_Click" Text="Bank Passbook Images"
                                    CssClass="btn btn-primary" OnClientClick="return Reg_validate_2();" Visible="false" />
                        <div id="dialog"></div>

                         <asp:Panel ID="Panel_first_id" runat="server" Style="overflow-x:auto;overflow-y:auto;height:300px;">

                         <div class="container-fluid" style="height:300px;overflow-y:auto;">
                             <%-- chane for search box v--%>
                            <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                            </div>
                              <asp:GridView ID="gv_checklist_uniform" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" OnRowDataBound ="gv_checklist_uniform_RowDataBound" ShowFooter="false" data-toggle="modal" href="#Div1" DataKeyNames="emp_code">
                                         <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                         <RowStyle ForeColor="#000066" />
                                         <Columns>
                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_header" runat="server" Text="SELECT EMPLOYEE"  />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_client" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                            <asp:BoundField DataField="client_Name" HeaderText="Client Name" SortExpression="client_Name" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="State Name" SortExpression="LOCATION" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                            <asp:BoundField DataField="emp_code" HeaderText="Employee Code" SortExpression="emp_code" />
                                             <asp:BoundField DataField="id_card_dispatch_date" HeaderText="Id Card Dispatch Date" SortExpression="id_card_dispatch_date" />
                                              <asp:BoundField DataField="unit_code" HeaderText="Branch Code" SortExpression="unit_code" />
                                             <asp:TemplateField HeaderText="Employee Photo">
                                <ItemTemplate>
                                    <asp:Image ID="original_photo" runat="server" Height="50" Width="50" />
                                </ItemTemplate>
                                                    </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                        </div>
                         </asp:Panel>

                       <%-- for dublicate id card 25-02-2020--%>


                         <div class="container-fluid" style="height:300px;overflow-y:auto;">
                             <%-- chane for search box v--%>
                            <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12">
                                    Search :
                        <asp:TextBox runat="server" ID="txt_search_box" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                            </div>
                              <asp:GridView ID="gv_dublicate_id" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" AutoGenerateColumns="false" OnRowDataBound="gv_dublicate_id_RowDataBound" ShowFooter="false" data-toggle="modal" href="#Div1" DataKeyNames="emp_code">
                                         <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                         <RowStyle ForeColor="#000066" />
                                         <Columns>
                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chk_gv_dublicate" runat="server" Text="SELECT EMPLOYEE"  />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_client_dublicate" runat="server" CssClass="center-block" />
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                            <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_Name" />
                                            <asp:BoundField DataField="state_name" HeaderText="State Name" SortExpression="state_name" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                            <asp:BoundField DataField="emp_code" HeaderText="Employee Code" SortExpression="emp_code" />
                                             <asp:BoundField DataField="unit_code" HeaderText="Branch Code" SortExpression="unit_code" />
                                            <asp:BoundField DataField="id_no_set" HeaderText="Dublicate Id Set" SortExpression="id_no_set" />
                                          <%--   <asp:BoundField DataField="id_card_dispatch_date" HeaderText="Id Card Dispatch Date" SortExpression="id_card_dispatch_date" />--%>
                                             <%-- <asp:BoundField DataField="unit_code" HeaderText="Branch Code" SortExpression="unit_code" />--%>
                                            <%-- <asp:TemplateField HeaderText="Employee Photo">
                                <ItemTemplate>
                                    <asp:Image ID="original_photo" runat="server" Height="50" Width="50" />
                                </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                        </div>

                          <%-- for dublicate id card 25-02-2020 end--%>





                            </div>
                            <br />
                        <br />
                    </div>
                </div>
  <br />  
                       
                      

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
                                    <asp:GridView ID="gv_appro_emp_count" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gv_appro_emp_count_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="client_Name" HeaderText="Client Name" SortExpression="client_Name" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="State Name" SortExpression="LOCATION" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />

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
                                    <asp:GridView ID="gv_appro_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gv_appro_emp_count_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="client_Name" HeaderText="Client Name" SortExpression="client_Name" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="State Name" SortExpression="LOCATION" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />

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
                            <h4 style="text-align: center">Employee Dispatch By Legal</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12" style="padding-left: 1%;">
                                    <asp:GridView ID="gv_reject_emp_legal" class="table" HeaderStyle-CssClass="FixedHeader" runat="server" OnRowDataBound="gv_appro_emp_count_RowDataBound" AutoGenerateColumns="false" ShowFooter="false" data-toggle="modal" href="#Div1">
                                        <Columns>
                                            <asp:BoundField DataField="client_Name" HeaderText="Client Name" SortExpression="client_Name" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="State Name" SortExpression="LOCATION" />
                                            <asp:BoundField DataField="unit_name" HeaderText="Branch Name" SortExpression="unit_name" />
                                            <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                            <asp:BoundField DataField="id_card_dispatch_date" HeaderText="Dispatch Date" SortExpression="id_card_dispatch_date" />
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



            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-12 ">
                <br />
            </div>

            <asp:Panel ID="Panel3" runat="server" CssClass="grid-view panel-body" ScrollBars="Auto">
                <asp:GridView ID="GridView2" class="table" runat="server" Width="100%" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                </asp:GridView>
            </asp:Panel>

        </asp:Panel>
    </div>
</asp:Content>



