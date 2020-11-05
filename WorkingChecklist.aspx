<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="Working Checklist" CodeFile="WorkingChecklist.aspx.cs" Inherits="WorkingChecklist" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>Billing & Salary Master</title>
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
    <script src="datatable/pdfmake.min.js"></script>
    <script type="text/javascript">
     function unblock() {
            $.unblockUI();
     }

     function pageload() {
       

     }
         </script>

    <style>
        .text-red {
            color: red;
        }

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

        .grid-view {
            max-height: 500px;
            height: auto;
            overflow-y: auto;
            overflow-x: hidden;
        }

        h5 {
            font-weight: bold;
            font-size: 15px;
        }

        .row {
            margin: 0px;
        }
    </style>

    <script type="text/javascript">




        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
            function EndRequestHandler(sender, args) {

                $('body').on('keyup', '.maskedExt', function () {
                    var num = $(this).attr("maskedFormat").toString().split(',');
                    var regex = new RegExp("^\\d{0," + num[0] + "}(\\.\\d{0," + num[1] + "})?$");
                    if (!regex.test(this.value)) {
                        this.value = this.value.substring(0, this.value.length - 1);
                    }
                });

                $.fn.dataTable.ext.errMode = 'none';
                var table = $('#<%=gv_working_checklist.ClientID%>').DataTable(
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
                     .appendTo('#<%=gv_working_checklist.ClientID%>_wrapper .col-sm-6:eq(0)');

           $.fn.dataTable.ext.errMode = 'none';
                

              

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

                $(".date-picker1").attr("readonly", "true");
                $(".date-picker2").attr("readonly", "true");
                //change_txt_box();
                //change_txt_box1();
               // billing_start_date();
                //ot_allowed();
                //add_number1();

            }
        });



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
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '32') || keyEntry == '45' || (keyEntry == '8') || (keyEntry == '9') || (keyEntry == '46') || (keyEntry == '44'))
                return true;
            else {
                // alert('Please Enter Only Character values.');
                return false;
            }
        }
        function r_validation(){
            var ddl_grade = document.getElementById('<%=ddl_grade.ClientID %>');
            var Selected_ddl_grade = ddl_grade.options[ddl_grade.selectedIndex].text;


            if (Selected_ddl_grade == "Select") {
                alert("Please Select Client");
                ddl_grade.focus();
                return false;
            }
            var txt_description = document.getElementById('<%=txt_description .ClientID %>');
            if (txt_description.value == "") {
                alert("Please Enter Discription");
                txt_description.focus();
                return false;
            }
            var ddl_type = document.getElementById('<%=ddl_type.ClientID %>');
            var Selected_ddl_type = ddl_type.options[ddl_type.selectedIndex].text;


            if (Selected_ddl_type == "Select") {
                alert("Please Select Job do on");
                ddl_type.focus();
                return false;
            }
            var txt_time = document.getElementById('<%=txt_time .ClientID %>');
            if (txt_time.value == "") {
                alert("Please Enter No of Time");
                txt_time.focus();
                return false;
            }
            var txt_time1 = document.getElementById('<%=txt_time.ClientID %>');
            if (parseInt(txt_time1.value) > 12) {
                alert("Please Enter Less Than or equal to 12.");
                txt_time1.focus();
                return false;
            }
            return true;
        }



    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Working Checklist</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="panel-body">

            <asp:Panel ID="panel1" runat="server" CssClass="panel panel-primary ">
                <div class="panel-body">

                    <div class="row">
                        <div class="col-sm-2 col-xs-12 text-left">
                            Designation:<span class="text-red"> *</span>

                            <asp:DropDownList ID="ddl_grade" class="form-control  pr_state js-example-basic-single text_box" Width="100%" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            Enter Description: <span class="text-red">*</span>
                            <asp:TextBox ID="txt_description" runat="server" class="form-control  text_box" placeholder=" description:" Width="100%" Style="display: inline"></asp:TextBox>
                        </div>


                        <div class="col-sm-2 col-xs-12">
                            Job do on :<span class="text-red"> *</span>
                            <asp:DropDownList ID="ddl_type" class="form-control  pr_state js-example-basic-single text_box" Width="100%" runat="server">

                                <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                <asp:ListItem Value="Weekly">Weekly</asp:ListItem>
                                <asp:ListItem Value="Fourth Nightly">Fourth Nightly</asp:ListItem>
                                <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                <asp:ListItem Value="Six Monthly">Six Monthly</asp:ListItem>
                                <asp:ListItem Value="Yearly">Yearly</asp:ListItem>

                            </asp:DropDownList>
                        </div>



                        <div class="col-sm-2 col-xs-12">
                            No of Time: <span class="text-red">*</span>
                            <asp:TextBox ID="txt_time" runat="server" class="form-control  text_box" MaxLength="2" placeholder="time:" Width="100%" Style="display: inline" onkeypress="return isNumber(event)" ></asp:TextBox>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txt_id" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                        </div>



                    </div>


                    <br />

                    <div class="row text-center">


                        <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text=" Save " OnClick="btnadd_click" OnClientClick="return r_validation();"/>
                        <asp:Button ID="btn_UPDATE" runat="server" class="btn btn-primary" Text=" Update " OnClick="btn_UPDATE_click" OnClientClick="return r_validation();" />
                        <asp:Button ID="btndelete" runat="server" class="btn btn-primary" Text="Delete" OnClick="btndelete_click" />
                        <asp:Button ID="btn_close" runat="server" class="btn btn-danger" Text=" Close " OnClick="btnclose_Click" />
                    </div>
                    <br />
                </div>
            </asp:Panel>




            <br />





            <asp:Panel ID="Panel2" runat="server" CssClass="grid-view">
                <asp:GridView ID="gv_working_checklist" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333"
                     Font-Size="X-Small" OnSelectedIndexChanged="fill_gridview_OnSelectedIndexChanged" OnRowDataBound="gv_working_checklist_RowDataBound" OnPreRender="gv_working_checklist_PreRender">
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />


                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                        <asp:BoundField DataField="comp_code" HeaderText="Com_code" SortExpression="comp_code" Visible="false" />
                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />
                    </Columns>

                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                </asp:GridView>
            </asp:Panel>
        </div>
        <br />
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


