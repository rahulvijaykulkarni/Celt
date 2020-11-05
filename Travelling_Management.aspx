<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Travelling_Management.aspx.cs" Inherits="Travelling_Management" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Travelling Management</title>
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
       <link href="datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="datatable/buttons.bootstrap.min.css" rel="stylesheet" />
    <%--<script src="datatable/jquery-1.12.3.js"></script>--%>
    <script src="datatable/jquery.dataTables.min.js"></script>
    <script src="datatable/dataTables.bootstrap.min.js"></script>
    <script src="datatable/dataTables.buttons.min.js"></script>
    <script src="datatable/buttons.bootstrap.min.js"></script>
    <%--  <script src="datatable/jszip.min.js"></script>--%>
    <%--    <script src="datatable/pdfmake.min.js"></script>--%>
    <script src="datatable/vfs_fonts.js"></script>
    <script src="datatable/buttons.html5.min.js"></script>
    <script src="datatable/buttons.print.min.js"></script>
    <script src="datatable/buttons.colVis.min.js"></script>

    <link href="css/new_stylesheet.css" rel="stylesheet" />

   

    <style>
        .container {
            max-width: 99%;
        }

        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .text_box {
            margin-top: 7px;
        }

        .auto-style1 {
            color: #FFFFFF;
        }

        h2 {
            border-radius: 5px;
        }

        .button {
            background-color: #D3D3D3;
            border: none;
            color: white;
            /*padding: 10px;*/
            text-align: center;
            text-decoration-color: black;
            text-decoration: black;
            display: inline-block;
            font-size: 12px;
            /*margin: 10px 5px;*/
            cursor: pointer;
            border-radius: 8px;
            border-color: #00008B;
            border-width: 1px;
            border-style: solid;
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
    </style>

    <script>

        $(function () {


            $('#<%=btnSubmit.ClientID%>').click(function () {
                if (validate()) {
                    $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
                }
            });
        });
    </script>
    <script type="text/javascript">
        window.onfocus = function () {
            $.unblockUI();

        }
        function validate() {


            var e_product = document.getElementById('<%=ddlpolicies.ClientID%>');
            var SelectedText = e_product.options[e_product.selectedIndex].text;
            if (SelectedText == "Select Policy") {
                alert("Please Select Policy !!!");
                e_product.focus();
                return false;
            }

            var ddl_designation = document.getElementById('<%=ddl_designation.ClientID %>');
            var SelectedText_ddl_designation = ddl_designation.options[ddl_designation.selectedIndex].text;
            if (SelectedText_ddl_designation == "Select Designation") {
                alert("Please Select Designation");
                ddl_designation.focus();
                return false;
            }
        }
        function openWindow() {
            window.open("html/travelling_managment.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function pageLoad(sender, args) {
            $(document).ready(function () {
                $('#<%=ddl_designation.ClientID%>').change(function () {
                    if (this.checked) {
                        document.getElementById("<%=CheckDepartment.ClientID%>").checked = false;
                        document.getElementById("<%=ChecUnit.ClientID%>").checked = false;
                    }
                });
                $('#<%=CheckDepartment.ClientID%>').change(function () {
                    if (this.checked) {
                        document.getElementById("<%=ddl_designation.ClientID%>").checked = false;
                        document.getElementById("<%=ChecUnit.ClientID%>").checked = false;
                    }
                });
                $('#<%=ChecUnit.ClientID%>').change(function () {
                    if (this.checked) {
                        document.getElementById("<%=CheckDepartment.ClientID%>").checked = false;
                        document.getElementById("<%=ddl_designation.ClientID%>").checked = false;
                    }
                });
            });
        }
        $(document).ready(function () {

            $(".check_list").hide();
            $(".check_dept").click(function () {

                $(".check_list").toggle();

                $(".listbox_left").hide();

            });

            $(".listbox_left").show();
        });
        function Req_validation() {
            var txt_date = document.getElementById('<%=lstLeft.ClientID %>');

            if (txt_date.value == "") {
                alert("Please Select EMP NAME");

                txt_date.focus();
                return false;
            }
        }
        function Req_validation1() {
            var txt_date = document.getElementById('<%=lstRight.ClientID %>');

            if (txt_date.value == "") {
                alert("Please Select EMP NAME");

                txt_date.focus();
                return false;
            }
        }
        $(document).ready(function () {
            $(document).on("Keyup", function () {
                SearchGrid('<%=txt_search.ClientID%>', '<%=emp_gv.ClientID%>');
            });


            $('[id*=Policy name]').click(function () {
                $("[id*='EMP_NAME']").attr('checked', this.checked);
            });


        });

        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=emp_gv.ClientID %>");
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

        $.fn.dataTable.ext.errMode = 'none';
        var table = $('#<%=emp_gv.ClientID%>').DataTable(
               {
                   scrollY: "210px", buttons: [
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
           .appendTo('#<%=emp_gv.ClientID%>_wrapper .col-sm-6:eq(0)');

          $.fn.dataTable.ext.errMode = 'none';
    </script>
    <%--  <script type="text/javascript">  
        $(document).ready(
            function ()
            {
                $('#btnleft').click(
                    function (e) {
                        $('#lstLeft > option:selected').appendTo('#lstRight');
                        e.preventDefault();
                    });

                $('#btnright').click(
              function (e) {
                  $('#lstRight > option:selected').appendTo('#lstLeft');
                  e.preventDefault();
              });
            });
            
       </script>--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--  <asp:UpdatePanel runat="server" ID="update">
            <ContentTemplate>--%>
        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase" font-size="25px"><b>Travelling Management</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Travelling Management Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <div class="row">

                    <div class="col-md-1 col-sm-3 col-xs-12" style="margin-top: 10px;"><b>Policies :</b></div>
                    <div class="col-md-2 col-sm-3 col-xs-12">

                        <asp:DropDownList AppendDataBoundItems="true" ID="ddlpolicies" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddlpolicies_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>
                     <div class="col-md-1 col-sm-3 col-xs-12" style="margin-top: 10px;"><b>Designation :</b></div>
                     <div class="col-md-2 col-sm-3 col-xs-12">

                        <asp:DropDownList AppendDataBoundItems="true" ID="ddl_designation" runat="server" class="form-control text_box" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>
                    
                                <br />
                    <br />
                                               
                   <%-- <div class="col-md-1  col-sm-3 col-xs-12"></div>
                    <div class=" col-md-2 col-sm-3 col-xs-12" style="margin-top: 10px;">
                        <%--<asp:CheckBox ID="checkemp" runat="server" Text="&nbsp&nbspEmployee List" OnCheckedChanged="checkemp_click" AutoPostBack="true" />--%>
                         <%--<asp:RadioButton ID="checkemp" runat="server" GroupName="group" Text="&nbsp&nbspEmployee List" OnCheckedChanged="checkemp_click" AutoPostBack="true" />
                    </div>--%>
                    <div class="col-md-2 col-sm-3 col-xs-12" style="margin-top: 10px;">
                        <%-- <asp:CheckBox ID="CheckDepartment" class="check_dept" runat="server" Text="&nbsp&nbspDepartment List" OnCheckedChanged="CheckDepartment_click" AutoPostBack="true" />--%>

                        <asp:RadioButton ID="CheckDepartment" Visible="false" runat="server" GroupName="group" Text="&nbsp&nbspDepartment List" OnCheckedChanged="CheckDepartment_click" AutoPostBack="true" />

                    </div>
                    <div class="col-md-2 col-sm-3 col-xs-12" style="margin-top: 10px;">
                        <%--   <asp:CheckBox ID="ChecUnit" runat="server" Text="&nbsp&nbspUnit List" OnCheckedChanged="ChecUnit_click" AutoPostBack="true" />--%>

                        <asp:RadioButton ID="ChecUnit" Visible="false" runat="server" GroupName="group" Text="&nbsp&nbspUnit List" OnCheckedChanged="ChecUnit_click" AutoPostBack="true" />
                    </div>

                </div>
                <br />
                <asp:Panel ID="employeepanel" runat="server">
                    <br />
                    <br />


                    <div class="row">
                        <div class="col-sm-3 col-xs-12 " style="border: 1px;">
                            <asp:CheckBoxList ID="checklistcox" runat="server" SelectionMode="Multiple" Width="100%"
                                Height="300" OnSelectedIndexChanged="checklistcox_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:CheckBoxList>

                        </div>
                        <div class="col-sm-3 col-xs-12 ">
                            <asp:ListBox ID="lstLeft" runat="server" DataTextField="emp_name" DataValueField="emp_code" SelectionMode="Multiple" Width="100%" Visible="true"
                                Height="300"></asp:ListBox>

                        </div>
                        <div class="col-sm-1 col-xs-12 "></div>
                        <div class="col-sm-2 col-xs-12 ">
                            <br />
                            <br />
                            <div class="row">
                                <asp:Button ID="brnallleft" value=">>" OnClick="brnallleft_Click" runat="server" Class="btn btn-primary" Style="padding-top: 11px; padding-right: 9px; margin-left: 3em;" Text=">>" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:Button ID="btnright" value="<" OnClick="btnright_click" runat="server" Class="btn btn-primary" Text=">" Style="padding-left: 16px; padding-top: 10px; margin-left: 3em;" OnClientClick="return Req_validation();"/>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Button ID="btnleft" value="<" OnClick="btnleft_click" runat="server" Class="btn btn-primary" Text="<" Style="padding-left: 16px; padding-top: 10px; margin-left: 3em;" OnClientClick="return Req_validation1();"/>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Button ID="allriht" value="<<" OnClick="allriht_click" runat="server" Class="btn btn-primary" Text="<<" Style="padding-top: 11px; padding-right: 9px; margin-left: 3em;" />
                            </div>
                            <br />



                        </div>
                        <div class="col-sm-3 col-xs-12">
                            <asp:ListBox ID="lstRight" DataTextField="emp_name" DataValueField="emp_code" runat="server" SelectionMode="Multiple" Width="100%"
                                Height="300"></asp:ListBox>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                    <br />
                    <br />

                <div class="row text-center">
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_click" OnClientClick="return validate();" Class="btn btn-primary" Style="margin-left: 32em;" />
                    <asp:Button ID="btnclose" Text="Close" runat="server" class="btn btn-danger" OnClick="btnclose_click" />
                </div>
                <br />
               
                <div class="container-fluid">
                     <div class="row">
                                <div class="col-sm-10 col-xs-12"></div>
                                <div class="col-sm-2 col-xs-12" id="search" runat="server">
                                    Search :
                        <asp:TextBox runat="server" ID="txt_search" CssClass=" form-control" onkeyup="Search_Gridview(this)" />
                                </div>
                         <br />
                         <br />
                            </div>
                                    <div class="row">
                                                  <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" Style="overflow-x: hidden;" >

                                                      <asp:GridView ID="emp_gv" class="table" runat="server" BackColor="White"
                                                          BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                        OnPreRender="emp_gv_PreRender" OnRowDataBound="emp_gv_RowDataBound"
                                                          AutoGenerateColumns="False" Width="100%">
                                                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                          <AlternatingRowStyle BackColor="White" />
                                                          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                                          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                          <RowStyle BackColor="#EFF3FB" />
                                                          <EditRowStyle BackColor="#2461BF" />
                                                          <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <Columns>
                                                 <asp:BoundField DataField="Policy name" HeaderText="Policy name" SortExpression="Policy name" />
                                                   <asp:BoundField DataField="EMP_NAME" HeaderText="Employee Name" SortExpression="EMP_NAME" />
                                            <asp:BoundField DataField="GRADE_DESC" HeaderText="Designation" SortExpression="GRADE_DESC" />
                                       
                                                </Columns>
                                            </asp:GridView>
                                                      </div>
                                        </asp:Panel>
                                    </div>
                    </div>
                 
            </div>
        </asp:Panel>
        <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>




