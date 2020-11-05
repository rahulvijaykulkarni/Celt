<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="Client GPS Policy" CodeFile="ClientPolicy.aspx.cs" Inherits="ClientPolicy" EnableEventValidation="false" %>

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
            $(".date-picker1").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                minDate: 0,
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


               

                ////////////////////////////////

                var table = $('#<%=GridView_daily_master.ClientID%>').DataTable(
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
                   .appendTo('#<%=GridView_daily_master.ClientID%>_wrapper .col-sm-6:eq(0)');



                //////////////////////////////////

              



            }
        });



            //function Number10(evt) {
            //    if (null != evt) {
            //        evt = (evt) ? evt : window.event;
            //        var charCode = (evt.which) ? evt.which : evt.keyCode;
            //        if ((charCode >= 48 || charCode <= 58)) {
            //            return false;
            //        }
            //    }
            //    return true;
            //}

            function Req_validation() {
                var txt_policy_name1 = document.getElementById('<%=txt_policy_name1.ClientID %>');
                if (txt_policy_name1.value == "") {
                    alert("Please Enter New Policy Name");
                    txt_policy_name1.focus();
                    return false;
                }
                var txt_start_date = document.getElementById('<%=txt_start_date.ClientID %>');
                if (txt_start_date.value == "") {
                    alert("Please Enter Start Date");
                    txt_start_date.focus();
                    return false;
                }
                var txt_end_date = document.getElementById('<%=txt_end_date.ClientID %>');
                if (txt_end_date.value == "") {
                    alert("Please Enter End Date");
                    txt_end_date.focus();
                    return false;
                }
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
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Client GPS Policy</b></div>
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
                            Client Name :<span class="text-red"> *</span>

                            <asp:DropDownList ID="ddl_unit_client" class="form-control  pr_state js-example-basic-single text_box" Width="100%" runat="server" OnSelectedIndexChanged="ddl_clientname_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            State :<span class="text-red"> *</span>

                            <asp:DropDownList ID="ddl_clientwisestate" runat="server" class="form-control pr_state js-example-basic-single text_box" OnSelectedIndexChanged="ddl_clientwisestate_SelectedIndexChanged" AutoPostBack="true" Width="100%">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-sm-3 col-xs-12 text-left">
                            Branch Not Having Policy : <span class="text-red">*</span>
                            <asp:ListBox ID="ddl_unitcode" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        </div>

                        <div class="col-sm-3 col-xs-12 text-left">
                            Branch Having Policy : <span class="text-red">*</span>
                            <asp:ListBox ID="ddl_unitcode_without" runat="server" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_unitcode_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                        </div>


                        <div class="col-md-2 col-xs-12">
                            Designation :<span class="text-red"> *</span>
                            <asp:ListBox ID="ddl_designation" runat="server" DataTextField="txt_policy_name" DataValueField="id" class="form-control pr_state js-example-basic-single text_box" SelectionMode="Multiple" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                            <%--<asp:DropDownList ID="ddl_designation" runat="server" class="form-control text_box" DataTextField="txt_policy_name" DataValueField="id">
                                </asp:DropDownList>--%>
                        </div>
                         </div>
                    <br />
                        <div class="row">


                        <div class="col-sm-2 col-xs-12">
                            Duty Hours :<span class="text-red"> *</span>
                            <asp:DropDownList ID="ddl_hours" runat="server" CssClass="form-control text_box" Width="100%" onchange="return billing_hours();">
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-xs-12">
                            New Policy Name: <span class="text-red">*</span>
                            <asp:TextBox ID="txt_policy_name1" runat="server" class="form-control text_box"
                                placeholder=" New Policy Name : " onkeypress="return AllowAlphabet_address(event)"></asp:TextBox>
                        </div>


                       <div class="col-sm-2 col-xs-12">                          
                                Start Date: <span class="text-red">*</span>
                                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control date-picker1"   Width="100%" ></asp:TextBox>
                            </div>

                        <div class="col-sm-2 col-xs-12">
                                End Date: <span class="text-red">*</span>
                                <asp:TextBox ID="txt_end_date" runat="server"  CssClass="form-control date-picker2" Width="100%" ></asp:TextBox>
                            </div>

                   
                        <div class="col-sm-2 col-xs-12">
                            <asp:TextBox ID="txt_id" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                        </div>


                    </div>


                    <br />


                    <br />
                </div>
            </asp:Panel>
            <br />


            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home">Daily</a></li>
                <li><a id="A1" data-toggle="tab" href="#menu1" runat="server">Weekly</a></li>
                <li><a data-toggle="tab" href="#menu2">Fourth Nightly</a></li>
                <li><a data-toggle="tab" href="#menu3">Monthly</a></li>
                <li><a data-toggle="tab" href="#menu4">Quarterly</a></li>
                <li><a data-toggle="tab" href="#menu5">Six Monthly</a></li>
                <li><a data-toggle="tab" href="#menu6">Yearly</a></li>

            </ul>
            <div class="tab-content">
                <div class="tab-pane fade in active" id="home">

                    <%--Daily code--%>
                    <br />
                    <br />
                    <asp:Panel ID="Panel2" runat="server" >
                        <asp:GridView ID="gv_client_policy" class="table" runat="server" OnRowDataBound="gv_client_policy_RowDataBound" CellPadding="1" AutoGenerateColumns="false"  OnPreRender="gv_client_policy_PreRender" ForeColor="#333333" Font-Size="X-Small">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />

                            <Columns>

                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade" runat="server" Text='<%# Eval("grade")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time" runat="server" ReadOnly="true" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_1_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="2Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_2_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_3_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_4_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_5_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_6_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_7_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_8_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_9_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_10_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_11_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_daily_12_time" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>

                               </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>

<%--                    <div class="row text-center">

                        <br />
                        <br />


                        <%-- new add gridview 11/04/2019--%>
                       <%-- <asp:Panel ID="panela" runat="server" CssClass="grid-view">


                            <asp:GridView ID="gv_client_daily" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanged="gv_client_daily_SelectedIndexChanged" OnRowDataBound="gv_client_daily_RowDataBound">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />

                                <Columns>

                                    <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                    <asp:BoundField DataField="grade" HeaderText="Garde" SortExpression="grade" />
                                    <asp:BoundField DataField="description" HeaderText="Description`" SortExpression="description" />
                                    <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />
                                    <asp:BoundField DataField="1_time" HeaderText="1_time" SortExpression="1_time" />
                                    <asp:BoundField DataField="2_time" HeaderText="2_time" SortExpression="2_time" />
                                    <asp:BoundField DataField="3_time" HeaderText="3_time" SortExpression="3_time" />
                                    <asp:BoundField DataField="4_time" HeaderText="4_time" SortExpression="4_time" />
                                    <asp:BoundField DataField="5_time" HeaderText="5_time" SortExpression="5_time" />
                                    <asp:BoundField DataField="6_time" HeaderText="6_time" SortExpression="6_time" />
                                    <asp:BoundField DataField="7_time" HeaderText="7_time" SortExpression="7_time" />
                                    <asp:BoundField DataField="8_time" HeaderText="8_time" SortExpression="8_time" />
                                    <asp:BoundField DataField="9_time" HeaderText="9_time" SortExpression="9_time" />
                                    <asp:BoundField DataField="10_time" HeaderText="10_time" SortExpression="10_time" />
                                    <asp:BoundField DataField="11_time" HeaderText="11_time" SortExpression="11_time" />
                                    <asp:BoundField DataField="12_time" HeaderText="12_time" SortExpression="12_time" />

                                </Columns>

                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                            </asp:GridView>

                        </asp:Panel>
                        <br />
                        <br />--%>


                    <%--</div>--%>

                </div>
                <div class="tab-pane" id="menu1">
  

                    <%--Weekly code--%>
                    <br />
                    <asp:Panel ID="Panel4" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_weekly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_weekly_PreRender" OnRowDataBound="gridview_weekly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />

                            <Columns>
                                 
                      
                                <asp:TemplateField HeaderText="Checklist" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle  />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_weekly" runat="server" Text='<%# Eval("grade")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle  />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_weekly" runat="server" Text='<%# Eval("description")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_weekly" runat="server" Text='<%# Eval("type")%>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle  />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_weekly" ReadOnly="true" runat="server" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>




                                <%--<asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">--%>
                                <asp:TemplateField HeaderText="1 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_6_time" runat="server" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_weekly_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>

                            <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />--%>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />

                        </asp:GridView>
                    </asp:Panel>


                </div>


                <div class="tab-pane" id="menu2">

                    <br />

                    <%--Fourth Nightly--%>

                    <asp:Panel ID="Panel8" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_fourth_nightly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_fourth_nightly_PreRender" OnRowDataBound="gridview_fourth_nightly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>

                               

                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />--%>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_fourth_nightly" runat="server" Text='<%# Eval("grade")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_fourth_nightly" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_fourth_nightly" runat="server" Text='<%# Eval("type")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_fourth_nightly" ReadOnly="true" runat="server" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>




                                <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"  Visible="false"/>
                                        <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />
                                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>








                                <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_6_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_fourth_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>




                    <%-- new add gridview_fourth 28/04/2019--%>
                    <%--<asp:Panel ID="panel12" runat="server" CssClass="grid-view">

                                
                                <asp:GridView ID="GridView_fourth_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanging="GridView_fourth_master_SelectedIndexChanging" OnRowDataBound="GridView_fourth_master_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />



                                    <Columns>

                                        <asp:BoundField DataField="Id"  HeaderText="Id" SortExpression="Id"/>
                                        <asp:BoundField DataField="client_code"  HeaderText="Client_Code" SortExpression="client_code"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy"  HeaderText="Unit_Code" SortExpression="branch_having_policy"/>
                                        <asp:BoundField DataField="state"  HeaderText="State" SortExpression="state"/>
                                        
                                        <asp:BoundField DataField="branch_not_having_policy"  HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy"/>
                                        <asp:BoundField DataField="designation"  HeaderText="Designation" SortExpression="designation"/>
                                        <asp:BoundField DataField="duty_hours"  HeaderText="Duty_hours" SortExpression="duty_hours"/>
                                        <asp:BoundField DataField="new_policy_name"  HeaderText="New_policy_name" SortExpression="new_policy_name"/>
                                        <asp:BoundField DataField="start_date"  HeaderText="Start_date" SortExpression="start_date"/>
                                        <asp:BoundField DataField="end_date"  HeaderText="End_date" SortExpression="end_date"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy_1"  HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1"/>
                                        
                                    </Columns>

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>

                            </asp:Panel>--%>



                    <div class="row text-center">


                        <%--  <asp:Button ID="btn_fourth_n_add" runat="server" class="btn btn-primary" Text=" Save " OnClick="btn_fourth_n_add_Click" OnClientClick="return Req_validation();" />
                       <asp:Button ID="btn_fourth_n_delete" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="btn_fourth_n_close" runat="server" class="btn btn-danger" Text=" Close " />--%>
                    </div>



                </div>



                <div class="tab-pane" id="menu3">


                    <%--Monthly code--%>

                    <asp:Panel ID="Panel5" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_monthly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_monthly_PreRender" OnRowDataBound="gridview_monthly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />



                            <Columns>

                                  <%-- <asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_Checklist_monthly" runat="server" CausesValidation="false" OnClick="lnk_remove_Checklist_monthly_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
--%>




                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />--%>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_monthly" runat="server" Text='<%# Eval("grade")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_monthly" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_monthly" runat="server" Text='<%# Eval("type")%>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_monthly" ReadOnly="true" runat="server" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>





                                <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"  Visible="false"/>
                                        <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />
                                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />

                                --%>








                                <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_6_time" runat="server" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_monthly_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>


                    <%-- new add gridview_fourth 29/04/2019--%>
                    <%--<asp:Panel ID="panel13" runat="server" CssClass="grid-view">

                                
                                <asp:GridView ID="GridView_monthly_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanging="GridView_fourth_master_SelectedIndexChanging" OnRowDataBound="GridView_fourth_master_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />



                                    <Columns>

                                        <asp:BoundField DataField="Id"  HeaderText="Id" SortExpression="Id"/>
                                        <asp:BoundField DataField="client_code"  HeaderText="Client_Code" SortExpression="client_code"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy"  HeaderText="Unit_Code" SortExpression="branch_having_policy"/>
                                        <asp:BoundField DataField="state"  HeaderText="State" SortExpression="state"/>
                                        
                                        <asp:BoundField DataField="branch_not_having_policy"  HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy"/>
                                        <asp:BoundField DataField="designation"  HeaderText="Designation" SortExpression="designation"/>
                                        <asp:BoundField DataField="duty_hours"  HeaderText="Duty_hours" SortExpression="duty_hours"/>
                                        <asp:BoundField DataField="new_policy_name"  HeaderText="New_policy_name" SortExpression="new_policy_name"/>
                                        <asp:BoundField DataField="start_date"  HeaderText="Start_date" SortExpression="start_date"/>
                                        <asp:BoundField DataField="end_date"  HeaderText="End_date" SortExpression="end_date"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy_1"  HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1"/>
                                        
                                    </Columns>

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>

                            </asp:Panel>--%>



                    <div class="row text-center">


                        <%-- <asp:Button ID="btn_month_add" runat="server" class="btn btn-primary"
                                    Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                               
                       
                        <asp:Button ID="btn_month_delete" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="btn_month_close" runat="server" class="btn btn-danger" Text=" Close " />--%>
                    </div>


                </div>
                <div class="tab-pane" id="menu4">


                    <%--Quarterly--%>

                    <asp:Panel ID="Panel6" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_quarterly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_quarterly_PreRender" OnRowDataBound="gridview_quarterly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />



                            <Columns>

                                 <%-- <asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_Checklist_quarterly" runat="server" CausesValidation="false" OnClick="lnk_remove_Checklist_quarterly_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />--%>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_quarterly" runat="server" Text='<%# Eval("grade")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_quarterly" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_quarterly" runat="server" Text='<%# Eval("type")%>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_quarterly" ReadOnly="true" runat="server" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>




                                <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"  Visible="false"/>
                                        <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />
                                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>








                                <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_6_time" runat="server" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_quarterly_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>


                    <%-- new add GridView_quarterly_master 30/04/2019--%>
                    <%--  <asp:Panel ID="panel14" runat="server" CssClass="grid-view">

                                
                                <asp:GridView ID="GridView_quarterly_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanging="GridView_fourth_master_SelectedIndexChanging" OnRowDataBound="GridView_fourth_master_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />



                                    <Columns>

                                        <asp:BoundField DataField="Id"  HeaderText="Id" SortExpression="Id"/>
                                        <asp:BoundField DataField="client_code"  HeaderText="Client_Code" SortExpression="client_code"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy"  HeaderText="Unit_Code" SortExpression="branch_having_policy"/>
                                        <asp:BoundField DataField="state"  HeaderText="State" SortExpression="state"/>
                                        
                                        <asp:BoundField DataField="branch_not_having_policy"  HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy"/>
                                        <asp:BoundField DataField="designation"  HeaderText="Designation" SortExpression="designation"/>
                                        <asp:BoundField DataField="duty_hours"  HeaderText="Duty_hours" SortExpression="duty_hours"/>
                                        <asp:BoundField DataField="new_policy_name"  HeaderText="New_policy_name" SortExpression="new_policy_name"/>
                                        <asp:BoundField DataField="start_date"  HeaderText="Start_date" SortExpression="start_date"/>
                                        <asp:BoundField DataField="end_date"  HeaderText="End_date" SortExpression="end_date"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy_1"  HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1"/>
                                        
                                    </Columns>

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>

                            </asp:Panel>--%>




                    <div class="row text-center">


                        <%--<asp:Button ID="btn_quarter_add" runat="server" class="btn btn-primary"
                                    Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                               
                        
                        <asp:Button ID="btn_quarter_delete" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="btn_quarter_close" runat="server" class="btn btn-danger" Text=" Close " />--%>
                    </div>



                </div>
                <div class="tab-pane" id="menu5">


                    <%--Six Monthly--%>

                    <asp:Panel ID="Panel7" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_six_monthly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_six_monthly_PreRender" OnRowDataBound="gridview_six_monthly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />



                            <Columns>
                               <%-- <asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_Checklist_six" runat="server" CausesValidation="false" OnClick="lnk_remove_Checklist_six_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>



                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />--%>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_six_month" runat="server" Text='<%# Eval("grade")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_six_month" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_six_month" runat="server" Text='<%# Eval("type")%>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_six_month" ReadOnly="true" runat="server" Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>



                                <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"  Visible="false"/>
                                        <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />
                                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>







                                <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_6_time" runat="server" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_six_month_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>





                    <%-- new add gridview_fourth 31/04/2019--%>
                    <%-- <asp:Panel ID="panel15" runat="server" CssClass="grid-view">--%>


                    <%--<asp:GridView ID="GridView_six_monthly_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanging="GridView_fourth_master_SelectedIndexChanging" OnRowDataBound="GridView_fourth_master_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />



                                    <Columns>

                                        <asp:BoundField DataField="Id"  HeaderText="Id" SortExpression="Id"/>
                                        <asp:BoundField DataField="client_code"  HeaderText="Client_Code" SortExpression="client_code"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy"  HeaderText="Unit_Code" SortExpression="branch_having_policy"/>
                                        <asp:BoundField DataField="state"  HeaderText="State" SortExpression="state"/>
                                        
                                        <asp:BoundField DataField="branch_not_having_policy"  HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy"/>
                                        <asp:BoundField DataField="designation"  HeaderText="Designation" SortExpression="designation"/>
                                        <asp:BoundField DataField="duty_hours"  HeaderText="Duty_hours" SortExpression="duty_hours"/>
                                        <asp:BoundField DataField="new_policy_name"  HeaderText="New_policy_name" SortExpression="new_policy_name"/>
                                        <asp:BoundField DataField="start_date"  HeaderText="Start_date" SortExpression="start_date"/>
                                        <asp:BoundField DataField="end_date"  HeaderText="End_date" SortExpression="end_date"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy_1"  HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1"/>
                                        
                                    </Columns>

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>

                            </asp:Panel>--%>


                    <div class="row text-center">


                        <%--<asp:Button ID="btn_six_month_add" runat="server" class="btn btn-primary"
                                    Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                               
                        
                        <asp:Button ID="btn_six_month_delete" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="btn_six_month_close" runat="server" class="btn btn-danger" Text=" Close " />--%>
                    </div>


                </div>




                <div class="tab-pane" id="menu6">
                    <%--Yearly--%>

                    <asp:Panel ID="Panel9" runat="server" CssClass="grid-view">
                        <asp:GridView ID="gridview_yearly" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnPreRender="gridview_yearly_PreRender" OnRowDataBound="gridview_yearly_RowDataBound">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="White" />



                            <Columns>

                                 <%--<asp:TemplateField>
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_remove_Checklist_yearly" runat="server" CausesValidation="false" OnClick="lnk_remove_Checklist_yearly_Click" OnClientClick="return R_validation();"><img alt="" height="15"  src="Images/delete_icon.png" width="15" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>



                                <asp:TemplateField HeaderText="Checklist" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_client" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="ID">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />--%>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_grade_yearly" runat="server" Text='<%# Eval("grade")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />--%>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description_yearly" runat="server" Text='<%# Eval("description")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />--%>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type_yearly" runat="server" Text='<%# Eval("type")%>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />--%>

                                <asp:TemplateField HeaderText="Time">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="lbl_time_yearly" ReadOnly="true" runat="server"  Text='<%# Eval("time")%>' Width="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>







                                <%--<asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id"  Visible="false"/>
                                        <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" Visible="false" />
                                        <asp:BoundField DataField="grade" HeaderText="Grade" SortExpression="grade" />
                                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                        <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time" />--%>







                                <asp:TemplateField HeaderText="1 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_1_time" runat="server" SelectedValue='<%# Bind("1_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="2 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_2_time" runat="server" SelectedValue='<%# Bind("2_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="3 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_3_time" runat="server" SelectedValue='<%# Bind("3_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="4 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_4_time" runat="server" SelectedValue='<%# Bind("4_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="5 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_5_time" runat="server" SelectedValue='<%# Bind("5_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="6 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_6_time" runat="server" SelectedValue='<%# Bind("6_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="7 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_7_time" runat="server" SelectedValue='<%# Bind("7_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="8 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_8_time" runat="server" SelectedValue='<%# Bind("8_time") %>'>
                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="9 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_9_time" runat="server" SelectedValue='<%# Bind("9_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="10 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_10_time" runat="server" SelectedValue='<%# Bind("10_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="11 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_11_time" runat="server" SelectedValue='<%# Bind("11_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="12 Time" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_yearly_12_time" runat="server" SelectedValue='<%# Bind("12_time") %>'>

                                            <asp:ListItem Value="0" Text="Set"></asp:ListItem>
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
                                            <%--<asp:ListItem Value="CO" Text="CO" />--%>
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                        </asp:GridView>
                    </asp:Panel>




                    <%-- new add gridview_fourth 32/04/2019--%>
                    <%--<asp:Panel ID="panel16" runat="server" CssClass="grid-view">

                                
                                <asp:GridView ID="GridView_yearly_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small" OnSelectedIndexChanging="GridView_fourth_master_SelectedIndexChanging" OnRowDataBound="GridView_fourth_master_RowDataBound">
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="White" />



                                    <Columns>

                                        <asp:BoundField DataField="Id"  HeaderText="Id" SortExpression="Id"/>
                                        <asp:BoundField DataField="client_code"  HeaderText="Client_Code" SortExpression="client_code"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy"  HeaderText="Unit_Code" SortExpression="branch_having_policy"/>
                                        <asp:BoundField DataField="state"  HeaderText="State" SortExpression="state"/>
                                        
                                        <asp:BoundField DataField="branch_not_having_policy"  HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy"/>
                                        <asp:BoundField DataField="designation"  HeaderText="Designation" SortExpression="designation"/>
                                        <asp:BoundField DataField="duty_hours"  HeaderText="Duty_hours" SortExpression="duty_hours"/>
                                        <asp:BoundField DataField="new_policy_name"  HeaderText="New_policy_name" SortExpression="new_policy_name"/>
                                        <asp:BoundField DataField="start_date"  HeaderText="Start_date" SortExpression="start_date"/>
                                        <asp:BoundField DataField="end_date"  HeaderText="End_date" SortExpression="end_date"/>
                                        <asp:BoundField DataField="comp_code"  HeaderText="Comp_code" SortExpression="comp_code"/>
                                        <asp:BoundField DataField="branch_having_policy_1"  HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1"/>
                                        
                                    </Columns>

                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                </asp:GridView>

                            </asp:Panel>--%>



                    <div class="row text-center">


                        <%--<asp:Button ID="btn_yearly_add" runat="server" class="btn btn-primary"
                                    Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                               
                        
                        <asp:Button ID="btn_yearly_delete" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="btn_yearly_close" runat="server" class="btn btn-danger" Text=" Close " />--%>
                    </div>




                </div>

                <div class="row text-center">



                    <asp:Button ID="btn_add" runat="server" class="btn btn-primary"
                        Text=" Save " OnClick="btnadd_Click" OnClientClick="return Req_validation();" />
                    <asp:Button ID="btn_update" runat="server" class="btn btn-primary"
                        Text=" Update" OnClick="btn_update_Click" OnClientClick="return Req_validation();" />
                 <%--   <asp:Button ID="btn_daily_delete" runat="server" OnClick="btn_daily_delete_Click" class="btn btn-primary" Text="Delete" />--%>
                    <asp:Button ID="btn_daily_close" runat="server" OnClick="btn_daily_close_Click" class="btn btn-danger" Text=" Close " />

                    <br />
                    <br />

                    <%-- new add gridview 26/04/2019--%>
                    <asp:Panel ID="panel10" runat="server" CssClass="grid" >


                        <asp:GridView ID="GridView_daily_master" class="table" runat="server" CellPadding="1" AutoGenerateColumns="false" ForeColor="#333333" Font-Size="X-Small"  OnSelectedIndexChanged="GridView_daily_master_SelectedIndexChanged" OnRowDataBound="GridView_daily_master_RowDataBound" OnPreRender="GridView_daily_master_PreRender1">
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="text-uppercase" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />



                            <Columns>

                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                <asp:BoundField DataField="client_name" HeaderText="Client Name" SortExpression="client_name" />
                                <asp:BoundField DataField="comp_code" HeaderText="Comp_code" SortExpression="comp_code" />
                                <asp:BoundField DataField="branch_having_policy" HeaderText="Unit_Code" SortExpression="branch_having_policy" />
                                <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />
                                <asp:BoundField DataField="branch_having_policy_1" HeaderText="Branch_Having_Policy" SortExpression="branch_having_policy_1" />
                                <asp:BoundField DataField="branch_not_having_policy" HeaderText="Branch_Not_Having_Policy" SortExpression="branch_not_having_policy" />
                                <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation" />
                                <asp:BoundField DataField="duty_hours" HeaderText="Duty_hours" SortExpression="duty_hours" />
                                <asp:BoundField DataField="new_policy_name" HeaderText="New_policy_name" SortExpression="new_policy_name" />
                                <asp:BoundField DataField="start_date" HeaderText="Start_date" SortExpression="start_date" />
                                <asp:BoundField DataField="end_date" HeaderText="End_date" SortExpression="end_date" />



                            </Columns>

                            <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />--%>


                        </asp:GridView>

                    </asp:Panel>


                    <br />





                    <%--<asp:Button ID="Button1" runat="server" class="btn btn-primary"
                                    Text=" Save" />
                               
                        
                        <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Delete"  />
                        <asp:Button ID="Button3" runat="server" class="btn btn-danger" Text=" Close " />--%>
                </div>

            </div>

        </div>
        <br />
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


