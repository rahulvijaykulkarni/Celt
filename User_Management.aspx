<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="User_Management.aspx.cs" Inherits="User_Management" Title="USER MANAGEMENT FORM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_title" runat="Server">
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

    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <script>

        $(function () {

            $('#<%=btnupdate.ClientID%>').click(function () {


                 $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });

             });

         });
    </script>
    <script type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        checkUncheckSwitch = true;
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            var evt = null;
            isNumber(evt);

        });

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



        function Validate() {

            var role = document.getElementById('<%=DropDownList1.ClientID%>');
             var confirmPassword = document.getElementById('<%=txt_con_password.ClientID%>');
             var password = document.getElementById('<%=txt_password.ClientID%>');
             var login = document.getElementById('<%=ddl_loginid.ClientID%>');

             if (password.value.length == 0 || password.value.length == "" && confirmPassword.value.length == 0) {
                 alert("Please Fill Details Of all Fields...!!!");
                 password.focus();
                 return false;
             }
             if (password.value != confirmPassword.value) {
                 alert("New Password & Confirm New Password do not match.");
                 return false;
             }

             if (password.value.length <= 8 || confirmPassword.value.length <= 8) {
                 alert("Please enter new password more than 8 bytes.");
                 return false;
             }


             if (role.value == 0) {
                 alert("Please Select Any Role!!!");

                 return false;
             }
             $.blockUI({ overlayCSS: { backgroundColor: '#CCCCCC' } });
             return true;
         }

         window.onfocus = function () {
             $.unblockUI();
         }


         function openWindow() {
             window.open("html/Usermgm.html", 'popUpWindow', 'height=500,width=600,left=100,top=100,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes, status=No');
         }

    </script>


    <!DOCTYPE html>

    <html>
    <head>
        <title>User Management Form</title>
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



    </head>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_righrbody" runat="Server">
    <div class="container-fluid">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<asp:UpdatePanel runat="server"><ContentTemplate>--%>
        <asp:Panel ID="Panel2" runat="server" Enabled="True">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-9">
                            <div style="text-align: center; color: white; font-size: small"><b>USER MANAGEMENT FORM</b> </div>
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
                        <div style="text-align: left; color:white; font-size: small;"><b>User Management Form Details</b></div>
                    </div>
                    <div class="col-sm-2 text-left">
                        
                    </div>
                </div>
            </div>

                <div class="panel-body">
                    <div class="container-fluid" style="background: #f3f1fe; border: 1px solid #e2e2dd; border-radius: 10px; padding:15px 15px 15px 15px; margin-bottom:20px; margin-top:20px">
                    <div class="jambotron">

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <br />
                               <b> Login Id :</b>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_loginid" DataValueField="login_id" DataTextField="login_name" runat="server" CssClass="form-control text_box" AutoPostBack="true" OnSelectedIndexChanged="ddl_loginid_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <br />
                               <b> Password :</b>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_password" runat="server" class="form-control text_box" placeholder="Please Enter Password" TextMode="Password" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <br />
                              <b>  Confirm Password :</b>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_con_password" runat="server" class="form-control text_box" placeholder="Please Enter Confirm Password" TextMode="Password" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <br />
                               <b> Select Role :</b>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control text_box">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-2 text-right">
                                <br />
                               <b> Active Flag :</b>
                            </div>
                            <div class="col-sm-1">
                                <br />
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        
                        <div class="row text-center">

                            <%-- <asp:Button ID="btn_login" runat="server" Text="Register"
                                   class="btn btn-primary"  OnClientClick="return Validate();"  meta:resourcekey="btn_addResource1"  OnClick="btn_login_Click" BackColor="#428BCA"  />
                          
                                    &nbsp&nbsp<asp:Button ID="btn_cancel" runat="server" Text="Clear"
                                   class="btn btn-primary" meta:resourcekey="btn_addResource1"  OnClick="btn_cancel_Click" BackColor="#428BCA"  />
                         
                               
                          
                                   &nbsp&nbsp <asp:Button ID="btnsearch" runat="server" Text="Search"
                                   class="btn btn-primary"  meta:resourcekey="btn_addResource1"  OnClick="btn_search_Click" BackColor="#428BCA"  />
                            
                                   &nbsp&nbsp--%>
                            <asp:Button ID="btnupdate" runat="server" Text="Update"
                                class="btn btn-primary" OnClientClick="return Validate();" meta:resourcekey="btn_addResource1" OnClick="btnreset_Click" BackColor="#428BCA" />

                            <%-- &nbsp&nbsp <asp:Button ID="btnassign_priv" runat="server" Text="Update Access"
                                   class="btn btn-primary"  meta:resourcekey="btn_addResource1"  OnClick="assign_priv" BackColor="#428BCA"  />--%>

                            <asp:Button ID="btnclose" runat="server" Text="Close"
                                class="btn btn-danger" meta:resourcekey="btn_addResource1" OnClick="btnclose_Click" />

                        </div>


                        <%--           
                
                <br />
                    <br />
            
                                <div class="table-responsive" >
                                            <asp:GridView ID="GridView1" class="table" style="margin:auto;" runat="server" CellPadding="4" DataKeyNames="USER_NAME" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_OnSelectedIndexChanged">
                                                    <EditRowStyle BackColor="#999999" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    <Columns>
                                                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" />
                                                    </Columns>
                                            </asp:GridView>
                                </div>
                        --%>
                    </div>
                </div>
            </div>

        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server">

            <div class="container">
                <div class="table-responsive">
                    <table class='table borderless'>
                        <tr>
                            <td style="align=center" class="auto-style1">
                                <%-- <asp:Panel ID="Panel4" runat="server" Enabled="True" style="margin-top: 23px" Width="444px">
                                    <br />
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="container-fluid">
                                              <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataKeyNames="USER_NAME" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_OnSelectedIndexChanged" Width="95%">
                                                    <EditRowStyle BackColor="#999999" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    <Columns>
                                                        <asp:CommandField HeaderText="-&gt;" SelectText="-&gt;" ShowSelectButton="True" />
                                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </asp:Panel>--%>
                                <%-- <div style="font-family: @Arial Unicode MS">
                    <asp:Panel ID="Panel1" runat="server" >
                        <div align="left" style="width: 167px;">
                            <asp:TreeView
                                ID="TreeView1"
                                ExpandDepth="0"
                                PopulateNodesFromClient="true"
                                ShowLines="true"
                                ShowExpandCollapse="true"
                                runat="server"
                                OnTreeNodePopulate="TreeView1_TreeNodePopulate" ShowCheckBoxes="all" onclick="OnTreeClick(event)" />
                        </div>
                    </asp:Panel>
                </div>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </div>
        </asp:Panel>
        <%--   </ContentTemplate></asp:UpdatePanel> --%>
    </div>
</asp:Content>
