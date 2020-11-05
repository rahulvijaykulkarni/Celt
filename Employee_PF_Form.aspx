<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employee_PF_Form.aspx.cs" Inherits="Employee_PF_Form" Title="EMPLOYEE MASTER" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
    <title>EMPLOYEE PF FORM</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_header" runat="Server">
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="css/new_stylesheet.css" rel="stylesheet" />
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="Scripts/jquery-1.11.3.js"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
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

    <script type="text/javascript">
        $(function () {
            $('#<%=btn_PRINT.ClientID%>').click(function () {
                $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
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
        function openWindow() {
            window.open("html/EMPLOYEE PF FORM.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
        }

        function Req_validation() {
            var appoi_emp_id = document.getElementById('<%=appoi_emp_id.ClientID %>');
            if (appoi_emp_id.value == "") {
                alert("Please Enter Employee Code");
                appoi_emp_id.focus();
                return false;
            }
            $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
            return true;
        }
        window.onfocus = function () {
            $.unblockUI();

        }
    </script>
    <style>
        .grid-view {
            height: auto;
            max-height: 250px;
            overflow: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">

    <div class="container-fluid">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Panel ID="Panel3" runat="server" CssClass="panel panel-primary">

            <div class="panel-heading">
                <div class="row">

                    <div class="col-sm-1"></div>
                    <div class="col-sm-9">
                        <div style="color: #fff; font-size: small;" class="text-center text-uppercase"><b>EMPLOYEE PF FORM</b></div>
                    </div>
                    <div class="col-sm-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openWindow();return false;" Style="font-size: 15px;">
                            <asp:Image runat="server" ID="Image1" Width="20" Height="20" ToolTip="Help" ImageUrl="Images/help_ico.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:25px 25px 25px 25px; margin-bottom:20px; margin-top:20px">
                <div class="row">
                    <div class="text-center">

                        <asp:Button ID="btn_PRINT" runat="server" class="btn btn-large" Text="FORM_NO_11" OnClick="btn_PRINT_Click" />
                        <asp:Button ID="btn_form_2" runat="server" class="btn btn-large" Text="FORM_NO_2" OnClick="btn_form_2_Click" />

                        <asp:Button ID="btnclose" runat="server" class="btn btn-danger" OnClick="btnclose_Click" Text="Close" />
                    </div>
                </div>
                <asp:Panel ID="panel_Experiance_Letter" runat="server">
                    <br />
                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-sm-3 text-right">
                            <%-- <label for="ex2">--%>
                                 Employee Code/Name :
                                 <%--</label>--%>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="appoi_emp_id" runat="server" class="form-control" onkeypress="return AllowAlphabet_address(event)" MaxLength="10"></asp:TextBox>
                        </div>
                        <asp:Button ID="btn_Experiancesearch_Letter" runat="server" class="btn btn-primary" OnClick="btn_Experiancesearch_Letter_Click" Text="Search" OnClientClick="return Req_validation()" />

                    </div>

                    <br />
                    <asp:Panel ID="panem_exp_letter" runat="server" ScrollBars="Auto" CssClass="grid-view" class="panel-body">
                        <asp:GridView ID="gv_experianceletter" class="table" AutoGenerateColumns="False" runat="server" OnRowDataBound="expering_OnRowDataBound" Width="100%" Height="60px" OnSelectedIndexChanged="gv_Experiance_SelectedIndexChanged">
                            <%--OnRowEditing="RowEditing",OnRowDeleting="GridView1_RowDeleting"-    --%>
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">

                                    <ItemTemplate>
                                        <div class="text-center">
                                            <asp:LinkButton ID="lnkPrint" runat="server" Text="PRINT" OnClick="gv_Experiance_SelectedIndexChanged" CommandName="Print" ToolTip="Print"
                                                CommandArgument=''> </asp:LinkButton>
                                            <%--<asp:ImageButton ID="ButtonDelete" Visible="false" runat="server"   ImageUrl="~/Images/edit.png" Width="15px" Height="15px" CommandName="Edit" ToolTip="Edit"  OnClick="DepartmentGridView_SelectedIndexChanged"/>--%>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ID" Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />

                                    <ItemTemplate>
                                        <asp:Label ID="lbl_realiving_id" runat="server" Text='<%# Eval("EMP_CODE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Employee Name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />

                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Emp_Name1" runat="server" Text='<%# Eval("EMP_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Joining Date">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Joining_Date1" runat="server" Text='<%# Eval("JOINING_DATE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Designation">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_designation1" runat="server" Text='<%# Eval("GRADE_CODE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LOCATION">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_location" runat="server" Text='<%# Eval("LOCATION")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="CTC Per Month">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_salary1" runat="server" Text='<%# Eval("EARN_TOTAL")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Birth_Date">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_birthdaydate" runat="server" Text='<%# Eval("BIRTH_DATE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" BackColor="#A1DCF2" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </asp:Panel>
                    </div>
                </asp:Panel>

            </div>

        </asp:Panel>
    </div>
</asp:Content>


