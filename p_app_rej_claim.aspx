<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_app_rej_claim.aspx.cs" Inherits="Default2" EnableEventValidation="false" %>

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

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
        function Req_validation() {

            var t_reason = document.getElementById('<%=txt_comments.ClientID %>');
            if (t_reason.value == "") {
                alert("Please Enter Comments for Rejection !!");
                t_reason.focus();
                return false;
            }
            return true;
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

        body {
            font-family: Verdana;
            font-size: 10px;
        }
    </style>
</head>
<body>
    <div class="panel-heading">
                <div class="row">
                   
                        <div style="text-align: center;margin-top:20px; color:black; font-size: small;"><h3><b><u>Approve/Reject Claims</u></b></h3></div>
                   
            </div>
    <form id="form1" runat="server">
        <div>
            <%--<br />
            <div class="row row_new_exp">
                <div class="col-sm-2 col-xs-12">
                    Date:
                
                                     <asp:TextBox ID="txt_date" runat="server" class="form-control date-picker"></asp:TextBox>

                </div>
                <div class="col-sm-2 col-xs-12">
                    Merchant:
                
                                     <asp:TextBox ID="txt_merchant" runat="server" class="form-control"></asp:TextBox>

                </div>

                <div class="col-sm-2 col-xs-12">
                    Category :
                            <asp:DropDownList ID="ddl_category" runat="server" CssClass="form-control text_box" DataTextField="category"
                                DataValueFiled="CATEGORY" meta:resourcekey="ddl_category1">
                                <asp:ListItem Value="0">Select_Category</asp:ListItem>
                                <asp:ListItem Value="Break Fast">Break Fast</asp:ListItem>
                                <asp:ListItem Value="Lunch">Lunch</asp:ListItem>
                                <asp:ListItem Value="Dinner">Dinner</asp:ListItem>
                                <asp:ListItem Value="Travelling">Travelling</asp:ListItem>
                                <asp:ListItem Value="Hotel Room">Hotel Room</asp:ListItem>
                            </asp:DropDownList>

                </div>


                <div class="col-sm-2 col-xs-12 text-right">
                    <div class="text-left">Amount:</div>
                    <asp:TextBox ID="txt_amount" runat="server" class="form-control text_box" MaxLength="7"
                        onkeypress="return isNumber(event)"></asp:TextBox>
                </div>
                <div class="col-sm-3 col-xs-12">
                    Description:
                
                                     <asp:TextBox ID="txt_description" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>

                </div>

            </div>
            <div class="text-center">
                <asp:Button ID="btn_add" runat="server" class="btn btn-primary"
                    Text=" Submit " OnClick="btn_add_Click" />
                <asp:Button ID="btnupdate" runat="server" class="btn btn-primary" OnClick="btnupdate_Click" Text="Update" TabIndex="2" />

            </div>
            <br />--%>
            <br /></br>
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="container-fluid" style="background: #f3f1fe; padding-top:10px; margin-bottom:15px; padding-left:20px; padding-bottom:15px; padding-top:15px; border-radius: 10px; border: 1px solid white ">
                    <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" ScrollBars="Auto">
                        <asp:GridView ID="gvexpenessadd" runat="server" class="table" AutoGenerateColumns="False"
                            OnRowDataBound="gv_paymentdetails_RowDataBound" CellPadding="3" ForeColor="#333333"
                            ShowFooter="True" DataKeyNames="id" BorderStyle="None">
                            <RowStyle ForeColor="#000066" />

                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_srnumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="expenses_id" HeaderText="expenses_id" />
                                <asp:BoundField DataField="id" HeaderText="id" />
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_city" runat="server" Text='<%# Eval("city_type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Travel type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_travel_type" runat="server" Text='<%# Eval("travel_type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Merchant ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_merchant" runat="server" Text='<%# Eval("Merchant") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Category" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Claimed Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_comment" Font-Size="X-Small" runat="server" onkeypress="return isNumber(event);" CssClass="form-control"
                                            OnTextChanged="txt_comment_TextChanged" HtmlEncode="false"
                                            AutoPostBack="true" Text='<%# Eval("app_amount")%>' Width="80"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description" runat="server" Text='<%# Eval("description")%>' Width="70"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_comments" runat="server" Text='<%# Eval("comments")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attachments">
                                    <ItemTemplate>

                                        <asp:ImageButton CommandArgument='<%#Eval("Path")%>'
                                            ID="Image1" runat="server" ImageUrl='<%#Eval("Path")%>' Width="50px" Height="50px" />

                                    </ItemTemplate>
                                </asp:TemplateField>
  <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_download" runat="server" Style="color: white" CausesValidation="false" CssClass="btn btn-primary" Text="DOWNLOAD" CommandArgument='<%# Eval("Path")%>' OnCommand="lnk_download_Command"></asp:LinkButton>
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
                <div class="col-sm-2 col-xs-12" id="comments_box" runat="server">
                    <asp:Label ID="commenrid" Font-Bold="true" Font-Size="Small" Text="Comments:" runat="server"></asp:Label>
                    <%--Comments:--%>
                </div>
                <div class="col-sm-4 col-xs-12" id="comments_box1" runat="server">

                    <asp:TextBox ID="txt_comments" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>

                </div>
                        <br />
                <div class="text-center">
                    <asp:Button ID="btn_approve" runat="server" class="btn btn-success" Text=" Approve " OnClick="btn_approve_Click" />
                    <asp:Button ID="btn_Seeking_Clarification" runat="server" OnClientClick="return Req_validation();" class="btn btn-danger" OnClick="btn_Seeking_Clarification_Click" Text=" Seeking Clarification " TabIndex="2" />
                    <asp:Button ID="btn_reject" runat="server" OnClientClick="return Req_validation();" class="btn btn-danger" OnClick="btn_reject_Click" Text=" Reject " TabIndex="2" />
                </div>
                        </div>
            </div>
            </div>
           

            <!-- Modal -->
            <%--<div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
    
                  <!-- Modal content-->
                  <div class="modal-content">
                    <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title">Images</h4>
                    </div>
                    <div class="modal-body">
                      <img class="img-rounded" id="modal_Img" runat="server" src='<%#Eval("image_path","~/{0}")%>' style="height:50px; width:50px;" />
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                  </div>
      
                </div>
              </div>--%>
        </div>
      </div>
    </form>
    
</body>
</html>
