<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_add_new_travel_plan2.aspx.cs" Inherits="p_add_new_travel_plan2"
    EnableEventValidation="false" %>

<html>
<head>
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

    <script>
        function pageLoad() {

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
        }



        function Req_validation() {


            var txt_dest_from = document.getElementById('<%=txt_dest_from.ClientID %>');
            var txttodeg = document.getElementById('<%=txttodeg.ClientID %>');
            var txt_from_date = document.getElementById('<%=txt_from_date.ClientID %>');
            var txt_to_date = document.getElementById('<%=txt_to_date.ClientID %>');
            var ddl_currency = document.getElementById('<%=ddl_currency.ClientID %>');
            var SelectedText11 = ddl_currency.options[ddl_currency.selectedIndex].text
            var txt_amout = document.getElementById('<%=txt_amout.ClientID %>');

            // alert("Hello");
            //if (SelectedText1 == "") {
            //    alert("Please Select Mode !!!");
            //    emp_category.focus();
            //    return false;
            //}
            if (txt_dest_from.value == "") {
                alert("Please Enter Destination From");

                txt_dest_from.focus();
                return false;
            }
            if (txttodeg.value == "") {
                alert("Please Enter Destination To");

                txttodeg.focus();
                return false;
            }
            if (txt_from_date.value == "") {
                alert("Please Select From Date");

                txt_from_date.focus();
                return false;
            }
            if (txt_to_date.value == "") {
                alert("Please Select To Date");

                txt_to_date.focus();
                return false;
            }
            if (SelectedText11 == "Select") {
                alert("Please Select Currency !!!");
                ddl_currency.focus();
                return false;
            }
            if (txt_amout.value == "") {
                alert("Please Enter Amount");

                txt_amout.focus();
                return false;
            }

        }



        function submit_hide() {

            var dest_from = document.getElementById('<%=txt_dest_from.ClientID %>');

            if ($(dest_from).val()) {
                $(".button_submit").hide();
            }
            else {
                $(".button_submit").show();
            }

        }
        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
               ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
               (charCode < 97 || charCode > 122)) {
                //alert("Enter letters only.");
                return false;
            }
            return true;
        }
        function AllowAlphabet_address(e) {
            if (null != e) {
                isIE = document.all ? 1 : 0
                keyEntry = !isIE ? e.which : e.keyCode;
                if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry < '31') || ((keyEntry >= '48') && (keyEntry <= '57')) ||
                    (keyEntry == '32') || (keyEntry == '38') || ((keyEntry == '39') && (keyEntry == '34')) || (keyEntry == '44') || ((keyEntry >= '45') && (keyEntry <= '47')) ||
                    (keyEntry == '58') || (keyEntry == '59') || (keyEntry == '61') || (keyEntry == '92') || (keyEntry == '32'))
                    return true;
                else {
                    // alert('Please Enter Only Character values.');
                    return false;
                }
            }
        }

        function allow_space() {
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32) {
                return true;
            } else {
                return false;
            }

        }

        function isNumber(evt) {
            if (null != evt) {
                evt = (evt) ? evt : window.event;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    if (charCode == 46) {
                        return true;
                    }
                    return false;
                }

            }
            return true;
        }

    </script>
    <style>
        .grid-view {
            height: auto;
            max-height: 200px;
            overflow: scroll;
        }

        .table {
            width: 100%;
        }

        .row {
            margin: 0px;
        }

        body {
            font-family: Verdana;
            font-size: 10px;
        }

        .form-control {
            font-size: 12px;
        }

        .shadow {
            box-shadow: 3px 3px 3px 3px #ccc;
        }
    </style>
</head>
<body>

    <div class="panel-heading">
                <div class="row">
                   
                        <div style="text-align: center;margin-top:20px; color:black; font-size: small;"><h3><b><u>New Travelling Plan</u></b></h3></div>
                   
            </div>
          </div>  
    <form id="form1" runat="server" style="padding:10px 10px 10px 10px; margin:10px 10px 10px 10px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div id="newrow" class="row" style="margin-top:20px">
                    <br />
                      <div class="col-sm-2 col-xs-12">
                       <b> City Type:</b>
                        <asp:DropDownList ID="ddl_city" runat="server" CssClass="form-control" AutoPostBack="true" >
                               <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-xs-12">
                       <b> Mode:</b>
                        <asp:DropDownList ID="emp_category" runat="server" CssClass="form-control" OnSelectedIndexChanged="emp_category_SelectedIndexChanged" AutoPostBack="true">
                               <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                       
                      
                        <asp:CheckBox ID="chk_exception" runat="server" Text="Exception" OnCheckedChanged="chk_exception_CheckedChanged" AutoPostBack="true" />
                        <br />
                        <br />
                        
                    </div>
                    <div id="newrow2" class="row" >
                     <div class="col-sm-2 col-xs-12" id="type_ddl" runat="server">
                      <b>  Types:</b>
                                <asp:DropDownList ID="ddl_type_bus" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_type_bus_SelectedIndexChanged" />
                     </div>
                   </div>
                    <br />
                    <div class="col-sm-2 col-xs-12">
                      <b>  Destination From:</b>
                                <asp:TextBox ID="txt_dest_from" runat="server" class="form-control text_box " onchange="submit_hide()" onkeypress="return  AllowAlphabet_address(event)"
                                    MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-xs-12 ">
                        <b>DestinationTo:</b>
               
                            <asp:TextBox ID="txttodeg" runat="server" class="form-control text_box"
                                MaxLength="50" onkeypress="return  AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-2 col-xs-12 ">
                       
                       <b> From Date:</b>
                
                             <asp:TextBox ID="txt_from_date" runat="server" class="form-control date-picker1"></asp:TextBox>

                    </div>

                    <div class="col-sm-2 col-xs-12">
                        <b>To Date:</b>
               
                                         <asp:TextBox ID="txt_to_date" runat="server" class="form-control date-picker2"
                                             MaxLength="50"></asp:TextBox>
                    </div>
                       
                    <div class="col-sm-2 col-xs-12">
                        <br />
                       


                        <asp:CheckBox ID="chk_roundtrip" runat="server" Text=" Round Trip" />


                    </div>
                        

                    <%-- <div class="col-sm-2 col-xs-9 ">
                    Traveling_plan_id:
               
                            <asp:TextBox ID="txt_traveling_id" runat="server" class="form-control text_box"
                                MaxLength="50"></asp:TextBox>
                </div>--%>
                </div>
                <br />
                <br />
                <br />
                <div class="row">

                    <div class="col-sm-2  text-right">
                        <div class="text-left"><b>Currency:</b></div>
                        <asp:DropDownList ID="ddl_currency" runat="server" class="form-control"
                            AutoPostBack="true">
                             <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="RS">RS</asp:ListItem>
                        </asp:DropDownList>

                        <%--<button type="button" class="btn btn-info btn-xs text-right" data-toggle="modal" data-target="#myModal">More</button>--%>

                    </div>
                   
                     
                     <div class="col-sm-2 col-xs-8" id="rate" runat="server">
                           <b>Total KM:</b>
                                     <asp:TextBox ID="txt_rate" runat="server" Font-Size="X-Small" class="form-control " OnTextChanged="txt_rate_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    <div class="col-sm-2 text-right">
                        <div class="text-left"><b>Amount:</b></div>
                        <asp:TextBox ID="txt_amout" runat="server" class="form-control text_box" MaxLength="7" onkeypress="return isNumber(event)"></asp:TextBox>
                    </div>
                   
                    <div class="col-sm-6 col-xs-12 ">
                        
                      <b>  Description about Travel Plan:</b>
               
                            <asp:TextBox ID="txt_add" runat="server" class="form-control text_box" TextMode="MultiLine" onKeyPress="return  AllowAlphabet_address(event)"></asp:TextBox>
                    </div>
                </div>
                
                <br /><br />
                <div class="row text-center">
                    <asp:Button ID="btn_save" CssClass="btn btn-primary" runat="server" Text="Save As Draft" OnClick="btn_save_Click" OnClientClick=" return Req_validation();" />

                    <%--<asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update" OnClick="btn_update_Click" OnClientClick="return Req_validation();" />--%>

                    <asp:Button ID="btn_submit" CssClass="btn btn-primary button_submit" runat="server" Text="Submit" OnClientClick="return confirm('Once you Submit you cannot make changes?');" OnClick="btn_submit_Click" />

                </div>

          <%--      <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Currency Description</h4>
                            </div>

                            <div class="modal-body">
                                <asp:Panel runat="server" class="grid-view" ScrollBars="Auto">
                                    <asp:GridView ID="gv_currency" runat="server" class="table" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3">

                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />

                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Currency Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Eval("currency_desp")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Currency Countries">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFROMDESIG" runat="server" Text='<%# Eval("currency_countries")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>--%>

                <br />
                <div class="row">
                    <div class="col-sm-12 col-xs-12">
                        <div class="container-fluid" style="background: #f3f1fe; padding-top:10px; margin-left:2px; margin-bottom:15px; padding-left:20px; padding-bottom:15px; padding-top:15px; margin-right:2px; border-radius: 10px; border: 1px solid white ">
                        <asp:Panel ID="newpanel" runat="server" CssClass="grid-view text-center" ScrollBars="Auto">
                            <asp:GridView ID="gv_paymentdetails" runat="server" class="table" AutoGenerateColumns="False"
                                CellPadding="3" ForeColor="#333333" Font-Size="Small"
                                ShowFooter="True" BorderStyle="None" OnSelectedIndexChanged="gv_paymentdetails_SelectedIndexChanged"
                                OnRowDataBound="gv_paymentdetails_RowDataBound">
                                <RowStyle ForeColor="#000066" />

                                <Columns>
                                    <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this Travel Plan?');" OnClick="lnkbtn_removeitem_Click">  <%--OnClientClick="return confirm('Are you sure you want to delete this Travel Plan?');"--%>
                                                            <img alt="Delete" height="15" src="Images/Delete-icon.png" width="15" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="expenses_id" HeaderText="expenses_id" />
                                    <asp:BoundField DataField="id" HeaderText="id" />
                                      <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_city_type" runat="server" Text='<%# Eval("city_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_travel_mod" runat="server" Text='<%# Eval("travel_mode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exception">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_exception" runat="server" Text='<%# Eval("exception_case")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form Destination">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_from_designation" runat="server" Text='<%# Eval("from_designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Destination">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_to_designation" runat="server" Text='<%# Eval("to_designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_from_date" runat="server" AutoPostBack="True" Text='<%# Eval("from_date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_to_date" runat="server" AutoPostBack="true" Text='<%# Eval("to_date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_curreny" runat="server" AutoPostBack="true" Text='<%# Eval("curreny_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("adv_amount")%>' onkeypress="return isNumber(event)"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_add" runat="server" Text='<%# Eval("Add_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>

                </div>


                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</body>
</html>
