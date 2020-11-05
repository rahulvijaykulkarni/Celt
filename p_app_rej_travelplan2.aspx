<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_app_rej_travelplan2.aspx.cs" Inherits="p_add_new_travel_plan2"
    EnableEventValidation="false" %>

<html>
<head>
    <title></title>
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

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Req_validation() {

            var txt_comments = document.getElementById('<%=txt_comments.ClientID %>');
             if (txt_comments.value == "") {
                 alert("Please Enter Comment for Rejection !!");
                 txt_comments.focus();
                 return false;
             }
             else {
                 confirm('Once you Submit you cannot make changes?');
             }
             return true;

         }

    </script>

    <script type="text/javascript">
        function approve() {
            alert("Add Travel Plan Approved");
        }

        function Rejected() {
            alert("Add Travel Plan Rejected");
        }
    </script>

    <style>
        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }

        .table {
            width: 100%;
        }

        .row {
            margin: 0px;
        }
    </style>
</head>
<body>
    <div class="panel-heading">
                <div class="row">
                   
                        <div style="text-align: center;margin-top:20px; color:black; font-size: small;"><h3><b><u>Approve/Reject Travel Plan</u></b></h3></div>
                   
            </div>
    <form id="form1" runat="server">
        <div>


            <br /><br />
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="container-fluid" style="background: #f3f1fe; padding-top:10px; margin-bottom:15px; padding-left:20px; padding-bottom:15px; padding-top:15px; border-radius: 10px; border: 1px solid white ">
                    <asp:Panel ID="newpanel" runat="server" CssClass="grid-view text-center" ScrollBars="Auto">
                        <asp:GridView ID="gv_paymentdetails" runat="server" class="table" AutoGenerateColumns="False"
                            CellPadding="3" ForeColor="#333333" Font-Size="X-Small"
                            ShowFooter="True" BorderStyle="None"
                            OnRowDataBound="gv_paymentdetails_RowDataBound">
                            <RowStyle ForeColor="#000066" />

                            <Columns>

                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="expenses_id" HeaderText="expenses_id" />
                                <asp:BoundField DataField="id" HeaderText="id" />
                                <asp:TemplateField HeaderText="City Type">
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
                                        <asp:Label ID="lbl_from_date" runat="server" Text='<%# Eval("from_date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_to_date" runat="server" Text='<%# Eval("to_date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_curreny" runat="server" Text='<%# Eval("curreny_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("adv_amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("expense_status")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <div class="col-sm-2 col-xs-12" id="cmt_txt" runat="server">
                        <asp:Label ID="commenrid" Font-Bold="true" Font-Size="Small" Text="Comments:" runat="server"></asp:Label>
                        <%--Comments:--%>
                    </div>
                    <div class="col-sm-4 col-xs-12" id="cmt_txt1" runat="server">
                        <asp:TextBox ID="txt_comments" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btn_approve" runat="server" OnClientClick="approve();" class="btn btn-success" Text=" Approve " OnClick="btn_approve_Click" />
                        <asp:Button ID="btn_Seeking_Clarification" runat="server" OnClientClick="return Req_validation();" class="btn btn-danger" OnClick="btn_Seeking_Clarification_Click" Text=" Seeking Clarification " />
                        <asp:Button ID="btn_reject" runat="server" OnClientClick="return confirm('Are you sure you want to Reject?');" class="btn btn-danger" OnClick="btn_reject_Click" Text=" Reject " TabIndex="2" />
                    </div>
                </div>

            </div>


        </div>
    </form>
</body>
</html>
