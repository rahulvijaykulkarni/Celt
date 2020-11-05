<%@ Page Language="C#" AutoEventWireup="true" CodeFile="size_details.aspx.cs" Inherits="size_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link href="css/new_stylesheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                   <asp:Panel ID="Panel3" CssClass="grid-view" runat="server">
                                <asp:GridView ID="gv" CssClass="table" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="X-Small"
                                    CellPadding="3"
                                    ShowFooter="false">

                                    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#000000" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />


                                    <Columns>
                                    <asp:TemplateField HeaderText="Sr No.">
                                                                  <ItemStyle Width="20px" />
                                                                  <ItemTemplate>
                                                                      <asp:Label ID="lbl_srnumber" runat="server" Text='<%# Container.DataItemIndex+1 %>' Width="20px"></asp:Label>
                                                                  </ItemTemplate>
                                                              </asp:TemplateField>
                                         <asp:BoundField DataField="emp_code" HeaderText="emp_code" SortExpression="emp_code" />
                                        <asp:BoundField DataField="No_of_set" HeaderText="No_of_set" SortExpression="No_of_set" />
                                        <asp:BoundField DataField="size" HeaderText="size" SortExpression="size" />
                                        <asp:BoundField DataField="dispatch_date" HeaderText="dispatch_date" SortExpression="dispatch_date" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    <div>
   
    </div>
    </form>
</body>
</html>
