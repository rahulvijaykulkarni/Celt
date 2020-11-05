<%@ Page Language="C#" AutoEventWireup="true" CodeFile="p_add_expencess1.aspx.cs" Inherits="Default2" EnableEventValidation="false" %>

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

    <link href="css/GridViewFreezeStyle.css" rel="stylesheet" type="text/css" />
    <script>
        function pageLoad() {

            $('.date-picker').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'dd/mm/yy',
                maxDate: 0,
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-date .ui-datepicker-month .ui-datepicker-year :selected").val();

                }
            });

            $(".date-picker").attr("readonly", "true");
        }



        function Req_validation() {
            var txt_date = document.getElementById('<%=txt_date.ClientID %>');
            var txt_merchant = document.getElementById('<%=txt_merchant.ClientID %>');
            var txt_amount = document.getElementById('<%=txt_amount.ClientID %>');
            var txt_description = document.getElementById('<%=txt_description.ClientID %>');
            var photo_upload = document.getElementById('<%=photo_upload.ClientID %>');

            if (txt_date.value == "") {
                alert("Please Select Date");

                txt_date.focus();
                return false;
            }
            if (txt_merchant.value == "") {
                alert("Please Enter Merchant");

                txt_merchant.focus();
                return false;
            }
            //if (SelectedText11 == "Select_Category") {
            //    alert("Please Select Category  !!!");
            //    ddl_category.focus();
            //    return false;
            //}
            if (txt_amount.value == "") {
                alert("Please Enter Amount");
                txt_amount.Value = "0";
                txt_amount.focus();
                return false;
            }
            if (txt_description.value == "") {
                alert("Please Enter The Description");

                txt_description.focus();
                return false;
            }
            //if (photo_upload.value == "") {
            //    //alert("Hello");
            //    alert("Please Select the Files");
            //    photo_upload.focus();
            //    return false;
            //}

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
        function submit_hide() {

            var dest_from = document.getElementById('<%=txt_date.ClientID %>');

            if ($(dest_from).val()) {
                $(".button_submit").hide();
            }
            else {
                $(".button_submit").show();
            }


        }
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
    <script type="text/javascript">
        $(function () {
            $("#Image4").elevateZoom({
                zoomType: "inner",
                cursor: "crosshair"
            });

        });
    </script>
    <style>
        .grid-view {
            height: auto;
            max-height: 175px;
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
                   
                        <div style="text-align: center;margin-top:20px; margin-bottom:20px; color:black; font-size: small;"><h3><b><u>New Claim Expense</u></b></h3></div>
                   
            </div>
    <form id="form1" runat="server" style="padding:5px 5px 5px 5px; margin:10px 10px 10px 10px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div>
                    <br />
                    <div class="row row_new_exp">
                        <div class="col-sm-2 col-xs-12">
                       <b> City Type:</b>
                        <asp:DropDownList ID="ddl_city" runat="server" CssClass="form-control" AutoPostBack="true" >
                               <asp:ListItem Value="0">Select</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class="col-sm-2 col-xs-12">
                           <b>  Particular :</b>
                                <asp:DropDownList ID="ddlparticular" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlparticular_SelectedIndexChanged">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Travel"> Travel</asp:ListItem>
                                    <asp:ListItem Value="Accomodation">Hotel Accomodation</asp:ListItem>
                                    <asp:ListItem Value="Food">Food</asp:ListItem>
                                  <%--  <asp:ListItem Value="Stay">Stay</asp:ListItem>--%>
                                   
                                </asp:DropDownList>
                        </div>
                         <div class="col-sm-2 col-xs-12" id="travel_mode" runat="server">
                             Mode :
                                <asp:DropDownList ID="ddl_travelmode" runat="server" class="form-control" OnSelectedIndexChanged="ddl_travelmode_SelectedIndexChanged" AutoPostBack="true">
                                   <asp:ListItem Value="Select">Select</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                            <div class="col-sm-2 col-xs-12" id="type" runat="server">
                            <b> Type :</b>
                                <asp:DropDownList ID="ddl_type" runat="server" class="form-control" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged" AutoPostBack="true">
                              <asp:ListItem Value="Select">Select</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                        
                        <div class="col-sm-2 col-xs-12">
                            <b> Date:</b>
                
                                     <asp:TextBox ID="txt_date" runat="server" Font-Size="X-Small" class="form-control date-picker" OnTextChanged="txt_date_Click" AutoPostBack="true"></asp:TextBox>

                        </div>
                        <div class="col-sm-2 col-xs-12">
                           <b>  Merchant:</b>
                
                                     <asp:TextBox ID="txt_merchant" runat="server" Font-Size="X-Small" class="form-control" onkeypress="return lettersOnly(event);"></asp:TextBox>

                        </div>
                          <div class="col-sm-2 col-xs-12" id="rate" runat="server">
                           <b> Total KM:</b>
                
                                     <asp:TextBox ID="txt_rate" runat="server" Font-Size="X-Small" class="form-control " OnTextChanged="txt_rate_TextChanged" AutoPostBack="true"></asp:TextBox>

                        </div>
                        </div>
                        <div class="row" style="margin-top:20px">
                        <div class="col-sm-2 col-xs-12 text-right">
                            <div class="text-left"> <b>Amount:</b></div>
                            <asp:TextBox ID="txt_amount" Font-Size="X-Small" runat="server" class="form-control text_box" MaxLength="7" value="0"
                                onkeypress="return isNumber(event)" OnTextChanged="txt_amount_Click" AutoPostBack="true">0</asp:TextBox>

                        </div>
                       
                        <div class="col-sm-4 col-xs-12">
                            
                            <b> Description:</b>
                
                                     <asp:TextBox ID="txt_description" Font-Size="X-Small" runat="server" class="form-control" TextMode="MultiLine" Height="35px" onkeypress="return  AllowAlphabet_address(event)"></asp:TextBox>

                        </div>
                            
                        <%--  <div class="col-sm-2 col-xs-12">
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

                </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-2 col-xs-12">
                            <br /><br />
                         <b>   Attachment :</b>
                        </div>
                        <div class="col-sm-4 col-xs-12">
                            <br /><br />
                            <asp:FileUpload ID="photo_upload" runat="server" meta:resourcekey="photo_uploadResource1" />
                         
                            <asp:Label ID="lbl_note" runat="server" Text="Note : Only JPG and PNG files with 5 MB size." ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-2 col-xs-12">
                            <%--<asp:ImageButton OnClick="Image1_Click" CommandArgument='<%#Eval("Path")%>'
                              ID="Image4" runat="server" ImageUrl='<%#Eval("Path")%>' Width="50px" Height="50px"/> 
                            --%>
                            <br /><br />
                            <asp:Image ID="Image4" runat="server" OnClick="image_click" Width="55px" Height="55px" ImageUrl="~/Images/placeholder.png" />
                        </div>
                        <asp:TextBox ID="txtdata" runat="server" Visible="false"></asp:TextBox>
                    </div>
                    <br />
                    <%--<div class="row text-center">
                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" meta:resourcekey="btnUploadResource1" OnClick="Upload_File" Text="Upload" />
                </div>--%>
                    <br />
                    <div class="text-center">
                        <asp:Button ID="btn_add" runat="server" class="btn btn-primary"
                            Text=" Save As Draft " OnClick="btn_add_Click" OnClientClick="return Req_validation();" />
                        <%--  <asp:Button ID="btnupdate" runat="server" class="btn btn-primary" OnClick="btnupdate_Click" Text="Update" TabIndex="2" />--%>

                        <asp:Button ID="btn_submit" CssClass="btn btn-primary button_submit" runat="server" Text="Submit" OnClientClick="return confirm('Once you Submit you cannot make changes?');" OnClick="btn_submit_Click" />
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 col-xs-12">
                            <div class="container-fluid" style="background: #f3f1fe; padding-top:10px; margin-bottom:15px; padding-left:20px; padding-bottom:15px; padding-top:15px; border-radius: 10px; border: 1px solid white ">
                            <asp:Panel ID="Panel1" runat="server" CssClass="grid-view" ScrollBars="Auto">
                                <asp:GridView ID="gvexpenessadd" runat="server" class="table" AutoGenerateColumns="False"
                                    OnRowDataBound="gvexpenessadd_RowDataBound" OnSelectedIndexChanged="expenses_data_edit" CellPadding="3" ForeColor="#333333"
                                    ShowFooter="True" BorderStyle="None" DataKeyNames="id">
                                    <RowStyle ForeColor="#000066" />

                                    <Columns>
                                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_removeitem" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Claim?');" OnClick="lnkbtn_removeitem_Click">
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
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_city_type" runat="server" Text='<%# Eval("city_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Merchant ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_merchant" runat="server" Text='<%# Eval("Merchant") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Travel Mode ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_travel_type" runat="server" Text='<%# Eval("travel_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("type")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Category" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Advance Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("travel_amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Amount">
                                            <ItemTemplate>
                                                <asp:TextBox Height="25" ID="txt_comment" runat="server" onkeypress="return isNumber(event);" CssClass="form-control"
                                                    OnTextChanged="txt_comment_TextChanged" HtmlEncode="false" Font-Size="X-Small"
                                                    AutoPostBack="true" Text='<%# Eval("Amount")%>' Width="80"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_app_amount" runat="server" Text='<%# Eval("app_amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lbl_description" runat="server" Text='<%# Eval("description")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_status" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_particular" runat="server" Text='<%# Eval("particular")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:CommandField ShowSelectButton="True" SelectText="Get Details" HeaderText="Get Details" />
                                        <asp:TemplateField HeaderText="Comments" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_comments" Visible="false" runat="server" Text='<%# Eval("comments")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Attachment">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Attachment" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DOWNLOAD FILE">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_download" runat="server" Style="color: white" CausesValidation="false" CssClass="btn btn-primary" Text="DOWNLOAD" CommandArgument='<%# Eval("Value")%>' OnCommand="lnk_download_Command"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <%-- <asp:TemplateField HeaderText="Download File">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_download" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="lnk_download_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <%-- <asp:TemplateField HeaderText="Drag Receipt(s)">
                            <ItemTemplate>
                                <asp:FileUpload ID="file_upload1" runat="server" meta:resourcekey="photo_uploadResource1" />
                                <asp:Image ID="Image1" runat="server" OnClick="image_click" Width="50px" Height="50px" ImageUrl="~/Images/placeholder.png" />
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Drag Receipt(s)">
                            <ItemTemplate>
                                  <asp:Button ID="btnUpload1" runat="server" CssClass="btn btn-primary" meta:resourcekey="btnUploadResource1" OnClick="Upload_File1" Text="Upload" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
           <%-- </ContentTemplate>--%>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_add" />
                <asp:PostBackTrigger ControlID="btn_submit" />
            </Triggers>
        <%--/asp:UpdatePanel>--%>
    </form>
</body>
</html>
