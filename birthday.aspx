<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="birthday.aspx.cs" Inherits="birthday" Title="Today's Birthdays" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="cph_title">
    <title>Birthday Master</title>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cph_header" runat="server">
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <style>
        .container {
            max-width: 99%;
        }

        .grid-view {
            height: auto;
            max-height: 500px;
            overflow: scroll;
        }
    </style>
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
        $(document).ready(function () {
            $(".flip1").click(function () {
                $(".panel-disp1").slideToggle("slow");
            });
            $(".flip2").click(function () {
                $(".panel-disp2").slideToggle("slow");
            });
            $(".flip3").click(function () {
                $(".panel-disp3").slideToggle("slow");
            });

        });

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
    </script>
    <script>
        function openWindow() {
            window.open("html/birthday.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta charset="utf-8" />


    <script src="js/bootstrap.js" type="text/javascript"></script>

    <script src="js/bootstrap.js"></script>

    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/datetimepicker.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="Scripts/datatables.js"></script>

    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.min.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid">



        <asp:Panel ID="Panel2" runat="server" CssClass="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>Birthday Master</b></div>
                    </div>
                    <div class="col-sm-2 text-right">

                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 10px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                <div class=" panel panel-primary flip1" style="margin-bottom: -5px;">
                    <div class="panel-heading text-center" style="padding: -20px">
                        <div class="head-space" style="font-size: small;"><b>Today's Birthdays</b></div>
                    </div>
                </div>
                <br />
                <asp:Panel ID="Panel1" class="panel panel-primary panel-disp1" runat="server" ScrollBars="Auto" Height="250px">

                    <div class="panel-body" onscroll="yes">
                        <asp:DataList ID="DataList1" runat="server" BorderColor="#ffffff"
                            BorderStyle="None" BorderWidth="0px" CellPadding="3" CellSpacing="2" class="table"
                            Font-Names="Verdana" Font-Size="X-Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Horizontal"
                            Height="250px">

                            <FooterStyle BackColor="#D1DDF1" ForeColor="#8C4510" />

                            <HeaderStyle BackColor="#428BCA" Font-Bold="True" Font-Size="Large" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle BackColor="White" ForeColor="black" BorderWidth="1px" />




                            <ItemTemplate>
                                <div class="row">
                                    <br />
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-2 col-xs-12">
                                        <%--<asp:ImageField DataImageUrlField="emp_photo" NullImageUrl="~/Images/placeholder.PNG" ControlStyle-Height="75" ControlStyle-Width="75" HeaderText="EMP IMAGE" />--%>
                                        <asp:ImageButton ID="Image12" runat="server" CssClass="img-responsive" meta:resourcekey="Image12Resource1" Height="130px" Width="100%" ImageUrl='<%# Eval("emp_photo") %>' Style="padding-left: 40px" /><br />
                                    </div>

                                    <div class="col-sm-4 col-xs-12">
                                        <asp:Label ID="lbl_emp_code" runat="server" Text='<%# Eval("emp_code") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_Employee_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt_enter_message" CssClass="form-control" Rows="1" placeholder="Write a birthday wishes..." TextMode="MultiLine" runat="server" onkeypress="return AllowAlphabet_address(event)" />

                                        <br />
                                        <br />
                                        <asp:Button ID="btn_send_notification" CssClass="btn btn-success" Text="Send Notification" OnClick="send_notification" runat="server" />
                                    </div>
                                </div>

                            </ItemTemplate>

                        </asp:DataList>
                    </div>


                </asp:Panel>

                <div class=" panel panel-primary flip2" style="margin-bottom: -5px;">
                    <div class="panel-heading text-center">
                        <div class="head-space" style="font-size: small;"><b>Recent Birthdays</b></div>
                    </div>
                </div>
                <br />
                <asp:Panel ID="Panel4" class="panel panel-primary panel-disp2" runat="server" ScrollBars="Auto" Height="250px">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-8">
                                <br />
                                <asp:GridView ID="GridView2" class="table" AutoGenerateColumns="False" runat="server" Font-Size="X-Small">
                                    <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-  OnRowDataBound="OnRowDataBound"  OnSelectedIndexChanged ="SearchGridView_SelectedIndexChanged"  --%>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMP NAME">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Start_Date" runat="server" Text='<%# Eval("emp_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMP GRADE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Task_Name" runat="server" Text='<%# Eval("Grade_desc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="BIRTH DATE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Task_Description" runat="server" Text='<%# Eval("birth_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ImageField DataImageUrlField="emp_photo" ControlStyle-Height="75" NullImageUrl="~/Images/placeholder.PNG" ControlStyle-Width="75" HeaderText="EMP IMAGE" />

                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class=" panel panel-primary flip3" style="margin-bottom: -5px;">
                    <div class="panel-heading text-center">
                        <div class="head-space" style="font-size: small"><b>Upcoming Birthdays </b></div>
                    </div>
                </div>
                <br />
                <asp:Panel ID="Panel3" class="panel panel-primary panel-disp3" runat="server" ScrollBars="Auto" Height="250px">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-2"></div>
                            <div class="col-sm-8">
                                <asp:GridView ID="GridView1" class="table" AutoGenerateColumns="False" runat="server" Font-Size="X-Small">
                                    <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-  OnRowDataBound="OnRowDataBound"  OnSelectedIndexChanged ="SearchGridView_SelectedIndexChanged"  --%>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMP NAME">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Start_Date" runat="server" Text='<%# Eval("emp_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMP GRADE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Task_Name" runat="server" Text='<%# Eval("Grade_desc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="BIRTH DATE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Task_Description" runat="server" Text='<%# Eval("birth_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ImageField DataImageUrlField="emp_photo" NullImageUrl="~/Images/placeholder.PNG" ControlStyle-Height="75" ControlStyle-Width="75" HeaderText="EMP IMAGE" />

                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <HeaderStyle BackColor="#424D7A" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                <br />
                <div class="row text-center">
                    <asp:Button ID="btn_Close" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btn_Close_Click" Text="Close" />
                </div>
                <br />    

            </div>

        </asp:Panel>
    </div>


</asp:Content>
