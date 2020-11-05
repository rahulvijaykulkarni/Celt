<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Income_Tax.aspx.cs" Inherits="guest_new" Title="Income Tax" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cph_header" runat="server">
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
    <style>
        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
    <style>
        .label_text {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
        }

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
    </style>
    <script>

        function unblock() {
            $.unblockUI();
        }


        $(function () {

            $('#<%=btn_exel.ClientID%>').click(function () {

                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

            });

        });



    </script>
    <script type="text/javascript">

        function pageLoad() {

            $('.date-picker').datepicker({
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yy',
                maxDate: 0,
                yearRange: "1950",
                onClose: function (dateText, inst) {
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, 1));
                }

            });

            $(".date-picker").focus(function () {
                $(".ui-datepicker-month").hide();
            });

            $(".date-picker").attr("readonly", "true");
        }

        function Req_Validation() {
            var view_data = document.getElementById('<%=ddl_view_type.ClientID %>');
            var SelectedText1 = view_data.options[view_data.selectedIndex].text;

            var income_tax_no = document.getElementById('<%=txt_ref_no.ClientID %>');

            // select Gender

            if (SelectedText1 == "Select Type gender") {
                alert("Select Gender");
                view_data.focus();
                return false;
            }

            // Income tax date
            if (income_tax_no.value == "") {
                alert("Search Income Tax");
                income_tax_no.focus();
                return false;
            }

            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;

        }

        window.onfocus = function () {

            $.unblockUI();

        }


        $(document).ready(function () {
            var evt = null;
            isNumber(evt);
        });

        function lettersOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
               ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 65 || charCode > 90) &&
               (charCode < 97 || charCode > 122) && charCode == 37) {
                //alert("Enter letters only.");
                return false;
            }
            return true;
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


        function name_12() {
            var type = document.getElementById('<%=ddl_type.ClientID%>');
            var Selected_type = type.options[type.selectedIndex].text;
            var t_Since = document.getElementById('<%=txt_Since.ClientID %>');
            var t_Lower_Value = document.getElementById('<%=txt_Lower_Value.ClientID %>');
            var t_Upper_Value = document.getElementById('<%=txt_Upper_Value.ClientID %>');
            var t_ITax = document.getElementById('<%=txt_ITax.ClientID %>');
            var t_Cess = document.getElementById('<%=txt_Cess.ClientID %>');
            var t_Surcharge = document.getElementById('<%=txt_Surcharge.ClientID %>');


            if (Selected_type == "Select") {
                alert("Please Select Type !!!");
                type.focus();
                return false;

            }


            //t_Since   
            if (t_Since.value == "") {
                alert("Please Enter the Effective Since !!!");
                t_Since.focus();
                return false;
            }


            //t_Lower_Value
            if (t_Lower_Value.value == "") {
                alert("Please Enter the Lower Value !!!");
                t_Lower_Value.focus();
                return false;
            }



            //t_Upper_Value  
            if (t_Upper_Value.value == "") {
                alert("Please Enter the Upper Value !!!");
                t_Upper_Value.focus();
                return false;
            }

            //t_ITax    
            if (t_ITax.value == "") {
                alert("Please Enter the I.Tax % !!!");
                t_ITax.focus();
                return false;
            }


            //t_Cess    
            if (t_Cess.value == "") {
                alert("Please Enter the Cess !!!");
                t_Cess.focus();
                return false;
            }


            //t_Surcharge    
            if (t_Surcharge.value == "") {
                alert("Please Enter the Surcharge !!!");
                t_Surcharge.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        function openWindow() {
            window.open("html/Income_Tax.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }
        function R_Validation() {
            var ddl_view_type = document.getElementById('<%=ddl_view_type.ClientID%>');
            var Selected_ddl_view_type = ddl_view_type.options[ddl_view_type.selectedIndex].text;
            var txt_ref_no = document.getElementById('<%=txt_ref_no.ClientID %>');
             if (Selected_ddl_view_type == "Select Type gender") {
                 alert("Please Select Gender");
                 ddl_view_type.focus();
                 return false;
             }
             if (txt_ref_no.value == "") {
                 alert("Please Select Income Tax Year");
                 txt_ref_no.focus();
                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }
    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div class="container-fluid">

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Income Tax</b></div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>Income Tax Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="border: 1px solid #ddd9d9; background: #f3f1fe; border-radius: 10px; margin-bottom:20px; margin-top:20px; padding:15px 15px 15px 15px;">
                    <br />
                    <div class="row">
                        <div class="col-sm-3 col-xs-12">
                            <asp:Label ID="lbl_view_type" runat="server" Text="Select Gender :" Font-Bold="true"></asp:Label>
                            <br />
                            <br />
                            <asp:DropDownList ID="ddl_view_type" class="form-control text_box" runat="server">
                                <asp:ListItem Value="Select Type gender">Select Type gender</asp:ListItem>
                                <asp:ListItem Value="Senior Citizen">Senior Citizen</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-xs-12">
                            <b>Search Income Tax (Year) :</b>
                            <br />
                            <br />
                            <asp:TextBox ID="txt_ref_no" runat="server" class="form-control date-picker text_box"></asp:TextBox>
                        </div>
                        <br />
                        <br />
                       

                        <div class="col-sm-3 col-xs-12">
                            <asp:Button ID="btn_new" runat="server" class="btn btn-primary" OnClientClick="return R_Validation();" Text="New" OnClick="btn_new_Click" BackColor="#428BCA" />
                            <asp:Button ID="btn_search" runat="server" class="btn btn-large" Text="View Data" OnClick="btn_search_Click1" OnClientClick="return Req_Validation();" BackColor="#428BCA" />
                            <%--<asp:Button ID="btn_close1" runat="server" CssClass="btn btn-danger" onclick="btn_Close_Click"  Text="Close" />--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-5"></div>
                        <div class="col-sm-3">
                            <asp:Label ID="lbl_error" Text="No Record Found." runat="server" ForeColor="Red" Font-Bold="True" Font-Size="X-Large" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <br />
                </div>
                <br />

                <asp:Panel ID="Panel1" runat="server" Style="text-align: center" class="panel-body">
                    <div class="container-fluid" style="border: 1px solid #ddd9d9; background: beige; border-radius: 10px;">
                        <br />
                        <div class="row">
                            <div class="col-sm-2 col-xs-12 text-left">
                                <asp:Label ID="lbl_type" runat="server" Text="Type :"></asp:Label>

                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:DropDownList ID="ddl_type" class="form-control" runat="server">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Senior Citizen">Senior Citizen</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 col-xs-12 text-left">
                                <asp:Label ID="lbl_Since" runat="server" Text="Effective Since : "></asp:Label>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_Since" runat="server" class="form-control date-picker"></asp:TextBox>

                            </div>
                            <div class="col-sm-2 col-xs-12 text-left">
                                <asp:Label ID="lbl_Lower_Value" runat="server" Text="Lower Value :"></asp:Label>
                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_Lower_Value" runat="server" class="form-control" MaxLength="10" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-xs-12 text-left">
                                <asp:Label ID="lbl_Upper_Value" runat="server" Text="Upper Value :"></asp:Label>
                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_Upper_Value" runat="server" onkeypress="return isNumber(event)" class="form-control" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-xs-12 text-left">
                                <asp:Label ID="lbl_ITax" runat="server" Text="ITax % :"></asp:Label>
                                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_ITax" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-xs-12 text-left">
                                <asp:Label ID="lbl_Cess" runat="server" Text="Cess :"></asp:Label>
                                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_Cess" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-xs-12 text-left ">
                                <asp:Label ID="lbl_Surcharge" runat="server" Text="Surcharge :"></asp:Label>
                                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text=" * "></asp:Label>
                                <asp:TextBox ID="txt_Surcharge" runat="server" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-xs-12 text-left">
                                <br />
                                <asp:LinkButton ID="lnkbtn_add_icon" runat="server" OnClick="lnkbtn_add_icon_Click" OnClientClick="return name_12();"
                                    TabIndex="12"><img alt="Add Item"  src="Images/add_icon.png"  /></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" CssClass="grid-view">
                            <asp:GridView ID="gv_itemslist" runat="server" BackColor="White" class="table" Style="text-align: center"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                Width="100%" OnRowDataBound="gv_itemslist_RowDataBound"
                                AutoGenerateColumns="False">
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtn_removeitem" runat="server" CausesValidation="false"
                                                OnClick="lnkbtn_removeitem_Click"><img alt="" height="15" 
                                                    src="Images/Delete-icon.png" width="15" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Sr_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Gender">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_type" runat="server" Style="text-align: center" Text='<%# Eval("type")%>' class="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Effective Since">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_since" runat="server" Style="text-align: center" Text='<%# Eval("Effective_Since")%>' class="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Lower Value">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_lower_value" runat="server" Style="text-align: center" Text='<%# Eval("Lower_Value")%>' class="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Upper Value">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle />
                                        <ItemStyle HorizontalAlign="center" />
                                        <HeaderStyle HorizontalAlign="center" />

                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_Upper_Value" runat="server" Style="text-align: center" onkeypress="return isNumber(event)" class="form-control" Text='<%# Eval("Upper_Value")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="I.Tax %">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_ITax" runat="server" Style="text-align: center" class="form-control" Text='<%# Eval("ITax")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Cess">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_cess" runat="server" Style="text-align: center" class="form-control" Text='<%# Eval("Cess")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Surcharge">
                                        <HeaderStyle HorizontalAlign="center" />
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbl_surcharge" runat="server" Style="text-align: center" class="form-control" Text='<%# Eval("Surcharge")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </asp:Panel>
                        <div class="row text-center">
                            <asp:Button ID="btn_Save" runat="server" class="btn btn-primary" Text="Save" OnClientClick="return R_Validation();" OnClick="btn_add_Click" />
                            <asp:Button ID="btn_Update" runat="server" class="btn btn-primary" Text="Update" OnClick="btnupdate_Click" />
                            <asp:Button ID="btn_Delete" runat="server" class="btn btn-primary" Text="Delete" OnClick="btndelete_Click" />

                            <cc1:ConfirmButtonExtender ID="btn_Delete_ConfirmButtonExtender" runat="server"
                                ConfirmText="Are you sure you want to delete record?"
                                Enabled="True" TargetControlID="btn_Delete"></cc1:ConfirmButtonExtender>

                            <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text=" Cancel " OnClick="btn_cancel_Click" />
                            <asp:Button ID="btn_exel" runat="server" class="btn btn-primary" Text="Export To Excel" OnClick="btnexporttoexceldesignation_Click" />
                            <asp:Button ID="btn_Close" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btn_Close_Click" TabIndex="14" Text="Close" />
                        </div>
                        <br />
                    </div>
                </asp:Panel>


            </div>
        </asp:Panel>
    </div>
</asp:Content>
